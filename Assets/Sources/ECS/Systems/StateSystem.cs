using Entitas;
using Entitas.Unity;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace Game
{
    public class StateSystem : IInitializeSystem, IFixedExecuteSystem
    {
        IGroup<GameEntity> _group;
        
        public void Initialize()
        {
            var _context = Contexts.Instance.game;
            _group = _context.GetGroup(Matcher<GameEntity>.AllOf(GameMatcher.State));
        }

        public void FixedExecute()
        {
            var iter = _group.GetEntities().GetEnumerator();
        }
    }
}