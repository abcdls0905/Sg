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
    public class GamePlayerSettingManagerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Game.PlayerSettingManager);
			Utils.BeginObjectRegister(type, L, translator, 0, 7, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSetting", _m_SetSetting);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsNeedGuide", _m_IsNeedGuide);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CloseGuide", _m_CloseGuide);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetPlayerSetting", _m_SetPlayerSetting);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CheckPlayerSetting", _m_CheckPlayerSetting);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SendSetting", _m_SendSetting);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Destroy", _m_Destroy);
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					Game.PlayerSettingManager __cl_gen_ret = new Game.PlayerSettingManager();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Game.PlayerSettingManager constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSetting(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.PlayerSettingManager __cl_gen_to_be_invoked = (Game.PlayerSettingManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    ulong setting = LuaAPI.lua_touint64(L, 2);
                    ulong tutorial = LuaAPI.lua_touint64(L, 3);
                    
                    __cl_gen_to_be_invoked.SetSetting( setting, tutorial );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsNeedGuide(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.PlayerSettingManager __cl_gen_to_be_invoked = (Game.PlayerSettingManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Game.PlayerGuide type;translator.Get(L, 2, out type);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.IsNeedGuide( type );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CloseGuide(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.PlayerSettingManager __cl_gen_to_be_invoked = (Game.PlayerSettingManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Game.PlayerGuide type;translator.Get(L, 2, out type);
                    
                    __cl_gen_to_be_invoked.CloseGuide( type );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPlayerSetting(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.PlayerSettingManager __cl_gen_to_be_invoked = (Game.PlayerSettingManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool open = LuaAPI.lua_toboolean(L, 2);
                    Game.PlayerSetting type;translator.Get(L, 3, out type);
                    
                    __cl_gen_to_be_invoked.SetPlayerSetting( open, type );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CheckPlayerSetting(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.PlayerSettingManager __cl_gen_to_be_invoked = (Game.PlayerSettingManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Game.PlayerSetting value;translator.Get(L, 2, out value);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.CheckPlayerSetting( value );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SendSetting(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.PlayerSettingManager __cl_gen_to_be_invoked = (Game.PlayerSettingManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.SendSetting(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Destroy(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.PlayerSettingManager __cl_gen_to_be_invoked = (Game.PlayerSettingManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Destroy(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
