
using System;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using Pathfinding.Serialization.JsonFx;
using System.Collections.Generic;
using GameJson;

namespace SugarTool
{
    class SugarComMenuItem : Editor
    {
        [MenuItem("Sg/Main/StartGame", false, 0)]
        static void StartEditorGame()
        {
            EditorSceneManager.OpenScene("Assets/ArtRes/Scene/Main.unity");
            EditorApplication.isPlaying = true;
        }
        [MenuItem("Sg/Main/Write", false, 0)]
        static void WriteTest()
        {
            string path = "guideconfig";
            var config = new Game.JsonDataType<Game.GuideSystem>(path);
            config.Load();
            int x = 0;
            x = 1;
        }
    }
}