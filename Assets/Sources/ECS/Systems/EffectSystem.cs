using Entitas;
using Game;
using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    public class EffectSystem : IFixedExecuteSystem, ITearDownSystem, IExecuteSystem
    {
        public EffectSystem()
        {
        }

        public void Execute()
        {
            EffectComponent effectComp = Contexts.Instance.game.effect;
            var iter = effectComp.followDic.GetEnumerator();
            while (iter.MoveNext())
            {
                ulong entityId = iter.Current.Key;
                GameEntity entity = Util.GetEntityWithId(entityId);
                if (entity != null)
                {
                    List<FollowEffect> followList = iter.Current.Value;
                    for (int i = 0; i < followList.Count; i++)
                    {
                        FollowEffect follow = followList[i];
                        follow.gameObject.transform.position = entity.transform.position;
                    }
                }
            }
        }

        public void FixedExecute()
        {
            EffectComponent effectComp = Contexts.Instance.game.effect;
            for (int i = effectComp.effects.Count - 1; i >= 0; i--)
            {
                DealEffects(effectComp.effects[i]);
                effectComp.effects.RemoveAt(i);
            }
        }
        public void DealEffects(EffectData effectData)
        {
            EffectComponent effectComp = Contexts.Instance.game.effect;
            GameObject gameObject = Util.CreateGameObject(effectData.name, AkResourceType.GameScene);
            Transform transform = gameObject.transform;
            if (effectData.lifeTime > 0)
                DelayManager.Instance.AddDelay(gameObject, effectData.lifeTime);
            switch (effectData.type)
            {
                case AkEffectType.Ak_Follow:
                    {
                        effectComp.AddFollowEffect(effectData.targetId, effectData.name, gameObject);
                        break;
                    }
                case AkEffectType.Ak_Attach:
                    {
                        break;
                    }
                default:
                    {
                        transform.position = effectData.position + effectData.offsetPos;
                        break;
                    }
            }
        }

        bool AddResident(AkEffectType type, GameObject gameObject)
        {
            EffectComponent effectComp = Contexts.Instance.game.effect;
            var residents = effectComp.residents;
            GameObject old;
            if (residents.TryGetValue(type, out old))
                return false;
            residents[type] = gameObject;
            return true;
        }

        public void TearDown()
        {

        }
    }
}