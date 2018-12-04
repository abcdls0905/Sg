using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using GameUtil;
using System;

namespace Game
{
    public class AssetInfo : SimplePool<AssetInfo>
    {
        public string m_Name;
        public AssetBundleInfo m_AssetBundle;

        public UnityEngine.Object m_Asset;
        public int m_ReferencedCount;
        public AssetBundleRequest m_Request;
        public Action<AssetInfo> m_LoadFinish;

        public void Unload()
        {
            if(m_Asset != null)
                GameObject.DestroyImmediate(m_Asset, true);
            m_Asset = null;
            m_Request = null;
            m_ReferencedCount = 0;
            m_LoadFinish = null;

            m_AssetBundle.DecRef(true);
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
            if (m_AssetBundle != null && m_AssetBundle.IsLoading())
                return true;
            else
                return m_Request != null;
        }
    }

    public class AssetManager : MonoSingleton<AssetManager>
    {
        Dictionary<string, AssetInfo> m_Assets = new Dictionary<string, AssetInfo>();
        Dictionary<string, string> m_BundleNameMap = new Dictionary<string, string>();
        List<AssetInfo> m_LoadingAssets = new List<AssetInfo>();
        Action<AssetBundleInfo> m_BundleLoadFinish;

        public override void Init()
        {
            m_BundleLoadFinish = AssetBundleLoadFinish;
        }

        public void Update()
        {
            for (int i = m_LoadingAssets.Count - 1; i >= 0; --i)
            {
                AssetInfo info = m_LoadingAssets[i];
                if (info.m_Request == null) // 等待加载Bundle
                    continue;
                if (!info.m_Request.isDone) // 等待加载Asset
                    continue;
                m_LoadingAssets.RemoveAt(i);
                info.m_Asset = info.m_Request.asset;
                info.m_Request = null;
            }
        }

        public AssetInfo LoadAssetAsync(string assetName, string assetBundleName, Action<AssetInfo> onFinish = null)
        {
            AssetBundleInfo bundleInfo = AssetBundleManager.Instance.LoadAssetBundleAsync(assetBundleName, m_BundleLoadFinish);
            if (bundleInfo == null)
            {
                Debug.LogWarning("LoadAssetAsync Fail!!!" + assetBundleName);
                return null;
            }
            AssetInfo info = GetAsset(assetName);
            if(info == null)
            {
                info = AssetInfo.New();
                info.m_Name = assetName;
                info.m_AssetBundle = bundleInfo;
                info.m_ReferencedCount = 0;

                m_BundleNameMap[assetBundleName] = assetName;
                m_Assets.Add(assetName, info);
            }
            info.m_LoadFinish = onFinish;

            m_LoadingAssets.Add(info);
            return info;
        }

        public void AssetBundleLoadFinish(AssetBundleInfo bundleInfo)
        {
            string assetName;
            if (!m_BundleNameMap.TryGetValue(bundleInfo.m_Name, out assetName))
                return;
            AssetInfo info = GetAsset(assetName);
            if (info == null || bundleInfo.m_AssetBundle == null)
                return;
            if (info.IsLoaded() || info.IsLoading())
                return;
            info.m_Request = bundleInfo.m_AssetBundle.LoadAssetAsync(info.m_Name);
        }

        public AssetInfo GetAsset(string assetName)
        {
            AssetInfo info;
            m_Assets.TryGetValue(assetName, out info);
            return info;
        }

        public void UnloadAsset(string assetName)
        {
            AssetInfo info = GetAsset(assetName);
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