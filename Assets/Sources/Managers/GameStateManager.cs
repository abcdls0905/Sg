
using System;
using GameUtil;
using System.Reflection;
using System.Collections.Generic;

namespace Game
{
    public class GameStateManager : Singleton<GameStateManager>
    {
        GameStateMachine gameStateMacine;
        public override void Init()
        {
            gameStateMacine = new GameStateMachine();
            gameStateMacine.RegisterState("GameLoadingState", new GameLoadingState(gameStateMacine));
            gameStateMacine.RegisterState("GameBattleState", new GameBattleState(gameStateMacine));
            gameStateMacine.RegisterState("GameLobbyState", new GameLobbyState(gameStateMacine));
        }

        public void ChangeState(string name, bool force = false)
        {
            gameStateMacine.ChangeState(name, force);
        }

        public void Update()
        {
            gameStateMacine.Update();
        }
    }
}