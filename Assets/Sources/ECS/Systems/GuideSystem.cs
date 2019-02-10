using Entitas;
using UnityEngine;
using Game;
using GameJson;
using System.Collections.Generic;

namespace Game
{
    public class GuideSystem : IInitializeSystem, IFixedExecuteSystem
    {
        public void Initialize()
        {
            bool firstGame = LocalDataManager.Instance.IsFirstEnterGame();
            if (!firstGame)
                return;
        }

        public void FixedExecute()
        {
            bool firstGame = LocalDataManager.Instance.IsFirstEnterGame();
            if (!firstGame)
                return;

        }
    }
}