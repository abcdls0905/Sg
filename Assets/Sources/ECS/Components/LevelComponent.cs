using Entitas;
using UnityEngine;
using Entitas.CodeGeneration.Attributes;

namespace Game
{
    [Game]
    [Unique]
    public class LevelComponent : IComponent
    {
        public int destroyCount;
        public int level;
        public float randTime;
        public float stableTime;
        public float dangerTime;
        public int levelBox;
        public void Reset()
        {
            destroyCount = 0;
            level = 1;
            GameJson.LevelConfig levelConfig = DataManager.Instance.levelConfig.Data;
            var burnConfig = DataManager.Instance.boxConfig.Data;
            randTime = levelConfig.randTime;
            stableTime = burnConfig.stableTime;
            dangerTime = burnConfig.dangerTime;
            levelBox = levelConfig.difficultyBox;
        }
    }
}