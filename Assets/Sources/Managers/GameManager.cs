using GameLua;
using GameUtil;
using UnityEngine;
using System.Text;

namespace Game
{

    public class GameManager : MonoSingleton<GameManager>
    {
        void Start()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += Util.OnSceneLoaded;
            GameEnvSetting.ApplySetting();
//#if !UNITY_EDITOR
            InitLogReport();
//#endif
            FPSCounter.CreateInstance();
            // IMChecker
            AccountIMChecker.CreateInstance();

            DataManager.CreateInstance();
            StartCoroutine(DataManager.Instance.LoadAll());

            DebugManager.CreateInstance();
            AssetBundleManager.CreateInstance();
            ResourceManager.CreateInstance();
            GameObjectPool.CreateInstance();
            //LuaManager.CreateInstance();
            EventManager.CreateInstance();
            PlayerSettingManager.CreateInstance();

            NetworkManager.CreateInstance();

            //WwiseAudioManager.CreateInstance();
            GameStateManager.Instance.ChangeState("GameLobbyState");

            AudioManager.CreateInstance();
        }

        // 宕机查看方式
        // 内网：http://192.168.112.94:5601/app/kibana
        // 外网：http://cdlog1.2980.com:5601/app/kibana
        public void InitLogReport()
        {
            //Log日志，测试中开启！！！！
            //if (VersionManager.Instance.TestPackage)
                gameObject.AddComponent<LogListener>();


            CrashReportToELK.Instance.appName = "m2jh";
            CrashReportToELK.Instance.uuid = GameInterface.GetKeyChain("x6_deviceid");
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            CrashReportToELK.Instance.url = "http://192.168.112.94:3134";
#else
            CrashReportToELK.Instance.url = "http://cdlog1.2980.com:3134";
#endif
            CrashReportToELK.Instance.CSharpCrashFunction = (logType, level, logString, stack, time) =>
            {
                CrashReportToELK.Instance.EnableCSharpCrash = false;

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("uuid={0} \n", CrashReportToELK.Instance.uuid);
                sb.AppendFormat("time={0:D2}/{1:D2}/{2:D2} {3:D2}:{4:D2}:{5:D2} \n", System.DateTime.Now.Year,
                    System.DateTime.Now.Month, System.DateTime.Now.Day, System.DateTime.Now.Hour, System.DateTime.Now.Minute, System.DateTime.Now.Second);
                sb.AppendFormat("version={0} \n", VersionManager.Instance.PackVersion);
                if (VersionManager.Instance.TestPackage)
                {
                    sb.AppendFormat("msg={0} \n", logString);
                    sb.AppendFormat("stack={0} \n", stack);
                }
                else
                    sb.AppendFormat("msg={0} \n", logString);
            };


            //CrashReportToELK.Instance.Install();
        }

        public static int GetPingData()
        {
            return (int)NetworkManager.Instance.GetBattlePingTime();
        }

        // Update is called once per frame
        void Update()
        {
            NetworkManager.Instance.Update();
            AssetBundleManager.Instance.Update();
            ResourceManager.Instance.Update();
            GameObjectPool.Instance.Update();
            UIManager.Instance.Update(Time.deltaTime);
            EventManager.Instance.Update();
            GameStateManager.Instance.Update();
            if (ECSManager.HasInstance())
                ECSManager.Instance.Update();
        }

        void OnApplicationQuit()
        {
            NetworkManager.Instance.CloseGameServer();
            NetworkManager.Instance.CloseBattleServer();
            PlayerSettingManager.Instance.Destroy();
        }
        void OnApplicationFocus(bool focus)
        {
//             if (LuaManager.HasInstance())
//                 LuaManager.Instance.FocusChg(focus);
        }
    }
}