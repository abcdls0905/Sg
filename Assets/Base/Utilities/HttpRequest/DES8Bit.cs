using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GameUtil
{
    /// <summary>
    /// DES的8位密钥的加密(.net core的DES不支持8位)
    /// </summary>
    public class DES8Bit
    {
        #region Bit
        private static int[] IP1 = { 58, 50, 42, 34, 26, 18, 10, 2,  60, 52, 44, 36, 28, 20, 12, 4,    //initial change
                            62, 54, 46, 38, 30, 22, 14, 6,  64, 56, 48, 40, 32, 24, 16, 8,
                            57, 49, 41, 33, 25, 17, 9,  1,  59, 51, 43, 35, 27, 19, 11, 3,
                            61, 53, 45, 37, 29, 21, 13, 5,  63, 55, 47, 39, 31, 23, 15, 7,
                            };
        private static int[] IP2 = { 40, 8, 48, 16, 56, 24, 64, 32, 39, 7, 47, 15, 55, 23, 63, 31,    //opp initial change
                            38, 6, 46, 14, 54, 22, 62, 30, 37, 5, 45, 13, 53, 21, 61, 29,
                            36, 4, 44, 12, 52, 20, 60, 28, 35, 3, 43, 11, 51, 19, 59, 27,
                            34, 2, 42, 10, 50, 18, 58, 26, 33, 1, 41, 9, 49, 17, 57, 25
                            };
        private static int[,,] s = {
                            {                                                                   //S-diagram array
	                            { 14,4,13,1,2,15,11,8,3,10,6,12,5,9,0,7 },
	                            { 0,15,7,4,14,2,13,1,10,6,12,11,9,5,3,8 },
	                            { 4,1,14,8,13,6,2,11,15,12,9,7,3,10,5,0 },
	                            { 15,12,8,2,4,9,1,7,5,11,3,14,10,0,6,13 }
	                        },
	                        {
		                        { 15,1,8,14,6,11,3,4,9,7,2,13,12,0,5,10 },
		                        { 3,13,4,7,15,2,8,14,12,0,1,10,6,9,11,5 },
		                        { 0,14,7,11,10,4,13,1,5,8,12,6,9,3,2,15 },
		                        { 13,8,10,1,3,15,4,2,11,6,7,12,0,5,14,9 }
	                        },
	                        {
		                        { 10,0,9,14,6,3,15,5,1,13,12,7,11,4,2,8 },
		                        { 13,7,0,9,3,4,6,10,2,8,5,14,12,11,15,1 },
		                        { 13,6,4,9,8,15,3,0,11,1,2,12,5,10,14,7 },
		                        { 1,10,13,0,6,9,8,7,4,15,14,3,11,5,2,12 }
	                        },
	                        {
		                        { 7,13,14,3,0,6,9,10,1,2,8,5,11,12,4,15 },
		                        { 13,8,11,5,6,15,0,3,4,7,2,12,1,10,14,9 },
		                        { 10,6,9,0,12,11,7,13,15,1,3,14,5,2,8,4 },
		                        { 3,15,0,6,10,1,13,8,9,4,5,11,12,7,2,14 }
	                        },
	                        {
		                        { 2,12,4,1,7,10,11,6,8,5,3,15,13,0,14,9 },
		                        { 14,11,2,12,4,7,13,1,5,0,15,10,3,9,8,6 },
		                        { 4,2,1,11,10,13,7,8,15,9,12,5,6,3,0,14 },
		                        { 11,8,12,7,1,14,2,13,6,15,0,9,10,4,5,3 }
	                        },
	                        {
		                        { 12,1,10,15,9,2,6,8,0,13,3,4,14,7,5,11 },
		                        { 10,15,4,2,7,12,9,5,6,1,13,14,0,11,3,8 },
		                        { 9,14,15,5,2,8,12,3,7,0,4,10,1,13,11,6 },
		                        { 4,3,2,12,9,5,15,10,11,14,1,7,6,0,8,13 }
	                        },
	                        {
		                        { 4,11,2,14,15,0,8,13,3,12,9,7,5,10,6,1 },
		                        { 13,0,11,7,4,9,1,10,14,3,5,12,2,15,8,6 },
		                        { 1,4,11,13,12,3,7,14,10,15,6,8,0,5,9,2 },
		                        { 6,11,13,8,1,4,10,7,9,5,0,15,14,2,3,12 }
	                        },
	                        {
		                        { 13,2,8,4,6,15,11,1,10,9,3,14,5,0,12,7 },
		                        { 1,15,13,8,10,3,7,4,12,5,6,11,0,14,9,2 },
		                        { 7,11,4,1,9,12,14,2,0,6,10,13,15,3,5,8 },
		                        { 2,1,14,7,4,10,8,13,15,12,9,0,3,5,6,11 }
	                        }
                        };
        private static int[] Ex = { 32,1,2,3,4,5,                                          //Expand array
                            4,5,6,7,8,9,
                            8,9,10,11,12,13,
                            12,13,14,15,16,17,
                            16,17,18,19,20,21,
                            20,21,22,23,24,25,
                            24,25,26,27,28,29,
                            28,29,30,31,32,1
                            };
        private static int[] P = { 16,7,20,21,                                                     //P-change
                            29,12,28,17,
                            1,15,23,26,
                            5,18,31,10,
                            2,8,24,14,
                            32,27,3,9,
                            19,13,30,6,
                            22,11,4,25
                            };
        private static int[] PC1 = { 57,49,41,33,25,17,9,                                  //PC-1 in keyBuild
                            1,58,50,42,34,26,18,
                            10,2,59,51,43,35,27,
                            19,11,3,60,52,44,36,
                            63,55,47,39,31,33,15,
                            7,62,54,46,38,30,22,
                            14,6,61,53,45,37,29,
                            21,13,5,28,20,12,4
                            };
        private static int[] PC2 = { 14,17,11,24,1,5,                                      //PC-2 in keyBuild
                            3,28,15,6,21,10,
                            23,19,12,4,26,8,
                            16,7,27,20,13,2,
                            41,52,31,37,47,55,
                            30,40,51,45,33,48,
                            44,49,39,56,34,53,
                            46,42,50,36,29,32
                            };

        private static int[,] key = new int[16,48];
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="keychar"></param>
        public static void Encode(ref int[] str, int[] keychar)   //encode: input 8 chars,8 keychars
        {
            int[] lData = new int[32];
            int[] rData = new int[32];
            int[] temp = new int[32];
            int i, j;
            keyBuild(keychar);

            EncodeData(ref lData, ref rData,ref str);
            for (i = 0; i < 16; i++)
            {
                for (j = 0; j < 32; j++)
                    temp[j] = rData[j];
                F(ref rData, ref key,i);
                for (j = 0; j < 32; j++)
                {
                    rData[j] = rData[j] ^ lData[j];
                }
                for (j = 0; j < 32; j++)
                    lData[j] = temp[j];
            }

            DecodeData(ref str,ref rData,ref lData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keychar"></param>
        public static void keyBuild(int[] keychar)//create key array
        {            
            int i;
            int[] movebit = { 1,1,2,2,2,2,2,2, 1, 2, 2, 2, 2, 2, 2, 1 };

            int[] midkey2 = new int[56];
            int[] midkey = new int[64];
            StrtoBin(ref midkey, keychar);
            for (i = 0; i < 56; i++)
                midkey2[i] = midkey[PC1[i] - 1];
            for (i = 0; i < 16; i++)
                keyCreate(ref midkey2, movebit[i], i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lData"></param>
        /// <param name="rData"></param>
        /// <param name="str"></param>
        public static void EncodeData(ref int[] lData,ref  int[] rData,ref int[] str)//encodedata function
        {   
            int i, j, lint, rint;//int h;
            int[] temp = new int[8];
            int[] data = new int[64];
            lint = 0;
            rint = 0;
            for (i = 0; i < 4; i++)
            {
                j = 0;
                while (str[i] != 0)
                {
                    temp[j] = str[i] % 2;
                    str[i] = str[i] / 2;
                    j++;
                }
                while (j < 8) temp[j++] = 0;
                for (j = 0; j < 8; j++)
                    lData[lint++] = temp[7 - j];
                j = 0;
                while (str[i + 4] != 0)
                {
                    temp[j] = str[i + 4] % 2;
                    str[i + 4] = str[i + 4] / 2;
                    j++;
                }
                while (j < 8) temp[j++] = 0;
                for (j = 0; j < 8; j++) rData[rint++] = temp[7 - j];
            }
            for (i = 0; i < 32; i++)
            {
                data[i] = lData[i];
                data[i + 32] = rData[i];
            }
            for (i = 0; i < 32; i++)
            {
                lData[i] = data[IP1[i] - 1];//printf("P1:%5d:%5d,%5d\n",IP1[i],lData[i],data[IP1[i]-1]);
                rData[i] = data[IP1[i + 32] - 1];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="lData"></param>
        /// <param name="rData"></param>
        private static void DecodeData(ref int[] str,ref int[] lData,ref int[] rData)
        {    //DecodeData from binary
            int i; int a, b;
            int[] data = new int[64];
            a = 0;
            b = 0;
            for (i = 0; i < 32; i++)
            {
                data[i] = lData[i];
                data[i + 32] = rData[i];
            }
            for (i = 0; i < 32; i++)
            {
                lData[i] = data[IP2[i] - 1];
                rData[i] = data[IP2[i + 32] - 1];
            }
            for (i = 0; i < 32; i++)
            {
                a = (lData[i] & 0x1) + (a << 1);
                b = (rData[i] & 0x1) + (b << 1);
                if ((i + 1) % 8 == 0)
                {
                    str[i / 8] = a; a = 0;//printf("%d",i/8);
                    str[i / 8 + 4] = b; b = 0;//printf("%d",i/8+4);
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rData"></param>
        /// <param name="key"></param>
        /// <param name="n"></param>
        public static void F(ref int[] rData, ref int[,] key,int n)//F function
        {
            int i;
            int[] rDataP = new int[48];
            Expand(ref rData,ref rDataP);
            for (i = 0; i < 48; i++)
            {
                rDataP[i] = rDataP[i] ^ key[n,i];// printf("%10d",rDataP[i]);if((i+1)%6==0)printf("\n");
            }
            ExchangeS(ref rDataP, ref rData);
            ExchangeP(ref rData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="midkey"></param>
        /// <param name="keychar"></param>
        public static void StrtoBin(ref int[] midkey, int[] keychar)//change into binary
        {     
            int i, j, k, n;
            int[] trans = new int[8];
            n = 0;
            for (i = 0; i < 8; i++)
            {
                j = 0;
                while (keychar[i] != 0)
                {
                    trans[j] = keychar[i] % 2;
                    keychar[i] = keychar[i] / 2;
                    j++;
                }
                for (k = j; k < 8; k++) trans[k] = 0;
                for (k = 0; k < 8; k++)
                    midkey[n++] = trans[7 - k];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="midkey2"></param>
        /// <param name="movebit"></param>
        /// <param name="n"></param>
        public static void keyCreate(ref int[] midkey2, int movebit, int n)
        {
            int i;
            int[]temp = new int[4];
            temp[0] = midkey2[0];
            temp[1] = midkey2[1];
            temp[2] = midkey2[28];
            temp[3] = midkey2[29];
            if (movebit == 2)
            {
                for (i = 0; i < 26; i++)
                {
                    midkey2[i] = midkey2[i + 2];
                    midkey2[i + 28] = midkey2[i + 30];
                }
                midkey2[26] = temp[0]; midkey2[27] = temp[1];
                midkey2[54] = temp[2]; midkey2[55] = temp[3];
            }
            else
            {
                for (i = 0; i < 27; i++)
                {
                    midkey2[i] = midkey2[i + 1];
                    midkey2[i + 28] = midkey2[i + 29];
                }
                midkey2[27] = temp[0]; midkey2[55] = temp[2];
            }
            for (i = 0; i < 48; i++)
                key[n,i] = midkey2[PC2[i] - 1];
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="rData"></param>
        /// <param name="rDataP"></param>
        public static void Expand(ref int[] rData, ref int[] rDataP)//Expand function
        {          
            int i;
            for (i = 0; i < 48; i++)
                rDataP[i] = rData[Ex[i] - 1];
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="rDataP"></param>
        /// <param name="rData"></param>
        public static void ExchangeS(ref int[] rDataP,ref int[] rData)
        {          //S-diagram change
            int i, n, linex, liney;
            linex = liney = 0;
            for (i = 0; i < 48; i += 6)
            {
                n = i / 6; //printf("%10d\n",(rDataP[i]<<1));
                linex = (rDataP[i] << 1) + rDataP[i + 5];
                liney = (rDataP[i + 1] << 3) + (rDataP[i + 2] << 2) + (rDataP[i + 3] << 1) + rDataP[i + 4];
                FillBin(ref rData, n, s[n,linex,liney]);
            }
        }

        /// <summary>
        ///   
        /// </summary>
        /// <param name="rData"></param>
        public static void ExchangeP(ref int[] rData)//P change
        {
            int i;
            int[] temp = new int[32];
            for (i = 0; i < 32; i++)
                temp[i] = rData[i];

            for (i = 0; i < 32; i++)
                rData[i] = temp[P[i] - 1];
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="rData"></param>
        /// <param name="n"></param>
        /// <param name="s"></param>
        public static void FillBin(ref int[] rData, int n, int s)
        {         // data to binary;call by S-Diagram change function
            int[] temp = new int[4];
            int i;
            for (i = 0; i < 4; i++)
            {
                temp[i] = s % 2;
                s = s / 2;
            }
            for (i = 0; i < 4; i++)
                rData[n * 4 + i] = temp[3 - i];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="keychar"></param>
        public static void Decode(int[] str, int[] keychar)
        {           //decode :input 8 chars,8 keychars
            int[] lData = new int[32];
            int[] rData = new int[32];
            int[] temp = new int[32];
            int i, j;
            keyBuild(keychar);
            EncodeData(ref lData,ref  rData,ref  str); //这个位置
            for (i = 0; i < 16; i++)
            {
                for (j = 0; j < 32; j++)
                    temp[j] = rData[j];
                F(ref rData,ref key,15-i);
                for (j = 0; j < 32; j++)
                {
                    rData[j] = rData[j] ^ lData[j];
                }

                for (j = 0; j < 32; j++)
                {
                    lData[j] = temp[j];
                }
            }
            DecodeData(ref str,ref  rData,ref lData);
        }

        //以下的函数为自己添加的

        /// <summary>
        /// 转为16进制
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string Byte2Hex(int[] values)
        {
            StringBuilder ret = new StringBuilder();
            foreach (int b in values)
            {
                ret.AppendFormat("{0:X2}", b);//要小写就 用 x
            }
            return ret.ToString();
        }

        /// <summary>
        /// 加密（不支持加密汉字）
        /// </summary>
        /// <param name="plaintxt">明文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string  DESEncrypt(string plaintxt,string key)
        {
            int remainder = plaintxt.Length % 8; // 根据明文长度填充不同字符
            switch (remainder)
            {
                case 0:
                    plaintxt += fillCharacter(8,'\b');
                    break;
                case 1:
                    plaintxt += fillCharacter(7, '\a');
                    break;
                case 2:
                    plaintxt += fillCharacter(6, '\x6');
                    break;
                case 3:
                    plaintxt += fillCharacter(5, '\x5');
                    break;
                case 4:
                    plaintxt += fillCharacter(4, '\x4');
                    break;
                case 5:
                    plaintxt += fillCharacter(3, '\x3');
                    break;
                case 6:
                    plaintxt += fillCharacter(2, '\x2');
                    break;
                case 7:
                    plaintxt += fillCharacter(1, '\x1');
                    break;

            }
            StringBuilder ciphertext = new StringBuilder();
            int[] key2 = new int[8];
            for (int i = 0; i < 8; i++)
                key2[i] = key[i];

            for (int count = 0; count < plaintxt.Length / 8; count++)
            {
                int[] plain = new int[8];
                for (int i = 0; i < 8; i++)
                {
                    plain[i] = plaintxt[count * 8 + i];
                }
                Encode(ref plain, key2);//一次只能加密8个字符

                for (int i = 0; i < 8; i++)
                    key2[i] = key[i];
                ciphertext.Append(Byte2Hex(plain));
            }
            return ciphertext.ToString();
        }

        /// <summary>
        ///  填充字符
        /// </summary>
        /// <param name="count"></param>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static string fillCharacter(int count,char ch)
        {
            StringBuilder sb = new StringBuilder();
            for (int i=0; i<count; i++)
            {
                sb.Append(ch);
            }
            return sb.ToString();
        }

        /// <summary>
        ///  解密
        /// </summary>
        /// <param name="ciphertext">密文</param>
        /// <param name="sKey">密钥</param>
        /// <returns></returns>
        public static string DESDecryptt(string ciphertext, string sKey)
        {
            byte[] data = Hex2Byte(ciphertext);
            if (data == null || string.IsNullOrEmpty(sKey))
            {
                return "";
            }
            byte[] key = Encoding.UTF8.GetBytes(sKey);
            List<int> dataList = new List<int>();
            int[] keyArray = new int[key.Length];

            
            for (int i = 0; i < data.Length; i++)
            {
                dataList.Add(data[i]);
            }
            for (int i=0; i< key.Length; i++)
            {
                keyArray[i] = key[i];
            }
            
            int[] dataArray = new int[8];
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dataList.Count / 8; i++)
            {
                for (int c = 0; c < 8; c++)
                {
                    dataArray[c] = dataList[c + i * 8];
                }
                Decode(dataArray, keyArray); // 一次只能解码8个字符
                for (int k = 0; k < key.Length; k++)
                {
                    keyArray[k] = key[k];//恢复key
                }
                for (int j = 0; j < dataArray.Length; j++)
                {
                    if (dataArray[j] > 8 || dataArray[j] == 0)//填充的字符\X1 -- \X6， \a, \b ascii码为 1 -- 8
                        sb.Append((char)dataArray[j]);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        ///  将HEX串还原到数组
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] Hex2Byte(string s)
        {
            if (s == null || s.Length % 2 != 0)
                return null;
            byte[] ba = new byte[(s.Length + 1) / 2];
            for (int i = 0; i < ba.Length; i++)
            {
                ba[i] = byte.Parse(s.Substring(2 * i, 2), NumberStyles.HexNumber);
            }
            return ba;
        }
    }
}
