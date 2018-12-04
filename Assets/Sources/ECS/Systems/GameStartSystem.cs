using Entitas;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace Game
{
    public class GameStartSystem : IInitializeSystem
    {
        public void Initialize()
        {
            Util.CreateMaster(0);
        }
    }
}