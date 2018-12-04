using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePlatform
{

    //公告返回
    public class RetNotice
    {

        [SerializableAttribute]
        public struct NoticeItem
        {
            public string serverIds;//需要发布公告的服务器列表，不设置默认是在所有服显示。
            public int platform;//平台，按位与，1为ios，2为android，4为pc
            public int level;//优先级，0普通，1优先，2加急
            public int startDate;//公告开始时间 1970年1月1号后的秒数
            public int endDate;//公告结束时间

            public string title;//公告标题
            public string summary;//公告简要
            public string content;//公告内容

            
            public bool roll;//设置是否滚动显示
            public int type;//公告类型，0维护公告，1排队，2更新公告，3维护提示
            

            public string language;//语言
            public string imgurl;//预览图地址,一般不需要
        }

        [SerializableAttribute]
        public struct ExtData
        {
            public int records;//公告条数
            public NoticeItem[] items;//所有公告
        }

       
        public ExtData ext_data;//只关心这个，下面几个参数是网站的冗余参数
        public string Message;//不用
        public bool RetSucceed;//不用
        public int code;//不用
        public bool Succeed;//不用

        //////////////////////////////////////上面是参数 下面是一些对参数的判定

        List<NoticeItem> listRet = null;
        //获取有效的通知，包括服务器id，终端平台和有效时间判定
        public List<NoticeItem> GetNoticItem(string serverId)
        {
            if(listRet != null)
            {
                listRet.Clear();
            }
            if(ext_data.records > 0)
            {
                for(int i = 0; i < ext_data.records; i++)
                {
                    NoticeItem item = ext_data.items[i];
                    if(IsFitSeverIds(serverId,item.serverIds))
                    {
                        if(IsFitPlatform(item.platform) && IsTimeActive(item.startDate, item.endDate))
                        {
                            if(listRet == null)
                            {
                                listRet = new List<NoticeItem>();
                            }
                            listRet.Add(item);
                        }
                    }
                }
            }
            return listRet;
        }

        // 判定服务器id是否匹配serverIdIn为自己的服务器id，severIds为公告参数里的服务器id
        public bool IsFitSeverIds(string serverIdIn, string severIds)
        {
            if(severIds == "0")
            {
                return true;
            }
            string[] arrServerId = severIds.Split('|');
            if(arrServerId != null)
            {
                for(int i = 0; i < arrServerId.Length; i++)
                {
                    if(serverIdIn == arrServerId[i])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //判定当前时间是否在benginTme和endTime之间 用于公告的startDate和endDate判定
        public bool IsTimeActive(int benginTme, int endTime)
        {
            int now = PlatformUtil.TimeToGreenwich(DateTime.Now);
            return (now >= benginTme && now <= endTime);
        }

        //判定公告是否属于当前终端平台 传入platform：1为ios，2为android，4为pc
        public bool IsFitPlatform(int flag)
        {
            if (flag == 0)
                return true;
#if UNITY_IPHONE
            return (flag & 1) > 0;
#elif UNITY_ANDROID
            return (flag & (1 << 1)) > 0;
#else
            return (flag & (1<<2)) > 0;
#endif
        }
    }


    public class RetRegAcct
    {
        [SerializableAttribute]
        public struct Data
        {
            public string img;
            public string vkey;
            public string vcode;
        }
        public int code;
        public string msg;
        public string requestid;
        public Data data;
    }

    
}
