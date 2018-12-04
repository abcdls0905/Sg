using Entitas;
using UnityEngine;
using Game;
using System;
using System.Collections.Generic;
using GameJson;

namespace Game
{
    public class BulletSystem : IFixedExecuteSystem
    {
        public BulletSystem()
        {
        }

        public void FixedExecute()
        {
            MapComponent mapComp = Contexts.Instance.game.map;
            for (int i = mapComp.bulletList.Count - 1; i >= 0; i--)
            {
                GameEntity bulletEntity = mapComp.bulletList[i];
                if (DealEntity(bulletEntity))
                {
                    mapComp.bulletList.RemoveAt(i);
                    Util.DestroyEntity(bulletEntity);
                }
            }
        }

        public bool CrossMapGrid(Vector3 start, Vector3 target)
        {
            return (int)start.x != (int)target.x || (int)start.z != (int)target.z;
        }

        bool DealEntity(GameEntity entity)
        {
            var mapComp = Contexts.Instance.game.map;
            BoxConfig burnConfig = DataManager.Instance.boxConfig.Data;
            float deltaTime = Contexts.Instance.game.frame.frameTime;
            Vector3 offset = entity.bullet.speed * deltaTime * entity.bullet.forward;
            Vector3 start = entity.transform.position;
            entity.transform.position = start + offset;
            if (CrossMapGrid(start, entity.transform.position))
            {
                int x = (int)(entity.transform.position.x);
                int y = (int)(entity.transform.position.z);
                if (!Util.CheckCoordValid(x, y))
                    return false;
                GameEntity boxEntity = mapComp.mapData[y, x];
                if (boxEntity != null)
                {
                    if (boxEntity.box.eColor == entity.bullet.eBoxColor && boxEntity.burn.eBurnState == AkBurnState.Ak_None)
                    {
                        boxEntity.box.isPositive = false;
                        mapComp.AddBurnEntity(boxEntity);
                    }
                    return true;
                }
            }
            entity.bullet.distance += entity.bullet.speed * deltaTime;
            if (entity.bullet.distance >= burnConfig.burnRange)
                return true;

            return false;
        }
    }
}
