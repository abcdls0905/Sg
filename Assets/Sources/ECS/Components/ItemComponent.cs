using Entitas;
using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    public enum AkItemType
    {
        Ak_Fire,
        Ak_Transform,
        Ak_Boom,
        Ak_Max,
    }

    public class ItemProperty
    {
        public AkItemType type;
        public int value;
        public float cd;
    }
    public class Item
    {
        public ItemProperty property;
        public float cd;
        public float runTime;
        public bool isRunCD;
        public Item()
        {
            property = new ItemProperty();
        }
    }

    [Game]
    public class ItemComponent : IComponent
    {
        public List<Item> items = new List<Item>();
        public void Reset()
        {
            float cd = 60;
            items.Clear();
            Item item1 = new Item();
            item1.property.cd = cd;
            item1.isRunCD = false;
            item1.cd = 0;
            item1.runTime = 0;
            item1.property.type = AkItemType.Ak_Fire;
            items.Add(item1);
            Item item2 = new Item();
            item2.property.cd = cd;
            item2.isRunCD = false;
            item2.cd = 0;
            item2.runTime = 0;
            item2.property.type = AkItemType.Ak_Transform;
            items.Add(item2);
            Item item3 = new Item();
            item3.property.cd = cd;
            item3.isRunCD = false;
            item3.cd = 0;
            item3.runTime = 0;
            item3.property.type = AkItemType.Ak_Boom;
            items.Add(item3);
        }
    }
}