using System;
using System.Collections.Generic;
using System.Reflection;

namespace Game
{
    public interface IState
    {
        string name
        {
            get; set;
        }
        bool OnStateEnterCheck();
        bool OnStateLeaveCheck();
        void OnStateEnter();
        void OnStateLeave();
        void OnStateOverride();
        void OnStateResume();
        void OnUpdate();
        void OnFixedExecute();
    }

    public class StateMachine
    {
        protected Dictionary<string, IState> _registedState = new Dictionary<string, IState>();
        protected Stack<IState> _stateStack = new Stack<IState>();
        public IState tarState
        {
            get;
            private set;
        }
        public int Count
        {
            get
            {
                return _stateStack.Count;
            }
        }
        public void RegisterState(string name, IState state)
        {
            if (name == null || state == null)
            {
                return;
            }
            if (_registedState.ContainsKey(name))
            {
                return;
            }
            state.name = name;
            _registedState.Add(name, state);
        }
        public void RegisterState<TStateImplType>(TStateImplType State, string name) where TStateImplType : IState
        {
            RegisterState(name, State);
        }
        public IState UnregisterState(string name)
        {
            if (name == null)
                return null;
            IState result;
            if (!_registedState.TryGetValue(name, out result))
                return null;
            _registedState.Remove(name);
            return result;
        }
        public IState GetState(string name)
        {
            if (name == null)
                return null;
            IState state = null;
            _registedState.TryGetValue(name, out state);
            return state;
        }
        public string GetStateName(IState state)
        {
            if (state == null)
                return null;
            var enumerator = _registedState.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (current.Value == state)
                    return current.Key;
            }
            return null;
        }
        public void Push(IState state)
        {
            if (state == null)
                return;
            if (!state.OnStateEnterCheck())
                return;
            if (_stateStack.Count > 0)
                _stateStack.Peek().OnStateOverride();
            _stateStack.Push(state);
            state.OnStateEnter();
        }
        public void Push(string name)
        {
            if (name == null)
                return;
            IState state;
            if (!_registedState.TryGetValue(name, out state))
                return;
            Push(state);
        }
        public IState PopState()
        {
            if (_stateStack.Count <= 0)
                return null;
            IState state = _stateStack.Pop();
            state.OnStateLeave();
            if (_stateStack.Count > 0)
                _stateStack.Peek().OnStateResume();
            return state;
        }
        public IState ChangeState(IState state, bool force)
        {
            if (state == null)
                return null;
            IState state2 = TopState();
            if (!force)
            {
                if (!state.OnStateEnterCheck() || (state2 != null && !state2.OnStateLeaveCheck()))
                    return null;
            }
            tarState = state;
            if (_stateStack.Count > 0)
            {
                state2.OnStateLeave();
                _stateStack.Pop();
            }
            _stateStack.Push(state);
            state.OnStateEnter();
            return state2;
        }
        public bool IsState(string name)
        {
            IState state = TopState();
            if (state == null)
                return false;
            return state.name == name;
        }
        virtual public IState ChangeState(string name, bool force = false)
        {
            //UnityEngine.Debug.Log(name);
            if (name == null)
                return null;
            IState state;
            if (!_registedState.TryGetValue(name, out state))
                return null;
            return ChangeState(state, force);
        }
        public IState TopState()
        {
            if (_stateStack.Count <= 0)
                return null;
            return _stateStack.Peek();
        }
        public string TopStateName()
        {
            if (_stateStack.Count <= 0)
                return null;
            IState state = _stateStack.Peek();
            return state.name;
        }
        public void Clear()
        {
            while (_stateStack.Count > 0)
            {
                _stateStack.Pop().OnStateLeave();
            }
        }
    }
}