using System;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class CBinaryObject : ScriptableObject
    {
        public byte[] m_data;
    }

    public enum enResourceState
    {
        Unload = 0,
        ResourceLoading,
        AssetBundleLoading,
        WaitAssetBundle,
        Cancel,
        Loaded
    }

    public class Resource
    {
        public enResourceState m_state;
        public string m_key;
        public string m_name;
        public string m_fullPathInResources;
        public AkResourceType m_resourceType;
        public Type m_contentType;
        public UnityEngine.Object m_content;
        public bool m_unloadBundle;
        public Action<Resource> m_loadFinish;

        public AssetBundleRequest m_bundleRequest;
        public ResourceRequest m_resRequest;
        public Resource(string key, string fullPathInResources, Type contentType, AkResourceType resourceType)
        {
            m_state = enResourceState.Unload;
            m_key = key;
            m_fullPathInResources = fullPathInResources;
            m_name = FileManager.GetFullName(fullPathInResources);
            m_resourceType = resourceType;
            m_contentType = contentType;
            m_content = null;
            m_unloadBundle = false;
            m_loadFinish = null;
        }

        public void LoadFromResource()
        {
            string[] extensions = ResourceManager.Instance.GetResourceExtension(m_contentType);
            if (extensions != null)
            {
                string path = FileManager.EraseExtension(m_fullPathInResources);
                foreach(string extension in extensions)
                {
                    m_content = Resource.LoadResource(path, extension, m_contentType);
                    if (m_content != null)
                    {
                        if (m_content.GetType() == typeof(TextAsset))
                        {
                            CBinaryObject cBinaryObject = ScriptableObject.CreateInstance<CBinaryObject>();
                            cBinaryObject.m_data = (m_content as TextAsset).bytes;
                            m_content = cBinaryObject;
                        }
                        break;
                    } 
                } 
            }
            m_state = enResourceState.Loaded;
        }

        public static UnityEngine.Object LoadResource(string path, string extension, Type type)
        {
            UnityEngine.Object resource = null;
#if UNITY_EDITOR
            string fullpath = "Assets/ArtRes/Resources_/" + path + extension;
            resource = UnityEditor.AssetDatabase.LoadAssetAtPath(fullpath, type);
#endif
            return resource;
        } 

        public void LoadFromResourceAsync(Action<Resource> finish)
        {
            if (m_contentType == null)
                m_resRequest = Resources.LoadAsync(FileManager.EraseExtension(m_fullPathInResources));
            else
                m_resRequest = Resources.LoadAsync(FileManager.EraseExtension(m_fullPathInResources), m_contentType);
            m_loadFinish += finish;
            m_state = enResourceState.ResourceLoading;
        }

        public void LoadFromResourceFinish()
        {
            m_content = m_resRequest.asset;
            m_resRequest = null;
            if (m_content != null && m_content.GetType() == typeof(TextAsset))
            {
                CBinaryObject cBinaryObject = ScriptableObject.CreateInstance<CBinaryObject>();
                cBinaryObject.m_data = (m_content as TextAsset).bytes;
                m_content = cBinaryObject;
            }
            m_state = enResourceState.Loaded;
            if (m_loadFinish != null)
                m_loadFinish(this);
        }

        public void LoadFromAssetBundle()
        {
            AssetBundleInfo bundleInfo = AssetBundleManager.Instance.LoadAssetBundle(m_key);
            if (bundleInfo == null || bundleInfo.m_AssetBundle == null)
            {
                Debug.LogWarning("LoadBundle Failed " + m_key);
                return;
            }
            if (m_contentType == null)
                m_content = bundleInfo.m_AssetBundle.LoadAsset(m_name);
            else
                m_content = bundleInfo.m_AssetBundle.LoadAsset(m_name, m_contentType);
            if (m_content != null && m_content.GetType() == typeof(TextAsset))
            {
                CBinaryObject cBinaryObject = ScriptableObject.CreateInstance<CBinaryObject>();
                cBinaryObject.m_data = (m_content as TextAsset).bytes;
                m_content = cBinaryObject;
            }
            if (m_unloadBundle)
                AssetBundleManager.Instance.UnloadAssetBundle(m_key, false);
            m_state = enResourceState.Loaded;
        }

        public void LoadFromAssetBundleAsync(Action<Resource> finish)
        {
            m_loadFinish += finish;
            m_state = enResourceState.WaitAssetBundle;
        }

        public void AssetBundleLoadFinish(AssetBundle assetBundle)
        {
            if (m_contentType == null)
                m_bundleRequest = assetBundle.LoadAssetAsync(m_name);
            else
                m_bundleRequest = assetBundle.LoadAssetAsync(m_name, m_contentType);
            m_state = enResourceState.AssetBundleLoading;
        }

        public void LoadFromAssetBundleFinish()
        {
            m_content = m_bundleRequest.asset;
            m_bundleRequest = null;
            if (m_unloadBundle)
                AssetBundleManager.Instance.UnloadAssetBundle(m_key, false);
            if (m_content != null && m_content.GetType() == typeof(TextAsset))
            {
                CBinaryObject cBinaryObject = ScriptableObject.CreateInstance<CBinaryObject>();
                cBinaryObject.m_data = (m_content as TextAsset).bytes;
                m_content = cBinaryObject;
            }
            m_state = enResourceState.Loaded;
            if (m_loadFinish != null)
                m_loadFinish(this);
        }

        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public void Unload()
        {
            m_state = enResourceState.Unload;
            m_content = null;
            m_unloadBundle = false;
            m_loadFinish = null;
        }

        public bool IsLoading()
        {
            return m_state == enResourceState.ResourceLoading || m_state == enResourceState.AssetBundleLoading || m_state == enResourceState.WaitAssetBundle;
        }

        public bool IsUnloaded()
        {
            return m_state == enResourceState.Unload || m_state == enResourceState.Cancel;
        }
        public bool CheckLoaded()
        {
            if (m_bundleRequest != null)
                return m_bundleRequest.isDone;
            else if (m_resRequest != null)
                return m_resRequest.isDone;
            else
                return false;
        }
    }
}