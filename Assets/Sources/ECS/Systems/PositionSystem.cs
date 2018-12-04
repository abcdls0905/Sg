using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using GameJson;
using EventType = Pb.B2C_Event.EventDataOneofCase;
using StateType = Pb.B2C_State.StateDataOneofCase;
using Pb;
using System;

namespace Game
{
    public class PositionSystem : IInitializeSystem
    {
        public void Initialize()
        {
        }

        public void DealGrenadeState(GameEntity entity, B2C_State state)
        {
        }

        public void DealMoveTriggerState(GameEntity entity, B2C_State state)
        {
        }

        public void DealPositionState(GameEntity entity, B2C_State state)
        {
        }
    }
}