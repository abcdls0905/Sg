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
        public int score;
        public void Reset()
        {
            score = 0;
        }
    }
}