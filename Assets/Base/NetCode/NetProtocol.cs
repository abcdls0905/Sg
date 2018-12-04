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

    // 主线程和网络线程的数据通道
    public class NetQueue : IDisposable
    {
        private Queue<INetPack> queues = new Queue<INetPack>();
        private int size = 0;
        private int maxSize = 2 * 1024 * 1024; // 2 MiB
        private AutoResetEvent dataAvailable = new AutoResetEvent(false);
        private AutoResetEvent spaceAvailable = new AutoResetEvent(true);
        private object lockQueue = new object();

        /// <summary>
        /// 从接收队列取出包，队列为空时将阻塞调用方
        /// </summary>
        /// <returns>网络包</returns>
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
        /// 尝试从接收队列取出包，不阻塞
        /// </summary>
        /// <param name="pack">网络包</param>
        /// <returns>是否成功</returns>
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
        /// 将包添加到发送队列，队列为满时将阻塞调用方
        /// </summary>
        /// <param name="pack">要发送的包</param>
        public void Push(INetPack pack)
        {
            if (disposedValue) throw new ObjectDisposedException("NetQueue");
            while (!PushInternal(pack))
            {
                spaceAvailable.WaitOne();
            }
        }

        /// <summary>
        /// 尝试将包添加到发送队列，不阻塞
        /// </summary>
        /// <param name="pack">要发送的包</param>
        /// <returns>是否成功</returns>
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
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // 释放托管状态(托管对象)。
                    dataAvailable.Close();
                    spaceAvailable.Close();
                }

                // 将大型字段设置为 null。
                queues = null;

                disposedValue = true;
            }
        }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
        }
        #endregion
    }
}