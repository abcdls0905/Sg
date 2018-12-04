using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Net.NetworkInformation;
using GameUtil;


namespace GamePlatform
{
    /// <summary>
    /// 用于构建大数据内容的一些固定参数获取
    /// </summary>
    public class BigDataBase
    {
        //固定格式的时间
        public static string GetCurTime()
        {
            return string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
        }

        public static string GetNetType()
        {
            string strTyp = "";
#if UNITY_ANDROID || UNITY_IPHONE
            strTyp = NativeHelp.GetNetType();
#else
            NetworkReachability rb = Application.internetReachability;
            if (rb == NetworkReachability.ReachableViaLocalAreaNetwork)
                strTyp = "wifi";
            else if (rb == NetworkReachability.ReachableViaCarrierDataNetwork)
                strTyp = "liuliang";
            else
                strTyp = "no";
#endif
            return strTyp;
        }

        // 获取mac地址， iOS8后无法获取mac地址
        public static string GetMAC()
        {
            string macAddresses = "";
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            for (int i = 0; i < adapters.Length; ++i)
            {
                NetworkInterface adapter = adapters[i];
                if (adapter.Description == "en0")
                {
                    macAddresses = adapter.GetPhysicalAddress().ToString();
                    break;
                }
                else
                {
                    macAddresses = adapter.GetPhysicalAddress().ToString();
                    if (macAddresses != "")
                    {
                        break;
                    }
                }
            }
        return macAddresses;
        }

        //获取ip地址
        public static string GetIP()
        {
            string ipAddresses = "";
#if UNITY_ANDROID
            ipAddresses = NativeHelp.GetIp();
#else
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            for (int i = 0; i < adapters.Length; ++i)
            {
                NetworkInterface adapter = adapters[i];
                if (adapter.Supports(NetworkInterfaceComponent.IPv4))
                {
                    UnicastIPAddressInformationCollection uniCast = adapter.GetIPProperties().UnicastAddresses;
                    for (int j = 0; j < uniCast.Count; ++j)
                    {
                        UnicastIPAddressInformation uni = uniCast[j];
                        if (uni.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            ipAddresses = uni.Address.ToString();
                        }
                    }
                }
            }
#endif
            return ipAddresses;

        }

        public static string GetAppsTimeInfo()
        {
            string ret = "";
#if UNITY_ANDROID
            ret = NativeHelp.GetAppsTimeInfo();
#endif
            return ret;
        }

        //获取imei
        public static string GetImei()
        {
            string imei = "";
#if UNITY_ANDROID
            imei = NativeHelp.GetImei();
#endif
            return imei;

        }

        //唯一标志符
        public static string GetIdentifierForVendor()
        {
#if UNITY_IPHONE
            return UnityEngine.iOS.Device.vendorIdentifier;
#else
            return "";
#endif
        }

        // 广告id， Ios专有
        public static string GetIdentifierForAdvertising()
        {
#if UNITY_IPHONE
            return UnityEngine.iOS.Device.advertisingIdentifier;
#else
            return "";
#endif
        }


        public static string GetSysVersion()
        {
#if UNITY_IPHONE
            return UnityEngine.iOS.Device.systemVersion;
#else
            return SystemInfo.operatingSystem;
#endif
        }

        public static string GetDeviceType()
        {
            return GetDeviceType(SystemInfo.deviceModel);
        }
        static Dictionary<string, string> model2Type = new Dictionary<string, string> {
            {"iPhone5,1", "iPhone5"}, {"iPhone5,2", "iPhone5"}, {"iPhone5,3", "iPhone5c"}, {"iPhone5,4", "iPhone5c"},
            {"iPhone6,1", "iPhone5s"}, {"iPhone6,2", "iPhone5s"}, {"iPhone7,1", "iPhone6Plus"}, {"iPhone7,2", "iPhone6"},
            {"iPhone8,1", "iPhone6s"}, {"iPhone8,2", "iPhone6sPlus"}, {"iPhone8,3", "iPhoneSE"}, {"iPhone8,4", "iPhoneSE"},
            {"iPhone9,1", "iPhone7"}, {"iPhone9,2", "iPhone7Plus"},
            {"iPhone10,1", "iPhone8"}, {"iPhone10,2", "iPhone8Plus"},{"iPhone10,3", "iPhoneX"},
            {"iPhone10,4", "iPhone8"}, {"iPhone10,5", "iPhone8Plus"},{"iPhone10,6", "iPhoneX"},

            {"iPad4,4", "iPadmini2"}, {"iPad4,5", "iPadmini2"}, {"iPad4,6", "iPadmini2"}, {"iPad4,7", "iPadmini3"},
            {"iPad4,8", "iPadmini3"}, {"iPad4,9", "iPadmini3"}, {"iPad5,1", "iPadmini4"}, {"iPad5,2", "iPadmini4"},
        };
        static string GetDeviceType(string deviceModel)
        {
            if (model2Type.ContainsKey(deviceModel))
                return model2Type[deviceModel];
            else
                return deviceModel;
        }

        public static bool IsiPhoneX()
        {
            return SystemInfo.deviceModel == "iPhone10,3" || SystemInfo.deviceModel == "iPhone10,6";
        }

    }
}

       

