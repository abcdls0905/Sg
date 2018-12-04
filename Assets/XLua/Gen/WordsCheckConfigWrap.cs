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
    public class WordsCheckConfigWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(WordsCheck.Config);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 7, 7);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "DataRelatePath", _g_get_DataRelatePath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "IsAudit", _g_get_IsAudit);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "MaxFaceIndex", _g_get_MaxFaceIndex);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "LangChatshieldName", _g_get_LangChatshieldName);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "DataRelatePathAssets", _g_get_DataRelatePathAssets);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "DataRelatePathStream", _g_get_DataRelatePathStream);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "DataRelatePathUpdate", _g_get_DataRelatePathUpdate);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "DataRelatePath", _s_set_DataRelatePath);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "IsAudit", _s_set_IsAudit);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "MaxFaceIndex", _s_set_MaxFaceIndex);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "LangChatshieldName", _s_set_LangChatshieldName);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "DataRelatePathAssets", _s_set_DataRelatePathAssets);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "DataRelatePathStream", _s_set_DataRelatePathStream);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "DataRelatePathUpdate", _s_set_DataRelatePathUpdate);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					WordsCheck.Config __cl_gen_ret = new WordsCheck.Config();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to WordsCheck.Config constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DataRelatePath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, WordsCheck.Config.DataRelatePath);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsAudit(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, WordsCheck.Config.IsAudit);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MaxFaceIndex(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, WordsCheck.Config.MaxFaceIndex);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LangChatshieldName(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, WordsCheck.Config.LangChatshieldName);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DataRelatePathAssets(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, WordsCheck.Config.DataRelatePathAssets);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DataRelatePathStream(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, WordsCheck.Config.DataRelatePathStream);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DataRelatePathUpdate(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, WordsCheck.Config.DataRelatePathUpdate);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DataRelatePath(RealStatePtr L)
        {
		    try {
                
			    WordsCheck.Config.DataRelatePath = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsAudit(RealStatePtr L)
        {
		    try {
                
			    WordsCheck.Config.IsAudit = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_MaxFaceIndex(RealStatePtr L)
        {
		    try {
                
			    WordsCheck.Config.MaxFaceIndex = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LangChatshieldName(RealStatePtr L)
        {
		    try {
                
			    WordsCheck.Config.LangChatshieldName = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DataRelatePathAssets(RealStatePtr L)
        {
		    try {
                
			    WordsCheck.Config.DataRelatePathAssets = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DataRelatePathStream(RealStatePtr L)
        {
		    try {
                
			    WordsCheck.Config.DataRelatePathStream = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DataRelatePathUpdate(RealStatePtr L)
        {
		    try {
                
			    WordsCheck.Config.DataRelatePathUpdate = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
