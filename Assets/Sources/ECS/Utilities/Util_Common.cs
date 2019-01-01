using UnityEngine;
using System;
using System.Collections.Generic;

namespace Game
{
    public static partial class Util
    {
        private static Camera _mainCamera = null;
        private static float _gridRadius = 1 / 2.0f;

        public static Vector3 ZeroPosition = new Vector3(-10, -10, -10);

        public static float GridRadius
        {
            get
            {
                return _gridRadius;
            }
        }
        public static Camera MainCamera
        {
            get 
            {
                if (_mainCamera == null) 
                    _mainCamera = Camera.main; 
                return _mainCamera;
            }
        }

        public static void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadMode)
        {
            _mainCamera = Camera.main;
        }

        public static int Lerp(int value, int min, int max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }

        public static float GetRealTime()
        {
            return UnityEngine.Time.realtimeSinceStartup;
        }

        public static void InitializeBoxDir(GameEntity entity)
        {
            var boxConfig = DataManager.Instance.boxColorConfig.Data;
            if (BattleManager.Instance.eColorType == AkColorType.AK_TWO)
                boxConfig = DataManager.Instance.boxColor2Config.Data;
            else if (BattleManager.Instance.eColorType == AkColorType.AK_THREE)
                boxConfig = DataManager.Instance.boxColorConfig.Data;
            entity.box.eColor = boxConfig.up;
            entity.box.eFrontColor = boxConfig.front;
            entity.box.eBackColor = boxConfig.back;
            entity.box.eLeftColor = boxConfig.left;
            entity.box.eRightColor = boxConfig.right;
            entity.box.eDownColor = boxConfig.down;
        }

        public static void TurnDiceBox(GameEntity entity, AkTurnDir eTurnDir)
        {
            if (entity == null)
                return;
            BoxComponent box = entity.box;
            AkBoxColor eColor = box.eColor;
            AkBoxColor eFrontColor = box.eFrontColor;
            AkBoxColor eDownColor = box.eDownColor;
            AkBoxColor eBackColor = box.eBackColor;
            AkBoxColor eRightColor = box.eRightColor;
            AkBoxColor eLeftColor = box.eLeftColor;
            switch (eTurnDir)
            {
                case AkTurnDir.Ak_Back:
                    {
                        box.eColor = eFrontColor;
                        box.eFrontColor = eDownColor;
                        box.eBackColor = eColor;
                        box.eDownColor = eBackColor;
                        break;
                    }
                case AkTurnDir.AK_Front:
                    {
                        box.eColor = eBackColor;
                        box.eFrontColor = eColor;
                        box.eBackColor = eDownColor;
                        box.eDownColor = eFrontColor;
                        break;
                    }
                case AkTurnDir.Ak_Left:
                    {
                        box.eColor = eRightColor;
                        box.eLeftColor = eColor;
                        box.eRightColor = eDownColor;
                        box.eDownColor = eLeftColor;
                        break;
                    }
                case AkTurnDir.Ak_Right:
                    {
                        box.eColor = eLeftColor;
                        box.eLeftColor = eDownColor;
                        box.eRightColor = eColor;
                        box.eDownColor = eRightColor;
                        break;
                    }
            }
        }

        public static Vector3 GetTurnDirRotation(AkTurnDir eTurnDir)
        {
            Vector3 ret = new Vector3();
            switch (eTurnDir)
            {
                case AkTurnDir.Ak_Back:
                    {
                        ret.x = -90;
                        break;
                    }
                case AkTurnDir.AK_Front:
                    {
                        ret.x = 90;
                        break;
                    }
                case AkTurnDir.Ak_Left:
                    {
                        ret.z = 90;
                        break;
                    }
                case AkTurnDir.Ak_Right:
                    {
                        ret.z = -90;
                        break;
                    }
            }
            return ret;
        }

        public static AkTurnDir GetJoystickDir(float h, float v)
        {
            AkTurnDir dir = AkTurnDir.Ak_Max;
            if (h > 0 && v >= 0)
                dir = AkTurnDir.Ak_Right;
            else if (h >= 0 && v < 0)
                dir = AkTurnDir.Ak_Back;
            else if (h < 0 && v <= 0)
                dir = AkTurnDir.Ak_Left;
            else if (h <= 0 && v > 0)
                dir = AkTurnDir.AK_Front;
            return dir;
//             AkTurnDir dir = AkTurnDir.Ak_Max;
//             if (v > 0 && v > Mathf.Abs(h))
//                 dir = AkTurnDir.AK_Front;
//             else if (h > 0 && h >= Mathf.Abs(v))
//                 dir = AkTurnDir.Ak_Right;
//             else if (v < 0 && (-v) >= Mathf.Abs(h))
//                 dir = AkTurnDir.Ak_Back;
//             else if (h < 0 && (-h) >= Mathf.Abs(v))
//                 dir = AkTurnDir.Ak_Left;
//             return dir;
        }

