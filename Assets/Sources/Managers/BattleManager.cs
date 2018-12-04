using Entitas;
using GameUtil;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{

    public enum AkCtrlMode
    {
        Ak_Four = 0,
        Ak_360,
    }

    public class BattleManager : Singleton<BattleManager>
    {
        public string battleScene = "tese_v3";
        public int mapSize;
        public AkColorType eColorType;
        public AkCtrlMode eCtrlMode;
        public bool isCameraDown;
        public bool isGuiding;
        public bool isSingle;

        public override void Init()
        {
            eColorType = AkColorType.AK_THREE;
            eCtrlMode = AkCtrlMode.Ak_Four;
            isCameraDown = false;
            isGuiding = false;
            isSingle = true;
        }

        public void QuitGame()
        {
            SceneManager.UnloadSceneAsync(battleScene);
            UIManager.Instance.CloseAllPage();
            EventManager.Instance.Clear();
            ECSManager.DestroyInstance();
            GameObjectPool.Instance.ClearPooledObjects();
            ResourceManager.Instance.RemoveAllCachedResources();

            DataManager.Instance.OnLeaveScene();
            AudioManager.Instance.QuitAudio();
        }
    }
}