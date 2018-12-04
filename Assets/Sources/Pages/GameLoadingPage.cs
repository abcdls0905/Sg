using FairyGUI;
using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    public class GameLoadingPage : UIPage
    {
        const string packageName = "Start";
        const string componentName = "StartLoadingPage";

        GImage imgProgress;
        GTextField text;
        protected override void OnInit()
        {
            InitWindow(packageName, componentName);

            GComponent comProgress = contentPane.GetChild("comProgress").asCom;
            imgProgress = comProgress.GetChild("fillAmount").asImage;
            text = comProgress.GetChild("title").asTextField;
        }

        public void SetFillAmount(float value)
        {
            imgProgress.fillAmount = value;
            text.text = ((int)(value * 100)).ToString() + "%";
        }
    }
}