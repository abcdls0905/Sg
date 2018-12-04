using Entitas;
using UnityEngine;
using Game;
using System;
using GameJson;
using System.Collections.Generic;

namespace Game
{
    public class BurnSystem : IInitializeSystem, IFixedExecuteSystem, IFixedCleanupSystem
    {
        IGroup<GameEntity> _group;

        List<GameEntity> boombList = new List<GameEntity>(20);

        public List<GameEntity> toBurnList = new List<GameEntity>(10);
        public BurnSystem()
        {
            var _context = Contexts.Instance.game;
            _group = _context.GetGroup(Matcher<GameEntity>.AllOf(GameMatcher.Burn));
            _group.OnEntityAdded += OnBurnAdded;
        }

        void OnBurnAdded(IGroup<GameEntity> group, GameEntity entity, int index, IComponent component)
        {
            var sg = entity.burn.burnStateMachine;
            sg.RegisterState("BurnStableState", new BurnStableState(sg, entity));
            sg.RegisterState("BurnDangerState", new BurnDangerState(sg, entity));
            sg.RegisterState("BurnNormalState", new BurnNormalState(sg, entity));
        }

        public void Initialize()
        {

        }

        public void FixedCleanup()
        {
        }

        public void Check()
        {
            MapComponent mapComp = Contexts.Instance.game.map;
            int count = mapComp.burnEntities.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                GameEntity entity = mapComp.burnEntities[i];
                for (int j = count - 1; j >= 0; j--)
                {
                    if (i == j)
                        continue;
                    if (entity == mapComp.burnEntities[j])
                        return;
                }
            }
        }

