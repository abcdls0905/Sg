
using System.IO;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;

namespace ArtRes
{
    [CustomEditor(typeof(BoxColor))]

    class BoxColorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical();

            if (GUILayout.Button(new GUIContent("生成数据"), GUILayout.Width(200)))
            {
                GenerateColorData();
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        void GenerateColorData()
        {
            BoxColor myTarget = (BoxColor)target;
            Color3Data data = new Color3Data();
            data.up = myTarget.Up;
            data.front = myTarget.Front;
            data.back = myTarget.Back;
            data.left = myTarget.Left;
            data.right = myTarget.Right;
            data.down = myTarget.Down;
            string path = "color3config.json";
            if (myTarget.colorMode == AkColorMode.Ak_TwoColor)
                path = "color2config.json";
            JsonWriter writer = new JsonWriter(path);
            writer.Write(data);
            writer.TextWriter.Close();
        }
    }
}