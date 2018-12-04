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
        public List<GameEntity> boxes;
    }

    public struct LevelParam : IEventParam
    {
        public int level;
    }

    public struct ComboParam : IEventParam
    {
        public int combo;
    }

    public struct TermsChangeParam : IEventParam
    {

    }
    public struct UseItemParam : IEventParam
    {
        public AkItemType type;
    }
}