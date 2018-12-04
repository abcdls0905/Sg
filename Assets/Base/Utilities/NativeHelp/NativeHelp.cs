using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace GameUtil
{
    //一些原生接口的集合，避免过多的jar包
    public class NativeHelp
    {
#if UNITY_IPHONE
        [DllImport("__Internal")]
        public static extern System.IntPtr GetNetTypeI(); // 返回的字符需要转换
        public static  string GetNetType()
        {
            string ret = null;
            System.IntPtr ptr = GetNetTypeI();
            ret = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ptr);
            return ret;

        }


#elif UNITY_ANDROID //!UNITY_EDITOR && 
        private static AndroidJavaClass jc = null;
        private static AndroidJavaClass GetJc()
        {
            if(jc == null)
            {
                jc = new AndroidJavaClass("com.duoyi.nativehelp.NativeHelp");
            }
            return jc;

        }
        //判定是否是streamingassets里的文件或文件夹,可以输入全路径或相对于Streamingassets的路径
        public static bool IsStreamingAssetsExists(string path) 
		{
            path = ParsePath(path);
            return GetJc().CallStatic<bool>("IsAssetsFile", path);
        }

        // 加载streamingAssets下的文件
        //调用格式：NativeHelp.LoadStreamingAssetsFile(Application.streamingAssetsPath + "/file");
        public static byte[] LoadStreamingAssetsFile(string path)
        {
            path = ParsePath(path);
            return GetJc().CallStatic<byte[]>("LoadAssetsFile", path);
        }

        //获取移动网络类型 no(没联网), wifi,2g,3g,4g,unknown（未知联网类型）
        public static string GetNetType()
        {
            return GetJc().CallStatic<string>("GetNetType");
        }

        //获取ip地址 格式为192.168.1.1
        public static string GetIp()
        {
            return GetJc().CallStatic<string>("GetIp");
        }

        //获取mac地址 格式为020AC0000002,12位大写的16进制
        public static string GetMac()
        {
            return GetJc().CallStatic<string>("GetMac");
        }

        //获取非系统app的安装时间和更新时间，返回这种类型组合name1_time1_time2&name2_time1_time2&... 给大数据统计
        public static string GetAppsTimeInfo()
        {
            return GetJc().CallStatic<string>("GetAppsTimeInfo");
        }

        //获取imei值
        public static string GetImei()
        {
            return GetJc().CallStatic<string>("GetImei");
        }

        //返回基于streamingassets的相对路径
        private static string ParsePath(string strPath)
        {
            int keyLen = 8;
            int idx = strPath.IndexOf("!assets/"); //bundle加载路径:Application.dataPath+"!assets" = /data/app/com.xxx.apk!assets
            if (idx == -1)
            {
                idx = strPath.IndexOf("!/assets/"); //www加载路径:Application.streamingAssetsPath = jar:file:///data/app/com.xxx.apk!/assets
                keyLen = 9;
            }
            if (idx != -1)
            {
                strPath = strPath.Substring(idx + keyLen);
            }
            return strPath;
            
        }


#else

        public static bool IsAssetsFile() { return false; }

#endif
    }
}