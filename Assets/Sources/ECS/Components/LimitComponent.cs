
using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;

namespace Game
{
    public enum AkLimitType
    {
        Ak_LimitMove = 0,
        Ak_LimitCross,
        Ak_Max,
    }

    class LimitComparer : IEqualityComparer<AkLimitType>
    {
        public bool Equals(AkLimitType t1, AkLimitType t2) { return t1 == t2; }
        public int GetHashCode(AkLimitType t) { return (int)t; }
    }

    [Game]
    public class LimitComponent : IComponent
    {
        public Dictionary<AkLimitType, int> dicLimit = new Dictionary<AkLimitType, int>(new LimitComparer());
        public void Reset()
        {
            dicLimit.Clear();
        }

        public bool HasLimit(AkLimitType eLimit)
        {
            int limit = 0;
            dicLimit.TryGetValue(eLimit, out limit);
            return limit > 0;
        }

        public void AddLimit(AkLimitType eLimit)
        {
            int limit = 0;
            if (dicLimit.TryGetValue(AkLimitType.Ak_LimitMove, out limit))
                dicLimit[eLimit] = limit + 1;
        }

        public void RemoveLimit(AkLimitType eLimit)
        {
            int limit = 0;
            if (dicLimit.TryGetValue(AkLimitType.Ak_LimitMove, out limit))
            {
                limit = limit - 1;
                dicLimit[eLimit] = limit < 0 ? 0 : limit;
            }
        }
    }
}