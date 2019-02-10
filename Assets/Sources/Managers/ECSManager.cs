using Entitas;
using GameUtil;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    public enum AkColorType
    {
        AK_TWO,
        AK_THREE,
    }

    public class ECSManager : Singleton<ECSManager>
    {
        Systems _systems;

        public bool IsPlaying;
        public bool isGameStart;
        public bool StopMsg;
        public bool single;
        public float deltaTime;


        public override void Init()
        {
            // 全局组件
            InitGlobalComponent();

            // 系统
            _systems = CreateSystems();
            deltaTime = 0;
        }

        public override void UnInit()
        {
            _systems.TearDown();
            Contexts.Instance.game.DestroyAllEntities();
            Contexts.Instance.game.DestroySingleEntity();
            Contexts.Instance.game.ResetCreationIndex();
            Contexts.Instance.Reset();
            Contexts.Instance = null;

            IsPlaying = false;
            isGameStart = false;
            StopMsg = false;
        }

        public void PreparePage()
        {
            UIManager.Instance.LoadUIPackage(true);
            UIManager.Instance.PreparePage<GameHUDPage>();
            UIManager.Instance.PreparePage<GameLeftJoystic>();
        }
        
        public void StartMultGame()
        {
            _systems.Initialize();
            PreparePage();
            IsPlaying = true;
        }

        public void Reconnect(ulong uuid)
        {

        }

        public void Play(ulong uuid)
        {
            isGameStart = true;
            IsPlaying = true;
            StopMsg = false;

            var game = Contexts.Instance.game;
            game.gameStart.uuid = uuid;

            _systems.Initialize();
            PreparePage();
        }

        public void PlaySingle(ulong uuid)
        {
            isGameStart = true;
            IsPlaying = true;
            single = true;
            StopMsg = false;
            var game = Contexts.Instance.game;
            game.gameStart.uuid = uuid;
            _systems.Initialize();
        }

        public void Stop()
        {
            StopMsg = true;
            //single = false;
            IsPlaying = false;
        }
        public void Pause()
        {
            ECSManager.Instance.IsPlaying = false;
        }

        public void Resume()
        {
            StopMsg = false;
            IsPlaying = true;
        }

        public void OnSkipFrame()
        {
        }

        public void Update()
        {
            if (!BattleManager.Instance.isSingle)
                SingleUpdate();
            else
                MultiUpdate();
        }

        void MultiUpdate()
        {
            if (!IsPlaying)
                return;

            _systems.Execute();

            var frame = Contexts.Instance.game.frame;
            int tryCount = frame.serverFrameIndex - frame.frameIndex;
            tryCount = Util.Lerp(tryCount, 1, 100);

            int num = (int)(Util.GetRealTime() * 1000) - frame.startFrameTime;
            int delay = num - (frame.serverFrameIndex + 1) * (int)(frame.GetTickTime() * 1000);
            int num2 = frame.CalculateJitterDelay(delay);

            deltaTime = num - frame.GetSvrTime();
            deltaTime = deltaTime - num2;

            float tickTime = frame.GetTickTime() * 1000;

            while (tryCount > 0)
            {
                if (deltaTime < tickTime)
                {
                    tryCount = 0;
                    break;
                }

                frame.UpdateServerFrame();
                _systems.FixedExecute();
                _systems.FixedCleanup();
                tryCount = tryCount - 1;

                deltaTime = num - frame.GetSvrTime();
                deltaTime = deltaTime - num2;
            }

            frame.lerpPercent = deltaTime / tickTime;
            //Debug.Log("lerpPercent:" + frame.lerpPercent);
            _systems.Cleanup();
        }

        void SingleUpdate()
        {
            if (!IsPlaying)
                return;
            _systems.Execute();

            var frame = Contexts.Instance.game.frame;
            float tickTime = frame.GetTickTime() * 1000;
            deltaTime = deltaTime + (Time.unscaledDeltaTime * 1000);
            deltaTime = Mathf.Clamp(deltaTime, 0, 100);
            while (deltaTime > tickTime)
            {
                frame.UpdateServerFrame();
                _systems.FixedExecute();
                _systems.FixedCleanup();

                deltaTime -= tickTime;
            }
            frame.lerpPercent = deltaTime / tickTime;
            _systems.Cleanup();
        }

        void InitGlobalComponent()
        {
            GameContext game = Contexts.Instance.game;
            InputContext input = Contexts.Instance.input;

            game.AddGameMaster(null);
            game.AddFrame();
            game.AddGameStart();
            game.AddHUD();
            game.AddMap();
            game.AddCommand();
            game.AddScore();
            game.AddEffect();
            game.AddTimer();
            game.AddLevelTerms();
            game.AddCombo();
            game.AddLevel();
        }

        Systems CreateSystems()
        {
            return new Feature("GameSystems")
                .Add(new FrameSystem())
                .Add(new GuideSystem())
                .Add(new GameStartSystem())
                .Add(new InputSystem())
                .Add(new AudioSystem())
                .Add(new RandLevelSystem())
                .Add(new PositionSystem())
                .Add(new ViewSystem())
                .Add(new HUDSystem())
                .Add(new CameraViewSystem())
                .Add(new PlayerMoveSystem())
                .Add(new BoxMoveSystem())
                .Add(new PlayerRotateSystem())
                .Add(new AnimationSystem())
                .Add(new BurnSystem())
                .Add(new LimitSystem())
                .Add(new EffectSystem())
                .Add(new BulletSystem())
                .Add(new ChangeModelSystem())
                .Add(new CameraSystem())
                .Add(new TimerSystem())
                .Add(new ComboSystem())
                .Add(new TermsSystem())
                .Add(new ItemSkillSystem())
                .Add(new AISystem())
                ;
        }

        public void GameEnd()
        {
            UIManager.Instance.OpenPage<GameFailPage>();
            BattleManager.Instance.isCameraDown = false;
            Pause();
        }
    }
}