using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WordsCheck
{
    class NameCheckInfo
    {


        public static NameCheckInfo GetInstance()
        {
            if (sInstance == null)
            {
                sInstance = new NameCheckInfo();
                sInstance.Init();
            }
            return sInstance;
        }
        private static NameCheckInfo sInstance;


        /// 一般名字检查，包括长度，必须包含英文或者汉字，是否有屏蔽字，是否有非法字符判定
        /// 如果返回为空，正常，否则返回提示内容
        /// 这个属于业务逻辑层，正常应该在具体功能里做
        /// 

        /// 是否是有效字符
        public bool IsValidCode(string strIn)
        {
            for (int i = 0; i < strIn.Length; i++)
            {
                if (!IsValidCode(strIn[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsValidCode(char cIn)
        {
            if (SpecialCode.IsEnCode(cIn) || SpecialCode.IsArabicNum(cIn) || SpecialCode.IsConnectCode(cIn))
            {
                return true;
            }
            if (IsWhiteCode(cIn))
            {
                return true;
            }

            return IsInUnicodeZone(cIn);
        }


        /// 是否包含英文字母或者汉字
        public bool IsHaveEnOrCnCode(string strIn)
        {

            int len = strIn.Length;
            for (int i = 0; i < len; i++)
            {
                if (IsEnOrCnCode(strIn[i]))
                {
                    return true;
                }
            }
            return false;
        }
        private bool IsEnOrCnCode(char cIn)
        {
            return SpecialCode.IsEnCode(cIn) || SpecialCode.IsCnCode(cIn);
        }

        private void Init()
        {
            LoadWhiteCode();
            LoadUnicodeZone();
        }


        private List<char> listWhite;// 白名单

        public bool IsWhiteCode(char cIn)
        {
            if (listWhite == null)
            {
                return false;
            }
            return listWhite.IndexOf(cIn) >= 0;
        }

        private void LoadWhiteCode()
        {
            if (listWhite == null)
            {
                listWhite = new List<char>();

            }
            listWhite.Clear();
            string[] AllLines = CheckComon.LoadTextLines("whitelist");
            if (AllLines == null)
            {
                Debug.LogError(" load whitelist failed");
                return;
            }
            int len = AllLines.Length;
            for (int i = 1; i < len; i++)
            {
                string strLine = AllLines[i].Replace("\r", "");
                for (int j = 0; j < strLine.Length; j++)
                {
                    listWhite.Add(strLine[j]);
                }
            }

        }

        private List<RangeU16> listUnicodeRange; // unicode范围

        /// 每一个字符都在unicode区间判定
        public bool IsInUnicodeZone(string strIn)
        {

            for (int i = 0; i < strIn.Length; i++)
            {
                if (!IsInUnicodeZone(strIn[i]))
                {
                    return false;
                }
            }
            return true;
        }

        /// 判断字符是否在允许的空间范围内
        public bool IsInUnicodeZone(char In)
        {
            if (listUnicodeRange == null)
            {
                return false;
            }
            for (int i = 0; i < listUnicodeRange.Count; i++)
            {
                if (listUnicodeRange[i].Begin <= (UInt16)In && listUnicodeRange[i].End >= (UInt16)In)
                {
                    return true;
                }
            }
            return false;
        }
        private void LoadUnicodeZone()
        {
            if (listUnicodeRange == null)
            {
                listUnicodeRange = new List<RangeU16>();

            }
            listUnicodeRange.Clear();
            string[] AllLines = CheckComon.LoadTextLines("unicodezone");
            if (AllLines == null)
            {
                Debug.LogError(" load unicodezone failed");
                return;
            }
            int len = AllLines.Length;
            for (int i = 1; i < len; i++)
            {
                string strLine = AllLines[i].Replace("\r", "");
                string[] temp = strLine.Split(',');
                if (temp.Length >= 3)
                {
                    RangeU16 rg = new RangeU16();
                    rg.Begin = Convert.ToUInt16(temp[0], 16);
                    rg.End = Convert.ToUInt16(temp[1], 16);
                    listUnicodeRange.Add(rg);
                }
            }

        }


    }
}
