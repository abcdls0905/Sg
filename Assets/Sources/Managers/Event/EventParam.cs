using Game;
using System;
using UnityEngine;
using Pb;
using System.Collections.Generic;

namespace Game
{
    public interface IEventParam
    {
    }

    public struct MMapElemParam : IEventParam
    {
        public Vector2 position;
    }

    public struct ScoreParam : IEventParam
    {
        public int score;
    }

    public struct DesBoxParam : IEventParam
    {
        public GameEntity entity;
    }
    public struct DesGroupParam : IEventParam
    {
        public int count;
    }
}