using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public abstract class BaseAttachPoint
    {
        public Transform parent;
        public string attachPrefabName;
        protected GameObject attachChild;
        protected bool _forGameUsage;
        public bool needCache = true;
        public GameObject AttachChild
        {
            get
            {
                return attachChild;
            }
        }
        abstract public void Init();
        abstract public void Cleanup();

        abstract public GameObject Attach(string prefabName, Vector3 scale, int layer);

        abstract public void Unattach();

        protected void Recycle()
        {
            if (attachChild != null)
            {
                attachChild.transform.position = Util.ZeroPosition;
                attachChild.transform.localRotation = Quaternion.identity;
                attachChild.transform.localScale = new Vector3(1, 1, 1);
                if (needCache)
                    GameObjectPool.Instance.RecycleGameObject(attachChild);
                else
                    GameObject.Destroy(attachChild);
                attachChild = null;
            }
        }
    }
}
