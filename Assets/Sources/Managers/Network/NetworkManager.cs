using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using GameUtil;
using System.Collections;
using GameNetwork;
using XLua;

namespace Game
{
    [LuaCallCSharp]
    public enum NetworkType
    {
        NT_Game,
        NT_Battle,
        NT_None,
    }

    [LuaCallCSharp]
    public class NetworkManager : Singleton<NetworkManager>
    {
        public class MsgMapper
        {
            public ushort id;
            public Type type;
            public MsgHandler handler;
            public MsgHandler_Gate handler_gate;
        }

        public delegate void MsgHandler(IMessage msg);
        public delegate void MsgHandler_Gate(IMessage msg, GameConnector conn);

        private GameConnector gameConnector;
        private GameConnector battleConnector;
        private Dictionary<ushort, MsgMapper> msgMappers;

        public GameConnector BattleConnector {get { return battleConnector; } }
        public GameConnector LobbyConnector { get { return gameConnector; } }

        public NetworkReachability netReach = NetworkReachability.ReachableViaLocalAreaNetwork;

        public List<Type> msgHandler = new List<Type>() {
            typeof(GameMsgHandler.GateMsgHandler),
            typeof(GameMsgHandler.BattleMsgHandler),
            typeof(GameMsgHandler.GameMsgHandler),
        };
        public override void Init()
        {
            msgMappers = new Dictionary<ushort, MsgMapper>();
            var enumerator = msgHandler.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Type current = enumerator.Current;
                MethodInfo[] methods = current.GetMethods();
                for (int i = 0; i < methods.Length; ++i)
                {
                    MethodInfo methodInfo = methods[i];
                    if (!methodInfo.IsStatic)
                        continue;
                    object[] customAttributes = methodInfo.GetCustomAttributes(typeof(MessageHandlerAttribute), true);
                    if (customAttributes == null || customAttributes.Length <= 0)
                        continue;
                    MessageHandlerAttribute attr = customAttributes[0] as MessageHandlerAttribute;

                    MsgMapper mapper = new MsgMapper();
                    mapper.id = attr.MessageID;
                    mapper.type = attr.MessageType;
                    if (attr.MessageID < (ushort)Gatewaypb.MSG.Gwmax)
                        mapper.handler_gate = (MsgHandler_Gate)Delegate.CreateDelegate(typeof(MsgHandler_Gate), methodInfo);
                    else
                        mapper.handler = (MsgHandler)Delegate.CreateDelegate(typeof(MsgHandler), methodInfo);

                    RegisterMsgMapper(mapper);
                }
            }

            netReach = Application.internetReachability;
        }

        public void RegisterMsgMapper(MsgMapper mapper)
        {
            if((mapper.handler == null && mapper.handler_gate == null) ||
               (mapper.handler != null && mapper.handler_gate != null))
            {
                Debug.LogError("MsgHander error!");
            }

            if (this.msgMappers.ContainsKey(mapper.id))
                return;
            msgMappers.Add(mapper.id, mapper);
        }

        public bool GetMsgInfo(ushort id, out MsgMapper mapper)
        {
            return msgMappers.TryGetValue(id, out mapper);
        }

        public void ConnectGameServer(string ip, int port, int gsid)
        {
            if (gameConnector != null)
                gameConnector.Close();

            ConnectionParam param = new ConnectionParam(ip, port, ConnectionType.TCP);
            param.secure = true;
            //param.connectBlock = false;
            //param.updateSync = false;
            param.ackTimeout = 5000;

            gameConnector = new GameConnector(param, NetworkType.NT_Game, gsid);
            gameConnector.ConnectAsync();
        }

        public void ConnectBattleServer(string ip, int port, int gsid)
        {
            if (battleConnector != null)
                battleConnector.Close();

            ConnectionParam param = new ConnectionParam(ip, port, ConnectionType.KCP);
            param.secure = true;
            //param.connectBlock = false;
            //param.updateSync = false;
            param.ackTimeout = 5000;

            battleConnector = new GameConnector(param, NetworkType.NT_Battle, gsid);
            battleConnector.ConnectAsync();
        }

        public void CloseGameServer()
        {
            if (gameConnector != null)
            {
                gameConnector.Close();
                gameConnector = null;
            }
        }

        public void CloseBattleServer()
        {
            if (battleConnector != null)
            {
                battleConnector.Close();
                battleConnector = null;
            }
        }

        public void DisconnectBattleServer()
        {
            if (battleConnector != null)
            {
                battleConnector.ConnectError(ConnectionError.ServerClose);
                battleConnector = null;
            }
        }


        public void DisconnectGameServer()
        {
            if (gameConnector != null)
            {
                gameConnector.ConnectError(ConnectionError.ServerClose);
                gameConnector = null;
            }
        }

        public bool IsGameConnected()
        {
            return gameConnector != null && gameConnector.IsConnected;
        }

        public bool IsBattleConnected()
        {
            return battleConnector != null && battleConnector.IsConnected;
        }

        public void Update()
        {
            if (gameConnector != null)
                gameConnector.Update();
            if (battleConnector != null)
                battleConnector.Update();
        }

        public void SendGameMsg(ushort id, IMessage msg)
        {
            if (gameConnector != null)
                gameConnector.SendMsg(id, msg);
        }

        public void SendGameMsg(Pb.MSG id, IMessage msg)
        {
            SendGameMsg((ushort)id, msg);
        }

        public void SendGameMsg(ushort id, byte[] data)
        {
            if (gameConnector != null)
                gameConnector.SendMsg(id, data);
        }

        public void SendBattleMsg(ushort id, IMessage msg)
        {
            if (battleConnector != null)
                battleConnector.SendMsg(id, msg);
        }

        public void SendBattleMsg(Pb.MSG id, IMessage msg)
        {
            SendBattleMsg((ushort)id, msg);
        }

        public long GetBattlePingTime()
        {
            if (battleConnector != null)
                return battleConnector.PingTime;
            else
                return 0;
        }
    }
}