using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game
{
    public class BaseDataType<T> : IDataType
    {
        public bool IsLoad { get; set; }
        public string DataPath { get; set; }
        private T data;
        public T Data {
            get
            {
                if (!IsLoad)
                    Load();
                return data;
            }
        }
        public BaseDataType(string name)
        {
            IsLoad = false;
            DataPath = name;
        }
        public virtual T Serialize(byte[] binary)
        {
            return default(T);
        }
        public void Load()
        {
            if (IsLoad)
                return;
            IsLoad = true;
            var bytes = GameInterface.ReadGameData(DataPath);
            if (bytes == null)
                return;
            data = Serialize(bytes);
            DataManager.Instance.OnLoad(this);
        }
        public void Unload()
        {
            if (!IsLoad)
                return;
            IsLoad = false;
            data = default(T);
            DataManager.Instance.OnUnload(this);
        }
    }
}