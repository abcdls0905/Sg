#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using System;


namespace XLua
{
    public partial class ObjectTranslator
    {
        
        class IniterAdderUnityEngineVector2
        {
            static IniterAdderUnityEngineVector2()
            {
                LuaEnv.AddIniter(Init);
            }
			
			static void Init(LuaEnv luaenv, ObjectTranslator translator)
			{
			
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Vector2>(translator.PushUnityEngineVector2, translator.Get, translator.UpdateUnityEngineVector2);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Vector3>(translator.PushUnityEngineVector3, translator.Get, translator.UpdateUnityEngineVector3);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Vector4>(translator.PushUnityEngineVector4, translator.Get, translator.UpdateUnityEngineVector4);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Color>(translator.PushUnityEngineColor, translator.Get, translator.UpdateUnityEngineColor);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Quaternion>(translator.PushUnityEngineQuaternion, translator.Get, translator.UpdateUnityEngineQuaternion);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Ray>(translator.PushUnityEngineRay, translator.Get, translator.UpdateUnityEngineRay);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Bounds>(translator.PushUnityEngineBounds, translator.Get, translator.UpdateUnityEngineBounds);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.Ray2D>(translator.PushUnityEngineRay2D, translator.Get, translator.UpdateUnityEngineRay2D);
				translator.RegisterPushAndGetAndUpdate<GameLua.LuaEventType>(translator.PushGameLuaLuaEventType, translator.Get, translator.UpdateGameLuaLuaEventType);
				translator.RegisterPushAndGetAndUpdate<GameLua.ExpressArgs>(translator.PushGameLuaExpressArgs, translator.Get, translator.UpdateGameLuaExpressArgs);
				translator.RegisterPushAndGetAndUpdate<UnityEngine.NetworkReachability>(translator.PushUnityEngineNetworkReachability, translator.Get, translator.UpdateUnityEngineNetworkReachability);
				translator.RegisterPushAndGetAndUpdate<FairyGUI.RelationType>(translator.PushFairyGUIRelationType, translator.Get, translator.UpdateFairyGUIRelationType);
				translator.RegisterPushAndGetAndUpdate<FairyGUI.FitScreen>(translator.PushFairyGUIFitScreen, translator.Get, translator.UpdateFairyGUIFitScreen);
				translator.RegisterPushAndGetAndUpdate<FairyGUI.UIContentScaler.ScreenMatchMode>(translator.PushFairyGUIUIContentScalerScreenMatchMode, translator.Get, translator.UpdateFairyGUIUIContentScalerScreenMatchMode);
				translator.RegisterPushAndGetAndUpdate<FairyGUI.ListSelectionMode>(translator.PushFairyGUIListSelectionMode, translator.Get, translator.UpdateFairyGUIListSelectionMode);
				translator.RegisterPushAndGetAndUpdate<Game.NetworkType>(translator.PushGameNetworkType, translator.Get, translator.UpdateGameNetworkType);
				translator.RegisterPushAndGetAndUpdate<Game.UIPageLayer>(translator.PushGameUIPageLayer, translator.Get, translator.UpdateGameUIPageLayer);
			
			}
        }
        
