
using System.Collections.Generic;
using System;

namespace GameUtil
{
    public class CacheBase
    {
        public virtual void OnInit()
        {

        }

        public virtual void OnDestroy()
        {

        }

        public virtual void Reset()
        {

        }

        public void Destroy()
        {
            OnDestroy();
            //CacheManager.Instance.Cache(this);
        }
    }
    public class CacheManager : MonoSingleton<CacheManager>
    {
        private const int size = 4;
        System.Collections.Generic.Dictionary<Type, List<CacheBase>> caches = new Dictionary<Type, List<CacheBase>>();
        public void RegisterCache(System.Type type)
        {
            List<CacheBase> list = null;
            if (!caches.TryGetValue(type, out list))
            {
                list = new List<CacheBase>(size);
                caches.Add(type, list);
            }
        }

        public void Push(CacheBase obj)
        {
            System.Type type = obj.GetType();
            List<CacheBase> list = null;
            if (caches.TryGetValue(type, out list))
                list.Add(obj);
            else
            {
                list = new List<CacheBase>(size);
                caches.Add(type, list);
                list.Add(obj);
            }
        }

        public T Pop<T>() where T : CacheBase, new()
        {
            List<CacheBase> list = null;
            Type type = typeof(T);
            if (caches.TryGetValue(type, out list))
            {
                if (list.Count > 0)
                {
                    T obj = (T)list[0];
                    list.RemoveAt(0);
                    return obj;
                }
                return new T();
            }
            list = new List<CacheBase>(size);
            caches.Add(type, list);
            return new T();
        }

        public void PreProtoBufCache()
        {
            System.Type type = typeof(Pb.C2B_PlayerCommand.Types);
            System.Type[] nestedTypes = type.GetNestedTypes();
            for (int i = 0; i < nestedTypes.Length; i++)
            {
                System.Type nestedType = nestedTypes[i];
                Google.Protobuf.ProtobufManager.New(nestedType);
            }
        }

        public override void UnInit()
        {
            var iter = caches.GetEnumerator();
            while (iter.MoveNext())
            {
                var value = iter.Current.Value;
                for (int i = 0; i < value.Count; i++)
                {
                    value[i].Destroy();
                }
            }
            caches.Clear();
        }
    }
}