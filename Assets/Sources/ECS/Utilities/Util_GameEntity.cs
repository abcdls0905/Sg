using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    public static partial class Util
    {
        static ulong entityId = 0;
        public static ulong GetEntityId()
        {
            entityId += 1;
            return entityId;
        }
        public static GameEntity CreatePlayer(ulong uuid)
        {
            var game = Contexts.Instance.game;
            GameEntity entity = game.CreateEntity();
            entity.AddID(uuid);
            entity.AddTransform();
            entity.AddPlayer();
            entity.AddView("Prefabs/funnyboy_001");
            entity.AddMove();
            entity.AddCoord();
            entity.AddAnimation();
            entity.AddLimit();
            entity.AddAudio();
            entity.AddRotate();
            entity.AddItem();
            return entity;
        }

        public static void RemovePlayer(GameEntity entity)
        {
            entity.RemoveID();
            entity.RemoveTransform();
            entity.RemoveView();
            entity.RemoveMove();
            entity.RemoveCoord();
            entity.RemoveAnimation();
            entity.RemoveLimit();
            entity.RemoveAudio();
            entity.RemoveRotate();
            entity.RemoveItem();
        }

        public static GameEntity CreateBox(ulong uuid, AkBoxType eBoxType = AkBoxType.Ak_Normal)
        {
            var game = Contexts.Instance.game;
            GameEntity entity = game.CreateEntity();
            entity.AddID(uuid);
            entity.AddTransform();

            //todo
//             string box = "Prefabs/BOX_3C";
//             if (BattleManager.Instance.eColorType == AkColorType.AK_TWO)
//                 box = "Prefabs/BOX_2C";
//             entity.AddView(box);
            entity.AddView("Prefabs/Box");
            entity.AddAnimation();
            entity.AddBox();
            entity.AddCoord();
            entity.AddBurn();
            entity.AddModel();
            entity.box.eBoxType = eBoxType;
            GameJson.BoxConfig boxConfig = DataManager.Instance.boxConfig.Data;
            if (eBoxType == AkBoxType.Ak_Ice)
                entity.box.iceCount = boxConfig.iceCount;
            return entity;
        }

        public static void RemoveBox(GameEntity entity)
        {
            entity.RemoveID();
            entity.RemoveTransform();
            entity.RemoveView();
            entity.RemoveAnimation();
            entity.RemoveBox();
            entity.RemoveCoord();
            entity.RemoveBurn();
            entity.RemoveModel();
        }

        public static GameEntity CreateBullet(ulong uuid)
        {
            var game = Contexts.Instance.game;
            GameEntity entity = game.CreateEntity();
            entity.AddID(uuid);
            entity.AddTransform();
            entity.AddView("Prefabs/Effect/cube_explosion_flare01");
            entity.AddBullet();
            return entity;
        }

        public static void RemoveBullet(GameEntity entity)
        {
            entity.RemoveID();
            entity.RemoveTransform();
            entity.RemoveView();
            entity.RemoveBullet();
        }

        public static GameEntity CreateMonster(ulong uuid)
        {
            var game = Contexts.Instance.game;
            GameEntity entity = game.CreateEntity();
            entity.AddID(uuid);
            entity.AddTransform();
            entity.AddView("Prefabs/Monster_New1");
            entity.AddMove();
            entity.AddCoord();
            entity.AddAnimation();
            entity.AddAI();
            entity.AddRotate();
            return entity;
        }

        public static void RemoveMonster(GameEntity entity)
        {
            entity.RemoveID();
            entity.RemoveTransform();
            entity.RemoveView();
            entity.RemoveMove();
            entity.RemoveCoord();
            entity.RemoveAnimation();
            entity.RemoveAI();
            entity.RemoveRotate();
        }

        public static void CreateMaster(ulong uuid)
        {
            var game = Contexts.Instance.game;
            if (game.gameMaster.entity != null)
            {
                game.gameMaster.entity.Destroy();
            }
            GameEntity entity = CreatePlayer(Util.GetEntityId());
            game.gameMaster.entity = entity;
        }

        public static void DestroyEntity(GameEntity entity)
        {
            MapComponent mapComp = Contexts.Instance.game.map;
            if (mapComp.burnEntities.Contains(entity))
                Debug.Log("Destroy Burned Entity");
            entity.Destroy();
        }
    }
}
