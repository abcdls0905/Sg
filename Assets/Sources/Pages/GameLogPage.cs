using FairyGUI;
using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    public class GameLogPage : UIPage
    {
        const string packageName = "Common";
        const string componentName = "GameLogPage";
        GTextField textError;

        protected override void OnInit()
        {
            InitWindow(packageName, componentName, UIPageLayer.GM);
            textError = contentPane.GetChild("TextError").asTextField;
        }

        public void SetError(string str)
        {
            textError.text = str;
        }
    }
}