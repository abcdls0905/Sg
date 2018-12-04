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
    public class GameSettingPage : UIPage
    {
        const string packageName = "Game";
        const string componentName = "GameSettingPage";

        protected override void OnInit()
        {
            InitWindow(packageName, componentName, UIPageLayer.Setting);
            var btnSendLog = contentPane.GetChild("BtnSendLog").asButton;
            btnSendLog.onClick.Add(() =>
            {
                GameInterface.SendErrorLog();
            });
            var btnClose = contentPane.GetChild("BtnClose").asButton;
            btnClose.onClick.Add(() =>
            {
                UIManager.Instance.ClosePage<GameSettingPage>();
            });
        }
    }
}