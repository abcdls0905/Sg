


using FairyGUI;
using UnityEngine;

namespace Game
{
    class GameLeftJoystic : UIPage
    {
        public float offseth = 0.0f;
        public float offsetv = 0.0f;
        public float offsetPixl = 0.0f;
        public float distance = 0.0f;

        const string packageName = "Game";
        const string componentName = "GameLeftJoystic";
        public JoystickModule joystick;
        public GComponent panel;
        protected override void OnInit()
        {
            InitWindow(packageName, componentName, UIPageLayer.LeftJoystic, true);
            joystick = new JoystickModule(contentPane);
            panel = contentPane.GetChild("JoystickMove").asCom;
        }

        protected override void OnHide()
        {
            offseth = 0.0f;
            offsetv = 0.0f;
            offsetPixl = 0.0f;
            distance = 0.0f;

            joystick.OnDispose();
            EventManager.Instance.RemoveEvent(GEventType.EVENT_OPENBACKPACK, HideJoystick);
        }

        protected override void OnShown()
        {
            EventManager.Instance.AddEvent(GEventType.EVENT_OPENBACKPACK, HideJoystick);
        }

        private void HideJoystick()
        {
            if (!joystick.IsButtonDowm())
                joystick.ClearData();
        }

        public float GetRadius()
        {
            if (joystick != null)
                return joystick.GetRadius();
            return 0.0f;
        }

    }
}
