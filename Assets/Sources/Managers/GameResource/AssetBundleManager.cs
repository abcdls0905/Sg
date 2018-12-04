using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using GameUtil;
using Game;

namespace Game
{
    public enum AssetBundleState
    {
        Unload = 0,
        Pending,
        Loading,
        WaitingDep,
        Cancel,
        Loaded
    }
    public class AssetBundleInfo
    {
        public string m_Name;
        public AssetBundleInfo[] m_Dependents;
        public AssetBundle m_AssetBundle;
        public AssetBundleCreateRequest m_Request;
        public int m_ReferencedCount;
        public Action<AssetBundleInfo> m_LoadFinish;
        public AssetBundleState m_state = AssetBundleState.Unload;

        public void AddRef()
        {
            ++m_ReferencedCount;
        }

        public void DecRef(bool unloadObjs)
        {
            if (m_state == AssetBundleState.Unload || m_state == AssetBundleState.Cancel)
            {
                Debug.LogWarningFormat("DecRef Failed!!! {0}", m_Name);
                return;
            }
            if (--m_ReferencedCount > 0)
                return;
            if (m_state == AssetBundleState.Loading || m_state == AssetBundleState.WaitingDep)
                m_state = AssetBundleState.Cancel;
            else
                Unload(unloadObjs);
        }

        public void Unload(bool unloadObjs)
        {
            if (m_AssetBundle != null)
                m_AssetBundle.Unload(unloadObjs);
            m_AssetBundle = null;
            m_Request = null;
            m_ReferencedCount = 0;
            m_LoadFinish = null;
            m_state = AssetBundleState.Unload;
        }

        public bool CheckDepLoaded()
        {
            for (int i = 0; i < m_Dependents.Length; ++i)
            {
                if (!m_Dependents[i].CheckDepLoaded())
                    return false;
            }
            return CheckLoaded();
        }

        public bool CheckLoaded()
        {
            return m_AssetBundle != null || (m_Request != null && m_Request.isDone);
        }

        public bool IsLoading()
        {
            return m_state == AssetBundleState.Loading || m_state == AssetBundleState.Pending || m_state == AssetBundleState.WaitingDep;
        }

        public bool IsCancel()
        {
            return m_state == AssetBundleState.Cancel;
        }

        public bool IsLoaded()
        {
            return m_state == AssetBundleState.Loaded;
        }
    }

    public class AssetBundleManager : Singleton<AssetBundleManager>
    {
        const int MaxLoadingBundleNum = 50;
        string AssetBundlesUpdatePath = string.Empty;
        string AssetBundlesStreamPath = string.Empty;

        Dictionary<string, AssetBundleInfo> m_AssetBundles = new Dictionary<string, AssetBundleInfo>();
        List<AssetBundleInfo> m_LoadingAssetBundles = new List<AssetBundleInfo>();

        public List<AssetBundleInfo> GetLoadingAssetBundleList()
        {
            return m_LoadingAssetBundles;
        }

        public Dictionary<string, AssetBundleInfo> GetAssetBundleMap()
        {
            return m_AssetBundles;
        }

        public override void Init()
        {
            AssetBundlesUpdatePath = Application.persistentDataPath + "/assets/" + GameEnvSetting.AssetBundleReadPath;
#if UNITY_ANDROID
            AssetBundlesStreamPath = Application.dataPath + "!assets/" + GameEnvSetting.AssetBundleReadPath;
#else
            AssetBundlesStreamPath = Application.streamingAssetsPath + "/" + GameEnvSetting.AssetBundleReadPath;
#endif

            if (GameEnvSetting.EnableAssetBundle)
                InitManifest();
        }

