using Entitas;
using UnityEngine;
using Game;
using System;
using GameJson;
using System.Collections.Generic;

namespace Game
{
    public class TermsSystem : IInitializeSystem
    {
        //public void Ini7
        public void Initialize()
        {
            LevelParam param = new LevelParam();
            param.level = 1;
            EventManager.Instance.PushEvent<LevelParam>(GEventType.EVENT_LEVELCHG, ref param);
            RandNextTerms();
            EventManager.Instance.AddEvent<DesGroupParam>(GEventType.EVENT_BOXDESTORYGROUP, DestroyGroup);
        }

        void RandNextTerms(int level = 1)
        {
            LevelTermsComponent terms = Contexts.Instance.game.levelTerms;
            LevelComponent levelComp = Contexts.Instance.game.level;
            levelComp.level = level;
            var levelCfg = DataManager.Instance.levelConfig.Data;
            int needCount = levelCfg.difficultyBox + 10 * level;
            //int needCount = 3 + 2 * level;

            LevelTerms lelTerms = null;
            int red = levelCfg.defRed;
            int blue = levelCfg.defBlue;
            int yellow = levelCfg.defYellow;
            if (levelCfg.dicTerms.TryGetValue(level, out lelTerms))
            {
                red = lelTerms.red;
                blue = lelTerms.yellow;
                yellow = lelTerms.blue;
            }
            terms.Add(AkBoxColor.Ak_Red, red);
            terms.Add(AkBoxColor.Ak_Blue, blue);
            terms.Add(AkBoxColor.Ak_Yellow, yellow);
//             return;
//             if (level >= levelCfg.levelOne && level < levelCfg.levelTwo)
//             {
//                 //˫ɫ
//                 AkBoxColor color1 = (AkBoxColor)UnityEngine.Random.Range(0, (int)AkBoxColor.Ak_Universal);
//                 AkBoxColor color2 = (AkBoxColor)UnityEngine.Random.Range(0, (int)AkBoxColor.Ak_Universal);
//                 int count1 = UnityEngine.Random.Range(0, needCount);
//                 int count2 = needCount - UnityEngine.Random.Range(0, needCount);
//                 terms.Add(color1, count1);
//                 terms.Add(color2, count2);
//                 red = color1 == AkBoxColor.Ak_Red ? count1 : 0;
//                 red = color2 == AkBoxColor.Ak_Red ? count2 : 0;
//                 blue = color1 == AkBoxColor.Ak_Blue ? count1 : 0;
//                 blue = color2 == AkBoxColor.Ak_Blue ? count2 : 0;
//                 yellow = color1 == AkBoxColor.Ak_Yellow ? count1 : 0;
//                 yellow = color2 == AkBoxColor.Ak_Yellow ? count2 : 0;
//             }
//             else
//             {
//                 red = UnityEngine.Random.Range(0, needCount / 2 - 1) + 1;
//                 blue = UnityEngine.Random.Range(0, needCount / 2 - 1) + 1;
//                 yellow = needCount - red - blue;
//                 terms.Add(AkBoxColor.Ak_Red, red);
//                 terms.Add(AkBoxColor.Ak_Blue, blue);
//                 terms.Add(AkBoxColor.Ak_Yellow, yellow);
//             }
        }

        public void DestroyGroup(ref DesGroupParam param)
        {
            var levelConfig = DataManager.Instance.levelConfig.Data;
            LevelComponent levelComp = Contexts.Instance.game.level;
            bool isPositive = false;
            int red = 0;
            int blue = 0;
            int yellow = 0;
            for (int i = 0; i < param.boxes.Count; i++)
            {
                GameEntity box = param.boxes[i];
                if (box.box.eSrcColor == AkBoxColor.Ak_Red)
                    red++;
                if (box.box.eSrcColor == AkBoxColor.Ak_Blue)
                    blue++;
                if (box.box.eSrcColor == AkBoxColor.Ak_Yellow)
                    yellow++;
                if (box.box.isPositive)
                    isPositive = true;
            }
            if (isPositive)
                levelComp.timeLeft += levelConfig.addTime;
            LevelTermsComponent terms = Contexts.Instance.game.levelTerms;
            terms.DelTerms(AkBoxColor.Ak_Red, red);
            terms.DelTerms(AkBoxColor.Ak_Blue, blue);
            terms.DelTerms(AkBoxColor.Ak_Yellow, yellow);

            if (terms.Empty())
            {
                levelComp.level = levelComp.level + 1;
                LevelParam paramLevel = new LevelParam();
                paramLevel.level = levelComp.level;
                levelComp.timeLeft = 0;
                int timeLeft = levelConfig.defaultTime;
                levelConfig.dicTimes.TryGetValue(levelComp.level, out timeLeft);
                levelComp.timeLeft = timeLeft;
                EventManager.Instance.PushEvent<LevelParam>(GEventType.EVENT_LEVELCHG, ref paramLevel);
                RandNextTerms(levelComp.level);
            }
            TermsChangeParam tCParam = new TermsChangeParam();
            EventManager.Instance.PushEvent<TermsChangeParam>(GEventType.EVENT_TERMSCHANGE, ref tCParam);
        }
    }
}