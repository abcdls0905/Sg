using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class PooledGameObjectScript : MonoBehaviour
    {
        public string m_prefabKey;
        public bool m_isInit;
        public Vector3 m_defaultScale;
        private List<PooledMonoBehaviour> m_cachedIPooledMonos = new List<PooledMonoBehaviour>();
        public int m_checkTime;

        public void Initialize(string prefabKey)
        {
            m_cachedIPooledMonos.Clear();
            MonoBehaviour[] componentsInChildren = base.gameObject.GetComponentsInChildren<MonoBehaviour>(true);
            if (componentsInChildren != null)
            {
                for (int j = 0; j < componentsInChildren.Length; j++)
                {
                    if (componentsInChildren[j] is PooledMonoBehaviour)
                        this.m_cachedIPooledMonos.Add(componentsInChildren[j] as PooledMonoBehaviour);
                }
            }

            m_prefabKey = prefabKey;
            m_defaultScale = base.gameObject.transform.localScale;
            m_isInit = true;
            m_checkTime = 0;

            // ´´½¨
            var pool = GameObjectPool.Instance.GetPool(m_prefabKey, false);
            if (pool != null)
                ++pool.m_instantiate;
        }
        public void AddCachedMono(PooledMonoBehaviour mono)
        {
            if (mono != null)
            {
                this.m_cachedIPooledMonos.Add(mono);
            }
        }
        public void OnCreate()
        {
            if (this.m_cachedIPooledMonos != null)
            {
                foreach (PooledMonoBehaviour mono in this.m_cachedIPooledMonos)
                {
                    mono.OnCreate();
                }
            }
        }
        public void OnGet()
        {
            if (!base.gameObject.activeSelf)
            {
                base.gameObject.SetActive(true);
            }
            if (this.m_cachedIPooledMonos != null)
            {
                foreach (PooledMonoBehaviour mono in this.m_cachedIPooledMonos)
                {
                    mono.OnGet();
                }
            }
        }
        public void OnRecycle()
        {
            if (this.m_cachedIPooledMonos != null)
            {
                foreach (PooledMonoBehaviour mono in this.m_cachedIPooledMonos)
                {
                    mono.OnRecycle();
                }
            }
            base.gameObject.SetActive(false);
        }

        public void OnDestroy()
        {
            // Ïú»Ù
            var pool = GameObjectPool.Instance.GetPool(m_prefabKey, false);
            if (pool != null)
                --pool.m_instantiate;
        }
    }
}