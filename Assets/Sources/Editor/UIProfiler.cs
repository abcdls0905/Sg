using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using UnityEditor.SceneManagement;
using FairyGUI;

public class UIProfiler : EditorWindow
{
    [MenuItem("Tools/Profiler/UI")]
    static void AddWindow()
    {
        //创建窗口
        Rect wr = new Rect(0, 0, 300, 300);
        UIProfiler window = (UIProfiler)EditorWindow.GetWindowWithRect(typeof(UIProfiler), wr, true, "UIProfiler");
        window.Show();
        window.autoRepaintOnSceneChange = true;    
    }

    void Update()
    {
        Repaint();
    }

    void OnGUI()
    {
        EditorGUILayout.Space();
        EditorGUILayout.FloatField("ObjectCount", Stats.ObjectCount);

        EditorGUILayout.Space();
        EditorGUILayout.FloatField("GraphicsCount", Stats.GraphicsCount);

        EditorGUILayout.Space();
        EditorGUILayout.FloatField("LatestGraphicsCreation", Stats.LatestGraphicsCreation);

        EditorGUILayout.Space();
        EditorGUILayout.FloatField("LatestObjectCreation", Stats.LatestObjectCreation);

        EditorGUILayout.Space();
        //EditorGUILayout.FloatField("LatestTextFieldCount", Stats.LatestTextFieldCount);
    }

}
