
using System.Collections.Generic;
using UnityEngine;
using GameUtil;

namespace Game
{
    public class DelayEffect
    {
        public GameObject gameObject;
        public float lifeTime;
        public float lastTime;
        public DelayEffect(GameObject gameObject, float lifeTime)
        {
            this.gameObject = gameObject;
            this.lifeTime = lifeTime;
            lastTime = 0;
        }
        public DelayEffect()
        {
            lastTime = 0;
        }
    }

    public class DelayManager : MonoSingleton<DelayManager>
    {
        List<DelayEffect> delayList = new List<DelayEffect>();
        void Update()
        {
            for (int i = delayList.Count - 1; i >= 0; i--)
            {
                DelayEffect delay = delayList[i];
                delay.lastTime += Time.deltaTime;
                if (delay.lastTime > delay.lifeTime)
                {
                    delayList.RemoveAt(i);
                    GameObjectPool.Instance.RecycleGameObject(delay.gameObject);
                }
            }
        }

        public void AddDelay(GameObject gameObject, float lifeTime)
        {
            DelayEffect delay = new DelayEffect(gameObject, lifeTime);
            delayList.Add(delay);
        }

        public override void UnInit()
        {

        }
    }
}