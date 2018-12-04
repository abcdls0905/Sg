using System;
using System.Net;

namespace GameNetwork
{
    public enum ConnectionError
    {
        None,
        ConnectFailed,
        NoConnResult,
        ServerClose,
        DataOverLimit,
        UnPackFailed,
        SplitFailed,
        HeartTimeout,
        WriteBufferError,
        ReadBufferError,
        RecvOverLimit,
        WorkThreadException,
    }

    public enum ConnectionState
    {
        Undefined,
        PrepareConnect,
        Connected,
        Closed,
    }

    public enum ConnectionType
    {
        Undefined,
        TCP,
        UDP,
        KCP,
    }

    public class ConnectionParam
    {
        /// <summary>
        /// 直接连接到的远程地址（代理服或网关服）
        /// </summary>
        public IPEndPoint RemoteEndPoint { get; private set; }

        /// <summary>
        /// 需要代理转接到的网关地址。不使用代理时设置为null
        /// </summary>
        public IPEndPoint GatewayEndPoint = null;

        public ConnectionType type = ConnectionType.Undefined;
        public int connectTimeout = 2000;
        [Obsolete("无论设置为什么值，将始终进行异步连接")]
        public bool connectBlock = true;    // 连接阻塞
        public volatile bool checkTimeout = true;    // 检测断线重连
        public int ackTimeout = 15000;
        public bool secure = false; // 建立安全连接

        public int ReceiveBufferSize = 8192;
        public int SendBufferSize = 8192;
        public int MaxPackSize = 4096;
        public int MaxSendBufferSize = 1024 * 1024;
        public int MaxReceiveBufferSize = 1024 * 1024;
        public bool NoDelay = true;
        public int MaxSplitPackSize = 1441;
        public int HeartTickTime = 1000;
        public int MaxPackPoolNum = 8;
        public string IP;
        public int Port;
        public ConnectionParam(string ip, int port, ConnectionType type)
        {
            IP = ip;
            Port = port;
            IPAddress ipAddress;
            if (!IPAddress.TryParse(ip, out ipAddress))
                throw new Exception("Provided remoteIPAddress string was not succesfully parsed.");

            RemoteEndPoint = new IPEndPoint(ipAddress, port);
            this.type = type;
        }

        public void ResetAddress(string ip, int port)
        {
            IP = ip;
            Port = port;
            IPAddress ipAddress;
            if (!IPAddress.TryParse(ip, out ipAddress))
                throw new Exception("Provided remoteIPAddress string was not succesfully parsed.");

            RemoteEndPoint = new IPEndPoint(ipAddress, port);
        }

        private string _cacheString = null;
        public override string ToString()
        {
            if (_cacheString == null)
                _cacheString = "[" + type.ToString() + "] " + RemoteEndPoint.Address + ":" + RemoteEndPoint.Port.ToString();
            return _cacheString;
        }
    }
}