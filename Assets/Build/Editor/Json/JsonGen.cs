using CodeDomDemo;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class JsonGenClass
{
    static string SrcPath = Application.dataPath + "/GameData";
    static string DestPath = Application.dataPath + "/Json";

    static void SearchDirectory(string srcPath, string[] extensions, List<string> list)
    {
        foreach (var file in Directory.GetFiles(srcPath, "*.*", SearchOption.AllDirectories).Where(path => extensions == null || extensions.Contains(Path.GetExtension(path))))
            list.Add(file);
    }

    static List<string> SearchAllJson()
    {
        List<string> list = new List<string>();
        SearchDirectory(SrcPath, new string[]{ ".json"}, list);
        return list;
    }

    static void LogList(List<string> list)
    {
        string[] array = list.ToArray();
        for (int i = 0; i < array.Length; ++i)
            array[i] += "\n";
        Debug.Log(string.Concat(array));
    }

    public static void JsonGen()
    {
        List<string> list = SearchAllJson();
        LogList(list);
        for (int i = 0; i < list.Count; ++i)
        {
            string jsonText = File.ReadAllText(list[i]);
            string name = Path.GetFileNameWithoutExtension(list[i]);
            if (name == null || name.Length < 0)
                name = "UnName";
            name = name.Substring(0, 1).ToUpper() + name.Substring(1);
            CodeOrganization code = new CodeOrganization("GameJson", name, jsonText, DestPath);
            code.GenerateCode();
        }
    }
}