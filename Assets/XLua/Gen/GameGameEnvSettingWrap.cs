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
    public class GameGameEnvSettingWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Game.GameEnvSetting);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 4, 26, 26);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "ApplySetting", _m_ApplySetting_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ApplyRenderQuality", _m_ApplyRenderQuality_xlua_st_);
            
			
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "AssetBundleReadPath", Game.GameEnvSetting.AssetBundleReadPath);
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "EnablePersistentPath", _g_get_EnablePersistentPath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "EnableAssetBundle", _g_get_EnableAssetBundle);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "EnableStreamerPool", _g_get_EnableStreamerPool);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "AssetBundleDir", _g_get_AssetBundleDir);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "basePath", _g_get_basePath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "AssetBundleOutputPath", _g_get_AssetBundleOutputPath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "ResolutionX", _g_get_ResolutionX);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "ResolutionY", _g_get_ResolutionY);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "dataPath", _g_get_dataPath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "dataUpdatePath", _g_get_dataUpdatePath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "dataStreamPath", _g_get_dataStreamPath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "protoPath", _g_get_protoPath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "protoUpdatePath", _g_get_protoUpdatePath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "protoStreamPath", _g_get_protoStreamPath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "rootUpdatePath", _g_get_rootUpdatePath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "rootStreamPath", _g_get_rootStreamPath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "UseLigthingMap", _g_get_UseLigthingMap);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "RealTimeLighting", _g_get_RealTimeLighting);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "DeviceLevel", _g_get_DeviceLevel);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "nowLevel", _g_get_nowLevel);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "frameRate", _g_get_frameRate);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "frameTime", _g_get_frameTime);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "UseMultiThread", _g_get_UseMultiThread);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "UseShadowThread", _g_get_UseShadowThread);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "UseShadowLerp", _g_get_UseShadowLerp);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "debugShowLog", _g_get_debugShowLog);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "EnablePersistentPath", _s_set_EnablePersistentPath);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "EnableAssetBundle", _s_set_EnableAssetBundle);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "EnableStreamerPool", _s_set_EnableStreamerPool);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "AssetBundleDir", _s_set_AssetBundleDir);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "basePath", _s_set_basePath);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "AssetBundleOutputPath", _s_set_AssetBundleOutputPath);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "ResolutionX", _s_set_ResolutionX);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "ResolutionY", _s_set_ResolutionY);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "dataPath", _s_set_dataPath);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "dataUpdatePath", _s_set_dataUpdatePath);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "dataStreamPath", _s_set_dataStreamPath);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "protoPath", _s_set_protoPath);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "protoUpdatePath", _s_set_protoUpdatePath);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "protoStreamPath", _s_set_protoStreamPath);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "rootUpdatePath", _s_set_rootUpdatePath);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "rootStreamPath", _s_set_rootStreamPath);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "UseLigthingMap", _s_set_UseLigthingMap);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "RealTimeLighting", _s_set_RealTimeLighting);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "DeviceLevel", _s_set_DeviceLevel);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "nowLevel", _s_set_nowLevel);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "frameRate", _s_set_frameRate);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "frameTime", _s_set_frameTime);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "UseMultiThread", _s_set_UseMultiThread);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "UseShadowThread", _s_set_UseShadowThread);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "UseShadowLerp", _s_set_UseShadowLerp);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "debugShowLog", _s_set_debugShowLog);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "Game.GameEnvSetting does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ApplySetting_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    Game.GameEnvSetting.ApplySetting(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ApplyRenderQuality_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    Game.GameEnvSetting.ApplyRenderQuality(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_EnablePersistentPath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, Game.GameEnvSetting.EnablePersistentPath);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_EnableAssetBundle(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, Game.GameEnvSetting.EnableAssetBundle);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_EnableStreamerPool(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, Game.GameEnvSetting.EnableStreamerPool);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AssetBundleDir(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, Game.GameEnvSetting.AssetBundleDir);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_basePath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Game.GameEnvSetting.basePath);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AssetBundleOutputPath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Game.GameEnvSetting.AssetBundleOutputPath);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ResolutionX(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, Game.GameEnvSetting.ResolutionX);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ResolutionY(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, Game.GameEnvSetting.ResolutionY);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_dataPath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Game.GameEnvSetting.dataPath);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_dataUpdatePath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Game.GameEnvSetting.dataUpdatePath);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_dataStreamPath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Game.GameEnvSetting.dataStreamPath);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_protoPath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Game.GameEnvSetting.protoPath);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_protoUpdatePath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Game.GameEnvSetting.protoUpdatePath);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_protoStreamPath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Game.GameEnvSetting.protoStreamPath);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rootUpdatePath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Game.GameEnvSetting.rootUpdatePath);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rootStreamPath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, Game.GameEnvSetting.rootStreamPath);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UseLigthingMap(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, Game.GameEnvSetting.UseLigthingMap);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RealTimeLighting(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, Game.GameEnvSetting.RealTimeLighting);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DeviceLevel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, Game.GameEnvSetting.DeviceLevel);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_nowLevel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, Game.GameEnvSetting.nowLevel);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_frameRate(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, Game.GameEnvSetting.frameRate);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_frameTime(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushnumber(L, Game.GameEnvSetting.frameTime);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UseMultiThread(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, Game.GameEnvSetting.UseMultiThread);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UseShadowThread(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, Game.GameEnvSetting.UseShadowThread);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UseShadowLerp(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, Game.GameEnvSetting.UseShadowLerp);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_debugShowLog(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, Game.GameEnvSetting.debugShowLog);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_EnablePersistentPath(RealStatePtr L)
        {
		    try {
                
			    Game.GameEnvSetting.EnablePersistentPath = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_EnableAssetBundle(RealStatePtr L)
        {
		    try {
                
			    Game.GameEnvSetting.EnableAssetBundle = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_EnableStreamerPool(RealStatePtr L)
        {
		    try {
                
			    Game.GameEnvSetting.EnableStreamerPool = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_AssetBundleDir(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    Game.GameEnvSetting.AssetBundleDir = (string[])translator.GetObject(L, 1, typeof(string[]));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_basePath(RealStatePtr L)
        {
		    try {
                
			    Game.GameEnvSetting.basePath = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_AssetBundleOutputPath(RealStatePtr L)
        {
		    try {
                
			    Game.GameEnvSetting.AssetBundleOutputPath = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ResolutionX(RealStatePtr L)
        {
		    try {
                
			    Game.GameEnvSetting.ResolutionX = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ResolutionY(RealStatePtr L)
        {
		    try {
                
			    Game.GameEnvSetting.ResolutionY = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_dataPath(RealStatePtr L)
        {
		    try {
                
			    Game.GameEnvSetting.dataPath = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_dataUpdatePath(RealStatePtr L)
        {
		    try {
                
			    Game.GameEnvSetting.dataUpdatePath = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_dataStreamPath(RealStatePtr L)
        {
		    try {
                
			    Game.GameEnvSetting.dataStreamPath = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_protoPath(RealStatePtr L)
        {
		    try {
                
			    Game.GameEnvSetting.protoPath = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_protoUpdatePath(RealStatePtr L)
        {
		    try {
                
			    Game.GameEnvSetting.protoUpdatePath = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_protoStreamPath(RealStatePtr L)
        {
		    try {
                
			    Game.GameEnvSetting.protoStreamPath = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rootUpdatePath(RealStatePtr L)
        {
		    try {
                
			    Game.GameEnvSetting.rootUpdatePath = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rootStreamPath(RealStatePtr L)
        {
		    try {
                
			    Game.GameEnvSetting.rootStreamPath = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UseLigthingMap(RealStatePtr L)
        {
		    try {
                
			    Game.GameEnvSetting.UseLigthingMap = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RealTimeLighting(RealStatePtr L)
        {
		    try {
                
			    Game.GameEnvSetting.RealTimeLighting = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DeviceLevel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			GameUtil.SGameRenderQuality __cl_gen_value;translator.Get(L, 1, out __cl_gen_value);
				Game.GameEnvSetting.DeviceLevel = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_nowLevel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			GameUtil.SGameRenderQuality __cl_gen_value;translator.Get(L, 1, out __cl_gen_value);
				Game.GameEnvSetting.nowLevel = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_frameRate(RealStatePtr L)
        {
		    try {
                
			    Game.GameEnvSetting.frameRate = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_frameTime(RealStatePtr L)
        {
		    try {
                
			    Game.GameEnvSetting.frameTime = (float)LuaAPI.lua_tonumber(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UseMultiThread(RealStatePtr L)
        {
		    try {
                
			    Game.GameEnvSetting.UseMultiThread = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UseShadowThread(RealStatePtr L)
        {
		    try {
                
			    Game.GameEnvSetting.UseShadowThread = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UseShadowLerp(RealStatePtr L)
        {
		    try {
                
			    Game.GameEnvSetting.UseShadowLerp = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_debugShowLog(RealStatePtr L)
        {
		    try {
                
			    Game.GameEnvSetting.debugShowLog = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
