#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class SystemIOFileWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(System.IO.File);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 35, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "AppendAllText", _m_AppendAllText_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "AppendText", _m_AppendText_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Copy", _m_Copy_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateText", _m_CreateText_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Delete", _m_Delete_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Exists", _m_Exists_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetAttributes", _m_GetAttributes_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetCreationTime", _m_GetCreationTime_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetCreationTimeUtc", _m_GetCreationTimeUtc_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetLastAccessTime", _m_GetLastAccessTime_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetLastAccessTimeUtc", _m_GetLastAccessTimeUtc_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetLastWriteTime", _m_GetLastWriteTime_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetLastWriteTimeUtc", _m_GetLastWriteTimeUtc_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Move", _m_Move_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Open", _m_Open_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "OpenRead", _m_OpenRead_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "OpenText", _m_OpenText_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "OpenWrite", _m_OpenWrite_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Replace", _m_Replace_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetAttributes", _m_SetAttributes_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetCreationTime", _m_SetCreationTime_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetCreationTimeUtc", _m_SetCreationTimeUtc_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetLastAccessTime", _m_SetLastAccessTime_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetLastAccessTimeUtc", _m_SetLastAccessTimeUtc_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetLastWriteTime", _m_SetLastWriteTime_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetLastWriteTimeUtc", _m_SetLastWriteTimeUtc_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadAllBytes", _m_ReadAllBytes_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadAllLines", _m_ReadAllLines_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadAllText", _m_ReadAllText_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "WriteAllBytes", _m_WriteAllBytes_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "WriteAllLines", _m_WriteAllLines_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "WriteAllText", _m_WriteAllText_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Encrypt", _m_Encrypt_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Decrypt", _m_Decrypt_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "System.IO.File does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AppendAllText_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    string contents = LuaAPI.lua_tostring(L, 2);
                    
                    System.IO.File.AppendAllText( path, contents );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.Encoding>(L, 3)) 
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    string contents = LuaAPI.lua_tostring(L, 2);
                    System.Text.Encoding encoding = (System.Text.Encoding)translator.GetObject(L, 3, typeof(System.Text.Encoding));
                    
                    System.IO.File.AppendAllText( path, contents, encoding );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.File.AppendAllText!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AppendText_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        System.IO.StreamWriter __cl_gen_ret = System.IO.File.AppendText( path );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Copy_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string sourceFileName = LuaAPI.lua_tostring(L, 1);
                    string destFileName = LuaAPI.lua_tostring(L, 2);
                    
                    System.IO.File.Copy( sourceFileName, destFileName );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    string sourceFileName = LuaAPI.lua_tostring(L, 1);
                    string destFileName = LuaAPI.lua_tostring(L, 2);
                    bool overwrite = LuaAPI.lua_toboolean(L, 3);
                    
                    System.IO.File.Copy( sourceFileName, destFileName, overwrite );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.File.Copy!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateText_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        System.IO.StreamWriter __cl_gen_ret = System.IO.File.CreateText( path );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Delete_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                    System.IO.File.Delete( path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Exists_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        bool __cl_gen_ret = System.IO.File.Exists( path );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetAttributes_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        System.IO.FileAttributes __cl_gen_ret = System.IO.File.GetAttributes( path );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCreationTime_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        System.DateTime __cl_gen_ret = System.IO.File.GetCreationTime( path );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCreationTimeUtc_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        System.DateTime __cl_gen_ret = System.IO.File.GetCreationTimeUtc( path );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLastAccessTime_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        System.DateTime __cl_gen_ret = System.IO.File.GetLastAccessTime( path );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLastAccessTimeUtc_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        System.DateTime __cl_gen_ret = System.IO.File.GetLastAccessTimeUtc( path );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLastWriteTime_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        System.DateTime __cl_gen_ret = System.IO.File.GetLastWriteTime( path );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLastWriteTimeUtc_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        System.DateTime __cl_gen_ret = System.IO.File.GetLastWriteTimeUtc( path );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Move_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string sourceFileName = LuaAPI.lua_tostring(L, 1);
                    string destFileName = LuaAPI.lua_tostring(L, 2);
                    
                    System.IO.File.Move( sourceFileName, destFileName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Open_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.IO.FileMode>(L, 2)) 
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    System.IO.FileMode mode;translator.Get(L, 2, out mode);
                    
                        System.IO.FileStream __cl_gen_ret = System.IO.File.Open( path, mode );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.IO.FileMode>(L, 2)&& translator.Assignable<System.IO.FileAccess>(L, 3)) 
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    System.IO.FileMode mode;translator.Get(L, 2, out mode);
                    System.IO.FileAccess access;translator.Get(L, 3, out access);
                    
                        System.IO.FileStream __cl_gen_ret = System.IO.File.Open( path, mode, access );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.IO.FileMode>(L, 2)&& translator.Assignable<System.IO.FileAccess>(L, 3)&& translator.Assignable<System.IO.FileShare>(L, 4)) 
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    System.IO.FileMode mode;translator.Get(L, 2, out mode);
                    System.IO.FileAccess access;translator.Get(L, 3, out access);
                    System.IO.FileShare share;translator.Get(L, 4, out share);
                    
                        System.IO.FileStream __cl_gen_ret = System.IO.File.Open( path, mode, access, share );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.File.Open!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenRead_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        System.IO.FileStream __cl_gen_ret = System.IO.File.OpenRead( path );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenText_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        System.IO.StreamReader __cl_gen_ret = System.IO.File.OpenText( path );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenWrite_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        System.IO.FileStream __cl_gen_ret = System.IO.File.OpenWrite( path );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Replace_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    string sourceFileName = LuaAPI.lua_tostring(L, 1);
                    string destinationFileName = LuaAPI.lua_tostring(L, 2);
                    string destinationBackupFileName = LuaAPI.lua_tostring(L, 3);
                    
                    System.IO.File.Replace( sourceFileName, destinationFileName, destinationBackupFileName );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    string sourceFileName = LuaAPI.lua_tostring(L, 1);
                    string destinationFileName = LuaAPI.lua_tostring(L, 2);
                    string destinationBackupFileName = LuaAPI.lua_tostring(L, 3);
                    bool ignoreMetadataErrors = LuaAPI.lua_toboolean(L, 4);
                    
                    System.IO.File.Replace( sourceFileName, destinationFileName, destinationBackupFileName, ignoreMetadataErrors );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.File.Replace!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAttributes_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    System.IO.FileAttributes fileAttributes;translator.Get(L, 2, out fileAttributes);
                    
                    System.IO.File.SetAttributes( path, fileAttributes );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCreationTime_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    System.DateTime creationTime;translator.Get(L, 2, out creationTime);
                    
                    System.IO.File.SetCreationTime( path, creationTime );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCreationTimeUtc_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    System.DateTime creationTimeUtc;translator.Get(L, 2, out creationTimeUtc);
                    
                    System.IO.File.SetCreationTimeUtc( path, creationTimeUtc );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLastAccessTime_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    System.DateTime lastAccessTime;translator.Get(L, 2, out lastAccessTime);
                    
                    System.IO.File.SetLastAccessTime( path, lastAccessTime );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLastAccessTimeUtc_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    System.DateTime lastAccessTimeUtc;translator.Get(L, 2, out lastAccessTimeUtc);
                    
                    System.IO.File.SetLastAccessTimeUtc( path, lastAccessTimeUtc );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLastWriteTime_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    System.DateTime lastWriteTime;translator.Get(L, 2, out lastWriteTime);
                    
                    System.IO.File.SetLastWriteTime( path, lastWriteTime );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLastWriteTimeUtc_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    System.DateTime lastWriteTimeUtc;translator.Get(L, 2, out lastWriteTimeUtc);
                    
                    System.IO.File.SetLastWriteTimeUtc( path, lastWriteTimeUtc );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadAllBytes_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        byte[] __cl_gen_ret = System.IO.File.ReadAllBytes( path );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadAllLines_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        string[] __cl_gen_ret = System.IO.File.ReadAllLines( path );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.Encoding>(L, 2)) 
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    System.Text.Encoding encoding = (System.Text.Encoding)translator.GetObject(L, 2, typeof(System.Text.Encoding));
                    
                        string[] __cl_gen_ret = System.IO.File.ReadAllLines( path, encoding );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.File.ReadAllLines!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadAllText_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = System.IO.File.ReadAllText( path );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.Encoding>(L, 2)) 
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    System.Text.Encoding encoding = (System.Text.Encoding)translator.GetObject(L, 2, typeof(System.Text.Encoding));
                    
                        string __cl_gen_ret = System.IO.File.ReadAllText( path, encoding );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.File.ReadAllText!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteAllBytes_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    byte[] bytes = LuaAPI.lua_tobytes(L, 2);
                    
                    System.IO.File.WriteAllBytes( path, bytes );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteAllLines_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<string[]>(L, 2)) 
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    string[] contents = (string[])translator.GetObject(L, 2, typeof(string[]));
                    
                    System.IO.File.WriteAllLines( path, contents );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<string[]>(L, 2)&& translator.Assignable<System.Text.Encoding>(L, 3)) 
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    string[] contents = (string[])translator.GetObject(L, 2, typeof(string[]));
                    System.Text.Encoding encoding = (System.Text.Encoding)translator.GetObject(L, 3, typeof(System.Text.Encoding));
                    
                    System.IO.File.WriteAllLines( path, contents, encoding );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.File.WriteAllLines!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteAllText_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    string contents = LuaAPI.lua_tostring(L, 2);
                    
                    System.IO.File.WriteAllText( path, contents );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.Encoding>(L, 3)) 
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    string contents = LuaAPI.lua_tostring(L, 2);
                    System.Text.Encoding encoding = (System.Text.Encoding)translator.GetObject(L, 3, typeof(System.Text.Encoding));
                    
                    System.IO.File.WriteAllText( path, contents, encoding );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.File.WriteAllText!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Encrypt_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                    System.IO.File.Encrypt( path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Decrypt_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                    System.IO.File.Decrypt( path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
