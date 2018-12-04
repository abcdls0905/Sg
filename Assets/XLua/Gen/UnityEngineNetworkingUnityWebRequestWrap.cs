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
    public class UnityEngineNetworkingUnityWebRequestWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UnityEngine.Networking.UnityWebRequest);
			Utils.BeginObjectRegister(type, L, translator, 0, 7, 20, 10);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Dispose", _m_Dispose);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SendWebRequest", _m_SendWebRequest);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Abort", _m_Abort);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetRequestHeader", _m_GetRequestHeader);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetRequestHeader", _m_SetRequestHeader);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetResponseHeader", _m_GetResponseHeader);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetResponseHeaders", _m_GetResponseHeaders);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "disposeDownloadHandlerOnDispose", _g_get_disposeDownloadHandlerOnDispose);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "disposeUploadHandlerOnDispose", _g_get_disposeUploadHandlerOnDispose);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "method", _g_get_method);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "error", _g_get_error);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "useHttpContinue", _g_get_useHttpContinue);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "url", _g_get_url);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "responseCode", _g_get_responseCode);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uploadProgress", _g_get_uploadProgress);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isModifiable", _g_get_isModifiable);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isDone", _g_get_isDone);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isNetworkError", _g_get_isNetworkError);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isHttpError", _g_get_isHttpError);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "downloadProgress", _g_get_downloadProgress);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uploadedBytes", _g_get_uploadedBytes);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "downloadedBytes", _g_get_downloadedBytes);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "redirectLimit", _g_get_redirectLimit);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "chunkedTransfer", _g_get_chunkedTransfer);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uploadHandler", _g_get_uploadHandler);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "downloadHandler", _g_get_downloadHandler);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "timeout", _g_get_timeout);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "disposeDownloadHandlerOnDispose", _s_set_disposeDownloadHandlerOnDispose);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "disposeUploadHandlerOnDispose", _s_set_disposeUploadHandlerOnDispose);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "method", _s_set_method);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "useHttpContinue", _s_set_useHttpContinue);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "url", _s_set_url);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "redirectLimit", _s_set_redirectLimit);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "chunkedTransfer", _s_set_chunkedTransfer);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "uploadHandler", _s_set_uploadHandler);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "downloadHandler", _s_set_downloadHandler);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "timeout", _s_set_timeout);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 18, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Get", _m_Get_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Delete", _m_Delete_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Head", _m_Head_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetAssetBundle", _m_GetAssetBundle_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Put", _m_Put_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Post", _m_Post_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "EscapeURL", _m_EscapeURL_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "UnEscapeURL", _m_UnEscapeURL_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SerializeFormSections", _m_SerializeFormSections_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GenerateBoundary", _m_GenerateBoundary_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SerializeSimpleForm", _m_SerializeSimpleForm_xlua_st_);
            
			
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "kHttpVerbGET", UnityEngine.Networking.UnityWebRequest.kHttpVerbGET);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "kHttpVerbHEAD", UnityEngine.Networking.UnityWebRequest.kHttpVerbHEAD);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "kHttpVerbPOST", UnityEngine.Networking.UnityWebRequest.kHttpVerbPOST);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "kHttpVerbPUT", UnityEngine.Networking.UnityWebRequest.kHttpVerbPUT);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "kHttpVerbCREATE", UnityEngine.Networking.UnityWebRequest.kHttpVerbCREATE);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "kHttpVerbDELETE", UnityEngine.Networking.UnityWebRequest.kHttpVerbDELETE);
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					UnityEngine.Networking.UnityWebRequest __cl_gen_ret = new UnityEngine.Networking.UnityWebRequest();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 2 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING))
				{
					string url = LuaAPI.lua_tostring(L, 2);
					
					UnityEngine.Networking.UnityWebRequest __cl_gen_ret = new UnityEngine.Networking.UnityWebRequest(url);
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 3 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING))
				{
					string url = LuaAPI.lua_tostring(L, 2);
					string method = LuaAPI.lua_tostring(L, 3);
					
					UnityEngine.Networking.UnityWebRequest __cl_gen_ret = new UnityEngine.Networking.UnityWebRequest(url, method);
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 5 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING) && translator.Assignable<UnityEngine.Networking.DownloadHandler>(L, 4) && translator.Assignable<UnityEngine.Networking.UploadHandler>(L, 5))
				{
					string url = LuaAPI.lua_tostring(L, 2);
					string method = LuaAPI.lua_tostring(L, 3);
					UnityEngine.Networking.DownloadHandler downloadHandler = (UnityEngine.Networking.DownloadHandler)translator.GetObject(L, 4, typeof(UnityEngine.Networking.DownloadHandler));
					UnityEngine.Networking.UploadHandler uploadHandler = (UnityEngine.Networking.UploadHandler)translator.GetObject(L, 5, typeof(UnityEngine.Networking.UploadHandler));
					
					UnityEngine.Networking.UnityWebRequest __cl_gen_ret = new UnityEngine.Networking.UnityWebRequest(url, method, downloadHandler, uploadHandler);
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Networking.UnityWebRequest constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Dispose(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Dispose(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SendWebRequest(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        UnityEngine.Networking.UnityWebRequestAsyncOperation __cl_gen_ret = __cl_gen_to_be_invoked.SendWebRequest(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Abort(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.Abort(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetRequestHeader(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string name = LuaAPI.lua_tostring(L, 2);
                    
                        string __cl_gen_ret = __cl_gen_to_be_invoked.GetRequestHeader( name );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetRequestHeader(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string name = LuaAPI.lua_tostring(L, 2);
                    string value = LuaAPI.lua_tostring(L, 3);
                    
                    __cl_gen_to_be_invoked.SetRequestHeader( name, value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetResponseHeader(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string name = LuaAPI.lua_tostring(L, 2);
                    
                        string __cl_gen_ret = __cl_gen_to_be_invoked.GetResponseHeader( name );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetResponseHeaders(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        System.Collections.Generic.Dictionary<string, string> __cl_gen_ret = __cl_gen_to_be_invoked.GetResponseHeaders(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Get_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string uri = LuaAPI.lua_tostring(L, 1);
                    
                        UnityEngine.Networking.UnityWebRequest __cl_gen_ret = UnityEngine.Networking.UnityWebRequest.Get( uri );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Delete_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string uri = LuaAPI.lua_tostring(L, 1);
                    
                        UnityEngine.Networking.UnityWebRequest __cl_gen_ret = UnityEngine.Networking.UnityWebRequest.Delete( uri );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Head_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string uri = LuaAPI.lua_tostring(L, 1);
                    
                        UnityEngine.Networking.UnityWebRequest __cl_gen_ret = UnityEngine.Networking.UnityWebRequest.Head( uri );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetAssetBundle_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string uri = LuaAPI.lua_tostring(L, 1);
                    
                        UnityEngine.Networking.UnityWebRequest __cl_gen_ret = UnityEngine.Networking.UnityWebRequest.GetAssetBundle( uri );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    string uri = LuaAPI.lua_tostring(L, 1);
                    uint crc = LuaAPI.xlua_touint(L, 2);
                    
                        UnityEngine.Networking.UnityWebRequest __cl_gen_ret = UnityEngine.Networking.UnityWebRequest.GetAssetBundle( uri, crc );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    string uri = LuaAPI.lua_tostring(L, 1);
                    uint version = LuaAPI.xlua_touint(L, 2);
                    uint crc = LuaAPI.xlua_touint(L, 3);
                    
                        UnityEngine.Networking.UnityWebRequest __cl_gen_ret = UnityEngine.Networking.UnityWebRequest.GetAssetBundle( uri, version, crc );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.Hash128>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    string uri = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.Hash128 hash;translator.Get(L, 2, out hash);
                    uint crc = LuaAPI.xlua_touint(L, 3);
                    
                        UnityEngine.Networking.UnityWebRequest __cl_gen_ret = UnityEngine.Networking.UnityWebRequest.GetAssetBundle( uri, hash, crc );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.CachedAssetBundle>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    string uri = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.CachedAssetBundle cachedAssetBundle;translator.Get(L, 2, out cachedAssetBundle);
                    uint crc = LuaAPI.xlua_touint(L, 3);
                    
                        UnityEngine.Networking.UnityWebRequest __cl_gen_ret = UnityEngine.Networking.UnityWebRequest.GetAssetBundle( uri, cachedAssetBundle, crc );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Networking.UnityWebRequest.GetAssetBundle!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Put_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string uri = LuaAPI.lua_tostring(L, 1);
                    byte[] bodyData = LuaAPI.lua_tobytes(L, 2);
                    
                        UnityEngine.Networking.UnityWebRequest __cl_gen_ret = UnityEngine.Networking.UnityWebRequest.Put( uri, bodyData );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string uri = LuaAPI.lua_tostring(L, 1);
                    string bodyData = LuaAPI.lua_tostring(L, 2);
                    
                        UnityEngine.Networking.UnityWebRequest __cl_gen_ret = UnityEngine.Networking.UnityWebRequest.Put( uri, bodyData );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Networking.UnityWebRequest.Put!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Post_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string uri = LuaAPI.lua_tostring(L, 1);
                    string postData = LuaAPI.lua_tostring(L, 2);
                    
                        UnityEngine.Networking.UnityWebRequest __cl_gen_ret = UnityEngine.Networking.UnityWebRequest.Post( uri, postData );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.WWWForm>(L, 2)) 
                {
                    string uri = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.WWWForm formData = (UnityEngine.WWWForm)translator.GetObject(L, 2, typeof(UnityEngine.WWWForm));
                    
                        UnityEngine.Networking.UnityWebRequest __cl_gen_ret = UnityEngine.Networking.UnityWebRequest.Post( uri, formData );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection>>(L, 2)) 
                {
                    string uri = LuaAPI.lua_tostring(L, 1);
                    System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> multipartFormSections = (System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection>));
                    
                        UnityEngine.Networking.UnityWebRequest __cl_gen_ret = UnityEngine.Networking.UnityWebRequest.Post( uri, multipartFormSections );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Collections.Generic.Dictionary<string, string>>(L, 2)) 
                {
                    string uri = LuaAPI.lua_tostring(L, 1);
                    System.Collections.Generic.Dictionary<string, string> formFields = (System.Collections.Generic.Dictionary<string, string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, string>));
                    
                        UnityEngine.Networking.UnityWebRequest __cl_gen_ret = UnityEngine.Networking.UnityWebRequest.Post( uri, formFields );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection>>(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    string uri = LuaAPI.lua_tostring(L, 1);
                    System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> multipartFormSections = (System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection>));
                    byte[] boundary = LuaAPI.lua_tobytes(L, 3);
                    
                        UnityEngine.Networking.UnityWebRequest __cl_gen_ret = UnityEngine.Networking.UnityWebRequest.Post( uri, multipartFormSections, boundary );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Networking.UnityWebRequest.Post!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EscapeURL_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string s = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = UnityEngine.Networking.UnityWebRequest.EscapeURL( s );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.Encoding>(L, 2)) 
                {
                    string s = LuaAPI.lua_tostring(L, 1);
                    System.Text.Encoding e = (System.Text.Encoding)translator.GetObject(L, 2, typeof(System.Text.Encoding));
                    
                        string __cl_gen_ret = UnityEngine.Networking.UnityWebRequest.EscapeURL( s, e );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Networking.UnityWebRequest.EscapeURL!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UnEscapeURL_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string s = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = UnityEngine.Networking.UnityWebRequest.UnEscapeURL( s );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Text.Encoding>(L, 2)) 
                {
                    string s = LuaAPI.lua_tostring(L, 1);
                    System.Text.Encoding e = (System.Text.Encoding)translator.GetObject(L, 2, typeof(System.Text.Encoding));
                    
                        string __cl_gen_ret = UnityEngine.Networking.UnityWebRequest.UnEscapeURL( s, e );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Networking.UnityWebRequest.UnEscapeURL!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SerializeFormSections_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> multipartFormSections = (System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection>)translator.GetObject(L, 1, typeof(System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection>));
                    byte[] boundary = LuaAPI.lua_tobytes(L, 2);
                    
                        byte[] __cl_gen_ret = UnityEngine.Networking.UnityWebRequest.SerializeFormSections( multipartFormSections, boundary );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GenerateBoundary_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        byte[] __cl_gen_ret = UnityEngine.Networking.UnityWebRequest.GenerateBoundary(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SerializeSimpleForm_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.Collections.Generic.Dictionary<string, string> formFields = (System.Collections.Generic.Dictionary<string, string>)translator.GetObject(L, 1, typeof(System.Collections.Generic.Dictionary<string, string>));
                    
                        byte[] __cl_gen_ret = UnityEngine.Networking.UnityWebRequest.SerializeSimpleForm( formFields );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_disposeDownloadHandlerOnDispose(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.disposeDownloadHandlerOnDispose);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_disposeUploadHandlerOnDispose(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.disposeUploadHandlerOnDispose);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_method(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.method);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_error(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.error);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_useHttpContinue(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.useHttpContinue);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_url(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.url);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_responseCode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushint64(L, __cl_gen_to_be_invoked.responseCode);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uploadProgress(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.uploadProgress);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isModifiable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.isModifiable);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isDone(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.isDone);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isNetworkError(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.isNetworkError);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isHttpError(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.isHttpError);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_downloadProgress(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.downloadProgress);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uploadedBytes(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushuint64(L, __cl_gen_to_be_invoked.uploadedBytes);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_downloadedBytes(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushuint64(L, __cl_gen_to_be_invoked.downloadedBytes);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_redirectLimit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.redirectLimit);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_chunkedTransfer(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.chunkedTransfer);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uploadHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.uploadHandler);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_downloadHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.downloadHandler);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_timeout(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.timeout);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_disposeDownloadHandlerOnDispose(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.disposeDownloadHandlerOnDispose = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_disposeUploadHandlerOnDispose(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.disposeUploadHandlerOnDispose = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_method(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.method = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_useHttpContinue(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.useHttpContinue = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_url(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.url = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_redirectLimit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.redirectLimit = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_chunkedTransfer(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.chunkedTransfer = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uploadHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.uploadHandler = (UnityEngine.Networking.UploadHandler)translator.GetObject(L, 2, typeof(UnityEngine.Networking.UploadHandler));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_downloadHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.downloadHandler = (UnityEngine.Networking.DownloadHandler)translator.GetObject(L, 2, typeof(UnityEngine.Networking.DownloadHandler));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_timeout(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Networking.UnityWebRequest __cl_gen_to_be_invoked = (UnityEngine.Networking.UnityWebRequest)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.timeout = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
