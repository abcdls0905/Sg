using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class AttachmentPoint : BaseAttachPoint
    {
        override public void Init()
        {
        }
        override public void Cleanup()
        {
            Unattach();
        }
        override public void Unattach()
        {
            Recycle();
            attachPrefabName = null;
        }
        override public GameObject Attach(string prefabName, Vector3 scale, int layer)
        {
            if (attachPrefabName != prefabName)
            {
                attachPrefabName = prefabName;
                Recycle();
                if (attachPrefabName != null)
                {
                    attachChild = GameObjectPool.Instance.GetGameObject(prefabName, AkResourceType.GameScene);
                    if (attachChild)
                    {
                        Transform ct = attachChild.transform;
                        ct.parent = parent;
                        ct.localPosition = Vector3.zero;
                        ct.localRotation = Quaternion.identity;
                        ct.localScale = scale;
                    }
                }
            }
            return attachChild;
        }
    }
}
