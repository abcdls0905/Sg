using Entitas;
using Entitas.CodeGeneration.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [Game, Unique]
    public class CheckPointComponent : IComponent
    {
        public Dictionary<uint, GameObject> triggerObjs = new Dictionary<uint, GameObject>();
        public Dictionary<GameObject, int> smokeDic = new Dictionary<GameObject, int>();
        public Dictionary<uint, GameObject> dicGameObjects = new Dictionary<uint, GameObject>();
        public Dictionary<uint, GameObject> obstacles = new Dictionary<uint, GameObject>();
        public void Reset()
        {
            obstacles.Clear();
            triggerObjs.Clear() ;
            smokeDic.Clear() ;
            dicGameObjects.Clear();
        }
    }
}
