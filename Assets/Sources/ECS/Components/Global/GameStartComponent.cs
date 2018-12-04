using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;
namespace Game
{
    [Game]
    [Unique]
    public class GameStartComponent : IComponent
    {
        public ulong uuid;
        public void Reset()
        {
            uuid = 0;
        }
    }
}