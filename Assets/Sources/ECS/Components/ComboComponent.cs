using Entitas;
using UnityEngine;
using Entitas.CodeGeneration.Attributes;

namespace Game
{
    [Game]
    [Unique]
    public class ComboComponent : IComponent
    {
        public int value;
        public float lastTime;
        public float comboTime;
        public void Reset()
        {
            value = 0;
            lastTime = 0;
            comboTime = 5000;
        }
    }
}