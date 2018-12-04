using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
namespace GameUtil
{
	public static class Drawing
	{
        public struct LineType
        {
            public Vector2 pointA;
            public Vector2 pointB;
            public Color color;
        }
		private static Material lineMaterial;
        private static List<LineType> lines;
		static Drawing()
		{
			Drawing.lineMaterial = null;
            lines = new List<LineType>();
            Drawing.Initialize();
		}
		public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width, bool antiAlias = true)
		{
            LineType line = new LineType();
            line.color = color;
            line.pointA = pointA;
            line.pointB = pointB;
            lines.Add(line);
        }

        public static void RenderLines()
        {
            if (lines.Count <= 0)
                return;
            GL.PushMatrix();
            lineMaterial.SetPass(0);
            GL.Begin(GL.LINES);

            for (int i = 0; i < lines.Count; ++i)
            {
                LineType line = lines[i];
                GL.Color(line.color);
                GL.Vertex(line.pointA);
                GL.Vertex(line.pointB);
            }
            GL.End();
            GL.PopMatrix();
            lines.Clear();
        }

        public static void DrawText(Vector2 pos, Color c, string text)
        {
            Color old = GUI.color;
            GUI.color = c;
            GUI.Label(new Rect(pos.x, pos.y, 200, 30),text);
            GUI.color = old;
        }

        private static void Initialize()
		{
            var shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMaterial.SetInt("_ZWrite", 0);
		}
	}
}
