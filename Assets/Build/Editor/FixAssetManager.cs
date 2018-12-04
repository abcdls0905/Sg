using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using AssetReport;

namespace FixAssetReport
{
    public static class FixAssetManager
    {
        public static int AssetCount
        {
            get
            {
                return AssetReferMap.Count;
            }
        }
        public static Dictionary<string, AssetInfo> AssetReferMap = new Dictionary<string, AssetInfo>();

        public static AssetInfo GetAssetInfo(string assetPath)
        {
            AssetInfo ret = null;
            AssetReferMap.TryGetValue(assetPath.ToLower(), out ret);
            if (ret == null)
                Debug.Log("null GetAssetInfo " + assetPath);
            return ret;
        }

        public static void BuildCacheData()
        {
            if (AssetCount != 0)
                AssetReferMap.Clear();
            //EditorSettings.serializationMode = SerializationMode.ForceText;
            string assetBasePath = Application.dataPath;
            string[] files = Directory.GetFiles(assetBasePath, "*.*", SearchOption.AllDirectories);
            int fileCount = files.Length;
            for (int fileIndex = 0; fileIndex < fileCount; ++fileIndex)
            {
                string fullAssetPath = files[fileIndex];
                string assetPath = ARUtil.GetRelativeAssetsPath(fullAssetPath);
                if (ARUtil.IsFileNotReport(assetPath))
                    continue;
                EditorUtility.DisplayProgressBar("Creating...", assetPath, (float)fileIndex / (float)fileCount);

                AssetReferMap.Add(assetPath.ToLower(), new AssetInfo(assetPath, FiltersUsed.GetFileFilterName(assetPath), ARUtil.GetFileSize(fullAssetPath)));
            }

            //Debug.Log("Asset Count " + AssetCount);

            int assetIndex = 0;
            foreach (var kv in AssetReferMap)
            {
                ++assetIndex;
                string[] dependences = AssetDatabase.GetDependencies(kv.Key, false);
                if (dependences.Length == 0)
                    continue;
                string assetPath = kv.Key;
                AssetInfo assetInfo = kv.Value;
                EditorUtility.DisplayProgressBar("Analysing...", assetPath, (float)assetIndex / (float)AssetCount);

                for (int dependIndex = 0; dependIndex < dependences.Length; ++dependIndex)
                {
                    string dependAsset = dependences[dependIndex];
                    if (dependAsset.ToLower() == assetPath.ToLower())
                        continue;

                    AssetInfo dependAssetInfo = GetAssetInfo(dependAsset);
                    if (dependAssetInfo != null)
                    {
                        dependAssetInfo.Referred(assetPath);
                        assetInfo.Depend(dependAsset);
                    }
                    else
                    {
                        Debug.LogWarning("---- Not find in refermap " + dependAsset);
                    }
                }
            }

            EditorUtility.ClearProgressBar();
        }
        public static HashSet<AssetInfo> GetAssets(FileFilters type, FileFilters folder, string filtername)
        {
            HashSet<AssetInfo> ret = new HashSet<AssetInfo>();
            foreach (var kv in AssetReferMap)
            {
                if (type != null && !type.IsFileInFilter(kv.Key))
                    continue;
                if (folder != null && !folder.IsFileInFilter(kv.Key))
                    continue;
                if (kv.Value.CheckFilterName(filtername))
                    ret.Add(kv.Value);
            }
            return ret;
        }
        public static HashSet<AssetInfo> GetReferrenceAssets(string filtername)
        {
            HashSet<AssetInfo> ret = new HashSet<AssetInfo>();
            if (AssetCount == 0)
                return ret;
            FileFilters resFilter = FiltersUsed.GetFileFiltersByLabel("-->Resources");
            HashSet<AssetInfo> resArray = GetAssets(null, resFilter, "");
            var scenes = EditorBuildSettings.scenes;
            foreach (var s in scenes)
            {
                var info = GetAssetInfo(s.path);
                if (info != null)
                    resArray.Add(GetAssetInfo(s.path));
                else
                    Debug.Log("Scene " + s.path + " is not exist.");
            }

            foreach (var info in resArray)
            {
                if (info.CheckFilterName(filtername))
                    ret.Add(info);

                foreach (var deps in info.Dependences)
                {
                    AssetInfo depinfo = GetAssetInfo(deps);
                    if (depinfo.CheckFilterName(filtername))
                        ret.Add(depinfo);
                }
            }
            return ret;
        }
        public static HashSet<AssetInfo> GetUnreferrenceAssets(string filtername)
        {
            HashSet<AssetInfo> ret = new HashSet<AssetInfo>();
            HashSet<AssetInfo> referrence = GetReferrenceAssets(filtername);
            foreach (var kv in AssetReferMap)
            {
                if (!kv.Value.CheckFilterName(filtername))
                    continue;
                if (referrence.Contains(kv.Value))
                    continue;
                ret.Add(kv.Value);
            }
            return ret;
        }
        public static HashSet<AssetInfo> GetDependencyItems(List<AssetInfo> listinfo)
        {
            HashSet<AssetInfo> ret = new HashSet<AssetInfo>();
            Dictionary<string, bool> recordMap = new Dictionary<string, bool>();
            foreach (var info in listinfo)
            {
                RecursiveAddDependences(info, ret, recordMap);
            }
            return ret;
        }

        private static void RecursiveAddDependences(AssetInfo info, HashSet<AssetInfo> retList, Dictionary<string, bool> recordMap)
        {
            foreach (var assetstring in info.Dependences)
            {
                if (recordMap.ContainsKey(assetstring))
                    continue;

                AssetInfo assetinfo = GetAssetInfo(assetstring);
                if (assetinfo != null)
                {
                    retList.Add(assetinfo);
                    recordMap.Add(assetstring, true);

                    RecursiveAddDependences(assetinfo, retList, recordMap);
                }
            }
        }

        public static HashSet<AssetInfo> GetReferredItems(List<AssetInfo> listinfo)
        {
            HashSet<AssetInfo> ret = new HashSet<AssetInfo>();
            foreach (var info in listinfo)
            {
                foreach (var assetstring in info.References)
                {
                    AssetInfo assetinfo = GetAssetInfo(assetstring);
                    if (assetinfo != null)
                        ret.Add(assetinfo);
                }
            }
            return ret;
        }
    }
}
