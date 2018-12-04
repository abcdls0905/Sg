using Entitas.Unity.Editor;
using UnityEditor;
using UnityEngine;
using Game;
using System.Linq;

[CustomEditor(typeof(DebugManager))]
public class DebugManagerEditor : Editor
{
    //DebugManager _debugManager;

    static bool _trackCamera = false;

    void OnEnable()
    {
        //_debugManager = target as DebugManager;
    }

    void OnDisable()
    {
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        DrawUtilities();
        DrawPool();
        EditorGUILayout.Space();
        EditorUtility.SetDirty(target);
    }

    static bool _utilities = true;
    void DrawUtilities()
    {
        _utilities = EntitasEditorLayout.DrawSectionHeaderToggle("Utilities", _utilities);
        if (_utilities)
        {
            EntitasEditorLayout.BeginSectionContent();
            {
                var trackCamera = EditorGUILayout.Toggle("Track MainCamera", _trackCamera);
                if (_trackCamera != trackCamera)
                {
                    if (trackCamera)
                        SceneView.onSceneGUIDelegate += OnSceneFunc;
                    else
                        SceneView.onSceneGUIDelegate -= OnSceneFunc;

                    _trackCamera = trackCamera;
                }
            }
            EntitasEditorLayout.EndSectionContent();
        }
    }

    bool _gameObjectPool;
    bool _resource;
    bool _assetBundle;
    void DrawPool()
    {
        var gomap = GameObjectPool.Instance.GetPooledGameObjectMap();
        var gorecycle = GameObjectPool.Instance.GetDelayRecycle();

        var TextStyle = new GUIStyle();
        TextStyle.normal.textColor = Color.red;
        TextStyle.alignment = TextAnchor.MiddleCenter;
        TextStyle.fontStyle = FontStyle.Bold;

        var all = gomap.Sum(o => o.Value.m_instantiate);
        var pooled = gomap.Sum(o => o.Value.m_pooled.Count);
        var delay = gorecycle.Count;
        var title = string.Format("GameObject(All:{0}, Pooled:{1}, Delay:{2})", all, pooled, delay);

        _gameObjectPool = EntitasEditorLayout.DrawSectionHeaderToggle(title, _gameObjectPool);
        if (_gameObjectPool)
        {
            EntitasEditorLayout.BeginSectionContent();
            EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField("Pooled:", TextStyle);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Name", GUILayout.Width(150));
            EditorGUILayout.LabelField("AllNum", GUILayout.Width(60));
            EditorGUILayout.LabelField("PoolNum", GUILayout.Width(60));
            EditorGUILayout.LabelField("LeftTime", GUILayout.Width(60));
            EditorGUILayout.EndHorizontal();

            var array = gomap.OrderBy(o=>o.Key).ToArray();
            for (int i = 0; i < array.Length; ++i)
            {
                var poolName = array[i].Key;
                var pool = array[i].Value;
                if (pool.m_instantiate == 0)
                    continue;
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(FileManager.GetFullName(poolName), GUILayout.Width(150));
                EditorGUILayout.LabelField(pool.m_instantiate.ToString(), GUILayout.Width(60));
                EditorGUILayout.LabelField(pool.m_pooled.Count.ToString(), GUILayout.Width(60));
                float leftTime = pool.m_pooled.Count > 0 ? pool.m_pooled[0].m_checkTime : 0;
                EditorGUILayout.LabelField(leftTime.ToString(), GUILayout.Width(60));
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
            EntitasEditorLayout.EndSectionContent();
        }

        var resMap = ResourceManager.Instance.GetCachedResourceMap();
        var resLoading = ResourceManager.Instance.GetLoadingResourceList();

        var resall = resMap.Count;
        var resloading = resLoading.Count;
        var restitle = string.Format("Resource(All:{0}, Loading:{1})", resall, resloading);
        _resource = EntitasEditorLayout.DrawSectionHeaderToggle(restitle, _resource);
        if (_resource)
        {
            EntitasEditorLayout.BeginSectionContent();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Loading:", TextStyle);
            foreach (var res in resLoading)
            {
                EditorGUILayout.LabelField(FileManager.GetFullName(res.m_name), GUILayout.Width(150));
            }
            EditorGUILayout.LabelField("All:", TextStyle);
            EditorGUILayout.LabelField("Name", GUILayout.Width(150));
            var array = resMap.OrderBy(o => o.Key).ToArray();
            foreach (var res in array)
            {
                EditorGUILayout.LabelField(FileManager.GetFullName(res.Key), GUILayout.Width(150));
            }
            EditorGUILayout.EndVertical();
            EntitasEditorLayout.EndSectionContent();
        }

        var assetMap = AssetBundleManager.Instance.GetAssetBundleMap();
        var assetLoading = AssetBundleManager.Instance.GetLoadingAssetBundleList();

        var assetall = assetMap.Sum(o => (o.Value.IsLoaded() ? 1 : 0));
        var assetloading = assetLoading.Count;
        var assettitle = string.Format("AssetBundle(All:{0}, Loading:{1})", assetall, assetloading);
        _assetBundle = EntitasEditorLayout.DrawSectionHeaderToggle(assettitle, _assetBundle);
        if (_assetBundle)
        {
            EntitasEditorLayout.BeginSectionContent();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Loading:", TextStyle);
            foreach (var res in assetLoading)
            {
                EditorGUILayout.LabelField("    " + FileManager.GetFullName(res.m_Name), GUILayout.Width(150));
            }
            EditorGUILayout.LabelField("All:", TextStyle);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Name", GUILayout.Width(150));
            EditorGUILayout.LabelField("Ref", GUILayout.Width(60));
            EditorGUILayout.EndHorizontal();
            var array = assetMap.OrderBy(o => o.Key).ToArray();
            foreach (var res in array)
            {
                if (res.Value.IsLoaded())
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(FileManager.GetFullName(res.Key), GUILayout.Width(150));
                    EditorGUILayout.LabelField(res.Value.m_ReferencedCount.ToString(), GUILayout.Width(60));
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndVertical();
            EntitasEditorLayout.EndSectionContent();
        }
    }

    public void OnSceneFunc(SceneView sceneView)
    {
        if (_trackCamera)
        {
            var mainCamera = Camera.main;
            if (SceneView.lastActiveSceneView != null && mainCamera != null)
                SceneView.lastActiveSceneView.LookAtDirect(mainCamera.transform.position, mainCamera.transform.rotation);
        }
    }
}