        public void InitManifest()
        {
            AssetBundleInfo manifestInfo = new AssetBundleInfo();
            manifestInfo.m_Name = "assetbundles";
            LoadFromFile(manifestInfo);
            if (manifestInfo.m_AssetBundle == null)
            {
                Debug.LogWarning("Load Manifest Fail!!!");
                return;
            }
            AssetBundleManifest manifest = (AssetBundleManifest)manifestInfo.m_AssetBundle.LoadAsset("assetbundlemanifest");
            if (manifest == null)
            {
                manifestInfo.m_AssetBundle.Unload(true);
                Debug.LogWarning("Load AssetBundleManifest Fail!!!");
                return;
            }
            var bundles = manifest.GetAllAssetBundles();
            foreach (var bundle in bundles)
            {
                AssetBundleInfo info = new AssetBundleInfo();
                info.m_Name = bundle;
                m_AssetBundles.Add(bundle, info);
            }
            foreach (var iter in m_AssetBundles)
            {
                AssetBundleInfo info = iter.Value;
                var deps = manifest.GetDirectDependencies(iter.Key);
                info.m_Dependents = new AssetBundleInfo[deps.Length];
                for (int i = 0; i < deps.Length; ++i)
                {
                    AssetBundleInfo depInfo;
                    if (!m_AssetBundles.TryGetValue(deps[i], out depInfo))
                    {
                        Debug.LogWarning("Load AssetBundle Depend Fail!!!" + deps[i]);
                    }
                    info.m_Dependents[i] = depInfo;
                }
            }
            UnityEngine.Object.DestroyImmediate(manifest, true);
            manifestInfo.Unload(true);
        }

        public override void UnInit()
        {
            //UnloadAllAssetBundles();
        }

        public void UnloadAllAssetBundles() //TODO
        {
            for (int i = m_LoadingAssetBundles.Count - 1; i >= 0; --i)
            {
                var info = m_LoadingAssetBundles[i];
                if (info.m_state == AssetBundleState.Pending)
                {
                    info.Unload(true);
                    m_LoadingAssetBundles.RemoveAt(i);
                }
                else
                    info.m_state = AssetBundleState.Cancel;
            }
            var iter = m_AssetBundles.GetEnumerator();
            while(iter.MoveNext())
            {
                var info = iter.Current.Value;
                if (info.m_state == AssetBundleState.Loaded)
                    info.Unload(true);
            }
        }

        public bool NeedPending()
        {
            int loading_num = 0;
            for (int i = 0; i < m_LoadingAssetBundles.Count; ++i)
            {
                if (m_LoadingAssetBundles[i].m_state == AssetBundleState.Loading)
                    loading_num++;
            }
            return loading_num > MaxLoadingBundleNum;
        }

        public void Update()
        {
            int loading_num = MaxLoadingBundleNum;
            for (int i = m_LoadingAssetBundles.Count - 1; i >= 0; --i)
            {
                AssetBundleInfo info = m_LoadingAssetBundles[i];
                bool ret = UpdateAssetBundle(info);
                if (!ret)
                {
                    if (info.m_state == AssetBundleState.Loading)
                        --loading_num;
                }
                else
                    m_LoadingAssetBundles.RemoveAt(i);
            }
            for (int i = m_LoadingAssetBundles.Count - 1; i >= 0; --i)
            {
                AssetBundleInfo info = m_LoadingAssetBundles[i];
                bool ret = UpdateAssetBundle(info);
                if (!ret)
                {
                    if (loading_num > 0 && info.m_state == AssetBundleState.Pending)
                    {
                        info.m_state = AssetBundleState.Loading;

                        string path = GetAssetBundlePath(info.m_Name);
                        info.m_Request = AssetBundle.LoadFromFileAsync(path);
                        --loading_num;
                    }
                }
                else
                    m_LoadingAssetBundles.RemoveAt(i);
            }
        }

        public bool UpdateAssetBundle(AssetBundleInfo info)
        {
            switch(info.m_state)
            {
                case AssetBundleState.Loaded:
                        return true;
                case AssetBundleState.Loading:
                    {
                        if (!info.CheckLoaded()) // 自身加载完成
                            return false;
                        info.m_state = AssetBundleState.WaitingDep;
                        return false;
                    }
                case AssetBundleState.WaitingDep: 
                    {
                        if (!info.CheckDepLoaded()) // 等待依赖加载
                            return false;
                        info.m_state = AssetBundleState.Loaded;
                        info.m_AssetBundle = info.m_Request.assetBundle;
                        info.m_Request = null;
                        if (info.m_LoadFinish != null)
                            info.m_LoadFinish(info);
                        return true;
                    }
                case AssetBundleState.Cancel:
                    {
                        if (!info.CheckLoaded()) // 自身加载完成
                            return false;
                        info.m_state = AssetBundleState.Unload;
                        info.m_AssetBundle = info.m_Request.assetBundle;
                        info.m_Request = null;
                        info.Unload(true);
                        return true;
                    }
                case AssetBundleState.Pending:// 等待队列
                    {
                        return false;
                    }
                default:
                    {
                        info.m_state = AssetBundleState.Unload;
                        return true;
                    }
            }
        }