        public void FixedExecute()
        {
            LevelComponent levelComp = Contexts.Instance.game.level;
            MapComponent mapComp = Contexts.Instance.game.map;
            BoxConfig burnConfig = DataManager.Instance.boxConfig.Data;
            float frameTime = Contexts.Instance.game.frame.frameTime;
            float totleTime = levelComp.dangerTime + levelComp.stableTime;
            boombList.Clear();
            int count = mapComp.burnEntities.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                GameEntity burnEntity = mapComp.burnEntities[i];
                if (burnEntity.burn.burnTime <= levelComp.stableTime / 1000)
                {
                    if (!burnEntity.burn.burnStateMachine.IsState("BurnStableState"))
                        burnEntity.burn.burnStateMachine.ChangeState("BurnStableState");
                }
                if (burnEntity.burn.burnTime > levelComp.stableTime / 1000)
                {
                    if (!burnEntity.burn.burnStateMachine.IsState("BurnDangerState"))
                        burnEntity.burn.burnStateMachine.ChangeState("BurnDangerState");
                }
                burnEntity.burn.burnTime += frameTime;

                //消除
                if (burnEntity.burn.burnTime >= totleTime / 1000)
                {
                    mapComp.burnEntities.RemoveAt(i);
                    boombList.Add(burnEntity);
                }
            }
            if (boombList.Count > 0)
            {
                ComboComponent combo = Contexts.Instance.game.combo;
                ScoreParam param = new ScoreParam();
                float score = boombList.Count * (100 + 20 * (boombList.Count - 3) * (1 + 0.1f * combo.value));
                param.score = (int)score;
                Contexts.Instance.game.score.value += param.score;
                EventManager.Instance.PushEvent(GEventType.EVENT_SCORECHANGE, ref param);
                EffectComponent effectComp = Contexts.Instance.game.effect;
                for (int i = boombList.Count - 1; i >= 0; i--)
                {
                    GameEntity burnEntity = boombList[i];
                    //bomb effect
                    EffectData effectData = new EffectData();
                    effectData.name = "Prefabs/Effect/cube_explosion01";
                    effectData.position = burnEntity.transform.position;
                    effectData.lifeTime = 1.0f;
                    effectComp.effects.Add(effectData);
                    DealBombArround(burnEntity);
                }
                DesGroupParam desGroupParam = new DesGroupParam();
                desGroupParam.boxes = boombList;
                EventManager.Instance.PushEvent(GEventType.EVENT_BOXDESTORYGROUP, ref desGroupParam);
                for (int i = boombList.Count - 1; i >= 0; i--)
                {
                    GameEntity burnEntity = boombList[i];
                    DesBoxParam desParam = new DesBoxParam();
                    desParam.entity = burnEntity;
                    EventManager.Instance.PushEvent(GEventType.EVENT_BOXDESTORY, ref desParam);
                    ForceRefreshFollowBox(burnEntity);
                    //Contexts.Instance.game.levelTerms.Add(burnEntity.box.eColor);
                    CheckPlayerOn(burnEntity);
                    mapComp.mapData[burnEntity.coord.y, burnEntity.coord.x] = null;
                    effectComp.RemoveFollow(burnEntity.iD.value);
                    Util.DestroyEntity(burnEntity);
                    boombList.RemoveAt(i);
                }
                GameEntity master = Contexts.Instance.game.gameMaster.entity;
                AudioManager.Instance.PlayAudio("Audio/boom", master.view.gameObject);
            }
        }

        void ForceRefreshFollowBox(GameEntity entity)
        {
            MapComponent mapComp = Contexts.Instance.game.map;
            if (mapComp.followBoxs.Contains(entity))
            {
                mapComp.followBoxs.Remove(entity);
                entity.coord.x = entity.box.followEntity.coord.x;
                entity.coord.y = entity.box.followEntity.coord.y;
            }
        }

        void EmissinBullet(GameEntity entity, AkTurnDir eDir, ref GameEntity target, ref int minDistance)
        {
            MapComponent mapComp = Contexts.Instance.game.map;
            int x = 0;
            int y = 0;
            Util.GetCoordByDir(entity.coord, eDir, ref x, ref y);
            if (!Util.CheckCoordValid(x, y))
                return;
            GameEntity nextEntity = mapComp.mapData[y, x];
            if (nextEntity != null && nextEntity.box.eColor != entity.box.eColor)
                return;
            if (nextEntity != null)
            {
                if (nextEntity.box.eColor == entity.box.eColor && nextEntity.burn.eBurnState == AkBurnState.Ak_None)
                {
                    nextEntity.box.isPositive = false;
                    mapComp.AddBurnEntity(nextEntity);
                }
            }
            else
            {
                //产生火花
                GameEntity bulletEntity = Util.CreateBullet(Util.GetEntityId());
                bulletEntity.bullet.forward = Util.GetForwardByDir(eDir);
                bulletEntity.transform.position = entity.transform.position;
                bulletEntity.bullet.eBoxColor = entity.box.eColor;
                mapComp.bulletList.Add(bulletEntity);
            }
        }

        void DealBombArround(GameEntity entity)
        {
            int burnRange = DataManager.Instance.boxConfig.Data.burnRange;
            int minDistance = 100;
            GameEntity targetEntity = null;
            for (int i = 0; i < (int)AkTurnDir.Ak_Max; i++)
                EmissinBullet(entity, (AkTurnDir)i, ref targetEntity, ref minDistance);
        }

        bool CheckEntityBurnBlock(int x, int y, GameEntity entity, ref GameEntity target, ref int minDistance)
        {
            MapComponent mapComp = Contexts.Instance.game.map;
            CoordComponent coord = entity.coord;
            if (!Util.CheckCoordValid(x, y))
                return false;
            GameEntity e = mapComp.mapData[y, x];
            if (e == null)
                return false;
            if (e.burn.eBurnState != AkBurnState.Ak_None)
                return false;
            if (e.box.eColor != entity.box.eColor)
                return true;
            int distance = Util.GetEntityDistance(entity, e);
            if (distance < minDistance)
            {
                minDistance = distance;
                target = e;
            }
            return false;
        }

        void BurnArround(GameEntity entity)
        {
            MapComponent mapComp = Contexts.Instance.game.map;
            int burnRange = DataManager.Instance.boxConfig.Data.burnRange;
            CoordComponent coord = entity.coord;
            int minDistance = 100;
            GameEntity targetEntity = null;

            for (int i = coord.x - 1; i >= coord.x - burnRange; i--)
            {
                if (CheckEntityBurnBlock(i, coord.y, entity, ref targetEntity, ref minDistance))
                    break;
            }
            for (int i = coord.x + 1; i <= coord.x + burnRange; i++)
            {
                if (CheckEntityBurnBlock(i, coord.y, entity, ref targetEntity, ref minDistance))
                    break;
            }

            for (int j = coord.y - 1; j >= coord.y - burnRange; j--)
            {
                if (CheckEntityBurnBlock(coord.x, j, entity, ref targetEntity, ref minDistance))
                    break;
            }
            for (int j = coord.y + 1; j <= coord.y + burnRange; j++)
            {
                if (CheckEntityBurnBlock(coord.x, j, entity, ref targetEntity, ref minDistance))
                    break;
            }

            //引燃
            if (targetEntity != null)
            {
                targetEntity.burn.eBurnState = AkBurnState.Ak_Stable;
                toBurnList.Add(targetEntity);
            }
        }

        void CheckPlayerOn(GameEntity boxEntity)
        {
            if (Util.IsSameCoord(boxEntity, Contexts.Instance.game.gameMaster.entity))
            {
                ECSManager.Instance.GameEnd();
            }
        }
    }
}