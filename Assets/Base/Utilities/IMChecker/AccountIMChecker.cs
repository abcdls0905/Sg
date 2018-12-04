using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;
using System.IO;
using UnityEngine.Networking;

namespace GameUtil
{
    /// 用法:将此脚本挂接到场景中的常驻GemeObject上
    /// 检测触发包名：当打包时BundleIdentify字符中含有"com.jiajiu", "com.duoyient"就参与检测
    /// 检测触时机：游戏启动或从后台切到前台
    /// 检测内容有2项：
    /// 1：火星，没有安装提示安装，超过一天没有登录提示登录火星
    /// 2：火星关联的游戏帐号变更后，游戏需弹窗是否切换帐号选择
    /// 用到的函数有3个：
    /// 函数1：public static string[] GetCCAccAndPsw()；获取火星关联的帐号和密码
    /// 函数2： static public void SetUsedAcc(string acc)；登录后设置登入的帐号，以便后台能检测帐号变更
    /// 函数3：public static void RegFunAccChanged(Action act)；注册帐号变更的回调函数，后台检测到帐号变更后好通知游戏端，需要游戏端做出是否切换帐号响应。牵扯到具体登录，所以中间件只能通知了
    public class AccountIMChecker : MonoSingleton<AccountIMChecker>
    {
        public static int RequestTimeOut = 2000;
        public static string RequestString = "https://im.2980.com:10011/game_check_account?name=";
        public static int MaxCheckTimes = 3;
        public static bool DontCheckCC = false;
        private static string[] bundleslist = { "com.jiajiu", "com.duoyient" };

        private string sUsedAcc = string.Empty;
        //保存已经登录的帐号，用来判断是否变更
        public void SetUsedAcc(string acc)
        {
            sUsedAcc = acc;
        }

        public void RegFunAccChanged(Action act)
        {
            ActOnAccountChanged = act;
        }

        private Action ActOnAccountChanged; // 外部只关心cc帐号已经发生改变

        ///获取cc对应的游戏帐号和密码
        public string[] GetCCAccAndPsw()
        {
            string dyEncodeCCAccount = string.Empty;
            string keyChainValue = string.Empty;
            if (GameUtil.AccountBind.keyChain.TryGetValue("dytestaccount_new", out keyChainValue))
            {
                dyEncodeCCAccount = GameUtil.AccountBind.GetKeyChain(keyChainValue);
                if (dyEncodeCCAccount != null && !dyEncodeCCAccount.Equals(string.Empty))
                {
                    string[] a = { "@mobile" };
                    string[] accAndPsw = dyEncodeCCAccount.Split(a, System.StringSplitOptions.None);
                    if (accAndPsw != null && accAndPsw.Length > 0)
                    {
                        return accAndPsw;
                    }
                }
            }
            return null;
        }

        public bool IsFitBundle()
        {
            for (int i = 0; i < bundleslist.Length; ++i)
            {
                string bundleid = bundleslist[i];
                if (IMChecker.IsFitBundle(bundleid))
                {
                    return true;
                }

            }
            return false;
        }
        public bool IsNeedCheck()
        {
            if (DontCheckCC)
            {
                return false;
            }
//#if (!UNITY_EDITOR && UNITY_IPHONE)
#if (!UNITY_EDITOR && (UNITY_IPHONE || UNITY_ANDROID))
            return IsFitBundle();
#endif
            return false;

        }

        //到服务器检测CC帐号是否有效
        public bool IsCCAccountValid(string dyTestAccount, out bool timeout)
        {
            timeout = true;
            HttpWebResponse response = null;
            try
            {
                string url = RequestString + dyTestAccount;
                response = HttpUtil.CreateGetHttpResponse(url, RequestTimeOut, null, null);
                Stream stream = response.GetResponseStream();   //获取响应的字符串流
                StreamReader sr = new StreamReader(stream); //创建一个stream读取流
                string html = sr.ReadToEnd();   //从头读到尾，放到字符串html
                sr.Close();
                stream.Close();

                timeout = false;
                if (html.StartsWith("0"))
                    return true;
                else
                    return false;
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning(ex.ToString());
            }
            finally
            {
                if (response == null)
                    response.Close();
            }
            return true;
        }

        public IEnumerator CheckCCAccount(string dyTestAccount)
        {
            string url = RequestString + dyTestAccount;

            bool quitGame = false;
            bool checkLogin = true;
            for (int i = 0; i < MaxCheckTimes; ++i)
            {
                UnityWebRequest request = UnityWebRequest.Get(url);
                yield return request.SendWebRequest();
                if (request.isNetworkError)
                {
                    continue;
                }
                else
                {
                    if (request.responseCode == 200)
                    {
                        string text = request.downloadHandler.text;
                        if (text.StartsWith("0"))
                            checkLogin = false;
                        else
                            quitGame = true;
                        break;
                    }
                }
            }
            if (checkLogin)
                IMChecker.CheckImLogin();
            if (quitGame)
                Application.Quit();
        }

        public override void Init()
        {
            OnApplicationFocus(true);
        }

        public override void UnInit()
        {
        }

        void OnApplicationFocus(bool focus)
        {
            if (focus == true && IsNeedCheck())
            {
                CheckCCState();
                CheckAccChanged();
            }
        }

        ///检测cc帐号, 如果发生网络切换，第一次request是会失败的。
        public void CheckCCState()
        {
            //判断是否联网
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                IMChecker.CheckImLogin();
                return;
            }

            //检测设备是否储存CC帐号
            bool isExistLocalCCAcount = false;
            string dyCCAccount = string.Empty;
            string keyChainValue = string.Empty;
            
            if (AccountBind.keyChain.TryGetValue("ccaccount", out keyChainValue))//获取火星用户名
            {
                dyCCAccount = AccountBind.GetKeyChain(keyChainValue);
                if (!string.IsNullOrEmpty(dyCCAccount))
                {
                    isExistLocalCCAcount = true;
                    StartCoroutine(CheckCCAccount(dyCCAccount));
                }
            }

            if (!isExistLocalCCAcount)
            {
                IMChecker.CheckImLogin();
                return;
            }
        }

        ///检测cc的帐号已经变更，要通知登入端从新登入
        private void CheckAccChanged()
        {
            string[] curCCAccAndPsw = GetCCAccAndPsw();
            if (curCCAccAndPsw != null && curCCAccAndPsw.Length > 1)
            {
                if (!string.IsNullOrEmpty(sUsedAcc))
                {
                    if (!curCCAccAndPsw[0].Equals(sUsedAcc))
                    {
                       // 通知登入端帐号变更了，可以选择是否进行重新登入
                        if(ActOnAccountChanged != null)
                        {
                            ActOnAccountChanged();
                        }
                    }
                }
            }
        }



    }
}
