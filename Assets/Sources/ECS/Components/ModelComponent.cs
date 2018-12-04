
using Entitas;
using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    
    [Game]
    public class ModelComponent : IComponent
    {
        public ChangeModel chgModel;
        public void Reset()
        {
            chgModel = null;
        }
    }
}