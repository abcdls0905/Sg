using System.Collections.Generic;
using UnityEngine;
using GameUtil;
using System;

namespace Game
{
    public class GameObjectRequest
    {
        bool first;
        PooledGameObjectScript _script;
        public GameObject gameObject
        { // 第一次使用的时候初始化
            get
            {
                if (first)
                {
                    _script.OnGet();
                    first = false;
                }
                return _script.gameObject;
            }
        }
        public void Finish(PooledGameObjectScript script, bool _first = true)
        {
            _script = script;
            first = _first;
            isDone = true;
        }
        public bool isDone = false;
    }

    public class GameObjectQueue
    {
        public List<PooledGameObjectScript> m_pooled = new List<PooledGameObjectScript>();
        public bool m_needDel;
        int _instantiate = 0;
        public int m_instantiate
        {
            get
            {
                return _instantiate;
            }
            set
            {
                _instantiate = value;
                if (_instantiate <= 0)
                    m_needDel = true;
                else
                    m_needDel = false;
            }
        }

        public void PushCache(PooledGameObjectScript script)
        {
            m_pooled.Insert(0, script);
        }

        public PooledGameObjectScript PopCache()
        {
            PooledGameObjectScript script = null;
            while (m_pooled.Count > 0)
            {
                script = m_pooled[m_pooled.Count - 1];
                m_pooled.RemoveAt(m_pooled.Count - 1);
                if (script != null && script.gameObject != null)
                {
                    script.gameObject.transform.SetParent(null, true);
                    script.gameObject.transform.localScale = script.m_defaultScale;
                    break;
                }
                script = null;
            }
            return script;
        }
    }

    public class GameObjectPool : Singleton<GameObjectPool>
    {
        public class stDelayRecycle
        {
            public GameObject recycleObj;
            public int timeMillSecondsLeft;
            public OnDelayRecycleDelegate callback;
        }
        public delegate void OnDelayRecycleDelegate(GameObject recycleObj);
        private Dictionary<string, GameObjectQueue> m_pooledGameObjectMap = new Dictionary<string, GameObjectQueue>();
        private LinkedList<stDelayRecycle> m_delayRecycle = new LinkedList<stDelayRecycle>();

        private bool m_clearPooledObjects;
        //private float m_checkPooledTime;

        private const int m_checkPooledInterval = 5; // 5秒检测一次
        public const int m_destroyPooledInterval = 30;// 30秒未使用则删除
        public GameObject m_root;

        public Dictionary<string, GameObjectQueue> GetPooledGameObjectMap()
        {
            return m_pooledGameObjectMap;
        }
        public LinkedList<stDelayRecycle> GetDelayRecycle()
        {
            return m_delayRecycle;
        }

