using System;
using System.IO;
using System.Net;

namespace GameNetwork
{
    class ProxyStream : Stream
    {
        private Stream m_BaseStream;

        /// <summary>
        /// 创建新的不使用代理的ProxyStream实例
        /// </summary>
        /// <param name="baseStream">基础流</param>
        public ProxyStream(Stream baseStream) : this(baseStream, null)
        {
        }

        /// <summary>
        /// 创建新的ProxyStream实例
        /// </summary>
        /// <param name="baseStream">基础流</param>
        /// <param name="server">网关服务器地址和端口</param>
        public ProxyStream(Stream baseStream, IPEndPoint server)
        {
            if (baseStream == null) throw new ArgumentNullException("baseStream");
            m_BaseStream = baseStream;

            if (server != null)
            {
                byte[] address = server.Address.GetAddressBytes();
                m_BaseStream.Write(address, 0, address.Length);
                byte[] port = BitConverter.GetBytes(server.Port);
                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(port);
                m_BaseStream.Write(port, 0, port.Length);
            }

            // Receive hello message
            var buf = new byte[1];
            m_BaseStream.Read(buf, 0, 1);
            if (buf[0] != 0) throw new ProtocolViolationException();

            // 提供透明代理协议支持
            const int proxyHeaderLength = 25;
            m_BaseStream.Write(new byte[proxyHeaderLength], 0, proxyHeaderLength);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_BaseStream.Dispose();
            }
            base.Dispose(disposing);
        }

        #region CallBase
        public override bool CanRead { get { return m_BaseStream.CanRead; } }

        public override bool CanSeek { get { return m_BaseStream.CanSeek; } }

        public override bool CanWrite { get { return m_BaseStream.CanWrite; } }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return m_BaseStream.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            m_BaseStream.Write(buffer, offset, count);
        }

        public override void Close()
        {
            m_BaseStream.Close();
        }

        public override void Flush()
        {
            m_BaseStream.Flush();
        }

        public override long Length { get { return m_BaseStream.Length; } }

        public override long Position
        {
            get { return m_BaseStream.Position; }
            set { m_BaseStream.Position = value; }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return m_BaseStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            m_BaseStream.SetLength(value);
        }
        #endregion
    }
}
