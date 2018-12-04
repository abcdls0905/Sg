using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordsCheck
{
    public class Config
    {
        public static string DataRelatePath = "ban"; //数据存放的相对路径,相对于StreamingAssets
        public static bool IsAudit = false;  // 是否是审核版
        public static int MaxFaceIndex = 190; // #1...MaxFaceIndex 表示的是表情，在这个范围内我们认为是保护字段 目前最大处理为999
        public static string LangChatshieldName = "chatzb_public_cn";// 语言屏蔽字库，由于不同的语言版本可能不一样，当然也可以用同样的文件名放不同的内容

        //////////////////////////////////////////////////////////////////////////可选，主要是用于匹配x7的规则，如果不填写我们按DataRelatePath规则
        public static string DataRelatePathAssets = string.Empty;//相对于Assets的路径比如Gamedata/ban
        public static string DataRelatePathStream = string.Empty;//相对于Application.streamingAssetsPath的路径
        public static string DataRelatePathUpdate = string.Empty;//相对于Application.persistentDataPath 的路径
    }
}
