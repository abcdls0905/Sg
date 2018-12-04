using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using Game;
using DG.Tweening;

namespace Game
{
    public class PopBase
    {

    }

    public class ScoreInfo : PopBase
    {
        const string packageName = "Game";
        const string infoCompName = "ComPopScore";
        public bool isFinish;
        GComponent comScore;
        GTextField textScore;
        float moveOffset = 65;
        float moveValue;
        float moveSpeed = 65;

        public ScoreInfo(GComponent parent)
        {
            comScore = UIPackage.CreateObject(packageName, infoCompName).asCom;
            parent.AddChild(comScore);
            textScore = comScore.GetChild("n1").asTextField;
        }

        public void Initialize(int score = 0)
        {
            moveValue = 0;
            isFinish = false;
            comScore.visible = true;
            comScore.alpha = 1;
            textScore.text = score.ToString();
            Vector2 pos = Util.WorldPosToUIPos(new Vector3(5, 0, 5));
            comScore.SetXY(pos.x - 100, pos.y);
        }

        public void UpdatePosition(float deltaTime)
        {
            Vector3 pos = comScore.position;
            moveValue += moveSpeed * deltaTime;
            pos.y -= moveSpeed * deltaTime;
            comScore.SetXY(pos.x, pos.y);
            float alpha = (moveOffset - moveValue) / moveOffset;
            comScore.alpha = Mathf.Clamp(alpha, 0, 1);
            if (moveValue >= moveOffset)
                isFinish = true;
        }

        public void OnFinish()
        {
            comScore.visible = false;
        }

        public void Update(float deltaTime)
        {
            UpdatePosition(deltaTime);
        }
    }

    public class ComboInfo : PopBase
    {
        const string packageName = "Game";
        const string infoCompName = "ComPopCombo";
        public bool isFinish;
        GComponent comScore;
        GTextField textScore;
        float moveOffset = 65;
        float moveValue;
        float moveSpeed = 65;
        ImgNumList imgNumCombo;

        public ComboInfo(GComponent parent)
        {
            comScore = UIPackage.CreateObject(packageName, infoCompName).asCom;
            parent.AddChild(comScore);
            textScore = comScore.GetChild("n1").asTextField;
            imgNumCombo = new ImgNumList(3, "ImgCombo");
            imgNumCombo.Initialize(comScore);
        }

        public void Initialize(int score = 0)
        {
            moveValue = 0;
            isFinish = false;
            comScore.visible = true;
            comScore.alpha = 1;
            textScore.text = "Combo " + score;
            Vector2 pos = Util.WorldPosToUIPos(new Vector3(5, 0, 5));
            comScore.SetXY(pos.x + 100, pos.y);
            imgNumCombo.ShowImg(score);
        }

        public void UpdatePosition(float deltaTime)
        {
            Vector3 pos = comScore.position;
            moveValue += moveSpeed * deltaTime;
            pos.y -= moveSpeed * deltaTime;
            comScore.SetXY(pos.x, pos.y);
            float alpha = (moveOffset - moveValue) / moveOffset;
            comScore.alpha = Mathf.Clamp(alpha, 0, 1);
            if (moveValue >= moveOffset)
                isFinish = true;
        }

        public void OnFinish()
        {
            comScore.visible = false;
        }

        public void Update(float deltaTime)
        {
            UpdatePosition(deltaTime);
        }
    }

    public class GamePopInfoPage : UIPage
    {
        const string packageName = "Game";
        const string componentName = "GamePopInfoPage";

        private List<ScoreInfo> activeInfo;
        private List<ScoreInfo> cacheInfo;

        private List<ComboInfo> activeInfoCombo;
        private List<ComboInfo> cacheInfoCombo;

        protected override void OnInit()
        {
            InitWindow(packageName, componentName, UIPageLayer.Default, true);
            activeInfo = new List<ScoreInfo>();
            cacheInfo = new List<ScoreInfo>();

            activeInfoCombo = new List<ComboInfo>();
            cacheInfoCombo = new List<ComboInfo>();
        }

        protected override void OnShown()
        {
            EventManager.Instance.AddEvent<ScoreParam>(GEventType.EVENT_SCORECHANGE, OnScore);
            EventManager.Instance.AddEvent<ComboParam>(GEventType.EVENT_COMBO, OnCombo);
        }

        protected override void OnHide()
        {
            EventManager.Instance.RemoveEvent<ScoreParam>(GEventType.EVENT_SCORECHANGE, OnScore);
            EventManager.Instance.RemoveEvent<ComboParam>(GEventType.EVENT_COMBO, OnCombo);
        }

        public override void Update(float deltaTime)
        {
            UpdateScoreInfo(deltaTime);
            UpdateComboInfo(deltaTime);
        }

        public void UpdateScoreInfo(float deltaTime)
        {
            for (int i = activeInfo.Count - 1; i >= 0; i--)
            {
                ScoreInfo scoreInfo = activeInfo[i];
                if (scoreInfo.isFinish)
                {
                    scoreInfo.OnFinish();
                    cacheInfo.Add(scoreInfo);
                    activeInfo.RemoveAt(i);
                    continue;
                }
                scoreInfo.Update(deltaTime);
            }
        }

        private void OnScore(ref ScoreParam param)
        {
            ScoreInfo scoreInfo = null;
            if (cacheInfo.Count > 0)
            {
                scoreInfo = cacheInfo[0];
                cacheInfo.RemoveAt(0);
            }
            else
            {
                scoreInfo = new ScoreInfo(contentPane);
            }
            scoreInfo.Initialize(param.score);
            activeInfo.Add(scoreInfo);
        }

        private void UpdateComboInfo(float deltaTime)
        {
            for (int i = activeInfoCombo.Count - 1; i >= 0; i--)
            {
                ComboInfo comboInfo = activeInfoCombo[i];
                if (comboInfo.isFinish)
                {
                    comboInfo.OnFinish();
                    cacheInfoCombo.Add(comboInfo);
                    activeInfoCombo.RemoveAt(i);
                    continue;
                }
                comboInfo.Update(deltaTime);
            }
        }

        private void OnCombo(ref ComboParam param)
        {
            ComboInfo comboInfo = null;
            if (cacheInfoCombo.Count > 0)
            {
                comboInfo = cacheInfoCombo[0];
                cacheInfoCombo.RemoveAt(0);
            }
            else
            {
                comboInfo = new ComboInfo(contentPane);
            }
            comboInfo.Initialize(param.combo);
            activeInfoCombo.Add(comboInfo);
        }
    }
}
