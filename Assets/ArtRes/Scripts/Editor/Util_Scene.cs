
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System;

namespace SceneEditor
{
    public static class Util_Scene
    {

        public static GameObject GetPrefab(GameObject targetObj)
        {
            if (IsPrefab(targetObj))
            {
                GameObject prefab = (GameObject)PrefabUtility.GetPrefabParent(targetObj);
                return prefab;
            }
            return null;
        }

        public static bool IsPrefab(GameObject target)
        {
            if (target == null)
                return false;
            PrefabType prefabType = PrefabUtility.GetPrefabType(target);
            return (prefabType == PrefabType.PrefabInstance || prefabType == PrefabType.DisconnectedPrefabInstance);
        }

        public static void ApplyPrefab(GameObject target)
        {
            if (!IsPrefab(target))
                return;
            UnityEngine.Object prefabParent = PrefabUtility.GetPrefabParent(target);
            GameObject gameObject = PrefabUtility.FindValidUploadPrefabInstanceRoot(target);
            PrefabUtility.ReplacePrefab(gameObject, prefabParent, ReplacePrefabOptions.ConnectToPrefab);
        }

        public static void RevertPrefab(GameObject target)
        {
            PrefabType prefabType = PrefabUtility.GetPrefabType(target);
            bool flag = prefabType == PrefabType.DisconnectedModelPrefabInstance || prefabType == PrefabType.DisconnectedPrefabInstance;
            GameObject gameObject;
            if (flag)
                gameObject = PrefabUtility.FindRootGameObjectWithSameParentPrefab(target);
            else
                gameObject = PrefabUtility.FindValidUploadPrefabInstanceRoot(target);
            if (flag)
                PrefabUtility.ReconnectToLastPrefab(gameObject);
            PrefabUtility.RevertPrefabInstance(gameObject);
        }

        public static void SavePrefab(string path, GameObject target)
        {
            if (File.Exists(path))
            {
                Debug.LogWarning("Prefab Has Exist!");
                ApplyPrefab(target);
                return;
            }
            PrefabUtility.CreatePrefab(path, (GameObject)target, ReplacePrefabOptions.ConnectToPrefab);
        }

        public static void DelTypes(Type t)
        {
            UnityEngine.Object[] objs = GameObject.FindObjectsOfType(t);
            for (int i = objs.Length - 1; i >= 0; i--)
            {
                UnityEngine.Object obj = objs[i];
                MonoBehaviour script = (MonoBehaviour)obj;
                UnityEngine.Object.DestroyImmediate(script.gameObject, true);
            }
        }

        public static string GetAssetResourcesPath(UnityEngine.Object asset)
        {
            if (asset == null)
                return "";
            string path = AssetDatabase.GetAssetPath(asset.GetInstanceID());
            string separator = "Resources/";
            string[] splits = Regex.Split(path, separator, RegexOptions.IgnoreCase);
            return splits[1];
        }


    }
}