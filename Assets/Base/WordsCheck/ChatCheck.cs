using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WordsCheck
{
    /// <summary>
    /// 聊天相关检测处理
    /// </summary>
    public class ChatCheck : IChatCheck
    {
        private List<string> listSentenceShield; // 整句屏蔽
        private List<string> listTotalWordsShield; // 公共屏蔽+ 语言屏蔽 的组合，根据InitChanenl决定

        private DictStringList dicStrListWd; // 单句屏蔽的映射
        private DictStringList dicStrListSc; // 整句屏蔽的映射
        private bool IsAudit = false; // 是否是审核
        private bool NeedPublicShield = true; // 启用公共屏蔽字库
        private bool NeedLangShield = false; // 启用语言屏蔽字库
        private string WhiteProtectName; //  白名单保护字段

        public ChatCheck()
        {
            ShieldInfo.GetInstance().AddRefresh(Refresh);
        }

        ~ChatCheck()
        {
            ShieldInfo.GetInstance().RemoveRefresh(Refresh);
        }

        public void InitChannel(bool bPublicShield, bool bLangShield, string strWhiteProtectName)
        {
            IsAudit = Config.IsAudit;
            NeedPublicShield = bPublicShield;
            NeedLangShield = bLangShield;
            WhiteProtectName = strWhiteProtectName;

            if (listTotalWordsShield == null)
            {
                listTotalWordsShield = new List<string>();
            }
            listTotalWordsShield.Clear();

            if (listSentenceShield == null)
            {
                listSentenceShield = new List<string>();
            }
            listSentenceShield.Clear();

            if (IsAudit)
            {
                listTotalWordsShield.AddRange(ShieldInfo.GetInstance().ShPublicShield.StringList);
            }
            else
            {
                if (bPublicShield)
                {
                    listTotalWordsShield.AddRange(ShieldInfo.GetInstance().PublicShield.StringList);
                }
                if (bLangShield)
                {
                    listTotalWordsShield.AddRange(ShieldInfo.GetInstance().LangShield.StringList);
                }
                listSentenceShield = ShieldInfo.GetInstance().SentenceShield.StringList;
                if (!string.IsNullOrEmpty(strWhiteProtectName))
                {
                    string strVersion = string.Empty;
                    List<string> listWhiteProtect = null;
                    if (!string.IsNullOrEmpty(strWhiteProtectName))
                        listWhiteProtect = CheckComon.GetStringListFromFile(strWhiteProtectName, listWhiteProtect, ref strVersion);
                    CheckComon.RemoveFromList(listTotalWordsShield, listWhiteProtect); // 单句屏蔽移除白名单里的项
                    CheckComon.RemoveFromList(listSentenceShield, listWhiteProtect);  //  整句屏蔽移除白名单里的项
                }
            }
            if (dicStrListWd == null)
                dicStrListWd = new DictStringList();
            dicStrListWd.Init(listTotalWordsShield);

            if (dicStrListSc == null)
                dicStrListSc = new DictStringList();
            dicStrListSc.Init(listSentenceShield);
        }

        public void Refresh()
        {
            InitChannel(NeedPublicShield, NeedLangShield, WhiteProtectName);
        }

        public string ShieldWord(string strIn, bool bProtect)
        {
            string strTRet= WordsConvert.ShieldConvet(strIn);
            bool bChanged = !strIn.Equals(strTRet);
            List<RangeU16> listRg = null;
            if (bProtect)
            {
                listRg = CheckComon.GetProtectRange(strTRet);
            }
            strTRet = CheckComon.ShieldWords(strTRet, dicStrListWd, listRg);
            strTRet = CheckComon.ShieldSpecialNum(strTRet, listRg);
            if (bChanged)//还原替换过的文字
            {
                StringBuilder ts = new StringBuilder(strIn);
                int len = ts.Length;
                for (int i  = 0; i < len; i++)
                {
                    if(strTRet[i] == '*')
                    {
                        ts[i] = '*';
                    }
                }
                return ts.ToString();
            }
            else
            {
                return strTRet;
            }
        }


        /// 如果启用屏蔽保护，将保护内容先替换成空格，然后继续检测
        public bool IsSentenceShield(string strIn, bool bProtect, bool bSerialNum)
        {
            if (IsAudit)
            {
                return false;
            }
            strIn = WordsConvert.ShieldConvet(strIn);
            int len = strIn.Length;
            if (bProtect)
            {
                List<RangeU16> listRg = CheckComon.GetProtectRange(strIn);
                if (listRg != null && listRg.Count > 0)
                {
                    strIn = CheckComon.ConverListRangeToSpace(strIn, listRg);

                }
            }
            strIn = strIn.Replace(" ", "");
            if (bSerialNum)
            {
                if (len >= SpecialCode.SENTENCE_SHIELD_SERIALNUM_COUNT && SpecialCode.GetSerialNumCodeNum(strIn) >= SpecialCode.SENTENCE_SHIELD_SERIALNUM_COUNT)
                {
                    return true;
                }
            }
            return CheckComon.IsHaveShieldWord(strIn, dicStrListSc, null);
        }


    }


}
