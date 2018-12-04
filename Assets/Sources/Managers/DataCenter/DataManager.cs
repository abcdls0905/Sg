using System.Collections.Generic;
using GameUtil;
using System.Reflection;
using System;
using System.Collections;

namespace Game
{
    /* 大部分数据以Json/Csv/Scriptobject类型保存
    Json读取规则：
        1. Json文件保存于GameData\Resource\
        2. C#映射文件保存于Json\
        3. C#文件必须添加[Serializable]，成员变量必须为Public
    */
    public partial class DataManager : Singleton<DataManager>
    {
        private HashSet<IDataType> LoadedData;
        public override void Init()
        {
            LoadedData = new HashSet<IDataType>();
        }

        public override void UnInit()
        {
            UnLoadAll();
        }

        public void OnLoad(IDataType data)
        {
            LoadedData.Add(data);
        }

        public void OnUnload(IDataType data)
        {
            LoadedData.Remove(data);
        }
        public IEnumerator LoadAll()
        {
            var fields = typeof(DataManager).GetFields();
            foreach(var field in fields)
            {
                Type type = field.FieldType;
                var data = field.GetValue(this) as IDataType;
                if (data == null)
                    continue;
                data.Load();
                yield return Yielders.EndOfFrame;
            }
        }

        public void UnLoadAll()
        {
            var TempData = LoadedData;
            LoadedData = new HashSet<IDataType>();

            var iter = TempData.GetEnumerator();
            while(iter.MoveNext())
                iter.Current.Unload();
        }
    }
}