

using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;

namespace Game
{
    public class LogEntity
    {
        public GameEntity entity;
        public string trace;
    }

    [Game]
    [Unique]
    public class MapComponent : IComponent
    {
        public int mapWidth;
        public int mapHeight;
        public GameEntity[,] mapData;
        public List<GameEntity> followBoxs = new List<GameEntity>();
        public List<GameEntity> burnEntities = new List<GameEntity>();
        public List<GameEntity> bornList = new List<GameEntity>(8);
        public List<GameEntity> bulletList = new List<GameEntity>(16);
        public List<GameEntity> monsters = new List<GameEntity>(8);
        public void Reset()
        {
            mapWidth = 10;
            mapHeight = 10;
            followBoxs.Clear();
            burnEntities.Clear();
            bornList.Clear();
            bulletList.Clear();
            monsters.Clear();
        }

        public void AddBurnEntity(GameEntity entity)
        {
            if (burnEntities.Contains(entity))
                UnityEngine.Debug.Log(Util.GetStackStrace());
            entity.burn.eBurnState = AkBurnState.Ak_Stable;
            entity.burn.burnTime = 0;
            entity.box.eSrcColor = entity.box.eColor;
            burnEntities.Add(entity);
        }
    }
}