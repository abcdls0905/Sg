using System;
using System.Collections.Generic;
using System.Threading;
namespace GameUtil
{
    public class BackgroundWorker : Singleton<BackgroundWorker>
    {
        public delegate void BackgroudDelegate();
        private Thread WorkingThread;
        private bool bRequestExit;
        private List<BackgroudDelegate> PendingWork = new List<BackgroudDelegate>();
        private List<BackgroudDelegate> WorkingList = new List<BackgroudDelegate>();
        public int ThreadID;
        public override void Init()
        {
            WorkingThread = new Thread(new ThreadStart(StaticEntry));
            ThreadID = WorkingThread.ManagedThreadId;
            WorkingThread.Start();
        }
        public override void UnInit()
        {
            bRequestExit = true;
            WorkingThread.Join();
            WorkingThread = null;
        }
        protected static void StaticEntry()
        {
            Instance.Entry();
        }
        private static void Swap<T>(ref T a, ref T b)
        {
            T t = a;
            a = b;
            b = t;
        }
        protected void Entry()
        {
            while (!bRequestExit)
            {
                List<BackgroudDelegate> pendingWork = PendingWork;
                System.Threading.Monitor.Enter(pendingWork);
                try
                {
                    Swap(ref PendingWork, ref WorkingList);
                }
                finally
                {
                    System.Threading.Monitor.Exit(pendingWork);
                }
                int count = WorkingList.Count;
                for (int i = 0; i < count; i++)
                {
                    try
                    {
                        WorkingList[i]();
                    }
                    catch (Exception)
                    {
                    }
                }
                WorkingList.Clear();
                Thread.Sleep(30);
            }
        }
        public void AddBackgroudOperation(BackgroudDelegate InDelegate)
        {
            List<BackgroudDelegate> pendingWork = PendingWork;
            System.Threading.Monitor.Enter(pendingWork);
            try
            {
                PendingWork.Add(InDelegate);
            }
            finally
            {
                System.Threading.Monitor.Exit(pendingWork);
            }
        }
    }
}