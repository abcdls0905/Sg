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
        public float lastTimeIce = 0;
        public List<MapCoord> emptyList;
        public void Initialize()
        {
            InitializeMap();
            lastTime = 0;
            lastTimeIce = 0;
            Util.ListenCommand<RestartCommand>(HandleRestartCommand);
            var gameMaster = Contexts.Instance.game.gameMaster.entity;
            AudioManager.Instance.PlayAudio("Audio/bgm", gameMaster.view.gameObject, true);
            EventManager.Instance.AddEvent<LevelParam>(GEventType.EVENT_LEVELCHG, OnLevelChg);
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
            Util.DestroyAllMonster();
            mapComp.bornList.Clear();
            mapComp.burnEntities.Clear();
            mapComp.followBoxs.Clear();
            mapComp.bulletList.Clear();
            mapComp.monsters.Clear();
            Contexts.Instance.game.effect.Reset();
            InitializeMap();
            LevelParam param = new LevelParam();
            param.level = 1;
            EventManager.Instance.PushEvent<LevelParam>(GEventType.EVENT_LEVELCHG, ref param);

            Contexts.Instance.game.levelTerms.ClearTerms();
            Contexts.Instance.game.score.value = 0;
            ScoreParam param1 = new ScoreParam();
            EventManager.Instance.PushEvent(GEventType.EVENT_SCORECHANGE, ref param1);

            RefTermsParam param2 = new RefTermsParam();
            EventManager.Instance.PushEvent<RefTermsParam>(GEventType.EVENT_REFTERMS, ref param2);

            TermsChangeParam tCParam = new TermsChangeParam();
            EventManager.Instance.PushEvent<TermsChangeParam>(GEventType.EVENT_TERMSCHANGE, ref tCParam);
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
            gameMaster.move.moveStateMachine.ChangeState("StopMoveState");

            RandMonster();

            //出生动画
            EffectData effectData = new EffectData();
            effectData.name = "Prefabs/Effect/character_spawn";
            effectData.position = gameMaster.transform.position;
            effectData.lifeTime = 2.0f;
            EffectComponent effectComp = Contexts.Instance.game.effect;
            effectComp.effects.Add(effectData);
        }

        public List<MapCoord> randList = new List<MapCoord>(100);

        void RandMonster()
        {
            randList.Clear();
            GameEntity master = Contexts.Instance.game.gameMaster.entity;
            var mapComp = Contexts.Instance.game.map;
            for (int i = 0; i < mapComp.mapWidth; i++)
            {
                for (int j = 0; j < mapComp.mapHeight; j++)
                {
                    GameEntity entity = mapComp.mapData[j, i];
                    if (entity != null && !Util.IsSameCoord(master.coord, entity.coord))
                        randList.Add(new MapCoord(i, j));
                }
            }
            GameJson.PlayerConfig plyCfg = DataManager.Instance.playerConfig.Data;
            int index = Random.Range(0, randList.Count);

            MapCoord coord = randList[index];
            int x = coord.iX;
            int y = coord.iY;

            GameEntity monster = Util.CreateMonster(Util.GetEntityId());
            monster.coord.x = x;
            monster.coord.y = y;
            monster.transform.position = new Vector3(x, plyCfg.defaultHeight, y);
            mapComp.monsters.Add(monster);
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

        bool GenerateBox(AkBoxType eBoxType = AkBoxType.Ak_Normal)
        {
            RefreshEmptyGrid();
            MapComponent mapComp = Contexts.Instance.game.map;
            List<GameEntity> bornList = mapComp.bornList;
            if (emptyList.Count > 0)
            {
                int index = Random.Range(0, emptyList.Count);
                MapCoord coord = emptyList[index];

                GameEntity boxEntity = Util.CreateBox(Util.GetEntityId(), eBoxType);
                boxEntity.box.eBoxType = eBoxType;
                Util.LoadBoxMainModel(boxEntity);
                if (eBoxType == AkBoxType.Ak_Normal)
                {
                    Util.InitializeBoxDir(boxEntity);
                    int randIndex = Random.Range(0, 10000) % (int)AkTurnDir.Ak_Max;
                    AkTurnDir eTurnDir = (AkTurnDir)randIndex;
                    Util.TurnDiceBox(boxEntity, eTurnDir);
                    Vector3 rot = Util.GetTurnDirRotation(eTurnDir);
                    rot += boxEntity.transform.rotation.eulerAngles;
                    boxEntity.transform.rotation = Quaternion.Euler(rot);
                }
                boxEntity.transform.position = new Vector3(coord.iX, -0.5f, coord.iY);
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
                return true;
            }
            return false;
        }

        void UpdateRandBorn()
        {
            MapComponent mapComp = Contexts.Instance.game.map;
            LevelComponent levelComp = Contexts.Instance.game.level;
            List<GameEntity> bornList = mapComp.bornList;
            GameJson.LevelConfig levelConfig = DataManager.Instance.levelConfig.Data;
            lastTime += Contexts.Instance.game.frame.frameTime;
            float randTime = levelComp.randTime;
            if (lastTime >= randTime / 1000)
            {
                lastTime = 0;
                if (GenerateBox())
                {
                    levelComp.randBox++;
                    if (levelComp.randBox > levelConfig.randMonster)
                    {
                        levelComp.randBox = 0;
                        RandMonster();
                    }
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

        public void OnLevelChg(ref LevelParam param)
        {
            var levelCfg = DataManager.Instance.levelConfig.Data;
            var burnConfig = DataManager.Instance.boxConfig.Data;
            LevelComponent levelComp = Contexts.Instance.game.level;
            LevelTermsComponent terms = Contexts.Instance.game.levelTerms;
            levelComp.destroyCount = 0;
            int level = param.level;
            levelComp.level = level;
            if (levelComp.level < levelCfg.levelOne)
            {
                levelComp.randTime = levelCfg.randTime - 500 * (level - 1);
            }
            else if (level >= levelCfg.levelOne && level < levelCfg.levelTwo)
            {
                levelComp.randTime = 1000;
                levelComp.stableTime = burnConfig.stableTime - 500 * (levelComp.level - 9);
            }
            else
            {
                levelComp.randTime = 1000;
                levelComp.stableTime = 2000;
            }
        }

        int GetBoxCount()
        {
            int count = 0;
            MapComponent mapComp = Contexts.Instance.game.map;
            for (int i = 0; i < mapComp.mapWidth; i++)
            {
                for (int j = 0; j < mapComp.mapHeight; j++)
                {
                    GameEntity entity = mapComp.mapData[j, i];
                    if (entity != null)
                        count++;
                }
            }
            return count;
        }

        //应急刷新策略
        void DestroyGroup(ref DesGroupParam param)
        {
            int count = 0;
            var mapComp = Contexts.Instance.game.map;
            for (int j = 0; j < param.boxes.Count; j++)
            {
                GameEntity entity = param.boxes[j];
                if (entity.box.isPositive)
                    count++;
            }
            int boxCount = GetBoxCount();
            int remain = boxCount - param.boxes.Count;
            var levelCfg = DataManager.Instance.levelConfig.Data;
            if (remain < levelCfg.urgentNum)
            {
                int addNum = count + levelCfg.urgentAdd;
                for (int i = 0; i < addNum; i++)
                {
                    GenerateBox();
                }
            }
        }

        public void UpdateTimeLeft()
        {
            var frame = Contexts.Instance.game.frame;
            LevelComponent level = Contexts.Instance.game.level;
            level.timeLeft -= frame.frameTime;
            if (level.timeLeft <= 0)
            {
                ECSManager.Instance.GameEnd();
            }
        }

        void UpdateIceBorn()
        {
            LevelComponent levelComp = Contexts.Instance.game.level;
            GameJson.LevelConfig levelConfig = DataManager.Instance.levelConfig.Data;
            GameJson.IceBoxConfig iceConfig = null;
            if (!levelConfig.dicIces.TryGetValue(levelComp.level, out iceConfig))
                return;
            MapComponent mapComp = Contexts.Instance.game.map;
            List<GameEntity> bornList = mapComp.bornList;
            lastTimeIce += Contexts.Instance.game.frame.frameTime;
            if (lastTimeIce >= iceConfig.intervalTime / 1000)
            {
                lastTimeIce = 0;
                for (int i = 0; i < iceConfig.count; ++i)
                {
                    if (!GenerateBox(AkBoxType.Ak_Ice))
                        ECSManager.Instance.GameEnd();
                }
            }
        }

        public void FixedExecute()
        {
            UpdateRandBorn();
            //UpdateIceBorn();
            UpdateBornPos();
            UpdateTimeLeft();
        }
    }
}