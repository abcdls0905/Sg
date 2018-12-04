using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace GameUtil
{
    //
    //NetState.Clear();//清空初始化，第一次统计也需要调用
    //NetState.UpdateNet(); 
    //GetSend()获取发送字节数
    //GetRecive()获取接收字节数
    //GetAll()获取发送和接收总字节数

    public class NetState
    {
#if !UNITY_EDITOR && UNITY_IPHONE
        [DllImport("__Internal")]
        public static extern void Clear();

        [DllImport("__Internal")]
        public static extern void UpdateNet();

        [DllImport("__Internal")]
        public static extern double GetReceive();

        [DllImport("__Internal")]
        public static extern double GetSend();

        [DllImport("__Internal")]
        public static extern double GetAll();


#elif !UNITY_EDITOR && UNITY_ANDROID
        public static void UpdateNet() { }

        public static void Clear() 
        { 
             AndroidJavaClass jc = new AndroidJavaClass("com.duoyi.netstate.NetState");
             jc.CallStatic("Clear");
        }

        public static double GetReceive() 
        { 
             AndroidJavaClass jc = new AndroidJavaClass("com.duoyi.netstate.NetState");
            return jc.CallStatic<double>("GetReceive");
        }

        public static double GetSend() 
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.duoyi.netstate.NetState");
            return jc.CallStatic<double>("GetSend");
        }

        public static double GetAll()
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.duoyi.netstate.NetState");
            return jc.CallStatic<double>("GetAll");
        }


#else

        public static void UpdateNet() { }

        public static void Clear() 
        { 
        }

        public static double GetReceive() 
        {
            return 0f;
        }

        public static double GetSend() 
        {
            return 0f;
        }

        public static double GetAll()
        {
            return 0f;
        }


#endif
    }
}