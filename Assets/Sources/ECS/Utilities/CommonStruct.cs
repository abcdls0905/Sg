
using System;

namespace Game
{
    public class MapCoord
    {
        public int iX;
        public int iY;
        public MapCoord(int x, int y)
        {
            iX = x;
            iY = y;
        }
    }
    public enum AkBoxColor
    {
        Ak_Red = 0,
        Ak_Yellow,
        Ak_Blue,
        Ak_Universal,
        Ak_Ice,
        Ak_Max,
    }

    [Serializable]
    public enum AkTurnDir
    {
        AK_Front = 0,
        Ak_Left,
        Ak_Back,
        Ak_Right,
        Ak_Null,
        Ak_Max,
    }

    public enum AkMapGridType
    {
        Ak_Box = 0,
        AK_Item,
        Ak_Max,
    }

}