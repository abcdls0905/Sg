using System.Collections.Generic;
using System;
using UnityEngine;
using XLua;
using System.Reflection;
using System.Linq;

/*
热更新规则：

不热更：
1. warp文件不热更
2. 第三方库不热更
3. 读档文件不热更
4. Protobuf生成文件不热更
5. ECS组件不热更

热更：
1. ECS系统必须热更（System）stateless
2. Page必须热更 stateful

Warp:
1. 整个ECS系统
2. Manager

其他：
Page参数必须配置化
*/
public static class ExampleGenConfig
{
    //lua中要使用到C#库的配置，比如C#标准库，或者Unity API，第三方库等。
    [LuaCallCSharp]
    public static List<Type> LuaCallCSharp = new List<Type>() {
                //其他
                typeof(XluaHotFixTest),
                //System
                typeof(System.Object),
                typeof(System.Collections.Generic.List<int>),
                typeof(System.IO.File),
                typeof(System.Type),
                typeof(System.IO.Path),
                //UnityEngin
                typeof(UnityEngine.SceneManagement.SceneManager),
                typeof(UnityEngine.Networking.UnityWebRequest),
                typeof(UnityEngine.Physics),
                typeof(UnityEngine.Object),
                typeof(UnityEngine.Input),
                typeof(Vector2),
                typeof(Vector3),
                typeof(Vector4),
                typeof(Quaternion),
                typeof(Color),
                typeof(Ray),
                typeof(Bounds),
                typeof(Ray2D),
                typeof(Time),
                typeof(GameObject),
                typeof(Component),
                typeof(Behaviour),
                typeof(Transform),
                typeof(Resources),
                typeof(TextAsset),
                typeof(Keyframe),
                typeof(AnimationCurve),
                typeof(AnimationClip),
                typeof(MonoBehaviour),
                typeof(ParticleSystem),
                typeof(SkinnedMeshRenderer),
                typeof(Renderer),
                typeof(WWW),
                typeof(UnityEngine.Debug),
                typeof(Application),
                typeof(PlayerPrefs),
                typeof(NetworkReachability),
                //WordsCheck
                typeof(WordsCheck.NameCheck),
                typeof(WordsCheck.Config),
                //GameUtil
                typeof(GameUtil.AccountIMChecker),
                //Game
                typeof(Game.FileManager),
                //FairyGUI
                typeof(FairyGUI.EventDispatcher),
                typeof(FairyGUI.EventListener),
                typeof(FairyGUI.InputEvent),
                typeof(FairyGUI.DisplayObject),
                typeof(FairyGUI.Container),
                typeof(FairyGUI.Stage),
                typeof(FairyGUI.Controller),
                typeof(FairyGUI.GObject),
                typeof(FairyGUI.GGraph),
                typeof(FairyGUI.GGroup),
                typeof(FairyGUI.GImage),
                typeof(FairyGUI.GLoader),
                typeof(FairyGUI.PlayState),
                typeof(FairyGUI.GMovieClip),
                typeof(FairyGUI.TextFormat),
                typeof(FairyGUI.GTextField),
                typeof(FairyGUI.GRichTextField),
                typeof(FairyGUI.GTextInput),
                typeof(FairyGUI.GComponent),
                typeof(FairyGUI.GList),
                typeof(FairyGUI.GRoot),
                typeof(FairyGUI.GLabel),
                typeof(FairyGUI.GButton),
                typeof(FairyGUI.GComboBox),
                typeof(FairyGUI.GProgressBar),
                typeof(FairyGUI.GSlider),
                typeof(FairyGUI.PopupMenu),
                typeof(FairyGUI.ScrollPane),
                typeof(FairyGUI.Transition),
                typeof(FairyGUI.UIPackage),
                typeof(FairyGUI.UIConfig),
                typeof(FairyGUI.Shape),
                typeof(FairyGUI.GoWrapper),
                typeof(FairyGUI.Window),
                typeof(FairyGUI.GObjectPool),
                typeof(FairyGUI.Relations),
                typeof(FairyGUI.RelationType),
                typeof(FairyGUI.UIPanel),
                typeof(FairyGUI.FitScreen),
                typeof(FairyGUI.EventContext),
                typeof(FairyGUI.UIContentScaler.ScreenMatchMode),
                typeof(FairyGUI.ListSelectionMode),
                //Goole
                typeof(Google.Protobuf.ProtobufManager),
            };

