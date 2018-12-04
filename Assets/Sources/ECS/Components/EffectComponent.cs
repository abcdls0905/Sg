using Entitas;
using Entitas.CodeGeneration.Attributes;
using Game;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public enum AkEffectType
    {
        None = 0,
        Ak_Follow,
        Ak_Attach,
    }

    class EffectTypeComparer : IEqualityComparer<AkEffectType>
    {
        public bool Equals(AkEffectType t1, AkEffectType t2) { return t1 == t2; }
        public int GetHashCode(AkEffectType t) { return (int)t; }
    }

    public struct EffectData
    {
        public string name;
        public AkEffectType type;

        public bool isAttach;
        public float lifeTime;
        public Vector3 position;
        public Vector3 offsetPos;
        public Vector3 offsetRot;
        public ulong targetId;
    }

    public class FollowEffect
    {
        public string name;
        public GameObject gameObject;
    }

    [Game]
    [Unique]
    public class EffectComponent : IComponent
    {
        public List<EffectData> effects = new List<EffectData>(20);
        public Dictionary<AkEffectType, GameObject> residents = new Dictionary<AkEffectType, GameObject>(new EffectTypeComparer());
        public Dictionary<ulong, List<FollowEffect>> followDic = new Dictionary<ulong, List<FollowEffect>>();

        public void Reset()
        {
            effects.Clear();
            var iter = residents.GetEnumerator();
            while(iter.MoveNext())
                GameObjectPool.Instance.RecycleGameObject(iter.Current.Value);
            var iter1 = followDic.GetEnumerator();
            while (iter1.MoveNext())
            {
                List<FollowEffect> followList = iter1.Current.Value;
                for (int i = followList.Count - 1; i >= 0; i--)
                {
                    GameObjectPool.Instance.RecycleGameObject(followList[i].gameObject);
                }
                followList.Clear();
            }
            residents.Clear();
            followDic.Clear();
        }

        public bool ContainEffect(ulong id, string effect)
        {
            List<FollowEffect> followList = null;
            if (!followDic.TryGetValue(id, out followList))
                return false;
            for (int i = 0; i < followList.Count; i++)
            {
                FollowEffect follow = followList[i];
                if (follow.name == effect)
                    return true;
            }
            return false;
        }

        public void AddFollowEffect(ulong id, string name, GameObject gameObject)
        {
            List<FollowEffect> followList = null;
            if (!followDic.TryGetValue(id, out followList))
            {
                followList = new List<FollowEffect>();
                followDic.Add(id, followList);
            }
            FollowEffect follow = new FollowEffect();
            follow.name = name;
            follow.gameObject = gameObject;
            followList.Add(follow);
        }

        public void RemoveFollow(ulong id, string name)
        {
            List<FollowEffect> followList = null;
            if (!followDic.TryGetValue(id, out followList))
                return;

            for (int i = followList.Count - 1; i >= 0; i--)
            {
                FollowEffect follow = followList[i];
                if (follow.name == name)
                {
                    followList.RemoveAt(i);
                    GameObjectPool.Instance.RecycleGameObject(follow.gameObject);
                }
            }
        }
        public void RemoveFollow(ulong id)
        {
            List<FollowEffect> followList = null;
            if (!followDic.TryGetValue(id, out followList))
                return;

            for (int i = followList.Count - 1; i >= 0; i--)
            {
                FollowEffect follow = followList[i];
                GameObjectPool.Instance.RecycleGameObject(follow.gameObject);
            }
            followList.Clear();
            followDic.Remove(id);
        }
    }
}