        static IniterAdderUnityEngineVector2 s_IniterAdderUnityEngineVector2_dumb_obj = new IniterAdderUnityEngineVector2();
        static IniterAdderUnityEngineVector2 IniterAdderUnityEngineVector2_dumb_obj {get{return s_IniterAdderUnityEngineVector2_dumb_obj;}}
        
        
        int UnityEngineVector2_TypeID = -1;
        public void PushUnityEngineVector2(RealStatePtr L, UnityEngine.Vector2 val)
        {
            if (UnityEngineVector2_TypeID == -1)
            {
			    bool is_first;
                UnityEngineVector2_TypeID = getTypeId(L, typeof(UnityEngine.Vector2), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 8, UnityEngineVector2_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Vector2 ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Vector2 val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector2_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector2");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Vector2");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Vector2)objectCasters.GetCaster(typeof(UnityEngine.Vector2))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineVector2(RealStatePtr L, int index, UnityEngine.Vector2 val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector2_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector2");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Vector2 ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineVector3_TypeID = -1;
        public void PushUnityEngineVector3(RealStatePtr L, UnityEngine.Vector3 val)
        {
            if (UnityEngineVector3_TypeID == -1)
            {
			    bool is_first;
                UnityEngineVector3_TypeID = getTypeId(L, typeof(UnityEngine.Vector3), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 12, UnityEngineVector3_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Vector3 ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Vector3 val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector3_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector3");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Vector3");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Vector3)objectCasters.GetCaster(typeof(UnityEngine.Vector3))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineVector3(RealStatePtr L, int index, UnityEngine.Vector3 val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector3_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector3");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Vector3 ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineVector4_TypeID = -1;
        public void PushUnityEngineVector4(RealStatePtr L, UnityEngine.Vector4 val)
        {
            if (UnityEngineVector4_TypeID == -1)
            {
			    bool is_first;
                UnityEngineVector4_TypeID = getTypeId(L, typeof(UnityEngine.Vector4), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 16, UnityEngineVector4_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Vector4 ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Vector4 val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector4_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector4");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Vector4");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Vector4)objectCasters.GetCaster(typeof(UnityEngine.Vector4))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineVector4(RealStatePtr L, int index, UnityEngine.Vector4 val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector4_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector4");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Vector4 ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineColor_TypeID = -1;
        public void PushUnityEngineColor(RealStatePtr L, UnityEngine.Color val)
        {
            if (UnityEngineColor_TypeID == -1)
            {
			    bool is_first;
                UnityEngineColor_TypeID = getTypeId(L, typeof(UnityEngine.Color), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 16, UnityEngineColor_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Color ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Color val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineColor_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Color");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Color");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Color)objectCasters.GetCaster(typeof(UnityEngine.Color))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineColor(RealStatePtr L, int index, UnityEngine.Color val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineColor_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Color");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Color ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineQuaternion_TypeID = -1;
        public void PushUnityEngineQuaternion(RealStatePtr L, UnityEngine.Quaternion val)
        {
            if (UnityEngineQuaternion_TypeID == -1)
            {
			    bool is_first;
                UnityEngineQuaternion_TypeID = getTypeId(L, typeof(UnityEngine.Quaternion), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 16, UnityEngineQuaternion_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Quaternion ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Quaternion val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineQuaternion_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Quaternion");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Quaternion");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Quaternion)objectCasters.GetCaster(typeof(UnityEngine.Quaternion))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineQuaternion(RealStatePtr L, int index, UnityEngine.Quaternion val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineQuaternion_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Quaternion");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Quaternion ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineRay_TypeID = -1;
        public void PushUnityEngineRay(RealStatePtr L, UnityEngine.Ray val)
        {
            if (UnityEngineRay_TypeID == -1)
            {
			    bool is_first;
                UnityEngineRay_TypeID = getTypeId(L, typeof(UnityEngine.Ray), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 24, UnityEngineRay_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Ray ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Ray val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRay_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Ray");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Ray");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Ray)objectCasters.GetCaster(typeof(UnityEngine.Ray))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineRay(RealStatePtr L, int index, UnityEngine.Ray val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRay_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Ray");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Ray ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineBounds_TypeID = -1;
        public void PushUnityEngineBounds(RealStatePtr L, UnityEngine.Bounds val)
        {
            if (UnityEngineBounds_TypeID == -1)
            {
			    bool is_first;
                UnityEngineBounds_TypeID = getTypeId(L, typeof(UnityEngine.Bounds), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 24, UnityEngineBounds_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Bounds ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Bounds val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineBounds_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Bounds");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Bounds");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Bounds)objectCasters.GetCaster(typeof(UnityEngine.Bounds))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineBounds(RealStatePtr L, int index, UnityEngine.Bounds val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineBounds_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Bounds");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Bounds ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineRay2D_TypeID = -1;
        public void PushUnityEngineRay2D(RealStatePtr L, UnityEngine.Ray2D val)
        {
            if (UnityEngineRay2D_TypeID == -1)
            {
			    bool is_first;
                UnityEngineRay2D_TypeID = getTypeId(L, typeof(UnityEngine.Ray2D), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 16, UnityEngineRay2D_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Ray2D ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.Ray2D val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRay2D_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Ray2D");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Ray2D");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Ray2D)objectCasters.GetCaster(typeof(UnityEngine.Ray2D))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineRay2D(RealStatePtr L, int index, UnityEngine.Ray2D val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRay2D_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Ray2D");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Ray2D ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int GameLuaLuaEventType_TypeID = -1;
		int GameLuaLuaEventType_EnumRef = -1;
        
        public void PushGameLuaLuaEventType(RealStatePtr L, GameLua.LuaEventType val)
        {
            if (GameLuaLuaEventType_TypeID == -1)
            {
			    bool is_first;
                GameLuaLuaEventType_TypeID = getTypeId(L, typeof(GameLua.LuaEventType), out is_first);
				
				if (GameLuaLuaEventType_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(GameLua.LuaEventType));
				    GameLuaLuaEventType_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, GameLuaLuaEventType_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, GameLuaLuaEventType_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for GameLua.LuaEventType ,value="+val);
            }
			
			LuaAPI.lua_getref(L, GameLuaLuaEventType_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out GameLua.LuaEventType val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != GameLuaLuaEventType_TypeID)
				{
				    throw new Exception("invalid userdata for GameLua.LuaEventType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for GameLua.LuaEventType");
                }
				val = (GameLua.LuaEventType)e;
                
            }
            else
            {
                val = (GameLua.LuaEventType)objectCasters.GetCaster(typeof(GameLua.LuaEventType))(L, index, null);
            }
        }
		
        public void UpdateGameLuaLuaEventType(RealStatePtr L, int index, GameLua.LuaEventType val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != GameLuaLuaEventType_TypeID)
				{
				    throw new Exception("invalid userdata for GameLua.LuaEventType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for GameLua.LuaEventType ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int GameLuaExpressArgs_TypeID = -1;
        public void PushGameLuaExpressArgs(RealStatePtr L, GameLua.ExpressArgs val)
        {
            if (GameLuaExpressArgs_TypeID == -1)
            {
			    bool is_first;
                GameLuaExpressArgs_TypeID = getTypeId(L, typeof(GameLua.ExpressArgs), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 96, GameLuaExpressArgs_TypeID);
            if (!CopyByValue.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for GameLua.ExpressArgs ,value="+val);
            }
			
        }
		
        public void Get(RealStatePtr L, int index, out GameLua.ExpressArgs val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != GameLuaExpressArgs_TypeID)
				{
				    throw new Exception("invalid userdata for GameLua.ExpressArgs");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for GameLua.ExpressArgs");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue.UnPack(this, L, index, out val);
			}
            else
            {
                val = (GameLua.ExpressArgs)objectCasters.GetCaster(typeof(GameLua.ExpressArgs))(L, index, null);
            }
        }
		
        public void UpdateGameLuaExpressArgs(RealStatePtr L, int index, GameLua.ExpressArgs val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != GameLuaExpressArgs_TypeID)
				{
				    throw new Exception("invalid userdata for GameLua.ExpressArgs");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for GameLua.ExpressArgs ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int UnityEngineNetworkReachability_TypeID = -1;
		int UnityEngineNetworkReachability_EnumRef = -1;
        
        public void PushUnityEngineNetworkReachability(RealStatePtr L, UnityEngine.NetworkReachability val)
        {
            if (UnityEngineNetworkReachability_TypeID == -1)
            {
			    bool is_first;
                UnityEngineNetworkReachability_TypeID = getTypeId(L, typeof(UnityEngine.NetworkReachability), out is_first);
				
				if (UnityEngineNetworkReachability_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.NetworkReachability));
				    UnityEngineNetworkReachability_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineNetworkReachability_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineNetworkReachability_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.NetworkReachability ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineNetworkReachability_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out UnityEngine.NetworkReachability val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineNetworkReachability_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.NetworkReachability");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.NetworkReachability");
                }
				val = (UnityEngine.NetworkReachability)e;
                
            }
            else
            {
                val = (UnityEngine.NetworkReachability)objectCasters.GetCaster(typeof(UnityEngine.NetworkReachability))(L, index, null);
            }
        }
		
        public void UpdateUnityEngineNetworkReachability(RealStatePtr L, int index, UnityEngine.NetworkReachability val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineNetworkReachability_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.NetworkReachability");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.NetworkReachability ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int FairyGUIRelationType_TypeID = -1;
		int FairyGUIRelationType_EnumRef = -1;
        
        public void PushFairyGUIRelationType(RealStatePtr L, FairyGUI.RelationType val)
        {
            if (FairyGUIRelationType_TypeID == -1)
            {
			    bool is_first;
                FairyGUIRelationType_TypeID = getTypeId(L, typeof(FairyGUI.RelationType), out is_first);
				
				if (FairyGUIRelationType_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(FairyGUI.RelationType));
				    FairyGUIRelationType_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, FairyGUIRelationType_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, FairyGUIRelationType_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for FairyGUI.RelationType ,value="+val);
            }
			
			LuaAPI.lua_getref(L, FairyGUIRelationType_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out FairyGUI.RelationType val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != FairyGUIRelationType_TypeID)
				{
				    throw new Exception("invalid userdata for FairyGUI.RelationType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for FairyGUI.RelationType");
                }
				val = (FairyGUI.RelationType)e;
                
            }
            else
            {
                val = (FairyGUI.RelationType)objectCasters.GetCaster(typeof(FairyGUI.RelationType))(L, index, null);
            }
        }
		
        public void UpdateFairyGUIRelationType(RealStatePtr L, int index, FairyGUI.RelationType val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != FairyGUIRelationType_TypeID)
				{
				    throw new Exception("invalid userdata for FairyGUI.RelationType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for FairyGUI.RelationType ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int FairyGUIFitScreen_TypeID = -1;
		int FairyGUIFitScreen_EnumRef = -1;
        
        public void PushFairyGUIFitScreen(RealStatePtr L, FairyGUI.FitScreen val)
        {
            if (FairyGUIFitScreen_TypeID == -1)
            {
			    bool is_first;
                FairyGUIFitScreen_TypeID = getTypeId(L, typeof(FairyGUI.FitScreen), out is_first);
				
				if (FairyGUIFitScreen_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(FairyGUI.FitScreen));
				    FairyGUIFitScreen_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, FairyGUIFitScreen_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, FairyGUIFitScreen_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for FairyGUI.FitScreen ,value="+val);
            }
			
			LuaAPI.lua_getref(L, FairyGUIFitScreen_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out FairyGUI.FitScreen val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != FairyGUIFitScreen_TypeID)
				{
				    throw new Exception("invalid userdata for FairyGUI.FitScreen");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for FairyGUI.FitScreen");
                }
				val = (FairyGUI.FitScreen)e;
                
            }
            else
            {
                val = (FairyGUI.FitScreen)objectCasters.GetCaster(typeof(FairyGUI.FitScreen))(L, index, null);
            }
        }
		
        public void UpdateFairyGUIFitScreen(RealStatePtr L, int index, FairyGUI.FitScreen val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != FairyGUIFitScreen_TypeID)
				{
				    throw new Exception("invalid userdata for FairyGUI.FitScreen");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for FairyGUI.FitScreen ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int FairyGUIUIContentScalerScreenMatchMode_TypeID = -1;
		int FairyGUIUIContentScalerScreenMatchMode_EnumRef = -1;
        
        public void PushFairyGUIUIContentScalerScreenMatchMode(RealStatePtr L, FairyGUI.UIContentScaler.ScreenMatchMode val)
        {
            if (FairyGUIUIContentScalerScreenMatchMode_TypeID == -1)
            {
			    bool is_first;
                FairyGUIUIContentScalerScreenMatchMode_TypeID = getTypeId(L, typeof(FairyGUI.UIContentScaler.ScreenMatchMode), out is_first);
				
				if (FairyGUIUIContentScalerScreenMatchMode_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(FairyGUI.UIContentScaler.ScreenMatchMode));
				    FairyGUIUIContentScalerScreenMatchMode_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, FairyGUIUIContentScalerScreenMatchMode_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, FairyGUIUIContentScalerScreenMatchMode_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for FairyGUI.UIContentScaler.ScreenMatchMode ,value="+val);
            }
			
			LuaAPI.lua_getref(L, FairyGUIUIContentScalerScreenMatchMode_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out FairyGUI.UIContentScaler.ScreenMatchMode val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != FairyGUIUIContentScalerScreenMatchMode_TypeID)
				{
				    throw new Exception("invalid userdata for FairyGUI.UIContentScaler.ScreenMatchMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for FairyGUI.UIContentScaler.ScreenMatchMode");
                }
				val = (FairyGUI.UIContentScaler.ScreenMatchMode)e;
                
            }
            else
            {
                val = (FairyGUI.UIContentScaler.ScreenMatchMode)objectCasters.GetCaster(typeof(FairyGUI.UIContentScaler.ScreenMatchMode))(L, index, null);
            }
        }
		
        public void UpdateFairyGUIUIContentScalerScreenMatchMode(RealStatePtr L, int index, FairyGUI.UIContentScaler.ScreenMatchMode val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != FairyGUIUIContentScalerScreenMatchMode_TypeID)
				{
				    throw new Exception("invalid userdata for FairyGUI.UIContentScaler.ScreenMatchMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for FairyGUI.UIContentScaler.ScreenMatchMode ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int FairyGUIListSelectionMode_TypeID = -1;
		int FairyGUIListSelectionMode_EnumRef = -1;
        
        public void PushFairyGUIListSelectionMode(RealStatePtr L, FairyGUI.ListSelectionMode val)
        {
            if (FairyGUIListSelectionMode_TypeID == -1)
            {
			    bool is_first;
                FairyGUIListSelectionMode_TypeID = getTypeId(L, typeof(FairyGUI.ListSelectionMode), out is_first);
				
				if (FairyGUIListSelectionMode_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(FairyGUI.ListSelectionMode));
				    FairyGUIListSelectionMode_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, FairyGUIListSelectionMode_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, FairyGUIListSelectionMode_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for FairyGUI.ListSelectionMode ,value="+val);
            }
			
			LuaAPI.lua_getref(L, FairyGUIListSelectionMode_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out FairyGUI.ListSelectionMode val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != FairyGUIListSelectionMode_TypeID)
				{
				    throw new Exception("invalid userdata for FairyGUI.ListSelectionMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for FairyGUI.ListSelectionMode");
                }
				val = (FairyGUI.ListSelectionMode)e;
                
            }
            else
            {
                val = (FairyGUI.ListSelectionMode)objectCasters.GetCaster(typeof(FairyGUI.ListSelectionMode))(L, index, null);
            }
        }
		
        public void UpdateFairyGUIListSelectionMode(RealStatePtr L, int index, FairyGUI.ListSelectionMode val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != FairyGUIListSelectionMode_TypeID)
				{
				    throw new Exception("invalid userdata for FairyGUI.ListSelectionMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for FairyGUI.ListSelectionMode ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int GameNetworkType_TypeID = -1;
		int GameNetworkType_EnumRef = -1;
        
        public void PushGameNetworkType(RealStatePtr L, Game.NetworkType val)
        {
            if (GameNetworkType_TypeID == -1)
            {
			    bool is_first;
                GameNetworkType_TypeID = getTypeId(L, typeof(Game.NetworkType), out is_first);
				
				if (GameNetworkType_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(Game.NetworkType));
				    GameNetworkType_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, GameNetworkType_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, GameNetworkType_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for Game.NetworkType ,value="+val);
            }
			
			LuaAPI.lua_getref(L, GameNetworkType_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out Game.NetworkType val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != GameNetworkType_TypeID)
				{
				    throw new Exception("invalid userdata for Game.NetworkType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for Game.NetworkType");
                }
				val = (Game.NetworkType)e;
                
            }
            else
            {
                val = (Game.NetworkType)objectCasters.GetCaster(typeof(Game.NetworkType))(L, index, null);
            }
        }
		
        public void UpdateGameNetworkType(RealStatePtr L, int index, Game.NetworkType val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != GameNetworkType_TypeID)
				{
				    throw new Exception("invalid userdata for Game.NetworkType");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for Game.NetworkType ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        int GameUIPageLayer_TypeID = -1;
		int GameUIPageLayer_EnumRef = -1;
        
        public void PushGameUIPageLayer(RealStatePtr L, Game.UIPageLayer val)
        {
            if (GameUIPageLayer_TypeID == -1)
            {
			    bool is_first;
                GameUIPageLayer_TypeID = getTypeId(L, typeof(Game.UIPageLayer), out is_first);
				
				if (GameUIPageLayer_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(Game.UIPageLayer));
				    GameUIPageLayer_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, GameUIPageLayer_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, GameUIPageLayer_TypeID);
            if (!CopyByValue.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for Game.UIPageLayer ,value="+val);
            }
			
			LuaAPI.lua_getref(L, GameUIPageLayer_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public void Get(RealStatePtr L, int index, out Game.UIPageLayer val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != GameUIPageLayer_TypeID)
				{
				    throw new Exception("invalid userdata for Game.UIPageLayer");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for Game.UIPageLayer");
                }
				val = (Game.UIPageLayer)e;
                
            }
            else
            {
                val = (Game.UIPageLayer)objectCasters.GetCaster(typeof(Game.UIPageLayer))(L, index, null);
            }
        }
		
        public void UpdateGameUIPageLayer(RealStatePtr L, int index, Game.UIPageLayer val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != GameUIPageLayer_TypeID)
				{
				    throw new Exception("invalid userdata for Game.UIPageLayer");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for Game.UIPageLayer ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        
		// table cast optimze
		
        
    }
	
	public partial class StaticLuaCallbacks
    {
	    internal static bool __tryArrayGet(Type type, RealStatePtr L, ObjectTranslator translator, object obj, int index)
		{
		
			if (type == typeof(UnityEngine.Vector2[]))
			{
			    UnityEngine.Vector2[] array = obj as UnityEngine.Vector2[];
				translator.PushUnityEngineVector2(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Vector3[]))
			{
			    UnityEngine.Vector3[] array = obj as UnityEngine.Vector3[];
				translator.PushUnityEngineVector3(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Vector4[]))
			{
			    UnityEngine.Vector4[] array = obj as UnityEngine.Vector4[];
				translator.PushUnityEngineVector4(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Color[]))
			{
			    UnityEngine.Color[] array = obj as UnityEngine.Color[];
				translator.PushUnityEngineColor(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Quaternion[]))
			{
			    UnityEngine.Quaternion[] array = obj as UnityEngine.Quaternion[];
				translator.PushUnityEngineQuaternion(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Ray[]))
			{
			    UnityEngine.Ray[] array = obj as UnityEngine.Ray[];
				translator.PushUnityEngineRay(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Bounds[]))
			{
			    UnityEngine.Bounds[] array = obj as UnityEngine.Bounds[];
				translator.PushUnityEngineBounds(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Ray2D[]))
			{
			    UnityEngine.Ray2D[] array = obj as UnityEngine.Ray2D[];
				translator.PushUnityEngineRay2D(L, array[index]);
				return true;
			}
			else if (type == typeof(GameLua.LuaEventType[]))
			{
			    GameLua.LuaEventType[] array = obj as GameLua.LuaEventType[];
				translator.PushGameLuaLuaEventType(L, array[index]);
				return true;
			}
			else if (type == typeof(GameLua.ExpressArgs[]))
			{
			    GameLua.ExpressArgs[] array = obj as GameLua.ExpressArgs[];
				translator.PushGameLuaExpressArgs(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.NetworkReachability[]))
			{
			    UnityEngine.NetworkReachability[] array = obj as UnityEngine.NetworkReachability[];
				translator.PushUnityEngineNetworkReachability(L, array[index]);
				return true;
			}
			else if (type == typeof(FairyGUI.RelationType[]))
			{
			    FairyGUI.RelationType[] array = obj as FairyGUI.RelationType[];
				translator.PushFairyGUIRelationType(L, array[index]);
				return true;
			}
			else if (type == typeof(FairyGUI.FitScreen[]))
			{
			    FairyGUI.FitScreen[] array = obj as FairyGUI.FitScreen[];
				translator.PushFairyGUIFitScreen(L, array[index]);
				return true;
			}
			else if (type == typeof(FairyGUI.UIContentScaler.ScreenMatchMode[]))
			{
			    FairyGUI.UIContentScaler.ScreenMatchMode[] array = obj as FairyGUI.UIContentScaler.ScreenMatchMode[];
				translator.PushFairyGUIUIContentScalerScreenMatchMode(L, array[index]);
				return true;
			}
			else if (type == typeof(FairyGUI.ListSelectionMode[]))
			{
			    FairyGUI.ListSelectionMode[] array = obj as FairyGUI.ListSelectionMode[];
				translator.PushFairyGUIListSelectionMode(L, array[index]);
				return true;
			}
			else if (type == typeof(Game.NetworkType[]))
			{
			    Game.NetworkType[] array = obj as Game.NetworkType[];
				translator.PushGameNetworkType(L, array[index]);
				return true;
			}
			else if (type == typeof(Game.UIPageLayer[]))
			{
			    Game.UIPageLayer[] array = obj as Game.UIPageLayer[];
				translator.PushGameUIPageLayer(L, array[index]);
				return true;
			}
            return false;
		}
		
		internal static bool __tryArraySet(Type type, RealStatePtr L, ObjectTranslator translator, object obj, int array_idx, int obj_idx)
		{
		
			if (type == typeof(UnityEngine.Vector2[]))
			{
			    UnityEngine.Vector2[] array = obj as UnityEngine.Vector2[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Vector3[]))
			{
			    UnityEngine.Vector3[] array = obj as UnityEngine.Vector3[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Vector4[]))
			{
			    UnityEngine.Vector4[] array = obj as UnityEngine.Vector4[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Color[]))
			{
			    UnityEngine.Color[] array = obj as UnityEngine.Color[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Quaternion[]))
			{
			    UnityEngine.Quaternion[] array = obj as UnityEngine.Quaternion[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Ray[]))
			{
			    UnityEngine.Ray[] array = obj as UnityEngine.Ray[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Bounds[]))
			{
			    UnityEngine.Bounds[] array = obj as UnityEngine.Bounds[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Ray2D[]))
			{
			    UnityEngine.Ray2D[] array = obj as UnityEngine.Ray2D[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(GameLua.LuaEventType[]))
			{
			    GameLua.LuaEventType[] array = obj as GameLua.LuaEventType[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(GameLua.ExpressArgs[]))
			{
			    GameLua.ExpressArgs[] array = obj as GameLua.ExpressArgs[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.NetworkReachability[]))
			{
			    UnityEngine.NetworkReachability[] array = obj as UnityEngine.NetworkReachability[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(FairyGUI.RelationType[]))
			{
			    FairyGUI.RelationType[] array = obj as FairyGUI.RelationType[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(FairyGUI.FitScreen[]))
			{
			    FairyGUI.FitScreen[] array = obj as FairyGUI.FitScreen[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(FairyGUI.UIContentScaler.ScreenMatchMode[]))
			{
			    FairyGUI.UIContentScaler.ScreenMatchMode[] array = obj as FairyGUI.UIContentScaler.ScreenMatchMode[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(FairyGUI.ListSelectionMode[]))
			{
			    FairyGUI.ListSelectionMode[] array = obj as FairyGUI.ListSelectionMode[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(Game.NetworkType[]))
			{
			    Game.NetworkType[] array = obj as Game.NetworkType[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(Game.UIPageLayer[]))
			{
			    Game.UIPageLayer[] array = obj as Game.UIPageLayer[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
            return false;
		}
	}
}