using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace WordsCheck
{
    /// <summary>
    /// 文字列表维护，主要包括文件加载，文件重载，临时屏蔽屏蔽字段增加，临时屏蔽的屏蔽字段删除
    /// </summary>
    class WordsList
    {
      
        private List<string> listFromFile;
        private List<string> listTmp;
        private List<string> listTotal;
        private string FileName = string.Empty; // 文件名，相对与Resource的名字
        private string FVersion = string.Empty;
        private string CurVersion = string.Empty;

        public WordsList(string strName)
        {
            FileName = strName;

        }

        public string VersionInf
        {
            get
            {
                return FVersion;
            }
        }

        private void Save()
        {
            if(!CurVersion.Equals(FVersion)) // 我们认为服务端返回屏蔽库一定是最新版本的，所以版本号不一致就用服务端的覆盖
            {
                if(TxtOpr.Save(listFromFile, CurVersion, FileName))
                {
                    FVersion = CurVersion;
                }
            }
        }

        public void Init()
        {
            listFromFile = CheckComon.GetStringListFromFile(FileName, listFromFile, ref FVersion);
            CurVersion = FVersion;

        }

        //更新最新数据，并存储
        public void UpdateFileInfo(List<string> listIn, string strVersion)
        {
            if(listIn ==null || listIn.Count < 1)
            {
                return;
            }
            if(strVersion != CurVersion)
            {
                CurVersion = strVersion;
                listFromFile.Clear();
                listFromFile.AddRange(listIn);
                Save();
            }

        }

        /// 添加临时屏蔽字段
        public void AddTmp(string strIn)
        {
            if(!string.IsNullOrEmpty(strIn))
            {
                if(listTmp == null)
                {
                    listTmp = new List<string>();
                }
                if (listTmp.IndexOf(strIn) < 0)
                {
                    listTmp.Add(strIn);
                }

            }
        }

        /// 添加临时屏蔽字段列表
        public void AddTmp(List<string> listIn)
        {
            if(listIn != null)
            {
                for(int i = 0; i < listIn.Count; i++)
                {
                    AddTmp(listIn[i]);
                }
            }
        }

        /// 移除临时屏蔽字段,对已经添加的临时屏蔽字段进行移除
        public void RemoveTmp(string strIn)
        {
            if (!string.IsNullOrEmpty(strIn))
            {
                if (listTmp == null)
                {
                    listTmp = new List<string>();
                }
                if (listTmp != null && listTmp.IndexOf(strIn) >= 0)
                {
                    listTmp.Remove(strIn);
                }

            }
        }

        public void ClearTmp()
        {
            if (listTmp != null)
            {
                listTmp.Clear();
            }
        }

        // 获取需要屏蔽的字段
        public List<string> StringList
        {
            get
            {
                if(listTotal == null)
                {
                    listTotal = new List<string>();
                }
                listTotal.Clear();
                if(listFromFile != null)
                    listTotal.AddRange(listFromFile);
                if (listTmp != null)
                    listTotal.AddRange(listTmp);
                return listTotal;
            }
        }
    }
}
