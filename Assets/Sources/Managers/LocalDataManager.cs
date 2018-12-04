
using System;
using GameUtil;
using UnityEngine;

namespace Game
{
    public class LocalDataManager : Singleton<LocalDataManager>
    {
        public static string FirstGameKey = "firstgame";
        public override void Init()
        {
            
        }

        public bool IsFirstEnterGame()
        {
            return PlayerPrefs.GetInt(FirstGameKey, 0) == 0;
        }

        public void SetFirstGame(int value)
        {
            PlayerPrefs.SetInt(FirstGameKey, value);
            PlayerPrefs.Save();
        }

        public void Update()
        {
        }
    }
}