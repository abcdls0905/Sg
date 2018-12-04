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
    public class GamePausePage : UIPage
    {
        const string packageName = "Game";
        const string componentName = "GamePausePage";
        protected override void OnInit()
        {
            InitWindow(packageName, componentName, UIPageLayer.Setting);
            GButton btnMain = contentPane.GetChild("BtnMain").asButton;
            btnMain.onClick.Add(() =>
            {
                Util.PlayUIAudio();
                BattleManager.Instance.QuitGame();
                UIManager.Instance.LoadUIPackage(false);
                GameStateManager.Instance.ChangeState("GameLobbyState");
            });
            GButton btnRefresh = contentPane.GetChild("BtnRefresh").asButton;
            btnRefresh.onClick.Add(() =>
            {
                Util.PlayUIAudio();
                RestartCommand command = new RestartCommand();
                Util.PushCommand<RestartCommand>(ref command);
                UIManager.Instance.ClosePage<GamePausePage>();
            });
        }
    }
}