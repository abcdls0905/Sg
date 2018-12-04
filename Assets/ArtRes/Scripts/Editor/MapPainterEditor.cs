using UnityEngine;
using UnityEditor;

namespace SceneEditor
{
	[CustomEditor(typeof(MapPainter))]
	public class MapPainterEditor : Editor
    {

        void UpdateBrush()
        {
            MapPainter painter = target as MapPainter;
            if (!painter.startBuild)
                return;
            if (painter.projector == null)
                painter.projector = GameObject.Instantiate<Transform>(painter.mousePainter);
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
            Camera sceneCamera = SceneView.lastActiveSceneView.camera;
            Vector2 mousePos = Event.current.mousePosition;
            Event cur = Event.current;
            Ray ray = HandleUtility.GUIPointToWorldRay(cur.mousePosition);

            RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                if (!hit.collider.gameObject.CompareTag("PainterGround"))
                    continue;
                painter.projector.position = hit.point;
                if (cur.type == EventType.MouseDown || cur.type == EventType.MouseDrag)
                {
                    int hitX = (int)(hit.point.x + 0.5f);
                    int hitZ = (int)(hit.point.z + 0.5f);
                    if (cur.button == 0)   //left mouse
                    {
                        if (hitX >= painter.mapObject.GetLength(0) || hitX < 0 || hitZ > painter.mapObject.GetLength(1) || hitZ < 0)
                            break;
                        if (painter.mapObject[hitX, hitZ] != null)
                            break;
                        Vector3 position = new Vector3();
                        position.x = hitX;
                        position.z = hitZ;
                        position.y += painter.offsetY;
                        GameObject gameObject = GameObject.Instantiate<GameObject>(painter.mapGrid);
                        gameObject.transform.position = position;
                        if (painter.mapRoot != null)
                            gameObject.transform.parent = painter.mapRoot.transform;
                        painter.mapObject[hitX, hitZ] = gameObject;
                    }
                    else if (cur.button == 1)
                    {
                        if (hitX >= painter.mapObject.GetLength(0) || hitX < 0 || hitZ > painter.mapObject.GetLength(1) || hitZ < 0)
                            break;
                        if (painter.mapObject[hitX, hitZ] == null)
                            break;
                        GameObject.DestroyImmediate(painter.mapObject[hitX, hitZ], false);
                        painter.mapObject[hitX, hitZ] = null;
                    }
                }
                return;
            }
        }

        public void OnSceneGUI()
        {
            UpdateBrush();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Repaint();
        }
    }
}