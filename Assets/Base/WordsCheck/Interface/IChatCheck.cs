using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordsCheck
{
    public interface IChatCheck
    {

   
        /// 用于初始化聊天频道，具体
        ///  bPublicShield 是否启用启用公共屏蔽字库  
        ///  bLangShield  是否启用语言屏蔽字库
        ///  strWhiteProtectName 白名单保护字段，加入白名单的字段不再屏蔽，对单句屏蔽和整句屏蔽都生效
        void InitChannel(bool bPublicShield, bool bLangShield, string strWhiteProtectName);


        ///单词屏蔽,将屏蔽后的字用*替换，返回替换后的结果
        /// bool bProtect 是否使用屏蔽保护，如果为真对于符合屏蔽保护规则的词汇不进行屏蔽
        string ShieldWord(string strIn, bool bProtect);


        /// 用于判断是否是整句屏蔽
        /// bool bProtect 是否启用屏蔽保护
        /// bool bSerialNum 是否判断连续数字，如果启用连续数字5个以上将返回为真
        bool IsSentenceShield(string strIn, bool bProtect, bool bSerialNum);


    }
}
