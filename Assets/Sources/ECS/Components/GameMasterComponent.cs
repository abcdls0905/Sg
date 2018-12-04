using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;
using Game;
using UnityEngine;

namespace Game
{
    [Game]
    [Unique]
    public class GameMasterComponent : IComponent
    {
        public void Reset()
        {

            entity = null;
        }
        [PrimaryMember]
        public GameEntity entity;

    }
}