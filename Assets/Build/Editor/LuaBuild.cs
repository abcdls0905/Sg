using CSObjectWrapEditor;
using Game;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameBuild
{
    public static class LuaBuild
    {
#if UNITY_EDITOR_WIN
        // 拷贝
        static string SrcDir = Application.dataPath + "/StreamingAssets/luabinfiles";
        static string DestDir = Application.dataPath + "/../Output/thg_Data/StreamingAssets/luabinfiles";
#elif UNITY_EDITOR_OSX
        static string SrcDir = Application.dataPath + "/StreamingAssets/luabinfiles";
        static string DestDir = Application.dataPath + "/../../Entitas_iOS/Data/Raw/luabinfiles";
#endif

        static string LuaLogPath = Application.dataPath + "/../lua.log";
        static string[] IgnoreExtension = {
            ".meta",
        };

#if UNITY_EDITOR_WIN
        const string lua_bin_template =
@"
.\Tools\encryptexe.exe -en-source ${SEARCHPATH}/ .\Assets\StreamingAssets\luabinfiles\${BINPATH}
";
#elif UNITY_EDITOR_OSX
        const string lua_bin_template =
@"
chmod 777 Tools/encryptexe
Tools/encryptexe -en-source ${SEARCHPATH}/ Assets/StreamingAssets/luabinfiles/${BINPATH}
";
#endif

        public static void Build()
        {
            FileManager.DeleteDirectory(SrcDir);
            FileManager.CreateDirectory(SrcDir);
            string cmd = "";
            foreach(var path in GameLua.LuaManager.LuaPath)
            {
                cmd += lua_bin_template
                    .Replace("${SEARCHPATH}", Path.Combine("./Assets", path))
                    .Replace("${BINPATH}", path.ToLower() + ".bin");
            }
            var output = BuildUtility.ExecuteCmd(cmd);
            if (output.IndexOf("FAIL") >= 0)
                Debug.LogError("Build Lua Bin Error!");
        }

        public static string BackupPath = Path.GetFullPath(Path.Combine(Application.dataPath, "../CSWrapBackup"));
        static void RenameDir(string oriName, string dstName, bool isAssets)
        {
            if (!Directory.Exists(oriName))
                return;
            if (Directory.Exists(dstName))
                Directory.Delete(dstName, true);
            Directory.Move(oriName, dstName);
            AssetDatabase.DeleteAsset(oriName.Substring(oriName.IndexOf("Assets") + "Assets".Length));
            AssetDatabase.Refresh();
        }

        public static void RollBackCSWrap()
        {
            //RenameDir(BackupPath, GeneratorConfig.common_path, false);
        }

        public static void BuildCSWrap()
        {
//             RenameDir(GeneratorConfig.common_path, BackupPath, true);
//             //重新生成wrap
//             CSObjectWrapEditor.Generator.ClearAll();
//             //UnityEditor.ImportAssetOptions.ForceSynchronousImport
//             UnityEditor.AssetDatabase.Refresh(UnityEditor.ImportAssetOptions.ForceSynchronousImport);
//             ExampleGenConfig.isReGenerateWrap = true;
//             CSObjectWrapEditor.Generator.GenAll();
//             ExampleGenConfig.isReGenerateWrap = false;
//             UnityEditor.AssetDatabase.Refresh(UnityEditor.ImportAssetOptions.ForceSynchronousImport);
        }

        public static void CopyLuaFile()
        {
            FileManager.DeleteFile(LuaLogPath);
            FileManager.DeleteDirectory(DestDir);
            FileManager.CopyDirectory(SrcDir, DestDir, (string path, bool isDirectory) =>
            {
                string ext = FileManager.GetExtension(path);
                return Array.Find(IgnoreExtension, str => str == ext) == null;
            });
        }
    }
}