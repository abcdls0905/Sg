using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections;

namespace GameUtil
{
    public class Keychain
    {
#if UNITY_IPHONE
        [DllImport("__Internal", EntryPoint = "GetKeyChain")]
        public static extern System.IntPtr __GetKeyChain(string key);
        [DllImport("__Internal", EntryPoint = "SaveKeyChain")]
        public static extern void __SaveKeyChain(string key, string value);
        [DllImport("__Internal", EntryPoint = "DeleteKeyChain")]
        public static extern void __DeleteKeyChain(string key);

        [DllImport("__Internal", EntryPoint = "GetKeyChain2")]
        public static extern System.IntPtr __GetKeyChain2(string key, string service);

        public static string GetKeyChain(string key)
        {
            System.IntPtr ptr = __GetKeyChain(key);
            string value = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ptr);
            Debug.Log("Keychain GetKeyChain key: " + key + " value:" + value);
            return value;
        }

        public static string GetKeyChain2(string key, string service)
        {
            System.IntPtr ptr = __GetKeyChain2(key, service);
            string value = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ptr);
            Debug.Log("Keychain GetKeyChain key: " + key + " value:" + value);
            return value;
        }

        public static void SaveKeyChain(string key, string value)
        {
            Debug.Log("Keychain SaveKeyChain key: " + key + " value:" + value);
            __SaveKeyChain(key, value);
        }

        public static void DeleteKeyChain(string key)
        {
            Debug.Log("Keychain DeleteKeyChain key: " + key);
            __DeleteKeyChain(key);
        }
#else
        public static string GetKeyChain(string key)
        {
            return PlayerPrefs.GetString(key, "");
        }
        public static string GetKeyChain2(string key, string service)
        {
            return PlayerPrefs.GetString(key, "");
        }

        public static void SaveKeyChain(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        public static void DeleteKeyChain(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }
#endif
    }
}