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
            if (termsDic.TryGetValue(eBoxColor, out count))
                termsDic[eBoxColor] = termsDic[eBoxColor] + value;
            else
                termsDic.Add(eBoxColor, value);
        }

        public void ClearTerms()
        {
            termsDic.Clear();
        }

        public void DelTerms(AkBoxColor eColor, int count)
        {
            int value = 0;
            if (termsDic.TryGetValue(eColor, out value))
            {
                int temp = value - count;
                temp = temp > 0 ? temp : 0;
                termsDic[eColor] = temp;
            }
        }

        public int Get(AkBoxColor eColor)
        {
            int value = 0;
            termsDic.TryGetValue(eColor, out value);
            return value;
        }

        public bool Empty()
        {
            var iter = termsDic.GetEnumerator();
            while (iter.MoveNext())
            {
                if (iter.Current.Value > 0)
                    return false;
            }
            return true;
        }
    }
}