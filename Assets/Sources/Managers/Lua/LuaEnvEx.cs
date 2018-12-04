using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using EncryptLua;
using XLua;
using System;
using Game;

#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

namespace XLua
{
    public partial class LuaEnv
    {
        public static string luaUpdatePath = Application.persistentDataPath + "/assets/luabinfiles";
        public static string luaStreamPath = Application.streamingAssetsPath + "/luabinfiles";

        private List<string> searchPaths = new List<string>();
#if UNITY_ANDROID
        private List<Persist> _encrypts = new List<Persist>();
#else
        private List<PersistEx> _encrypts = new List<PersistEx>();
#endif
        private bool _loadFromFile;

        public void InitEx()
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            _loadFromFile = true;
#else
            _loadFromFile = false;
#endif
            // 在搜索完库文件后
            AddSearcher(Loader, 3);

            AddBuildin("pb", LoadPB);
            AddBuildin("cjson", LoadCJson);
            AddBuildin("protobuf.c", LoadProtobufC);
        }


        [MonoPInvokeCallback(typeof(LuaCSFunction))]
        internal static int LoadPB(RealStatePtr L)
        {
            return LuaAPI.luaopen_pb(L);
        }

        [MonoPInvokeCallback(typeof(LuaCSFunction))]
        internal static int LoadCJson(RealStatePtr L)
        {
            return LuaAPI.luaopen_cjson(L);
        }

        [MonoPInvokeCallback(typeof(LuaCSFunction))]
        internal static int LoadProtobufC(RealStatePtr L)
        {
            return LuaAPI.luaopen_protobuf_c(L);
        }

        [MonoPInvokeCallback(typeof(LuaCSFunction))]
        internal static int Loader(RealStatePtr L)
        {
            try
            {
                string fileName = LuaAPI.lua_tostring(L, 1);
                if (!Path.HasExtension(fileName))
                    fileName += ".lua";
                LuaEnv self = ObjectTranslatorPool.Instance.Find(L).luaEnv;
                self.LoadFiles(fileName);

                return 1;
            }
            catch (Exception e)
            {
                return LuaAPI.luaL_error(L, "c# exception in LoadBuiltinLib:" + e);
            }
        }

        public bool LoadFiles(string fileName)
        {
            bool hasFile = false;
            bool ret = false;

            if(_loadFromFile)
                ret = DirectLoad(L, fileName, out hasFile);
            if (!hasFile)
                ret = EncryptLoad(L, fileName);
            return ret; 
        }
        public void AddPath(string path)
        {
            AddBinaryPath(path.ToLower() + ".bin");
#if UNITY_EDITOR
            AddSearchPath(Path.Combine(Application.dataPath, path));
#else
            AddSearchPath(Path.Combine(Application.streamingAssetsPath, path));
#endif
        }

        private void AddBinaryPath(string path)
        {
            string fullPath = null;
            bool fileExist = false;
#if UNITY_ANDROID
            byte[] cache = null;
#endif
            if (GameEnvSetting.EnablePersistentPath)
            {
                fullPath = Path.Combine(luaUpdatePath, path);
#if !UNITY_ANDROID
                fileExist = (File.Exists(fullPath));
#else
                cache = Game.FileManager.ReadFileByWWW(fullPath);
                fileExist = (cache != null);
#endif
            }
            if (!fileExist)
            {
                fullPath = Path.Combine(luaStreamPath, path);
#if !UNITY_ANDROID
                fileExist = (File.Exists(fullPath));
#else
                cache = Game.FileManager.ReadFileByWWW(fullPath);
                fileExist = (cache != null);
#endif
            }

            if (!fileExist)
                return;

#if !UNITY_ANDROID
            _encrypts.Add(new PersistEx(fullPath));
#else
            _encrypts.Add(new Persist(fullPath, cache));
#endif
        }

        private bool AddSearchPath(string path, bool front = false)
        {
            if (path.Length > 0 && path[path.Length - 1] != '/')
            {
                path += "/";
            }

            int index = searchPaths.IndexOf(path);

            if (index >= 0)
            {
                return false;
            }

            if (front)
            {
                searchPaths.Insert(0, path);
            }
            else
            {
                searchPaths.Add(path);
            }

            return true;
        }

