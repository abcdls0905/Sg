using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace GameNetwork
{
    public delegate INetPack Compress(INetPack gamePack);
    public delegate INetPack UnCompress(INetPack gamePack);
    public delegate void ConnectHandler(Connection conn);
    public delegate void ConnectErrorHandler(Connection conn, ConnectionError error);
    public delegate void RecvMsgHandler(Connection conn, INetPack pack);
    public delegate INetPack SendHeartHandler(long time);
    public delegate bool RecvHeartHandler(INetPack pack, out long time);

    public class Connection
    {
        static Connection()
        {
            Stopwatch = System.Diagnostics.Stopwatch.StartNew();
        }

        static System.Diagnostics.Stopwatch Stopwatch;

        /// <summary>
        /// 网络连接使用的高精度参考时钟
        /// </summary>
        /// <returns>时钟值，单位毫秒</returns>
        public static long GetClockMS()
        {
            return Stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// 创建一个新的Connection对象
        /// </summary>
        /// <param name="param">连接参数</param>
        /// <param name="protocol">上层协议</param>
        public Connection(ConnectionParam param, NetProtocol protocol)
        {
            InitParam = param;
            InitProtocol = protocol;
            SendPool = new NetQueue();
            RecvPool = new NetQueue();
            connState = ConnectionState.Undefined;
            mergeCache = null;
            mergeCacheSize = 0;
            splitCache = new List<INetPack>();
        }
        public ConnectionParam InitParam { get; private set; }
        public NetProtocol InitProtocol { get; private set; }
        public ConnectionState GetConnectState() { return connState; }

        private Socket socket;
        private Stream stream;
        private NetQueue SendPool, RecvPool;
        private Thread m_SendThread, m_RecvThread, m_ConnThread;
        private ConnectionState connState;

        public ConnectHandler ConnectedEvent;
        public ConnectErrorHandler ConnectErrorEvent;
        public RecvMsgHandler RecvMsgEvent;
        public SendHeartHandler SendHeartEvent; // 非主线程调用
        public RecvHeartHandler RecvHeartEvent; // 非主线程调用
        private Timer m_HeartTickTimer;
        public long m_RTT;
        private long m_LastSeen; // Interlocked

        private byte[] mergeCache;
        private int mergeCacheSize;
        private List<INetPack> splitCache;
        private Compress CompressPack;
        private UnCompress UnCompressPack;
        private volatile ConnectionError m_LastError = ConnectionError.None;
        private long m_ConnectStartTime;

        private void Start()
        {
            m_RecvThread = new Thread(RunReceive)
            {
                Priority = ThreadPriority.AboveNormal
            };
            m_SendThread = new Thread(RunSend)
            {
                Priority = ThreadPriority.AboveNormal
            };
            m_HeartTickTimer = new Timer((o) =>
            {
                lock (SendPool)
                {
                    if (SendPool != null)
                        WriteHeart();
                }
            }, null, InitParam.HeartTickTime, InitParam.HeartTickTime);
            m_RecvThread.Start();
            m_SendThread.Start();
        }

        private static readonly byte[] certificate = new byte[] {
0x30, 0x82, 0x04, 0xFD, 0x30, 0x82, 0x02, 0xE5, 0xA0, 0x03, 0x02, 0x01, 0x02, 0x02, 0x11, 0x00,
0xA1, 0x8F, 0xD7, 0x55, 0x2C, 0x55, 0x60, 0x4D, 0x0A, 0x3B, 0xAF, 0xE6, 0xF9, 0x46, 0x6C, 0x17,
0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x0B, 0x05, 0x00, 0x30,
0x12, 0x31, 0x10, 0x30, 0x0E, 0x06, 0x03, 0x55, 0x04, 0x0A, 0x13, 0x07, 0x41, 0x63, 0x6D, 0x65,
0x20, 0x43, 0x6F, 0x30, 0x1E, 0x17, 0x0D, 0x31, 0x38, 0x30, 0x36, 0x30, 0x37, 0x30, 0x37, 0x31,
0x39, 0x33, 0x38, 0x5A, 0x17, 0x0D, 0x32, 0x38, 0x30, 0x36, 0x30, 0x34, 0x30, 0x37, 0x31, 0x39,
0x33, 0x38, 0x5A, 0x30, 0x12, 0x31, 0x10, 0x30, 0x0E, 0x06, 0x03, 0x55, 0x04, 0x0A, 0x13, 0x07,
0x41, 0x63, 0x6D, 0x65, 0x20, 0x43, 0x6F, 0x30, 0x82, 0x02, 0x22, 0x30, 0x0D, 0x06, 0x09, 0x2A,
0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00, 0x03, 0x82, 0x02, 0x0F, 0x00, 0x30,
0x82, 0x02, 0x0A, 0x02, 0x82, 0x02, 0x01, 0x00, 0xAB, 0x7A, 0xFE, 0x41, 0x1D, 0x82, 0x2C, 0x7A,
0x6B, 0x8B, 0x74, 0x42, 0x7B, 0xA4, 0x5B, 0x84, 0x87, 0x6F, 0x91, 0xB2, 0x83, 0xB1, 0x15, 0xD6,
0xDA, 0xA4, 0xAF, 0x10, 0x10, 0x29, 0xEC, 0xAA, 0x7F, 0x72, 0x9D, 0xD4, 0xF8, 0xB5, 0xA2, 0x91,
0xE6, 0xB3, 0x2A, 0x1C, 0x87, 0xEA, 0xA0, 0xF2, 0x56, 0x2C, 0xBA, 0x2F, 0xD1, 0x7C, 0x8A, 0x16,
0xEA, 0x85, 0xF5, 0xC1, 0x8F, 0xA5, 0xF6, 0x68, 0xBC, 0x01, 0xF3, 0x0E, 0x47, 0x4F, 0xCD, 0x46,
0x90, 0x7D, 0x35, 0x7A, 0xF0, 0x53, 0xD3, 0x09, 0x24, 0x0C, 0x90, 0x6D, 0xA8, 0xDE, 0xAD, 0x10,
0xE8, 0x27, 0x23, 0xDC, 0x5D, 0xB4, 0xD2, 0xF4, 0x8F, 0x33, 0x75, 0x21, 0x28, 0x0E, 0x38, 0xDA,
0x41, 0xFB, 0xF6, 0xFA, 0xC0, 0x21, 0xAE, 0x05, 0xE3, 0x8E, 0xF5, 0x73, 0x74, 0x7B, 0xDA, 0xA2,
0x0F, 0x68, 0x5D, 0xB6, 0x91, 0xCC, 0x95, 0x73, 0x15, 0x42, 0x77, 0x16, 0x77, 0x8B, 0x22, 0x58,
0x99, 0x49, 0xAA, 0x95, 0x40, 0xD1, 0xC8, 0x7C, 0x88, 0xA6, 0x24, 0x15, 0xB9, 0x23, 0xD6, 0x28,
0x2C, 0x86, 0x6F, 0x50, 0xB9, 0xE0, 0x9E, 0xC6, 0x5E, 0x96, 0xDE, 0xFF, 0x08, 0x23, 0x80, 0xAC,
0xA8, 0x2B, 0x18, 0x5A, 0xF3, 0x06, 0x64, 0x80, 0xEF, 0x1E, 0x30, 0x51, 0x1B, 0xD5, 0xBA, 0xC1,
0x71, 0xBD, 0x07, 0xA0, 0x55, 0x88, 0x20, 0xEA, 0xB6, 0xF1, 0x09, 0x38, 0x27, 0xAB, 0x1F, 0x6F,
0x6D, 0xBA, 0xDC, 0xA6, 0xFC, 0xC2, 0x56, 0x6B, 0xD9, 0x57, 0xC7, 0x5C, 0x27, 0xB7, 0x04, 0x6A,
0x71, 0xAA, 0x53, 0xC8, 0xD5, 0xF6, 0xE6, 0x7B, 0x4A, 0x95, 0xF9, 0x26, 0xE5, 0x6A, 0xBF, 0x67,
0x0E, 0xA0, 0x79, 0x08, 0x69, 0x26, 0x98, 0x30, 0x6F, 0xAD, 0x4C, 0xCD, 0x06, 0x66, 0xB0, 0x9C,
0xCE, 0xF6, 0x4F, 0x83, 0x3E, 0x65, 0x74, 0x88, 0x96, 0x2C, 0xED, 0x3D, 0x2B, 0x6D, 0xBC, 0x06,
0xCE, 0x6D, 0xB8, 0xCE, 0x92, 0x6A, 0xB1, 0x67, 0x52, 0xA2, 0x2F, 0xF5, 0xD4, 0x17, 0x68, 0xD8,
0x07, 0xE2, 0xFA, 0x37, 0x16, 0x68, 0x77, 0x9F, 0x55, 0xEF, 0x99, 0xB0, 0xDF, 0xEE, 0x04, 0x17,
0xD5, 0xD5, 0xD2, 0xA8, 0x86, 0xF6, 0x89, 0x55, 0x13, 0xBF, 0x23, 0xD8, 0x41, 0x58, 0x02, 0x7F,
0xDB, 0x01, 0xE0, 0x6E, 0x69, 0xDB, 0xBC, 0xB1, 0xAD, 0xB1, 0x87, 0xFA, 0x88, 0xC5, 0x57, 0xFA,
0x43, 0xFD, 0x41, 0xBB, 0xEA, 0x96, 0xA7, 0x60, 0xBA, 0x90, 0x26, 0xC7, 0xD0, 0x45, 0x8D, 0xCE,
0x8D, 0x36, 0x6C, 0x61, 0xD8, 0xB9, 0xDB, 0x5B, 0xCF, 0x7D, 0x42, 0x75, 0x53, 0x90, 0x1B, 0xBC,
0x5A, 0x6F, 0x5F, 0xC0, 0x12, 0xA8, 0xD5, 0xB7, 0xD0, 0x02, 0x14, 0x40, 0x2E, 0x40, 0xAE, 0x9F,
0xF8, 0xC8, 0x35, 0xDD, 0x75, 0x97, 0x2E, 0xF8, 0x17, 0x54, 0xDB, 0xD2, 0x66, 0x7E, 0xD5, 0x7C,
0x7A, 0xC1, 0x0C, 0x67, 0x2B, 0x04, 0xEF, 0xF2, 0xA8, 0xC5, 0x3D, 0x5D, 0x5F, 0xA4, 0x9B, 0x4A,
0xCC, 0x34, 0xD1, 0x9C, 0x69, 0x74, 0x9D, 0xBF, 0x51, 0x2D, 0x54, 0x03, 0x27, 0x0C, 0x5E, 0xB9,
0xDB, 0x16, 0xEA, 0x02, 0x58, 0xCE, 0x06, 0x36, 0x9A, 0x5D, 0x39, 0x6C, 0xED, 0xB3, 0x1B, 0x81,
0x81, 0xB9, 0x43, 0x89, 0xFD, 0xDA, 0xA0, 0xCA, 0xE7, 0x30, 0x93, 0xD6, 0xD2, 0x82, 0x2C, 0x8E,
0x63, 0x9D, 0x9B, 0x97, 0x2A, 0xF6, 0x46, 0xC2, 0x59, 0x4E, 0x79, 0x47, 0x41, 0xBD, 0xE8, 0xAE,
0x02, 0x2B, 0x9F, 0x3F, 0x4B, 0x79, 0x6B, 0xD1, 0xB5, 0xFD, 0x62, 0x2B, 0xDD, 0xB9, 0x01, 0xB6,
0x56, 0xC3, 0xD4, 0x52, 0xDC, 0x1B, 0xC0, 0xA3, 0x26, 0xAE, 0x38, 0x17, 0xD9, 0x84, 0xC2, 0x4A,
0xBA, 0x09, 0xB4, 0xDB, 0x7B, 0x73, 0x92, 0x6D, 0x02, 0x03, 0x01, 0x00, 0x01, 0xA3, 0x4E, 0x30,
0x4C, 0x30, 0x0E, 0x06, 0x03, 0x55, 0x1D, 0x0F, 0x01, 0x01, 0xFF, 0x04, 0x04, 0x03, 0x02, 0x02,
0xA4, 0x30, 0x13, 0x06, 0x03, 0x55, 0x1D, 0x25, 0x04, 0x0C, 0x30, 0x0A, 0x06, 0x08, 0x2B, 0x06,
0x01, 0x05, 0x05, 0x07, 0x03, 0x01, 0x30, 0x0F, 0x06, 0x03, 0x55, 0x1D, 0x13, 0x01, 0x01, 0xFF,
0x04, 0x05, 0x30, 0x03, 0x01, 0x01, 0xFF, 0x30, 0x14, 0x06, 0x03, 0x55, 0x1D, 0x11, 0x04, 0x0D,
0x30, 0x0B, 0x82, 0x09, 0x6C, 0x6F, 0x63, 0x61, 0x6C, 0x68, 0x6F, 0x73, 0x74, 0x30, 0x0D, 0x06,
0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x0B, 0x05, 0x00, 0x03, 0x82, 0x02, 0x01,
0x00, 0x4D, 0xC5, 0xB7, 0xFD, 0x94, 0x6B, 0x48, 0x7D, 0xD7, 0xCF, 0x62, 0x37, 0x87, 0xDA, 0xFB,
0x97, 0xE1, 0xEA, 0xC1, 0x14, 0xE3, 0x32, 0x13, 0xB2, 0xD6, 0x01, 0x07, 0xFC, 0xE1, 0xDC, 0xE0,
0x2E, 0x20, 0x2D, 0x4C, 0x5F, 0x61, 0x25, 0xB0, 0x96, 0x78, 0x49, 0x74, 0x17, 0x31, 0xCA, 0x1E,
0x0C, 0x9F, 0xBC, 0xF2, 0x10, 0x6A, 0x01, 0x40, 0x14, 0xCB, 0xC9, 0x9C, 0x4D, 0xB5, 0xC9, 0xFC,
0xF4, 0x22, 0x51, 0xD4, 0x56, 0x07, 0x07, 0xDD, 0xD2, 0xFB, 0x00, 0xE0, 0xBD, 0x89, 0xFF, 0x70,
0x34, 0x11, 0x48, 0x61, 0xBC, 0x8E, 0xAB, 0x4A, 0xBA, 0xC3, 0xD6, 0x67, 0xB9, 0x7B, 0xDE, 0x6C,
0xDB, 0x9A, 0xAC, 0x5D, 0x63, 0x9A, 0x05, 0xE1, 0x92, 0x78, 0xB0, 0x49, 0x14, 0x83, 0x2C, 0x55,
0x4D, 0x4A, 0x1D, 0x69, 0x26, 0x54, 0xE6, 0xD3, 0x47, 0xD8, 0xB6, 0x2C, 0xC8, 0x97, 0xAC, 0x44,
0xBD, 0x27, 0x78, 0xC6, 0x80, 0xB4, 0x8B, 0x20, 0x01, 0x86, 0x8B, 0xA3, 0x94, 0xFB, 0xF2, 0xA0,
0xAE, 0x0E, 0x5E, 0x50, 0x6C, 0x97, 0xAC, 0x9C, 0xC3, 0x16, 0x85, 0xD6, 0x9A, 0xC7, 0xD1, 0xF8,
0x4F, 0x83, 0xA0, 0x98, 0x37, 0xA9, 0xE1, 0x2A, 0xF9, 0x3F, 0xEF, 0xDC, 0xBE, 0xFE, 0xD0, 0x2F,
0xC6, 0x85, 0x17, 0xCD, 0x52, 0xAD, 0x31, 0x4E, 0x4E, 0x98, 0x01, 0x57, 0xD9, 0x26, 0x21, 0xFD,
0xE0, 0x8D, 0xC2, 0x26, 0xBF, 0x34, 0xBA, 0xD2, 0x5E, 0xCE, 0xED, 0xD6, 0x67, 0x4C, 0xD9, 0xD7,
0x90, 0xF1, 0xF0, 0xE9, 0x98, 0xE1, 0xC6, 0x7C, 0xA7, 0x6B, 0x5C, 0x3B, 0x7E, 0x78, 0x6C, 0x3F,
0x5C, 0x53, 0x4B, 0xDE, 0xEB, 0x30, 0x52, 0xB1, 0xBB, 0xD1, 0xF9, 0xF7, 0x5E, 0x3C, 0x3A, 0xF5,
0xA3, 0xD3, 0x3D, 0x1D, 0xDB, 0x38, 0x53, 0xC1, 0xFA, 0x41, 0x32, 0x15, 0xCB, 0x5F, 0xB5, 0x0B,
0xE9, 0x84, 0xC3, 0xDC, 0xD0, 0xBE, 0xA4, 0xF5, 0x33, 0x66, 0x4D, 0xFA, 0xDA, 0xCA, 0xC7, 0xD0,
0xC4, 0x56, 0xB7, 0x37, 0x28, 0xFF, 0x3A, 0x70, 0xB5, 0x60, 0xDD, 0x74, 0xE6, 0x29, 0xD3, 0x3E,
0xA9, 0x29, 0x1A, 0xDC, 0xA8, 0x39, 0x2B, 0xEF, 0x06, 0xB6, 0x05, 0xA0, 0x69, 0x8C, 0xE0, 0x73,
0xF5, 0xF7, 0xEF, 0x75, 0x32, 0x38, 0x42, 0xB5, 0xA7, 0x24, 0x02, 0xE0, 0x0C, 0x7A, 0xD6, 0x29,
0x93, 0x49, 0xBA, 0x1D, 0x65, 0x52, 0x62, 0x73, 0xB5, 0x17, 0x62, 0x3C, 0xEB, 0x93, 0x62, 0xA0,
0x4F, 0x82, 0x98, 0xC5, 0x65, 0x7E, 0x2F, 0xB3, 0x8C, 0x13, 0xA0, 0x1A, 0xB3, 0xAD, 0x41, 0x8B,
0xDA, 0xCA, 0x66, 0xB5, 0xBE, 0x61, 0x42, 0x34, 0xA0, 0xD0, 0x71, 0xDD, 0x88, 0x4E, 0x38, 0xFE,
0xC0, 0x4D, 0xF5, 0xCE, 0x24, 0x4B, 0x25, 0x42, 0xFA, 0x09, 0x6C, 0xE7, 0x00, 0x4F, 0x11, 0x63,
0x36, 0x45, 0x01, 0x65, 0xF8, 0x0D, 0xE4, 0x0B, 0x96, 0xE1, 0x89, 0xFA, 0x5F, 0x47, 0x29, 0x8B,
0x8A, 0xFD, 0x1C, 0x83, 0x1A, 0xE7, 0xB2, 0x91, 0x9B, 0xC0, 0x78, 0x4C, 0x30, 0xA4, 0xAA, 0xB0,
0x33, 0x20, 0x01, 0x6E, 0x82, 0x9F, 0x75, 0xBB, 0x3F, 0x3A, 0xCC, 0x69, 0x3D, 0x60, 0x1A, 0xC9,
0x80, 0x23, 0xF6, 0x70, 0xDE, 0x72, 0x1C, 0xE9, 0x48, 0x30, 0xF6, 0x09, 0xEC, 0x52, 0x5A, 0x49,
0xA4, 0x05, 0x4C, 0xA6, 0x24, 0xCF, 0xE7, 0x61, 0xE3, 0xED, 0x36, 0xB7, 0x83, 0x55, 0xFD, 0x55,
0xCB, 0x15, 0x10, 0xC2, 0xCB, 0xF8, 0x2A, 0x7C, 0xBF, 0xBB, 0xA5, 0x83, 0x9C, 0xF8, 0x26, 0xA7,
0xD0, 0xF1, 0xE8, 0xBF, 0x37, 0x82, 0x68, 0xC6, 0xD9, 0xB2, 0x9F, 0x32, 0x33, 0x68, 0xFB, 0x17,
0xA7, 0x13, 0xCF, 0xA9, 0x30, 0xDF, 0xBD, 0xEB, 0x14, 0x7B, 0xB9, 0xB3, 0x73, 0x0A, 0x0D, 0x85,
0xF6,
};

        /// <summary>
        /// 开始连接到服务器
        /// </summary>
        public void Connect()
        {
            if (connState != ConnectionState.Undefined)
                throw new InvalidOperationException();

            m_ConnThread = new Thread(RunConnect);
            m_ConnThread.Start();
            m_ConnectStartTime = GetClockMS();

            connState = ConnectionState.PrepareConnect;
        }

        private void ConnectError(ConnectionError error)
        {
            CloseInternal();

            if (ConnectErrorEvent != null)
                ConnectErrorEvent(this, error);
        }

        // 主线程调用
        public void Update()
        {
            switch (connState)
            {
                case ConnectionState.Undefined:
                    break;
                case ConnectionState.PrepareConnect:
                    if (!m_ConnThread.IsAlive)
                    {
                        if (m_LastError != ConnectionError.None)
                        {
                            ConnectError(m_LastError);
                            return;
                        }
                        m_ConnThread = null;
                        connState = ConnectionState.Connected;
                        Interlocked.Exchange(ref m_LastSeen, GetClockMS());

                        if (ConnectedEvent != null)
                            ConnectedEvent(this);

                        Start();
                    }
                    else if (m_ConnectStartTime + InitParam.connectTimeout < GetClockMS())
                    {
                        ConnectError(ConnectionError.ConnectFailed);
                        return;
                    }
                    break;
                case ConnectionState.Connected:
                    if (m_LastError != ConnectionError.None)
                    {
                        ConnectError(m_LastError);
                        return;
                    }
                    RecvData();
                    break;
                case ConnectionState.Closed:
                    break;
                default: throw new NotImplementedException();
            }
        }

        //如果需要压缩和解压缩，需要调用此方法传递压缩与解压缩函数
        public void InitCompressMethod(Compress compressPack = null, UnCompress unCompress = null)
        {
            CompressPack = compressPack;
            UnCompressPack = unCompress;
        }

        [Obsolete("使用GetConnectState代替")]
        public bool connected
        {
            get
            {
                return connState == ConnectionState.Connected;
            }
        }

        //isCompress表示是否压缩，isSplit表示是否分包
        public void SendData(INetPack pack, bool isCompress = false, bool isSplit = false)
        {
            if (connState != ConnectionState.Connected)
                throw new InvalidOperationException();

            if (pack.GetByteSize() > InitParam.MaxPackSize || SendPool.ByteSize > InitParam.MaxSendBufferSize)
            {
                UnityEngine.Debug.LogWarningFormat("SendDataFail! PackSize = {0}, SendPoolSize = {1}", pack.GetByteSize(), SendPool.ByteSize);
                ConnectError(ConnectionError.DataOverLimit);
                return;
            }

            //如果有压缩标记则压缩
            if (isCompress)
            {
                if (CompressPack != null)
                    pack = CompressPack(pack);
                //else
                //    UnityEngine.Debug.LogWarning("Connection: The compress method is null");
            }

            //如果有分包标记则标记
            if (isSplit)
            {
                if (pack.SplitPack(InitParam.MaxSplitPackSize, splitCache))
                {
                    for (int i = 0; i < splitCache.Count; ++i)
                        SendPool.Push(splitCache[i]);
                    splitCache.Clear();
                }
                else
                    SendPool.Push(pack);
            }
            else
                SendPool.Push(pack);
        }

        /// <summary>
        /// 主线程主动检测网络消息
        /// </summary>
        private void RecvData()
        {
            if (RecvPool.ByteSize > InitParam.MaxReceiveBufferSize)
            {
                ConnectError(ConnectionError.RecvOverLimit);
                return;
            }
            INetPack pack;
            while (RecvPool.TryPop(out pack))
            {
                if (!pack.MergePack(ref mergeCache, ref mergeCacheSize))
                {
                    if (UnCompressPack != null)
                        pack = UnCompressPack(pack);
                    //else
                    //    UnityEngine.Debug.LogWarning("Connection: The uncompress method is null");

                    if (RecvMsgEvent != null)
                    {
                        RecvMsgEvent(this, pack);
                    }
                }
            }
        }

        #region 心跳

        // 网络线程调用
        private bool WriteHeart()
        {
            if (SendHeartEvent == null)
                return false;
            INetPack heartPack = SendHeartEvent(GetClockMS());
            if (heartPack != null)
                SendPool.Push(heartPack);
            return true;
        }

        // 网络线程调用
        private bool ReadHeart(INetPack pack)
        {
            if (RecvHeartEvent == null)
                return false;
            long timestamp;
            bool ret = RecvHeartEvent(pack, out timestamp);
            if (!ret)
                return false;
            m_RTT = GetClockMS() - timestamp;
            // UnityEngine.Debug.LogFormat("RTT: {0}ms", m_RTT);
            return true;
        }

        /// <summary>
        /// 获取网络延迟，单位毫秒
        /// </summary>
        public long RTT { get { return m_RTT; } }

        #endregion

        #region 线程
        // 连接线程入口点
        public void RunConnect()
        {
            try
            {
                ConnectInternal();
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
                m_LastError = ConnectionError.ConnectFailed;
            }
        }

        /// <summary>
        /// 组装协议栈并执行握手
        /// </summary>
        private void ConnectInternal()
        {
            if (InitParam.type == ConnectionType.TCP)
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            else // KCP & UDP
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            }
            socket.ReceiveBufferSize = InitParam.ReceiveBufferSize;
            socket.SendBufferSize = InitParam.SendBufferSize;
            socket.Connect(InitParam.RemoteEndPoint);
            if (InitParam.type == ConnectionType.KCP)
            {
                stream = new KcpStream(socket, true);
            }
            else // TCP & UDP
            {
                stream = new NetworkStream(socket, true);
            }
            // 透明代理协议
            if (InitParam.type == ConnectionType.TCP)
                stream = new ProxyStream(stream, InitParam.GatewayEndPoint);
            if (InitParam.secure)
            {
                var cert = new X509Certificate2(certificate);
                var s = new SslStream(stream, false, new RemoteCertificateValidationCallback(
                    (sender, c, chain, sslPolicyErrors) => c.Equals(cert)
                ));

                s.AuthenticateAsClient("ignored", new X509CertificateCollection { cert }, SslProtocols.Tls, false);
                if (!s.IsAuthenticated) throw new AuthenticationException("认证失败");
                if (!s.IsEncrypted) throw new AuthenticationException("连接未加密");
                if (!s.IsSigned) throw new AuthenticationException("连接未签名");
                stream = s;
            }
        }

        // 发送线程入口点
        private void RunSend()
        {
            NetByteBuf sendBuffer = new NetByteBuf(InitParam.SendBufferSize);
            do
            {
                // 写入buffer
                try
                {
                    INetPack pack = SendPool.Pop();
                    if (!InitProtocol.WritePack(pack, sendBuffer))
                    {
                        throw new Exception("WritePack OverFlow!");
                    }

                    if (sendBuffer.ReadableBytes() > 0)
                    {
                        int count = sendBuffer.ReadableBytes();
                        stream.Write(sendBuffer.Raw, sendBuffer.ReaderIndex, count);
                        sendBuffer.ReaderIndex += count;
                        NetStatistics.OnSend(count);
                        sendBuffer.ResetBuffer();
                    }
                }
                catch (IOException) { break; } // socket被本机主动关闭
                catch (ThreadInterruptedException) { break; }
                catch (Exception ex)
                {
                    if (connState == ConnectionState.Closed) break;
                    m_LastError = ConnectionError.WorkThreadException;
                    UnityEngine.Debug.LogException(ex);
                }
            } while (m_SendThread != null) ;
            // UnityEngine.Debug.Log("Send thread exiting normally.");
        }

        // 接收线程入口点
        private void RunReceive()
        {
            NetByteBuf recvBuffer = new NetByteBuf(InitParam.ReceiveBufferSize);
            do
            {
                try
                {
                    int count = stream.Read(recvBuffer.Raw, recvBuffer.WriterIndex, recvBuffer.WritableBytes());
                    if (count <= 0)
                    {
                        // 对方断开连接
                        m_LastError = ConnectionError.ServerClose;
                        break;
                    }
                    recvBuffer.WriterIndex += count;
                    NetStatistics.OnReceive(count);
                    Interlocked.Exchange(ref m_LastSeen, GetClockMS());
                    while (recvBuffer.ReadableBytes() > 0)
                    {
                        INetPack pack = InitProtocol.ReadPack(recvBuffer);
                        if (pack != null)
                        {
                            if (!ReadHeart(pack))
                                RecvPool.Push(pack);
                        }
                        else break;
                    }
                    recvBuffer.ResetBuffer();
                }
                catch (IOException) { break; } // socket被本机主动关闭
                catch (ThreadInterruptedException) { break; }
                catch (Exception ex)
                {
                    if (connState == ConnectionState.Closed) break;
                    m_LastError = ConnectionError.WorkThreadException;
                    UnityEngine.Debug.LogException(ex);
                }
            } while (m_RecvThread != null);
            // UnityEngine.Debug.Log("Receive thread exiting normally.");
        }
        #endregion

        /// <summary>
        /// 关闭此Connection实例，并释放所有相关的资源
        /// </summary>
        public void Close()
        {
            if (connState == ConnectionState.Closed) return;
            CloseInternal();
        }

        private void CloseInternal()
        {
            UnityEngine.Debug.Log("Closing");

            connState = ConnectionState.Closed;
            if (m_HeartTickTimer != null)
            {
                m_HeartTickTimer.Dispose();
                m_HeartTickTimer = null;
            }
            if (m_ConnThread != null)
            {
                m_ConnThread.Interrupt();
                m_ConnThread = null;
            }
            if (m_SendThread != null)
            {
                m_SendThread.Interrupt();
                m_SendThread = null;
            }
            if (m_RecvThread != null)
            {
                m_RecvThread.Interrupt();
                m_RecvThread = null;
            }
            try
            {
                if (stream != null)
                {
                    // This will close socket gracefully
                    stream.Close();
                }
                else if (socket != null && socket.Connected)
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogWarning(e);
            }
            stream = null;
            socket = null;
            mergeCache = null;
            mergeCacheSize = 0;
            if (SendPool != null)
            {
                SendPool.Dispose();
                SendPool = null;
            }
            if (RecvPool != null)
            {
                RecvPool.Dispose();
                RecvPool = null;
            }
        }

        //压缩解压方法参考如下，如下方法对SnappyPI有依赖，因此修改成为接口形式，可使用InitCompressMethod方法从上层传递压缩与解压缩的方法。
        /*
        protected GameNetPack CompressPack(GameNetPack gamePack)
        {
            int compressedSize = SnappyPI.SnappyCodec.GetMaximumCompressedLength(gamePack.body.Length);
            int compressRet = SnappyPI.SnappyCodec.Compress(gamePack.body, gamePack.body.Length, lzoMsg, ref compressedSize);
            if (compressRet == 0 && (compressedSize < gamePack.body.Length))
            {
                gamePack.flag |= 0x02;
                byte[] compressedMsg = new byte[compressedSize];
                Buffer.BlockCopy(lzoMsg, 0, compressedMsg, 0, compressedSize);
                gamePack.body = compressedMsg;
            }
            return gamePack;
        }

        protected GameNetPack UnCompressPack(GameNetPack gamePack)
        {
            int outputSize = lzoMsg.Length;
            Array.Clear(lzoMsg, 0, lzoMsg.Length);
            int errid = SnappyPI.SnappyCodec.Uncompress(gamePack.body, gamePack.body.Length, lzoMsg, ref outputSize);
            byte[] temp = new byte[outputSize];
            Buffer.BlockCopy(lzoMsg, 0, temp, 0, outputSize);
            gamePack.body = temp;
            return gamePack;
        }*/
    }
}