    //C#静态调用Lua的配置（包括事件的原型），仅可以配delegate，interface
    [CSharpCallLua]
    public static List<Type> CSharpCallLua = new List<Type>() {
                typeof(Action),
                typeof(Action<string>),
                typeof(Func<double, double, double>),
                typeof(Action<string>),
                typeof(Action<double>),
                typeof(Action<bool>),
                typeof(Action<UnityEngine.NetworkReachability>),
                typeof(Action<Game.NetworkType>),
                typeof(Action<Game.NetworkType, bool, bool>),
                typeof(Action<ushort, byte[]>),
                typeof(UnityEngine.Events.UnityAction),
                typeof(System.Collections.IEnumerator),

                typeof(FairyGUI.PlayCompleteCallback),
                typeof(FairyGUI.EventCallback0),
                typeof(FairyGUI.EventCallback1),
                typeof(FairyGUI.TimerCallback),
                typeof(FairyGUI.ListItemRenderer),
            };

    //黑名单
    [BlackList]
    public static List<List<string>> BlackList = new List<List<string>>()  {
                new List<string>(){"UnityEngine.WWW", "movie"},
                new List<string>(){"UnityEngine.Texture2D", "alphaIsTransparency"},
                new List<string>(){"UnityEngine.Security", "GetChainOfTrustValue"},
                new List<string>(){"UnityEngine.CanvasRenderer", "onRequestRebuild"},
                new List<string>(){"UnityEngine.Light", "areaSize"},
                new List<string>(){"UnityEngine.AnimatorOverrideController", "PerformOverrideClipListCleanup"},
    #if !UNITY_WEBPLAYER
                new List<string>(){"UnityEngine.Application", "ExternalEval"},
    #endif
                new List<string>(){"UnityEngine.GameObject", "networkView"}, //4.6.2 not support
                new List<string>(){"UnityEngine.Component", "networkView"},  //4.6.2 not support
                new List<string>(){"System.IO.FileInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
                new List<string>(){"System.IO.FileInfo", "SetAccessControl", "System.Security.AccessControl.FileSecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
                new List<string>(){"System.IO.DirectoryInfo", "SetAccessControl", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "CreateSubdirectory", "System.String", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "Create", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"UnityEngine.MonoBehaviour", "runInEditMode"},
                new List<string>() {"System.IO.File", "Create", "System.String"},
                new List<string>() {"System.IO.File", "Create", "System.String", "System.Int32"},
                new List<string>() {"System.IO.File", "Create", "System.String", "System.Int32", "System.IO.FileOptions"},
                new List<string>() {"System.IO.File", "Create", "System.String", "System.Int32", "System.IO.FileOptions", "System.Security.AccessControl.FileSecurity"},
                new List<string>() {"System.IO.File", "GetAccessControl", "System.String" },
                new List<string>() {"System.IO.File", "GetAccessControl", "System.String", "System.Security.AccessControl.AccessControlSections" },
                new List<string>() {"System.IO.File", "SetAccessControl", "System.String", "System.Security.AccessControl.FileSecurity" },
                new List<string>() { "UnityEngine.Input", "IsJoystickPreconfigured", "System.String" },
                
                //UNITY_EDITOR下函数
                new List<string>() { "AkGameObj", "RefreshListeners"},
                new List<string>() { "AkGameObj", "OnDrawGizmosSelected"},
                new List<string>() { "AkGameObj", "Migrate9"},
                new List<string>() { "AkGameObj", "Migrate10"},
                new List<string>() { "AkGameObj", "PreMigration14"},
                new List<string>() { "AkGameObj", "Migrate14"},
                new List<string>() { "AkGameObj", "PostMigration14"},
                new List<string>() { "EVP.VehicleController", "OnDrawGizmos"},
                new List<string>() { "UnityEngine.Terrain", "bakeLightProbesForTrees"},
                new List<string>() { "AkEnvironmentPortal", "envList"},
                new List<string>() { "AkEnvironment", "valueGuid"},
                new List<string>() { "AkGameObjListenerList", "AddToInitialListenerList", "AkAudioListener"},
                new List<string>() { "AkGameObjListenerList", "RemoveFromInitialListenerList", "AkAudioListener"},
                new List<string>() {"UnityEngine.WWW", "GetMovieTexture"},
            };

    [Hotfix]
    public static List<Type> HitFixCustom
    {
        get
        {
            return (from type in Assembly.Load("Assembly-CSharp").GetTypes()
                    where (type.Namespace == "Game")
                    select type).ToList();
        }
    }

    private static HashSet<string> dirtyTypes = new HashSet<string>();


    private static bool IsReliableType(Type value, ref HashSet<Type> hasSets, ref Assembly onlyAssembly)
    {
        if (value == null || (!value.IsPublic && !value.IsNestedPublic)) return false;
        if (value == typeof(void)) return false;//value.IsGenericParameter ||
        if (value.Assembly != onlyAssembly && value.Namespace != "UnityEngine") return false;
        return true;
    }

    private static void AddType(Type value, ref HashSet<Type> hasSets, ref Assembly onlyAssembly)
    {
        if (!IsReliableType(value, ref hasSets, ref onlyAssembly)) return;
        hasSets.Add(value);
    }

    private static void GetReliableClass(Type valueClass, ref HashSet<Type> hasSets, ref Assembly onlyAssembly)
    {
        var memInfos = valueClass.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Static);
        if (memInfos == null) return;
        foreach (var memInfo in memInfos)
        {
            switch (memInfo.MemberType)
            {
                case MemberTypes.Field:
                    {
                        var fieldInfo = memInfo as FieldInfo;
                        GetReliableType(fieldInfo.FieldType, ref hasSets, ref onlyAssembly);
                    }
                    break;
                case MemberTypes.Method:
                    {
                        var methodInfo = memInfo as MethodInfo;
                        //1、返回值
                        var res = methodInfo.ReturnType;
                        if (res != null)
                            GetReliableType(res, ref hasSets, ref onlyAssembly);
                        //2、参数
                        var param = methodInfo.GetParameters();
                        if (param != null)
                        {
                            for (int j = 0; j < param.Length; j++)
                            {
                                var paramType = param[j].ParameterType;
                                if (paramType != null)
                                    GetReliableType(paramType, ref hasSets, ref onlyAssembly);
                            }
                        }
                        //3、函数体
                        var body = methodInfo.GetMethodBody();
                        if (body != null && body.LocalVariables != null)
                        {
                            foreach (var loc in body.LocalVariables)
                            {
                                if (loc.LocalType != null)
                                    GetReliableType(loc.LocalType, ref hasSets, ref onlyAssembly);
                            }
                        }
                    }
                    break;
                case MemberTypes.Property:
                    {
                        var propInfo = memInfo as PropertyInfo;

                        var res = propInfo.PropertyType;
                        if (res != null)
                            GetReliableType(res, ref hasSets, ref onlyAssembly);

                        var getMethod = propInfo.GetGetMethod(true);
                        if (getMethod != null)
                        {
                            var body = getMethod.GetMethodBody();
                            if (body != null && body.LocalVariables != null)
                            {
                                foreach (var loc in body.LocalVariables)
                                {
                                    GetReliableType(loc.LocalType, ref hasSets, ref onlyAssembly);
                                }
                            }
                        }

                        var setMethod = propInfo.GetSetMethod(true);
                        if (setMethod != null)
                        {
                            var body = setMethod.GetMethodBody();
                            if (body != null && body.LocalVariables != null)
                            {
                                foreach (var loc in body.LocalVariables)
                                {
                                    GetReliableType(loc.LocalType, ref hasSets, ref onlyAssembly);
                                }
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private static void GetReliableType(Type valueClass, ref HashSet<Type> hasSets, ref Assembly onlyAssembly)
    {
        var nameString = valueClass.ToString();
        if (!IsReliableType(valueClass, ref hasSets, ref onlyAssembly) || dirtyTypes.Contains(nameString)) return;
        dirtyTypes.Add(nameString);
        if (valueClass.IsArray)//数组
        {
            var elementType = valueClass.GetElementType();
            if (elementType != null)
                GetReliableType(elementType, ref hasSets, ref onlyAssembly);
        }
        else if (valueClass.IsGenericType())//泛型
        {
            //GetReliableType(valueClass.GetGenericTypeDefinition(), ref hasSets, ref onlyAssembly);
            if (!valueClass.IsValueType)//值类型泛型会报错
            {
                AddType(valueClass, ref hasSets, ref onlyAssembly);

                Type[] typeParameters = valueClass.GetGenericArguments();
                for (int i = 0; i < typeParameters.Length; i++)
                {
                    GetReliableType(typeParameters[i], ref hasSets, ref onlyAssembly);
                }
            }
        }
        else if (valueClass.IsByRef || valueClass.IsPointer)
        {
            var nameType = valueClass.ToString();
            nameType = nameType.TrimEnd('&');
            nameType = nameType.TrimEnd('*');
            var elementType = Type.GetType(nameType);
            if (elementType == null)
                elementType = valueClass.Assembly.GetType(nameType);

            if (elementType != null)
                GetReliableType(elementType, ref hasSets, ref onlyAssembly);
            else
                Debug.LogWarning("cannot get type from " + valueClass);
        }
        else if (valueClass.IsClass)
        {
            AddType(valueClass, ref hasSets, ref onlyAssembly);
            if (valueClass.Assembly == onlyAssembly)
                GetReliableClass(valueClass, ref hasSets, ref onlyAssembly);
        }
        else
        {
            AddType(valueClass, ref hasSets, ref onlyAssembly);
        }
    }

    public static bool isReGenerateWrap = false;
    [LuaCallCSharp]
    public static List<Type> LuaCallCSharpCustom
    {
        get
        {
            dirtyTypes.Clear();
            //黑名单
            dirtyTypes.Add("T");
            dirtyTypes.Add("TStateImplType");

            if (!isReGenerateWrap) return new List<Type>();

            var onlyAssmbly = Assembly.Load("Assembly-CSharp");
            var list = (from type in onlyAssmbly.GetTypes()
                        where (type.Namespace == "Game" || type.Namespace == "Pb") && type.IsPublic
                        select type).ToList();
            var hasSets = new HashSet<Type>();
            for (int i = 0; i < list.Count; i++)
            {
                //if(list[i].ToString() == "Game.PostEvent`1[T]")
                GetReliableType(list[i], ref hasSets, ref onlyAssmbly);
            }

            //去重
            //for (int i = 0; i < LuaCallCSharp.Count; i++)
            //{
            //    var alreadyInType = LuaCallCSharp[i];
            //    if (hasSets.Contains(alreadyInType))
            //        hasSets.Remove(alreadyInType);
            //}
#if UNITY_EDITOR
            foreach (var item in hasSets)
                Debug.Log("LuaCallCSharpCustom......" + item);
#endif
            //var aa = hasSets.ToList();
            //var bb = new List<string>();
            //for (int i = 0; i < aa.Count; i++)
            //{
            //    bb.Add(aa[i].ToString());
            //}
            //var cc = bb.ToArray();
            //Array.Sort(cc);

            return hasSets.ToList();
        }
    }

    [CSharpCallLua]
    public static List<Type> CSharpCallLuaCustom
    {
        get
        {
            if (!isReGenerateWrap) return new List<Type>();
            var editor = Assembly.Load("Assembly-CSharp-Editor");
            var list = (from type in Assembly.Load("Assembly-CSharp").GetTypes()
                        where (type.Namespace == "Game") && type.IsPublic
                        select type).ToList();
            HashSet<Type> delegates = new HashSet<Type>();
            for (int i = 0; i < list.Count; ++i)
            {
                if (typeof(Delegate).IsAssignableFrom(list[i]))
                {
                    var type = list[i];
                    if (type.Assembly == editor)
                        continue;
                    if (typeof(Delegate).IsAssignableFrom(type) && type.IsPublic)
                        delegates.Add(type);
                }
                else
                {
                    var pis = list[i].GetProperties();
                    foreach (var pi in pis)
                    {
                        var type = pi.PropertyType;
                        if (type.Assembly == editor)
                            continue;
                        if (typeof(Delegate).IsAssignableFrom(type) && type.IsPublic)
                            delegates.Add(type);
                    }
                    var mets = list[i].GetMethods();
                    foreach (var me in mets)
                    {
                        var pars = me.GetParameters();
                        foreach (var par in pars)
                        {
                            var type = par.GetType();
                            if (type.Assembly == editor)
                                continue;
                            if (typeof(Delegate).IsAssignableFrom(type) && type.IsPublic)
                                delegates.Add(type);
                        }
                    }
                    var mems = list[i].GetMembers();
                    foreach (var me in mems)
                    {
                        var type = me.GetType();
                        if (type.Assembly == editor)
                            continue;
                        if (typeof(Delegate).IsAssignableFrom(type) && type.IsPublic)
                            delegates.Add(type);
                    }
                }
            }
            return delegates.ToList();
        }
    }
}