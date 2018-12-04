using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameJson
{
    [Serializable]
    public class PageData
    {
        public GameLeftJoystickCfg GameLeftJoystick;
    }
    [Serializable]
    public class GameLeftJoystickCfg
    {
        public float radius;
        public float limitRadius;
        public float deadRadius;
    }
}