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

                //Ïû³ý
                if (burnEntity.burn.burnTime >= totleTime / 1000)
                {
                    mapComp.burnEntities.RemoveAt(i);
                    boombList.Add(burnEntity);
                }
            }
            Util.BoomBoxList(boombList);
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

            //ÒýÈ¼
            if (targetEntity != null)
            {
                targetEntity.burn.eBurnState = AkBurnState.Ak_Stable;
                toBurnList.Add(targetEntity);
            }
        }
    }
}