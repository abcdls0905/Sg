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
        /// ֱ�����ӵ���Զ�̵�ַ������������ط���
        /// </summary>
        public IPEndPoint RemoteEndPoint { get; private set; }

        /// <summary>
        /// ��Ҫ����ת�ӵ������ص�ַ����ʹ�ô���ʱ����Ϊnull
        /// </summary>
        public IPEndPoint GatewayEndPoint = null;

        public ConnectionType type = ConnectionType.Undefined;
        public int connectTimeout = 2000;
        [Obsolete("��������Ϊʲôֵ����ʼ�ս����첽����")]
        public bool connectBlock = true;    // ��������
        public volatile bool checkTimeout = true;    // ����������
        public int ackTimeout = 15000;
        public bool secure = false; // ������ȫ����

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