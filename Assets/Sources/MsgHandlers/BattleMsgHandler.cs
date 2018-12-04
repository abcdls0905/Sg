using DG.Tweening;
using Game;
using GameLua;
using Google.Protobuf;
using Pb;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace GameMsgHandler
{
    [MessageHandlerClass]
    public class BattleMsgHandler
    {
//         [MessageHandler(MSG.TalentChoose, typeof(B2C_LevelUpChooseTalent))]
//         public static void OnTalentChoose(IMessage iMsg)
//         {
//             B2C_LevelUpChooseTalent message = iMsg as B2C_LevelUpChooseTalent;
//             LvUpChooseTalentInfo param = new LvUpChooseTalentInfo();
//             param.level = message.Level;
//             param.bSpe = (param.level == 1);
//             param.talentIdx = new List<int>();
//             for (int i = 0; i < message.Talentidx.Count; i++)
//                 param.talentIdx.Add(message.Talentidx[i]);
//             param.charid = (int)message.Charid;
//             if (param.level == 1 && param.talentIdx.Count > 0)
//                 UIManager.Instance.OpenPage<GameChooseSuitPage>();
//             EventManager.Instance.PushEvent(GEventType.EVENT_LVUPTALENTCHOICE, ref param);
//             ProtobufManager.Reclaim(iMsg);
//         }
    }
}