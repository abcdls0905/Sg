using UnityEngine;

namespace Game
{
    public abstract class PooledMonoBehaviour : MonoBehaviour
    {
        protected void Awake ()
        {
            PooledGameObjectScript sc = GetComponent<PooledGameObjectScript>();
            if (sc != null)
            {
                sc.AddCachedMono(this);
                OnCreate();
            }
        }
        public abstract void OnCreate();
        public abstract void OnGet();
        public abstract void OnRecycle();
    }
}