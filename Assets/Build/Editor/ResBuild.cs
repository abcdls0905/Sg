using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using Game;

namespace GameBuild
{
    public static class ResBuild
    {
        // 临时变量
        static string[] ResourcesDir;
        static string AssetBundleOutputPath;
        public static void Build(BuildTarget target)
        {
            ResourcesDir = GameEnvSetting.AssetBundleDir;
            AssetBundleOutputPath = GameEnvSetting.AssetBundleOutputPath;
            //CheckResourceName();
            //BuildBundle(target, true);
            BuildAssetBundles(target, true);
            BuildFileMd5();
        }

        static bool CheckName(string fileName)
        {
            foreach (char c in fileName)
            {
                if (!(c == '!' || c == '@' || c == '_' || c == '.' || c == '-' || c == '(' || c == ')' || (c <= '9' && c >= '0') || (c <= 'z' && c >= 'A')))
                {
                    return false;
                }
            }
            return true;
        }

        static bool CheckResourceName()
        {
            List<string> invalidNames = new List<string>();

            foreach (var dir in ResourcesDir)
            {
                string assetBasePath = Path.Combine(Application.dataPath, dir);
                string[] files = Directory.GetFiles(assetBasePath, "*.*", SearchOption.AllDirectories);
                int fileCount = files.Length;

                for (int fileIndex = 0; fileIndex < fileCount; ++fileIndex)
                {
                    string fileName = Path.GetFileNameWithoutExtension(files[fileIndex]);
                    if (!CheckName(fileName))
                    {
                        invalidNames.Add(files[fileIndex]);
                    }
                }
            }

            foreach (string invalidName in invalidNames)
            {
                Debug.Log(invalidName);
            }

            if (invalidNames.Count > 0)
            {
                Debug.LogError("Exist Invalid File Name!");
                return false;
            }

            Debug.Log("Check OK!");
            return true;
        }

        static string GetBundleName(string name)
        {
            foreach (var info in ResourcesDir)
            {
                string resDir = "Assets/" + info;
                if (name.StartsWith(resDir, System.StringComparison.OrdinalIgnoreCase))
                {
                    name = name.Substring(resDir.Length);
                    int atIdx = name.LastIndexOf('@');
                    if (atIdx >= 0)
                    {
                        name = name.Remove(atIdx);
                    }
                    break;
                }
            }

            // bundleName必须是相对路径，不能以分隔符起始
            if (name.StartsWith("/"))
            {
                name = name.Substring(1);
            }
            // 去掉扩展名
            // 为了保持Resources和AssetBundle的路径完全一致
            // 因为AssetBundle读取时会根据路径判断文件是否存在
            int exIdx = name.LastIndexOf('.');
            if (exIdx >= 0)
            {
                name = name.Remove(exIdx);
            }
            return name;
        }

        public static bool IsFileOfType(string filepath, string typeExtenstion)
        {
            return !string.IsNullOrEmpty(filepath) && filepath.ToLower().EndsWith(typeExtenstion.ToLower());
        }

        public static bool IsFileInAPath(string filepath, string pathToCheck)
        {
            return !string.IsNullOrEmpty(filepath) && filepath.ToLower().IndexOf(pathToCheck.ToLower()) != -1;
        }

