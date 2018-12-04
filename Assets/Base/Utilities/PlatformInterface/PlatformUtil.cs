using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using GameUtil;
using UnityEngine.Networking;

namespace GamePlatform
{

    public class PlatformUtil
    {
        //转换为格里威治时间 即相对于1970年1月1日所经历的秒数
        public static int TimeToGreenwich(DateTime dtIn)
        {
            DateTime dtGc = new DateTime(1970, 1, 1);
            dtGc = TimeZone.CurrentTimeZone.ToLocalTime(dtGc);
            return (int)(dtIn - dtGc).TotalSeconds;
        }

        //格里威治时间转换为Datetime
        public static DateTime TimeFromGreenwich(int seconds)
        {
            DateTime dtGc = new DateTime(1970, 1, 1);
            dtGc = dtGc.AddSeconds(seconds);
            dtGc = TimeZone.CurrentTimeZone.ToLocalTime(dtGc);
            return dtGc;
        }

        //获取md5值
        public static string GetMd5Hash(string input)
        {
            if (input == null)
            {
                return null;
            }
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public static string GetUrlParame(Dictionary<string, string> parameters)
        {
            StringBuilder buffer = new StringBuilder();
            int i = 0;
            foreach (string key in parameters.Keys)
            {
                if (i > 0)
                {
                    buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                }
                else
                {
                    buffer.AppendFormat("{0}={1}", key, parameters[key]);
                }
                i++;
            }
            return buffer.ToString();
        }

        public static string Byte2Hex(byte[] values)
        {
            StringBuilder ret = new StringBuilder();
            foreach (byte b in values)
            {
                ret.AppendFormat("{0:x2}", b);//要小写就 用 x
            }
            return ret.ToString();
        }

        public static byte[] Hex2Byte(string strHex)
        {
            strHex = strHex.Replace("   ", " ");
            byte[] buffer = new byte[strHex.Length / 2];
            for (int i = 0; i < strHex.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(strHex.Substring(i, 2), 16);
            return buffer;
        }

        // 将utf8字符转化为16进制字符
        public static string Str2Hex(string strIn)
        {
            byte[] bRet = Encoding.UTF8.GetBytes(strIn);
            return Byte2Hex(bRet);
        }


        public static string GetHttpResponse(string url, int timeOut = 2000)
        {

            string ret = "";
            try
            {
                using (HttpWebResponse response = HttpUtil.CreateGetHttpResponse(url, timeOut, null, null))
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(stream))
                        {
                            ret = sr.ReadToEnd();
                            sr.Close();
                        }
                        stream.Flush();
                        stream.Close();
                    }
                    response.Close();
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning(ex.ToString());
            }
            return ret;
        }

        public static IEnumerator GetPostHttpResponse(string url, string postData, int timeOut = 2000)
        {
            using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
            {
                byte[] postBytes = Encoding.UTF8.GetBytes(postData);
                www.uploadHandler = new UploadHandlerRaw(postBytes);
                www.downloadHandler = new DownloadHandlerBuffer();
                www.timeout = timeOut;
                www.SetRequestHeader("Content-Type", "application/json");
                yield return www.SendWebRequest();
                if (www.isNetworkError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    // Show results as text    
                    if (www.responseCode == 200)
                    {
                        Debug.Log(www.downloadHandler.text);
                    }
                }
            }
        }
    }


    public class DES
    {
        public static string Encrypt(string toEncry, string sKey)
        {
            if (toEncry == null || sKey == null) return null;
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Mode = System.Security.Cryptography.CipherMode.ECB;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(toEncry);
            des.Key = Encoding.UTF8.GetBytes(sKey);
            des.IV = Encoding.UTF8.GetBytes(sKey);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return PlatformUtil.Byte2Hex(ms.ToArray());
        }
        //解密
        public static string Decrypt(string data, string sKey)
        {
            return Decrypt(PlatformUtil.Hex2Byte(data), sKey);
        }
        public static string Decrypt(byte[] data, string sKey)
        {
            if (data == null || sKey == null) return null;
            DESCryptoServiceProvider MyServiceProvider = new DESCryptoServiceProvider();
            MyServiceProvider.Padding = PaddingMode.PKCS7;
            MyServiceProvider.Mode = CipherMode.ECB;
            byte[] key = Encoding.UTF8.GetBytes(sKey);
            ICryptoTransform MyTransform =
                MyServiceProvider.CreateDecryptor(key, null);
            MemoryStream ms = new MemoryStream();
            ms.Write(data, 0, data.Length);
            ms.Seek(0, SeekOrigin.Begin);
            ms.Position = 0;
            //CryptoStream对象的作用是将数据流连接到加密转换的流
            CryptoStream MyCryptoStream = new CryptoStream(ms, MyTransform, CryptoStreamMode.Read);
            byte[] YourInputStorage = new byte[data.Length];
            //将字节数组中的数据写入到加密流中
            int len = MyCryptoStream.Read(YourInputStorage, 0, YourInputStorage.Length);
            MyCryptoStream.Close();//关闭加密流对象
            return Encoding.UTF8.GetString(YourInputStorage, 0, len);
        }
    }
 }


