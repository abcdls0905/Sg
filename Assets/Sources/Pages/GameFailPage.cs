using FairyGUI;
using UnityEngine;
using UniRx;
using System.Collections.Generic;
using GameJson;
using System.Text;
using System;
using Pb;
using DG.Tweening;

namespace Game
{
    public class GameFailPage : UIPage
    {
        const string packageName = "Game";
        const string componentName = "GameFailPage";

        protected override void OnInit()
        {
            InitWindow(packageName, componentName);
            var btnLeave = contentPane.GetChild("BtnLeave").asButton;
            btnLeave.onClick.Add(() =>
            {
                Util.PlayUIAudio();
                BattleManager.Instance.QuitGame();
                UIManager.Instance.LoadUIPackage(false);
                GameStateManager.Instance.ChangeState("GameLobbyState");
            });
            var btnRestart = contentPane.GetChild("BtnAgain").asButton;
            btnRestart.onClick.Add(() =>
            {
                Util.PlayUIAudio();
                RestartCommand command = new RestartCommand();
                Util.PushCommand<RestartCommand>(ref command);
                UIManager.Instance.ClosePage<GameFailPage>();
                ECSManager.Instance.IsPlaying = true;
            });
        }
    }
}