        public string GetFullPathFileName(string fileName)
        {
            if (fileName == string.Empty)
            {
                return string.Empty;
            }

            if (Path.IsPathRooted(fileName))
            {
                return fileName;
            }

            string fullPath = null;

            for (int i = 0; i < searchPaths.Count; i++)
            {
                fullPath = Path.Combine(searchPaths[i], fileName);

                if (File.Exists(fullPath))
                {
                    return fullPath;
                }
            }

            return null;
        }

        public byte[] LoadFile(ref string filepath)
        {
            string path = GetFullPathFileName(filepath);
            return Game.FileManager.ReadFile(path);
        }

        public bool EncryptLoad(IntPtr L, string fileName)
        {
            try
            {
                for (int i = 0; i < _encrypts.Count; ++i)
                {
                    int pos = 0, len = 0;
                    if (_encrypts[i].CheckFile(fileName, ref pos, ref len))
                    {
                        int ret = _encrypts[i].LoadFile(L, "@" + fileName, pos, len);
                        switch (ret)
                        {
                            case 0:
                                {
                                    return true;
                                }
                            case 1:
                                {
                                    LuaAPI.luaL_error(L, "error open file in load");
                                }
                                break;
                            case 2:
                                {
                                    LuaAPI.luaL_error(L, "error read file in load");
                                }
                                break;
                            case 3:
                                {
                                    LuaAPI.luaL_error(L, "error decrpt in load");
                                }
                                break;
                            case 4:
                                {
                                    LuaAPI.luaL_error(L, String.Format("error loading module {0}, {1}",
                                        LuaAPI.lua_tostring(L, 1), LuaAPI.lua_tostring(L, -1)));
                                }
                                break;
                            default:
                                {
                                    LuaAPI.luaL_error(L, "error unknow in load");
                                }
                                break;
                        }
                        return false;
                    }
                }
                LuaAPI.luaL_error(L, string.Format(
                    "no such file '{0}'!", fileName));
            }
            catch (Exception e)
            {
                LuaAPI.luaL_error(L, "c# exception in load:" + e);
            }
            return false;
        }

        public bool DirectLoad(IntPtr L, string fileName, out bool hasFile)
        {
            hasFile = true;
            try
            {
                var buffer = LoadFile(ref fileName);
                if (buffer != null)
                {
                    if (LuaAPI.xluaL_loadbuffer(L, buffer, buffer.Length, "@" + fileName) == 0)
                        return true;
                    LuaAPI.luaL_error(L, String.Format("error loading module {0}, {1}",
                            LuaAPI.lua_tostring(L, 1), LuaAPI.lua_tostring(L, -1)));
                }
                else
                {
                    hasFile = false;
                    return false;
                }
            }
            catch (System.Exception e)
            {
                LuaAPI.luaL_error(L, "c# exception in load:" + e);
            }
            return false;
        }

        public void DoFile(string fileName, LuaTable env = null)
        {
#if THREAD_SAFE || HOTFIX_ENABLE
            lock (luaEnvLock)
            {
#endif
                if (!Path.HasExtension(fileName))
                    fileName += ".lua";

                var _L = L;
                int oldTop = LuaAPI.lua_gettop(_L);
                int errFunc = LuaAPI.load_error_func(_L, errorFuncRef);

                //bool ret = _loadFromFile ? DirectLoad(L, fileName) : EncryptLoad(L, fileName);
                bool ret = LoadFiles(fileName);

                if (ret)
                {
                    if (env != null)
                    {
                        env.push(_L);
                        LuaAPI.lua_setfenv(_L, -2);
                    }

                    if (LuaAPI.lua_pcall(_L, 0, -1, errFunc) == 0)
                        LuaAPI.lua_remove(_L, errFunc);
                    else
                        ThrowExceptionFromError(oldTop);
                }
                else
                    ThrowExceptionFromError(oldTop);

#if THREAD_SAFE || HOTFIX_ENABLE
            }
#endif
        }
    }
}
