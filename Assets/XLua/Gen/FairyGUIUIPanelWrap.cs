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
    public class FairyGUIUIPanelWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(FairyGUI.UIPanel);
			Utils.BeginObjectRegister(type, L, translator, 0, 10, 7, 4);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateUI", _m_CreateUI);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSortingOrder", _m_SetSortingOrder);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetHitTestMode", _m_SetHitTestMode);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CacheNativeChildrenRenderers", _m_CacheNativeChildrenRenderers);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ApplyModifiedProperties", _m_ApplyModifiedProperties);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "MoveUI", _m_MoveUI);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetUIWorldPosition", _m_GetUIWorldPosition);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "EM_BeforeUpdate", _m_EM_BeforeUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "EM_Update", _m_EM_Update);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "EM_Reload", _m_EM_Reload);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "container", _g_get_container);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ui", _g_get_ui);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "EM_sortingOrder", _g_get_EM_sortingOrder);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "packageName", _g_get_packageName);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "componentName", _g_get_componentName);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "fitScreen", _g_get_fitScreen);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "sortingOrder", _g_get_sortingOrder);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "packageName", _s_set_packageName);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "componentName", _s_set_componentName);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "fitScreen", _s_set_fitScreen);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "sortingOrder", _s_set_sortingOrder);
            
			
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
					
					FairyGUI.UIPanel __cl_gen_ret = new FairyGUI.UIPanel();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to FairyGUI.UIPanel constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateUI(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.UIPanel __cl_gen_to_be_invoked = (FairyGUI.UIPanel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.CreateUI(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSortingOrder(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.UIPanel __cl_gen_to_be_invoked = (FairyGUI.UIPanel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int value = LuaAPI.xlua_tointeger(L, 2);
                    bool apply = LuaAPI.lua_toboolean(L, 3);
                    
                    __cl_gen_to_be_invoked.SetSortingOrder( value, apply );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetHitTestMode(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.UIPanel __cl_gen_to_be_invoked = (FairyGUI.UIPanel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.HitTestMode value;translator.Get(L, 2, out value);
                    
                    __cl_gen_to_be_invoked.SetHitTestMode( value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CacheNativeChildrenRenderers(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.UIPanel __cl_gen_to_be_invoked = (FairyGUI.UIPanel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.CacheNativeChildrenRenderers(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ApplyModifiedProperties(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.UIPanel __cl_gen_to_be_invoked = (FairyGUI.UIPanel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool sortingOrderChanged = LuaAPI.lua_toboolean(L, 2);
                    bool fitScreenChanged = LuaAPI.lua_toboolean(L, 3);
                    
                    __cl_gen_to_be_invoked.ApplyModifiedProperties( sortingOrderChanged, fitScreenChanged );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MoveUI(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.UIPanel __cl_gen_to_be_invoked = (FairyGUI.UIPanel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Vector3 delta;translator.Get(L, 2, out delta);
                    
                    __cl_gen_to_be_invoked.MoveUI( delta );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetUIWorldPosition(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.UIPanel __cl_gen_to_be_invoked = (FairyGUI.UIPanel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        UnityEngine.Vector3 __cl_gen_ret = __cl_gen_to_be_invoked.GetUIWorldPosition(  );
                        translator.PushUnityEngineVector3(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EM_BeforeUpdate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.UIPanel __cl_gen_to_be_invoked = (FairyGUI.UIPanel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.EM_BeforeUpdate(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EM_Update(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.UIPanel __cl_gen_to_be_invoked = (FairyGUI.UIPanel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.UpdateContext context = (FairyGUI.UpdateContext)translator.GetObject(L, 2, typeof(FairyGUI.UpdateContext));
                    
                    __cl_gen_to_be_invoked.EM_Update( context );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EM_Reload(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FairyGUI.UIPanel __cl_gen_to_be_invoked = (FairyGUI.UIPanel)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.EM_Reload(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_container(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.UIPanel __cl_gen_to_be_invoked = (FairyGUI.UIPanel)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.container);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ui(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.UIPanel __cl_gen_to_be_invoked = (FairyGUI.UIPanel)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.ui);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_EM_sortingOrder(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.UIPanel __cl_gen_to_be_invoked = (FairyGUI.UIPanel)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.EM_sortingOrder);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_packageName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.UIPanel __cl_gen_to_be_invoked = (FairyGUI.UIPanel)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.packageName);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_componentName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.UIPanel __cl_gen_to_be_invoked = (FairyGUI.UIPanel)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.componentName);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_fitScreen(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.UIPanel __cl_gen_to_be_invoked = (FairyGUI.UIPanel)translator.FastGetCSObj(L, 1);
                translator.PushFairyGUIFitScreen(L, __cl_gen_to_be_invoked.fitScreen);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_sortingOrder(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.UIPanel __cl_gen_to_be_invoked = (FairyGUI.UIPanel)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.sortingOrder);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_packageName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.UIPanel __cl_gen_to_be_invoked = (FairyGUI.UIPanel)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.packageName = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_componentName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.UIPanel __cl_gen_to_be_invoked = (FairyGUI.UIPanel)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.componentName = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_fitScreen(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.UIPanel __cl_gen_to_be_invoked = (FairyGUI.UIPanel)translator.FastGetCSObj(L, 1);
                FairyGUI.FitScreen __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.fitScreen = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_sortingOrder(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FairyGUI.UIPanel __cl_gen_to_be_invoked = (FairyGUI.UIPanel)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.sortingOrder = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
