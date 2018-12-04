using System.Collections.Generic;

namespace GameNetwork
{
    // 此处考虑复用body，减少内存分配，如：按照2, 4, 8, 16, 32，64，128，256，512，1024，2048，4096 分配内存池pool[12], 池最多为10。
    // 总分配内存大小为：81900字节
    public static class BytesCache
    {
        static object lockPool = new object();

        public class BytesInfo
        {
            public BytesInfo(int poolSize, int byteSize)
            {
                this.pool = new List<byte[]>(poolSize);
                this.byteSize = byteSize;
            }
            public List<byte[]> pool;
            public int byteSize;
            public byte[] Pop()
            {
                if (pool.Count > 0)
                {
                    byte[] bytes = pool[pool.Count - 1];
                    pool.RemoveAt(pool.Count - 1);
                    return bytes;
                }
                else
                    return new byte[byteSize];
            }
            public void Push(byte[] bytes)
            {
                if (pool.Count >= pool.Capacity)
                    return;
                pool.Add(bytes);
            }
        }

        static BytesInfo[] cacheBytes;
        public static int DefaultSize = 12;
        public static int DefaultPoolSize = 2;

        static BytesCache()
        {
            Init(DefaultSize);
        }

        public static void Init(int maxSize)
        {
            lock (lockPool)
            {
                cacheBytes = new BytesInfo[maxSize];

                int size = 1;
                for (int i = 0; i < maxSize; ++i)
                {
                    size = size * 2;
                    cacheBytes[i] = new BytesInfo(DefaultPoolSize, size);
                    for (int j = 0; j < DefaultPoolSize; ++j)
                        cacheBytes[i].pool.Add(new byte[size]);
                }
            }
        }

        public static byte[] New(int size)
        {
            lock (lockPool)
            {
                int temp = size - 1;
                int idx = -1;
                while (temp > 0)
                {
                    temp >>= 1;
                    ++idx;
                }
                if (temp < 0)
                    temp = 0;
                if (idx < 0 || cacheBytes == null || idx >= cacheBytes.Length)
                    return new byte[size];
                else
                    return cacheBytes[idx].Pop();
            }
        }

        public static void Reclaim(byte[] bytes)
        {
            lock (lockPool)
            {
                if (bytes == null || cacheBytes == null)
                    return;
                int len = bytes.Length;
                for (int i = cacheBytes.Length - 1; i >= 0; --i)
                {
                    int byteSize = cacheBytes[i].byteSize;
                    if (byteSize < len)
                        return;
                    if (byteSize == len)
                    {
                        cacheBytes[i].Push(bytes);
                        return;
                    }
                }
            }
        }
    }

    public static class NetPackPool<T> where T : INetPack, new()
    {
        static List<T> pool;
        public static int DefaultPoolSize = 10;
        static object lockPool = new object();

        static NetPackPool()
        { 
            pool = new List<T>(DefaultPoolSize);
        }

        public static T New()
        {
            lock (lockPool)
            {
                if (pool.Count > 0)
                {
                    T pack = pool[pool.Count - 1];
                    pool.RemoveAt(pool.Count - 1);
                    return pack;
                }
                else
                    return new T();
            }
        }

        public static void Reclaim(T pack)
        {
            lock (lockPool)
            {
                if (pool.Count >= DefaultPoolSize)
                    return;
                pool.Add(pack);
                pack.Reset();
            }
        }
    }
}
