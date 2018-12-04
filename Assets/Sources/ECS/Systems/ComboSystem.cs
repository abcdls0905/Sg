using Entitas;
using UnityEngine;
using Game;
using System;
using GameJson;
using System.Collections.Generic;

namespace Game
{
    public class ComboSystem : IInitializeSystem, IFixedExecuteSystem
    {
        //public void Ini7
        public void Initialize()
        {
            EventManager.Instance.AddEvent<DesGroupParam>(GEventType.EVENT_BOXDESTORYGROUP, DestroyGroup);
        }

        public void FixedExecute()
        {
            float frameTime = Contexts.Instance.game.frame.frameTime;
            ComboComponent combo = Contexts.Instance.game.combo;
            combo.lastTime += frameTime;
            if (combo.lastTime >= 5)
            {
                combo.value = 0;
            }
        }

        public void DestroyGroup(ref DesGroupParam param)
        {
            ComboComponent combo = Contexts.Instance.game.combo;
            bool isPositive = false;
            for (int i = 0; i < param.boxes.Count; i++)
            {
                GameEntity entity = param.boxes[i];
                if (entity.box.isPositive)
                {
                    isPositive = true;
                    break;
                }
            }
            if (isPositive)
            {
                combo.lastTime = 0;
                combo.value++;
                ComboParam comboParam = new ComboParam();
                comboParam.combo = combo.value;
                EventManager.Instance.PushEvent<ComboParam>(GEventType.EVENT_COMBO, ref comboParam);
            }
        }
    }
}