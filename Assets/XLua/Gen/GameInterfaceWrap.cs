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
    public class GameInterfaceWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(GameInterface);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 32, 1, 1);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "GetPrefabKey", _m_GetPrefabKey_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetMD5Hash", _m_GetMD5Hash_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetMAC", _m_GetMAC_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetIP", _m_GetIP_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetIdentifierForVendor", _m_GetIdentifierForVendor_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetSysVersion", _m_GetSysVersion_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetDeviceModel", _m_GetDeviceModel_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsiPhoneX", _m_IsiPhoneX_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FitiPhoneX", _m_FitiPhoneX_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetSortingOrder", _m_SetSortingOrder_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetBundleIdentifier", _m_GetBundleIdentifier_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetKeyChain", _m_GetKeyChain_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SaveKeyChain", _m_SaveKeyChain_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DeleteKeyChain", _m_DeleteKeyChain_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "StrDesEncrypt", _m_StrDesEncrypt_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "StrDesDecrypt", _m_StrDesDecrypt_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "StrDesEncryptEx", _m_StrDesEncryptEx_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "StrDesDecryptEx", _m_StrDesDecryptEx_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetHttpReq", _m_GetHttpReq_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadGameData", _m_ReadGameData_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadProtoData", _m_ReadProtoData_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadRootData", _m_ReadRootData_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GenDeviceId", _m_GenDeviceId_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SearchFilesEx", _m_SearchFilesEx_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SendErrorLog", _m_SendErrorLog_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SendLogFile", _m_SendLogFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetPortName", _m_SetPortName_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetCRC32", _m_GetCRC32_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "StopGame", _m_StopGame_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ResumeGame", _m_ResumeGame_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "EnterAutoTest", _m_EnterAutoTest_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "m_cacheKey", _g_get_m_cacheKey);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "m_cacheKey", _s_set_m_cacheKey);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "GameInterface does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPrefabKey_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string prefabFullPath = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = GameInterface.GetPrefabKey( prefabFullPath );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetMD5Hash_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    byte[] buffer = LuaAPI.lua_tobytes(L, 1);
                    
                        string __cl_gen_ret = GameInterface.GetMD5Hash( buffer );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetMAC_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string __cl_gen_ret = GameInterface.GetMAC(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetIP_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string __cl_gen_ret = GameInterface.GetIP(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetIdentifierForVendor_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string __cl_gen_ret = GameInterface.GetIdentifierForVendor(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSysVersion_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string __cl_gen_ret = GameInterface.GetSysVersion(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetDeviceModel_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string __cl_gen_ret = GameInterface.GetDeviceModel(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsiPhoneX_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        bool __cl_gen_ret = GameInterface.IsiPhoneX(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FitiPhoneX_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& translator.Assignable<FairyGUI.Window>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    FairyGUI.Window window = (FairyGUI.Window)translator.GetObject(L, 1, typeof(FairyGUI.Window));
                    bool needFit = LuaAPI.lua_toboolean(L, 2);
                    
                    GameInterface.FitiPhoneX( window, needFit );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 1&& translator.Assignable<FairyGUI.Window>(L, 1)) 
                {
                    FairyGUI.Window window = (FairyGUI.Window)translator.GetObject(L, 1, typeof(FairyGUI.Window));
                    
                    GameInterface.FitiPhoneX( window );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to GameInterface.FitiPhoneX!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSortingOrder_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    FairyGUI.Window window = (FairyGUI.Window)translator.GetObject(L, 1, typeof(FairyGUI.Window));
                    Game.UIPageLayer layer;translator.Get(L, 2, out layer);
                    
                    GameInterface.SetSortingOrder( window, layer );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetBundleIdentifier_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string __cl_gen_ret = GameInterface.GetBundleIdentifier(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetKeyChain_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string key = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = GameInterface.GetKeyChain( key );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SaveKeyChain_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string key = LuaAPI.lua_tostring(L, 1);
                    string value = LuaAPI.lua_tostring(L, 2);
                    
                    GameInterface.SaveKeyChain( key, value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DeleteKeyChain_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string key = LuaAPI.lua_tostring(L, 1);
                    
                    GameInterface.DeleteKeyChain( key );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StrDesEncrypt_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string plaintxt = LuaAPI.lua_tostring(L, 1);
                    string key = LuaAPI.lua_tostring(L, 2);
                    
                        string __cl_gen_ret = GameInterface.StrDesEncrypt( plaintxt, key );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StrDesDecrypt_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string ciphertext = LuaAPI.lua_tostring(L, 1);
                    string sKey = LuaAPI.lua_tostring(L, 2);
                    
                        string __cl_gen_ret = GameInterface.StrDesDecrypt( ciphertext, sKey );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StrDesEncryptEx_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string text = LuaAPI.lua_tostring(L, 1);
                    string key = LuaAPI.lua_tostring(L, 2);
                    string iv = LuaAPI.lua_tostring(L, 3);
                    
                        string __cl_gen_ret = GameInterface.StrDesEncryptEx( text, key, iv );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StrDesDecryptEx_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string text = LuaAPI.lua_tostring(L, 1);
                    string key = LuaAPI.lua_tostring(L, 2);
                    string iv = LuaAPI.lua_tostring(L, 3);
                    
                        string __cl_gen_ret = GameInterface.StrDesDecryptEx( text, key, iv );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetHttpReq_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string url = LuaAPI.lua_tostring(L, 1);
                    int timeout = LuaAPI.xlua_tointeger(L, 2);
                    
                        string __cl_gen_ret = GameInterface.GetHttpReq( url, timeout );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadGameData_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        byte[] __cl_gen_ret = GameInterface.ReadGameData( path );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadProtoData_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        byte[] __cl_gen_ret = GameInterface.ReadProtoData( path );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadRootData_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        byte[] __cl_gen_ret = GameInterface.ReadRootData( path );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GenDeviceId_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string __cl_gen_ret = GameInterface.GenDeviceId(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SearchFilesEx_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    string searchPattern = LuaAPI.lua_tostring(L, 2);
                    
                        string[] __cl_gen_ret = GameInterface.SearchFilesEx( path, searchPattern );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SendErrorLog_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    GameInterface.SendErrorLog(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SendLogFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                    GameInterface.SendLogFile( path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPortName_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string name = LuaAPI.lua_tostring(L, 1);
                    
                    GameInterface.SetPortName( name );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCRC32_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    byte[] bytes = LuaAPI.lua_tobytes(L, 1);
                    
                        uint __cl_gen_ret = GameInterface.GetCRC32( bytes );
                        LuaAPI.xlua_pushuint(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StopGame_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    GameInterface.StopGame(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ResumeGame_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    GameInterface.ResumeGame(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EnterAutoTest_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        bool __cl_gen_ret = GameInterface.EnterAutoTest(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_cacheKey(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, GameInterface.m_cacheKey);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_cacheKey(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    GameInterface.m_cacheKey = (System.Collections.Generic.Dictionary<string, string>)translator.GetObject(L, 1, typeof(System.Collections.Generic.Dictionary<string, string>));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
