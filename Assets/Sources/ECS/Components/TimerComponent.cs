
using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;

namespace Game
{
    public delegate void TimerCallBack(GameEntity entity);
    public class Timer
    {
        public float lastTime;
        public float interval;
        public TimerCallBack callBack;
        public ulong entityId;
        public Timer(float i, ulong id, TimerCallBack call)
        {
            lastTime = 0;
            interval = i;
            callBack = call;
            entityId = id;
        }
    }

    [Game]
    [Unique]
    public class TimerComponent : IComponent
    {
        public List<Timer> timers = new List<Timer>();
        public void Reset()
        {
            timers.Clear();
        }

        public void AddTimer(Timer timer)
        {
            timers.Add(timer);
        }
    }
}