using Entitas;
using Entitas.Unity;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace Game
{

    public class AnimationSystem : IInitializeSystem
    {
        private IGroup<GameEntity> _group;
        
        public AnimationSystem()
        {
            var _context = Contexts.Instance.game;
            _group = _context.GetGroup(Matcher<GameEntity>.AllOf(GameMatcher.Animation));
            _group.OnEntityAdded += OnEntityAdd;
            Mesh mesh;
        }
        public void Initialize()
        {
            //Util.ListenCommand<AnimCommand>(HandleAnimCommand);
        }

        void HandleAnimCommand(ref AnimCommand command)
        {
        }

        protected void OnEntityAdd(IGroup<GameEntity> group, GameEntity entity, int index, IComponent component)
        {
            entity.animation.animator = entity.view.gameObject.GetComponentInChildren<Animator>();
        }
    }
}