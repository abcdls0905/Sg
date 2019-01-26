using Game;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameBuild
{
    public static class UnityBuild
    {
        static void RenameDir(string oriName, string dstName)
        {
            if (!Directory.Exists(oriName))
                return;
            if (Directory.Exists(dstName))
                Directory.Delete(dstName, true);
            Debug.LogFormat("RenameDir: {0}->{1}", oriName, dstName);
            Directory.Move(oriName, dstName);
        }

        static void RenameResourcesDir(bool bRevert)
        {
            string projPath = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));

            // 重命名Resources文件夹，防止完整包再次导入同样的资源
            foreach (var dir in GameEnvSetting.AssetBundleDir)
            {
                string resPath = Path.Combine(Application.dataPath, dir);
                //string abPath = resPath + "_Bundle";
                string abPath = Path.Combine(projPath, "ABBundle");
                if (resPath.EndsWith("/"))
                    abPath = resPath.Substring(0, resPath.Length - 1) + "_Bundle";

                if (bRevert)
                {
                    RenameDir(abPath, resPath);
                }
                else
                {
                    RenameDir(resPath, abPath);
                }
            }

            AssetDatabase.Refresh();
        }

        public static bool CheckScene()
        {
            foreach (var scene in EditorBuildSettings.scenes)
            {
                if (!File.Exists(scene.path))
                {
                    Debug.Log("Scene Not Found" + scene.path);
                    return false;
                }
            }
            return true;
        }

        public static void Build(BuildTarget target, bool apk = false)
        {
            try
            {
                switch (target)
                {
                    case BuildTarget.StandaloneWindows64:
                        {
                            //RenameResourcesDir(false);
                            BuildPipeline.BuildPlayer(BuildUtility.GetBuildScenes(), Application.dataPath + "/../sg/sg.exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
                            //RenameResourcesDir(true);
                        }
                        break;
                    case BuildTarget.iOS:
                        {
                            //BuildUtility.StartBuild();
                            //RenameResourcesDir(false);
                            //BuildUtility.EndBuild("BuildUnity-RenameResourcesDir false ");

                            BuildUtility.StartBuild();
                            PlayerSettings.enableInternalProfiler = true;
                            PlayerSettings.SetIncrementalIl2CppBuild(BuildTargetGroup.iOS, true);// Warning Very Important Don't Edit!!!
                            BuildPipeline.BuildPlayer(BuildUtility.GetBuildScenes(), "proj.ios", BuildTarget.iOS, BuildOptions.Development | BuildOptions.ConnectWithProfiler);
                            BuildUtility.EndBuild("BuildUnity-BuildPlayer ");

                            //BuildUtility.StartBuild();
                            //RenameResourcesDir(true);
                            //BuildUtility.EndBuild("BuildUnity-RenameResourcesDir true ");
                        }
                        break;
                    case BuildTarget.Android:
                        {
                            //RenameResourcesDir(false);
                            if (apk)
                            {
                                string apkPath = Application.dataPath + "/../sg.apk";
                                PlayerSettings.SetIncrementalIl2CppBuild(BuildTargetGroup.Android, true);// Warning Very Important Don't Edit!!!
                                //EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
                                EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Internal;
                                BuildPipeline.BuildPlayer(BuildUtility.GetBuildScenes(), apkPath, BuildTarget.Android, BuildOptions.Development);
                            }
                            else
                            {
                                string outDir = Application.dataPath + "/../sg";
                                if (Directory.Exists(outDir))
                                    Directory.Delete(outDir, true);
                                Directory.CreateDirectory(outDir);
                                PlayerSettings.SetIncrementalIl2CppBuild(BuildTargetGroup.Android, true);// Warning Very Important Don't Edit!!!
                                EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
                                string ret = BuildPipeline.BuildPlayer(BuildUtility.GetBuildScenes(), outDir, BuildTarget.Android, BuildOptions.AcceptExternalModificationsToPlayer | BuildOptions.Development | BuildOptions.ConnectWithProfiler);
                                Debug.LogWarning(ret);
                            }
                            //RenameResourcesDir(true);
                        }
                        break;
                }
            }
            finally
            {
            }
        }
    }
}