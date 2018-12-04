using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace GameNetwork
{
    /// <summary>
    /// A stream wrapper for KCP
    /// </summary>
    class KcpStream : Stream
    {
        private const int MaxPacketSize = 64 * 1024; // 64 KiB
        private bool m_OwnsSocket;
        private Socket m_Socket;
        private KCP m_Kcp;
        private byte[] m_Buffer = new byte[MaxPacketSize];
        private bool m_Closed = false;
        private Timer m_UpdateTimer;
        private NetByteBuf m_ReceiveBuf;

        public KcpStream(Socket socket) : this(socket, false) { }

        public KcpStream(Socket socket, bool ownsSocket)
        {
            if (socket == null) throw new ArgumentNullException("socket");
            m_Socket = socket;
            m_OwnsSocket = ownsSocket;
            m_Kcp = new KCP((uint)new Random().Next(1, Int32.MaxValue), PumpOut);
            m_Kcp.NoDelay(1, 10, 2, 1);
            m_Kcp.WndSize(256, 256);
            m_Kcp.SetMtu(1464);
            m_ReceiveBuf = new NetByteBuf(2 * MaxPacketSize);
            m_UpdateTimer = new Timer((o) =>
            {
                if (m_Closed) return;
                lock (m_Kcp)
                {
                    m_Kcp.Update((uint)Connection.GetClockMS());
                }
            }, null, 10, 10);
        }

        public override bool CanRead { get { return !m_Closed; } }

        public override bool CanSeek { get { return false; } }

        public override bool CanWrite { get { return !m_Closed; } }

        // 仅限单一线程调用
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (m_Closed) throw new ObjectDisposedException(this.GetType().FullName);
            if (buffer == null) throw new ArgumentNullException("buffer");
            if (offset < 0 || offset > buffer.Length) throw new ArgumentOutOfRangeException("offset");
            if (count < 0 || count > buffer.Length - offset) throw new ArgumentOutOfRangeException("count");

            // 从Socket缓存转移到KCP缓存
            if (m_Socket.Available > 0) PumpIn();

            int read = 0;
            do
            {
                // 从KCP缓存转移到NetByteBuf缓存
                lock (m_Kcp)
                {
                    for (int size = m_Kcp.PeekSize(); size > 0 && m_ReceiveBuf.WritableBytes() >= size; size = m_Kcp.PeekSize())
                    {
                        if (m_Kcp.Recv(m_Buffer, 0, size) > 0)
                        {
                            m_ReceiveBuf.WriteBytes(m_Buffer, 0, size);
                        }
                    }
                }

                read = m_ReceiveBuf.ReadableBytes();
                if (read > 0)
                {
                    if (read > count) read = count;
                    m_ReceiveBuf.ReadBytes(buffer, offset, read);
                    m_ReceiveBuf.ResetBuffer();
                }
                else if (PumpIn() <= 0) // 如果没有数据可供读取，阻塞
                {
                    return 0;
                }
            } while (read <= 0);
            return read;
        }

        private int PumpIn()
        {
            int len = 0;
            try
            {
                len = m_Socket.Receive(m_Buffer);
            }
            catch (Exception)
            {
                return 0;
            }
            if (len > 0)
            {
                lock (m_Kcp)
                {
                    m_Kcp.Input(m_Buffer, len);
                }
            }
            return len;
        }

        // 仅限单一线程调用
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (m_Closed) throw new ObjectDisposedException(this.GetType().FullName);
            if (buffer == null) throw new ArgumentNullException("buffer");
            if (offset < 0 || offset > buffer.Length) throw new ArgumentOutOfRangeException("offset");
            if (count < 0 || count > buffer.Length - offset) throw new ArgumentOutOfRangeException("count");

            lock (m_Kcp)
            {
                m_Kcp.Send(buffer, offset, count);
            }
        }

        private void PumpOut(byte[] buff, int size)
        {
            if (m_Closed) return;
            if (m_Socket.Send(buff, 0, size, SocketFlags.None) != size)
            {
                UnityEngine.Debug.LogWarningFormat("PumpOut fail!");
            }
        }

        public override void Close()
        {
            m_Closed = true;
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && m_OwnsSocket)
            {
                if (m_Socket != null)
                {
                    m_Socket.Close();
                    m_Socket = null;
                }
                if (m_UpdateTimer != null)
                {
                    using (var waitHandle = new AutoResetEvent(false))
                    {
                        m_UpdateTimer.Dispose(waitHandle);
                        waitHandle.WaitOne();
                    }
                    m_UpdateTimer = null;
                }
            }
            base.Dispose(disposing);
        }

        #region NotSupported
        public override void Flush()
        {
        }

        public override long Length { get { throw new NotSupportedException(); } }

        public override long Position
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }
        #endregion
    }
}
