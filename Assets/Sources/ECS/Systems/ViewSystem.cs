using Entitas;
using Entitas.Unity;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace Game
{

    public class ViewSystem : IExecuteSystem, IFixedExecuteSystem, ICleanupSystem
    {
        private IGroup<GameEntity> _group;
        private IGroup<GameEntity> _plyGroup;

        public ViewSystem()
        {
            var game = Contexts.Instance.game;

            IGroup<GameEntity> viewGroup = game.GetGroup(GameMatcher.View);
            viewGroup.OnEntityAdded += OnEntityAdd;
            viewGroup.OnEntityRemoved += OnEntityDel;

            _group = game.GetGroup(Matcher<GameEntity>.AllOf(GameMatcher.View, GameMatcher.Transform));
            //_plyGroup = Contexts.Instance.game.GetGroup(GameMatcher.Player);
        }

        public void Cleanup()
        {

        }

        // 处理移动物体的Lerp！！
        public void Execute()
        {
            float per = Contexts.Instance.game.frame.lerpPercent;
            var iter = _group.GetEntities().GetEnumerator();
            while (iter.MoveNext())
            {
                GameEntity entity = iter.Current;

                TransformComponent transform = entity.transform;
                Transform viewTrans = entity.view.gameObject.transform;

                if (viewTrans.position != transform.position)
                {
                    viewTrans.position = Vector3.Lerp(transform.lastPosition, transform.position, per);
                }
                if (viewTrans.rotation != transform.rotation)
                {
                    viewTrans.rotation = Quaternion.Lerp(transform.lastRotation, transform.rotation, per);
                }
            }
        }

        public void FixedExecute()
        {
            var iter = _group.GetEntities().GetEnumerator();
            while (iter.MoveNext())
            {
                var transform = iter.Current.transform;
                transform.lastPosition = transform.position;
                transform.lastRotation = transform.rotation;
            }
        }

        protected void OnEntityAdd(IGroup<GameEntity> group, GameEntity entity, int index, IComponent component)
        {
            GameObject gameObject = GameObjectPool.Instance.GetGameObject(entity.view.path, AkResourceType.GameScene);
            if (gameObject == null)
            {
                gameObject = GameObjectPool.Instance.GetGameObject("Prefabs/EmptyPrefab", AkResourceType.GameScene);
            }
            var game = Contexts.Instance.game;

            entity.view.gameObject = gameObject;
            gameObject.transform.position = Util.ZeroPosition;
            gameObject.GetComponentsInChildren<Renderer>(entity.view.renderers);
#if UNITY_EDITOR
            gameObject.Link(entity, game);
#endif
        }

        protected void OnEntityDel(IGroup<GameEntity> group, GameEntity entity, int index, IComponent component)
        {
            ViewComponent viewComp = component as ViewComponent;
            foreach (Renderer renderer in viewComp.renderers)
            {
                if (renderer != null)
                    renderer.SetPropertyBlock(null);
            }
            GameObject gameObject = viewComp.gameObject;
            if (gameObject == null)
                return;
            gameObject.transform.position = Util.ZeroPosition;
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
#if UNITY_EDITOR
            gameObject.Unlink();
#endif
            GameObjectPool.Instance.RecycleGameObject(gameObject);
        }
    }
}