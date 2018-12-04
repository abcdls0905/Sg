using Game;
using System;
using System.IO;
using UnityEngine;

namespace GameBuild
{
    public static class DataBuild
    {
        static string SrcDir = Application.dataPath + "/GameData";
        static string DestDir = Application.streamingAssetsPath + "/gamedata";
        static string[] IgnoreExtension = {
            ".meta",
            ".svn",
            ".scene",
            ".lua",
            ".navmesh",
        };

        static string[] BlackList = {
            "navmesh",
            "scenecfg",
            "behavior",
            "efielddpdf.json",
            "mappoint.json"
        };

        // ¿½±´Êý¾Ýµ½StreamingAssets
        public static void Build(UnityEditor.BuildTarget target)
        {
            FileManager.DeleteDirectory(DestDir);
            FileManager.CreateDirectory(DestDir);
            FileManager.CopyDirectory(SrcDir, DestDir, (string path, bool isDirectory) =>{
                if (!CheckPlateform(path, isDirectory, target))
                    return false;
                if (!CheckBlackList(path, isDirectory))
                    return false;
                string ext = FileManager.GetExtension(path);
                return Array.Find(IgnoreExtension, str => str == ext) == null;
            });
        }

        public static bool CheckPlateform(string path, bool isDirectory, UnityEditor.BuildTarget target)
        {
            if (isDirectory)
            {
                var dirName = Path.GetFileNameWithoutExtension(path);
                if (dirName == "Windows")
                {
                    if (target == UnityEditor.BuildTarget.StandaloneWindows64)
                        return true;
                    else
                        return false;
                }
                else if (dirName == "iOS")
                {
                    if (target == UnityEditor.BuildTarget.iOS)
                        return true;
                    else
                        return false;
                }
                else if (dirName == "Android")
                {
                    if (target == UnityEditor.BuildTarget.Android)
                        return true;
                    else
                        return false;
                }
            }
            return true;
        }

        public static bool CheckBlackList(string path, bool isDirectory)
        {
            var fileName = Path.GetFileName(path);
            foreach(var blackName in BlackList)
            {
                if (fileName == blackName)
                    return false;
            }
            return true;
        }
    }
}