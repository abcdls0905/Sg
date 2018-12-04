using UnityEngine;

namespace Game
{
    public static partial class Util
    {
        public static Vector2 WorldPosToUIPos(Vector3 worldPos)
        {
            bool inScreen = false;
            return UIManager.WorldPosToUIPos(worldPos, out inScreen);
        }
    }
}