        public static Vector3 GetStartMovementByDir(GameEntity entity, AkTurnDir eTurnDir, Vector3 forward)
        {
            Vector3 movement = new Vector3();
            float deltaTime = Contexts.Instance.game.frame.frameTime;
            float deltaMove = entity.move.GetMoveSpeed() * deltaTime;
            if (BattleManager.Instance.eCtrlMode == AkCtrlMode.Ak_Four)
            {
                Vector3 position = entity.transform.position;
                if (eTurnDir == AkTurnDir.AK_Front)
                    movement.z = deltaMove;
                else if (eTurnDir == AkTurnDir.Ak_Back)
                    movement.z = -deltaMove;
                else if (eTurnDir == AkTurnDir.Ak_Left)
                    movement.x = -deltaMove;
                else if (eTurnDir == AkTurnDir.Ak_Right)
                    movement.x = deltaMove;
            }
            else if (BattleManager.Instance.eCtrlMode == AkCtrlMode.Ak_360)
            {
                movement = deltaMove * forward;
            }
            return movement;
        }

        public static void GetCoordByDir(CoordComponent coord, AkTurnDir eMoveDir, ref int x, ref int y)
        {
            x = coord.x;
            y = coord.y;
            if (eMoveDir == AkTurnDir.AK_Front)
                y = coord.y + 1;
            else if (eMoveDir == AkTurnDir.Ak_Back)
                y = coord.y - 1;
            else if (eMoveDir == AkTurnDir.Ak_Left)
                x = coord.x - 1;
            else if (eMoveDir == AkTurnDir.Ak_Right)
                x = coord.x + 1;
        }

        public static Vector3 GetForwardByDir(AkTurnDir eDir)
        {
            Vector3 forward = new Vector3();
            if (eDir == AkTurnDir.AK_Front)
                forward.z = 1;
            else if (eDir == AkTurnDir.Ak_Back)
                forward.z = -1;
            else if (eDir == AkTurnDir.Ak_Left)
                forward.x = -1;
            else if (eDir == AkTurnDir.Ak_Right)
                forward.x = 1;
            return forward;
        }

        public static bool CheckCoordValid(int x, int y)
        {
            int width = BattleManager.Instance.mapSize;
            int height = BattleManager.Instance.mapSize;
            return x >= 0 && x < width && y >= 0 && y < height;
        }

        public static void GetRotatePoint(AkTurnDir eDir, int x, int y, ref Vector3 rotPoint, ref Vector3 axis)
        {
            rotPoint = new Vector3(0, -0.5f, 0);
            axis = new Vector3();
            if (eDir == AkTurnDir.AK_Front)
            {
                rotPoint.x = x;
                rotPoint.z = y + 0.5f;
                axis.x = 1;
            }
            else if (eDir == AkTurnDir.Ak_Back)
            {
                rotPoint.x = x;
                rotPoint.z = y - 0.5f;
                axis.x = -1;
            }
            else if (eDir == AkTurnDir.Ak_Left)
            {
                rotPoint.x = x - 0.5f;
                rotPoint.z = y;
                axis.z = 1;
            }
            else if (eDir == AkTurnDir.Ak_Right)
            {
                rotPoint.x = x + 0.5f;
                rotPoint.z = y;
                axis.z = -1;
            }
        }

        public static Vector3 GetRotateByDir(AkTurnDir eDir)
        {
            Vector3 rotate = new Vector3(0, 0, 0);
            if (eDir == AkTurnDir.AK_Front)
                rotate.y = 0;
            else if (eDir == AkTurnDir.Ak_Back)
                rotate.y = 180;
            else if (eDir == AkTurnDir.Ak_Left)
                rotate.y = 270;
            else
                rotate.y = 90;
            return rotate;
        }

        public static bool IsSameCoord(GameEntity entity1, GameEntity entity2)
        {
            return entity1.coord.x == entity2.coord.x && entity1.coord.y == entity2.coord.y;
        }

