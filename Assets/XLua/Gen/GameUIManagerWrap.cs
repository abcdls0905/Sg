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
    public class GameUIManagerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Game.UIManager);
			Utils.BeginObjectRegister(type, L, translator, 0, 19, 1, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Init", _m_Init);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PreloadUIComponent", _m_PreloadUIComponent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddUIPackage", _m_AddUIPackage);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddPackDelayDel", _m_AddPackDelayDel);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveUIPackage", _m_RemoveUIPackage);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadUIPackage", _m_LoadUIPackage);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OpenPage", _m_OpenPage);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClosePage", _m_ClosePage);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PreparePage", _m_PreparePage);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "NewObject", _m_NewObject);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RecycleObject", _m_RecycleObject);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CloseAllPage", _m_CloseAllPage);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Update", _m_Update);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CloseAllLuaPages", _m_CloseAllLuaPages);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCacheNumString", _m_GetCacheNumString);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "MsgTip", _m_MsgTip);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ShowTip", _m_ShowTip);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CloseTip", _m_CloseTip);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "MoveForwad", _m_MoveForwad);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "speedStr", _g_get_speedStr);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "speedStr", _s_set_speedStr);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 2, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "WorldPosToUIPos", _m_WorldPosToUIPos_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					Game.UIManager __cl_gen_ret = new Game.UIManager();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Game.UIManager constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.UIManager __cl_gen_to_be_invoked = (Game.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Init(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PreloadUIComponent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.UIManager __cl_gen_to_be_invoked = (Game.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.PreloadUIComponent(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddUIPackage(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.UIManager __cl_gen_to_be_invoked = (Game.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 2);
                    
                        FairyGUI.UIPackage __cl_gen_ret = __cl_gen_to_be_invoked.AddUIPackage( path );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddPackDelayDel(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.UIManager __cl_gen_to_be_invoked = (Game.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.AddPackDelayDel( path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveUIPackage(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.UIManager __cl_gen_to_be_invoked = (Game.UIManager)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    string path = LuaAPI.lua_tostring(L, 2);
                    bool unloadAssets = LuaAPI.lua_toboolean(L, 3);
                    
                    __cl_gen_to_be_invoked.RemoveUIPackage( path, unloadAssets );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string path = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.RemoveUIPackage( path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Game.UIManager.RemoveUIPackage!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadUIPackage(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.UIManager __cl_gen_to_be_invoked = (Game.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool isEnterBattle = LuaAPI.lua_toboolean(L, 2);
                    
                    __cl_gen_to_be_invoked.LoadUIPackage( isEnterBattle );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenPage(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.UIManager __cl_gen_to_be_invoked = (Game.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Type type = (System.Type)translator.GetObject(L, 2, typeof(System.Type));
                    
                        Game.UIPage __cl_gen_ret = __cl_gen_to_be_invoked.OpenPage( type );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClosePage(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.UIManager __cl_gen_to_be_invoked = (Game.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Type type = (System.Type)translator.GetObject(L, 2, typeof(System.Type));
                    
                    __cl_gen_to_be_invoked.ClosePage( type );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PreparePage(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.UIManager __cl_gen_to_be_invoked = (Game.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Type type = (System.Type)translator.GetObject(L, 2, typeof(System.Type));
                    
                    __cl_gen_to_be_invoked.PreparePage( type );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_NewObject(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.UIManager __cl_gen_to_be_invoked = (Game.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string objName = LuaAPI.lua_tostring(L, 2);
                    
                        FairyGUI.GObject __cl_gen_ret = __cl_gen_to_be_invoked.NewObject( objName );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RecycleObject(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.UIManager __cl_gen_to_be_invoked = (Game.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string objName = LuaAPI.lua_tostring(L, 2);
                    FairyGUI.GObject obj = (FairyGUI.GObject)translator.GetObject(L, 3, typeof(FairyGUI.GObject));
                    
                    __cl_gen_to_be_invoked.RecycleObject( objName, obj );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CloseAllPage(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.UIManager __cl_gen_to_be_invoked = (Game.UIManager)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool cache = LuaAPI.lua_toboolean(L, 2);
                    
                    __cl_gen_to_be_invoked.CloseAllPage( cache );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 1) 
                {
                    
                    __cl_gen_to_be_invoked.CloseAllPage(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Game.UIManager.CloseAllPage!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Update(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.UIManager __cl_gen_to_be_invoked = (Game.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float deltaTime = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    __cl_gen_to_be_invoked.Update( deltaTime );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CloseAllLuaPages(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.UIManager __cl_gen_to_be_invoked = (Game.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.CloseAllLuaPages(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCacheNumString(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.UIManager __cl_gen_to_be_invoked = (Game.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int num = LuaAPI.xlua_tointeger(L, 2);
                    bool bSpecial = LuaAPI.lua_toboolean(L, 3);
                    
                        string __cl_gen_ret = __cl_gen_to_be_invoked.GetCacheNumString( num, bSpecial );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WorldPosToUIPos_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Vector3 pos;translator.Get(L, 1, out pos);
                    bool inScreen;
                    
                        UnityEngine.Vector2 __cl_gen_ret = Game.UIManager.WorldPosToUIPos( pos, out inScreen );
                        translator.PushUnityEngineVector2(L, __cl_gen_ret);
                    LuaAPI.lua_pushboolean(L, inScreen);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MsgTip(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.UIManager __cl_gen_to_be_invoked = (Game.UIManager)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 6&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)) 
                {
                    string text = LuaAPI.lua_tostring(L, 2);
                    float time = (float)LuaAPI.lua_tonumber(L, 3);
                    int type = LuaAPI.xlua_tointeger(L, 4);
                    bool show = LuaAPI.lua_toboolean(L, 5);
                    float totalTime = (float)LuaAPI.lua_tonumber(L, 6);
                    
                    __cl_gen_to_be_invoked.MsgTip( text, time, type, show, totalTime );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 5&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)) 
                {
                    string text = LuaAPI.lua_tostring(L, 2);
                    float time = (float)LuaAPI.lua_tonumber(L, 3);
                    int type = LuaAPI.xlua_tointeger(L, 4);
                    bool show = LuaAPI.lua_toboolean(L, 5);
                    
                    __cl_gen_to_be_invoked.MsgTip( text, time, type, show );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    string text = LuaAPI.lua_tostring(L, 2);
                    float time = (float)LuaAPI.lua_tonumber(L, 3);
                    int type = LuaAPI.xlua_tointeger(L, 4);
                    
                    __cl_gen_to_be_invoked.MsgTip( text, time, type );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    string text = LuaAPI.lua_tostring(L, 2);
                    float time = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    __cl_gen_to_be_invoked.MsgTip( text, time );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string text = LuaAPI.lua_tostring(L, 2);
                    
                    __cl_gen_to_be_invoked.MsgTip( text );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Game.UIManager.MsgTip!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ShowTip(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.UIManager __cl_gen_to_be_invoked = (Game.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    GameLua.TipArgs args;translator.Get(L, 2, out args);
                    int tipType = LuaAPI.xlua_tointeger(L, 3);
                    int tipStyle = LuaAPI.xlua_tointeger(L, 4);
                    
                    __cl_gen_to_be_invoked.ShowTip( args, tipType, tipStyle );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CloseTip(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.UIManager __cl_gen_to_be_invoked = (Game.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.CloseTip(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MoveForwad(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.UIManager __cl_gen_to_be_invoked = (Game.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FairyGUI.Window page = (FairyGUI.Window)translator.GetObject(L, 2, typeof(FairyGUI.Window));
                    
                    __cl_gen_to_be_invoked.MoveForwad( page );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_speedStr(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.UIManager __cl_gen_to_be_invoked = (Game.UIManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.speedStr);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_speedStr(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.UIManager __cl_gen_to_be_invoked = (Game.UIManager)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.speedStr = (SpeedString)translator.GetObject(L, 2, typeof(SpeedString));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
