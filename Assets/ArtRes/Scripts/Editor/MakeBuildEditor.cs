
using Game;
using GameJson;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;

namespace SceneEditor
{
    [CustomEditor(typeof(BuildMake))]
    public class BuildMakeEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            BuildMake build = (BuildMake)target;
            base.OnInspectorGUI();
            if (GUILayout.Button("Generate"))
            {
                GameObject prefab = Util_Scene.GetPrefab(build.gameObject);
                if (prefab == null)
                    return;
                BuildRes res = new BuildRes();
                res.type = build.type;
                res.resPath = "Prefab/Build/" + prefab.name;
                string path = "building";
                var config = new JsonDataType<BuildConfig>(path);
                config.Load();
                BuildConfig buildCfg = config.Data;
                if (buildCfg == null)
                    buildCfg = new BuildConfig();
                BuildRes outBuild = null;
                if (!buildCfg.buildings.TryGetValue(res.resPath, out outBuild))
                {
                    buildCfg.buildings.Add(res.resPath, res);
                }

                //save
                JsonWriter writer = new JsonWriter(path + ".json");
                writer.Write(buildCfg);
                writer.TextWriter.Close();
            }
        }
    }
}