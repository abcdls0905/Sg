/*  
 *  Description:用于实现捕获Android系统崩溃的相关信息。
 *              该文件是对CrashReport.jar的封装，方便Unity项目C#代码调用
 *              捕获的信息包括：手机相关信息；Java层堆栈信息；Linux层c/c++堆栈信息
 *  CreatedTime: 2018/3/23 14:21:20
 *  Author:      wanghong01
 *  Company:     duoyi
 *
 */

using System;
using UnityEngine;
using System.IO;
#if UNITY_ANDROID
namespace GameUtil
{
    public class AndroidCrashReport
    {
        public delegate void notifyCrashInfoIsSave(string crashFilePath);

        /// <summary>
        /// 解析崩溃信息的JSON数据结构
        /// </summary>
        private class CrashData
        {
            public string phone = null;
            public string java = null;
            public string linux = null;
        }

        private class UnityCallBack : AndroidJavaProxy
        {
            private notifyCrashInfoIsSave notifyCallBack = null;

            public UnityCallBack(notifyCrashInfoIsSave fun) : base("com.duoyi.crashreport.UnityCallBack")
            {
                notifyCallBack = fun;
            }

            void CrashInfoIsSave()
            {
                if (notifyCallBack != null)
                {
                    notifyCallBack(Instance.androidCrashInfoFile);
                }
            }
        }

        private static AndroidCrashReport _instance = null;
        public static AndroidCrashReport Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AndroidCrashReport();

                return _instance;
            }
        }

        private AndroidJavaObject javaCrashObj;
        public string androidCrashInfoFile = "";

        private AndroidCrashReport()
        {
            // C#调用Java的相关处理
            var javaCrashClass = new AndroidJavaClass("com.duoyi.crashreport.CallMain");
            javaCrashObj = javaCrashClass.CallStatic<AndroidJavaObject>("GetInstance");

            var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            var appContext = activity.Call<AndroidJavaObject>("getApplicationContext");
            javaCrashObj.Call("AndroidBound", appContext);

            // 设置一个保存crash信息的文件路径
            var dir = Application.persistentDataPath + "/logs";
            androidCrashInfoFile = dir + "/androidCrash.txt";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            if (File.Exists(androidCrashInfoFile))
                File.Delete(androidCrashInfoFile);
                

            javaCrashObj.Call("SetCrashFilePath", androidCrashInfoFile);

        }

        /// <summary>
        /// 设置崩溃时的回调接口，通知unity崩溃数据已经保存到文件中了
        /// </summary>
        /// <param name="callBack">回调函数</param>
        public void SetCallBack(notifyCrashInfoIsSave callBack)
        {
            javaCrashObj.Call("SetUnityCallBack", new UnityCallBack(callBack));
        }

        /// <summary>
        /// 安装崩溃检测
        /// 一般应该在游戏启动的时候执行一次
        /// </summary>
        public void Install()
        {
            javaCrashObj.Call("Install");
        }

        /// <summary>
        /// 卸载崩溃检测
        /// 游戏退出前执行一次。
        /// </summary>
        public void Uninstall()
        {
            javaCrashObj.Call("Uninstall");
        }

        /// <summary>
        /// 解析保存在crash文件中的信息，crash文件中保存的是一个json格式的文本
        /// </summary>
        /// <param name="phoneInfo">手机相关信息</param>
        /// <param name="javaInfo">java层的堆栈信息</param>
        /// <param name="linuxInfo">linux层的堆栈信息</param>
        /// <returns>0：解析成功；其余值失败</returns>
        public int GetCrashInifo(out string phoneInfo, out string javaInfo, out string linuxInfo)
        {
            phoneInfo = null;
            javaInfo = null;
            linuxInfo = null;

            string crashInfo = "";
            using (var reader = new StreamReader(Instance.androidCrashInfoFile))
            {
                crashInfo = reader.ReadToEnd();
                reader.Close();
            }

            if (String.IsNullOrEmpty(crashInfo))
                return -1;

            var crashData = JsonUtility.FromJson<CrashData>(crashInfo);
            phoneInfo = crashData.phone;
            javaInfo = crashData.java;
            linuxInfo = crashData.linux;

            return 0;
        }

        /*
        public void TestJava(int number)
        {
            javaCrashObj.Call("TestUncatchException", number);
        }

        public void TestLinux(int number)
        {
            javaCrashObj.Call("TestLinuxCrash", number);
        }
        */
        
    }
}
#endif