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
    public class GameUtilAccountIMCheckerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(GameUtil.AccountIMChecker);
			Utils.BeginObjectRegister(type, L, translator, 0, 10, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetUsedAcc", _m_SetUsedAcc);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RegFunAccChanged", _m_RegFunAccChanged);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCCAccAndPsw", _m_GetCCAccAndPsw);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsFitBundle", _m_IsFitBundle);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsNeedCheck", _m_IsNeedCheck);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsCCAccountValid", _m_IsCCAccountValid);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CheckCCAccount", _m_CheckCCAccount);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Init", _m_Init);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UnInit", _m_UnInit);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CheckCCState", _m_CheckCCState);
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 4, 4);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "RequestTimeOut", _g_get_RequestTimeOut);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "RequestString", _g_get_RequestString);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "MaxCheckTimes", _g_get_MaxCheckTimes);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "DontCheckCC", _g_get_DontCheckCC);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "RequestTimeOut", _s_set_RequestTimeOut);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "RequestString", _s_set_RequestString);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "MaxCheckTimes", _s_set_MaxCheckTimes);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "DontCheckCC", _s_set_DontCheckCC);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					GameUtil.AccountIMChecker __cl_gen_ret = new GameUtil.AccountIMChecker();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to GameUtil.AccountIMChecker constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetUsedAcc(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameUtil.AccountIMChecker __cl_gen_to_be_invoked = (GameUtil.AccountIMChecker)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string acc = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.SetUsedAcc( acc );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RegFunAccChanged(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameUtil.AccountIMChecker __cl_gen_to_be_invoked = (GameUtil.AccountIMChecker)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Action act = translator.GetDelegate<System.Action>(L, 2);
                    
                    __cl_gen_to_be_invoked.RegFunAccChanged( act );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCCAccAndPsw(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameUtil.AccountIMChecker __cl_gen_to_be_invoked = (GameUtil.AccountIMChecker)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        string[] __cl_gen_ret = __cl_gen_to_be_invoked.GetCCAccAndPsw(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsFitBundle(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameUtil.AccountIMChecker __cl_gen_to_be_invoked = (GameUtil.AccountIMChecker)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.IsFitBundle(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsNeedCheck(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameUtil.AccountIMChecker __cl_gen_to_be_invoked = (GameUtil.AccountIMChecker)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.IsNeedCheck(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsCCAccountValid(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameUtil.AccountIMChecker __cl_gen_to_be_invoked = (GameUtil.AccountIMChecker)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string dyTestAccount = LuaAPI.lua_tostring(L, 2);
                    bool timeout;
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.IsCCAccountValid( dyTestAccount, out timeout );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    LuaAPI.lua_pushboolean(L, timeout);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CheckCCAccount(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameUtil.AccountIMChecker __cl_gen_to_be_invoked = (GameUtil.AccountIMChecker)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string dyTestAccount = LuaAPI.lua_tostring(L, 2);
                    
                        System.Collections.IEnumerator __cl_gen_ret = __cl_gen_to_be_invoked.CheckCCAccount( dyTestAccount );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameUtil.AccountIMChecker __cl_gen_to_be_invoked = (GameUtil.AccountIMChecker)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Init(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UnInit(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameUtil.AccountIMChecker __cl_gen_to_be_invoked = (GameUtil.AccountIMChecker)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.UnInit(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CheckCCState(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GameUtil.AccountIMChecker __cl_gen_to_be_invoked = (GameUtil.AccountIMChecker)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.CheckCCState(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RequestTimeOut(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, GameUtil.AccountIMChecker.RequestTimeOut);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RequestString(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, GameUtil.AccountIMChecker.RequestString);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MaxCheckTimes(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, GameUtil.AccountIMChecker.MaxCheckTimes);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DontCheckCC(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, GameUtil.AccountIMChecker.DontCheckCC);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RequestTimeOut(RealStatePtr L)
        {
		    try {
                
			    GameUtil.AccountIMChecker.RequestTimeOut = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RequestString(RealStatePtr L)
        {
		    try {
                
			    GameUtil.AccountIMChecker.RequestString = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_MaxCheckTimes(RealStatePtr L)
        {
		    try {
                
			    GameUtil.AccountIMChecker.MaxCheckTimes = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DontCheckCC(RealStatePtr L)
        {
		    try {
                
			    GameUtil.AccountIMChecker.DontCheckCC = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