        public AssetBundleInfo LoadAssetBundleAsync(string assetBundleName, Action<AssetBundleInfo> onFinish = null)
        {
            AssetBundleInfo info = GetAssetBundleInfo(assetBundleName);
            if (info == null)
            {
                Debug.LogWarningFormat("LoadAssetBundle Failed!!! {0}", assetBundleName);
                return null;
            }
            return LoadAssetBundleAsyncInternal(info, onFinish);
        }

        protected AssetBundleInfo LoadAssetBundleAsyncInternal(AssetBundleInfo info, Action<AssetBundleInfo> onFinish)
        {
            for (int i = 0; i < info.m_Dependents.Length; ++i)
            {
                if (info.m_Dependents[i] != null)
                    LoadAssetBundleAsyncInternal(info.m_Dependents[i], null);
            }
            if (info.IsLoaded())
            {
                if (onFinish != null)
                    onFinish(info);
                info.AddRef();
            }
            else
            {
                if (onFinish != null)
                    info.m_LoadFinish += onFinish;

                if (info.IsLoading())
                    info.AddRef();
                else
                    LoadFromFileAsync(info);
            }
            return info;
        }

        public AssetBundleInfo LoadAssetBundle(string assetBundleName)
        {
            AssetBundleInfo info = GetAssetBundleInfo(assetBundleName);
            if (info == null)
            {
                Debug.LogWarningFormat("LoadAssetBundle Failed!!! {0}", assetBundleName);
                return null;
            }
            return LoadAssetBundleInternal(info);
        }

        AssetBundleInfo LoadAssetBundleInternal(AssetBundleInfo info)
        {
            for (int i = 0; i < info.m_Dependents.Length; ++i)
            {
                if(info.m_Dependents[i] != null)
                    LoadAssetBundleInternal(info.m_Dependents[i]);
            }
            if (info.IsLoaded() || info.IsLoading())
                info.AddRef();
            else
                LoadFromFile(info);
            return info;
        }

        void LoadFromFile(AssetBundleInfo info)
        {
            string path = GetAssetBundlePath(info.m_Name);
            if (path == string.Empty)
                return;
            info.m_state = AssetBundleState.Loaded;
            info.m_AssetBundle = AssetBundle.LoadFromFile(path);
            info.m_ReferencedCount = 1;
        }

        void LoadFromFileAsync(AssetBundleInfo info)
        {
            string path = GetAssetBundlePath(info.m_Name);
            if (path == string.Empty)
                return;
            bool addList = !info.IsCancel();
            if (!NeedPending())
            {
                info.m_state = AssetBundleState.Loading;
                info.m_Request = AssetBundle.LoadFromFileAsync(path);
            }
            else
                info.m_state = AssetBundleState.Pending;
            info.m_ReferencedCount = 1;
            if (addList)
                m_LoadingAssetBundles.Add(info);
        }

        public void UnloadAssetBundle(string assetBundleName, bool unloadObjs)
        {
            AssetBundleInfo info = GetAssetBundleInfo(assetBundleName);
            if(info == null || info.m_state == AssetBundleState.Unload)
            {
                Debug.LogWarningFormat("UnloadAssetBundle Failed!!! {0}", assetBundleName);
                return;
            }
            UnloadAssetBundleInternal(info, unloadObjs);
        }

        protected void UnloadAssetBundleInternal(AssetBundleInfo info, bool unloadObjs)
        {
            for (int i = 0; i < info.m_Dependents.Length; ++i)
            {
                if (info.m_Dependents[i] != null)
                    UnloadAssetBundleInternal(info.m_Dependents[i], unloadObjs);
            }
            info.DecRef(unloadObjs);
        }

        public AssetBundleInfo GetAssetBundleInfo(string assetBundleName)
        {
            AssetBundleInfo info;
            m_AssetBundles.TryGetValue(assetBundleName, out info);
            return info;
        }

        public string GetAssetBundlePath(string assetBundleName)
        {
            string path = string.Empty;
            if (GameEnvSetting.EnablePersistentPath)
            {
                path = Path.Combine(AssetBundlesUpdatePath, assetBundleName);
                if (File.Exists(path))
                    return path;
            }
            path = Path.Combine(AssetBundlesStreamPath, assetBundleName);
            return path;
        }
    }
}
