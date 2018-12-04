using Pathfinding.Serialization.JsonFx;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class JsonDataType<T> : BaseDataType<T>
    {
        public static JsonReaderSettings setting;
        public JsonDataType(string name) : base(name + ".json")
        {
        }
        public override T Serialize(byte[] binary)
        {
            //try
            //{
                if(setting == null)
                {
                    setting = new JsonReaderSettings();
                    setting.AddTypeConverter(new VectorConverter());
                }
                JsonReader reader = new JsonReader(System.Text.Encoding.UTF8.GetString(binary), setting);
                T obj = (T)reader.Deserialize(0, typeof(T));
                return obj;
            //}
            //catch (Exception e)
            //{
            //    Debug.LogErrorFormat("SerializeJson Error, Path:{0}, Error:{1}", DataPath, e);
            //    return default(T);
            //}
        }
    }
}