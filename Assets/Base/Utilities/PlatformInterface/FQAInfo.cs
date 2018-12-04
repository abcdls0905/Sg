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

//客服中心相关接口
namespace GamePlatform
{

    public class FQAInfo : Singleton<FQAInfo>
    {
        private string cryptKey = "1bec508a";
        //加密
      
        private string FQAUrl
        {
            get
            {
                if (PlatformConfig.gameNet == GameNet.INNER_TEST)
                {
                    return "http://192.168.191.175:10802";
                }
                else if (PlatformConfig.gameNet == GameNet.OUTTER_TEST)
                {
                    return "http://112.73.64.105:10802";
                }
                else // 正式服
                {
                    return "http://121.201.102.238:10802";
                }

            }
        }
        private Dictionary<string, string> sDicArgs = new Dictionary<string, string>();
        //根据参数请求客服页面，参数教多，只有lang参数是必须的，其他参数非必须
        //lang:语言版本（zh_CN,zh_HK,zh_TW,en,ja）其中zh_CN=简体中文 zh_HK=繁体中文（香港）不区分港台，繁体默认值 zh_TW = 繁体中文（台湾）en=英文 ja = 日语
        // nologin：是否未登录，1-未登录，0-已登录（不传默认已登录） nick：角色名 userid：角色ID aid：账号ID
        //category：FAQ类别ID question：FAQ问题ID device：设备机型 os：操作系统（Android或iOS，注意大写A，小写i）
        //sysver：系统版本号 appver：应用版本号 scriptver：脚本版本号 goto：需要跳转到的模块："topic"为默认值，"advice"则会跳到"反馈建议"模块
        //paycost：累积充值 email：玩家邮箱帐号 extdata：额外的拓展字段，json格式，只有一级，不能嵌套。exp:{"a":"1", "b":"2",.....}

        public string GetFQAPage(string lang, int nologin = 0, string nick = null, string userid = null, string serverid = null, string aid = null, string category = null, string question = null, string device = null, 
            string os = null, string sysver = null, string appver = null, string scriptver = null, string gotoType = null, string paycost = null, string email = null, string extdata=null)
        {
            sDicArgs.Clear();
            sDicArgs.Add("gate", PlatformConfig.productGate);
            sDicArgs.Add("lang", lang);
            if (!string.IsNullOrEmpty(nick))
            {
                sDicArgs.Add("nick", PlatformUtil.Str2Hex(nick));
            }
            if (!string.IsNullOrEmpty(userid))
            {
                sDicArgs.Add("userid", userid);
            }
            if (!string.IsNullOrEmpty(serverid))
            {
                sDicArgs.Add("serverid", serverid);
            }
            if (!string.IsNullOrEmpty(aid))
            {
                sDicArgs.Add("aid", aid);
            }
            if (!string.IsNullOrEmpty(category))
            {
                sDicArgs.Add("category", category);
            }
            if (!string.IsNullOrEmpty(question))
            {
                sDicArgs.Add("question", question);
            }
            if (!string.IsNullOrEmpty(device))
            {
                sDicArgs.Add("device", device);
            }
            if (!string.IsNullOrEmpty(os))
            {
                sDicArgs.Add("os", os);
            }
            if (!string.IsNullOrEmpty(sysver))
            {
                sDicArgs.Add("sysver", sysver);
            }
            if (!string.IsNullOrEmpty(appver))
            {
                sDicArgs.Add("appver", appver);
            }
            if (!string.IsNullOrEmpty(scriptver))
            {
                sDicArgs.Add("scriptver", scriptver);
            }
            if (nologin != 0)
            {
                sDicArgs.Add("nologin", nologin.ToString());
            }
            if (!string.IsNullOrEmpty(gotoType))
            {
                sDicArgs.Add("goto", gotoType);
            }
            if (!string.IsNullOrEmpty(paycost))
            {
                sDicArgs.Add("paycost", paycost);
            }
            if (!string.IsNullOrEmpty(email))
            {
                sDicArgs.Add("email", email);
            }
            if (!string.IsNullOrEmpty(extdata))
            {
                sDicArgs.Add("extdata", extdata);
            }
            string info = PlatformUtil.GetUrlParame(sDicArgs);
            string infoEct = DES.Encrypt(info, cryptKey);
            string strRequest = FQAUrl + "/views/kf.aspx?gameinfo=" + infoEct;//游戏简称_语言版本.txt
            string strRet = PlatformUtil.GetHttpResponse(strRequest);
            return strRet;

        }
    }
}
