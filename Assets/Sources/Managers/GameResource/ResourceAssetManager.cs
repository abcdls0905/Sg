using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using GameUtil;
using System;

namespace Game
{
    public class ResourceInfo : SimplePool<ResourceInfo>
    {
        public string m_Name;

        public UnityEngine.Object m_Asset;
        public int m_ReferencedCount;
        public ResourceRequest m_Request;
        public Action<ResourceInfo> m_LoadFinish;

        public void Unload()
        {
            // 此处卸载会影响磁盘Prefab对象, 需要使用Resources.UnloadUnusedAssets
            //if (m_Asset != null)
            //    GameObject.DestroyImmediate(m_Asset, true);
            m_Asset = null;
            m_Request = null;
            m_ReferencedCount = 0;
            m_LoadFinish = null;

        }

        public void AddRef()
        {
            ++m_ReferencedCount;
        }

        public void DecRef()
        {
            if (--m_ReferencedCount > 0)
                return;
            Unload();
        }

        public bool IsLoaded()
        {
            return m_Asset != null;
        }

        public bool IsLoading()
        {
            return m_Request != null;
        }
    }

    public class ResourceAssetManager : MonoSingleton<ResourceAssetManager>
    {
        Dictionary<string, ResourceInfo> m_Assets = new Dictionary<string, ResourceInfo>();
        List<ResourceInfo> m_LoadingAssets = new List<ResourceInfo>();

        public override void Init()
        {
        }

        public void Update()
        {
            for (int i = m_LoadingAssets.Count - 1; i >= 0; --i)
            {
                ResourceInfo info = m_LoadingAssets[i];
                if (info.m_Request == null) // 等待加载Bundle
                    continue;
                if (!info.m_Request.isDone) // 等待加载Asset
                    continue;
                m_LoadingAssets.RemoveAt(i);
                info.m_Asset = info.m_Request.asset;
                info.m_Request = null;
            }
        }

        public ResourceInfo LoadAssetAsync(string assetName, string assetBundleName, Action<ResourceInfo> onFinish = null)
        {
            ResourceInfo info = GetAsset(assetName);
            if (info == null)
            {
                info = ResourceInfo.New();
                info.m_Name = assetName;

                m_Assets.Add(assetName, info);
            }
            info.m_LoadFinish = onFinish;
            m_LoadingAssets.Add(info);

            if (info.IsLoaded() || info.IsLoading())
                return info;
            info.m_Request = Resources.LoadAsync(assetBundleName);
            info.m_ReferencedCount = 0;
            return info;
        }

        public ResourceInfo GetAsset(string assetName)
        {
            ResourceInfo info;
            m_Assets.TryGetValue(assetName, out info);
            return info;
        }

        public void UnloadAsset(string assetName)
        {
            ResourceInfo info = GetAsset(assetName);
            if (info == null)
                return;
            info.DecRef();
        }

        public int GetLoadingNum()
        {
            return m_LoadingAssets.Count;
        }
    }
}