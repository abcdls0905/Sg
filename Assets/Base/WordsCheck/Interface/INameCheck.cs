using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordsCheck
{
   public  interface INameCheck
    {

        /// 用于初始化，确定是否是审核版本以及是否需要加入屏蔽白名单。
        ///  strWhiteProtectName 白名单保护字段,加入白名单里的词在名字屏蔽表中不再生效。写的是ban文件夹下的文件名，如果没有填""
        void Init(string strWhiteProtectName);


        /// 用于检查名字是否合法
         bool IsNameValid(string strIn);


        /// 一般名字检查，包括长度，必须包含英文或者汉字，是否有屏蔽字，是否有非法字符判定
        /// 返回的是错误码，逻辑层根据错误码进行提示
        /// NAME_ERROR 对错误类型进行了定义，具体意思如下
        ///  NONE = 0, // 正常
        /// LENGTH_INVALID = 1, // 长度不合法，默认是2-7个字符，特殊情况需要用SetNameLengthRange（）设置合法字符长度
        ///NEED_EN_OR_CN_CODE = 2, //名字必须含有汉字或字母
        ///HAVE_SHIELD_NAME = 3, // 包含屏蔽表里的文字
        ///HANVE_INVLALID_CODE = 4, // 有非法字符
        NAME_ERROR NormaNameCheck(string strIn);


    }
}
