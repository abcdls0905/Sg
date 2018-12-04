using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameBattleState : GameStateBase
    {
        public string name
        {
            get;
            set;
        }

        public GameBattleState(GameStateMachine parent) : base(parent) { }
        public override void OnStateEnter()
        {
            UIManager.Instance.CloseAllPage();
            ECSManager.Instance.Play(0);
            UIManager.Instance.OpenPage<GameHUDPage>();
            UIManager.Instance.OpenPage<GameLeftJoystic>();
            UIManager.Instance.OpenPage<GamePopInfoPage>();
            if (LocalDataManager.Instance.IsFirstEnterGame())
                UIManager.Instance.OpenPage<GameGuidePage>();
        }

        public override void OnStateLeave()
        {
            base.OnStateLeave();
            UIManager.Instance.CloseAllPage();
        }

        public override void OnUpdate()
        {

        }
    }
}