        public override void Init()
        {
            m_clearPooledObjects = false;
            //m_checkPooledTime = 0;
            m_root = new GameObject("GameObjectPool");
            GameObject.DontDestroyOnLoad(m_root);
        }
        public override void UnInit()
        {
            GameObject.Destroy(m_root);
        }
        public void Update()
        {
            UpdateDelayRecycle();
            UpdatePooledGameObject();
            UpdateClearPooledObjects();
        }
        public void ClearPooledObjects()
        {
            m_clearPooledObjects = true;
        }
        public void UpdateClearPooledObjects()
        {
            if (!m_clearPooledObjects)
                return;
            m_clearPooledObjects = false;

            for (LinkedListNode<stDelayRecycle> linkedListNode = m_delayRecycle.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
            {
                if (null != linkedListNode.Value.recycleObj)
                {
                    RecycleGameObject(linkedListNode.Value.recycleObj);
                }
            }
            m_delayRecycle.Clear();
            var iter = m_pooledGameObjectMap.GetEnumerator();
            while (iter.MoveNext())
            {
                var pool = iter.Current.Value;
                var queue = pool.m_pooled;
                for (int i = 0; i < queue.Count; ++i)
                {
                    var script = queue[i];
                    if (script != null && script.gameObject != null)
                        UnityEngine.Object.Destroy(script.gameObject);
                }
                queue.Clear();
                ResourceManager.Instance.RemoveResource(iter.Current.Key);
            }
            m_pooledGameObjectMap.Clear();
        }
        private void UpdateDelayRecycle()
        {
            int num = (int)(1000f * Time.deltaTime);
            LinkedListNode<stDelayRecycle> linkedListNode = m_delayRecycle.First;
            while (linkedListNode != null)
            {
                LinkedListNode<stDelayRecycle> linkedListNode2 = linkedListNode;
                linkedListNode = linkedListNode2.Next;
                if (null == linkedListNode2.Value.recycleObj)
                {
                    m_delayRecycle.Remove(linkedListNode2);
                }
                else
                {
                    linkedListNode2.Value.timeMillSecondsLeft -= num;
                    if (linkedListNode2.Value.timeMillSecondsLeft <= 0)
                    {
                        if (linkedListNode2.Value.callback != null)
                        {
                            linkedListNode2.Value.callback(linkedListNode2.Value.recycleObj);
                        }
                        RecycleGameObject(linkedListNode2.Value.recycleObj);
                        m_delayRecycle.Remove(linkedListNode2);
                    }
                }
            }
        }

        //先屏蔽定时删除缓存策略
        private void UpdatePooledGameObject()
        {
//             m_checkPooledTime -= Time.unscaledDeltaTime;
//             if (m_checkPooledTime > 0)
//                 return;
//             m_checkPooledTime = m_checkPooledInterval;
// 
//             var iter = m_pooledGameObjectMap.GetEnumerator();
//             while (iter.MoveNext())
//             {
//                 var pool = iter.Current.Value;
//                 var queue = pool.m_pooled;
//                 for (int i = queue.Count - 1; i >= 0; --i)
//                 {
//                     var script = queue[i];
//                     script.m_checkTime -= m_checkPooledInterval;
//                     if (script.m_checkTime <= 0) // 需要删除
//                     {
//                         UnityEngine.Object.Destroy(script.gameObject);
//                         queue.RemoveAt(i);
//                     }
//                 }
//                 if (pool.m_needDel && pool.m_instantiate <= 0)
//                 {
//                     pool.m_needDel = false;
//                     ResourceManager.Instance.RemoveResource(iter.Current.Key);
//                 }
//             }
        }

        public GameObjectQueue GetPool(string text, bool makeNew = true)
        {
            GameObjectQueue queue = null;
            if (!m_pooledGameObjectMap.TryGetValue(text, out queue))
            {
                if (makeNew)
                {
                    queue = new GameObjectQueue();
                    m_pooledGameObjectMap.Add(text, queue);
                }
            }
            return queue;
        }

        public GameObject GetGameObject(string prefabFullPath, AkResourceType resourceType)
        {
            try
            {
                string prefabKey = GameInterface.GetPrefabKey(prefabFullPath);
                var script = GetPool(prefabKey).PopCache();
                if (script == null)
                    script = CreateGameObject(prefabFullPath, prefabKey, resourceType);
                if (script == null)
                    return null;
                script.OnGet();
                return script.gameObject;
            }
            catch
            {
                throw new System.Exception("Can't get prefab from " + prefabFullPath);
            }
        }

        public GameObjectRequest GetGameObjectAsync(string prefabFullPath, AkResourceType resourceType)
        {
            string prefabKey = GameInterface.GetPrefabKey(prefabFullPath);
            var request = new GameObjectRequest();
            var script = GetPool(prefabKey).PopCache();
            if (script != null)
            {
                request.Finish(script);
            }
            else
            {
                ResourceManager.Instance.GetResourceAsync(prefabFullPath, typeof(GameObject), resourceType, (Resource resource) =>
                {
                    var script2 = InstantiateGameObject(resource.m_key, resource.m_content as GameObject);
                    request.Finish(script2);
                });
            }
            return request;
        }

        public void GetGameObjectAsync(string prefabFullPath, AkResourceType resourceType, Action<GameObject> onFinish)
        {
            string prefabKey = GameInterface.GetPrefabKey(prefabFullPath);
            var script = GetPool(prefabKey).PopCache();
            if (script != null)
            {
                onFinish(script.gameObject);
            }
            else
            {
                ResourceManager.Instance.GetResourceAsync(prefabFullPath, typeof(GameObject), resourceType, (Resource resource) =>
                {
                    var gameObject = InstantiateGameObject(resource.m_key, resource.m_content as GameObject).gameObject;
                    onFinish(gameObject);
                });
            }
        }

        public void PreperGameObject(string prefabFullPath, AkResourceType resourceType)
        {
            string prefabKey = FileManager.EraseExtension(prefabFullPath).ToLower();
            prefabFullPath = FileManager.AddExtension(prefabFullPath, ".prefab");

            GameObjectQueue queue = null;
            if (!m_pooledGameObjectMap.TryGetValue(prefabKey, out queue))
            {
                queue = new GameObjectQueue();
                m_pooledGameObjectMap.Add(prefabKey, queue);
            }
            PooledGameObjectScript PooledGameObjectScript = CreateGameObject(prefabFullPath, prefabKey, resourceType);
            if (PooledGameObjectScript == null || PooledGameObjectScript.gameObject == null)
                return;
            else
            {
                PooledGameObjectScript.gameObject.transform.SetParent(null, true);
                PooledGameObjectScript.gameObject.transform.position = new Vector3(10, -10, -10);
                PooledGameObjectScript.gameObject.transform.rotation = Quaternion.identity;
                PooledGameObjectScript.gameObject.transform.localScale = PooledGameObjectScript.m_defaultScale;
            }
            _RecycleGameObject(PooledGameObjectScript.gameObject, PooledGameObjectScript.m_isInit);
        }
        public void RecycleGameObjectDelay(GameObject pooledGameObject, int delayMillSeconds, OnDelayRecycleDelegate callback = null)
        {
            stDelayRecycle stDelayRecycle = new stDelayRecycle();
            stDelayRecycle.recycleObj = pooledGameObject;
            stDelayRecycle.timeMillSecondsLeft = delayMillSeconds;
            stDelayRecycle.callback = callback;
            m_delayRecycle.AddLast(stDelayRecycle);
        }

        public void RecycleGameObject(GameObject pooledGameObject)
        {
            if (pooledGameObject == null)
                return;
            _RecycleGameObject(pooledGameObject, false);
        }

        public void DestroyGameObject(GameObject pooledGameObject)
        {

        }

        private void _RecycleGameObject(GameObject pooledGameObject, bool setIsInit)
        {
            if (pooledGameObject == null)
            {
                return;
            }
            PooledGameObjectScript component = pooledGameObject.GetComponent<PooledGameObjectScript>();
            if (component != null)
            {
                GameObjectQueue queue = null;
                if (m_pooledGameObjectMap.TryGetValue(component.m_prefabKey, out queue))
                {
                    component.OnRecycle();
                    component.gameObject.transform.SetParent(m_root.transform, true);
                    component.m_isInit = setIsInit;
                    component.m_checkTime = m_destroyPooledInterval;
                    queue.m_pooled.Add(component);
                    return;
                }
            }
            UnityEngine.Object.Destroy(pooledGameObject);
        }

        private PooledGameObjectScript CreateGameObject(string prefabFullPath, string prefabKey, AkResourceType resourceType)
        {
            GameObject gameObject = ResourceManager.Instance.GetResource(prefabFullPath, typeof(GameObject), resourceType).m_content as GameObject;
            if (gameObject == null)
                return null;
            return InstantiateGameObject(prefabKey, gameObject);
        }

        private PooledGameObjectScript InstantiateGameObject(string prefabKey, GameObject gameObject)
        {
            if (gameObject == null)
            {
                Debug.Log("instantiate gamobjetct is null " + prefabKey);
                return null;
            }   
            GameObject gameObject2 = (UnityEngine.Object.Instantiate(gameObject) as GameObject);
            Debug.Assert(gameObject2 != null);
            GameObject.DontDestroyOnLoad(gameObject2);
            PooledGameObjectScript script = gameObject2.GetComponent<PooledGameObjectScript>();
            if (script == null)
            {
                script = gameObject2.AddComponent<PooledGameObjectScript>();
            }
            script.Initialize(prefabKey);
            script.OnCreate();
            return script;
        }
    }
}