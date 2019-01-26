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
            return;
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