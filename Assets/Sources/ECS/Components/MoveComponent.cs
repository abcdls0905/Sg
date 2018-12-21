
using Entitas;
using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    public class MoveStateMachine : StateMachine
    {
        public void Update()
        {
            if (tarState != null)
                tarState.OnUpdate();
        }

        public void FixedExecute()
        {
            if (tarState != null)
                tarState.OnFixedExecute();
        }
    }

    public class MoveStateBase : IState
    {
        public GameEntity entity;
        protected MoveStateMachine parent;
        public MoveStateBase(MoveStateMachine parent, GameEntity entity)
        {
            this.parent = parent;
            this.entity = entity;
        }
        public string name
        {
            get;
            set;
        }
        public virtual bool OnStateEnterCheck() { return true; }
        public virtual bool OnStateLeaveCheck() { return true; }
        public virtual void OnStateEnter() { }
        public virtual void OnStateLeave() { }
        public virtual void OnStateOverride() { }
        public virtual void OnStateResume() { }
        public virtual void OnUpdate() { }
        public virtual void OnFixedExecute() { }
    }

    public class StopMoveState : MoveStateBase
    {
        public StopMoveState(MoveStateMachine parent, GameEntity entity) : base(parent, entity) { }

        public override void OnStateEnter()
        {
            entity.move.eMoveDir = AkTurnDir.Ak_Max;
            MapComponent mapComp = Contexts.Instance.game.map;
            GameEntity boxEntity = mapComp.mapData[entity.coord.y, entity.coord.x];
            if (boxEntity != null && boxEntity.burn.eBurnState != AkBurnState.Ak_None)
                entity.animation.animator.SetTrigger("scare");
            else
            {
                GameEntity followBox = Util.GetFollowBox(entity);
                if (followBox != null && followBox.burn.eBurnState != AkBurnState.Ak_None)
                    entity.animation.animator.SetTrigger("scare");
                else
                    entity.animation.animator.SetTrigger("idle");
            }
        }
    }

    public class AutoMoveState : MoveStateBase
    {
        public AutoMoveState(MoveStateMachine parent, GameEntity entity) : base(parent, entity) { }

        public override void OnStateEnter()
        {

        }

        public override void OnFixedExecute()
        {
        }
    }

    public class NormalMoveState : MoveStateBase
    {
        public NormalMoveState(MoveStateMachine parent, GameEntity entity) : base(parent, entity) { }

        public override void OnStateEnter()
        {
            entity.animation.animator.SetTrigger("walk");
        }

        void SetEntityCoord(GameEntity entity, int coordX, int coordZ)
        {
            entity.coord.x = coordX;
            entity.coord.y = coordZ;
        }

        void MaskBoxRotate(GameEntity entity, int coordX, int coordY)
        {
            MapComponent mapComp = Contexts.Instance.game.map;
            GameEntity boxEntity = mapComp.mapData[entity.coord.y, entity.coord.x];
            AkTurnDir eMoveDir = entity.move.eMoveDir;
            if (!Util.CheckCoordValid(coordX, coordY))
                return;
            GameEntity nextEntity = mapComp.mapData[coordY, coordX];
            if (nextEntity == null)
            {
                entity.limit.AddLimit(AkLimitType.Ak_LimitCross);
                boxEntity.box.followEntity = entity;
                boxEntity.box.eBoxDir = eMoveDir;
                boxEntity.box.rotateValue = 0;
                Util.GetRotatePoint(eMoveDir, entity.coord.x, entity.coord.y, ref boxEntity.box.rotPoint, ref boxEntity.box.axis);
                mapComp.followBoxs.Add(boxEntity);
                //auto move
                entity.move.isAutoMove = true;
                entity.move.moveTime = 0;
                entity.move.eAutoDir = eMoveDir;
                entity.move.limitX = coordX;
                entity.move.limitY = coordY;
                boxEntity.box.isRoting = true;
                GameEntity master = Contexts.Instance.game.gameMaster.entity;
                AudioManager.Instance.PlayAudio("Audio/boxrot", master.view.gameObject);
            }
            else
            {
                entity.move.isAutoMove1 = true;
                entity.move.moveTime1 = 0;
                entity.move.eAutoDir1 = eMoveDir;
            }
        }

        void DealCoordRefresh(GameEntity entity, int coordX, int coordZ)
        {
            OnLeaveCoord(entity.coord.x, entity.coord.y);
            OnEnterCoord(coordX, coordZ);
            MaskBoxRotate(entity, coordX, coordZ);
            SetEntityCoord(entity, coordX, coordZ);
        }

        void OnEnterCoord(int x, int z)
        {
            if (!Util.CheckCoordValid(x, z))
                return;
            MapComponent mapComp = Contexts.Instance.game.map;
            GameEntity entity = mapComp.mapData[z, x];
            if (entity != null)
            {
                Vector3 pos = entity.transform.position;
                GameJson.BoxConfig boxConfig = DataManager.Instance.boxConfig.Data;
                entity.transform.position = new Vector3(pos.x, boxConfig.sinkDistance, pos.z);
            }
        }

        void OnLeaveCoord(int x, int z)
        {
            if (!Util.CheckCoordValid(x, z))
                return;
            MapComponent mapComp = Contexts.Instance.game.map;
            GameEntity entity = mapComp.mapData[z, x];
            if (entity != null)
            {
                Vector3 pos = entity.transform.position;
                entity.transform.position = new Vector3(pos.x, 0, pos.z);
            }
        }

        void CheckCoordRefresh(GameEntity entity, AkTurnDir eMoveDir)
        {
            int width = BattleManager.Instance.mapSize;
            int height = BattleManager.Instance.mapSize;
            Vector3 position = entity.transform.position;
            float gridRadius = Util.GridRadius;
            float radius = DataManager.Instance.playerConfig.Data.crossRadius;
            if (eMoveDir == AkTurnDir.AK_Front)
            {
                float z = position.z + radius;
                float coordZ = entity.coord.y + gridRadius;
                if (z > coordZ && z < height)
                {
                    DealCoordRefresh(entity, entity.coord.x, entity.coord.y + 1);
                }
            }
            else if (eMoveDir == AkTurnDir.Ak_Back)
            {
                float z = position.z - radius;
                float coordZ = entity.coord.y - gridRadius;
                if (z < coordZ && z < height)
                {
                    DealCoordRefresh(entity, entity.coord.x, entity.coord.y - 1);
                }
            }
            else if (eMoveDir == AkTurnDir.Ak_Left)
            {
                float x = position.x - radius;
                float coordX = entity.coord.x - gridRadius;
                if (x < coordX && x >= 0)
                {
                    DealCoordRefresh(entity, entity.coord.x - 1, entity.coord.y);
                }
            }
            else if (eMoveDir == AkTurnDir.Ak_Right)
            {
                float x = position.x + radius;
                float coordX = entity.coord.x + gridRadius;
                if (x > coordX && x >= 0)
                {
                    DealCoordRefresh(entity, entity.coord.x + 1, entity.coord.y);
                }
            }
        }

        public override void OnFixedExecute()
        {
            if (entity.hasAI)
                return;
            Vector3 moveOffset = Util.GetStartMovementByDir(entity, entity.move.moveCommand.eTurnDir, Vector3.forward);
            if (entity.limit.HasLimit(AkLimitType.Ak_LimitCross))
            {
                Vector3 newPos = entity.transform.position + moveOffset;
                if (Util.IsCrossGrid(newPos, entity.move.moveCommand.eTurnDir, entity.move.limitX, entity.move.limitY))
                    return;
            }
            entity.transform.position += moveOffset;
            CheckCoordRefresh(entity, entity.move.moveCommand.eTurnDir);
        }
    }

    [Game]
    public class MoveComponent : IComponent
    {
        public float moveSpeed;
        public float speedUp;
        public AkTurnDir eMoveDir;
        //½ÃÕý1
        public float moveTime;
        public AkTurnDir eAutoDir;
        public int limitX;
        public int limitY;
        public bool isAutoMove;
        //½ÃÕý2
        public AkTurnDir eAutoDir1;
        public bool isAutoMove1;
        public float moveTime1;
        public MoveCommand moveCommand;
        public MoveStateMachine moveStateMachine = new MoveStateMachine();
        public bool isUpdateY;

        public void Reset()
        {
            GameJson.PlayerConfig playerConfig = DataManager.Instance.playerConfig.Data;
            moveSpeed = playerConfig.moveSpeed;
            speedUp = 0;
            moveTime = 0;
            eAutoDir = AkTurnDir.Ak_Max;
            limitX = 0;
            limitY = 0;
            isAutoMove = false;
            eMoveDir = AkTurnDir.Ak_Max;

            isAutoMove1 = false;
            moveTime1 = 0;
            eAutoDir1 = AkTurnDir.Ak_Max;
            moveStateMachine = new MoveStateMachine();
            isUpdateY = false;
        }

        public void ClearAutoMove()
        {
            moveTime = 0;
            isAutoMove = false;
        }

        public float GetMoveSpeed()
        {
            return moveSpeed * (1 + speedUp / 1000);
        }
    }
}