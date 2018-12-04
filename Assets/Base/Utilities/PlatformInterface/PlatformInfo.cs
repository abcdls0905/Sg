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
 
    //平台相关的一些简单的请求直接放置在这里，不单独列文件，复杂的单独文件处理包含：
    //1获取通知的构造函数
    //2服务条款的url
    //3帐号系统相关的一些接口，目前并为使用
    public class PlatformInfo : Singleton<PlatformInfo>
    {
        private  Dictionary<string, string> sDicArgs = new Dictionary<string, string>();
        private string productGate = PlatformConfig.productGate;

        private string NoticeUrl
        {
            get
            {
                if(PlatformConfig.gameNet == GameNet.OUTTER_TEST)
                {
                    return "http://112.73.64.109:802";
                }
                else if(PlatformConfig.gameNet == GameNet.OUTER_GAME)
                {
                    return "http://w2img.duoyi.com";
                }
                else
                {
                    return "http://10.17.64.52:8094";
                }
                     
            }
        }

        //获取通知接口
        //lang:语言(ch,big5,en,jp)其中ch为简体中文，big5繁体中文，en英文，jp日语
        public RetNotice GetNotice(string lang)
        {
            string msg = NoticeUrl + "/js/notice/" + PlatformConfig.productGate + "_" + lang + ".txt";//游戏简称_语言版本.txt    
            string strRet = GetHttpResponse(msg, null);
            RetNotice ret = null;
            if (!string.IsNullOrEmpty(strRet))
            {
                ret = JsonUtility.FromJson<RetNotice>(strRet);
            }
            return ret;
        }

        // 服务条款
        public string AgreementUrl
        {
            get
            {
                return "https://id.duoyi.com/html/service-agreement.html";
            }
        }



        public string GetHttpResponse(string url, Dictionary<string, string> parameters = null)
        {
            if (parameters != null && parameters.Count > 0)
            {
                url += "?" + PlatformUtil.GetUrlParame(parameters);
            }
            string ret = PlatformUtil.GetHttpResponse(url); ;
            return ret;
        }

        // 获取签名串
        private string CalcSign(Dictionary<string, string> dic)
        {
            List<string> listKey = new List<string>(dic.Keys);
            listKey.Sort();
            string strSign = "";
            for (int i = 0; i < listKey.Count; i++)
            {
                strSign += listKey[i] + dic[listKey[i]];
            }
            strSign += "#erv$ed%@3l4";
            strSign = strSign.ToLower();
            strSign = PlatformUtil.GetMd5Hash(strSign);
            return strSign;
        }

       


        public enum RegType
        {
            DEFAULT = 0,// 字母， 只需要  acct,  pass 两个参数
            PHONE = 1,//手机：需要所有参数
            MAINL,// 2980邮箱：炫耀所有参数

        }
        //注册帐号 acct帐号 pass密码 phone：手机号 vcode：验证码
        public  RetRegAcct RegAcct(RegType regType, string acct, string pass,string phone = null, string vcode = null)
        {
            RetRegAcct ret = null;
            string msg = "urs/reg_acct";
            pass = PlatformUtil.GetMd5Hash(pass);
            sDicArgs.Clear();
            sDicArgs.Add("acct", acct);
            sDicArgs.Add("pass", pass);
            sDicArgs.Add("gate", productGate);
            if (regType != RegType.DEFAULT)
            {
                sDicArgs.Add("phone", phone);
                sDicArgs.Add("vcode", vcode);
            }
            sDicArgs.Add("signmode", "1");
            sDicArgs.Add("sign", CalcSign(sDicArgs));
            string strRet = GetResp(msg, sDicArgs);
            if(strRet != "")
            {
                ret = JsonUtility.FromJson<RetRegAcct>(strRet);
            }
            return ret;

        }

        //x通过MAC取回注册的游戏账号
        public  string GetAcctByMac(string mac=null)
        {
            string msg = "urs/get_acct_by_mac";
            if (mac == null)
            {
                NetworkInterface[] nis = NetworkInterface.GetAllNetworkInterfaces();
                mac = nis[0].GetPhysicalAddress().ToString();
                mac = mac.ToLower();
            }
            sDicArgs.Clear();
            sDicArgs.Add("gate", productGate);
            sDicArgs.Add("mac", mac);
            sDicArgs.Add("signmode", "1");
            sDicArgs.Add("sign", CalcSign(sDicArgs));
            return GetResp(msg, sDicArgs);
        }

        //x手机号找回帐号（目前只支持找回非2980帐号）
        public  string GetAcctByMoblie(string phone, string smscode)
        {
            string msg = "urs/get_acct_by_mobile";
            sDicArgs.Clear();
            sDicArgs.Add("phone", phone);
            sDicArgs.Add("smscode", smscode);
            return GetResp(msg, sDicArgs);
        }

        //获取帐号可用于找回密码的途径信息
        public  string GetPassMethod(string acct, string vkey = null, string vcode = null)
        {
            string msg = "urs/get_pass_method";
            sDicArgs.Clear();
            sDicArgs.Add("acct", acct);
            if(vkey != null)
            {
                sDicArgs.Add("vkey", vkey);
            }
            if(vcode != null)
            {
                sDicArgs.Add("vcode", vcode);
            }
            sDicArgs.Add("gate", productGate);
            return GetResp(msg, sDicArgs);
        }

        //通过手机号找回(设置)密码 
        //acct:战盟帐号 phone:帐号绑定的手机号码 smscode:短信验证码 pass:密码
        public  string ModifyPasswordByMobile(string acct, string phone, string smscode, string pass)
        {
            string msg = "urs/modify_password_by_mobile";
            sDicArgs.Clear();
            sDicArgs.Add("acct", acct);
            sDicArgs.Add("phone", phone);
            sDicArgs.Add("smscode", smscode);
            pass = PlatformUtil.GetMd5Hash(pass);
            sDicArgs.Add("pass", pass);
            return GetResp(msg, sDicArgs);
        }

        //发送找回战盟帐号密码的邮件
        //acct:帐号 lang:区域/语言
        public  string GetPwdByMail(string acct, string lang = null)
        {
            string msg = "urs/get_pwd_by_mail";
            sDicArgs.Clear();
            sDicArgs.Add("acct", acct);
            sDicArgs.Add("gate", productGate);
            if(lang != null)
            {
                sDicArgs.Add("lang", lang);
            }
            return GetResp(msg, sDicArgs);
        }

        //通过邮件链接修改密码
        //pass:新密码 email_id:邮件链接中的aid参数 key:邮件编号的md5值，链接中的key参数
        public  string modify_password_by_mail(string pass, string email_id, string key)
        {
            string msg = "urs/modify_password_by_mail";
            sDicArgs.Clear();
            pass = PlatformUtil.GetMd5Hash(pass);
            sDicArgs.Add("pass", pass);
            sDicArgs.Add("email_id", email_id);
            sDicArgs.Add("key", key);
            return GetResp(msg, sDicArgs);
        }

        //检查找回链接是否超时
        //email_id:邮件链接中的aid参数 key:邮件编号的md5值，链接中的key参数
        public  string check_mail_expire(string email_id, string key)
        {
            string msg = "urs/check_mail_expire";
            sDicArgs.Clear();
            sDicArgs.Add("email_id", email_id);
            sDicArgs.Add("key", key);
            return GetResp(msg, sDicArgs);
        }

        //重发激活邮件 只针对非2980邮箱的战盟账号
        public  string ResendActiveMail(string acct, string lang = null)
        {
            string msg = "urs/resend_active_mail";
            sDicArgs.Clear();
            sDicArgs.Add("acct", acct);
            sDicArgs.Add("gate", productGate);
            if(lang != null)
            {
                sDicArgs.Add("lang", lang);
                
            }
            return GetResp(msg, sDicArgs);
        }

        //判断帐号是否存在
        //acct:战盟账号 checkactive:是否验证账号是否已激活
        public  string IsExistAcct(string acct, string checkactive = null)
        {
            string msg = "urs/isexist_acct";
            sDicArgs.Clear();
            sDicArgs.Add("acct", acct);
            if(checkactive != null)
            {
                sDicArgs.Add("checkactive", checkactive);
            }
            return GetResp(msg, sDicArgs);
        }

        //获取设备唯一ID
        //mac:(小于iOS7.0上传) 	idfv:UUID：idfv (大于等于iOS6.0上传) appid:bundleIdentifier mode:手机型号
        public  string GetIdentifier(string mac=null, string idfv = null, string appid = null, string mode = null)
        {
            string msg = "mpf/ios_getidentifier";
            sDicArgs.Clear();
            if(mac != null)
            {
                sDicArgs.Add("mac", mac);
            }
            if (idfv != null)
            {
                sDicArgs.Add("idfv", idfv);
            }
            if (appid != null)
            {
                sDicArgs.Add("appid", appid);
            }
            if (mode != null)
            {
                sDicArgs.Add("mode", mode);
            }

            return GetResp(msg, sDicArgs);
        }

        //获取游戏公告
        //filename:文件名，如m2sw_ch
        public  string GetCardbord(string filename)
        {
            string msg = "urs/getcardbord";
            string clientip = Network.player.ipAddress;
            sDicArgs.Clear();
            sDicArgs.Add("filename", filename);
            return GetResp(msg, sDicArgs);
        }

        //用于获取短信验证码
        //参数 phone 电话号码
        public  string SendSmsCode(string phone)
        {
            string msg = "common/send_smscode";
            string clientip = Network.player.ipAddress; 
            Debug.Log(clientip);
            sDicArgs.Clear();
            sDicArgs.Add("phone", phone);
            sDicArgs.Add("gate", productGate);
            sDicArgs.Add("clientip", clientip);
            return GetResp(msg, sDicArgs);  
        }

        //验证短信校验码
        //smskey:获取短信验证码返回的KEY smscode:短信验证码
        public  string CheckSms(string smskey, string smscode)
        {
            string msg = "common/checksms";
            sDicArgs.Clear();
            sDicArgs.Add("smskey", smskey);
            sDicArgs.Add("smscode", smscode);
            return GetResp(msg, sDicArgs);
        }

        // 获取图片验证码，用于测试用
        public RetRegAcct GetImgcode()
        {
            string msg = "urs/get_imgcode";
            string strRet = GetResp(msg, null);
            RetRegAcct ret = null;
            if (!string.IsNullOrEmpty(strRet))
            {
                ret = JsonUtility.FromJson<RetRegAcct>(strRet);
            }
            return ret;
        }


        //登录游戏
        //account:	多益通帐号  password:多益通密码 mymd5(明文密码)  passmd5:多益通密码 md5(明文密码)  server :服务器编号  ip:客户端ip  mac:客户端mac地址  
        public  string CheckUser(string account, string password, string passmd5, string server, string ip, string mac)
        {
            string msg = "xiyi/R/check_user.php";
            sDicArgs.Clear();
            sDicArgs.Add("gate", productGate);
            sDicArgs.Add("account", account);
            sDicArgs.Add("password", password);
            sDicArgs.Add("passmd5", passmd5);
            sDicArgs.Add("ip", ip);
            sDicArgs.Add("mac", mac);
            return GetResp(msg, sDicArgs);
        }

        //使用绑定帐号恢复设备帐号(设备换战盟帐号或第一次用战盟帐号登录用)
        //ip:客户端ip地址 number:当前登录通行证id，未登录时传-1 email:绑定帐号 password:绑定帐号密码 passmd5:绑定帐号密码md5形式 
        //server;当前登录或要登录的服务器id userid:当前登录角色id，未登录时传-1 bindtype:绑定类型gamecenter,email,dyt战盟帐号传dyt
        public  string EmailGetAcctInfo(string ip, string number, string email, string password, string passmd5, string server, string userid, string bindtype)
        {
            string msg = "urs/R/email_getacctinfo.php";
            sDicArgs.Clear();
            sDicArgs.Add("ip", ip);
            sDicArgs.Add("gate", productGate);
            sDicArgs.Add("number", number);
            sDicArgs.Add("email", email);
            sDicArgs.Add("password", password);
            sDicArgs.Add("passmd5", passmd5);
            sDicArgs.Add("server", server);
            sDicArgs.Add("userid", userid);
            sDicArgs.Add("bindtype", bindtype);
            return GetResp(msg, sDicArgs);
        }

        //新设备使用战盟帐号登录并绑定设备号
        //部分参数见上条 localaccount:手游设备帐号，@mobile localpass:设备本地密码 ismodelocalpass:手游－是否启用本游戏密码方式 1＝是，0＝否
        public  string EmailWGetAcctInfo(string ip, string number, string email, string password, string passmd5, 
            string server, string userid, string bindtype, string localaccount =null, string localpass = null, string ismodelocalpass = null)
        {
            string msg = "urs/W/email_w_getacctinfo.php";
            sDicArgs.Clear();
            sDicArgs.Add("ip", ip);
            sDicArgs.Add("gate", productGate);
            sDicArgs.Add("number", number);
            sDicArgs.Add("email", email);
            sDicArgs.Add("password", password);
            sDicArgs.Add("passmd5", passmd5);
            sDicArgs.Add("server", server);
            sDicArgs.Add("userid", userid);
            sDicArgs.Add("bindtype", bindtype);
            if(localaccount != null)
            {
                sDicArgs.Add("localaccount", localaccount);
            }
            if (localpass != null)
            {
                sDicArgs.Add("localpass", localpass);
            }
            if (ismodelocalpass != null)
            {
                sDicArgs.Add("ismodelocalpass", ismodelocalpass);
            }
            return GetResp(msg, sDicArgs);
        }

        //游戏内使用战盟帐号登录检测该战盟帐号是否被绑定
        //ip:获取短信验证码返回的KEY email:绑定帐号 bindtype:绑定类型(gamecenter/email/dyt)
        public  string EmailGetBindType(string ip, string email, string bindtype)
        {
            string msg = "urs/R/email_getbindtype.php";
            sDicArgs.Clear();
            sDicArgs.Add("ip", ip);
            sDicArgs.Add("gate", productGate);
            sDicArgs.Add("email", email);
            sDicArgs.Add("bindtype", bindtype);
            return GetResp(msg, sDicArgs);
        }

        //普通战盟帐号绑定手机
        //number战盟ID account:战盟帐号,与战盟ID至少传一个，优先传递number value:绑定的值，目前只有手机号 vtype:绑定类型，目前只有m,表示手机
        //act:(change,update,cancel)不传是绑定手机，change是手机号换绑到新账号(原帐号解绑)，update是更新绑定手机,cancel是删除绑定手机
        //country:国家英文简写，如CN mac:大数据埋点-mac地址 network_env:大数据埋点--网络环境 device:大数据埋点--设备平台
        public  string AcctValidInfo(string number, string account, string value, string vtype, string act = null, 
            string country = null, string mac = null, string network_env = null, string device = null)
        {
            string msg = "urs/W/acct_validinfo.php";
            sDicArgs.Clear();
            sDicArgs.Add("number", number);
            sDicArgs.Add("account", account);
            sDicArgs.Add("value", value);
            sDicArgs.Add("vtype", vtype);
            if (act != null)
            {
                sDicArgs.Add("act", act);
            }
            if (country != null)
            {
                sDicArgs.Add("country", country);
            }
            if (mac != null)
            {
                sDicArgs.Add("mac", mac);
            }
            if (network_env != null)
            {
                sDicArgs.Add("network_env", network_env);
            }
            if (device != null)
            {
                sDicArgs.Add("device", device);
            }
            return GetResp(msg, sDicArgs);
        }

        //设置关联手机
        //phone:手机号 acct:战盟帐号
        public  string SetAssociationPhone(string phone, string acct)
        {
            string msg = "urs/set_association_phone";
            sDicArgs.Clear();
            sDicArgs.Add("phone", phone);
            sDicArgs.Add("acct", acct);
            return GetResp(msg, sDicArgs);
        }

        //取消关联手机
        //email_id:取消关联手机邮件中的链接，包含的参数：aid的值
        public  string UnsetAssociationPhone(string email_id)
        {
            string msg = "urs/unset_association_phone";
            sDicArgs.Clear();
            sDicArgs.Add("email_id", email_id);
            return GetResp(msg, sDicArgs);
        }

        //游客身份登录后设备号绑定战盟账号
        //ip:客户端ip地址 number:通行证ID email:要绑定的战盟账号 password:16位 绑定的密码，通行证加密方式 server:服务器ID 
        //userid:角色ID(无角色游戏传1) bindtype:email,gamecenter,dyt,facebook,2980,phone,
        //birthday 绑定类型 nick:hex编码后的值 绑定帐号昵称 passmd5:md5值
        public  string EmailAcctBind(string ip, string number, string email, string password, 
            string server, string userid, string bindtype, string nick, string passmd5)
        {
            string msg = "email_acct_bind.php";
            sDicArgs.Clear();
            sDicArgs.Add("ip", ip);
            sDicArgs.Add("gate", productGate);
            sDicArgs.Add("number", number);
            sDicArgs.Add("email", email);
            sDicArgs.Add("password", password);
            sDicArgs.Add("server", server);
            sDicArgs.Add("userid", userid);
            sDicArgs.Add("bindtype", bindtype);
            sDicArgs.Add("gate", productGate);
            sDicArgs.Add("gate", productGate);
            sDicArgs.Add("nick", nick);
            sDicArgs.Add("passmd5", passmd5);
            return GetResp(msg, sDicArgs);
        }

        //手游，帐号绑定邮箱帐号,点击邮箱连接完成绑定
        //guid:绑定邮件链接的参数guid
        public  string MobileBindMail(string guid)
        {
            string msg = "urs/mobile_bindmail";
            sDicArgs.Clear();
            sDicArgs.Add("guid", guid);
            return GetResp(msg, sDicArgs);
        }

        //设备帐号和战盟绑定，发送邮件
        //smskey:获取短信验证码返回的KEY smscode:短信验证码
        public  string Mode1(string number, string userid, string server, string ip,
            string email, string password, string passmd5, string bindtype)
        {
            string msg = "urs/mobile_sendbindmai";
            sDicArgs.Clear();
            sDicArgs.Add("gate", productGate);
            sDicArgs.Add("number", number);
            sDicArgs.Add("userid", userid);
            sDicArgs.Add("server", server);
            sDicArgs.Add("ip", ip);
            sDicArgs.Add("email", email);
            sDicArgs.Add("password", password);
            sDicArgs.Add("passmd5", passmd5);
            sDicArgs.Add("bindtype", bindtype);
            return GetResp(msg, sDicArgs);
        }

        //获取绑定/关联帐号信息
        //
        public  string AcctGetBindeMail(string ip, string number, string server, string userid, string account, string bindtype)
        {
            string msg = "acct_getbindemail.php";
            sDicArgs.Clear();
            sDicArgs.Add("ip", ip);
            sDicArgs.Add("gate", productGate);
            sDicArgs.Add("number", number);
            sDicArgs.Add("server", server);
            sDicArgs.Add("userid", userid);
            sDicArgs.Add("account", account);
            sDicArgs.Add("bindtype", bindtype);
            return GetResp(msg, sDicArgs);
        }

        //检查密码
        //
        public  string CheckPass(string acct, string pass, string pstype, string gatesrc, string vregval, string imgcode, string encrypt=null, string adsid=null)
        {
            string msg = "checkpass";
            sDicArgs.Clear();
            sDicArgs.Add("acct", acct);
            sDicArgs.Add("pass", pass);
            sDicArgs.Add("pstype", pstype);
            sDicArgs.Add("gatesrc", gatesrc);
            sDicArgs.Add("vregval", vregval);
            sDicArgs.Add("imgcode", imgcode);
            if(encrypt != null)
            {
                sDicArgs.Add("encrypt", encrypt);
            }
            if (adsid != null)
            {
                sDicArgs.Add("adsid", adsid);
            }
            return GetResp(msg, sDicArgs);
        }

        //检查帐号密码（加密后的密码）
        //
        public  string CheckSecretPass(string acct, string secretpass, string pstype, string gatesrc, string vregval, string imgcode, string encrypt = null, string adsid = null)
        {
            string msg = "checksecretpass";
            sDicArgs.Clear();
            sDicArgs.Add("acct", acct);
            sDicArgs.Add("secretpass", secretpass);
            sDicArgs.Add("pstype", pstype);
            sDicArgs.Add("gatesrc", gatesrc);
            sDicArgs.Add("vregval", vregval);
            sDicArgs.Add("imgcode", imgcode);
            if (encrypt != null)
            {
                sDicArgs.Add("encrypt", encrypt);
            }
            if (adsid != null)
            {
                sDicArgs.Add("adsid", adsid);
            }
            return GetResp(msg, sDicArgs);
        }


       


        public  string AccUrl = "http://10.17.65.82:20801/";
        public  string GetResp(string strType, Dictionary<string, string> parameters)
        {
            string url = AccUrl + strType;
            return GetHttpResponse(url, parameters);

        }

    }
}
