
using System;
using UnityEngine;
using System.Collections.Generic;
using Game;

namespace GameJson
{
    [Serializable]
    public class GameBoxColor
    {
        public AkBoxColor up;
        public AkBoxColor front;
        public AkBoxColor back;
        public AkBoxColor left;
        public AkBoxColor right;
        public AkBoxColor down;
    }

    [Serializable]
    public class BoxConfig
    {
        public int eliminateNum;
        public float stableTime;
        public float dangerTime;
        public float extraTime;
        public int burnRange;
        public float sinkDistance;
        public int iceCount;
    }

    [Serializable]
    public class LevelTerms
    {
        public int red;
        public int yellow;
        public int blue;
    }

    [Serializable]
    public class IceBoxConfig
    {
        public int intervalTime;
        public int count;
    }

    [Serializable]
    public class LevelConfig
    {
        public int sevenX;
        public int sevenY;
        public int tenX;
        public int tenY;
        public int sevenCount;
        public int tenCount;
        public float randTime;
        public int difficultyBox;
        public int levelOne;
        public int levelTwo;
        public int urgentNum;
        public int urgentAdd;
        public int randMonster;
        public int defaultTime;
        public int addTime;
        public Dictionary<int, int> dicTimes;
        public int defRed;
        public int defYellow;
        public int defBlue;
        public Dictionary<int, LevelTerms> dicTerms;
        public Dictionary<int, IceBoxConfig> dicIces;
    }
}