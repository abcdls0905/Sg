using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameLoadingState : GameStateBase
    {
        public string name
        {
            get;
            set;
        }

        AsyncOperation asyncOperation = null;
        float percent;

        public GameLoadingState(GameStateMachine parent) : base(parent) { }
        public override void OnStateEnter()
        {
            UIManager.Instance.ClosePage<GameStartPage>();
            GameLoadingPage loadingPage = UIManager.Instance.OpenPage<GameLoadingPage>();
            loadingPage.SetFillAmount(0);
            percent = 0;
            asyncOperation = SceneManager.LoadSceneAsync("tese_v3");
        }

        public override void OnStateLeave()
        {
            UIManager.Instance.ClosePage<GameLoadingPage>();
        }

        public override void OnUpdate()
        {
            if (asyncOperation.isDone)
            {
                AudioManager.Instance.PreLoadAudio();
                parent.ChangeState("GameBattleState");
            }
            else
            {
                GameLoadingPage loadingPage = UIManager.Instance.GetPage<GameLoadingPage>();
                loadingPage.SetFillAmount(asyncOperation.progress);
            }
        }
    }
}