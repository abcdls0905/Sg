using Entitas;
using UnityEngine;
using Game;
using System;

namespace Game
{
    public class PlayerRotateSystem : IExecuteSystem, IInitializeSystem
    {
        IGroup<GameEntity> _group;
        public PlayerRotateSystem()
        {

        }

        public void Initialize()
        {
            var _context = Contexts.Instance.game;
            _group = _context.GetGroup(Matcher<GameEntity>.AllOf(GameMatcher.Rotate));
            Util.ListenCommand<RotateCommand>(HandleRotateCommand);
        }

        void HandleRotateCommand(ref RotateCommand command, ulong entityId)
        {
            GameEntity entity = Util.FindGroupEntity(_group, entityId);
            if (entity == null)
                return;
            if (command.eTurnDir == entity.move.eMoveDir)
                return;
            if (BattleManager.Instance.eCtrlMode == AkCtrlMode.Ak_Four)
            {
                entity.transform.rotation = Quaternion.Euler(Util.GetRotateByDir(command.eTurnDir));
                entity.move.eMoveDir = command.eTurnDir;
            }
            else
            {
                Quaternion rotate = entity.view.gameObject.transform.rotation;
                //entity.view.gameObject.transform.forward = command.forward;
                entity.view.gameObject.transform.forward = Vector3.forward;
                entity.transform.rotation = entity.view.gameObject.transform.rotation;
                entity.view.gameObject.transform.rotation = rotate;
                entity.move.eMoveDir = command.eTurnDir;
            }
        }
        
        public void Execute()
        {
            UpdateRotate();
        }

        public void UpdateRotate()
        {

        }
    }
}