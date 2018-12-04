using System;
using System.Collections.Generic;
using UnityEngine;
using GameUtil;

namespace Game
{
    public enum AkResourceType
    {
        GameScene,
        GameData,
        GameUI,
        GameAudio,
    }

    public enum ResType
    {
        RT_Mat,
        RT_Prefab,
        RT_Texture,
        RT_Audio,
        RT_Max,
    }

    public class ResourceManager : Singleton<ResourceManager>
    {
        public delegate void OnResourceLoaded(Resource resource);

        private Dictionary<string, Resource> m_cachedResourceMap = new Dictionary<string, Resource>();
        private List<Resource> m_loadingResources = new List<Resource>();

        private bool m_clearUnusedAssets;
        private int m_clearUnusedAssetsExecuteFrame;
        private static int s_frameCounter;
        public static bool isPreLoad;
        public Action<AssetBundleInfo> m_BundleLoadFinish;
        public string[][] extensions;

        public override void Init()
        {
            m_BundleLoadFinish = AssetBundleLoadFinish;

            extensions = new string[(int)ResType.RT_Max][];
            extensions[(int)ResType.RT_Mat] = new string[] { ".mat" };
            extensions[(int)ResType.RT_Prefab] = new string[] { ".prefab" };
            extensions[(int)ResType.RT_Texture] = new string[] { ".png" };
            extensions[(int)ResType.RT_Audio] = new string[] { ".mp3", ".wav" };
        }

        public string[] GetResourceExtension(Type type)
        {
            string[] extensions = null;
            if (type == typeof(Material))
                extensions = this.extensions[(int)ResType.RT_Mat];
            else if (type == typeof(GameObject))
                extensions = this.extensions[(int)ResType.RT_Prefab];
            else if (type == typeof(Texture2D))
                extensions = this.extensions[(int)ResType.RT_Texture];
            else if (type == typeof(AudioClip))
                extensions = this.extensions[(int)ResType.RT_Audio];
            return extensions;
        }

        public void Update()
        {
            s_frameCounter++;
            if (m_clearUnusedAssets && m_clearUnusedAssetsExecuteFrame == s_frameCounter)
            {
                ExecuteUnloadUnusedAssets();
                m_clearUnusedAssets = false;
            }

            for (int i = m_loadingResources.Count - 1; i >= 0; --i)
            {
                Resource res = m_loadingResources[i];
                bool ret = UpdateResource(res);
                if (ret)
                {
                    m_loadingResources.RemoveAt(i);
                }
            }
        }
        public bool UpdateResource(Resource res)
        {
            switch (res.m_state)
            {
                case enResourceState.Loaded:
                    return true;
                case enResourceState.WaitAssetBundle:
                    return false;
                case enResourceState.AssetBundleLoading:
                    if (!res.CheckLoaded())
                        return false;
                    res.LoadFromAssetBundleFinish();
                    return true;
                case enResourceState.ResourceLoading:
                    if (!res.CheckLoaded())
                        return false;
                    res.LoadFromResourceFinish();
                    return true;
                case enResourceState.Cancel:
                    if (!res.CheckLoaded())
                        return false;
                    return true;
                default:
                    return true;
            }
        }
        public Dictionary<string, Resource> GetCachedResourceMap()
        {
            return m_cachedResourceMap;
        }
        public List<Resource> GetLoadingResourceList()
        {
            return m_loadingResources;
        }
        public Resource GetResource(string fullPathInResources, Type resourceContentType, AkResourceType resourceType)
        {
            if (string.IsNullOrEmpty(fullPathInResources))
            {
                return new Resource(string.Empty, string.Empty, null, resourceType);
            }
            string prefabKey = GameInterface.GetPrefabKey(fullPathInResources);
            Resource resource = null;
            if (m_cachedResourceMap.TryGetValue(prefabKey, out resource))
            {
                if (resource.m_resourceType != resourceType)
                    resource.m_resourceType = resourceType;
                return resource;
            }
            resource = new Resource(prefabKey, fullPathInResources, resourceContentType, resourceType);
            m_cachedResourceMap.Add(prefabKey, resource);
            //Debug.Log("load resource:" + resource.m_name);
            //Debug.Log("New Resource: " + fullPathInResources);
            Load(resource);
            return resource;
        }

