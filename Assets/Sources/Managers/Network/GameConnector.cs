using Google.Protobuf;
using GameNetwork;
using UnityEngine;
using GameLua;
using UniRx;
using System;
using System.Collections;
using System.Net;

namespace Game
{
    // 暂且将回调函数置入Lua
    public class GameConnector
    {
        private Connection connection;
        private IDisposable connectAsync;
        private NetworkType nType = NetworkType.NT_None;
        private int gsId;
        public uint Sequence { get; set; }
        public uint ConnID { get; set; }
        public bool IsConnected { get; set; }
        public long PingTime { get { return connection.RTT; } }

        public string RealIP;
        public int RealPort;

        public int SendBytes;
        public int RecvBytes;

        public bool IsSvrDisconnect;
        public GameConnector(ConnectionParam param, NetworkType type, int gsid)
        {
            if (VersionManager.Instance.Proxy)
            {
                RealIP = param.IP;
                RealPort = param.Port;
                param = new ConnectionParam(VersionManager.Instance.ProxyIP, VersionManager.Instance.ProxyPort, ConnectionType.TCP);
            }

            GameProtocol_Optimize protocol = new GameProtocol_Optimize();
            connection = new Connection(param, protocol);
            connection.ConnectedEvent += OnConnected;
            connection.ConnectErrorEvent += OnConnectError;
            connection.RecvMsgEvent += OnRecvMsg;
            connection.SendHeartEvent += OnSendHeart;
            connection.RecvHeartEvent += OnRecvHeart;
            connection.InitCompressMethod(CompressPack, UnCompressPack);
            nType = type;
            gsId = gsid;

            Rest();
        }

        public void Rest()
        {
            if (connectAsync != null)
                connectAsync.Dispose();

            ConnID = 0;
            Sequence = 0;
            IsConnected = false;
            SendBytes = 0;
            RecvBytes = 0;
            IsSvrDisconnect = false;
        }

        //         public void Connect()
        //         {
        //             connection.Connect();
        //         }

        IEnumerator InternalConnect()
        {
            if (IsConnected)
                yield break;
            connection.Connect();

            yield return new WaitUntil(() => IsConnected);
        }

        IEnumerator InternalLogin()
        {
            var msg = Google.Protobuf.ProtobufManager.New<Gatewaypb.C2G_Dial>();
            msg.GsID = gsId;
            SendMsg((ushort)Gatewaypb.MSG.Dial, msg);

            yield return new WaitUntil(() => ConnID != 0);
        }

        public void ConnectAsync()
        {
            if (connectAsync != null)
                connectAsync.Dispose();

            // 断开连接将中断流程
            connectAsync = Observable
                .FromCoroutine(InternalConnect)
                .Timeout(TimeSpan.FromSeconds(2))
                .SelectMany(InternalLogin)
                .Timeout(TimeSpan.FromSeconds(3))
                .Subscribe(
                _ =>
                {
                    connectAsync = null;

                    // 转接入Lua中
                    LuaManager.Instance.ConnectSucced(nType);
                },
                e =>
                {
                    if (connectAsync == null)
                        return;
                    //Debug.Log(e.ToString() + connection.connected.ToString());

                    if (connection.ConnectErrorEvent != null)
                        connection.ConnectErrorEvent(connection, ConnectionError.ConnectFailed);
                    connectAsync = null;
                }
                )
                ;
        }

        public void Close()
        {
            Rest();
            connection.Close();
        }

        public void Update()
        {
            connection.Update();
        }

        public void ConnectError(ConnectionError error)
        {
            //connection.ConnectError(error);
            if (connection.ConnectErrorEvent != null)
                connection.ConnectErrorEvent(connection, error);
        }

        protected void OnConnected(Connection conn)
        {
            IsConnected = true;
            // 转接入Lua中
            //LuaManager.Instance.ConnectSucced();

            if (VersionManager.Instance.Proxy)
            {
                string proxyMsg = "proxy:" + RealIP + ":" + RealPort;
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(proxyMsg);
                SendMsg(0, bytes);
            }
        }

        protected void OnConnectError(Connection conn, ConnectionError error)
        {
            var stack = StackTraceUtility.ExtractStackTrace();
            Debug.LogFormat("ConnectError: {0}, {1}", error.ToString(), stack);
            Rest();
            if (error == ConnectionError.ConnectFailed)
            {
                // 转接入Lua中
                LuaManager.Instance.ConnectFailed(nType);
            }
            else
            {
                // 转接入Lua中
                LuaManager.Instance.ConnectError(nType);
            }
        }

        public void OnRecvMsg(Connection conn, INetPack iPack)
        {
            var pack = iPack as GameNetPack_Optimize;
            if (pack == null || pack.body == null)
            {
                // 解析包错误
                ConnectError(ConnectionError.UnPackFailed);
                return;
            }
            RecvBytes += pack.bodySize;

            NetworkManager.MsgMapper mapper;
            if (!NetworkManager.Instance.GetMsgInfo(pack.msgID, out mapper))
            {
                // 暂时处理
                if (pack.body.Length != pack.bodySize)
                {
                    byte[] temp = new byte[pack.bodySize];
                    Array.Copy(pack.body, 0, temp, 0, pack.bodySize);
                    LuaManager.Instance.RecvMsg(pack.msgID, temp);
                }
                else
                    LuaManager.Instance.RecvMsg(pack.msgID, pack.body);
                NetPackPool<GameNetPack_Optimize>.Reclaim(pack);
                return;
            }
            IMessage msg = Google.Protobuf.ProtobufManager.DeserializeFromBytes(mapper.type, pack.body, pack.bodySize);
            NetPackPool<GameNetPack_Optimize>.Reclaim(pack);
            try
            {
                if (msg == null)
                {
                    // 解析Protobufg错误
                    ConnectError(ConnectionError.UnPackFailed);
                    return;
                }
                if (mapper.handler_gate != null)
                    mapper.handler_gate(msg, this);
                else
                    mapper.handler(msg);
            }
            catch (Exception e)
            {
                Google.Protobuf.ProtobufManager.Reclaim(msg);
                Debug.LogWarning(e);
                return;
            }
        }

