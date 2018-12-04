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
using System.Collections.Generic;
using System.Reflection;


namespace XLua.CSObjectWrap
{
    public class XLua_Gen_Initer_Register__
	{
	    static XLua_Gen_Initer_Register__()
        {
		    XLua.LuaEnv.AddIniter((luaenv, translator) => {
			    
				translator.DelayWrapLoader(typeof(Game.GrassManager), GameGrassManagerWrap.__Register);
				
				translator.DelayWrapLoader(typeof(GameInterface), GameInterfaceWrap.__Register);
				
				translator.DelayWrapLoader(typeof(GameLua.LuaEventType), GameLuaLuaEventTypeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(GameLua.ExpressArgs), GameLuaExpressArgsWrap.__Register);
				
				translator.DelayWrapLoader(typeof(GameLua.TipArgs), GameLuaTipArgsWrap.__Register);
				
				translator.DelayWrapLoader(typeof(XluaHotFixTest), XluaHotFixTestWrap.__Register);
				
				translator.DelayWrapLoader(typeof(object), SystemObjectWrap.__Register);
				
				translator.DelayWrapLoader(typeof(System.Collections.Generic.List<int>), SystemCollectionsGenericList_1_SystemInt32_Wrap.__Register);
				
				translator.DelayWrapLoader(typeof(System.IO.File), SystemIOFileWrap.__Register);
				
				translator.DelayWrapLoader(typeof(System.Type), SystemTypeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(System.IO.Path), SystemIOPathWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.SceneManagement.SceneManager), UnityEngineSceneManagementSceneManagerWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Networking.UnityWebRequest), UnityEngineNetworkingUnityWebRequestWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Physics), UnityEnginePhysicsWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Object), UnityEngineObjectWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Input), UnityEngineInputWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Vector2), UnityEngineVector2Wrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Vector3), UnityEngineVector3Wrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Vector4), UnityEngineVector4Wrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Quaternion), UnityEngineQuaternionWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Color), UnityEngineColorWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Ray), UnityEngineRayWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Bounds), UnityEngineBoundsWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Ray2D), UnityEngineRay2DWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Time), UnityEngineTimeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.GameObject), UnityEngineGameObjectWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Component), UnityEngineComponentWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Behaviour), UnityEngineBehaviourWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Transform), UnityEngineTransformWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Resources), UnityEngineResourcesWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.TextAsset), UnityEngineTextAssetWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Keyframe), UnityEngineKeyframeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.AnimationCurve), UnityEngineAnimationCurveWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.AnimationClip), UnityEngineAnimationClipWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.MonoBehaviour), UnityEngineMonoBehaviourWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.ParticleSystem), UnityEngineParticleSystemWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.SkinnedMeshRenderer), UnityEngineSkinnedMeshRendererWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Renderer), UnityEngineRendererWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.WWW), UnityEngineWWWWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Debug), UnityEngineDebugWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.Application), UnityEngineApplicationWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.PlayerPrefs), UnityEnginePlayerPrefsWrap.__Register);
				
				translator.DelayWrapLoader(typeof(UnityEngine.NetworkReachability), UnityEngineNetworkReachabilityWrap.__Register);
				
				translator.DelayWrapLoader(typeof(WordsCheck.NameCheck), WordsCheckNameCheckWrap.__Register);
				
				translator.DelayWrapLoader(typeof(WordsCheck.Config), WordsCheckConfigWrap.__Register);
				
				translator.DelayWrapLoader(typeof(GameUtil.AccountIMChecker), GameUtilAccountIMCheckerWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Game.FileManager), GameFileManagerWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.EventDispatcher), FairyGUIEventDispatcherWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.EventListener), FairyGUIEventListenerWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.InputEvent), FairyGUIInputEventWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.DisplayObject), FairyGUIDisplayObjectWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.Container), FairyGUIContainerWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.Stage), FairyGUIStageWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.Controller), FairyGUIControllerWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.GObject), FairyGUIGObjectWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.GGraph), FairyGUIGGraphWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.GGroup), FairyGUIGGroupWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.GImage), FairyGUIGImageWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.GLoader), FairyGUIGLoaderWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.PlayState), FairyGUIPlayStateWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.GMovieClip), FairyGUIGMovieClipWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.TextFormat), FairyGUITextFormatWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.GTextField), FairyGUIGTextFieldWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.GRichTextField), FairyGUIGRichTextFieldWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.GTextInput), FairyGUIGTextInputWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.GComponent), FairyGUIGComponentWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.GList), FairyGUIGListWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.GRoot), FairyGUIGRootWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.GLabel), FairyGUIGLabelWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.GButton), FairyGUIGButtonWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.GComboBox), FairyGUIGComboBoxWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.GProgressBar), FairyGUIGProgressBarWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.GSlider), FairyGUIGSliderWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.PopupMenu), FairyGUIPopupMenuWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.ScrollPane), FairyGUIScrollPaneWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.Transition), FairyGUITransitionWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.UIPackage), FairyGUIUIPackageWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.UIConfig), FairyGUIUIConfigWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.Shape), FairyGUIShapeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.GoWrapper), FairyGUIGoWrapperWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.Window), FairyGUIWindowWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.GObjectPool), FairyGUIGObjectPoolWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.Relations), FairyGUIRelationsWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.RelationType), FairyGUIRelationTypeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.UIPanel), FairyGUIUIPanelWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.FitScreen), FairyGUIFitScreenWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.EventContext), FairyGUIEventContextWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.UIContentScaler.ScreenMatchMode), FairyGUIUIContentScalerScreenMatchModeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(FairyGUI.ListSelectionMode), FairyGUIListSelectionModeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Google.Protobuf.ProtobufManager), GoogleProtobufProtobufManagerWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Game.NetworkType), GameNetworkTypeWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Game.NetworkManager), GameNetworkManagerWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Game.PlayerSettingManager), GamePlayerSettingManagerWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Game.UIGoWrapper), GameUIGoWrapperWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Game.UIManager), GameUIManagerWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Game.UIWindow), GameUIWindowWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Game.VersionManager), GameVersionManagerWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Game.UIPageLayer), GameUIPageLayerWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Game.UIPage), GameUIPageWrap.__Register);
				
				translator.DelayWrapLoader(typeof(Game.GameEnvSetting), GameGameEnvSettingWrap.__Register);
				
				
				
				translator.AddInterfaceBridgeCreator(typeof(System.Collections.IEnumerator), SystemCollectionsIEnumeratorBridge.__Create);
				
			});
		}
		
		
	}
	
}
namespace XLua
{
	public partial class ObjectTranslator
	{
		static XLua.CSObjectWrap.XLua_Gen_Initer_Register__ s_gen_reg_dumb_obj = new XLua.CSObjectWrap.XLua_Gen_Initer_Register__();
		static XLua.CSObjectWrap.XLua_Gen_Initer_Register__ gen_reg_dumb_obj {get{return s_gen_reg_dumb_obj;}}
	}
	
	internal partial class InternalGlobals
    {
	    
	    static InternalGlobals()
		{
		    extensionMethodMap = new Dictionary<Type, IEnumerable<MethodInfo>>()
			{
			    
			};
			
			genTryArrayGetPtr = StaticLuaCallbacks.__tryArrayGet;
            genTryArraySetPtr = StaticLuaCallbacks.__tryArraySet;
		}
	}
}
