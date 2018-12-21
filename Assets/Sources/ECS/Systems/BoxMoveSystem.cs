using Entitas;
using UnityEngine;
using Game;
using System;
using GameJson;
using System.Collections.Generic;

namespace Game
{
    public class BoxMoveSystem : IFixedExecuteSystem, IFixedCleanupSystem
    {
        Queue<GameEntity> queue;
        List<GameEntity> list;
        public byte[,] mapMask;

        public BoxMoveSystem()
        {
            queue = new Queue<GameEntity>();
            list = new List<GameEntity>();
            mapMask = new byte[BattleManager.Instance.mapSize, BattleManager.Instance.mapSize];
        }

        public void FixedExecute()
        {
            FollowRotation();
        }

        void DealNode(GameEntity entity, int x, int y, AkBoxColor eColor)
        {
            if (!Util.CheckCoordValid(x, y))
                return;
            if (mapMask[y, x] != 0)
                return;
            mapMask[y, x] = 1;
            MapComponent mapComp = Contexts.Instance.game.map;
            GameEntity boxEntity = mapComp.mapData[y, x];
            if (boxEntity != null && boxEntity.box.eColor == eColor)
            {
                queue.Enqueue(boxEntity);
                list.Add(boxEntity);
            }
        }

        void ClearMask()
        {
            int width = mapMask.GetLength(0);
            int height = mapMask.GetLength(1);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    mapMask[i, j] = 0;
                }
            }
        }

        void CheckEliminate(GameEntity entity)
        {
            CoordComponent coord = entity.coord;
            MapComponent mapComp = Contexts.Instance.game.map;
            queue.Clear();
            list.Clear();
            ClearMask();
            queue.Enqueue(entity);
            list.Add(entity);
            while (queue.Count > 0)
            {
                GameEntity root = queue.Dequeue();
                mapMask[root.coord.y, root.coord.x] = 1;
                DealNode(root, root.coord.x, root.coord.y + 1, root.box.eColor);
                DealNode(root, root.coord.x - 1, root.coord.y, root.box.eColor);
                DealNode(root, root.coord.x, root.coord.y - 1, root.box.eColor);
                DealNode(root, root.coord.x + 1, root.coord.y, root.box.eColor);
            }
        }

        bool HasDifBurnBox(List<GameEntity> boxEntities)
        {
            bool hasUnBurnBox = false;
            bool hasBurnBox = false;
            for (int i = 0; i < boxEntities.Count; i++)
            {
                if (boxEntities[i].burn.eBurnState == AkBurnState.Ak_None)
                    hasUnBurnBox = true;
                else
                    hasBurnBox = true;
            }
            return hasUnBurnBox && hasBurnBox;
        }

        bool HasBurnBox(List<GameEntity> boxEntities)
        {
            for (int i = 0; i < boxEntities.Count; i++)
            {
                if (boxEntities[i].burn.eBurnState == AkBurnState.Ak_None)
                    return false;
            }
            return true;
        }

        void CheckCoverBornBox(CoordComponent coord)
        {
            MapComponent mapComp = Contexts.Instance.game.map;
            List<GameEntity> bornList = mapComp.bornList;
            for (int i = bornList.Count - 1; i >= 0; i--)
            {
                GameEntity born = bornList[i];
                if (Util.IsSameCoord(coord, born.coord))
                {
                    bornList.RemoveAt(i);
                    Util.DestroyEntity(born);
                }
            }
            //同帧生成的方块
            GameEntity targetEntity = mapComp.mapData[coord.y, coord.x];
            if (targetEntity != null)
                Util.DestroyEntity(targetEntity);
        }

        void CheckMasterLimitMove(GameEntity boxEntity)
        {
            GameEntity master = Contexts.Instance.game.gameMaster.entity;
            if (boxEntity.box.followEntity == master)
                master.limit.RemoveLimit(AkLimitType.Ak_LimitCross);
        }

        public void RefreshFollowBox(GameEntity entity)
        {
            MapComponent mapComp = Contexts.Instance.game.map;
            CoordComponent coord = entity.coord;
            Vector3 position = entity.transform.position;
            CheckCoverBornBox(entity.box.followEntity.coord);
            mapComp.mapData[entity.coord.y, entity.coord.x] = null;
            entity.coord.x = entity.box.followEntity.coord.x;
            entity.coord.y = entity.box.followEntity.coord.y;
            entity.box.isRoting = false;

            mapComp.mapData[entity.coord.y, entity.coord.x] = entity;
            Util.TurnDiceBox(entity, entity.box.eBoxDir);
            entity.box.eBoxDir = AkTurnDir.Ak_Max;
            CheckMasterLimitMove(entity);
            mapComp.followBoxs.Remove(entity);

            //消除规则
            CheckEliminate(entity);

            BoxConfig burnConfig = DataManager.Instance.boxConfig.Data;
            PlayerConfig plyCfg = DataManager.Instance.playerConfig.Data;
            bool hasDifBurnBox = HasDifBurnBox(list);

            GameEntity follower = entity.box.followEntity;
            if (list.Count >= burnConfig.eliminateNum)
            {
                if (hasDifBurnBox)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        GameEntity boxEntity = list[i];
                        if (boxEntity.burn.eBurnState == AkBurnState.Ak_None)
                            mapComp.AddBurnEntity(boxEntity);
                        else
                        {
                            if (boxEntity.burn.eBurnState == AkBurnState.Ak_Stable)
                            {
                                float time = boxEntity.burn.burnTime - burnConfig.extraTime;
                                boxEntity.burn.burnTime = time > 0 ? time : 0;
                            }
                            boxEntity.animation.animator.SetTrigger("connect");
                        }
                        boxEntity.box.isPositive = true;
                    }
                    DealPlayerUpdateY(entity.box.followEntity);
                    GameEntity master = Contexts.Instance.game.gameMaster.entity;
                    AudioManager.Instance.PlayAudio("Audio/collect", master.view.gameObject);
                }
                else if (!HasBurnBox(list))
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        GameEntity boxEntity = list[i];
                        boxEntity.box.isPositive = true;
                        mapComp.AddBurnEntity(boxEntity);
                    }
                    DealPlayerUpdateY(entity.box.followEntity);
                    GameEntity master = Contexts.Instance.game.gameMaster.entity;
                    AudioManager.Instance.PlayAudio("Audio/collect", master.view.gameObject);
                }
            }
        }

        void DealPlayerUpdateY(GameEntity player)
        {
            player.move.isUpdateY = true;
            TimerComponent timer = Contexts.Instance.game.timer;
            timer.AddTimer(new Timer(0.4f, player.iD.value, UpdateYTimer));
        }

        void UpdateYTimer(GameEntity player)
        {
            player.move.isUpdateY = false;
            Util.ResetPositionY(player);
        }

        void FollowRotation()
        {
            float frameRate = Contexts.Instance.game.frame.frameRate;
            MapComponent mapComp = Contexts.Instance.game.map;
            for (int i = mapComp.followBoxs.Count - 1; i >= 0 ; i--)
            {
                GameEntity boxEntity = mapComp.followBoxs[i];
                GameEntity follower = boxEntity.box.followEntity;
                Transform transform = boxEntity.view.gameObject.transform;
                Vector3 oldPos = transform.position;
                Quaternion oldRot = transform.rotation;
                transform.position = boxEntity.transform.position;
                transform.rotation = boxEntity.transform.rotation;

                float value = Util.GetRotationRate(follower) / frameRate;
                if (boxEntity.box.rotateValue + value > 90)
                    value = 90 - boxEntity.box.rotateValue;
                boxEntity.view.gameObject.transform.RotateAround(boxEntity.box.rotPoint, boxEntity.box.axis, value);
                boxEntity.transform.position = transform.position;
                boxEntity.transform.rotation = transform.rotation;
                transform.position = oldPos;
                transform.rotation = oldRot;
                boxEntity.box.rotateValue += value;
                if (boxEntity.box.rotateValue >= 90)
                    RefreshFollowBox(boxEntity);
            }
        }

        public void FixedCleanup()
        {
            ClearFollowBox();
        }

        void ClearFollowBox()
        {
        }
    }
}