using Entitas;
using UnityEngine;
using Game;
using System;
using GameJson;

namespace Game
{
    public class CameraSystem : IExecuteSystem
    {
        public void Execute()
        {
            CameraConfig cameraCfg = DataManager.Instance.cameraConfig.Data;
            bool isCameraDown = BattleManager.Instance.isCameraDown;
            if (isCameraDown)
            {
                GameEntity master = Contexts.Instance.game.gameMaster.entity;
                Vector3 plyPos = master.transform.position;
                Vector3 targetPos = plyPos + cameraCfg.downOffset;
                Vector3 cameraPos = Util.MainCamera.transform.position;
                if (targetPos == cameraPos)
                    return;
                Vector3 lerpPos = Vector3.Lerp(cameraPos, targetPos, Time.deltaTime * 3);
                Util.MainCamera.gameObject.transform.position = lerpPos;
            }
            else
            {
                Vector3 targetPos = cameraCfg.upPosition;
                Vector3 cameraPos = Util.MainCamera.transform.position;
                if (targetPos == cameraPos)
                    return;
                Vector3 lerpPos = Vector3.Lerp(cameraPos, targetPos, Time.deltaTime * 3);
                Util.MainCamera.gameObject.transform.position = lerpPos;
            }
        }
    }
}