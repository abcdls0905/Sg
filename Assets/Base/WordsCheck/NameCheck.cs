using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WordsCheck
{

    public enum NAME_ERROR
    {
        NONE = 0, // 正常
        LENGTH_INVALID = 1, // 长度不合法，默认是2-7个字符，特殊情况需要用SetNameLengthRange（）设置合法字符长度
        NEED_EN_OR_CN_CODE = 2, //名字必须含有汉字或字母
        HANVE_INVLALID_CODE = 3, // 有非法字符
    }

    /// <summary>
    /// 名字合法性检查
    /// </summary>
    public class NameCheck : INameCheck
    {


        private bool IsAudit = false; // 是否是审核
        private string WhiteProtectName; //  白名单保护字段
        private List<string> listNameShield;

        public NameCheck()
        {
            ShieldInfo.GetInstance().AddRefresh(Refresh);
        }
        ~NameCheck()
        {
            ShieldInfo.GetInstance().RemoveRefresh(Refresh);
        }
        public void Init(string strWhiteProtectName)
        {
            IsAudit = Config.IsAudit;
            WhiteProtectName = strWhiteProtectName;

            if (IsAudit)
            {
                listNameShield = ShieldInfo.GetInstance().ShNameShield.StringList;
            }
            else
            {
                listNameShield = ShieldInfo.GetInstance().NameShield.StringList;
                List<string> listWhiteProtect = null;
                string strVersion = string.Empty;
                if (!string.IsNullOrEmpty(strWhiteProtectName))
                    listWhiteProtect = CheckComon.GetStringListFromFile(strWhiteProtectName, listWhiteProtect, ref strVersion);
                CheckComon.RemoveFromList(listNameShield, listWhiteProtect); // 移除白名单允许的名字
            }


        }

        public void Refresh()
        {
            Init(WhiteProtectName);
        }

        /// 自动生成名是否有效
        public bool IsAutoNameValid(string strIn)
        {

            if (IsHaveShieldName(strIn))
            {
                return false;
            }
            return NameCheckInfo.GetInstance().IsValidCode(strIn);

        }

        /// 检查名字是否合法
        public bool IsNameValid(string strIn)
        {
            strIn = WordsConvert.ShieldConvet(strIn);
            if (IsHaveShieldName(strIn))
            {
                return false;
            }
            if (SpecialCode.IsHaveQyNum(strIn))
            {
                return false;
            }
            if (CheckComon.IsHaveSpecialNum(strIn, null))
            {
                return false;
            }
            return NameCheckInfo.GetInstance().IsValidCode(strIn);
        }

        private bool IsHaveShieldName(string strIn)
        {
            return CheckComon.IsContainListValue(strIn, listNameShield, true);
        }

        /// 一般名字检查，包括长度，必须包含英文或者汉字，是否有屏蔽字，是否有非法字符判定
        /// 如果返回为空，正常，否则返回提示内容
        /// 这个属于业务逻辑层，正常应该在具体功能里做
        /// 

        private int MinNameLen = 2;
        private int MaxNameLen = 7;
        public void SetNameLengthRange(int min, int max)
        {
            MinNameLen = min;
            MaxNameLen = max;

        }


        public NAME_ERROR NormaNameCheck(string strIn)
        {
            if (strIn.Length < MinNameLen || strIn.Length > MaxNameLen)
            {
                return NAME_ERROR.LENGTH_INVALID;

            }
            else if (!NameCheckInfo.GetInstance().IsHaveEnOrCnCode(strIn))
            {
                return NAME_ERROR.NEED_EN_OR_CN_CODE;
            }
            else if (!IsNameValid(strIn))
            {
                return NAME_ERROR.HANVE_INVLALID_CODE;
            }
            return NAME_ERROR.NONE;

        }


    }

}