using FairyGUI;
using UnityEngine;
using System.Collections.Generic;

namespace Game
{

    public class GameStartPage : UIPage
    {
        const string packageName = "Start";
        const string componentName = "StartLoginPage";

        protected override void OnInit()
        {
            InitWindow(packageName, componentName);
            var btnPlay = contentPane.GetChild("BtnPlay").asButton;
            BattleManager.Instance.mapSize = 10;
            BattleManager.Instance.eColorType = AkColorType.AK_THREE;
            btnPlay.onClick.Add(() =>
            {
                GameStateManager.Instance.ChangeState("GameLoadingState");
                Util.PlayUIAudio();
            });
        }
    }
}