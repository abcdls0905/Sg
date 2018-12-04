using System;
using UnityEngine;

namespace GameUtil
{
    public class MonoSingleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                return CreateInstance();
            }
        }
        public static T CreateInstance()
        {
            if (_instance == null)
            {
                Type typeFromHandle = typeof(T);
                _instance = (T)(FindObjectOfType(typeFromHandle));
                if (_instance == null)
                {
                    GameObject gameObject = new GameObject(typeof(T).Name);
                    _instance = gameObject.AddComponent<T>();
                }
            }
            return _instance;
        }
        public static bool HasInstance()
        {
            return _instance != null;
        }
        public static void DestroyInstance()
        {
            if (_instance != null)
            {
                (_instance as MonoSingleton<T>).UnInit();
                Destroy(_instance.gameObject);
            }
            _instance = null;
        }
        protected virtual void Awake()
        {
            if (_instance != null && MonoSingleton<T>._instance.gameObject != base.gameObject)
            {
                if (Application.isPlaying)
                {
                    Destroy(base.gameObject);
                }
                else
                {
                    DestroyImmediate(base.gameObject);
                }
            }
            else
            {
                if (_instance == null)
                {
                    _instance = base.GetComponent<T>();
                }
            }
            DontDestroyOnLoad(gameObject);
            this.Init();
        }
        protected virtual void OnDestroy()
        {
            if (_instance != null && _instance.gameObject == gameObject)
            {
                _instance = null;
            }
        }
        public virtual void Init()
        {
        }

        public virtual void UnInit()
        {
        }
    }
}