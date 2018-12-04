using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordsCheck
{
    class DictStringList
    {
        private Dictionary<char, List<string>> dicStringList;
        public void Init(List<string> listIn)
        {
            if(listIn == null)
            {
                return;
            }
            if (dicStringList == null)
            {
                dicStringList = new Dictionary<char, List<string>>();
            }
            dicStringList.Clear();
            int len = listIn.Count;
            for (int i = 0; i < len; i++)
            {
                if (!dicStringList.ContainsKey(listIn[i][0]))
                {
                    dicStringList.Add(listIn[i][0], new List<string>());
                }
                if (dicStringList[listIn[i][0]].IndexOf(listIn[i]) < 0) // 避免重复加入，影响检索速度
                    InsertByLength(dicStringList[listIn[i][0]], listIn[i]);
            }
        }

        //长度长的排在前面，尽量先把长的字符过滤掉
        private static void InsertByLength(List<string> listStr, string str)
        {
            for(int i = 0; i < listStr.Count; i++)
            {
                int strLen = str.Length;
                if(strLen > listStr[i].Length)
                {
                    listStr.Insert(i, str);
                    return;
                }
            }
            listStr.Add(str);
        }
        public List<string> GetStringList(char cIn)
        {
            if(dicStringList != null && dicStringList.ContainsKey(cIn))
            {
                return dicStringList[cIn];
            }
            return null;
        }
    }
}
