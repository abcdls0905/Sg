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
    public class GameGuidePage : UIPage
    {
        const string packageName = "Game";
        const string componentName = "GameGuidePage";
        protected override void OnInit()
        {
            InitWindow(packageName, componentName, UIPageLayer.GuidePage);
            BattleManager.Instance.isGuiding = true;
            GLoader comLoader = contentPane.GetChild("ComGuide").asLoader;
            comLoader.onClick.Add(() =>
            {
                UIManager.Instance.ClosePage<GameGuidePage>();
                LocalDataManager.Instance.SetFirstGame(1);
                BattleManager.Instance.isGuiding = false;
            });
        }
    }
}