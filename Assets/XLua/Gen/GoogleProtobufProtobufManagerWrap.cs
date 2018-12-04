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
    public class GoogleProtobufProtobufManagerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Google.Protobuf.ProtobufManager);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 6, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "PoolClear", _m_PoolClear_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "New", _m_New_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Reclaim", _m_Reclaim_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DeserializeFromBytes", _m_DeserializeFromBytes_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SerializeToBytes", _m_SerializeToBytes_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "Google.Protobuf.ProtobufManager does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PoolClear_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    Google.Protobuf.ProtobufManager.PoolClear(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_New_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.Type t = (System.Type)translator.GetObject(L, 1, typeof(System.Type));
                    
                        Google.Protobuf.IMessage __cl_gen_ret = Google.Protobuf.ProtobufManager.New( t );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Reclaim_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Google.Protobuf.IMessage m = (Google.Protobuf.IMessage)translator.GetObject(L, 1, typeof(Google.Protobuf.IMessage));
                    
                    Google.Protobuf.ProtobufManager.Reclaim( m );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DeserializeFromBytes_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.Type type = (System.Type)translator.GetObject(L, 1, typeof(System.Type));
                    byte[] bytes = LuaAPI.lua_tobytes(L, 2);
                    int len = LuaAPI.xlua_tointeger(L, 3);
                    
                        Google.Protobuf.IMessage __cl_gen_ret = Google.Protobuf.ProtobufManager.DeserializeFromBytes( type, bytes, len );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SerializeToBytes_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Google.Protobuf.IMessage data = (Google.Protobuf.IMessage)translator.GetObject(L, 1, typeof(Google.Protobuf.IMessage));
                    byte[] bytes = LuaAPI.lua_tobytes(L, 2);
                    int len = LuaAPI.xlua_tointeger(L, 3);
                    
                    Google.Protobuf.ProtobufManager.SerializeToBytes( data, bytes, len );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
