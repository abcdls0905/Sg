using FairyGUI;
using UnityEngine;
using UniRx;
using System.Collections.Generic;
using GameJson;
using System.Text;
using System;
using Game;

namespace Game
{
    public class GameHUDPage : UIPage
    {
        const string packageName = "Game";
        const string componentName = "GameHUDPage";

        Controller stateController;
        GTextField textScore;
        GTextField textDebug;

        protected override void OnInit()
        {
            InitWindow(packageName, componentName, UIPageLayer.HudPage);
            GButton btnPause = contentPane.GetChild("BtnPause").asButton;
            btnPause.onClick.Add(() =>
            {
                Util.PlayUIAudio();
                UIManager.Instance.OpenPage<GamePausePage>();
            });
            textScore = contentPane.GetChild("n155").asTextField;
            textScore.text = Contexts.Instance.game.score.score.ToString();

            GButton btnDown = contentPane.GetChild("BtnDown").asButton;
            btnDown.onClick.Add(() =>
            {
                Util.PlayUIAudio();
                stateController.selectedPage = "down";
                BattleManager.Instance.isCameraDown = true;
            });

            GButton btnUp = contentPane.GetChild("BtnUp").asButton;
            btnUp.onClick.Add(() =>
            {
                Util.PlayUIAudio();
                stateController.selectedPage = "normal";
                BattleManager.Instance.isCameraDown = false;
            });

            GButton btnSetting = contentPane.GetChild("BtnSetting").asButton;
            btnSetting.onClick.Add(() =>
            {
                Util.PlayUIAudio();
                UIManager.Instance.OpenPage<GameSettingPage>();
            });
            stateController = contentPane.GetController("state");
            stateController.selectedPage = "normal";

            textDebug = contentPane.GetChild("TextDebug").asTextField;
            textDebug.visible = false;
        }
        protected override void OnShown()
        {
            EventManager.Instance.AddEvent<ScoreParam>(GEventType.EVENT_SCORECHANGE, OnScore);
        }

        protected override void OnHide()
        {
            EventManager.Instance.RemoveEvent<ScoreParam>(GEventType.EVENT_SCORECHANGE, OnScore);
        }

        private void OnScore(ref ScoreParam param)
        {
            textScore.text = Contexts.Instance.game.score.score.ToString();
        }

        public override void Update(float deltaTime)
        {
            UpdateDebugInfo();
        }

        void UpdateDebugInfo()
        {
            if (!textDebug.visible)
                return;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(string.Format(" FPS: {0}\n", FPSCounter.Instance.FPS));
//             GameEntity master = Contexts.Instance.game.gameMaster.entity;
//             if (master != null)
//             {
//                 stringBuilder.Append(string.Format(" MoveSpeed: {0}\n", master.move.GetMoveSpeed()));
//             }
            FrameComponent frame = Contexts.Instance.game.frame;
            stringBuilder.Append(string.Format(" ServerFrame: {0}\n", frame.serverFrameIndex));
            stringBuilder.Append(string.Format(" ClientFrame: {0}\n", frame.frameIndex));
            textDebug.text = stringBuilder.ToString();
        }
    }
}