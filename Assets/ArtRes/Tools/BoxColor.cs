using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace ArtRes
{
    public enum AkColorMode
    {
        Ak_ThreeColor = 0,
        Ak_TwoColor,
    }

    [SerializeField]
    public class Color3Data
    {
        public AkBoxColor up;
        public AkBoxColor front;
        public AkBoxColor back;
        public AkBoxColor left;
        public AkBoxColor right;
        public AkBoxColor down;
    }

    public class BoxColor : MonoBehaviour
    {

        public AkBoxColor Up;
        public AkBoxColor Down;
        public AkBoxColor Front;
        public AkBoxColor Back;
        public AkBoxColor Left;
        public AkBoxColor Right;

        public AkColorMode colorMode;
    }

}
