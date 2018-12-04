using System.IO;
using UnityEngine;
using GameUtil;
/*
 * author:赖松林 3198
 * 
 * 和CDirectory相比，简化了一下接口
 * 在WWW接口模式下和AssetBundle接口模式下，路径不同
 * 安卓的路径和其他平台的路径不同，主要是因为如果地址是jar包内地址，就不是一个物理地址，需要单独处理
 * WWW：
 * 安卓下的streamingasset 使用Application.streamingAssetsPath
 * 其他平台需要加file://
 * 缓存目录都需要加file://
 * AssetBundle：
 * 安卓下的streamingasset 使用Application.dataPath + "!assets/";
 * 其他平台使用Application.streamingAssetsPath
 * 缓存目录不需要单独处理
 * 
 * 主要使用MakeFilePath(string fileName,bool useWWW = false)
 * 优先返回缓存目录的地址，如果没有，再返回StreamingAsset地址(热更新需要)
 * 提供useWWW参数，目的是在方便在安卓下使用www加载StreamingAsset下的文本信息
 * 
 * 对于需要添加assetbundles前缀的，使用MakeABPrefix接口
 * 
 * 提供Exist接口，类似于File.Exist
 * 因为安卓下的streamingasset地址不是一个真实的物理地址，System.IO无法适用SteamingAsset下的文件，使用java接口来判断
 * 
 * 其他的接口如果没有明确的目的，请不要使用！！！！
 */
public static class PathUtil {

    public const string PrefixAB = "assetbundles/";

    static string streamingPath,cachePath,wwwStreamingPath,wwwCachePath;

    static PathUtil() {
#if USEWWW
        if(Application.platform == RuntimePlatform.Android)
            streamingPath = Application.streamingAssetsPath+"/";
        else
            streamingPath = "file://"+Application.streamingAssetsPath + "/";
#else
        if (Application.platform == RuntimePlatform.Android)
            streamingPath = Application.dataPath + "!assets/";
        else
            streamingPath = Application.streamingAssetsPath+"/";
#endif
        if (Application.platform == RuntimePlatform.Android)
            wwwStreamingPath = Application.streamingAssetsPath + "/";
        else
            wwwStreamingPath = "file://" + Application.streamingAssetsPath + "/";

        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                cachePath = Application.persistentDataPath + "/";
                wwwCachePath = "file://" + cachePath;
                break;
            case RuntimePlatform.IPhonePlayer:
                cachePath = Application.persistentDataPath + "/";
                wwwCachePath = "file://" + cachePath;
                break;
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.WindowsEditor:
                cachePath = string.Format("{0}/../main.dir", Application.dataPath);
                if (!Directory.Exists(cachePath))
                    Directory.CreateDirectory(cachePath);
                cachePath = cachePath + "/";
                wwwCachePath = "file://" + cachePath;
                break;
            case RuntimePlatform.WindowsPlayer:
                cachePath = string.Format("{0}/main.dir", Application.dataPath);
                if (!Directory.Exists(cachePath))
                    Directory.CreateDirectory(cachePath);
                cachePath = cachePath + "/";
                wwwCachePath = "file://" + cachePath;
                break;
        }
    }

    public static bool Exist(string path)
    {
#if UNITY_ANDROID
        if (File.Exists(path))
            return true;
        return NativeHelp.IsStreamingAssetsExists(path);
#else
        return File.Exists(path);
#endif
    }

    public static string MakeABPrefix(string fileName) {
        return string.Concat(PrefixAB, fileName.ToLower());
    }

    public static string MakeFilePath(string fileName,bool useWWW = false) {
        string cachePath = MakeCachePath(fileName);
        if (File.Exists(cachePath))
        {
            if (useWWW)
                return MakeWWWCachePath(fileName);
            return cachePath;
        }
        else if (useWWW)
            return MakeWWWStreamingPath(fileName);
        else
            return MakeStreamingPath(fileName);
    }

    public static string MakeWWWCachePath(string fileName) {
        return string.Concat(wwwCachePath, fileName);
    }

    public static string MakeWWWStreamingPath(string fileName) {
        return string.Concat(wwwStreamingPath, fileName);
    }

    public static string MakeCachePath(string fileName) {
        return string.Concat(cachePath, fileName);
    }

    public static string MakeStreamingPath(string fileName) {
        return string.Concat(streamingPath, fileName);
    }

    public static string Standardize(string path)
    {
        if (string.IsNullOrEmpty(path)) return null;
        return path.Replace('\\', '/');
    }
}
