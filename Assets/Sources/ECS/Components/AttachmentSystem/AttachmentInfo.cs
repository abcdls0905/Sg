using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class AttachmentInfo : PooledMonoBehaviour
    {
        struct AttachSet
        {
            public int slot;
            public int slotType;
        }

        bool hasSetup;
        public Dictionary<int, BaseAttachPoint> attachments = new Dictionary<int, BaseAttachPoint>();
        static Dictionary<int, Dictionary<string, AttachSet>> attachCategories = new Dictionary<int, Dictionary<string, AttachSet>>();

        public override void OnCreate()
        {

        }
        public override void OnRecycle()
        {
            hasSetup = false;
            foreach (var item in attachments)
            {
                item.Value.Cleanup();
            }
            attachments.Clear();
        }
        public override void OnGet()
        {

        }

        public BaseAttachPoint GetAttachmentPoint(int slot)
        {
            BaseAttachPoint ap;
            if (attachments.TryGetValue(slot, out ap))
                return ap;
            return null;
        }

        public static void AddCategory(int categoryIndex, int slot, string attachName, int slotType)
        {
            Dictionary<string, AttachSet> attachCategory = null;
            if (!attachCategories.TryGetValue(categoryIndex, out attachCategory))
            {
                attachCategories.Add(categoryIndex, new Dictionary<string, AttachSet>());
                attachCategory = attachCategories[categoryIndex];
            }
            AttachSet set = new AttachSet();
            set.slot = slot;
            set.slotType = slotType;
            attachCategory.Add(attachName, set);
        }

        public static void ClearCategory()
        {
            attachCategories.Clear();
        }

        public void Setup(int categoryIndex, Transform mainTransform)
        {
            if (hasSetup)
            {
                //foreach(var item in attachments)
                //{
                //    item.Value.Init();
                //}
            }
            else
            {
                hasSetup = true;

                Dictionary<string, AttachSet> attachCategory = null;
                if (!attachCategories.TryGetValue(categoryIndex, out attachCategory))
                {
                    return;
                }
                foreach (var item in attachCategory)
                {
                    if (item.Value.slotType == 2)
                    {
                        Transform childTrans = mainTransform.Find(item.Key);
                        if (childTrans != null)
                        {
                            AttachmentPoint attachment = new AttachmentPoint();
                            attachment.parent = childTrans;
                            attachment.Init();
                            attachments.Add(item.Value.slot, attachment);
                        }
                    }
                }
            }
        }
    }
}
