using System;
using System.Linq;
using Entitas.CodeGeneration.CodeGenerator;
using Entitas.Utils;
using UnityEditor;
using UnityEngine;
using System.IO;

namespace Entitas.CodeGeneration.Unity.Editor {

    public static class UnityCodeGenerator {
        [MenuItem("Tools/Entitas/Generate #%g", false, 100)]
        public static void Generate() {
            if (!CheckCanGenerate())
                return;

            Debug.Log("Generating...");

            var codeGenerator = CodeGeneratorUtil.CodeGeneratorFromProperties();

            var progressOffset = 0f;

            codeGenerator.OnProgress += (title, info, progress) => {
                var cancel = EditorUtility.DisplayCancelableProgressBar(title, info, progressOffset + progress / 2);
                if (cancel) {
                    codeGenerator.Cancel();
                }
            };

            CodeGenFile[] dryFiles = null;
            CodeGenFile[] files = null;

            try {
                dryFiles = codeGenerator.DryRun();
                progressOffset = 0.5f;
                files = codeGenerator.Generate();
            } catch(Exception ex) {
                dryFiles = new CodeGenFile[0];
                files = new CodeGenFile[0];

                EditorUtility.DisplayDialog("Error", ex.Message, "Ok");
            }

            EditorUtility.ClearProgressBar();

            var totalGeneratedFiles = files.Select(file => file.fileName).Distinct().Count();

            var sloc = dryFiles
                .Select(file => file.fileContent.ToUnixLineEndings())
                .Sum(content => content.Split(new [] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Length);

            var loc = files
                .Select(file => file.fileContent.ToUnixLineEndings())
                .Sum(content => content.Split(new [] { '\n' }).Length);

            Debug.Log("Generated " + totalGeneratedFiles + " files (" + sloc + " sloc, " + loc + " loc)");

            AssetDatabase.Refresh();
        }

        static bool CheckCanGenerate() {
            if (EditorApplication.isCompiling) {
                UnityEditor.EditorUtility.DisplayDialog("Generate Error", "Cannot generate because Unity is still compiling. Please wait...", "Ok");
                //throw new Exception("Cannot generate because Unity is still compiling. Please wait...");
                return false;
            }

            var assembly = typeof(UnityEditor.Editor).Assembly;

            var logEntries = assembly.GetType("UnityEditorInternal.LogEntries")
                          ?? assembly.GetType("UnityEditor.LogEntries");

            logEntries.GetMethod("Clear").Invoke(new object(), null);
            var canCompile = (int)logEntries.GetMethod("GetCount").Invoke(new object(), null) == 0;
            if (!canCompile) {
                UnityEditor.EditorUtility.DisplayDialog("Generate Error", "There are compile errors! Generated code will be based on last compiled executable.", "Ok");
                //Debug.Log("There are compile errors! Generated code will be based on last compiled executable.");
                return false;
            }
            return true;
        }
    }
}
