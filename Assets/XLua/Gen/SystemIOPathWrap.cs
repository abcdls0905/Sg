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
    public class SystemIOPathWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(System.IO.Path);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 20, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "ChangeExtension", _m_ChangeExtension_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Combine", _m_Combine_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetDirectoryName", _m_GetDirectoryName_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetExtension", _m_GetExtension_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetFileName", _m_GetFileName_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetFileNameWithoutExtension", _m_GetFileNameWithoutExtension_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetFullPath", _m_GetFullPath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetPathRoot", _m_GetPathRoot_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetTempFileName", _m_GetTempFileName_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetTempPath", _m_GetTempPath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "HasExtension", _m_HasExtension_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsPathRooted", _m_IsPathRooted_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetInvalidFileNameChars", _m_GetInvalidFileNameChars_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetInvalidPathChars", _m_GetInvalidPathChars_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetRandomFileName", _m_GetRandomFileName_xlua_st_);
            
			
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "AltDirectorySeparatorChar", System.IO.Path.AltDirectorySeparatorChar);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "DirectorySeparatorChar", System.IO.Path.DirectorySeparatorChar);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PathSeparator", System.IO.Path.PathSeparator);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "VolumeSeparatorChar", System.IO.Path.VolumeSeparatorChar);
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "System.IO.Path does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ChangeExtension_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    string extension = LuaAPI.lua_tostring(L, 2);
                    
                        string __cl_gen_ret = System.IO.Path.ChangeExtension( path, extension );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Combine_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string path1 = LuaAPI.lua_tostring(L, 1);
                    string path2 = LuaAPI.lua_tostring(L, 2);
                    
                        string __cl_gen_ret = System.IO.Path.Combine( path1, path2 );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetDirectoryName_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = System.IO.Path.GetDirectoryName( path );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetExtension_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = System.IO.Path.GetExtension( path );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFileName_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = System.IO.Path.GetFileName( path );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFileNameWithoutExtension_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = System.IO.Path.GetFileNameWithoutExtension( path );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFullPath_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = System.IO.Path.GetFullPath( path );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPathRoot_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = System.IO.Path.GetPathRoot( path );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTempFileName_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string __cl_gen_ret = System.IO.Path.GetTempFileName(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTempPath_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string __cl_gen_ret = System.IO.Path.GetTempPath(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HasExtension_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        bool __cl_gen_ret = System.IO.Path.HasExtension( path );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsPathRooted_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        bool __cl_gen_ret = System.IO.Path.IsPathRooted( path );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetInvalidFileNameChars_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        char[] __cl_gen_ret = System.IO.Path.GetInvalidFileNameChars(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetInvalidPathChars_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        char[] __cl_gen_ret = System.IO.Path.GetInvalidPathChars(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetRandomFileName_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string __cl_gen_ret = System.IO.Path.GetRandomFileName(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
