using Entitas;
using Entitas.Unity;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace Game
{
    public class RandLevelSystem : IInitializeSystem, IFixedExecuteSystem
    {
        public float lastTime = 0;
        public List<MapCoord> emptyList;
        public void Initialize()
        {
            InitializeMap();
            lastTime = 0;
            Util.ListenCommand<RestartCommand>(HandleRestartCommand);
            var gameMaster = Contexts.Instance.game.gameMaster.entity;
            AudioManager.Instance.PlayAudio("Audio/bgm", gameMaster.view.gameObject, true);
            EventManager.Instance.AddEvent<DesGroupParam>(GEventType.EVENT_BOXDESTORYGROUP, DestroyGroup);
        }

        void HandleRestartCommand(ref RestartCommand command, ulong entityId)
        {
            var mapComp = Contexts.Instance.game.map;
            int mapWidth = BattleManager.Instance.mapSize;
            int mapHeight = BattleManager.Instance.mapSize;
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    GameEntity entity = mapComp.mapData[j, i];
                    if (entity != null)
                    {
                        Util.DestroyEntity(entity);
                        mapComp.mapData[j, i] = null;
                    }
                }
            }
            for (int i = 0; i < mapComp.bornList.Count; i++)
            {
                GameEntity entity = mapComp.bornList[i];
                Util.DestroyEntity(entity);
            }
            for (int i = 0; i < mapComp.bulletList.Count; i++)
            {
                GameEntity entity = mapComp.bulletList[i];
                Util.DestroyEntity(entity);
            }
            mapComp.bornList.Clear();
            mapComp.burnEntities.Clear();
            mapComp.followBoxs.Clear();
            mapComp.bulletList.Clear();
            Contexts.Instance.game.effect.Reset();
            InitializeMap();
            ECSManager.Instance.Resume();
        }

        void InitializeMap()
        {
            int mapWidth = BattleManager.Instance.mapSize;
            int mapHeight = BattleManager.Instance.mapSize;
            var mapComp = Contexts.Instance.game.map;
            mapComp.mapWidth = mapWidth;
            mapComp.mapHeight = mapHeight;
            mapComp.mapData = new GameEntity[mapHeight, mapWidth];
            GameJson.LevelConfig levelConfig = DataManager.Instance.levelConfig.Data;
            int boxCount = 25;
            //初始位置
            int ix = (mapWidth + 1) / 2;
            int iy = (mapWidth + 1) / 2;
            if (mapWidth == 7)
            {
                ix = levelConfig.sevenX;
                iy = levelConfig.sevenY;
                boxCount = levelConfig.sevenCount;
            }
            else
            {
                boxCount = levelConfig.tenCount;
                ix = levelConfig.tenX;
                iy = levelConfig.tenY;
            }
            emptyList = new List<MapCoord>(mapWidth * mapHeight);

            List<MapCoord> list = new List<MapCoord>();
            list.Add(new MapCoord(ix, iy));

            for (int i = 0; i < boxCount - 1; i++)
            {
                int x = Random.Range(0, mapWidth);
                int y = Random.Range(0, mapHeight);
                if (ContainMapCoord(list, x, y))
                {
                    i -= 1;
                    continue;
                }
                list.Add(new MapCoord(x, y));
            }
            GameObject gameObject = new GameObject();
            for (int i = 0; i < list.Count; i++)
            {
                MapCoord coord = list[i];
                GameEntity boxEntity = Util.CreateBox(Util.GetEntityId());
                Util.LoadBoxMainModel(boxEntity);
                Util.InitializeBoxDir(boxEntity);

                int randIndex = Random.Range(0, 10000) % (int)AkTurnDir.Ak_Max;
                AkTurnDir eTurnDir = (AkTurnDir)randIndex;
                Util.TurnDiceBox(boxEntity, eTurnDir);
                Vector3 rot = Util.GetTurnDirRotation(eTurnDir);
                boxEntity.transform.rotation = Quaternion.Euler(rot);

                boxEntity.transform.position = new Vector3(coord.iX, 0, coord.iY);
                boxEntity.coord.x = coord.iX;
                boxEntity.coord.y = coord.iY;
                mapComp.mapData[coord.iY, coord.iX] = boxEntity;
            }
            GameJson.PlayerConfig plyCfg = DataManager.Instance.playerConfig.Data;
            var gameMaster = Contexts.Instance.game.gameMaster.entity;
            gameMaster.transform.position = new Vector3(ix, plyCfg.defaultHeight, iy);
            gameMaster.coord.x = ix;
            gameMaster.coord.y = iy;
            AudioManager.Instance.PlayAudio("Audio/playstart", gameMaster.view.gameObject);

            //出生动画

            EffectData effectData = new EffectData();
            effectData.name = "Prefabs/Effect/character_spawn";
            effectData.position = gameMaster.transform.position;
            effectData.lifeTime = 2.0f;
            EffectComponent effectComp = Contexts.Instance.game.effect;
            effectComp.effects.Add(effectData);
        }

        bool ContainMapCoord(List<MapCoord> list, int x, int y)
        {
            for (int i = 0; i < list.Count; i++)
            {
                MapCoord coord = list[i];
                if (coord.iX == x && coord.iY == y)
                    return true;
            }
            return false;
        }

        bool IsBornListContain(int x, int y)
        {
            MapComponent mapComp = Contexts.Instance.game.map;
            List<GameEntity> bornList = mapComp.bornList;
            for (int i = 0; i < bornList.Count; i++)
            {
                CoordComponent coord = bornList[i].coord;
                if (coord.x == x && coord.y == y)
                    return true;
            }
            return false;
        }

        void RefreshEmptyGrid()
        {
            emptyList.Clear();
            MapComponent mapComp = Contexts.Instance.game.map;
            for (int i = 0; i < mapComp.mapWidth; i++)
            {
                for (int j = 0; j < mapComp.mapHeight; j++)
                {
                    GameEntity entity = mapComp.mapData[j, i];
                    if (entity == null && !IsBornListContain(i, j))
                        emptyList.Add(new MapCoord(i, j));
                }
            }
        }

        void UpdateRandBorn()
        {
            MapComponent mapComp = Contexts.Instance.game.map;
            List<GameEntity> bornList = mapComp.bornList;
            GameJson.LevelConfig levelConfig = DataManager.Instance.levelConfig.Data;
            lastTime += Contexts.Instance.game.frame.frameTime;
            float randTime = levelConfig.randTime;
            if (lastTime >= randTime / 1000)
            {
                lastTime = 0;
                RefreshEmptyGrid();
                if (emptyList.Count > 0)
                {
                    int index = Random.Range(0, emptyList.Count);
                    MapCoord coord = emptyList[index];

                    GameEntity boxEntity = Util.CreateBox(Util.GetEntityId());
                    Util.LoadBoxMainModel(boxEntity);
                    Util.InitializeBoxDir(boxEntity);
                    int randIndex = Random.Range(0, 10000) % (int)AkTurnDir.Ak_Max;
                    AkTurnDir eTurnDir = (AkTurnDir)randIndex;
                    Util.TurnDiceBox(boxEntity, eTurnDir);
                    Vector3 rot = Util.GetTurnDirRotation(eTurnDir);
                    boxEntity.transform.position = new Vector3(coord.iX, -0.5f, coord.iY);
                    rot += boxEntity.transform.rotation.eulerAngles;
                    boxEntity.transform.rotation = Quaternion.Euler(rot);
                    boxEntity.coord.x = coord.iX;
                    boxEntity.coord.y = coord.iY;

                    bornList.Add(boxEntity);

                    //effect
                    EffectData effectData = new EffectData();
                    effectData.name = "Prefabs/Effect/cube_spawn_01a";
                    effectData.position = boxEntity.transform.position;
                    effectData.lifeTime = 0.8f;
                    EffectComponent effectComp = Contexts.Instance.game.effect;
                    effectComp.effects.Add(effectData);

                    GameEntity master = Contexts.Instance.game.gameMaster.entity;
                    AudioManager.Instance.PlayAudio("Audio/boxborn", master.view.gameObject);
                }
                else
                {
                    ECSManager.Instance.GameEnd();
                }
            }
        }

        void UpdateBornPos()
        {
            MapComponent mapComp = Contexts.Instance.game.map;
            List<GameEntity> bornList = mapComp.bornList;
            for (int i = bornList.Count - 1; i >= 0; i--)
            {
                GameEntity entity = bornList[i];
                Vector3 position = entity.transform.position;
                entity.transform.position = new Vector3(position.x, position.y + 0.05f, position.z);
                if (entity.transform.position.y >= 0)
                {
                    entity.transform.position = new Vector3(position.x, 0, position.z);
                    bornList.RemoveAt(i);
                    mapComp.mapData[entity.coord.y, entity.coord.x] = entity;
                }
            }
        }

        public void DestroyGroup(ref DesGroupParam param)
        {
            var levelCfg = DataManager.Instance.levelConfig.Data;
            var burnConfig = DataManager.Instance.boxConfig.Data;
            LevelComponent levelComp = new LevelComponent();
            levelComp.destroyCount += param.count;
            int difLevel = levelComp.destroyCount / levelCfg.difficultyBox + 1;
            if (difLevel != levelComp.level)
            {
                levelComp.level = difLevel;
                if (difLevel < levelCfg.levelOne)
                {
                    levelComp.randTime = levelCfg.randTime - 500 * (difLevel - 1);
                }
                else if (difLevel >= levelCfg.levelOne && difLevel < levelCfg.levelTwo)
                {
                    levelComp.randTime = 1000;
                    levelComp.stableTime = burnConfig.stableTime - 500 * (difLevel - 9);
                }
                else
                {
                    levelComp.randTime = 1000;
                    levelComp.stableTime = 2000;
                }
                levelComp.levelBox = 30 + 10 * (difLevel - 1);
            }
        }

        public void FixedExecute()
        {
            UpdateRandBorn();
            UpdateBornPos();
        }
    }
}