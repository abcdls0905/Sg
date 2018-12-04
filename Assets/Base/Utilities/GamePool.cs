using System;
using System.Collections.Generic;
namespace GameUtil
{
    public class SimplePool<T> where T : new()
    {
        private static List<T> _pool = new List<T>();
        public static T New() {
            if(_pool.Count > 0)
            {
                int i = _pool.Count - 1;
                T obj = _pool[i];
                _pool.RemoveAt(i);
                return obj;
            }
            return new T();
        }
        public static void Reclaim(T t)
        {
            _pool.Add(t);
        }
        public static void Clear()
        {
            _pool.Clear();
        }
    }

    public class GamePools<KEY, TYPE>
    {
        public const int DEFAULTLISTNUM = 4;

        public Dictionary<KEY, List<TYPE>> _pools = new Dictionary<KEY, List<TYPE>>();

        public T New<T>(KEY key) where T : TYPE, new()
        {
            List<TYPE> pool = NewPool(key);
            if (pool.Count > 0)
            {
                int i = pool.Count - 1;
                T obj = (T)pool[i];
                pool.RemoveAt(i);
                return obj;
            }
            else
            {
                return new T();
            }
        }

        public TYPE New(KEY key, Type type)
        {
            List<TYPE> pool = NewPool(key);
            if (pool.Count > 0)
            {
                int i = pool.Count - 1;
                TYPE obj = pool[i];
                pool.RemoveAt(i);
                return obj;
            }
            else
            {
                return (TYPE)Activator.CreateInstance(type);
            }
        }

        public void Reclaim(KEY key, TYPE type)
        {
            List<TYPE> pool = NewPool(key);
            pool.Add(type);
        }

        public List<TYPE> NewPool(KEY key)
        {
            List<TYPE> pool;
            if (!_pools.TryGetValue(key, out pool))
            {
                pool = new List<TYPE>(DEFAULTLISTNUM);
                _pools[key] = pool;
            }
            return pool;
        }

        public void Clear()
        {
            _pools.Clear();
        }
    }
}