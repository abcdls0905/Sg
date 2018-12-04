using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using System.Net;
using GameUtil;


namespace GamePlatform
{
    //大数据统计，包括启动日志，周首登等信息
    public class BigDataStatistics : MonoSingleton<BigDataStatistics>
    {
        private string cryptKey = "duoyi888";

        // 大数据网址

        private string BigdataUrl
        {
            get
            {
                if (PlatformConfig.gameNet == GameNet.OUTER_GAME)
                {
                    return "http://chc-bd.duoyi.com/";
                }
                else if (PlatformConfig.gameNet == GameNet.OUTTER_TEST)
                {
                    return "http://219.132.195.38:39989/"; // 这个是备用的http://10.82.195.38:39989
                }
                else
                {
                    return "http://10.17.64.213:39989/"; // 内网提供的地址只有广州能用，成都不能用
                }

            }
        }

        // 一次发送一条
        public void SendBigData(string body)
        {
            StartCoroutine(PlatformUtil.GetPostHttpResponse(BigdataUrl, BuildHttpInfo(body)));
        }

        //一次发送多条
        public void SednBigData(List<string> bodys)
        {
            StartCoroutine(PlatformUtil.GetPostHttpResponse(BigdataUrl, BuildHttpInfo(bodys)));
        }

        //单条构建
        private string BuildHttpInfo(string body)
        {
            string ItemInfo = GemItemInfo(body);
            string msg = "[" + ItemInfo + "]";
            return msg;
        }

        //多条构建，组合发送减少通讯量

        private string BuildHttpInfo(List<string> bodys)
        {
            string msg = "[";
            for(int i = 0; i < bodys.Count; i++)
            {
                msg += GemItemInfo(bodys[i]);
                if(i< bodys.Count -1)
                {
                    msg += ",";
                }
            }
            msg += "]";
            return msg;
        }

        // 构建一条消息，统一采用加密方式构建
        private string GemItemInfo(string body)
        {
            string head = @"{""headers"":{ ""_enc"":1},""body"":""";
            string bodye = DES.Encrypt(body, cryptKey);
            string end = "\"}";
            return head + bodye + end;

        }

    }
}

       

