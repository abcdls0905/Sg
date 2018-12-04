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
    public class GameFileManagerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Game.FileManager);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 30, 2, 2);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "IsFileExist", _m_IsFileExist_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsDirectoryExist", _m_IsDirectoryExist_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateDirectory", _m_CreateDirectory_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DeleteDirectory", _m_DeleteDirectory_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetFileLength", _m_GetFileLength_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadFile", _m_ReadFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadFileByWWW", _m_ReadFileByWWW_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadFileByDirect", _m_ReadFileByDirect_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "WriteFile", _m_WriteFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DeleteFile", _m_DeleteFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CopyFile", _m_CopyFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetFileMd5", _m_GetFileMd5_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetMd5", _m_GetMd5_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CombinePath", _m_CombinePath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CombinePaths", _m_CombinePaths_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetStreamingAssetsPathWithHeader", _m_GetStreamingAssetsPathWithHeader_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetCachePath", _m_GetCachePath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetIFSExtractPath", _m_GetIFSExtractPath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetFullName", _m_GetFullName_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "EraseExtension", _m_EraseExtension_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "AddExtension", _m_AddExtension_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetExtension", _m_GetExtension_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetFullDirectory", _m_GetFullDirectory_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ClearDirectory", _m_ClearDirectory_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CopyDirectory", _m_CopyDirectory_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CopyFiles", _m_CopyFiles_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SearchResFiles", _m_SearchResFiles_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SaveToFile", _m_SaveToFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "UnZipFile", _m_UnZipFile_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "s_ifsExtractFolder", _g_get_s_ifsExtractFolder);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "s_delegateOnOperateFileFail", _g_get_s_delegateOnOperateFileFail);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "s_ifsExtractFolder", _s_set_s_ifsExtractFolder);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "s_delegateOnOperateFileFail", _s_set_s_delegateOnOperateFileFail);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "Game.FileManager does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsFileExist_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string filePath = LuaAPI.lua_tostring(L, 1);
                    
                        bool __cl_gen_ret = Game.FileManager.IsFileExist( filePath );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsDirectoryExist_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string directory = LuaAPI.lua_tostring(L, 1);
                    
                        bool __cl_gen_ret = Game.FileManager.IsDirectoryExist( directory );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateDirectory_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string directory = LuaAPI.lua_tostring(L, 1);
                    
                        bool __cl_gen_ret = Game.FileManager.CreateDirectory( directory );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DeleteDirectory_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string directory = LuaAPI.lua_tostring(L, 1);
                    
                        bool __cl_gen_ret = Game.FileManager.DeleteDirectory( directory );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFileLength_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string filePath = LuaAPI.lua_tostring(L, 1);
                    
                        int __cl_gen_ret = Game.FileManager.GetFileLength( filePath );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    string filePath = LuaAPI.lua_tostring(L, 1);
                    bool persist = LuaAPI.lua_toboolean(L, 2);
                    
                        byte[] __cl_gen_ret = Game.FileManager.ReadFile( filePath, persist );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string filePath = LuaAPI.lua_tostring(L, 1);
                    
                        byte[] __cl_gen_ret = Game.FileManager.ReadFile( filePath );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Game.FileManager.ReadFile!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadFileByWWW_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string filePath = LuaAPI.lua_tostring(L, 1);
                    
                        byte[] __cl_gen_ret = Game.FileManager.ReadFileByWWW( filePath );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadFileByDirect_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string filePath = LuaAPI.lua_tostring(L, 1);
                    
                        byte[] __cl_gen_ret = Game.FileManager.ReadFileByDirect( filePath );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string filePath = LuaAPI.lua_tostring(L, 1);
                    byte[] data = LuaAPI.lua_tobytes(L, 2);
                    
                        bool __cl_gen_ret = Game.FileManager.WriteFile( filePath, data );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    string filePath = LuaAPI.lua_tostring(L, 1);
                    byte[] data = LuaAPI.lua_tobytes(L, 2);
                    int offset = LuaAPI.xlua_tointeger(L, 3);
                    int length = LuaAPI.xlua_tointeger(L, 4);
                    
                        bool __cl_gen_ret = Game.FileManager.WriteFile( filePath, data, offset, length );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Game.FileManager.WriteFile!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DeleteFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string filePath = LuaAPI.lua_tostring(L, 1);
                    
                        bool __cl_gen_ret = Game.FileManager.DeleteFile( filePath );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CopyFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string srcFile = LuaAPI.lua_tostring(L, 1);
                    string dstFile = LuaAPI.lua_tostring(L, 2);
                    
                    Game.FileManager.CopyFile( srcFile, dstFile );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFileMd5_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string filePath = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = Game.FileManager.GetFileMd5( filePath );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetMd5_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    byte[] data = LuaAPI.lua_tobytes(L, 1);
                    
                        string __cl_gen_ret = Game.FileManager.GetMd5( data );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string str = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = Game.FileManager.GetMd5( str );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Game.FileManager.GetMd5!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CombinePath_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string path1 = LuaAPI.lua_tostring(L, 1);
                    string path2 = LuaAPI.lua_tostring(L, 2);
                    
                        string __cl_gen_ret = Game.FileManager.CombinePath( path1, path2 );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CombinePaths_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string[] values = translator.GetParams<string>(L, 1);
                    
                        string __cl_gen_ret = Game.FileManager.CombinePaths( values );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetStreamingAssetsPathWithHeader_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string fileName = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = Game.FileManager.GetStreamingAssetsPathWithHeader( fileName );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCachePath_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 0) 
                {
                    
                        string __cl_gen_ret = Game.FileManager.GetCachePath(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string fileName = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = Game.FileManager.GetCachePath( fileName );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Game.FileManager.GetCachePath!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetIFSExtractPath_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string __cl_gen_ret = Game.FileManager.GetIFSExtractPath(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFullName_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string fullPath = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = Game.FileManager.GetFullName( fullPath );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EraseExtension_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string fullName = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = Game.FileManager.EraseExtension( fullName );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddExtension_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string fullName = LuaAPI.lua_tostring(L, 1);
                    string extension = LuaAPI.lua_tostring(L, 2);
                    
                        string __cl_gen_ret = Game.FileManager.AddExtension( fullName, extension );
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
                    string fullName = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = Game.FileManager.GetExtension( fullName );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFullDirectory_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string fullPath = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = Game.FileManager.GetFullDirectory( fullPath );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearDirectory_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string fullPath = LuaAPI.lua_tostring(L, 1);
                    
                        bool __cl_gen_ret = Game.FileManager.ClearDirectory( fullPath );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<string[]>(L, 2)&& translator.Assignable<string[]>(L, 3)) 
                {
                    string fullPath = LuaAPI.lua_tostring(L, 1);
                    string[] fileExtensionFilter = (string[])translator.GetObject(L, 2, typeof(string[]));
                    string[] folderFilter = (string[])translator.GetObject(L, 3, typeof(string[]));
                    
                        bool __cl_gen_ret = Game.FileManager.ClearDirectory( fullPath, fileExtensionFilter, folderFilter );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Game.FileManager.ClearDirectory!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CopyDirectory_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<Game.FileManager.FileFilter>(L, 3)) 
                {
                    string srcPath = LuaAPI.lua_tostring(L, 1);
                    string tarPath = LuaAPI.lua_tostring(L, 2);
                    Game.FileManager.FileFilter filter = translator.GetDelegate<Game.FileManager.FileFilter>(L, 3);
                    
                    Game.FileManager.CopyDirectory( srcPath, tarPath, filter );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string srcPath = LuaAPI.lua_tostring(L, 1);
                    string tarPath = LuaAPI.lua_tostring(L, 2);
                    
                    Game.FileManager.CopyDirectory( srcPath, tarPath );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Game.FileManager.CopyDirectory!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CopyFiles_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string srcPath = LuaAPI.lua_tostring(L, 1);
                    string tarPath = LuaAPI.lua_tostring(L, 2);
                    Game.FileManager.FileFilter filter = translator.GetDelegate<Game.FileManager.FileFilter>(L, 3);
                    
                    Game.FileManager.CopyFiles( srcPath, tarPath, filter );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SearchResFiles_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        string[] __cl_gen_ret = Game.FileManager.SearchResFiles( path );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SaveToFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    byte[] bytes = LuaAPI.lua_tobytes(L, 2);
                    
                    Game.FileManager.SaveToFile( path, bytes );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UnZipFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string fileToUnZip = LuaAPI.lua_tostring(L, 1);
                    string zipedFolder = LuaAPI.lua_tostring(L, 2);
                    
                        bool __cl_gen_ret = Game.FileManager.UnZipFile( fileToUnZip, zipedFolder );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_s_ifsExtractFolder(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Game.FileManager.s_ifsExtractFolder);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_s_delegateOnOperateFileFail(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, Game.FileManager.s_delegateOnOperateFileFail);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_s_ifsExtractFolder(RealStatePtr L)
        {
		    try {
                
			    Game.FileManager.s_ifsExtractFolder = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_s_delegateOnOperateFileFail(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    Game.FileManager.s_delegateOnOperateFileFail = translator.GetDelegate<Game.FileManager.DelegateOnOperateFileFail>(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
