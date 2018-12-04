using Entitas;
using UnityEngine;

namespace Game
{
    [Game]
    public class BulletComponent : IComponent
    {
        public float speed;
        public float distance;
        public Vector3 forward;
        public AkBoxColor eBoxColor;
        public void Reset()
        {
            speed = 5;
            distance = 0;
            eBoxColor = AkBoxColor.Ak_Max;
        }
    }
}