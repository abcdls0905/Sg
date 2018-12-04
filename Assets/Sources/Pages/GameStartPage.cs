using FairyGUI;
using UnityEngine;
using System.Collections.Generic;

namespace Game
{

    public class GameStartPage : UIPage
    {
        const string packageName = "Start";
        const string componentName = "StartLoginPage";

        private GComponent comMode;
        private GComponent comNumber;
        private GComponent comPlay;

        protected override void OnInit()
        {
            InitWindow(packageName, componentName);
            comMode = contentPane.GetChild("ComMode").asCom;
            comNumber = contentPane.GetChild("ComNumber").asCom;
            comPlay = contentPane.GetChild("ComPlay").asCom;
            var btnTwo = comMode.GetChild("BtnTwo").asButton;
            var btnThree = comMode.GetChild("BtnThree").asButton;
            btnTwo.onClick.Add(() =>
            {
                BattleManager.Instance.eColorType = AkColorType.AK_TWO;
                comMode.visible = false;
                comNumber.visible = true;
                Util.PlayUIAudio();
            });
            btnThree.onClick.Add(() =>
            {
                BattleManager.Instance.eColorType = AkColorType.AK_THREE;
                comMode.visible = false;
                comNumber.visible = true;
                Util.PlayUIAudio();
            });
            var btnSeven = comNumber.GetChild("BtnSeven").asButton;
            var btnTen = comNumber.GetChild("BtnTen").asButton;
            var btnBack1 = comNumber.GetChild("BtnBack").asButton;
            btnSeven.onClick.Add(() =>
            {
                BattleManager.Instance.mapSize = 7;
                comNumber.visible = false;
                comPlay.visible = true;
                Util.PlayUIAudio();
            });
            btnTen.onClick.Add(() =>
            {
                BattleManager.Instance.mapSize = 10;
                comNumber.visible = false;
                comPlay.visible = true;
                Util.PlayUIAudio();
            });
            btnBack1.onClick.Add(() =>
            {
                comMode.visible = true;
                comNumber.visible = false;
                Util.PlayUIAudio();
            });

            var btnPlay = comPlay.GetChild("BtnPlay").asButton;
            var btnBack2 = comPlay.GetChild("BtnBack").asButton;
            btnPlay.onClick.Add(() =>
            {
                GameStateManager.Instance.ChangeState("GameLoadingState");
                Util.PlayUIAudio();
            });
            btnBack2.onClick.Add(() =>
            {
                comPlay.visible = false;
                comNumber.visible = true;
                Util.PlayUIAudio();
            });
        }
    }
}