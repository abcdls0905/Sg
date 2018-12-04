using Entitas;
using UnityEngine;
using Entitas.CodeGeneration.Attributes;

namespace Game
{
    [Game]
    public class ComboComponent : IComponent
    {
        public int combo;
        public float lastTime;
        public float comboTime;
        public void Reset()
        {
            combo = 0;
            lastTime = 0;
            comboTime = 5000;
        }
    }
}