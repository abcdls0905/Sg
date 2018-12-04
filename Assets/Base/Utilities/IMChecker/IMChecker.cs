using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections;


public class IMChecker
{
	#if !UNITY_EDITOR && UNITY_IPHONE
	[DllImport("__Internal",CallingConvention = CallingConvention.Cdecl)]
	public static extern void iCheckIMLogin(float expireDay);
	#else
	public static void iCheckIMLogin(float expireDay){}
#endif

    private static bool FORCE_CHECK = false;
    private static string[] bundleslist = { "com.duoyient", "com.duoyinet" };

    void Awake()
    {
#if UNITY_ANDROID
        OnApplicationFocus(true);
#endif
    }
    public static bool NeedCheckIMLogin()
	{
        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
        {
            if (FORCE_CHECK == true)
            {
                return true;
            }
            for(int i = 0; i < bundleslist.Length; ++i)
            {
                string bundleid = bundleslist[i];
                if(IsFitBundle(bundleid))
                {
                    return true;
                }

            }
		}
		return false;
	}

    public static bool IsFitBundle(string bundleid)
    {
#if UNITY_2017
                int npos = Application.identifier.IndexOf(bundleid);
#else
        int npos = Application.bundleIdentifier.IndexOf(bundleid);
#endif

        if (npos == 0)
        {
            return true;
        }
        return false;
    }

	void OnApplicationFocus(bool focus)
	{
        if (focus == true && NeedCheckIMLogin())
        {
            CheckImLogin();
        }
    }

    // 没有登入提示登入，超过一天提示重新登入
    public static void CheckImLogin()
    {
#if UNITY_IPHONE
            iCheckIMLogin(1);
#elif UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.duoyi.imcheck.ImActivity");
        jc.CallStatic("checkIMLogin", new object[] { 1.0f });
#endif
    }
}
