
using System;
using GameUtil;
using UnityEngine;
using System.Threading;
using System.Collections.Generic;

namespace Game
{
    public enum AkThreadState
    {
        AK_None = 0,
        AK_Started,
        AK_Finished,
    }
    
    public enum AkMainThreadState
    {
        AK_None = 0,
        AK_Assemblied,
        AK_Waiting,
        AK_Obtained,
    }

    public class GameThread
    {
        object objLock;
        //AkThreadState只在支线程中修改
        private AkThreadState eState;
        //AkMainThreadState只在主线程中修改
        public AkMainThreadState eMainState;
        public ThreadStart evThreadHandle = null;
        public GameThread(ThreadStart start)
        {
            evThreadHandle = start;
            eState = AkThreadState.AK_None;
            eMainState = AkMainThreadState.AK_None;
            objLock = new object();
        }

        public void SetState(AkThreadState s)
        {
            if (eState == s)
                return;
            lock (objLock)
            {
                eState = s;
            }
        }

        public AkThreadState GetState()
        {
            return eState;
        }

        public object LockObject()
        {
            return objLock; 
        }

        public void OnDestory()
        {
            objLock = null;
            evThreadHandle = null;
        }
    }

    public class ThreadMananger : MonoSingleton<ThreadMananger>
    {
        public Thread mainThread = null;
        public Thread subThread = null;
        List<GameThread> threadList = new List<GameThread>();
        //毫秒
        public static int frameTime = 16;
        private object objLock = new object();

        public override void Init()
        {
            base.Init();
            if (GameEnvSetting.UseMultiThread)
            {
                mainThread = Thread.CurrentThread;
                subThread = new Thread(SubThreadUpdate);
                subThread.Priority = System.Threading.ThreadPriority.Normal;
                subThread.Start();
            }
        }

        public GameThread CreateThread(ThreadStart start)
        {
            GameThread gameThread = new GameThread(start);
            lock(objLock)
            {
                threadList.Add(gameThread);
            }
            return gameThread;
        }

        public void Remove(GameThread thread)
        {
            lock(objLock)
            {
                int handleCount = threadList.Count;
                for (int i = 0; i < handleCount; i++)
                {
                    GameThread t = threadList[i];
                    if (t == thread)
                    {
                        t.OnDestory();
                        threadList.RemoveAt(i);
                        return;
                    }
                }
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (subThread != null && subThread.ThreadState == ThreadState.Running)
            {
                subThread.Abort();
                subThread = null;
            }
            int handleCount = threadList.Count;
            for (int i = 0; i < handleCount; i++)
            {
                threadList[i].OnDestory();
            }
            threadList.Clear();
            objLock = null;
        }

        public static void Sleep(int time, bool forceSleep = false)
        {
            if (time > 0 || forceSleep)
                Thread.Sleep(time);
        }

        public bool IsMainThread(GameThread gameThread)
        {
            return mainThread == subThread;
        }

        public void SubThreadUpdate()
        {
            while (true)
            {
                lock (objLock)
                {
                    int handleCount = threadList.Count;
                    for (int i = 0; i < handleCount; i++)
                    {
                        GameThread thread = threadList[i];
                        thread.evThreadHandle();
                    }
                }
                Sleep(1);
            }
        }

        void OnApplicationQuit()
        {
            OnDestroy();
        }

    }
}