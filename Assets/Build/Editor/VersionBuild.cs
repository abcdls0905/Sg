using Game;
using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace GameBuild
{
    public static class VersionBuild
    {
        public static string versionFilePath = Path.Combine(Application.streamingAssetsPath, "version.txt");
        public static void Build()
        {
            FileManager.DeleteFile(versionFilePath);
            var ret = BuildUtility.ExecuteCmd("svn info");
            string versionStr = "100000";
#if UNITY_EDITOR_WIN
            string[] lines = ret.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
#elif UNITY_EDITOR_OSX
            string[] lines = ret.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
#endif
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("Revision"))
                {
                    string[] sArr = lines[i].Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                    versionStr = sArr[1];
                    break;
                }
            }
            string dataVersion = versionStr;
            File.WriteAllText(versionFilePath, dataVersion, Encoding.ASCII);
        }
    }
}