using System;
using XLua;
using GameUtil;
using GameNetwork;
using Game;

namespace GameLua
{
    [LuaCallCSharp, GCOptimize]
    public enum LuaEventType
    {
        LE_Account,
        LE_SendLog,
        LE_LeaveGame,
        LE_PlayerSetting,
        LE_CloseAllPage,
        LE_Alert,
        LE_EnterBattleFailed,
        LE_TestServer,
        LE_TestHotFix,
        LE_AccountLogin,
        LE_SendMail,
        LE_ReConnectBS,
        LE_CommonTip,
        LE_UpdateCheckPointPage,
        LE_TopTip,
    }
    [LuaCallCSharp, GCOptimize]
    public struct ExpressArgs
    {
        public double arg1;
        public double arg2;
        public double arg3;
        public double arg4;
        public double arg5;
        public double arg6;
        public double arg7;
        public double arg8;
        public double arg9;
        public double arg10;
        public double arg11;
        public double arg12;
    }
    [LuaCallCSharp]
    public struct TipArgs
    {
        public object arg1;
        public object arg2;
        public object arg3;
        public object arg4;
        public object arg5;
        public object arg6;
        public object arg7;
        public object arg8;
        public object arg9;
        public object arg10;
    }

    [CSharpCallLua]
    public delegate int ExpressionRegister(string exp);
    [CSharpCallLua]
    public delegate ExpressArgs ExpressionHandler(int index, ExpressArgs args);

    public class LuaManager : MonoSingleton<LuaManager>
    {
        public static string[] LuaPath = new string[]
        {
            "LuaSources",
            "GameData",
            "Updater",
            "HotFix"
        };

        public LuaEnv _luaEnv;
        Action _luaUpdate;
        Action<ushort, byte[]> _luaRecvMsg;
        Action<NetworkType> _luaConnectSucced;
        Action<NetworkType> _luaConnectFailed;
        Action<NetworkType> _luaConnectError;
        Action<NetworkType, bool, bool> _luaConnectServer;
        Action<UnityEngine.NetworkReachability> _luaConnectChg;
        Action<bool> _luaFocusChg;
        ExpressionHandler _luaExpression;
        Action _luaEnterMain;
        LuaFunction _luaEvent;
        //LuaFunction _luaUIEvent;

        public override void Init()
        {
            _luaEnv = new LuaEnv();
            _luaEnv.InitEx();
            for (int i = 0; i < LuaPath.Length; ++i)
                _luaEnv.AddPath(LuaPath[i]);
        }

        public override void UnInit()
        {
            _luaEnv.Dispose();
            _luaEnv = null;
            _luaUpdate = null;
        }

        protected override void Awake()
        {
            base.Awake();
            _luaEnv.DoFile("main.lua");
            var luaStart = _luaEnv.Global.Get<Action>("Start");
            if (luaStart != null)
                luaStart();

            _luaUpdate = _luaEnv.Global.Get<Action>("Update");
            _luaConnectSucced = _luaEnv.Global.Get<Action<NetworkType>>("ConnectSucced");
            _luaConnectFailed = _luaEnv.Global.Get<Action<NetworkType>>("ConnectFailed");
            _luaConnectError = _luaEnv.Global.Get<Action<NetworkType>>("ConnectError");
            _luaConnectServer = _luaEnv.Global.Get<Action<NetworkType, bool, bool>>("ConnectServer");
            _luaRecvMsg = _luaEnv.Global.Get<Action<ushort, byte[]>>("RecvMsg");
            _luaExpression = _luaEnv.Global.Get<ExpressionHandler>("Expression");
            _luaEnterMain = _luaEnv.Global.Get<Action>("EnterMain");
            _luaEvent = _luaEnv.Global.Get<LuaFunction>("LuaEvent");
            //_luaUIEvent = _luaEnv.Global.Get<LuaFunction>("UIEvent");
            _luaConnectChg = _luaEnv.Global.Get<Action<UnityEngine.NetworkReachability>>("NetworkChg");
            _luaFocusChg = _luaEnv.Global.Get<Action<bool>>("FocusChg");
        }

        public void Update()
        {
            if (_luaUpdate != null)
                _luaUpdate();
        }

        public void CallFunc(string name)
        {
            var action = _luaEnv.Global.Get<Action>(name);
            if (action != null)
                action();
        }
        public LuaTable NewLuaTable()
        {
            return _luaEnv.NewTable();
        }
        public void RecvMsg(ushort msgID, byte[] body)
        {
            if (_luaRecvMsg != null)
                _luaRecvMsg(msgID, body);
        }
        public void ConnectSucced(NetworkType type)
        {
            if (_luaConnectSucced != null)
                _luaConnectSucced(type);
        }
        public void ConnectFailed(NetworkType type)
        {
            if (_luaConnectFailed != null)
                _luaConnectFailed(type);
        }
        public void ConnectError(NetworkType type)
        {
            if (_luaConnectError != null)
                _luaConnectError(type);
        }
        public void ConnectServer(NetworkType type, bool reconn, bool restart)
        {
            if (_luaConnectServer != null)
                _luaConnectServer(type, reconn, restart);
        }
        public void EnterMain()
        {
            if (_luaEnterMain != null)
                _luaEnterMain();
        }
        public ExpressArgs Expression(int index, ref ExpressArgs args)
        {
            if (_luaExpression != null)
                return _luaExpression(index, args);
            return default(ExpressArgs);
        }
        
        // 比较低效的Lua通用调用方式，慎用
        public object[] PushLuaEvent(params object[] args)
        {
            if (_luaEvent != null)
            {
                return _luaEvent.Call(args);
            }
            return null;
        }
        public void NetworkChg(UnityEngine.NetworkReachability reach)
        {
            if (_luaConnectChg != null)
                _luaConnectChg(reach);
        }
        public void FocusChg(bool focus)
        {
            if (_luaFocusChg != null)
                _luaFocusChg(focus);
        }
    }
}