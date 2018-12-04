using Game;
using Google.Protobuf;
using UnityEngine;
using Gatewaypb;

namespace GameMsgHandler
{
    [MessageHandlerClass]
    public class GateMsgHandler
    {
        [MessageHandler(MSG.Dial, typeof(G2C_Dial))]
        public static void OnDial(IMessage iMsg, GameConnector conn)
        {
            var msg = iMsg as G2C_Dial;
            conn.ConnID = msg.ConnID;
            ProtobufManager.Reclaim(iMsg);
        }

//         [MessageHandler(MSG.Heart, typeof(Heart))]
//         public static void OnHeart(IMessage iMsg, GameConnector conn)
//         {
//             var msg = iMsg as Heart;
//             conn.RecvPingAck(msg.PingTime);
//             ProtobufManager.Reclaim(iMsg);
//         }

        [MessageHandler(MSG.Disconncet, typeof(G2C_Disconnect))]
        public static void OnDisconnect(IMessage iMsg, GameConnector conn)
        {
            Debug.Log("OnDisconnect");
            conn.SvrDisconnect();
            ProtobufManager.Reclaim(iMsg);
        }

        [MessageHandler(MSG.Gsreconnect, typeof(G2C_Reconnect))]
        public static void OnReconnect(IMessage iMsg, GameConnector conn)
        {
            var msg = iMsg as G2C_Reconnect;
            Debug.Log("OnReconnect");
            conn.SvrReconnect(msg.Reconnect);
            ProtobufManager.Reclaim(iMsg);
        }

        [MessageHandler(MSG.Kick, typeof(G2C_Kick))]
        public static void OnKick(IMessage iMsg, GameConnector conn)
        {

            Debug.Log("OnKick");
            conn.ConnectError(GameNetwork.ConnectionError.ServerClose);
            ProtobufManager.Reclaim(iMsg);
        }
    }
}