        public static bool IsSameCoord(CoordComponent coord1, CoordComponent coord2)
        {
            return coord1.x == coord2.x && coord1.y == coord2.y;
        }

        public static int GetEntityDistance(GameEntity entity1, GameEntity entity2)
        {
            return Mathf.Abs(entity1.coord.x - entity2.coord.x) + Mathf.Abs(entity1.coord.y - entity2.coord.y);
        }

        public static int GetCoordDistance(CoordComponent coord1, MapCoord coord2)
        {
            return Mathf.Abs(coord1.x - coord2.iX) + Mathf.Abs(coord1.y - coord2.iY);
        }

        public static GameObject CreateGameObject(string prefab, AkResourceType eResType)
        {
            GameObject gameObject = GameObjectPool.Instance.GetGameObject(prefab, eResType);
            if (gameObject == null)
            {
                Debug.LogError("not find prefab " + prefab);
                gameObject = GameObjectPool.Instance.GetGameObject("Prefabs/EmptyPrefab", eResType);
            }
            return gameObject;
        }

        public static string GetStackStrace()
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
            System.Diagnostics.StackFrame[] sfs = st.GetFrames();
            //过虑的方法名称,以下方法将不会出现在返回的方法调用列表中
            string _filterdName = "ResponseWrite,ResponseWriteError,";
            string _fullName = string.Empty, _methodName = string.Empty;
            for (int i = 1; i < sfs.Length; ++i)
            {
                //非用户代码,系统方法及后面的都是系统调用，不获取用户代码调用结束
                if (System.Diagnostics.StackFrame.OFFSET_UNKNOWN == sfs[i].GetILOffset()) break;
                _methodName = sfs[i].GetMethod().Name;//方法名称
                //sfs[i].GetFileLineNumber();//没有PDB文件的情况下将始终返回0
                if (_filterdName.Contains(_methodName)) continue;
                _fullName = _methodName + "()->" + _fullName;
            }
            st = null;
            sfs = null;
            _filterdName = _methodName = null;
            return _fullName.TrimEnd('-', '>');
        }

        public static void LoadBoxMainModel(GameEntity entity)
        {
            string box = "Prefabs/BOX_3C";
            if (BattleManager.Instance.eColorType == AkColorType.AK_TWO)
                box = "Prefabs/BOX_2C";
            if (entity.box.eBoxType == AkBoxType.Ak_Ice)
                box = "Prefabs/Box_Ice";
            ChangeEntityModel(entity, box);
        }

        public static void ChangeEntityModel(GameEntity entity, string prefab)
        {
            entity.model.chgModel.LoadMainModel(prefab);
            entity.view.gameObject.GetComponentsInChildren<Renderer>(entity.view.renderers);
            entity.animation.animator = entity.view.gameObject.GetComponentInChildren<Animator>();
        }

        public static GameEntity GetFollowBox(GameEntity player)
        {
            MapComponent mapComp = Contexts.Instance.game.map;
            for (int i = 0; i < mapComp.followBoxs.Count; i++)
            {
                GameEntity boxEntity = mapComp.followBoxs[i];
                if (boxEntity.box.followEntity == player)
                    return boxEntity;
            }
            return null;
        }

        public static bool IsCrossGrid(Vector3 position, AkTurnDir eMoveDir, int targetX, int targetY)
        {
            float gridRadius = Util.GridRadius;
            float radius = DataManager.Instance.playerConfig.Data.crossRadius;
            if (eMoveDir == AkTurnDir.AK_Front)
            {
                float z = position.z + radius;
                return z > targetY + gridRadius;
            }
            else if (eMoveDir == AkTurnDir.Ak_Back)
            {
                float z = position.z -radius;
                return z < targetY - gridRadius;
            }
            else if (eMoveDir == AkTurnDir.Ak_Left)
            {
                float x = position.x - radius;
                return x < targetX - gridRadius;
            }
            else if (eMoveDir == AkTurnDir.Ak_Right)
            {
                float x = position.x + radius;
                return x > targetX + gridRadius;
            }
            return false;
        }

        public static float GetRotationRate(GameEntity follower)
        {
            return follower.move.GetMoveSpeed() * 90;
        }

        public static float GetMoveExTime(GameEntity entity)
        {
            return 500 / entity.move.GetMoveSpeed() /1000;
        }

        public static void ChangeBurnAnimator(GameEntity entity)
        {
            GameJson.BoxConfig burnConfig = DataManager.Instance.boxConfig.Data;
            if (entity.burn.burnTime <= burnConfig.stableTime / 1000)
            {
                entity.animation.animator.SetTrigger("stable");
            }
            else
            {
                entity.animation.animator.SetTrigger("danger");
            }
        }

        public static GameEntity GetEntityWithId(ulong uuid)
        {
            var game = Contexts.Instance.game;
            if (game != null)
                return game.GetEntityWithID(uuid);
            return null;
        }

        public static void ResetPositionY(GameEntity entity)
        {
            Vector3 position = entity.transform.position;
            entity.transform.position = new Vector3(position.x, 0.5f, position.z);
        }

        public static GameObject GetEntityObject(GameEntity entity)
        {
            if (entity == null)
                return null;

            if (entity.hasView)
                return entity.view.gameObject;
            return null;
        }

        public static void PlayUIAudio()
        {
            AudioManager.Instance.PlayAudio("Audio/ui", Util.MainCamera.gameObject);
        }

        public static FrameCommand<T> CreateFrameCommand<T>() where T : ICommand
        {
            FrameCommand<T> frameCommand = new FrameCommand<T>();
            frameCommand.command = default(T);
            frameCommand.entityId = 0;
            return frameCommand;
        }

        public static GameEntity FindGroupEntity(Entitas.IGroup<GameEntity> group, ulong id)
        {
            var iter = group.GetEntities().GetEnumerator();
            while (iter.MoveNext())
            {
                if (iter.Current.iD.value == id)
                    return iter.Current;
            }
            return null;
        }

        public static AkTurnDir GetDirByCoord(int x, int y, int x1, int y1)
        {
            if (x1 == x)
            {
                if (y1 < y)
                    return AkTurnDir.Ak_Back;
                else
                    return AkTurnDir.AK_Front;
            }
            if (y1 == y)
            {
                if (x1 < x)
                    return AkTurnDir.Ak_Left;
                else
                    return AkTurnDir.Ak_Right;
            }
            return AkTurnDir.Ak_Max;
        }

        public static bool IsAbsCrossGrid(GameEntity entity, AkTurnDir eMoveDir)
        {
            int width = BattleManager.Instance.mapSize;
            int height = BattleManager.Instance.mapSize;
            Vector3 position = entity.transform.position;
            float gridRadius = Util.GridRadius;
            if (eMoveDir == AkTurnDir.AK_Front)
            {
                float z = position.z;
                float coordZ = entity.coord.y + gridRadius;
                if (z > coordZ && z < height)
                    return true;
            }
            else if (eMoveDir == AkTurnDir.Ak_Back)
            {
                float z = position.z;
                float coordZ = entity.coord.y - gridRadius;
                if (z < coordZ && z < height)
                    return true;
            }
            else if (eMoveDir == AkTurnDir.Ak_Left)
            {
                float x = position.x;
                float coordX = entity.coord.x - gridRadius;
                if (x < coordX && x >= 0)
                    return true;
            }
            else if (eMoveDir == AkTurnDir.Ak_Right)
            {
                float x = position.x;
                float coordX = entity.coord.x + gridRadius;
                if (x > coordX && x >= 0)
                    return true;
            }
            return false;
        }

        public static void DestroyAllMonster()
        {
            var mapComp = Contexts.Instance.game.map;
            for (int i = mapComp.monsters.Count - 1; i >= 0; i--)
            {
                GameEntity monster = mapComp.monsters[i];
                Util.DestroyEntity(monster);
            }
        }

        public static AkTurnDir GetOppositeDir(AkTurnDir eDir)
        {
            if (eDir == AkTurnDir.AK_Front)
                return AkTurnDir.Ak_Back;
            else if (eDir == AkTurnDir.Ak_Back)
                return AkTurnDir.AK_Front;
            else if (eDir == AkTurnDir.Ak_Left)
                return AkTurnDir.Ak_Right;
            else if (eDir == AkTurnDir.Ak_Right)
                return AkTurnDir.Ak_Left;
            return AkTurnDir.Ak_Max;
        }

        public static void SetEntityCoord(GameEntity entity, int coordX, int coordZ)
        {
            entity.coord.x = coordX;
            entity.coord.y = coordZ;
        }

        public static bool CheckCrossCoord(GameEntity entity, AkTurnDir eMoveDir)
        {
            int width = BattleManager.Instance.mapSize;
            int height = BattleManager.Instance.mapSize;
            Vector3 position = entity.transform.position;
            float gridRadius = Util.GridRadius;
            if (eMoveDir == AkTurnDir.AK_Front)
            {
                float z = position.z;
                float coordZ = entity.coord.y + gridRadius;
                if (z > coordZ && z < height)
                {
                    SetEntityCoord(entity, entity.coord.x, entity.coord.y + 1);
                    return true;
                }
            }
            else if (eMoveDir == AkTurnDir.Ak_Back)
            {
                float z = position.z;
                float coordZ = entity.coord.y - gridRadius;
                if (z < coordZ && z < height)
                {
                    SetEntityCoord(entity, entity.coord.x, entity.coord.y - 1);
                    return true;
                }
            }
            else if (eMoveDir == AkTurnDir.Ak_Left)
            {
                float x = position.x;
                float coordX = entity.coord.x - gridRadius;
                if (x < coordX && x >= 0)
                {
                    SetEntityCoord(entity, entity.coord.x - 1, entity.coord.y);
                    return true;
                }
            }
            else if (eMoveDir == AkTurnDir.Ak_Right)
            {
                float x = position.x;
                float coordX = entity.coord.x + gridRadius;
                if (x > coordX && x >= 0)
                {
                    SetEntityCoord(entity, entity.coord.x + 1, entity.coord.y);
                    return true;
                }
            }
            return false;
        }

        public static void GetCoordByPosition(GameEntity entity, ref int x, ref int y)
        {
            Vector3 position = entity.transform.position;
            float gridRadius = Util.GridRadius;
            x = (int)Mathf.Ceil(position.x - gridRadius);
            y = (int)Mathf.Ceil(position.z - gridRadius);
        }

        static void CheckPlayerOn(GameEntity boxEntity)
        {
            if (Util.IsSameCoord(boxEntity, Contexts.Instance.game.gameMaster.entity))
            {
                ECSManager.Instance.GameEnd();
            }
        }

        static void EmissinBullet(GameEntity entity, AkTurnDir eDir, ref GameEntity target, ref int minDistance)
        {
            MapComponent mapComp = Contexts.Instance.game.map;
            int x = 0;
            int y = 0;
            Util.GetCoordByDir(entity.coord, eDir, ref x, ref y);
            if (!Util.CheckCoordValid(x, y))
                return;
            GameEntity nextEntity = mapComp.mapData[y, x];
//             if (nextEntity != null && nextEntity.box.eColor != entity.box.eColor)
//                 return;
//             if (nextEntity != null)
//             {
//                 if (nextEntity.box.eColor == entity.box.eColor && nextEntity.burn.eBurnState == AkBurnState.Ak_None)
//                 {
//                     nextEntity.box.isPositive = false;
//                     mapComp.AddBurnEntity(nextEntity);
//                 }
//             }
//             else
            {
                //产生火花
                GameEntity bulletEntity = Util.CreateBullet(Util.GetEntityId());
                bulletEntity.bullet.forward = Util.GetForwardByDir(eDir);
                bulletEntity.transform.position = entity.transform.position;
                bulletEntity.bullet.eBoxColor = entity.box.eColor;
                mapComp.bulletList.Add(bulletEntity);
            }
        }

        static void DealBombArround(GameEntity entity)
        {
            int burnRange = DataManager.Instance.boxConfig.Data.burnRange;
            int minDistance = 100;
            GameEntity targetEntity = null;
            for (int i = 0; i < (int)AkTurnDir.Ak_Max; i++)
                EmissinBullet(entity, (AkTurnDir)i, ref targetEntity, ref minDistance);
        }

        static void ForceRefreshFollowBox(GameEntity entity)
        {
            MapComponent mapComp = Contexts.Instance.game.map;
            if (mapComp.followBoxs.Contains(entity))
            {
                mapComp.followBoxs.Remove(entity);
                entity.coord.x = entity.box.followEntity.coord.x;
                entity.coord.y = entity.box.followEntity.coord.y;
            }
        }
        public static void BoomBoxList(List<GameEntity> boombList)
        {
            if (boombList.Count > 0)
            {
                MapComponent mapComp = Contexts.Instance.game.map;
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
                    if (burnEntity.box.eBoxType == AkBoxType.Ak_Normal)
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
    }
}