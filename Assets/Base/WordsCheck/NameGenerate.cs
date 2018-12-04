using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WordsCheck
{
    class NameGenerate :INameGenerate
    {

        public string GenerateName()
        {
            Init();
            int val = UnityEngine.Random.Range(0, maxWeight);
            int idx = GetWeighIndex(val);
            string[] strKeys = listNameWeight[idx].strCompose.Split('+');
            string strRet = string.Empty;
            int len = strKeys.Length;
            for (int i = 0; i < len; i++)
            {
                int typIdx = DicTypeIndex[strKeys[i]];
                int count = listNames[typIdx].Count;
                int geIdx = UnityEngine.Random.Range(0, count);
                strRet += listNames[typIdx][geIdx];
            }
            return strRet;

        }

        private NameCheck nameCheck;

        /// 生成符合检测标准的名字
        public string GenerateNameValid()
        {
            string strName = GenerateName();
            if(nameCheck == null)
            {
                nameCheck = new NameCheck(); // 名字检测判定
                nameCheck.Init(""); // 默认为非审核版本没有特殊字段保护白名单
            }
            if (nameCheck.IsAutoNameValid(strName))
            {
                return strName;

            }
            return GenerateNameValid();
        }

        private const int NameTypeNum = 17; // 构成名字的组合要素共17类
        private List<string>[] listNames;   // 存储每一类要素
        private Dictionary<string, int> DicTypeIndex; // 名字类型对应的索引值
        private void Init()
        {
            if (listNames == null)
            {
                listNames = new List<string>[NameTypeNum];
                for (int i = 0; i < NameTypeNum; i++)
                {
                    listNames[i] = new List<string>();
                }
                DicTypeIndex = new Dictionary<string, int>();

                ReadNameTxt();
                ReadNameWeight();
            }
        }
        private void ReadNameTxt()
        {
            for (int i = 0; i < listNames.Length; i++)
            {
                listNames[i].Clear();
            }
            DicTypeIndex.Clear();

            string[] AllLines = CheckComon.LoadTextLines("GeneName");
            if (AllLines == null)
            {
                Debug.LogError(" load GeneName failed");
                return;
            }
            int len = AllLines.Length;
            for (int i = 0; i < len; i++)
            {
                string strLine = AllLines[i].Replace("\r", "");
                string[] temp = strLine.Split('\t');
                int len2 = temp.Length;
                for (int j = 0; j < len2 && j < NameTypeNum; j++)
                {
                    if (i == 0)
                    {
                        DicTypeIndex.Add(temp[j], j);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(temp[j]))
                        {
                            listNames[j].Add(temp[j]);
                        }
                    }
                }

            }
            int num = AllLines.Length;
        }

        struct NameWeight
        {
            public string strCompose;
            public int len;
            public int weight;
            public int weightBegin;

        }

        List<NameWeight> listNameWeight = new List<NameWeight>();
        private int maxWeight;

        private void ReadNameWeight()
        {
            maxWeight = 0;
            string[] AllLines = CheckComon.LoadTextLines("NameWeight");
            if(AllLines == null)
            {
                Debug.LogError(" load NameWeight failed");
                return;
            }
            int len = AllLines.Length;
            for (int i = 1; i < len; i++)
            {
                string strLine = AllLines[i].Replace("\r", "");
                string[] temp = strLine.Split('\t');
                if (temp.Length >= 4)
                {
                    NameWeight weight = new NameWeight();
                    weight.strCompose = temp[1];
                    weight.len = int.Parse(temp[2]);
                    weight.weight = int.Parse(temp[3]);
                    weight.weightBegin = maxWeight;
                    maxWeight += weight.weight;
                    listNameWeight.Add(weight);
                }

            }


        }

        private int GetWeighIndex(int val)
        {
            if(listNameWeight == null)
            {
                return 0;
            }
            int len = listNameWeight.Count - 1;
            for (int i = 0; i < len; i++)
            {
                if (val >= listNameWeight[i].weightBegin && val < listNameWeight[i + 1].weightBegin)
                {
                    return i;
                }

            }
            return listNameWeight.Count - 1;
        }


    }

}

