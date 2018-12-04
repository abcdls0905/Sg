using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;

namespace GameUtil
{
    public class AccountBind : MonoBehaviour
    {
        //private static string[] bundleslist = { "com.jiajiu" };

        public static Dictionary<string, string> keyChain = new Dictionary<string, string>
        {
            {"dytestaccount" , "dytestaccount"},
            {"dytestaccount_new" , "dytestaccount_v2"},
            {"ccaccount" , "check_duoyi_account"},
            {"split" , "@mobile"},
            {"bundleid" , "com.duoyient"},
        };
#if !UNITY_EDITOR && UNITY_IPHONE
	    [DllImport("__Internal",CallingConvention = CallingConvention.Cdecl)]
        public static extern System.IntPtr GetChain(string key);
#endif
        //public static string ret = "li123123@mobile1312213";
        public static string GetKeyChain(string key)
        {
            string ret = null;
            if (key == null || key.Length == 0)
                return ret;

#if !UNITY_EDITOR && UNITY_IPHONE
            System.IntPtr ptr = GetChain(key);
            ret = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ptr);
            Debug.Log("PtrToStringAnsi cc->" + key + ":" + ret);
#elif !UNITY_EDITOR && UNITY_ANDROID
            AndroidJavaClass jc = new AndroidJavaClass("com.duoyi.imcheck.ImActivity");
            ret = jc.CallStatic<string>("getChain", new object[] { key });
            Debug.Log("getChain cc->" + key + ":" + ret);
#endif
            return ret;
        }
       
    }
}
