using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace GameUtil
{
    public class LogListener : MonoBehaviour
    {
        string outPath = string.Empty;

        bool hasError = false;
        bool needSend = true;
        bool isQuit = false;

        public string errorStr = "";

        public delegate void ShowErrorFunction(string log, string stack);
        public ShowErrorFunction ShowErrorFn;

        public delegate void ShowLogFunction(string log);
        public ShowLogFunction ShowLogFn;

        List<string> mWriteTxt = new List<string>();
        void Awake()
        {
            string outDir = Application.persistentDataPath + "/logs";
            outPath = outDir + "/outlog.txt";
            string outPrevPath = outDir + "/outlog_prev.txt";

            if (!Directory.Exists(outDir))
                Directory.CreateDirectory(outDir);
            if (File.Exists(outPrevPath))
                File.Delete(outPrevPath);
            if (File.Exists(outPath))
                File.Move(outPath, outPrevPath);

            using (var Writer = new StreamWriter(outPath, false, Encoding.UTF8))
            {
                Writer.WriteLine(System.DateTime.Now);
                Writer.WriteLine("LOG Start");
                Writer.Flush();
                Writer.Close();
            }
            //在这里做一个Log的监听
            Application.logMessageReceived += HandleLog;
            Application.SetStackTraceLogType(LogType.Error, StackTraceLogType.Full);
            Application.SetStackTraceLogType(LogType.Exception, StackTraceLogType.Full);
        }

        protected void OnApplicationQuit()
        {
            isQuit = true;
        }
        void Update()
        {
            try {
                if (mWriteTxt.Count > 0)
                {
                    using (var Writer = new StreamWriter(outPath, true, Encoding.UTF8))
                    {
                        for (int i = 0; i < mWriteTxt.Count; ++i)
                            Writer.WriteLine(mWriteTxt[i]);
                        Writer.Flush();
                        Writer.Close();
                    }
                    mWriteTxt.Clear();
                }
                if (hasError && needSend)
                {
                    needSend = false;
                    //FileSendUtil.SendFile(outPath);
                }
            }
            catch
            {
                // do nothing
            }
        }

        void HandleLog(string logString, string stackTrace, LogType type)
        {
            if (isQuit)
                return;
            mWriteTxt.Add(System.DateTime.Now.ToString());
            mWriteTxt.Add(logString);
            mWriteTxt.Add(stackTrace);

            if (type == LogType.Error || type == LogType.Exception)
            {
                hasError = true;
                if (!isQuit)
                {
                    if (ShowErrorFn != null)
                        ShowErrorFn(logString, stackTrace);
                    Game.GameLogPage gameLogPage = Game.UIManager.Instance.OpenPage<Game.GameLogPage>();
                    gameLogPage.SetError(stackTrace);
                }
            }
            if (!isQuit)
            {
                if (ShowLogFn != null)
                    ShowLogFn(logString);
            }
        }

    }
}