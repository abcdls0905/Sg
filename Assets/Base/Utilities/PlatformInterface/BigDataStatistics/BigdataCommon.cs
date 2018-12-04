using GameUtil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using UnityEngine;

namespace GamePlatform
{
    class BigdataCommon : Singleton<BigdataCommon>
    {
        IBigDataBuilder builder;

        public override void Init()
        {
            base.Init();
            SetBuilder(new BigDataBuilder());
        }

        public void SetBuilder(IBigDataBuilder bd)
        {
            builder = bd;
        }
        //启动日志
        public void StartGameLog(string logic_service_id, string title, string version, string uid, string role_id, string clientID
            , string login_channel, string stats_col)
        {
            builder.Clear();
            string ftime = BigDataBase.GetCurTime();
            string logname = "a_start_" + PlatformConfig.productGate + "_1.0";
            string plateform = SystemInfo.operatingSystem;
            string ip = BigDataBase.GetIP();
            string devices = BigDataBase.GetDeviceType();
            string network_env = BigDataBase.GetNetType();
            builder.PushData(ftime);
            builder.PushData(logic_service_id);
            builder.PushData(logname);
            builder.PushData(title);
            builder.PushData(version);
            builder.PushData(uid);
            builder.PushData(role_id);
            builder.PushData(clientID);
            builder.PushData(plateform);
            builder.PushData(ip);
            builder.PushData(devices);
            builder.PushData(network_env);
            builder.PushData(login_channel);
            builder.PushData(stats_col);
            BigDataStatistics.CreateInstance().SendBigData(builder.String());
        }

        //是否是周首判定,并刷新
        private bool IsWeekFirst()
        {
            string key = "WeekFirstLoginTime"; //上次促发时间
            if (PlayerPrefs.HasKey(key))
            {
                int last = PlayerPrefs.GetInt(key);
                DateTime lastDt = PlatformUtil.TimeFromGreenwich(last);
                TimeSpan ts = DateTime.Now - lastDt;
                if(ts.Days >= 6)//相隔超过6天
                {
                    PlayerPrefs.SetInt(key, PlatformUtil.TimeToGreenwich(DateTime.Now));
                    return true;
                }
                return false;
            }
            else//没有标记过视为首次登录
            {
                PlayerPrefs.SetInt(key, PlatformUtil.TimeToGreenwich(DateTime.Now));
                return true;
            }
        }

        //周首登日志
        public void WeekFirstLoginLog(string logic_service_id, string title, string version, string uid, string role_id, string clientID
            , string login_channel)
        {
            if(!IsWeekFirst())
            {
                return;
            }
            builder.Clear();
            string ftime = BigDataBase.GetCurTime();
            string logname = "a_applist_" + PlatformConfig.productGate + "_1.0";
            string plateform = SystemInfo.operatingSystem;
            string ip = BigDataBase.GetIP();
            string devices = BigDataBase.GetDeviceType();
            string network_env = BigDataBase.GetNetType();
            string stats_col = "mac:" + BigDataBase.GetMAC() + ",idfa:" + BigDataBase.GetIdentifierForAdvertising() + 
                ",imei：" + BigDataBase.GetImei() + ",app:" + BigDataBase.GetAppsTimeInfo();
            builder.PushData(ftime);
            builder.PushData(logic_service_id);
            builder.PushData(logname);
            builder.PushData(title);
            builder.PushData(version);
            builder.PushData(uid);
            builder.PushData(role_id);
            builder.PushData(clientID);
            builder.PushData(plateform);
            builder.PushData(ip);
            builder.PushData(devices);
            builder.PushData(network_env);
            builder.PushData(plateform);
            builder.PushData(login_channel);
            builder.PushData(stats_col);
            BigDataStatistics.CreateInstance().SendBigData(builder.String());
        }
    }
}
