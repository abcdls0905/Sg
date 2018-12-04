
using UnityEngine;

namespace SceneEditor
{
    [ExecuteInEditMode]
    public class MapPainter : MonoBehaviour
    {
        public Transform mousePainter;
        public Transform projector;

        public GameObject mapGrid;
        public GameObject mapRoot;

        public float offsetY = -0.49f;
        public Vector3 start = Vector3.zero;
        public int gridWidth = 100;
        public int gridHeight = 100;
        public int scale = 1;
        public Color lineColor = Color.green;

        public bool startBuild = false;
        public bool showGrid = true;

        //[System.NonSerialized]
        public GameObject[,] mapObject = new GameObject[10, 10];

        void OnDrawGizmos()
        {
            if (!showGrid)
                return;
            Gizmos.color = lineColor;
            float offset = -0.5f;
            for (int i = 0; i < gridWidth; i += scale)
            {
                Gizmos.DrawLine(new Vector3(i + offset, offsetY, 0 + offset), new Vector3(i + offset, offsetY, gridHeight + offset));
            }
            for (int j = 0; j < gridHeight; j += scale)
            {
                Gizmos.DrawLine(new Vector3(0 + offset, offsetY, j + offset), new Vector3(gridWidth + offset, offsetY, j + offset));
            }
        }

        public void Generate(int width, int height)
        {
            mapObject = new GameObject[width, height];
        }
    }
}