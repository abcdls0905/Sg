using Entitas;
using Entitas.Unity;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace Game
{

    public class ChangeModelSystem : IInitializeSystem, ITearDownSystem
    {
        GameContext _context;
        IGroup<GameEntity> _group;
        public ChangeModelSystem()
        {
            _context = Contexts.Instance.game;
            _group = _context.GetGroup(Matcher<GameEntity>.AllOf(GameMatcher.Model));
            _group.OnEntityAdded += OnModelAdded;
            _group.OnEntityRemoved += OnModelRemove;
        }

        private void OnModelAdded(IGroup<GameEntity> group, GameEntity entity, int index, IComponent component)
        {
            if (entity.hasView)
            {
                GameObject gameObj = entity.view.gameObject;
                ModelComponent comp = component as ModelComponent;
                ChangeModel chgModel = gameObj.AddComponent<ChangeModel>();
                comp.chgModel = chgModel;
            }
        }

        private void OnModelRemove(IGroup<GameEntity> group, GameEntity entity, int index, IComponent component)
        {

        }

        public void Initialize()
        {

        }

        public void TearDown()
        {

        }
    }
}