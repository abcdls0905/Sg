using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;
using Game;
using UnityEngine;

namespace Game
{
    [Game]
    [Unique]
    public class ScoreComponent : IComponent
    {
        public int value;
        public void Reset()
        {
            value = 0;
        }
    }
}