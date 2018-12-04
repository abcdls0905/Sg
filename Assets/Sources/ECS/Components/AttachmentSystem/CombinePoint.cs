using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CombinePoint : BaseAttachPoint
    {
        public override void Init()
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
            if (attachPrefabName != prefabName && attachPrefabName != null)
            {
                attachPrefabName = prefabName;
                Recycle();

                attachChild = GameObjectPool.Instance.GetGameObject(prefabName, AkResourceType.GameScene);
                if (attachChild)
                {
                    Transform ct = attachChild.transform;
                    ct.parent = parent;
                    ct.localPosition = Vector3.zero;
                    ct.localRotation = Quaternion.identity;
                    ct.localScale = scale;

                    var smr = attachChild.GetComponentInChildren<SkinnedMeshRenderer>();
                    if (smr)
                    {
                        List<Transform> bones = new List<Transform>();
                        Transform[] smrBones = smr.bones;
                        Transform[] hips = parent.GetComponentsInChildren<Transform>();
                        for (int i = 0; i < smrBones.Length; i++)
                        {
                            for (int j = 0; j < hips.Length; j++)
                            {
                                if (smrBones[i].name == hips[j].name)
                                {
                                    bones.Add(hips[j]);
                                    break;
                                }
                            }
                        }

                        smr.bones = bones.ToArray();
                    }
                }
            }
            return attachChild;
        }
    }
}
