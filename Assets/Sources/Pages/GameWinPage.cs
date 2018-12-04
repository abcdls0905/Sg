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
    public class GameWinPage : UIPage
    {
        const string packageName = "Game";
        const string componentName = "GameWinPage";

        protected override void OnInit()
        {
            InitWindow(packageName, componentName);
        }
    }
}