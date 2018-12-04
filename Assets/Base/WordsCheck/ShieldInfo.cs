using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordsCheck
{
    public enum ShieldType
    {
        PUBLIC = 1, //公共屏蔽单词（公聊+私聊）
        LANG = 2, //语言屏蔽单词（公聊）
        SENTENCE = 3, //整句屏蔽字库
        NAME = 4, //起名屏蔽字库
        NAME_AUDIT = 5,//审核起名屏蔽字库
        PUBLIC_AUDIT = 6,//审核聊天屏蔽字库
        ALL = 99, //全部字库
    }



    // 主要用于维护各个屏蔽表格
    class ShieldInfo
    {
        public static ShieldInfo GetInstance()
        {
            if (sInstance == null)
            {
                sInstance = new ShieldInfo();
            }
            return sInstance;
        }
        private static ShieldInfo sInstance;

        private WordsList[] _allWordsList;
        private WordsList[] AllWordsList
        {
            get
            {
                if (_allWordsList == null)
                {
                    _allWordsList = new WordsList[]
                    {
                            PublicShield,
                            LangShield,
                            SentenceShield,
                            NameShield,
                            ShNameShield,
                            ShPublicShield

                    };
                }
                return _allWordsList;
            }
        }

        public string GetVersion(ShieldType type)
        {
            WordsList wl = GetByType(type);
            if (wl != null)
            {
                return wl.VersionInf;
            }
            return string.Empty;
        }

        public void UpdateFileInfo(ShieldType type, List<string> listIn, string strVersion)
        {
            WordsList wl = GetByType(type);
            if (wl != null)
            {
                wl.UpdateFileInfo(listIn, strVersion);
                OnTmpShieldChanged();
            }
        }

        private WordsList GetByType(ShieldType type)
        {
            switch (type)
            {
                case ShieldType.PUBLIC:
                    return PublicShield;
                case ShieldType.LANG:
                    return LangShield;
                case ShieldType.SENTENCE:
                    return SentenceShield;
                case ShieldType.NAME:
                    return NameShield;
                case ShieldType.NAME_AUDIT:
                    return ShNameShield;
                case ShieldType.PUBLIC_AUDIT:
                    return ShPublicShield;
            }
            return null;
        }

        private List<Action> listRefresh;
        public void  AddRefresh(Action act)
        {
            if(listRefresh == null)
            {
                listRefresh = new List<Action>();
            }
            if(!listRefresh.Contains(act))
            {
                listRefresh.Add(act);
            }
        }

        public void RemoveRefresh(Action act)
        {
            if (listRefresh != null)
            {
                listRefresh.Remove(act);
            }
        }

        // 当临时屏蔽数据发生变化时要通知到上层刷新接口
        private void OnTmpShieldChanged()
        {
            if(listRefresh != null && listRefresh.Count > 0)
            {
                for(int i = listRefresh.Count - 1; i >= 0; i--)
                {
                    if(listRefresh[i] != null)
                    {
                        listRefresh[i]();
                    }
                    else
                    {
                        listRefresh.RemoveAt(i);
                    }
                }
            }
        }

        // 添加某个临时屏蔽字
        public void AddTmp(ShieldType type, string strIn)
        {
            if(type == ShieldType.ALL)
            {
                for (int i = 0; i < AllWordsList.Length; i++)
                {
                    AllWordsList[i].AddTmp(strIn);
                }
            }
            else
            {
                WordsList wl = GetByType(type);
                if(wl != null)
                {
                    wl.AddTmp(strIn);
                }

            }
            OnTmpShieldChanged();


        }

        /// 添加临时屏蔽字段列表
        public void AddTmp(ShieldType type, List<string> listIn)
        {
            if (listIn != null)
            {
                for (int i = 0; i < listIn.Count; i++)
                {
                    AddTmp(type, listIn[i]);
                }
            }
        }

        // 移除某个临时屏蔽字
        public void RemoveTmp(ShieldType type, string strIn)
        {
            if (type == ShieldType.ALL)
            {
                for (int i = 0; i < AllWordsList.Length; i++)
                {
                    AllWordsList[i].RemoveTmp(strIn);
                }
            }
            else
            {
                WordsList wl = GetByType(type);
                if (wl != null)
                {
                    wl.RemoveTmp(strIn);
                }

            }
            OnTmpShieldChanged();
        }

        // 清空临时屏蔽字
        public void ClearTmp(ShieldType type)
        {
            if (type == ShieldType.ALL)
            {
                for (int i = 0; i < AllWordsList.Length; i++)
                {
                    AllWordsList[i].ClearTmp();
                }
            }
            else
            {
                WordsList wl = GetByType(type);
                if (wl != null)
                {
                    wl.ClearTmp();
                }

            }
            OnTmpShieldChanged();
        }

        private WordsList _PublicShield;

        // 公共屏蔽字库
        public WordsList PublicShield
        {
            get
            {
                if (_PublicShield == null)
                {
                    _PublicShield = new WordsList("chathy_public_cn");
                    _PublicShield.Init();
                }
                return _PublicShield;
            }
        }


        // 语言屏蔽字库
        private WordsList _LangShield;
        public WordsList LangShield
        {
            get
            {
                if (_LangShield == null)
                {
                    _LangShield = new WordsList(Config.LangChatshieldName);//"chatzb_public_cn"
                    _LangShield.Init();
                }
                return _LangShield;
            }
        }


        // 整句屏蔽字库
        private WordsList _SentenceShield;
        public WordsList SentenceShield
        {
            get
            {
                if (_SentenceShield == null)
                {
                    _SentenceShield = new WordsList("black_public_cn");
                    _SentenceShield.Init();
                }
                return _SentenceShield;
            }
        }


        // 名字屏蔽字库
        private WordsList _NameShield;
        public WordsList NameShield
        {
            get
            {
                if (_NameShield == null)
                {
                    _NameShield = new WordsList("name_public_cn");
                    _NameShield.Init();
                }
                return _NameShield;
            }
        }

        // 审核公共屏蔽字库
        private WordsList _ShPublicShield;
        public WordsList ShPublicShield
        {
            get
            {
                if (_ShPublicShield == null)
                {
                    _ShPublicShield = new WordsList("sh");
                    _ShPublicShield.Init();
                }
                return _ShPublicShield;
            }
        }

        // 审核名字屏蔽字库
        private WordsList _ShNameShield;
        public WordsList ShNameShield
        {
            get
            {
                if (_ShNameShield == null)
                {
                    _ShNameShield = new WordsList("sh");
                    _ShNameShield.Init();
                }
                return _ShNameShield;
            }
        }


    }


   
}
