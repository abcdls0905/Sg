using System;
using System.Collections.Generic;
using System.Threading;

namespace GameNetwork
{
    public interface NetProtocol
    {
        INetPack ReadPack(NetByteBuf buf);
        bool WritePack(INetPack node, NetByteBuf buf);
    }

    public interface INetPack
    {
        int GetByteSize();
        void Serialize(NetByteBuf buf);
        void Deserialize(NetByteBuf buf, ushort packLen);
        bool MergePack(ref byte[] mergeCache, ref int mergeCacheSize);
        bool SplitPack(int SubPackageSize, List<INetPack> splitPackList);
        void Reset();
    }

    // ���̺߳������̵߳�����ͨ��
    public class NetQueue : IDisposable
    {
        private Queue<INetPack> queues = new Queue<INetPack>();
        private int size = 0;
        private int maxSize = 2 * 1024 * 1024; // 2 MiB
        private AutoResetEvent dataAvailable = new AutoResetEvent(false);
        private AutoResetEvent spaceAvailable = new AutoResetEvent(true);
        private object lockQueue = new object();

        /// <summary>
        /// �ӽ��ն���ȡ����������Ϊ��ʱ���������÷�
        /// </summary>
        /// <returns>�����</returns>
        public INetPack Pop()
        {
            if (disposedValue) throw new ObjectDisposedException("NetQueue");
            INetPack pack;
            while (!PopInternal(out pack))
            {
                dataAvailable.WaitOne();
            }
            return pack;
        }

        /// <summary>
        /// ���Դӽ��ն���ȡ������������
        /// </summary>
        /// <param name="pack">�����</param>
        /// <returns>�Ƿ�ɹ�</returns>
        public bool TryPop(out INetPack pack)
        {
            if (disposedValue) throw new ObjectDisposedException("NetQueue");
            return PopInternal(out pack);
        }

        private bool PopInternal(out INetPack pack)
        {
            lock (lockQueue)
            {
                if (queues.Count > 0)
                {
                    pack = queues.Dequeue();
                    size -= pack.GetByteSize();
                    spaceAvailable.Set();
                    return true;
                }
            }
            pack = null;
            return false;
        }

        /// <summary>
        /// ������ӵ����Ͷ��У�����Ϊ��ʱ���������÷�
        /// </summary>
        /// <param name="pack">Ҫ���͵İ�</param>
        public void Push(INetPack pack)
        {
            if (disposedValue) throw new ObjectDisposedException("NetQueue");
            while (!PushInternal(pack))
            {
                spaceAvailable.WaitOne();
            }
        }

        /// <summary>
        /// ���Խ�����ӵ����Ͷ��У�������
        /// </summary>
        /// <param name="pack">Ҫ���͵İ�</param>
        /// <returns>�Ƿ�ɹ�</returns>
        public bool TryPush(INetPack pack)
        {
            if (disposedValue) throw new ObjectDisposedException("NetQueue");
            return PushInternal(pack);
        }

        private bool PushInternal(INetPack node)
        {
            lock (lockQueue)
            {
                int newSize = size + node.GetByteSize();
                if (newSize <= maxSize)
                {
                    queues.Enqueue(node);
                    size = newSize;
                    dataAvailable.Set();
                    return true;
                }
            }
            return false;
        }

        public int MaxSize
        {
            get
            {
                if (disposedValue) throw new ObjectDisposedException("NetQueue");
                return maxSize;
            }
            set
            {
                if (disposedValue) throw new ObjectDisposedException("NetQueue");
                if (value < 4 * 1024) throw new ArgumentOutOfRangeException();
                maxSize = value;
            }
        }

        public int Count
        {
            get
            {
                if (disposedValue) throw new ObjectDisposedException("NetQueue");
                lock (lockQueue)
                {
                    return queues.Count;
                }
            }
        }

        public int ByteSize
        {
            get
            {
                if (disposedValue) throw new ObjectDisposedException("NetQueue");
                lock (lockQueue)
                {
                    return size;
                }
            }
        }

        public void Clear()
        {
            if (disposedValue) throw new ObjectDisposedException("NetQueue");
            lock (lockQueue)
            {
                queues.Clear();
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // Ҫ����������

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // �ͷ��й�״̬(�йܶ���)��
                    dataAvailable.Close();
                    spaceAvailable.Close();
                }

                // �������ֶ�����Ϊ null��
                queues = null;

                disposedValue = true;
            }
        }

        // ��Ӵ˴�������ȷʵ�ֿɴ���ģʽ��
        public void Dispose()
        {
            // ������Ĵ˴��롣���������������� Dispose(bool disposing) �С�
            Dispose(true);
        }
        #endregion
    }
}