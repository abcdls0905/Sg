
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SceneEditor
{
    [Serializable]
    public enum AkBuildType
    {
        Ak_Wall = 0,
        Ak_Max,
    }


    [Serializable]
    public class BuildRes
    {
        public AkBuildType type;
        public string resPath;
    }

//     class BuildComparer : IEqualityComparer<AkBuildType>
//     {
//         public bool Equals(AkBuildType t1, AkBuildType t2) { return t1 == t2; }
//         public int GetHashCode(AkBuildType t) { return (int)t; }
//     }

    [Serializable]
    public class BuildConfig
    {
        public Dictionary<string, BuildRes> buildings = new Dictionary<string, BuildRes>();
    }

    public class BuildMake : MonoBehaviour
    {
        public AkBuildType type;
    }
}