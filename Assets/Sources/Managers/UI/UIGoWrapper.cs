using FairyGUI;
using UnityEngine;

namespace Game
{
    [XLua.LuaCallCSharp]
    public class UIGoWrapper : GoWrapper
    {
        GGraph _holder;

        public float delayPlay { set; get; }
        public float delayStop { set; get; }
        public UIGoWrapper(GGraph holder)
        {
            _holder = holder;
        }

        public GameObject LoadModel(string model)
        {
            this.UnloadModel();
            _holder.SetNativeObject(this);
            GameObject go = GameObjectPool.Instance.GetGameObject(model, AkResourceType.GameUI);

            this.wrapTarget = go;
            this.delayStop = 0;
            this.delayPlay = 0;

            go.transform.localPosition = Vector3.zero;
            //go.transform.localRotation = Quaternion.identity;
            //go.transform.localScale = new Vector3(this._holder.width, this._holder.height, this._holder.height);
            //go.transform.localPosition = new Vector3(this._holder.width / 2, -this._holder.height / 2, 0);
            return go;
        }

        public void UnloadModel(int modelTime = 0)
        {
            _holder.SetNativeObject(null);
            // 回收GameObject
            if (wrapTarget != null)
            {
                if (modelTime <= 0)
                    GameObjectPool.Instance.RecycleGameObject(this.wrapTarget);
                else
                    GameObjectPool.Instance.RecycleGameObjectDelay(this.wrapTarget, modelTime);
                this.wrapTarget = null;
            }
        }

        public void SetActive(bool active)
        {
            this.wrapTarget.SetActive(active);
        }

        public bool IsActive()
        {
            return wrapTarget.activeSelf;
        }

        public void SetPos(float x, float y)
        {
            this.wrapTarget.transform.position = new Vector3(x, y, 0);
        }
        public void DoBling()
        {
            SetActive(false);
            SetActive(true);
        }
        public void Update(float deltaTime)
        {
            if (this.delayStop > 0)
            {
                this.delayStop -= deltaTime;
                if (this.delayStop <= 0)
                {
                    SetActive(false);
                }
            }

            if (this.delayPlay > 0)
            {
                this.delayPlay -= deltaTime;
                if (this.delayPlay <= 0)
                {
                    DoBling();
                }
            }
        }

    }


}
