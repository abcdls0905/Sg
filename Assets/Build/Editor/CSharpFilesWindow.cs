using Entitas.Unity.Editor;
using GameUtil;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CSharpFilesWindow : EditorWindow
{
    [MenuItem("Tools/CSharpFiles", false, 3)]
    public static void OpenWindow()
    {
        EntitasEditorLayout.ShowWindow<CSharpFilesWindow>("CSharpFiles");

    }

    public class ShowInfo
    {
        public ShowInfo(string nameSpace)
        {
            this.nameSpace = nameSpace;
        }
        public string nameSpace = string.Empty;
        public List<Type> nameClass = new List<Type>();
        public bool foldout = false;
    }

    List<Type> _all = new List<Type>();
    List<ShowInfo> _show = new List<ShowInfo>();
    bool _allDraw = true;
    string _cacheString = string.Empty;
    string _searchString = "";
    void OnEnable()
    {
        RefreshShow();
    }

    void OnGUI()
    {
        _allDraw = EntitasEditorLayout.DrawSectionHeaderToggle(_cacheString, _allDraw);
        if (_allDraw)
            RefreshViews(ref _show);
    }

    Vector2 scrollPos = new Vector2(0, 0);
    public void RefreshViews(ref List<ShowInfo> files)
    {
        EntitasEditorLayout.BeginSectionContent();
        var searchString = EntitasEditorLayout.SearchTextField(_searchString);
        if(_searchString != searchString)
        {
            _searchString = searchString;
            RefreshShow();
        }
        using (var scrollScope = new EditorGUILayout.ScrollViewScope(scrollPos))
        {
            scrollPos = scrollScope.scrollPosition;
            foreach (var info in files)
            {
                var title = string.Format("{0}( {1} )", info.nameSpace, info.nameClass.Count);
                info.foldout = EditorGUILayout.Foldout(info.foldout, title);
                if (info.foldout)
                {
                    foreach (var type in info.nameClass)
                    {
                        var typeName = "";
                        if (type.IsClass)
                            typeName = "Class";
                        else if (type.IsEnum)
                            typeName = "Enum";
                        else if (type.IsValueType)
                            typeName = "Struct";
                        else if (type.IsAbstract)
                            typeName = "Abstract";
                        else if (type.IsInterface)
                            typeName = "Interface";

                            var _name = string.Format("    {0}( {1} )", type.Name, typeName);
                        EditorGUILayout.LabelField(_name);
                    }
                }
            }
        }

        EntitasEditorLayout.EndSectionContent();
    }

    public void RefreshShow()
    {
        ClassSearch search = new ClassSearch(null, null, null);

        if (_all.Count == 0)
        {
            var iter = search.GetEnumerator();
            while (iter.MoveNext())
            {
                _all.Add(iter.Current);
            }
        }

        _show.Clear();
        foreach (var type in _all)
        {
            if (type.Name.Contains(_searchString))
            {
                var info = _show.Find(x => x.nameSpace == type.Namespace);
                if (info == null)
                {
                    info = new ShowInfo(type.Namespace);
                    _show.Add(info);
                }
                info.nameClass.Add(type);
            }
        }
        _cacheString = "AllFiles(" + _show.Count.ToString() + " / " + _all.Count.ToString() + ")";
    }
}
