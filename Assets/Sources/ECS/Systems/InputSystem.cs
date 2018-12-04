using Entitas;
using UnityEngine;
using Game;
using System;
using System.Collections.Generic;

namespace Game
{
    // 通过输入构建指令数据
    public class InputSystem : IExecuteSystem
    {
        public class TestCommandDelay
        {
            public MoveCommand command;
            public float lastTime;
        }
        List<TestCommandDelay> testCommand = new List<TestCommandDelay>();
        public InputSystem()
        {
        }

        public void Execute()
        {
            if (BattleManager.Instance.isGuiding)
                return;
            BuildMove();
            TestDelay();
        }
        void BuildMove()
        {
            var pageLeftJoystic = UIManager.Instance.GetPage<GameLeftJoystic>();
            if (pageLeftJoystic == null)
                return;

            float h = pageLeftJoystic.offseth;
            float v = pageLeftJoystic.offsetv;

            GameEntity master = Contexts.Instance.game.gameMaster.entity;
            bool walking = h != 0f || v != 0f;
            if (!walking)
            {
                h = Input.GetAxisRaw("Horizontal");
                v = Input.GetAxisRaw("Vertical");
                walking = h != 0f || v != 0f;
            }

            Vector3 dir = new Vector3(h, 0, v);
            dir = Vector3.ClampMagnitude(dir, 1);
            AkTurnDir moveDir = Util.GetJoystickDir(h, v);
            if (walking)
            {
                RotateCommand rotCommand = new RotateCommand();
                rotCommand.eTurnDir = moveDir;
                Util.PushCommand<RotateCommand>(ref rotCommand);
            }

            if (!CheckCanMove_New(master, moveDir))
                return;

            if (!walking && !master.move.moveStateMachine.IsState("NormalMoveState"))
                return;

            MoveCommand command = new MoveCommand();
            command.eTurnDir = moveDir;
            command.isMoving = walking;
            Util.PushCommand<MoveCommand>(ref command);
//             TestCommandDelay delay = new TestCommandDelay();
//             delay.command = command;
//             delay.lastTime = 0;
//             testCommand.Add(delay);
        }

        void TestDelay()
        {
            for (int i = testCommand.Count - 1; i >= 0; i--)
            {
                TestCommandDelay delay = testCommand[i];
                delay.lastTime += Time.deltaTime;
                if (delay.lastTime > 0.18f)
                {
                    testCommand.RemoveAt(i);
                    Util.PushCommand<MoveCommand>(ref delay.command);
                }
            }
        }

        bool CheckCanMove_New(GameEntity master, AkTurnDir eMoveDir)
        {
            if (master.limit.HasLimit(AkLimitType.Ak_LimitMove))
                return false;
            MapComponent mapComp = Contexts.Instance.game.map;
            CoordComponent coord = master.coord;
            Vector3 position = master.transform.position;
            if (position.x <= 0 && eMoveDir == AkTurnDir.Ak_Left)
                return false;
            if (position.x >= mapComp.mapWidth - 1 && eMoveDir == AkTurnDir.Ak_Right)
                return false;
            if (position.z <= 0 && eMoveDir == AkTurnDir.Ak_Back)
                return false;
            if (position.z >= mapComp.mapWidth - 1 && eMoveDir == AkTurnDir.AK_Front)
                return false;
            return true;
        }
    }
}
