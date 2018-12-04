using FairyGUI;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace Game
{
    public class JoystickModule : EventDispatcher
    {
        GButton _button;
        GButton _center;
        GComponent _mainView;

        int _touchId;
        float _radius;
        float _limitRadius;
        Rect _limitArea;
        Vector2 _startSave;
        Vector2 _lastSave;
        Vector2 _InitPos;

        public JoystickModule(GComponent panel)
        {
            _mainView = panel.GetChild("JoystickMove").asCom;
            _button = _mainView.GetChild("Button").asButton;
            _button.visible = true;

            _center = _mainView.GetChild("Center").asButton;
            _InitPos.x = _center.x + _center.width / 2;
            _InitPos.y = _center.y + _center.height / 2;

            var config = DataManager.Instance.pageDatas.Data;
            _radius = config.GameLeftJoystick.radius;
            _limitRadius = config.GameLeftJoystick.limitRadius;

            GObject limitArea = panel.GetChild("LimitArea");
            float limitX = limitArea.x - _mainView.x;
            float limitY = limitArea.y - _mainView.y;
            _limitArea = new Rect(limitX, limitY, limitArea.width, limitArea.height);

            GObject touchArea = panel.GetChild("TouchArea");
            touchArea.onTouchBegin.Add(OnTouchDown);
            ClearData();
        }

        public float GetRadius()
        {
            return _radius;
        }

        public bool IsStaticMode()
        {
            bool staticMode = PlayerSettingManager.Instance.CheckPlayerSetting(PlayerSetting.StaticLeftJoystic);
            staticMode = true;
            return staticMode;
        }

        public void SetMovePosition(Vector2 position)
        {
            var leftJoystic = UIManager.Instance.GetPage<GameLeftJoystic>();
            if (leftJoystic != null)
            {
                leftJoystic.offseth = position.x / _radius;
                leftJoystic.offsetv = -position.y / _radius;
                leftJoystic.offsetPixl = position.magnitude;
                leftJoystic.distance = position.magnitude / _radius;
            }
        }

        private void OnTouchDown(EventContext context)
        {
            InputEvent evt = (InputEvent)context.data;
            _touchId = evt.touchId;

            Vector2 pt = GRoot.inst.GlobalToLocal(evt.position);
            _lastSave.x = pt.x - _mainView.x;
            _lastSave.y = pt.y - _mainView.y;

            Vector2 limitPos = GetLimitPosition();
            //先设置center再计算pos
            SetCenterPos(limitPos.x, limitPos.y);
            SetButtonPos(limitPos);


            _button.GetController("button").selectedPage = "down";
            _center.selected = true;
            _center.visible = true;
            _button.visible = true;

            Stage.inst.onTouchMove.Add(this.OnTouchMove);
            Stage.inst.onTouchEnd.Add(this.OnTouchUp);
        }
        private void SetButtonPos(Vector2 pos)
        {
            Vector2 direction = pos - _startSave;
            if (direction.magnitude > _radius)
            {
                direction = direction.normalized * _radius;
            }

            Vector2 position = direction + _startSave;
            _button.x = position.x - _button.width / 2;
            _button.y = position.y - _button.height / 2;
            SetMovePosition(direction);
        }

        public void OnDispose()
        {
            ClearData();
        }

        private void OnTouchUp(EventContext context)
        {
            InputEvent inputEvt = (InputEvent)context.data;
            if (_touchId == -1 || inputEvt.touchId != _touchId)
                return;

            ClearData();
        }

        public void ClearData()
        {
            _touchId = -1;
            _center.visible = false;
            _center.position = Vector2.zero;
            _button.visible = true;
            _lastSave = _InitPos;
            _startSave = _InitPos;
            _button.TweenMove(_InitPos, 0.3f);
            _button.GetController("button").selectedPage = "up";

            Stage.inst.onTouchMove.Remove(this.OnTouchMove);
            Stage.inst.onTouchEnd.Remove(this.OnTouchUp);

            SetMovePosition(Vector2.zero);
        }

        private Vector2 GetLimitPosition()
        {
            Vector2 pos = _lastSave;
            Vector2 center = new Vector2(_center.x + _center.width / 2, _center.y + _center.height / 2);
            Vector2 direction = _lastSave - center;
            float distance = direction.magnitude;

            if (distance > _limitRadius)
            {
                pos = center + (distance - _limitRadius + _radius) * direction.normalized;
            }
            else
            {
                if (distance > _radius)
                {
                    pos = center + _radius * direction.normalized;
                }
            }

            return pos;
        }

        private void SetCenterPos(float x, float y)
        {
            float centerX = Mathf.Clamp(x, _limitArea.x, _limitArea.x + _limitArea.width);
            float centerY = Mathf.Clamp(y, _limitArea.y, _limitArea.y + _limitArea.height);

            _startSave.x = centerX;
            _startSave.y = centerY;

            _center.x = centerX - _center.width / 2;
            _center.y = centerY - _center.height / 2;
        }
        private void OnTouchMove(EventContext context)
        {
            InputEvent evt = context.inputEvent;
            if (_touchId == -1 || evt.touchId != _touchId)
                return;

            Vector2 pt = GRoot.inst.GlobalToLocal(evt.position);
            _lastSave.x = pt.x - _mainView.x;
            _lastSave.y = pt.y - _mainView.y;

            Vector2 limitPos = GetLimitPosition();
            Vector2 offset = limitPos - _startSave;
            float dis = offset.magnitude;
            bool isStaticMode = IsStaticMode();
            if (dis > _radius && !isStaticMode)
            {
                Vector2 dir = offset.normalized;
                Vector2 delta = dir * (dis - _radius);
                SetCenterPos(delta.x + _startSave.x, delta.y + _startSave.y);
            }
            SetButtonPos(limitPos);
        }

        public bool IsButtonDowm()
        {
            return this._touchId != -1;
        }
    }
}