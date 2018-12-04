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
    public class ImgNumList
    {
        public int capacity = 0;
        private string prefix;
        public List<GLoader> imgList;
        public ImgNumList()
        {
            imgList = new List<GLoader>();
        }
        public ImgNumList(int num, string prefix)
        {
            imgList = new List<GLoader>(num);
            capacity = num;
            this.prefix = prefix;
        }

        public void ShowImg(int value)
        {
            for (int i = 0; i < capacity; i++)
            {
                int pow = (int)Math.Pow(10, capacity - 1 - i);
                int num = value / pow;
                value -= num * pow;
                string url = UIPackage.GetItemURL("Game", prefix + num);
                GLoader loader = imgList[i];
                loader.url = url;
            }
        }

        public void Add(GLoader loader)
        {
            if (imgList.Count >= capacity)
            {
                Debug.LogError("capacity error " + capacity);
                return;
            }
            imgList.Add(loader);
        }

        public void Initialize(GComponent contentPane, string conName, int initValue = 0)
        {
            GComponent com = contentPane.GetChild(conName).asCom;
            for (int i = 0; i < capacity; i++)
            {
                GLoader Loader = com.GetChild("ImgNum" + i).asLoader;
                Add(Loader);
            }
            ShowImg(initValue);
        }

        public void Initialize(GComponent com, int initValue = 0)
        {
            for (int i = 0; i < capacity; i++)
            {
                GLoader Loader = com.GetChild("ImgNum" + i).asLoader;
                Add(Loader);
            }
            ShowImg(initValue);
        }
    }


    public class GameHUDPage : UIPage
    {
        const string packageName = "Game";
        const string componentName = "GameHUDPage";

        Controller stateController;
        GTextField textScore;
        GTextField textLevel;
        GTextField textDebug;
        GTextField textYellow;
        GTextField textBlue;
        GTextField textRed;
        GImage[] imgMask = new GImage[3];
        ImgNumList imgNumLevel = new ImgNumList(3, "ImgLevel");
        ImgNumList imgNumScore = new ImgNumList(6, "ImgScore");
        ImgNumList imgNumBlue = new ImgNumList(2, "ImgTar");
        ImgNumList imgNumYellow = new ImgNumList(2, "ImgTar");
        ImgNumList imgNumRed = new ImgNumList(2, "ImgTar");

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
            textScore.text = Contexts.Instance.game.score.value.ToString();

            textLevel = contentPane.GetChild("textLevel").asTextField;
            textLevel.text = "1";

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

            textYellow = contentPane.GetChild("ComTarYellow").asCom.GetChild("textTarget").asTextField;
            textRed = contentPane.GetChild("ComTarRed").asCom.GetChild("textTarget").asTextField;
            textBlue = contentPane.GetChild("ComTarBlue").asCom.GetChild("textTarget").asTextField;

            var comItem1 = contentPane.GetChild("ComItem1").asButton;
            var comItem2 = contentPane.GetChild("ComItem2").asButton;
            var comItem3 = contentPane.GetChild("ComItem3").asButton;
            imgMask[0] = comItem1.GetChild("ImgMask").asImage;
            imgMask[1] = comItem2.GetChild("ImgMask").asImage;
            imgMask[2] = comItem3.GetChild("ImgMask").asImage;
            imgMask[0].visible = false;
            imgMask[1].visible = false;
            imgMask[2].visible = false;
            comItem1.onClick.Add(() =>
            {
                GameEntity master = Contexts.Instance.game.gameMaster.entity;
                ItemComponent itemComp = master.item;
                Item item = itemComp.items[0];
                if (item.isRunCD)
                    return;
                UseItemParam param = new UseItemParam();
                param.type = AkItemType.Ak_Fire;
                EventManager.Instance.PushEvent<UseItemParam>(GEventType.EVENT_USEITEM, ref param);
            });
            comItem2.onClick.Add(() =>
            {
                GameEntity master = Contexts.Instance.game.gameMaster.entity;
                ItemComponent itemComp = master.item;
                Item item = itemComp.items[1];
                if (item.isRunCD)
                    return;
                UseItemParam param = new UseItemParam();
                param.type = AkItemType.Ak_Boom;
                EventManager.Instance.PushEvent<UseItemParam>(GEventType.EVENT_USEITEM, ref param);
            });
            comItem3.onClick.Add(() =>
            {
                GameEntity master = Contexts.Instance.game.gameMaster.entity;
                ItemComponent itemComp = master.item;
                Item item = itemComp.items[2];
                if (item.isRunCD)
                    return;
                UseItemParam param = new UseItemParam();
                param.type = AkItemType.Ak_Transform;
                EventManager.Instance.PushEvent<UseItemParam>(GEventType.EVENT_USEITEM, ref param);
            });
            textDebug = contentPane.GetChild("TextDebug").asTextField;
            textDebug.visible = false;

            imgNumLevel.Initialize(contentPane, "ComLTBar", 1);
            imgNumScore.Initialize(contentPane, "ComRTBar");
            imgNumBlue.Initialize(contentPane, "ComTarBlue");
            imgNumYellow.Initialize(contentPane, "ComTarYellow");
            imgNumRed.Initialize(contentPane, "ComTarRed");
        }
        protected override void OnShown()
        {
            EventManager.Instance.AddEvent<ScoreParam>(GEventType.EVENT_SCORECHANGE, OnScore);
            EventManager.Instance.AddEvent<LevelParam>(GEventType.EVENT_LEVELCHG, OnLevelChg);
            EventManager.Instance.AddEvent<TermsChangeParam>(GEventType.EVENT_TERMSCHANGE, RefreshTerms);
            TermsChangeParam tCParam = new TermsChangeParam();
            EventManager.Instance.PushEvent<TermsChangeParam>(GEventType.EVENT_TERMSCHANGE, ref tCParam);
        }

        protected override void OnHide()
        {
            EventManager.Instance.RemoveEvent<ScoreParam>(GEventType.EVENT_SCORECHANGE, OnScore);
            EventManager.Instance.RemoveEvent<LevelParam>(GEventType.EVENT_LEVELCHG, OnLevelChg);
            EventManager.Instance.RemoveEvent<TermsChangeParam>(GEventType.EVENT_TERMSCHANGE, RefreshTerms);
        }

        private void OnScore(ref ScoreParam param)
        {
            textScore.text = Contexts.Instance.game.score.value.ToString();
            imgNumScore.ShowImg(Contexts.Instance.game.score.value);
        }

        private void OnLevelChg(ref LevelParam param)
        {
            textLevel.text = param.level.ToString();
            imgNumLevel.ShowImg(param.level);
        }

        void RefreshTerms(ref TermsChangeParam param)
        {
            LevelTermsComponent terms = Contexts.Instance.game.levelTerms;
            int red = terms.Get(AkBoxColor.Ak_Red);
            int blue = terms.Get(AkBoxColor.Ak_Blue);
            int yellow = terms.Get(AkBoxColor.Ak_Yellow);
            textYellow.text = yellow.ToString();
            textBlue.text = blue.ToString();
            textRed.text = red.ToString();
            imgNumBlue.ShowImg(blue);
            imgNumYellow.ShowImg(yellow);
            imgNumRed.ShowImg(red);
        }

        public override void Update(float deltaTime)
        {
            UpdateDebugInfo();
            UpdateCD(deltaTime);
        }

        void UpdateCD(float deltaTime)
        {
            GameEntity master = Contexts.Instance.game.gameMaster.entity;
            ItemComponent itemComp = master.item;
            for (int i = 0; i < itemComp.items.Count; i++)
            {
                Item item = itemComp.items[i];
                if (item.isRunCD)
                {
                    if (!imgMask[i].visible)
                        imgMask[i].visible = true;
                    float per = (item.cd - item.runTime) / item.cd;
                    per = per >= 0 ? per : 0;
                    imgMask[i].fillAmount = per;
                }
                else
                {
                    if (imgMask[i].visible)
                        imgMask[i].visible = false;
                }
            }
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