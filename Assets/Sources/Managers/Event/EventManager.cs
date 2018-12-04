using System;
using System.Collections.Generic;
using GameUtil;

namespace Game
{
    public delegate void EventHandler<T>(ref T param) where T : IEventParam;

    public interface IPostEvent
    {
        void ExecCommand();
    }

    public struct PostEvent<T> : IPostEvent where T : IEventParam
    {
        public T prm;
        public EventHandler<T> callback;
        public PostEvent(EventHandler<T> _call, T _prm)
        {
            this.prm = _prm;
            this.callback = _call;
        }
        public void ExecCommand()
        {
            if (this.callback != null)
            {
                this.callback(ref this.prm);
            }
        }
    }

    public struct PostEvent2 : IPostEvent
    {
        public Action callback;
        public PostEvent2(Action _call)
        {
            this.callback = _call;
        }
        public void ExecCommand()
        {
            if (this.callback != null)
            {
                this.callback();
            }
        }
    }

    // 仅仅用于界面监听事件
    public class EventManager : Singleton<EventManager>
    {
        private Delegate[] _events = new Delegate[(int)GEventType.EVENT_MAX];
        private List<IPostEvent> _postEvents = new List<IPostEvent>();
        private bool CheckValidAdd(GEventType evt, Delegate handler)
        {
            Delegate @delegate = this._events[(int)evt];
            return @delegate == null || @delegate.GetType() == handler.GetType();
        }
        private bool CheckValidRmv(GEventType evt, Delegate handler)
        {
            Delegate @delegate = this._events[(int)evt];
            return @delegate != null && @delegate.GetType() == handler.GetType();
        }

        public void AddEvent(GEventType type, Action handler)
        {
            if (this.CheckValidAdd(type, handler))
                this._events[(int)type] = (Action)Delegate.Combine((Action)this._events[(int)type], handler);
        }
        public void RemoveEvent(GEventType type, Action handler)
        {
            if (this.CheckValidRmv(type, handler))
                this._events[(int)type] = (Action)Delegate.Remove((Action)this._events[(int)type], handler);
        }

        public void AddEvent<T>(GEventType type, EventHandler<T> handler) where T : IEventParam
        {
            if (this.CheckValidAdd(type, handler))
                this._events[(int)type] = (EventHandler<T>)Delegate.Combine((EventHandler<T>)this._events[(int)type], handler);
        }

        public void RemoveEvent<T>(GEventType type, EventHandler<T> handler) where T : IEventParam
        {
            if (this.CheckValidRmv(type, handler))
                this._events[(int)type] = (EventHandler<T>)Delegate.Remove((EventHandler<T>)this._events[(int)type], handler);
        }

        public void Clear()
        {
            for (int i = (int)GEventType.EVENT_MIN; i < (int)GEventType.EVENT_MAX; ++i)
            {
                _events[i] = null;
            }
        }

        public void PushEvent(GEventType type)
        {
            Action handler = this._events[(int)type] as Action;
            if (handler != null)
                handler();
        }

        public void PushEvent<T>(GEventType type, ref T param) where T : IEventParam
        {
            EventHandler<T> handler = _events[(int)type] as EventHandler<T>;
            if (handler != null)
                handler(ref param);
        }

        public void PostEvent<T>(GEventType type, ref T param) where T : IEventParam
        {
            EventHandler<T> refAction = this._events[(int)type] as EventHandler<T>;
            if (refAction != null)
            {
                PostEvent<T> postEvent = new PostEvent<T>(refAction, param);
                _postEvents.Add(postEvent);
            }
        }

        public void PostEvent(GEventType type)
        {
            Action refAction = this._events[(int)type] as Action;
            if (refAction != null)
            {
                PostEvent2 postEvent = new PostEvent2(refAction);
                _postEvents.Add(postEvent);
            }
        }

        public void Update()
        {
            if (_postEvents.Count > 0)
            {
                for (int i = 0; i < _postEvents.Count; ++i)
                {
                    var postEvent = _postEvents[i];
                    postEvent.ExecCommand();
                }
                _postEvents.Clear();
            }
        }
    }
}