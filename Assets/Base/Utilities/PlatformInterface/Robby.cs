using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using System.IO;
using System.Security.Cryptography;

using GameUtil;


namespace GamePlatform
{
    //请求robby服务器的消息,根据ip和游戏代号返回。
    public class Robby : Singleton<Robby>
    {
        [SerializableAttribute]
        private struct NodeInfo
        {
            public string[] gs;
            public string[] gw;
        }

        [SerializableAttribute]
        private class RobbyInfo //用于解密的中间类
        {
            public bool Encrypt;
            public NodeInfo Nodes;
        }
        private bool NeedDecrypt(string strIn)//是否需要解密判定，encrypt 值为 true时才解密
        {
            strIn = strIn.Replace(" ","");//移除空格
            int idx = strIn.IndexOf("\"Encrypt\":true");
            return ( idx != -1);
        }

        //strIpAndPort 请求的ip和端口， productGate 游戏代号 decryptKey解密的密码，需要跟服务端约定。 这个三个参数各个项目都不公用
        public string GetRobbyInfo(string strIpAndPort = "192.168.112.72:16500", string productGate = "m2jh", string decryptKey = "123456")
        {
            if(string.IsNullOrEmpty(productGate))
            {
                productGate = PlatformConfig.productGate;
            }
            string strRequet = string.Format("http://{0}/lobbyreq?proj={1}", strIpAndPort, productGate);
            string strRet = PlatformUtil.GetHttpResponse(strRequet);
            if(NeedDecrypt(strRet))
            {
                // 解密时先转换成json结构体，然后对部分参数解密，再转换成字符串。
                RobbyInfo rb = JsonUtility.FromJson<RobbyInfo>(strRet);
                if(rb != null)
                {
                    for(int i = 0; i < rb.Nodes.gs.Length; i++)
                    {
                        rb.Nodes.gs[i] = Decrypt(rb.Nodes.gs[i], decryptKey);
                    }
                    for (int i = 0; i < rb.Nodes.gw.Length; i++)
                    {
                        rb.Nodes.gw[i] = Decrypt(rb.Nodes.gw[i], decryptKey);
                    }
                    strRet = JsonUtility.ToJson(rb);
                }
            }

            return strRet;


        }

        // 跟服务端匹配的解密算法
        static string Decrypt(string cipherText, string password)
        {
            const int blockSize = 16;
            byte[] message, iv;
            {
                var data = Convert.FromBase64String(cipherText);
                iv = new byte[blockSize];
                message = new byte[data.Length - blockSize];
                Array.Copy(data, 0, iv, 0, blockSize);
                Array.Copy(data, blockSize, message, 0, message.Length);
            }

            // AES-256-CFB with 128b block
            using (var aes = Rijndael.Create())
            {
                aes.KeySize = 256;
                aes.BlockSize = blockSize * 8;
                aes.FeedbackSize = blockSize * 8;
                aes.Mode = CipherMode.CFB;
                aes.Padding = PaddingMode.None;
                using (var sha256 = SHA256.Create())
                {
                    aes.Key = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                }
                aes.IV = iv;
                using (var dec = aes.CreateDecryptor())
                {
                    var buffer = new byte[blockSize];
                    var plain = new MemoryStream();
                    for (int p = 0; p < message.Length; p += blockSize)
                    {
                        int remaining = message.Length - p;
                        if (remaining <= blockSize)
                        {
                            Array.Copy(message, p, buffer, 0, remaining);
                            if (remaining < blockSize)
                            {
                                Array.Clear(buffer, remaining, blockSize - remaining);
                            }
                            plain.Write(dec.TransformFinalBlock(buffer, 0, blockSize), 0, remaining);
                        }
                        else
                        {
                            dec.TransformBlock(message, p, blockSize, buffer, 0);
                            plain.Write(buffer, 0, blockSize);
                        }
                    }
                    return Encoding.UTF8.GetString(plain.ToArray());
                }
            }
        }
    }

        
      
}
