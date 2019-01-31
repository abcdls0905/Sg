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

        public void InitLogReport()
        {
            //Log日志，测试中开启！！！！
            //if (VersionManager.Instance.TestPackage)
            gameObject.AddComponent<LogListener>();
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