        public Resource GetResourceAsync(string fullPathInResources, Type resourceContentType, AkResourceType resourceType, Action<Resource> onFinish)
        {
            if (string.IsNullOrEmpty(fullPathInResources))
            {
                return new Resource(string.Empty, string.Empty, null, resourceType);
            }
            string prefabKey = GameInterface.GetPrefabKey(fullPathInResources);
            Resource resource = null;
            if (m_cachedResourceMap.TryGetValue(prefabKey, out resource))
            {
                if (resource.IsLoading())
                    resource.m_loadFinish += onFinish;
                else
                    onFinish(resource);
                return resource;
            }
            resource = new Resource(prefabKey, fullPathInResources, resourceContentType, resourceType);
            m_cachedResourceMap.Add(prefabKey, resource);
            LoadAsync(resource, onFinish);
            return resource;
        }

        public void RemoveResource(string fullPathInResources)
        {
            string prefabKey = GameInterface.GetPrefabKey(fullPathInResources);
            Resource resource = null;
            if (m_cachedResourceMap.TryGetValue(prefabKey, out resource))
            {
                if (resource.IsUnloaded())
                    return;
                if (GameEnvSetting.EnableAssetBundle)
                    AssetBundleManager.Instance.UnloadAssetBundle(resource.m_key, true);
                resource.Unload();
                m_cachedResourceMap.Remove(prefabKey);
            }
        }
        public void RemoveResources(AkResourceType resourceType, bool clearImmediately = true)
        {
            List<string> list = new List<string>();
            Dictionary<string, Resource>.Enumerator enumerator = m_cachedResourceMap.GetEnumerator();
            while (enumerator.MoveNext())
            {
                KeyValuePair<string, Resource> current = enumerator.Current;
                Resource value = current.Value;
                if (value.m_resourceType == resourceType)
                {
                    if (value.IsUnloaded())
                        continue;
                    if (GameEnvSetting.EnableAssetBundle)
                        AssetBundleManager.Instance.UnloadAssetBundle(value.m_key, true);
                    value.Unload();
                    list.Add(value.m_key);
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                m_cachedResourceMap.Remove(list[i]);
            }
            if (clearImmediately)
            {
                UnloadUnusedAssets();
            }
        }
        public void RemoveResources(AkResourceType[] resourceTypes)
        {
            for (int i = 0; i < resourceTypes.Length; i++)
            {
                RemoveResources(resourceTypes[i], false);
            }
            UnloadUnusedAssets();
        }
        public void RemoveAllCachedResources()
        {
            RemoveResources((AkResourceType[])Enum.GetValues(typeof(AkResourceType)));
        }
        public void UnloadUnusedAssets()
        {
            m_clearUnusedAssets = true;
            m_clearUnusedAssetsExecuteFrame = s_frameCounter + 1;
        }
        private void ExecuteUnloadUnusedAssets()
        {
            Resources.UnloadUnusedAssets();
            GC.Collect();
        }

        public Type GetResourceContentType(string extension)
        {
            Type result = null;
            if (string.Equals(extension, ".prefab", StringComparison.OrdinalIgnoreCase))
            {
                result = typeof(GameObject);
            }
            else if(string.Equals(extension, ".bytes", StringComparison.OrdinalIgnoreCase) || string.Equals(extension, ".xml", StringComparison.OrdinalIgnoreCase))
            {
                result = typeof(TextAsset);
            }
            else if(string.Equals(extension, ".asset", StringComparison.OrdinalIgnoreCase))
            {
                result = typeof(ScriptableObject);
            }
            return result;
        }

        public void Load(Resource resource)
        {
            if (GameEnvSetting.EnableAssetBundle)
                resource.LoadFromAssetBundle();
            else
                resource.LoadFromResource();
        }

        public void LoadAsync(Resource resource, Action<Resource> onFinish)
        {
            if (GameEnvSetting.EnableAssetBundle)
            {
                resource.LoadFromAssetBundleAsync(onFinish);
                AssetBundleManager.Instance.LoadAssetBundleAsync(resource.m_key, m_BundleLoadFinish);
            }
            else
            {
                resource.LoadFromResource();
                onFinish(resource);
                //resource.LoadFromResourceAsync(onFinish);
                m_loadingResources.Add(resource);
            }
        }

        public void AssetBundleLoadFinish(AssetBundleInfo bundleInfo)
        {
            Resource res;
            if (!m_cachedResourceMap.TryGetValue(bundleInfo.m_Name, out res))
                return;
            if (bundleInfo.m_AssetBundle == null)
                return;
            if (res.m_state != enResourceState.WaitAssetBundle)
                return;
            res.AssetBundleLoadFinish(bundleInfo.m_AssetBundle);
            m_loadingResources.Add(res);
        }
    }
}