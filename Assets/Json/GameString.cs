using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameJson
{
    [Serializable]
    public class GameString
    {
        public Dictionary<string, string> normal;
        public Dictionary<int, string> yell;
        public Dictionary<int, string> error;
        public Dictionary<int, string> WordsCheck;
        public Dictionary<int, string> baseprop;
        public Dictionary<int, string> speprop;
        public Dictionary<int, string> quality;
    }
}