/*  
 *  Description: 捕获Unity项目C#层的崩溃的错误信息和异常信息
 *
 *  CreatedTime: 2018/3/30 9:59:17
 *  Author:      wanghong01
 *  Company:     duoyi
 *
 */

using UnityEngine;

namespace GameUtil
{
    public class CSharpCrashReport
    {
        private static CrashReportToELK.SendCrashMsg SendMsg;
        
        /// <summary>
        /// 注册log事件的监听接口
        /// </summary>
        /// <param name="callBack">将log消息发送出去的回调接口</param>
        public static void Install(CrashReportToELK.SendCrashMsg callBack)
        {
            if (callBack != null)
            {
                SendMsg = callBack;
                Application.logMessageReceived += Handle;
                Application.SetStackTraceLogType(LogType.Error, StackTraceLogType.Full);
                Application.SetStackTraceLogType(LogType.Exception, StackTraceLogType.Full);
                Application.SetStackTraceLogType(LogType.Assert, StackTraceLogType.Full);
            }
        }

        /// <summary>
        /// 取消相应的设置
        /// </summary>
        public static void Uninstall()
        {
            SendMsg = null;
            Application.logMessageReceived -= Handle;
            Application.SetStackTraceLogType(LogType.Error, StackTraceLogType.ScriptOnly);
            Application.SetStackTraceLogType(LogType.Exception, StackTraceLogType.ScriptOnly);
        }

        private static void Handle(string logString, string stackTrace, LogType type)
        {
            if (type == LogType.Error)
                SendMsg("clientcrash", "Error", logString, stackTrace, System.DateTime.Now);
            else if (type == LogType.Exception)
                SendMsg("clientcrash", "Exception", logString, stackTrace, System.DateTime.Now);
            else if (type == LogType.Assert)
                SendMsg("clientcrash", "Assert", logString, stackTrace, System.DateTime.Now);
        }
    }
}
