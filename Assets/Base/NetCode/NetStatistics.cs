using System.Threading;

namespace GameNetwork
{
    /// <summary>
    /// 网络流量统计，主要用到以下几个静态方法
    /// NetStatistics.Enabled = true 开启流量统计
    /// Clear() 清空历史记录，即发送和接收的字节数清零
    /// BytesSend 获取发送字节数
    /// BytesRecive 获取接收字节数
    /// BytesTotal 获取发送和接收总字节数
    /// 此类的所有成员线程安全
    /// 统计量为传输层协议（TCP/UDP/KCP）的有效载荷
    /// </summary>
    public static class NetStatistics
    {
        private static long m_Send, m_Receive;
        private static bool m_Enabled = true;

        /// <summary>
        /// 是否开启标记
        /// </summary>
        public static bool Enabled
        {
            set { m_Enabled = value; }
            get { return m_Enabled; }
        }

        /// <summary>
        /// 清零
        /// </summary>
        public static void Clear()
        {
            Interlocked.Exchange(ref m_Send, 0);
            Interlocked.Exchange(ref m_Receive, 0);
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="size">数据量（字节）</param>
        public static void OnReceive(long size)
        {
            if (m_Enabled)
                Interlocked.Add(ref m_Receive, size);
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="size">数据量（字节）</param>
        public static void OnSend(long size)
        {
            if (m_Enabled)
                Interlocked.Add(ref m_Send, size);
        }

        /// <summary>
        /// 获取发送的字节数
        /// </summary>
        public static long BytesSent
        {
            get { return Interlocked.Read(ref m_Send); }
        }

        /// <summary>
        /// 获取接收的字节数
        /// </summary>
        public static long BytesReceived
        {
            get { return Interlocked.Read(ref m_Receive); }
        }

        /// <summary>
        /// 获取发送和接收的总字节数
        /// </summary>
        public static long BytesTotal
        {
            get { return BytesSent + BytesReceived; }
        }
    }
}
