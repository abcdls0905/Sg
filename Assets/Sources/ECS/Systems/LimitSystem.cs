using Entitas;
using UnityEngine;
using Game;
using System;
using System.Collections.Generic;

namespace Game
{
    public class LimitSystem : IInitializeSystem
    {
        IGroup<GameEntity> _group;
        public LimitSystem()
        {
            var _context = Contexts.Instance.game;
            _group = _context.GetGroup(Matcher<GameEntity>.AllOf(GameMatcher.Limit));
            _group.OnEntityAdded += OnLimitAdded;
        }
        void OnLimitAdded(IGroup<GameEntity> group, GameEntity entity, int index, IComponent component)
        {
            for (int i = 0; i < (int)AkLimitType.Ak_Max; i++)
            {
                entity.limit.dicLimit.Add((AkLimitType)i, 0);
            }
        }

        public void Initialize()
        {

        }
    }
}