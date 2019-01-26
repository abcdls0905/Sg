using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameLobbyState : GameStateBase
    {
        public string name
        {
            get;
            set;
        }

        public string lobbyScene = "lobby";
        string hallAudio = "Audio/hallbgm";

        public GameLobbyState(GameStateMachine parent) : base(parent) { }
        public override void OnStateEnter()
        {
            SceneManager.LoadScene(lobbyScene);
            UIManager.Instance.OpenPage<GameStartPage>();
            AudioManager.Instance.PlayAudio(hallAudio, Util.MainCamera.gameObject, true);
        }

        public override void OnStateLeave()
        {
            //SceneManager.UnloadSceneAsync(lobbyScene);
            AudioManager.Instance.StopAudio(hallAudio);
        }

        public override void OnUpdate()
        {

        }
    }
}