using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Game
{
    [Game]
    public class IDComponent : IComponent
    {
        [PrimaryEntityIndex, PrimaryMember]
        public ulong value;
        public void Reset()
        {
            value = 0;
        }
    }
}