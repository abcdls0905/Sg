using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace WordsCheck
{
    ///16位数字区间
    public struct RangeU16
    {
        public UInt16 Begin;
        public UInt16 End;
    }

    class CheckComon
    {
        private static List<string> sListTmp = new List<string>();
        public static bool IsInRange(int iIn, List<RangeU16> listRange)
        {
            if (listRange != null && listRange.Count > 0)
            {
                for (int i = 0; i < listRange.Count; i++)
                {
                    if (iIn >= listRange[i].Begin && iIn <= listRange[i].End)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsContainsRange(int begin, int end, List<RangeU16> listRange)
        {
            if (listRange != null && listRange.Count > 0)
            {
                for (int i = 0; i < listRange.Count; i++)
                {
                    if (begin <= listRange[i].Begin && end >= listRange[i].End)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static string[] LoadTextLines(string strName)
        {
            string strRet = TxtOpr.Load(strName);
            if (!string.IsNullOrEmpty(strRet))
            {
                return strRet.Split('\n');
            }
            return null;
        }

        ///针对单行文件的加载方式
        public static List<string> GetStringListFromFile(string strName, List<string> listIn, ref string fVersion)
        {
            if (listIn == null)
            {
                listIn = new List<string>();
            }
            listIn.Clear();
            string[] AllLines = LoadTextLines(strName);
            if (AllLines == null)
            {
                return listIn;
            }
            int len = AllLines.Length;
            for (int i = 0; i < len; i++)
            {
                string strLine = AllLines[i].Replace("\r", "");
                if (i == 0)
                {
                    fVersion = strLine;
                }
                else
                {
                    if (!strLine.Equals(string.Empty))
                        listIn.Add(strLine);
                }
            }
            return listIn;
        }

        public static void RemoveFromList(List<string> listNeedMove, List<string> listMove)
        {
            if (listNeedMove == null || listMove == null || listNeedMove.Count < 1 || listMove.Count < 1)
            {
                return;
            }
            for (int i = 0; i < listMove.Count; i++)
            {
                listNeedMove.Remove(listMove[i]);
            }
        }

        public static bool IsContainListValue(string strIn, List<string> listIn, bool bReg = false)
        {
            if (listIn == null)
            {
                return false;
            }
            strIn = strIn.Replace(" ", "");
            strIn = strIn.ToLower();
            int len = listIn.Count;
            for (int i = 0; i < len; i++)
            {
                if (strIn.Contains(listIn[i]))
                {
                    return true;
                }
                else if(bReg && listIn[i].Contains("&") && listIn[i].Length > 2 && RegMatch(strIn, listIn[i]))//正则匹配规则
                {
                    return true;
                }
            }
            return false;
        }
        // 匹配字符中&代替任意字符的情形，来自名字匹配规则
        public static bool RegMatch(string strIn, string strKey)
        {
            strKey = strKey.Replace("&", ".*");
            return Regex.IsMatch(strIn, strKey);
        }

        /// 屏蔽列表中所包含的文字
        public static bool IsHaveShieldWord(string strIn, DictStringList sdt, List<RangeU16> listRgPtct)
        {
            strIn = strIn.ToLower();
            int len = strIn.Length;
            for (int i = 0; i < len; i++)
            {
                if (IsInRange(i, listRgPtct))
                {
                    continue;
                }
                List<string> listString = sdt.GetStringList(strIn[i]);
                if (listString != null && listString.Count > 0)
                {
                    string strShield = GetShieldWord(strIn.Substring(i), listString);
                    if (!strShield.Equals(string.Empty)) // 查到了屏蔽字
                    {
                        int shieldLen = strShield.Length;
                        int end = i + shieldLen - 1;
                        if (IsInRange(end, listRgPtct) || IsContainsRange(i, end, listRgPtct))
                        {
                            continue;
                        }
                        return true;

                    }
                }
            }
            return false;
        }


        /// 屏蔽列表中所包含的文字
        public static string ShieldWords(string strIn, DictStringList sdt, List<RangeU16> listRgPtct)
        {
            int len = strIn.Length;
            char[] charArr = null;
            for (int i = 0; i < len; i++)
            {
                if (IsInRange(i, listRgPtct))
                {
                    continue;
                }
                List<string> listString = sdt.GetStringList(strIn[i]);//映射是需要转换成小写的
                if (listString != null && listString.Count > 0)
                {
                    string strShield = GetShieldWord(strIn.Substring(i), listString);
                    if (!strShield.Equals(string.Empty)) // 查到了屏蔽字
                    {
                        int shieldLen = strShield.Length;
                        int end = i + shieldLen - 1;
                        if (IsInRange(end, listRgPtct) || IsContainsRange(i, end, listRgPtct))
                        {
                            continue;
                        }
                        int replaceIdx = 0;
                        int j;
                        for (j = i; j < len && replaceIdx < shieldLen; j++)
                        {
                            if (strIn[j] == ' ')
                            {
                                continue;
                            }
                            else
                            {
                                if (charArr == null)
                                {
                                    charArr = strIn.ToArray();
                                }
                                charArr[j] = '*';
                                replaceIdx++;
                            }

                        }
                        i = j-1; // 替换到的地方 

                    }
                }
            }
            if (charArr == null)
            {
                return strIn;
            }
            return new string(charArr);
        }

        private static string GetShieldWord(string strIn, List<string> listIn)
        {
            strIn = strIn.Replace(" ", "");
            int strLen = strIn.Length;
            int listLen = listIn.Count;
            for(int i = 0; i < listLen; i++)
            {
                int listStrLen = listIn[i].Length;
                int j = 0;
                for(; j < strLen && j < listStrLen; j++)//从开始每个字符都一致
                {
                    if(strIn[j] != listIn[i][j])
                    {
                        break;
                    }
                }
                if(j == listStrLen)
                {
                    return listIn[i];
                }
            }
            return string.Empty;

        }

        public static bool IsHaveSpecialNum(string strIn, List<RangeU16> listRgPtct)
        {
            int len = strIn.Length;
            bool bNumBefore = false;
            for (int i = 0; i < len; i++)
            {
                if (IsInRange(i, listRgPtct))
                {
                    bNumBefore = false;
                    continue;
                }
                if (SpecialCode.IsArabicNum(strIn[i]))
                {
                    if (bNumBefore) // 不判断中间数字
                    {
                        continue;
                    }
                    bNumBefore = true;
                }
                else
                {
                    bNumBefore = false;
                    continue;
                }

                List<string> listString = SpecialCode.ListBadNum;
                string strShield = GetShieldNum(strIn.Substring(i), listString);
                if (!strShield.Equals(string.Empty)) // 查到了屏蔽字
                {
                    int shieldLen = strShield.Length;
                    int end = i + shieldLen - 1;
                    if (IsInRange(end, listRgPtct) || IsContainsRange(i, end, listRgPtct)) // 在保护区间就不算
                    {
                        continue;
                    }
                    return true;

                }

            }
            return false;
        }


        // 移除特殊字符，需要完全匹配 具体数据在SpecialCode.ListBadNum
        public static string ShieldSpecialNum(string strIn, List<RangeU16> listRgPtct)
        {
            int len = strIn.Length;
            char[] charArr = null;
            bool bNumBefore = false;
            for (int i = 0; i < len; i++)
            {
                if (IsInRange(i, listRgPtct))
                {
                    bNumBefore = false;
                    continue;
                }
                if (SpecialCode.IsArabicNum(strIn[i]))
                {
                    if (bNumBefore) // 不判断中间数字
                    {
                        continue;
                    }
                    bNumBefore = true;
                }
                else
                {
                    bNumBefore = false;
                    continue;
                }

                List<string> listString = SpecialCode.ListBadNum;
                string strShield = GetShieldNum(strIn.Substring(i), listString);
                if (!strShield.Equals(string.Empty)) // 查到了屏蔽字
                {
                    int shieldLen = strShield.Length;
                    int end = i + shieldLen - 1;
                    if (IsInRange(end, listRgPtct) || IsContainsRange(i, end, listRgPtct)) // 在保护区间就不算
                    {
                        continue;
                    }
                    int replaceIdx = 0;
                    int j;
                    for (j = i; j < len && replaceIdx < shieldLen; j++)
                    {
                        if (strIn[j] == ' ')
                        {
                            continue;
                        }
                        else
                        {
                            if (charArr == null)
                            {
                                charArr = strIn.ToArray();
                            }
                            charArr[j] = '*';
                            replaceIdx++;
                        }

                    }
                    i = j; // 替换到的地方 

                }

            }
            if (charArr == null)
            {
                return strIn;
            }
            return new string(charArr);


        }

        // 后面的字符不是数字才算
        private static string GetShieldNum(string strIn, List<string> listIn)
        {
            strIn = strIn.Replace(" ", "");
            sListTmp.Clear();
            sListTmp.AddRange(listIn);
            int len = strIn.Length;
            for (int i = 0; i < len; i++)
            {
                if (sListTmp.Count > 0)
                {
                    for (int j = sListTmp.Count - 1; j >= 0; j--)
                    {
                        if (i < sListTmp[j].Length)
                        {
                            if (strIn[i] != sListTmp[j][i])
                            {
                                sListTmp.RemoveAt(j);
                            }
                            else if (i == sListTmp[j].Length - 1)
                            {
                                if (i == len - 1 || !SpecialCode.IsArabicNum(strIn[i + 1]))
                                {
                                    return sListTmp[j];
                                }
                                else
                                {
                                    sListTmp.RemoveAt(j);
                                }

                            }

                        }

                    }
                }

            }
            return string.Empty;

        }

        public static string ConverListRangeToSpace(string strIn, List<RangeU16> listRg)
        {
            if (listRg != null && listRg.Count > 0)
            {
                int len = strIn.Length;
                char[] arrIn = strIn.ToArray();

                for (int i = 0; i < len; i++)
                {
                    if (CheckComon.IsInRange(i, listRg))
                    {
                        arrIn[i] = ' ';
                    }
                }
                strIn = new string(arrIn);
            }
            return strIn;
        }

        // 获取保护字段范围，包括.com 保护， #数字表情保护 后续还要有客户端定义的特定标记保护
        public static List<RangeU16> GetProtectRange(string strIn)
        {
            List<RangeU16> listRg = CheckComon.GetProtectComRange(strIn);
            listRg = GetProtectFaceRange(strIn, listRg);
            return listRg;
        }


        // 获取到被保护的字段区间 .com 类型的保护
        public static List<RangeU16> GetProtectComRange(string strIn)
        {
            int len = strIn.Length;
            List<RangeU16> listRange = null;
            for (int i = 0; i < len; i++)
            {
                List<string> listString = SpecialCode.ListProtectCom;
                string strFind = GetProcteCom(strIn.Substring(i), listString);
                if (!strFind.Equals(string.Empty)) // 查到了屏蔽字
                {
                    int shieldLen = strFind.Length;
                    if (i == 0 || SpecialCode.IsProtectComOkBf(strIn[i - 1]))
                    {
                        if (listRange == null)
                        {
                            listRange = new List<RangeU16>();
                        }
                        RangeU16 rg = new RangeU16();
                        rg.Begin = (UInt16)(i);
                        rg.End = (UInt16)(i + shieldLen - 1);
                        listRange.Add(rg);
                        i = i + shieldLen;
                    }


                }

            }
            return listRange;
        }
        // 后面的字符不是数字才算
        private static string GetProcteCom(string strIn, List<string> listIn)
        {
            sListTmp.Clear();
            sListTmp.AddRange(listIn);
            int len = strIn.Length;
            for (int i = 0; i < len; i++)
            {
                if (sListTmp.Count > 0)
                {
                    for (int j = sListTmp.Count - 1; j >= 0; j--)
                    {
                        if (i < sListTmp[j].Length)
                        {
                            if (strIn[i] != sListTmp[j][i])
                            {
                                sListTmp.RemoveAt(j);
                            }
                            else if (i == sListTmp[j].Length - 1)
                            {
                                if (i == len - 1 || SpecialCode.IsProtectComOkAf(strIn[i + 1]))
                                {
                                    return sListTmp[j];
                                }
                                else
                                {
                                    sListTmp.RemoveAt(j);
                                }

                            }

                        }

                    }
                }

            }
            return string.Empty;

        }

        /// 获取表情屏蔽字段
        /// 这个是在已经获取部分屏蔽段的基础上继续执行
        public static List<RangeU16> GetProtectFaceRange(string strIn, List<RangeU16> listRange)
        {

            if (listRange != null && listRange.Count > 0)
            {
                strIn = ConverListRangeToSpace(strIn, listRange);
            }
            int len = strIn.Length;
            for (int i = 0; i < len; i++)
            {
                if (strIn[i] == '#')
                {
                    int iFaceNum = 0;
                    if (i + 1 < len && SpecialCode.IsArabicNum(strIn[i + 1]) && strIn[i + 1] != '0')
                    {
                        iFaceNum = 1;
                        if (i + 2 < len && SpecialCode.IsArabicNum(strIn[i + 2]) && int.Parse(strIn.Substring(i+1, 2)) <= Config.MaxFaceIndex)
                        {
                            iFaceNum = 2;
                            if (i + 3 < len && SpecialCode.IsArabicNum(strIn[i + 3]) && int.Parse(strIn.Substring(i+1, 3)) <= Config.MaxFaceIndex)
                            {
                                iFaceNum = 3;
                            }
                        }
                        int endIdx = i + iFaceNum;
                        if (listRange == null)
                        {
                            listRange = new List<RangeU16>();
                        }
                        RangeU16 rg = new RangeU16();
                        rg.Begin = (UInt16)i;
                        rg.End = (UInt16)endIdx;
                        listRange.Add(rg);
                        i = endIdx + 1;

                    }

                }

            }
            return listRange;

        }
    }
}