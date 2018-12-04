using FairyGUI;
using System;

namespace Game
{
    [XLua.LuaCallCSharp]
    public class UIWindow : Window
    {
        public event Action onInit;
        public event Action onShown;
        public event Action onHide;

        public string WindowName
        {
            set
            {
                this.rootContainer.gameObject.name = value;
            }
            get
            {
                return this.rootContainer.gameObject.name;
            }
        }

        override protected void OnInit()
        {
            if (onInit != null)
                onInit();
        }

        override protected void OnShown()
        {
            if (onShown != null)
                onShown();
        }

        override protected void OnHide()
        {
            if (onHide != null)
                onHide();
        }

        override protected void DoShowAnimation()
        {
            OnShown();
        }

        override protected void DoHideAnimation()
        {
            this.HideImmediately();
        }
    }
}