        public static bool IsFileName(string filepath, string filenameToCheck)
        {
            return string.Equals(Path.GetFileName(filepath), filenameToCheck, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsUselessFile(string filepath)
        {
            return IsFileName(filepath, "Thumbs.db") || IsFileName(filepath, ".DS_Store") || IsFileName(filepath, "._.DS_Store");
        }
        public static bool IsFileNotReport(string filepath)
        {
            return IsFileInAPath(filepath, "/.svn/") || IsFileOfType(filepath, ".meta") || IsUselessFile(filepath);
        }

        static bool FilterFile(string name)
        {
            // 需要忽略 *.meta
            if (IsFileNotReport(name))
                return true;
            if (name.EndsWith(".cs"))
                return true;
            return false;
        }

        public static string GetRelativeAssetsPath(string path)
        {
            return "Assets" + path.Replace(Application.dataPath, "").Replace('\\', '/');
        }

        static void AddRes(string dir, List<string> resAssetList)
        {
            string resPath = Path.Combine(Application.dataPath, dir);

            string[] files = Directory.GetFiles(resPath, "*.*", SearchOption.AllDirectories);
            int fileCount = files.Length;
            for (int fileIndex = 0; fileIndex < fileCount; ++fileIndex)
            {
                string fullAssetPath = files[fileIndex];
                string assetPath = GetRelativeAssetsPath(fullAssetPath);

                if (FilterFile(assetPath))
                    continue;
                resAssetList.Add(assetPath);
            }
        }

        static void CleanSurplusBundles(string bundlePath)
        {
            if (!Directory.Exists(bundlePath))
                return;

            string mainAssetBundlePath = Path.GetFullPath(bundlePath + "/assetbundles");
            AssetBundle mainAssetBundle = AssetBundle.LoadFromFile(mainAssetBundlePath);
            if (mainAssetBundle == null)
                return;

            AssetBundleManifest mainAssetManifest = (AssetBundleManifest)mainAssetBundle.LoadAsset("assetbundlemanifest");
            if (mainAssetManifest == null)
                return;

            Dictionary<string, bool> allBundles = new Dictionary<string, bool>();
            allBundles[mainAssetBundlePath] = true;
            allBundles[mainAssetBundlePath + ".manifest"] = true;

            foreach (string assetName in mainAssetManifest.GetAllAssetBundles())
            {
                string fullPathName = Path.GetFullPath(Path.Combine(bundlePath, assetName));
                allBundles[fullPathName] = true;
                allBundles[fullPathName + ".manifest"] = true;
            }

            string[] files = Directory.GetFiles(bundlePath, "*.*", SearchOption.AllDirectories);
            for (int fileIndex = 0; fileIndex < files.Length; ++fileIndex)
            {
                string filePath = Path.GetFullPath(files[fileIndex]);

                if (!allBundles.ContainsKey(filePath))
                {
                    File.Delete(filePath);
                }
            }

            mainAssetBundle.Unload(true);
        }

        public static void DepAddRef(string name, ref Dictionary<string, int> ResAssetDic, bool root)
        {
            bool needDeep = true;
            if (!ResAssetDic.ContainsKey(name))
                ResAssetDic.Add(name, root ? 1 : 0);
            else
                needDeep = false;
            ResAssetDic[name]++;

            if (!needDeep)
                return;
            var deps = AssetDatabase.GetDependencies(name, false);
            foreach (var dep in deps)
            {
                DepAddRef(dep, ref ResAssetDic, false);
            }
        }

        private static string[] filterFiles = new string[]{
            ".meta",
            ".cs",
        };
        public static List<string> GetAllResFile(string fullPath)
        {
            List<string> dirList = new List<string>();
            string[] files = Directory.GetFiles(fullPath, "*.*", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                if (IsFilterFile(file))
                    continue;
                file = "Assets" + file.Replace(UnityEngine.Application.dataPath, "");
                file = file.Replace('\\', '/');
                dirList.Add(file);
            }
            return dirList;
        }

        static bool IsFilterFile(string file)
        {
            string suffix = Path.GetExtension(file);
            for (int i = 0; i < filterFiles.Length; i++)
            {
                if (suffix == filterFiles[i])
                    return true;
            }
            return false;
        }

        static List<string> GetDependences(List<string> rootList)
        {
            List<string> dependences = new List<string>();
            for (int i = 0; i < rootList.Count; i++)
            {
                string root = rootList[i];
                string[] deps = AssetDatabase.GetDependencies(root, true);
                for (int j = 0; j < deps.Length; j++)
                {
                    string dep = deps[j];
                    if (!dependences.Contains(dep) && !rootList.Contains(dep) && !IsFilterFile(dep))
                        dependences.Add(dep);
                }
            }
            return dependences;
        }

        static List<AssetReferenceNode> GenerateReferenceNodes(List<string> rootList, List<string> dependences)
        {
            var allPathSet = new HashSet<string>();
            foreach (var path in rootList)
            {
                allPathSet.Add(path);
            }
            foreach (var path in dependences)
            {
                allPathSet.Add(path);
            }
            //构造依赖关系图
            var map = new Dictionary<string, AssetReferenceNode>();

            foreach (var path in allPathSet)
            {
                map[path] = new AssetReferenceNode() { AssetPath = path };
            }
            foreach (var pair in map)
            {
                var path = pair.Key;
                var curNode = pair.Value;
                var deps = AssetDatabase.GetDependencies(path, true);

                foreach (var depPath in deps)
                {
                    if (map.ContainsKey(depPath) && path != depPath)
                    {
                        curNode.depence.Add(depPath);
                        map[depPath].depenceOnMe.Add(path);
                    }
                }
            }

            var rootQueue = new Queue<string>();
            List<string> toRemove = new List<string>();
            HashSet<string> depSet = new HashSet<string>();
            foreach (var path in rootList)
            {
                rootQueue.Enqueue(path);
            }
            while (rootQueue.Count > 0)
            {
                depSet.Clear();
                toRemove.Clear();
                var path = rootQueue.Dequeue();
                var node = map[path];
                foreach (var depPath in node.depence)
                {
                    depSet.Add(depPath);
                }
                foreach (var depPath in node.depence)
                {
                    var depNode = map[depPath];
                    foreach (var onMepath in depNode.depenceOnMe)
                    {
                        if (depSet.Contains(onMepath))
                            toRemove.Add(depPath);
                    }
                }
                foreach (var depPath in toRemove)
                {
                    node.depence.Remove(depPath);
                    var depNode = map[depPath];
                    depNode.depenceOnMe.Remove(path);
                }
                foreach (var depPath in node.depence)
                {
                    if (!rootQueue.Contains(depPath))
                        rootQueue.Enqueue(depPath);
                }
            }

            HashSet<string> hasSearchSet = new HashSet<string>();
            List<AssetReferenceNode> referenceNodes = new List<AssetReferenceNode>();
            foreach (var path in rootList)
            {
                rootQueue.Enqueue(path);
            }
            List<string> removed = new List<string>();
            while (rootQueue.Count > 0)
            {
                var nodePath = rootQueue.Dequeue();
                var node = map[nodePath];
                hasSearchSet.Add(nodePath);
                var depQueue = new Queue<string>(node.depence);
                while (depQueue.Count > 0)
                {
                    var depNodePath = depQueue.Dequeue();
                    if (rootList.Contains(depNodePath))
                        continue;
                    var depNode = map[depNodePath];
                    if (depNode.depenceOnMe.Count == 1)
                    {
                        node.depence.Remove(depNodePath);
                        map.Remove(depNodePath);
                        removed.Add(depNodePath);
                        node.buildAssets.Add(depNodePath);
                        foreach (var dep2Path in depNode.depence)
                        {
                            if (!node.depence.Contains(dep2Path))
                            {
                                node.depence.Add(dep2Path);
                                depQueue.Enqueue(dep2Path);
                            }
                            var dep2Node = map[dep2Path];
                            dep2Node.depenceOnMe.Remove(depNodePath);
                            if (!dep2Node.depenceOnMe.Contains(nodePath))
                                dep2Node.depenceOnMe.Add(nodePath);
                        }
                    }
                    else if (depNode.depenceOnMe.Count > 1)
                    {
                        if (!rootQueue.Contains(depNodePath) && !hasSearchSet.Contains(depNodePath))
                            rootQueue.Enqueue(depNodePath);
                    }
                }
                referenceNodes.Add(node);
            }
            return referenceNodes;
        }

        static AssetBundleBuild[] ConvertToBundleBulidList(List<AssetReferenceNode> nodeList)
        {
            var list = new List<AssetBundleBuild>();
            foreach (var node in nodeList)
            {
                var path = node.AssetPath;
                var bundleName = GetBundleName(path);
                List<string> assetNames = node.buildAssets;
                assetNames.Add(path);
                list.Add(new AssetBundleBuild() { assetNames = assetNames.ToArray(), assetBundleName = bundleName });
            }
            return list.ToArray();
        }
        public static void BuildBundle(BuildTarget platform, bool compress = false)
        {
            if (!Directory.Exists(AssetBundleOutputPath))
            {
                Directory.CreateDirectory(AssetBundleOutputPath);
            }

            List<string> rootFiles = new List<string>();
            for (int i = 0; i < ResourcesDir.Length; i++)
                AddRes(ResourcesDir[i], rootFiles);
            EditorUtility.DisplayProgressBar("BuildAssetBundles...", "AddRes", 0.1f);

            List<string> dependences = GetDependences(rootFiles);
            EditorUtility.DisplayProgressBar("BuildAssetBundles...", "DepAddRef", 0.2f);

            List<AssetReferenceNode> referencesNodes = GenerateReferenceNodes(rootFiles, dependences);
            EditorUtility.DisplayProgressBar("BuildAssetBundles...", "DepAddRef", 0.5f);

            AssetBundleBuild[] builds = ConvertToBundleBulidList(referencesNodes);

            Debug.Log("build count:" + builds.Length);
            BuildAssetBundleOptions compressOpt = compress ? BuildAssetBundleOptions.ChunkBasedCompression : BuildAssetBundleOptions.UncompressedAssetBundle;
            BuildPipeline.BuildAssetBundles(AssetBundleOutputPath, builds,
                compressOpt | BuildAssetBundleOptions.DeterministicAssetBundle | BuildAssetBundleOptions.DisableWriteTypeTree,
                platform);
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }

        public static void BuildAssetBundles(BuildTarget buildTarget, bool compress = false)
        {
            if (!Directory.Exists(AssetBundleOutputPath))
            {
                Directory.CreateDirectory(AssetBundleOutputPath);
            }

            List<string> ResAssetList = new List<string>();
            Dictionary<string, int> ResAssetDic = new Dictionary<string, int>();

            // 获取Resources目录的资源信息
            for (int i = 0; i < ResourcesDir.Length; i++)
                AddRes(ResourcesDir[i], ResAssetList);
            EditorUtility.DisplayProgressBar("BuildAssetBundles...", "AddRes", 0.1f);

            // 获取依赖信息
            foreach (var ResAsset in ResAssetList)
                DepAddRef(ResAsset, ref ResAssetDic, true);
            EditorUtility.DisplayProgressBar("BuildAssetBundles...", "DepAddRef", 0.3f);

            List<AssetBundleBuild> builds = new List<AssetBundleBuild>();
            // 加入Build列表
            foreach (var iter in ResAssetDic)
            {
                if (FilterFile(iter.Key))
                    continue;
                // 打包策略，如果多资源引用，就单独打包
                // 后期可以综合考虑 size * count 作为阈值
                if (iter.Value > 1)
                {
                    AssetBundleBuild build = new AssetBundleBuild();
                    build.assetBundleName = GetBundleName(iter.Key);
                    //build.assetBundleVariant = "";
                    build.assetNames = new string[] { iter.Key };
                    builds.Add(build);
                }
                else
                {
                    //Debug.Log("Single Ref " + assetInfo.Name);
                }
            }
            Debug.Log("build count:" + builds.Count);
            EditorUtility.DisplayProgressBar("BuildAssetBundles...", "BuildAssetBundles", 0.5f);

            BuildAssetBundleOptions compressOpt = compress ? BuildAssetBundleOptions.ChunkBasedCompression : BuildAssetBundleOptions.UncompressedAssetBundle;
            BuildPipeline.BuildAssetBundles(AssetBundleOutputPath, builds.ToArray(),
                compressOpt | BuildAssetBundleOptions.DeterministicAssetBundle | BuildAssetBundleOptions.DisableWriteTypeTree,
                buildTarget);
            EditorUtility.ClearProgressBar();
            CleanSurplusBundles(AssetBundleOutputPath);
        }

        public static void BuildFileMd5()
        {
            string[] names = Directory.GetFiles(AssetBundleOutputPath, "*.*", SearchOption.AllDirectories);
            ///----------------------创建文件列表-----------------------
            string lastListFilePath = Application.streamingAssetsPath + "/lastfiles.txt";
            if (File.Exists(lastListFilePath)) File.Delete(lastListFilePath);

            string listFilePath = Application.streamingAssetsPath + "/files.txt";
            if (File.Exists(listFilePath))
            {
                File.Copy(listFilePath, lastListFilePath, true);
                File.Delete(listFilePath);
            }

            FileStream fs = new FileStream(listFilePath, FileMode.CreateNew);
            StreamWriter sw = new StreamWriter(fs);
            foreach (string filename in names)
            {
                if (filename.EndsWith(".meta") || filename.Contains(".DS_Store")) continue;
                string fixname = filename.Replace('\\', '/');
                string md5 = CalcFileMd5(fixname);
                sw.WriteLine(fixname.Replace(AssetBundleOutputPath, "") + "|" + md5);
            }

            sw.Close(); fs.Close();
            Debug.Log("BuildFileIndex Finish!");

            GenDiffList();
        }

        private static string CalcFileMd5(string filename)
        {
            try
            {
                FileStream fs = new FileStream(filename, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(fs);
                fs.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("md5file() fail, error:" + ex.Message);
            }
        }

        private static void GenDiffList()
        {
            string lastListFilePath = Application.streamingAssetsPath + "/lastfiles.txt";
            string listFilePath = Application.streamingAssetsPath + "/files.txt";

            if (!File.Exists(lastListFilePath) || !File.Exists(lastListFilePath)) return;

            string diffFilePath = Application.streamingAssetsPath + "/filediff.txt";
            if (File.Exists(diffFilePath)) File.Delete(diffFilePath);
            FileStream fs = new FileStream(diffFilePath, FileMode.CreateNew);
            StreamWriter sw = new StreamWriter(fs);

            Dictionary<string, string> oldMd5Dic = new Dictionary<string, string>();

            foreach (var line in File.ReadAllLines(lastListFilePath))
            {
                string[] ls = line.Split('|');
                if (ls.Length < 2)
                    continue;

                if (!oldMd5Dic.ContainsKey(ls[0]))
                    oldMd5Dic.Add(ls[0], ls[1].Trim());
            }

            foreach (var line in File.ReadAllLines(listFilePath))
            {
                string[] ls = line.Split('|');
                if (ls.Length < 2)
                    continue;

                string file = ls[0];
                if (oldMd5Dic.ContainsKey(file))
                {
                    string newMd5 = ls[1].Trim();
                    if (!oldMd5Dic[file].Equals(newMd5))
                    {
                        sw.WriteLine("M|" + file);

                    }
                    oldMd5Dic.Remove(file);
                }
                else
                {
                    sw.WriteLine("A|" + file);
                }
            }

            foreach (string file in oldMd5Dic.Keys)
            {
                sw.WriteLine("D|" + file);
            }

            sw.Close(); fs.Close();
            Debug.Log("Gen MD5 Diff Finish!");
        }
    }
}