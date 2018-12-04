using Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace GameBuild
{
    public static class BuildUtility
    {
        public static void SearchDirectory(string srcPath, string[] extensions, List<string> list)
        {
            foreach (var file in Directory.GetFiles(srcPath, "*.*", SearchOption.AllDirectories).Where(path => extensions == null || extensions.Contains(Path.GetExtension(path))))
                list.Add(file);
        }

        //在这里找出你当前工程所有的场景文件，假设你只想把部分的scene文件打包 那么这里可以写你的条件判断 总之返回一个字符串数组。
        public static string[] GetBuildScenes()
        {
            List<string> names = new List<string>();

            foreach (EditorBuildSettingsScene e in EditorBuildSettings.scenes)
            {
                if (e == null)
                    continue;
                if (!File.Exists(e.path))
                    continue;
                if (e.enabled)
                    names.Add(e.path);
            }
            return names.ToArray();
        }
        public static void ClearConsole()
        {
#if UNITY_2017_1_OR_NEWER
            var logEntries = System.Type.GetType("UnityEditor.LogEntries,UnityEditor.dll");
#else
            var logEntries = System.Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
#endif
            if (logEntries != null)
            {
                var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                clearMethod.Invoke(null, null);
            }
        }

        static void CallBat(string fileName, string arg)
        {
#if UNITY_EDITOR_WIN
            System.Diagnostics.Process proc = null;
            try
            {
                proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = fileName;
                proc.StartInfo.Arguments = arg;
                proc.StartInfo.CreateNoWindow = false;
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
            }
#elif UNITY_EDITOR_OSX
            System.Diagnostics.Process proc = System.Diagnostics.Process.Start("/bin/bash", fileName + " " + arg);
            proc.WaitForExit();
#endif
        }

        public static string ExecuteCmd(string cmd)
        {
            var p = new System.Diagnostics.Process();
#if UNITY_EDITOR_WIN
            p.StartInfo.FileName = "cmd.exe";
#elif UNITY_EDITOR_OSX
            p.StartInfo.FileName = "/bin/bash";
#endif
            //p.StartInfo.Arguments = "/c" + cmd + "&exit";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(936);
            p.StartInfo.StandardErrorEncoding = Encoding.GetEncoding(936);
            System.Console.InputEncoding = Encoding.GetEncoding(936);

            p.Start();//启动程序

            string[] lines = cmd.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach(var line in lines)
            {
                p.StandardInput.WriteLine(line);
            }
            p.StandardInput.WriteLine("exit");
            string output = p.StandardOutput.ReadToEnd() + p.StandardError.ReadToEnd();
            p.WaitForExit();//等待程序执行完退出进程
            p.Close();

            Debug.Log(output);
            return output;
        }

        public static void LogList(List<string> list, string ExtraStr = null)
        {
            string[] array = list.ToArray();
            for (int i = 0; i < array.Length; ++i)
                array[i] += "\n";
            if (ExtraStr == null)
                Debug.Log(string.Concat(array));
            else
                Debug.Log(ExtraStr + string.Concat(array));
        }

        public static void EnableDefine(string name)
        {
            name = name.Trim();

            var buildTypes = System.Enum.GetValues(typeof(BuildTargetGroup)) as int[];

            for (int i = 0; i < buildTypes.Length; i++)
            {
                string defineString = PlayerSettings.GetScriptingDefineSymbolsForGroup((BuildTargetGroup)buildTypes[i]);
                if (defineString == null) continue;

                var defines = defineString.Split(';').Select((s) => s.Trim()).ToList();

                // Already enabled
                if (defines.Contains(name))
                {
                    continue;
                }

                defineString = defineString + ";" + name;
                PlayerSettings.SetScriptingDefineSymbolsForGroup((BuildTargetGroup)buildTypes[i], defineString);
            }
        }

        public static void DisableDefine(string name)
        {
            name = name.Trim();

            var buildTypes = System.Enum.GetValues(typeof(BuildTargetGroup)) as int[];

            for (int i = 0; i < buildTypes.Length; i++)
            {
                string defineString = PlayerSettings.GetScriptingDefineSymbolsForGroup((BuildTargetGroup)buildTypes[i]);

                if (defineString == null) continue;

                var defines = defineString.Split(';').Select((s) => s.Trim()).ToList();

                if (defines.Remove(name))
                {
                    defineString = string.Join(";", defines.Distinct().ToArray());
                    PlayerSettings.SetScriptingDefineSymbolsForGroup((BuildTargetGroup)buildTypes[i], defineString);
                }
            }
        }

        public static List<string> GetDefines(BuildTargetGroup group)
        {
            string defineString = PlayerSettings.GetScriptingDefineSymbolsForGroup(group);
            if (defineString == null)
                return new List<string>();
            return defineString.Split(';').Select((s) => s.Trim()).ToList();
        }

        public static long startTime = 0;
        public static void StartBuild(string message=null)
        {
            startTime = DateTime.Now.Ticks;

            if (!string.IsNullOrEmpty(message))
                Debug.Log(message);
        }

        public static void EndBuild(string message)
        {
            long endTime = DateTime.Now.Ticks;
            Debug.Log(message + " " + (float)(endTime - startTime)/10000000);
        }
    }
}