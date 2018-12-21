using Entitas;
using UnityEngine;
using Game;
using GameJson;
using System.Collections.Generic;

namespace Game
{
    public class AISystem : IInitializeSystem, IFixedExecuteSystem
    {
        private IGroup<GameEntity> group;
        public void Initialize()
        {
            var game = Contexts.Instance.game;
            EventManager.Instance.AddEvent<DesGroupParam>(GEventType.EVENT_BOXDESTORYGROUP, DestroyGroup);
            group = game.GetGroup(GameMatcher.AI);
        }

        List<AkTurnDir> turnList = new List<AkTurnDir>(4);

        bool IsValidCoord(int x, int y)
        {
            MapComponent mapComp = Contexts.Instance.game.map;
            if (Util.CheckCoordValid(x, y))
            {
                GameEntity boxEntity = mapComp.mapData[y, x];
                return boxEntity != null;
            }
            return false;
        }

        void ExecuteBehavirous()
        {
            MapComponent mapComp = Contexts.Instance.game.map;
            float frameTime = Contexts.Instance.game.frame.frameTime;
            var iter = group.GetEntities().GetEnumerator();
            while (iter.MoveNext())
            {
                turnList.Clear();
                GameEntity entity = iter.Current;
                AIComponent ai = entity.aI;
                ai.runTime += frameTime;
                if (ai.runTime > ai.period)
                {
                    ai.runTime = 0;
                    //todo
                    //Ѱ·
                    int coordX = entity.coord.x;
                    int coordY = entity.coord.y;
                    int tempX = coordX - 1;
                    int tempY = coordY;
                    if (IsValidCoord(tempX, tempY))
                    {
                        AkTurnDir eDir = Util.GetDirByCoord(coordX, coordY, tempX, tempY);
                        turnList.Add(eDir);
                    }
                    tempX = coordX + 1;
                    tempY = coordY;
                    if (IsValidCoord(tempX, tempY))
                    {
                        AkTurnDir eDir = Util.GetDirByCoord(coordX, coordY, tempX, tempY);
                        turnList.Add(eDir);
                    }
                    tempX = coordX;
                    tempY = coordY - 1;
                    if (IsValidCoord(tempX, tempY))
                    {
                        AkTurnDir eDir = Util.GetDirByCoord(coordX, coordY, tempX, tempY);
                        turnList.Add(eDir);
                    }
                    tempX = coordX;
                    tempY = coordY + 1;
                    if (IsValidCoord(tempX, tempY))
                    {
                        AkTurnDir eDir = Util.GetDirByCoord(coordX, coordY, tempX, tempY);
                        turnList.Add(eDir);
                    }
                }
                if (turnList.Count == 0)
                    continue;
                entity.aI.isMoving = true;
                entity.aI.moveDistance = 1.0f;
                int index = Random.Range(0, turnList.Count);
                AkTurnDir eMoveDir = turnList[index];
                ai.eMoveDir = eMoveDir;
                RotateCommand rotCommand = new RotateCommand();
                rotCommand.eTurnDir = eMoveDir;
                Util.PushCommand<RotateCommand>(ref rotCommand, entity.iD.value);
                if (!entity.move.moveStateMachine.IsState("NormalMoveState"))
                    entity.move.moveStateMachine.ChangeState("NormalMoveState");
            }
        }

        float Vec2Distance(float x, float y, float x1, float y1)
        {
            Vector2 v = new Vector2(x, y);
            Vector2 v1 = new Vector2(x1, y1);
            return Vector2.Distance(v, v1);
        }

        void ExecuteMove()
        {
            MapComponent mapComp = Contexts.Instance.game.map;
            float frameTime = Contexts.Instance.game.frame.frameTime;
            var iter = group.GetEntities().GetEnumerator();
            while (iter.MoveNext())
            {
                GameEntity entity = iter.Current;
                AIComponent ai = entity.aI;
                if (!ai.isMoving)
                    continue;
                int x = 0;
                int y = 0;
                Util.GetCoordByDir(entity.coord, ai.eMoveDir, ref x, ref y);
                if (Util.CheckCoordValid(x, y))
                {
                    GameEntity temp = mapComp.mapData[y, x];
                    if ((temp == null || temp.box.isRoting) && !entity.aI.isBack && entity.aI.moveOffset > 0.0f)
                    {
                        //目标方块不见了
                        ai.moveDistance = Vec2Distance(entity.transform.position.x, entity.transform.position.z, entity.coord.x, entity.coord.y);
                        ai.eMoveDir = Util.GetOppositeDir(ai.eMoveDir);
                        ai.runTime = 0;
                        ai.moveOffset = 0;
                        ai.isBack = true;
                        RotateCommand rotCommand = new RotateCommand();
                        rotCommand.eTurnDir = ai.eMoveDir;
                        Util.PushCommand<RotateCommand>(ref rotCommand, entity.iD.value);
                        continue;
                    }
                }
                Vector3 forward = Util.GetForwardByDir(ai.eMoveDir);
                float speed = 2.0f;
                float frameOffset = frameTime * speed;
                ai.moveOffset += frameOffset;
                if (ai.moveOffset >= ai.moveDistance)
                {
                    frameOffset = ai.moveOffset - ai.moveDistance;
                    int nowX = 0;
                    int nowY = 0; ;
                    Util.GetCoordByPosition(entity, ref nowX, ref nowY);
                    entity.coord.x = nowX;
                    entity.coord.y = nowY;
                    ai.ClearTemp();
                    if (!entity.move.moveStateMachine.IsState("StopMoveState"))
                        entity.move.moveStateMachine.ChangeState("StopMoveState");
                }
                Vector3 offset = forward * frameOffset;
                entity.transform.position += offset;
            }

        }

        void ExecuteCheckPlayer()
        {
            float distance = 0.5f;
            GameEntity master = Contexts.Instance.game.gameMaster.entity;
            var iter = group.GetEntities().GetEnumerator();
            while (iter.MoveNext())
            {
                GameEntity monster = iter.Current;
                Vector3 monsPos = monster.transform.position;
                Vector3 plyPos = master.transform.position;
                float dis = Vector3.Distance(monsPos, plyPos);
                if (dis <= distance || Util.IsSameCoord(monster, master))
                    ECSManager.Instance.GameEnd();
            }
        }

        public void FixedExecute()
        {
            ExecuteBehavirous();
            ExecuteMove();
            ExecuteCheckPlayer();
        }

        void DestroyGroup(ref DesGroupParam param)
        {
            var mapComp = Contexts.Instance.game.map;
            for (int i = mapComp.monsters.Count - 1; i >= 0; i--)
            {
                GameEntity monster = mapComp.monsters[i];
                for (int j = 0; j < param.boxes.Count; j++)
                {
                    GameEntity entity = param.boxes[j];
                    CoordComponent coord1 = monster.coord;
                    CoordComponent coord2 = entity.coord;
                    if (Util.IsSameCoord(monster.coord, entity.coord))
                    {
                        Util.DestroyEntity(monster);
                        mapComp.monsters.RemoveAt(i);
                        break;
                    }
                }
            }
        }
    }
}