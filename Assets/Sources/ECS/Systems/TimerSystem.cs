using Entitas;
using Game;
using System.Collections.Generic;

namespace Game
{
    public class TimerSystem : IFixedExecuteSystem, IExecuteSystem
    {
        public TimerSystem()
        {

        }

        public void Execute()
        {

        }

        public void FixedExecute()
        {
            TimerComponent timerComp = Contexts.Instance.game.timer;
            for (int i = timerComp.timers.Count - 1; i >= 0; i--)
            {
                if (!ExecuteTimer(timerComp.timers[i]))
                    timerComp.timers.RemoveAt(i);
            }
        }

        bool ExecuteTimer(Timer timer)
        {
            float frameTime = Contexts.Instance.game.frame.frameTime;
            timer.lastTime += frameTime;
            if (timer.lastTime >= timer.interval)
            {
                GameEntity entity = Util.GetEntityWithId(timer.entityId);
                if (entity == null)
                    return false;
                timer.callBack(entity);
                return false;
            }
            return true;
        }
    }
}