using Entitas;
using Game;
using System;

namespace Game
{
    class AudioSystem : IExecuteSystem, IInitializeSystem, ITearDownSystem
    {
        public void Initialize()
        {
            var game = Contexts.Instance.game;
            var gameEntity = game.gameMaster.entity;

            //WwiseAudioManager.Instance.SetFollowObject(gameEntity);
        }

        public void TearDown()
        {
        }

        public void Execute()
        {
        }

    }
}
