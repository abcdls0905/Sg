using Entitas;
using UnityEngine;
using Game;
using System;
using GameJson;
using System.Collections.Generic;

namespace Game
{
    public class ItemSkillSystem : IInitializeSystem, IFixedExecuteSystem
    {
        public void Initialize()
        {
            EventManager.Instance.AddEvent<UseItemParam>(GEventType.EVENT_USEITEM, UseItem);
        }

        public void UseItem(ref UseItemParam param)
        {
            switch (param.type)
            {
                case AkItemType.Ak_Fire:
                    {
                        FireItem();
                        break;
                    }
                case AkItemType.Ak_Transform:
                    {
                        TransformItem();
                        break;
                    }
                case AkItemType.Ak_Boom:
                    {
                        BoomItem();
                        break;
                    }
            }
        }

        List<AkBoxColor> list = new List<AkBoxColor>();
        List<AkBoxColor> GetTermsColor()
        {
            list.Clear();
            LevelTermsComponent terms = Contexts.Instance.game.levelTerms;
            var iter = terms.termsDic.GetEnumerator();
            while (iter.MoveNext())
            {
                if (iter.Current.Value > 0)
                    list.Add(iter.Current.Key);
            }
            return list;
        }

        void FireItem()
        {
            List<AkBoxColor> list = GetTermsColor();
            if (list.Count == 0)
                return;
            int index = UnityEngine.Random.Range(0, list.Count);
            AkBoxColor eColor = list[index];

            var mapComp = Contexts.Instance.game.map;
            int mapWidth = BattleManager.Instance.mapSize;
            int mapHeight = BattleManager.Instance.mapSize;
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    GameEntity entity = mapComp.mapData[j, i];
                    if (entity != null && entity.box.eColor == eColor&& entity.burn.eBurnState == AkBurnState.Ak_None)
                        mapComp.AddBurnEntity(entity);
                }
            }
            GameEntity master = Contexts.Instance.game.gameMaster.entity;
            ItemComponent itemComp = master.item;
            Item item = itemComp.items[0];
            item.cd = item.property.cd;
            item.runTime = 0;
            item.isRunCD = true;
        }

        void TransformItem()
        {
            GameEntity master = Contexts.Instance.game.gameMaster.entity;
            ItemComponent itemComp = master.item;
            Item item = itemComp.items[1];
            item.cd = item.property.cd;
            item.runTime = 0;
            item.isRunCD = true;
        }

        void BoomItem()
        {
            GameEntity master = Contexts.Instance.game.gameMaster.entity;
            ItemComponent itemComp = master.item;
            Item item = itemComp.items[2];
            item.cd = item.property.cd;
            item.runTime = 0;
            item.isRunCD = true;
            MapComponent mapComp = Contexts.Instance.game.map;
            for (int i = 0; i < mapComp.monsters.Count; i++)
            {
                GameEntity monster = mapComp.monsters[i];
                GameEntity boxEntity = mapComp.mapData[monster.coord.y, monster.coord.x];
                if (boxEntity != null)
                {
                    boxEntity.burn.burnTime = 100;
                    mapComp.burnEntities.Add(boxEntity);
                }
            }
        }

        public void FixedExecute()
        {
            float frameTime = Contexts.Instance.game.frame.frameTime;
            GameEntity master = Contexts.Instance.game.gameMaster.entity;
            ItemComponent itemComp = master.item;
            for (int i = 0; i < itemComp.items.Count; i++)
            {
                Item item = itemComp.items[i];
                if (item.isRunCD)
                {
                    item.runTime += frameTime;
                    if (item.runTime >= item.cd)
                    {
                        item.isRunCD = false;
                        item.runTime = 0;
                    }
                }
            }
        }
    }
}