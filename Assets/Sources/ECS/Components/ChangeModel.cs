using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public enum ModelLayer
    {
        Show = 0,
        Hide = 8,
    }
    public class ChangeModel : PooledMonoBehaviour
    {
        public GameObject mainModel;
        public AttachmentPoint mainAttach;
        protected new void Awake()
        {
            base.Awake();
            mainAttach = new AttachmentPoint();
            mainAttach.parent = transform;
            mainAttach.Init();
        }

        public override void OnCreate()
        {
        }
        public override void OnRecycle()
        {
            UnloadMainModel();
        }

        public override void OnGet()
        {

        }

        public void LoadMainModel(string prefabName, int defaultLayer = 0)
        {
            mainModel = mainAttach.Attach(prefabName, Vector3.one, defaultLayer);
        }

        public void UnloadMainModel()
        {
            if (!mainModel)
                return;
            mainAttach.Unattach();
            mainModel = null;
        }

        public void UnloadAll()
        {
        }
    }
}
