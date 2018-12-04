
using System;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

namespace SugarTool
{
    class SugarComMenuItem : Editor
    {
        [MenuItem("Sugar/Main/StartGame", false, 0)]
        static void StartEditorGame()
        {
            EditorSceneManager.OpenScene("Assets/ArtRes/Scene/Main.unity");
            EditorApplication.isPlaying = true;
        }
    }
}