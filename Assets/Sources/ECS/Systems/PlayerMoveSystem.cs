using Entitas;
using UnityEngine;
using Game;
using System;
using System.Collections.Generic;

namespace Game
{
    // 通过输入构建指令数据
    public class PlayerMoveSystem : IFixedExecuteSystem, IInitializeSystem, IFixedCleanupSystem
    {
        IGroup<GameEntity> _group;
        public PlayerMoveSystem()
        {
            var _context = Contexts.Instance.game;
            _group = _context.GetGroup(Matcher<GameEntity>.AllOf(GameMatcher.Move, GameMatcher.Player));
            _group.OnEntityAdded += OnMoveAdded;
        }

        public void Initialize()
        {
            Util.ListenCommand<MoveCommand>(HandleMoveCommand);
        }

        public void FixedExecute()
        {
            GameEntity master = Contexts.Instance.game.gameMaster.entity;
            PlayerMove(master);
            UpdateState(master);
            PlayerAotuMove(master);
            UpdateSpeedUp(master);
            UpdatePositionY();
            master.move.moveCommand.isMoving = false;
        }

        public void UpdateState(GameEntity entity)
        {
            entity.move.moveStateMachine.FixedExecute();
        }

        void UpdateSpeedUp(GameEntity entity)
        {
            MapComponent mapComp = Contexts.Instance.game.map;
            GameJson.PlayerConfig plyCfg = DataManager.Instance.playerConfig.Data;
            GameEntity boxEntity = Util.GetFollowBox(entity);
            if (boxEntity != null)
                entity.move.speedUp = boxEntity.burn.eBurnState != AkBurnState.Ak_None ? plyCfg.speedUp : 0;
            else
            {
                CoordComponent coord = entity.coord;
                boxEntity = mapComp.mapData[coord.y, coord.x];
                if (boxEntity != null && boxEntity.burn.eBurnState != AkBurnState.Ak_None)
                    entity.move.speedUp = plyCfg.speedUp;
                else
                    entity.move.speedUp = 0;
            }

            Animator animator = entity.animation.animator;
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Walk"))
                animator.speed = entity.move.GetMoveSpeed() / plyCfg.animationRate;
            else
                animator.speed = 1;
        }

        public void PlayerAotuMove(GameEntity entity)
        {
            float frameTime = Contexts.Instance.game.frame.frameTime;
            if (entity.move.isAutoMove)
            {
                float moveExTime = Util.GetMoveExTime(entity);
                entity.move.moveTime += frameTime;
                MoveComponent move = entity.move;
                if (entity.move.moveTime > moveExTime)
                    entity.move.ClearAutoMove();
                if (move.moveCommand.isMoving && move.moveCommand.eTurnDir == move.eAutoDir)
                    return;
                Vector3 forward = Util.GetForwardByDir(entity.move.eAutoDir);
                Vector3 offset = forward * frameTime * entity.move.GetMoveSpeed();
                if (entity.limit.HasLimit(AkLimitType.Ak_LimitCross))
                {
                    Vector3 newPos = entity.transform.position + offset;
                    if (Util.IsCrossGrid(newPos, entity.move.eAutoDir, entity.move.limitX, entity.move.limitY))
                    {
                        entity.move.ClearAutoMove();
                        return;
                    }
                }
                entity.transform.position += offset;
            }
            if (entity.move.isAutoMove1)
            {
                GameJson.PlayerConfig plyCfg = DataManager.Instance.playerConfig.Data;
                entity.move.moveTime1 += frameTime;
                MoveComponent move = entity.move;
                if (move.moveTime1 > plyCfg.autoTime / 1000)
                {
                    move.isAutoMove1 = false;
                    return;
                }
                if (move.moveCommand.isMoving && move.moveCommand.eTurnDir == move.eAutoDir1)
                    return;
                Vector3 forward = Util.GetForwardByDir(move.eAutoDir1);
                Vector3 offset = forward * frameTime * move.GetMoveSpeed();
                entity.transform.position += offset;
            }
        }

        void UpdatePositionY()
        {
            var iter = _group.GetEntities().GetEnumerator();
            while (iter.MoveNext())
            {
                GameEntity entity = iter.Current;
                if (!entity.move.isUpdateY)
                    continue;
                RaycastHit hit;
                Vector3 origin = new Vector3(entity.transform.position.x, 2.0f, entity.transform.position.z);
                GameJson.PlayerConfig plyCfg = DataManager.Instance.playerConfig.Data;
                float posY = plyCfg.defaultHeight;
                if (Physics.Raycast(origin, Vector3.down, out hit, 5, TagManager.boxLayer, QueryTriggerInteraction.Collide) && hit.collider != null)
                    posY = hit.point.y;
                entity.transform.position = new Vector3(entity.transform.position.x, posY, entity.transform.position.z);
            }
        }

        public void FixedCleanup()
        {
            //GameEntity master = Contexts.Instance.game.gameMaster.entity;
            //master.move.moveCommand.isMoving = false;
        }

        void HandleMoveCommand(ref MoveCommand command, ulong entityId)
        {
            GameEntity entity = Util.FindGroupEntity(_group, entityId);
            if (entity != null)
                entity.move.moveCommand = command;
        }
        
        void OnMoveAdded(IGroup<GameEntity> group, GameEntity entity, int index, IComponent component)
        {
            var sg = entity.move.moveStateMachine;
            sg.RegisterState("StopMoveState", new StopMoveState(sg, entity));
            sg.RegisterState("NormalMoveState", new NormalMoveState(sg, entity));
            sg.RegisterState("AutoMoveState", new AutoMoveState(sg, entity));
        }

        void PlayerMove(GameEntity entity)
        {
            if (entity.move.moveCommand.isMoving)
            {
                if (!entity.move.moveStateMachine.IsState("NormalMoveState"))
                {
                    entity.move.moveStateMachine.ChangeState("NormalMoveState");
                }
            }
            else if (!entity.move.moveStateMachine.IsState("StopMoveState"))
                entity.move.moveStateMachine.ChangeState("StopMoveState");
        }
    }
}