using Entitas;
using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    public class StateData
    {
    }

    [Game]
    public class StateComponent : IComponent
    {
        public List<StateData> stateList = new List<StateData>();
        public void Reset()
        {
            stateList.Clear();
        }
    }
}