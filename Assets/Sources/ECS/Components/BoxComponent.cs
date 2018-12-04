using Entitas;
using UnityEngine;

namespace Game
{
    [Game]
    public class BoxComponent : IComponent
    {
        public AkBoxColor eColor;
        public AkBoxColor eFrontColor;
        public AkBoxColor eBackColor;
        public AkBoxColor eLeftColor;
        public AkBoxColor eRightColor;
        public AkBoxColor eDownColor;
        public AkBoxColor eSrcColor;
        public GameEntity followEntity;
        public Vector3 rotPoint;
        public Vector3 axis;
        public AkTurnDir eBoxDir;
        public float rotateValue;
        public bool isPositive;
        public void Reset()
        {
            eColor = AkBoxColor.Ak_Max;
            eFrontColor = AkBoxColor.Ak_Max;
            eBackColor = AkBoxColor.Ak_Max;
            eLeftColor = AkBoxColor.Ak_Max;
            eRightColor = AkBoxColor.Ak_Max;
            eDownColor = AkBoxColor.Ak_Max;
            eSrcColor = AkBoxColor.Ak_Max;
            followEntity = null;
            eBoxDir = AkTurnDir.Ak_Max;
            rotPoint = new Vector3(0, 0, 0);
            axis = new Vector3(0, 0, 0);
            rotateValue = 0;
            isPositive = false;
        }
    }
}