using Entitas;

namespace Game
{
    [Game]
    public class CoordComponent : IComponent
    {
        public int x;
        public int y;
        public void Reset()
        {
            x = 0;
            y = 0;
        }
    }
}