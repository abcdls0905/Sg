/*  
 *  Description: 客户端崩溃日志上传服务器，并且按照ELK规则上传。
 *               通过了封装接口，便于项目使用。采用
 *
 *  CreatedTime: 2018/3/30 9:21:32
 *  Author:      wanghong01
 *  Company:     duoyi
 *
 */

using System;
using System.Text;
using UnityEngine;
using System.Security.Cryptography;
using System.IO;

namespace GameUtil
{
    public class CrashReportToELK : Singleton<CrashReportToELK>
    {
        public delegate void SendCrashMsg(string logType, string level, string log, string stack, DateTime time);

        /// <summary>
        /// ELK服务器的url
        /// </summary>
        public string url = "";

        /// <summary>
        /// app的名称，统一使用小写字符串
        /// </summary>
        public string appName = "";
        /// <summary>
        /// app唯一标识
        /// </summary>
        public string uuid = "0";
        /// <summary>
        /// 发送消息回调
        /// </summary>
        public SendCrashMsg CSharpCrashFunction = null;
        /// <summary>
        /// 捕获C#宕机
        /// </summary>
        public bool EnableCSharpCrash = true;

        private string timeZone = "";
        private string os = "Others";

        private class JsonData
        {
            //[JsonProperty(PropertyName = "@timestamp")]
            public string timestamp;
            public string level;
            public string logtype;
            public string app;
            public string os;
            public string crashmsg;
            public string uuid;
            public JsonData(string time, string level, string type, string appName, string os, string msg, string uuid)
            {
                timestamp = time;
                this.level = level;
                logtype = type;
                app = appName;
                this.os = os;
                crashmsg = msg;
                this.uuid = uuid;
            }
        }

        public override void Init()
        {
            timeZone = GetTimeZone();
#if UNITY_EDITOR
            os = "Editor";
#elif UNITY_IPHONE
            os = "Iphone";
#elif UNITY_ANDROID
            os = "Android";
#elif UNITY_STANDALONE
            os = "PC";
#endif
        }

        /// <summary>
        /// 生成字符串md5
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private string GetMD5(string msg)
        {
            var md5 = new MD5CryptoServiceProvider();
            byte[] data = Encoding.UTF8.GetBytes(msg);
            byte[] md5Data = md5.ComputeHash(data, 0, data.Length);
            md5.Clear();

            string destString = "";
            for (int i = 0; i < md5Data.Length; ++i)
                destString += Convert.ToString(md5Data[i], 16).PadLeft(2, '0');

            destString = destString.PadLeft(32, '0');
            return destString;
        }

        /// <summary>
        /// 格式化时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private string FormatTime(DateTime time)
        {
            return string.Format("{0:yyyy-MM-ddTHH:mm:ss.fff}", time) + timeZone;
        }

        /// <summary>
        /// 获取时区
        /// </summary>
        /// <returns>时区字符串 格式+0800</returns>
        private string GetTimeZone()
        {
			var refNow = DateTime.Now;
            var timeSpan = refNow - refNow.ToUniversalTime();
            string destString = ((timeSpan >= TimeSpan.Zero) ? "+" : "-") + string.Format("{0:hhmm}", timeSpan); // timeSpan.ToString("hhmm");

            return destString;
        }

        private void iOSCrashNotify()
        {
            if (CrashReport.lastReport != null)
            {
                SendLog("clientcrash", "iOSCrash", CrashReport.lastReport.text, "", CrashReport.lastReport.time);
                CrashReport.RemoveAll();
            }
        }
#if UNITY_ANDROID
        private void AndroidCrashNotify(string crashFile)
        {
            string phoneInfo, javaInfo, linuxInfo;
            if (AndroidCrashReport.Instance.GetCrashInifo(out phoneInfo, out javaInfo, out linuxInfo) == 0)
            {
                string msg = "PhoneInfo:\n" + phoneInfo + "\nJavaStack:\n" + javaInfo;
                if (!String.IsNullOrEmpty(linuxInfo))
                    msg += "LinuxStack:\n" + linuxInfo;

                SendLog("clientcrash", "AndroidCrash", msg, null, DateTime.Now);
            }
        }
#endif
        private void CSharpCrashNotify(string logType, string level, string logString, string stack, DateTime time)
        {
            if (!EnableCSharpCrash)
                return;

            if (CSharpCrashFunction != null)
                CSharpCrashFunction(logType, level, logString, stack, time);
            SendLog(logType, level, logString, stack, time);
        }

        /// <summary>
        /// 发送数据到url的服务器
        /// 编辑器模式下不能发送数据到服务器，避免服务器上充满各种调试日志
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="level">日志level</param>
        /// <param name="logString">日志</param>
        /// <param name="stack">堆栈信息</param>
        public void SendLog(string logType, string level, string logString, string stack, DateTime time)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
                return;

            string full_url = url + "/create?app=" + appName + "&logtype=" + logType + "&sign=" + GetMD5(appName + logType + "dychengdu");

            string msg = logString;
            if (!String.IsNullOrEmpty(stack))
                msg += "\nStack:\n" + stack;
            string json = JsonUtility.ToJson(new JsonData(FormatTime(time), level, logType, appName, os, msg, uuid), true);
            json = json.Replace("timestamp", "@timestamp");
            var www = new WWW(full_url, Encoding.UTF8.GetBytes(json));
            while (!www.isDone) { }
            Debug.Log(www.text);
        }

        /// <summary>
        /// 注册获取日志
        /// </summary>
        public void Install()
        {
            CSharpCrashReport.Install(CSharpCrashNotify);
#if UNITY_IOS
                iOSCrashNotify();
#endif
#if UNITY_ANDROID
                AndroidCrashReport.Instance.SetCallBack(AndroidCrashNotify);
                AndroidCrashReport.Instance.Install();
#endif
        }

        /// <summary>
        /// 卸载获取日志
        /// </summary>
        public void Uninstall()
        {
            CSharpCrashReport.Uninstall();
#if UNITY_ANDROID
                AndroidCrashReport.Instance.Uninstall();
#endif
        }
    }
}
