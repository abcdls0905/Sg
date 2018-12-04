
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Game
{

    public class GameStateBase : IState
    {
        protected GameStateMachine parent;
        public GameStateBase(GameStateMachine parent)
        {
            this.parent = parent;
        }
        public string name
        {
            get; set;
        }
        public virtual bool OnStateEnterCheck() { return true; }
        public virtual bool OnStateLeaveCheck() { return true; }
        public virtual void OnStateEnter() { }
        public virtual void OnStateLeave() { }
        public virtual void OnStateOverride() { }
        public virtual void OnStateResume() { }
        public virtual void OnUpdate() { }
        public virtual void OnFixedExecute() { }
        public bool CheckStateValid(ref string[] names, string _name)
        {
            if (names == null)
                return false;
            for (int i = 0; i < names.Length; ++i)
            {
                if (_name == names[i])
                    return false;
            }
            return true;
        }
    }

    public class GameStateMachine : StateMachine
    {
        public void Update()
        {
            if (tarState != null)
                tarState.OnUpdate();
        }
    }
}