using Entitas;
using UnityEngine;

namespace Game
{
    [Game]
    public class AIComponent : IComponent
    {
        public float period;
        public float runTime;
        public AkTurnDir eMoveDir;
        public float moveOffset;
        public float moveDistance;
        public bool isMoving;
        public bool isBack;
        public void Reset()
        {
            period = 2;
            runTime = 0;
            moveOffset = 0;
            eMoveDir = AkTurnDir.Ak_Max;
            isMoving = false;
            moveDistance = 1.0f;
        }

        public void ClearTemp()
        {
            isMoving = false;
            moveOffset = 0;
            eMoveDir = AkTurnDir.Ak_Max;
            isBack = false;
        }
    }
}