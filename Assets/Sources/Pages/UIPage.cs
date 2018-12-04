
using FairyGUI;
using System;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace Game
{
    [LuaCallCSharp]
    public enum UIPageLayer : int
    {
        HeadBar,
        Effect,
        Default,
        Chat,
        LeftJoystic,

        MiniMap,
        EasyTouch,
        HudPage,

        RightJoystic,
        Notice,
        GuidePage,
        Setting,
        Account,

        ExitGame,
        FinalPage,
        SelectScene,
        Shop,
        Tips,
        GM,
    }
    [LuaCallCSharp]
    public class UIPage : Window
    {
        public bool needCache;
        public void InitWindow(string packageName, string componentName, UIPageLayer layer = UIPageLayer.Default, bool needCache = false)
        {
            this.contentPane = (GComponent)UIPackage.CreateObject(packageName, componentName);
            this.SetSize(GRoot.inst.width, GRoot.inst.height);
            this.sortingOrder = (int)layer;
            this.needCache = needCache;
            this.contentPane.opaque = false;

            FitiPhoneX();
        }
        public virtual void OnDispose()
        {

        }
        public virtual void Update(float deltaTime)
        {

        }

        public virtual void FitiPhoneX()
        {
            GameInterface.FitiPhoneX(this);
        }
    }
}