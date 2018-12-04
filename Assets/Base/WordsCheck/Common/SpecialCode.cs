using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordsCheck
{
    /// <summary>
    /// 用于特殊字符处理，特殊的非配表的字符都放这边
    /// </summary>
    class SpecialCode
    {
        private static string QYCode = "Qq扣ＱYy外歪丫";
        public static bool IsQYCode(char cIn)
        {
            return QYCode.Contains(cIn);
        }

        public static int GetQYCodeNum(string strIn)
        {
            int retNum = 0;
            int len = strIn.Length;
            for (int i = 0; i < len; i++)
            {
                if (IsQYCode(strIn[i]))
                {
                    retNum++;
                }
            }
            return retNum;
        }

        private static string NumCode = "0123456789ⅰⅱⅲⅳⅴⅵⅶⅷⅸⅹ⒈⒉⒊⒋⒌⒍⒎⒏⒐⒑⒒⒓⒔⒕⒖⒗⒘⒙⒚⒛⑴⑵⑶⑷⑸⑹⑺⑻⑼⑽⑾⑿⒀⒁⒂⒃⒄⒅⒆⒇①②③④⑤⑥⑦⑧⑨⑩㈠㈡㈢㈣㈤㈥㈦㈧㈨ⅠⅡⅢⅣⅤⅥⅦⅧⅨⅩⅪⅫ一二三四五六七八九零壹贰叁肆伍陆柒捌玖lIO〇|▁▂━ㅡ一－¯￣＿_-―—ˉ❶❷❸❹❺❻❼❽❾❿";
        public static bool IsNumCode(char cIn)
        {
            return NumCode.Contains(cIn);
        }

        private static int QyNumValue = 4;
        // 是否有qy字符，q或y后面连续四个数字算qy字符
        public static bool IsHaveQyNum(string strIn)
        {
            strIn.Replace(" ", "");
            int len = strIn.Length;
            for (int i = 0; i < len; i++)
            {
                if (IsQYCode(strIn[i]) && (len - i) > QyNumValue)
                {
                    int numCodeNum = 0;
                    for (int j = i + 1; j < len; j++)
                    {
                        if (IsNumCode(strIn[j]))
                        {
                            numCodeNum++;
                            if (numCodeNum >= QyNumValue)
                            {
                                return true;
                            }
                        }
                    }
                }

            }
            return false;
        }


        public static int GetNumCodeNum(string strIn)
        {
            int retNum = 0;
            int len = strIn.Length;
            for (int i = 0; i < len; i++)
            {
                if (IsNumCode(strIn[i]))
                {
                    retNum++;
                }
            }
            return retNum;
        }

        public const int SENTENCE_SHIELD_SERIALNUM_COUNT = 6; // 这个是包含了第一个数字的

        ///当2个数字间的字符数＜5，则视后面的这个数字为“连续的数字”，1个数字返回的是1
        public static int GetSerialNumCodeNum(string strIn)
        {
            int maxNum = 0;
            int len = strIn.Length;
            int tmpNum = 0;
            int breakNum = 0;
            for (int i = 0; i < len; i++)
            {
                if (IsNumCode(strIn[i]))
                {
                    tmpNum++;
                    breakNum = 0;
                }
                else
                {
                    if (tmpNum > 0)
                    {
                        breakNum++;
                        if (breakNum >= 5)
                        {
                            if (tmpNum > maxNum)
                            {
                                maxNum = tmpNum;
                            }
                            tmpNum = 0;
                        }
                    }
                }
            }
            if (tmpNum > maxNum)
            {
                maxNum = tmpNum;
            }
            return maxNum;
        }

        private static List<string> sListBadNum = new List<string>{"89", "1989", "64", "5173","8964"};//敏感数字
        public static List<string>  ListBadNum
        {
            get
            {
                return sListBadNum;
            }
        }

        public static bool IsEnCode(char cIn)
        {
            return (cIn >= 'A' && cIn <= 'Z') || (cIn >= 'a' && cIn <= 'z');
        }

        private static char CnBegin = (char)(Convert.ToUInt16("0x4E00", 16));// 汉字开始码
        private static char CnEnd = (char)(Convert.ToUInt16("0x9FBF", 16));// 汉字结束码
        ///是否是汉字判定
        public static bool IsCnCode(char cIn)
        {
            return cIn >= CnBegin && cIn <= CnEnd;
        }

        ///是否是阿拉伯数字判定
        public static bool IsArabicNum(char cIn)
        {
            return cIn >= '0' && cIn <= '9'; 
        }

        public static bool IsConnectCode(char cIn)
        {
            return cIn >= '-' && cIn <= '_';
        }

        public static bool IsSpace(char cIn)
        {
            return cIn >= ' ';
        }

        private static List<string> sListProtectCom = new List<string> { "swsy.duoyi.com", "duoyi.com", "2980.com" };//这些
        public static List<string> ListProtectCom
        {
            get
            {
                return sListProtectCom;
            }
        }

        // 前面ok
        public static bool IsProtectComOkBf(char cIn)
        {
            return cIn == '.' || cIn == '。' || cIn == '、' || IsCnCode(cIn);
        }

        //后面ok的字段
        public static bool IsProtectComOkAf(char cIn)
        {
            return cIn == '/' || cIn == '。' || cIn == '、' || IsCnCode(cIn);
        }

    }
}
