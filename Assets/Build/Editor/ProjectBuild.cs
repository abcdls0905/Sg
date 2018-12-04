using System.IO;
using UnityEditor;
using UnityEngine;
using GameBuild;
using Game;
using System;

class ProjectBuild : Editor
{
    static void PrepareForAppBuild(BuildTarget target, bool buildProj, bool apk)
    {
        if (Application.isPlaying)
            throw new Exception("Unity Is Playing, Can't Build");

        if (!FileManager.IsDirectoryExist(Application.streamingAssetsPath))
            FileManager.CreateDirectory(Application.streamingAssetsPath);

        BuildUtility.StartBuild();
        VersionBuild.Build();
        BuildUtility.EndBuild("version build ");

        BuildUtility.StartBuild();
        LuaBuild.Build();
        BuildUtility.EndBuild("lua build ");

        BuildUtility.StartBuild();
        LuaBuild.BuildCSWrap();
        BuildUtility.EndBuild("cswrap build ");

        BuildUtility.StartBuild();
        DataBuild.Build(target);
        BuildUtility.EndBuild("data build ");

        BuildUtility.StartBuild();
        ProtobufBuild.Build();
        BuildUtility.EndBuild("protobuf build ");

        //AssetDatabase.LoadAssetAtPath();
        BuildUtility.StartBuild();
        ResBuild.Build(target);
        BuildUtility.EndBuild("res build ");

        if (buildProj)
        {
            BuildUtility.StartBuild("unity build start ");
            UnityBuild.Build(target, apk);
            BuildUtility.EndBuild("unity build ");
        }

        LuaBuild.RollBackCSWrap();
    }

    [MenuItem("Build/Build App/Windows64", false, 1)]
    public static void BuildWindows()
    {
        PrepareForAppBuild(BuildTarget.StandaloneWindows64, true, false);
        Debug.Log("Build App OK!" + System.DateTime.Now);
    }

    [MenuItem("Build/Build App/iOS", false, 2)]
    public static void BuildIOS()
    {
        PrepareForAppBuild(BuildTarget.iOS, true, false);
        Debug.Log("Build iOS OK!" + System.DateTime.Now);
    }

    [MenuItem("Build/Build App/Android", false, 3)]
    public static void BuildAndroid()
    {
        PrepareForAppBuild(BuildTarget.Android, true, false);
        Debug.Log("Build Android OK!" + System.DateTime.Now);
    }

    [MenuItem("Build/Build App/AndroidApk", false, 4)]
    public static void BuildAndroidApk()
    {
        PrepareForAppBuild(BuildTarget.Android, true, true);
        Debug.Log("Build Android Apk OK!" + System.DateTime.Now);
    }

    [MenuItem("Build/Build Protobuf", false, 1)]
    public static void BuildProtobuf()
    {
        ProtobufBuild.GenCSharp();
        ProtobufBuild.Build();
    }

    [MenuItem("Build/Build Entitas", false, 2)]
    public static void BuildEntitas()
    {
        Entitas.CodeGeneration.Unity.Editor.UnityCodeGenerator.Generate();
    }

    [MenuItem("Build/Build Lua/Build All", false, 3)]
    public static void BuildLuaAll()
    {
        BuildLuaBin();
        BuildLuaCSWrap();
    }

    [MenuItem("Build/Build Lua/Build Lua Bin", false, 3)]
    public static void BuildLuaBin()
    {
        LuaBuild.Build();
        LuaBuild.CopyLuaFile();
    }

    [MenuItem("Build/Build Lua/Build Lua CSWrap", false, 5)]
    public static void BuildLuaCSWrap()
    {
        LuaBuild.BuildCSWrap();
    }

    [MenuItem("Build/Build Asset/Windows64", false, 1)]
    public static void BuildAssetWindows()
    {
        ResBuild.Build(BuildTarget.StandaloneWindows64);
    }

    [MenuItem("Build/Build Asset/iOS", false, 2)]
    public static void BuildAssetIOS()
    {
        ResBuild.Build(BuildTarget.iOS);
    }

    [MenuItem("Build/Build Asset/Android", false, 3)]
    public static void BuildAssetAndroid()
    {
        ResBuild.Build(BuildTarget.Android);
    }

    [MenuItem("Build/Build Data/Windows64", false, 4)]
    public static void BuildDataWindows()
    {
        DataBuild.Build(BuildTarget.StandaloneWindows64);
    }

    [MenuItem("Build/Build Data/iOS", false, 4)]
    public static void BuildDataIOS()
    {
        DataBuild.Build(BuildTarget.iOS);
    }

    [MenuItem("Build/Build Data/Android", false, 4)]
    public static void BuildDataAndroid()
    {
        DataBuild.Build(BuildTarget.Android);
    }

    [MenuItem("Build/Build Version", false, 4)]
    public static void BuildVersion()
    {
        VersionBuild.Build();
    }


    [MenuItem("Build/Buid Patch/iOS", false, 5)]
    public static void BuildPatchIOS()
    {
        PrepareForAppBuild(BuildTarget.iOS, false, false);
    }

    [MenuItem("Build/Buid Patch/Android", false, 5)]
    public static void BuildPatchAndroid()
    {
        PrepareForAppBuild(BuildTarget.Android, false, false);
    }

    [MenuItem("Build/Buid Patch/Windows", false, 5)]
    public static void BuildPatchWindows()
    {
        PrepareForAppBuild(BuildTarget.StandaloneWindows64, false, false);
    }

    //自动化测试专用
    public static void PrepareAndroid()
    {
        PrepareForAppBuild(BuildTarget.Android, false, false);
    }
}
