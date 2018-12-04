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
    public class GameNetworkManagerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Game.NetworkManager);
			Utils.BeginObjectRegister(type, L, translator, 0, 15, 4, 2);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Init", _m_Init);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RegisterMsgMapper", _m_RegisterMsgMapper);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetMsgInfo", _m_GetMsgInfo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ConnectGameServer", _m_ConnectGameServer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ConnectBattleServer", _m_ConnectBattleServer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CloseGameServer", _m_CloseGameServer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CloseBattleServer", _m_CloseBattleServer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DisconnectBattleServer", _m_DisconnectBattleServer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DisconnectGameServer", _m_DisconnectGameServer);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsGameConnected", _m_IsGameConnected);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsBattleConnected", _m_IsBattleConnected);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Update", _m_Update);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SendGameMsg", _m_SendGameMsg);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SendBattleMsg", _m_SendBattleMsg);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetBattlePingTime", _m_GetBattlePingTime);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "BattleConnector", _g_get_BattleConnector);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LobbyConnector", _g_get_LobbyConnector);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "netReach", _g_get_netReach);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "msgHandler", _g_get_msgHandler);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "netReach", _s_set_netReach);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "msgHandler", _s_set_msgHandler);
            
			
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
					
					Game.NetworkManager __cl_gen_ret = new Game.NetworkManager();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Game.NetworkManager constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkManager __cl_gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Init(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RegisterMsgMapper(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkManager __cl_gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Game.NetworkManager.MsgMapper mapper = (Game.NetworkManager.MsgMapper)translator.GetObject(L, 2, typeof(Game.NetworkManager.MsgMapper));
                    
                    __cl_gen_to_be_invoked.RegisterMsgMapper( mapper );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetMsgInfo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkManager __cl_gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    ushort id = (ushort)LuaAPI.xlua_tointeger(L, 2);
                    Game.NetworkManager.MsgMapper mapper;
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.GetMsgInfo( id, out mapper );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    translator.Push(L, mapper);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ConnectGameServer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkManager __cl_gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string ip = LuaAPI.lua_tostring(L, 2);
                    int port = LuaAPI.xlua_tointeger(L, 3);
                    int gsid = LuaAPI.xlua_tointeger(L, 4);
                    
                    __cl_gen_to_be_invoked.ConnectGameServer( ip, port, gsid );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ConnectBattleServer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkManager __cl_gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string ip = LuaAPI.lua_tostring(L, 2);
                    int port = LuaAPI.xlua_tointeger(L, 3);
                    int gsid = LuaAPI.xlua_tointeger(L, 4);
                    
                    __cl_gen_to_be_invoked.ConnectBattleServer( ip, port, gsid );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CloseGameServer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkManager __cl_gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.CloseGameServer(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CloseBattleServer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkManager __cl_gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.CloseBattleServer(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DisconnectBattleServer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkManager __cl_gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.DisconnectBattleServer(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DisconnectGameServer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkManager __cl_gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.DisconnectGameServer(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsGameConnected(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkManager __cl_gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.IsGameConnected(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsBattleConnected(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkManager __cl_gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.IsBattleConnected(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Update(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkManager __cl_gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Update(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SendGameMsg(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkManager __cl_gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<Google.Protobuf.IMessage>(L, 3)) 
                {
                    ushort id = (ushort)LuaAPI.xlua_tointeger(L, 2);
                    Google.Protobuf.IMessage msg = (Google.Protobuf.IMessage)translator.GetObject(L, 3, typeof(Google.Protobuf.IMessage));
                    
                    __cl_gen_to_be_invoked.SendGameMsg( id, msg );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    ushort id = (ushort)LuaAPI.xlua_tointeger(L, 2);
                    byte[] data = LuaAPI.lua_tobytes(L, 3);
                    
                    __cl_gen_to_be_invoked.SendGameMsg( id, data );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 3&& translator.Assignable<Pb.MSG>(L, 2)&& translator.Assignable<Google.Protobuf.IMessage>(L, 3)) 
                {
                    Pb.MSG id;translator.Get(L, 2, out id);
                    Google.Protobuf.IMessage msg = (Google.Protobuf.IMessage)translator.GetObject(L, 3, typeof(Google.Protobuf.IMessage));
                    
                    __cl_gen_to_be_invoked.SendGameMsg( id, msg );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Game.NetworkManager.SendGameMsg!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SendBattleMsg(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkManager __cl_gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<Google.Protobuf.IMessage>(L, 3)) 
                {
                    ushort id = (ushort)LuaAPI.xlua_tointeger(L, 2);
                    Google.Protobuf.IMessage msg = (Google.Protobuf.IMessage)translator.GetObject(L, 3, typeof(Google.Protobuf.IMessage));
                    
                    __cl_gen_to_be_invoked.SendBattleMsg( id, msg );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 3&& translator.Assignable<Pb.MSG>(L, 2)&& translator.Assignable<Google.Protobuf.IMessage>(L, 3)) 
                {
                    Pb.MSG id;translator.Get(L, 2, out id);
                    Google.Protobuf.IMessage msg = (Google.Protobuf.IMessage)translator.GetObject(L, 3, typeof(Google.Protobuf.IMessage));
                    
                    __cl_gen_to_be_invoked.SendBattleMsg( id, msg );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Game.NetworkManager.SendBattleMsg!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetBattlePingTime(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.NetworkManager __cl_gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        long __cl_gen_ret = __cl_gen_to_be_invoked.GetBattlePingTime(  );
                        LuaAPI.lua_pushint64(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BattleConnector(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkManager __cl_gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.BattleConnector);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LobbyConnector(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkManager __cl_gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.LobbyConnector);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_netReach(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkManager __cl_gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineNetworkReachability(L, __cl_gen_to_be_invoked.netReach);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_msgHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkManager __cl_gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.msgHandler);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_netReach(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkManager __cl_gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
                UnityEngine.NetworkReachability __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.netReach = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_msgHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Game.NetworkManager __cl_gen_to_be_invoked = (Game.NetworkManager)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.msgHandler = (System.Collections.Generic.List<System.Type>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<System.Type>));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
