using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace EditorShow
{
    public class ShowTools : Editor
    {
        static bool IsInCamaraView(Transform tran)
        {
            return true;
        }

        static int GetChildCount(Transform tree)
        {
            int count = 0;
            int childCount = tree.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform child = tree.GetChild(i);
                if (child.gameObject.name == "TreeHolder")
                {
                    if (IsInCamaraView(child))
                        count++;
                }
            }
            return count;
        }

        [MenuItem("Tools/DebugShow/Standard检测")]
        static void CheckStandardShader()
        {
            Renderer[] renders = GameObject.FindObjectsOfType<Renderer>();
            for (int i = 0; i < renders.Length; i++)
            {
                Renderer render = renders[i];
                for (int j = 0; j < render.sharedMaterials.Length; j++)
                {
                    Material material = render.sharedMaterials[j];
                    if (material != null && material.shader != null && material.shader.name == "Standard")
                    {
                        Debug.Log("Standard Shader:" + render.gameObject.name);
                        Selection.activeGameObject = render.gameObject;
                    }
                }
            }
        }


        [MenuItem("Tools/DebugShow/Shadow")]
        static void HeightMap()
        {
            GameObject select = Selection.activeGameObject;
            Renderer[] renders = select.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renders.Length; i++)
            {
                Renderer render = renders[i];
                render.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                render.receiveShadows = true;
            }
        }
        [MenuItem("Tools/Lightmap/设置压缩格式")]
        public static void RefeshLightmapFormat()
        {
            string path = "Assets/ArtRes/Resources_/Lightmap/MapStar";
            string[] files = Directory.GetFiles(path);
            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                if (file.Contains(".meta"))
                    continue;
                TextureImporter texImport = (TextureImporter)TextureImporter.GetAtPath(file);
                TextureImporterPlatformSettings iPhone = texImport.GetPlatformTextureSettings("iPhone");
                iPhone.format = TextureImporterFormat.DXT1;
                texImport.SetPlatformTextureSettings(iPhone);
                TextureImporterPlatformSettings Standalone = texImport.GetPlatformTextureSettings("Standalone");
                Standalone.format = TextureImporterFormat.DXT1;
                texImport.SetPlatformTextureSettings(iPhone);
                texImport.SetPlatformTextureSettings(Standalone);
                AssetDatabase.ImportAsset(file, ImportAssetOptions.ForceUpdate);
            }
            AssetDatabase.Refresh();
        }

        [MenuItem("Tools/Lightmap/测试Lightmapping信息")]
        static void TestLightmapingInfo()
        {
            GameObject[] tempObject = Selection.gameObjects;
            int count = 0;
            for (int i = 0; i < tempObject.Length; i++)
            {
                Renderer[] renders = tempObject[i].GetComponentsInChildren<Renderer>();
                for (int j = 0; j < renders.Length; j++)
                {
                    Renderer render = renders[j];
                    Debug.Log("Object name: " + render.gameObject.name);
                    Debug.Log("Lightmaping Index: " + render.lightmapIndex);
                    Debug.Log("Lightmaping Offset: " + render.lightmapScaleOffset);
                    count++;
                }
            }
            Debug.Log("lightmap count:" + count);
        }

        [MenuItem("Tools/DebugShow/子节点数量")]
        public static void GetChildCount()
        {
            GameObject active = Selection.activeGameObject;
            if (active == null)
                return;
            Debug.Log("childCount:" + active.transform.childCount);
        }

        [MenuItem("Tools/DebugShow/草节点数量")]
        public static void GetCount()
        {
            GameObject active = Selection.activeGameObject;
            if (active == null)
                return;
            int count = 0;
            int forestCount = active.transform.childCount;
            for (int i = 0; i < forestCount; i++)
            {
                Transform forestRoot = active.transform.GetChild(i);
                int forestChildCount = forestRoot.childCount;
                for (int j = 0; j < forestChildCount; j++)
                {
                    Transform tree = forestRoot.GetChild(j);
                    if (tree.name != "TreeHolder")
                        continue;
                    if (IsInCamaraView(tree))
                        count++;
                    count += GetChildCount(tree);
                }
            }
            Debug.Log("Totle Grass Count:" + count);
        }
    }
}