        public void SendMsg(ushort id, IMessage msg, bool recycle = true, bool isCompress = true)
        {
            if (!IsConnected)
                return;
            var node = NetPackPool<GameNetPack_Optimize>.New();
            node.connID = ConnID;
            node.seq = Sequence++;
            node.msgID = id;
            node.bodySize = msg.CalculateSize();
            node.body = BytesCache.New(node.bodySize);
            Google.Protobuf.ProtobufManager.SerializeToBytes(msg, node.body, node.bodySize);

            SendBytes += node.bodySize;
#if UNITY_ANDROID
            connection.SendData(node, isCompress, true);
#else
            connection.SendData(node, isCompress, true);
#endif
        }

        public void SendMsg(ushort id, byte[] data, bool isCompress = true)
        {
            if (!IsConnected)
                return;
            var node = NetPackPool<GameNetPack_Optimize>.New();
            node.connID = ConnID;
            node.seq = Sequence++;
            node.msgID = id;
            node.body = data;
            node.bodySize = data.Length;

            SendBytes += node.bodySize;
#if UNITY_ANDROID
            connection.SendData(node, isCompress, true);
#else
            connection.SendData(node, isCompress, true);
#endif
        }

        public void OnDisconnect()
        {
            Rest();
            ConnectError(ConnectionError.ServerClose);
        }

        public void OnReconnect()
        {

        }

        // 多线程
        protected INetPack OnSendHeart(long time)
        {
            if (ConnID == 0)
                return null;
            Gatewaypb.Heart msgHeart = ProtobufManager.New<Gatewaypb.Heart>();
            msgHeart.PingTime = (time);

            GameNetPack_Optimize nodeHeart = NetPackPool<GameNetPack_Optimize>.New();
            nodeHeart.connID = ConnID;
            nodeHeart.msgID = (ushort)Gatewaypb.MSG.Heart;
            nodeHeart.bodySize = msgHeart.CalculateSize();
            nodeHeart.body = BytesCache.New(nodeHeart.bodySize);

            Google.Protobuf.ProtobufManager.SerializeToBytes(msgHeart, nodeHeart.body, nodeHeart.bodySize);

            // 回收
            ProtobufManager.Reclaim<Gatewaypb.Heart>(msgHeart);
            return nodeHeart;
        }

        // 多线程
        protected bool OnRecvHeart(INetPack pack, out long time)
        {
            time = 0;
            var node = pack as GameNetPack_Optimize;
            if (node == null || node.msgID != (ushort)Gatewaypb.MSG.Heart || ConnID == 0)
                return false;
            var msgHeart = (Gatewaypb.Heart)Google.Protobuf.ProtobufManager.DeserializeFromBytes(typeof(Gatewaypb.Heart), node.body, node.bodySize);
            time = msgHeart.PingTime;

            // 回收
            NetPackPool<GameNetPack_Optimize>.Reclaim(node);
            ProtobufManager.Reclaim<Gatewaypb.Heart>(msgHeart);
            return true;
        }

        public void SvrDisconnect()
        {
            LuaManager.Instance.ConnectServer(nType, false, false);
        }

        public void SvrReconnect(bool restart)
        {
            LuaManager.Instance.ConnectServer(nType, true, restart);
        }

        private static int MAX_LZO_LEN = 8196 * 8;
        private byte[] lzoMsg = new byte[MAX_LZO_LEN];
        protected INetPack CompressPack(INetPack pack)
        {
            GameNetPack_Optimize gamePack = pack as GameNetPack_Optimize;
            int compressedSize = SnappyPI.SnappyCodec.GetMaximumCompressedLength(gamePack.bodySize);
            int errid = SnappyPI.SnappyCodec.Compress(gamePack.body, gamePack.bodySize, lzoMsg, ref compressedSize);
            if (errid == 0)
            {
                if (compressedSize >= gamePack.bodySize)
                    return gamePack;

                gamePack.flag |= 0x02;
                byte[] compressedMsg = BytesCache.New(compressedSize);
                Buffer.BlockCopy(lzoMsg, 0, compressedMsg, 0, compressedSize);
                BytesCache.Reclaim(gamePack.body); // 回收
                gamePack.body = compressedMsg;
                gamePack.bodySize = compressedSize;
            }
            else
                throw new Exception("CompressPack Error: " + errid);
            return gamePack;
        }
        protected INetPack UnCompressPack(INetPack pack)
        {
            GameNetPack_Optimize gamePack = pack as GameNetPack_Optimize;
            if ((gamePack.flag &= 0x02) == 0)
                return gamePack;

            Array.Clear(lzoMsg, 0, lzoMsg.Length);
            int outputSize = lzoMsg.Length;
            int errid = SnappyPI.SnappyCodec.Uncompress(gamePack.body, gamePack.bodySize, lzoMsg, ref outputSize);
            if (errid == 0)
            {
                byte[] temp = BytesCache.New(outputSize);
                Buffer.BlockCopy(lzoMsg, 0, temp, 0, outputSize);
                BytesCache.Reclaim(gamePack.body); // 回收
                gamePack.body = temp;
                gamePack.bodySize = outputSize;
            }
            else
                throw new Exception("UnCompressPack Error: " + errid);
            return gamePack;
        }
    }
}