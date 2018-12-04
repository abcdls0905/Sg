using Entitas;
using System;
using System.Collections.Generic;
using Entitas.CodeGeneration.Attributes;

namespace Game
{
    class LevelTermsComparer : IEqualityComparer<AkBoxColor>
    {
        public bool Equals(AkBoxColor t1, AkBoxColor t2) { return t1 == t2; }
        public int GetHashCode(AkBoxColor t) { return (int)t; }
    }

    [Game]
    [Unique]
    public class LevelTermsComponent : IComponent
    {
        public Dictionary<AkBoxColor, int> termsDic = new Dictionary<AkBoxColor, int>(new LevelTermsComparer());
        public Dictionary<AkBoxColor, int> achieveDic = new Dictionary<AkBoxColor, int>(new LevelTermsComparer());
        public void Reset()
        {
            termsDic.Clear();
            achieveDic.Clear();
        }

        public void Add(AkBoxColor eBoxColor, int value = 1)
        {
            int count = 0;
            if (achieveDic.TryGetValue(eBoxColor, out count))
                achieveDic[eBoxColor] = achieveDic[eBoxColor] + value;
            else
                achieveDic.Add(eBoxColor, value);
        }
    }
}