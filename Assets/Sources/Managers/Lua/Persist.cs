using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

namespace EncryptLua
{
    class EncryptDll
    {
#if !UNITY_EDITOR && UNITY_IPHONE
        const string ENCRYPTDLL = "__Internal";
#else
        const string ENCRYPTDLL = "xlua";
#endif

        [DllImport(ENCRYPTDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern System.IntPtr NewFileEntry(byte[] buf, int bufsize);

        [DllImport(ENCRYPTDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void FreeFileEntry(System.IntPtr entry);

        [DllImport(ENCRYPTDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool GetFileEntry(System.IntPtr entry, string name, ref int pos, ref int len);

        [DllImport(ENCRYPTDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool DecryptBuffer(byte[] data, int len, byte[] outdata, ref int outsize);

        [DllImport(ENCRYPTDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int DecryptBufferEx(System.IntPtr entry, System.IntPtr L, string path, string fileName, int pos, int len);

        [DllImport(ENCRYPTDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern System.IntPtr NewFileEntryEx(string path);
    }

    public class Persist
    {
        IntPtr _entry = IntPtr.Zero;
        string _path = string.Empty;
        byte[] _cache = null;

        ~Persist()
        {
            if (_entry != IntPtr.Zero)
                EncryptDll.FreeFileEntry(_entry);
        }

        public Persist(string path, byte[] cache)
        {
            _path = path;
            _cache = cache;
            _entry = EncryptDll.NewFileEntry(_cache, _cache.Length);
        }

        public bool CheckFile(string filename, ref int pos, ref int len)
        {
            if (_entry == IntPtr.Zero)
                return false;
            if (!EncryptDll.GetFileEntry(_entry, filename, ref pos, ref len))
                return false;
            return true;
        }

        private byte[] GetCipherByCache(int pos, int len)
        {
            if (_cache == null)
                return null;

            byte[] cipher = new byte[len];
            int available = _cache.Length - pos;
            int realLen = (available > len) ? len : available;

            int idx = pos;
            for (int i = 0; i < realLen; i++)
            {
                cipher[i] = _cache[idx++];
            }

            return cipher;
        }

        private byte[] DecryptBuffer(byte[] cipher, int len, string filename)
        {
            int i = len * 10;
            for (int idx = 1; idx < 10; idx++, i *= 2)
            {
                int plainSize = i;
                byte[] plain = new byte[plainSize];
                if (!EncryptDll.DecryptBuffer(cipher, cipher.Length, plain, ref plainSize))
                {
                    if (plainSize <= i)
                    {
                        Debug.LogError("Read fail 2 !" + filename);
                        return null;
                    }
                    continue;
                }
                byte[] str = new byte[plainSize];
                System.Array.Copy(plain, str, plainSize);
                return str;
            }
            Debug.LogError("Read fail 3 !" + filename);
            return null;
        }

        // 错误类型
        // 0 : 成功
        // 1：打开文件失败
        // 2：读取文件失败
        // 3：解压文件失败
        // 4：加载Lua失败
        //#if UNITY_ANDROID
        public int LoadFile(IntPtr L, string fileName, int pos, int len)
        {
            if (_entry == IntPtr.Zero)
                return 1;
            //#if UNITY_ANDROID
            byte[] cipher = GetCipherByCache(pos, len);
            if (cipher == null)
                return 1;
            byte[] buffer = DecryptBuffer(cipher, len, fileName);
            if (buffer == null)
                return 3;
            if (XLua.LuaDLL.Lua.xluaL_loadbuffer(L, buffer, buffer.Length, "@" + fileName) == 0)
                return 0;
            return 4;
        }
    }


    public class PersistEx
    {
        IntPtr _entry = IntPtr.Zero;
        string _path = string.Empty;

        ~PersistEx()
        {
            if (_entry != IntPtr.Zero)
                EncryptDll.FreeFileEntry(_entry);
        }

        public PersistEx(string path)
        {
            _path = path;
            _entry = EncryptDll.NewFileEntryEx(path);
        }

        public bool CheckFile(string filename, ref int pos, ref int len)
        {
            if (_entry == IntPtr.Zero)
                return false;
            if (!EncryptDll.GetFileEntry(_entry, filename, ref pos, ref len))
                return false;
            return true;
        }

        // 错误类型
        // 0 : 成功
        // 1：打开文件失败
        // 2：读取文件失败
        // 3：解压文件失败
        // 4：加载Lua失败
        //#if UNITY_ANDROID
        public int LoadFile(IntPtr L, string fileName, int pos, int len)
        {
            if (_entry == IntPtr.Zero)
                return 1;
            return EncryptDll.DecryptBufferEx(_entry, L, _path, fileName, pos, len);
        }
    }
}
