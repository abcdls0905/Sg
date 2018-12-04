using Entitas;
using Pb;
using Game;
using UnityEngine;
using WorldDataType = Pb.B2C_WorldData.StateDataOneofCase;
using WorldEventType = Pb.B2C_WorldEvent.EventDataOneofCase;
using EventType = Pb.B2C_Event.EventDataOneofCase;
using StateType = Pb.B2C_State.StateDataOneofCase;
using System.Collections.Generic;
using System;

namespace Game
{
    public class HUDSystem : IInitializeSystem, IExecuteSystem, ITearDownSystem
    {
        public HUDSystem()
        {
        }

        public void Initialize()
        {
        }

        public void Execute()
        {

        }

        void DealPvpNpcData(B2C_WorldData worldData)
        {
        }

        void DealKillInfo(B2C_WorldEvent worldEvent)
        {

        }

        void DealAttackVoice(B2C_WorldEvent worldEvent)
        {

        }

        void DealKillNum(GameEntity entity, B2C_Event ev)
        {
        }

        void DealShopdataInfo(GameEntity entity, B2C_State state)
        {

        }

        void ITearDownSystem.TearDown()
        {

        }
    }
}