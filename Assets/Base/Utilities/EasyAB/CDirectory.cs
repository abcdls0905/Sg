using System;
using System.Text;
using System.IO;
using UnityEngine;

public static class CDirectory {
    public const string AssetBundlesOutputPath = "assetbundles";
    private static string streaming_patch;
    private static string cache_path;
    public static bool EnablePersistentPath = false;
    public static string AssetBundlesUpdatePath = Application.persistentDataPath + "/assets/" + AssetBundlesOutputPath;
    public static string AssetBundlesStreamPath = Application.streamingAssetsPath + "/" + AssetBundlesOutputPath;
#if USEWWW
    public static string AssetBundlesStreamPathAndroid = Application.streamingAssetsPath + "/" + AssetBundlesOutputPath;
#else
    public static string AssetBundlesStreamPathAndroid = Application.dataPath + "!assets/" + AssetBundlesOutputPath;
#endif
    static CDirectory() {
        Init();
    }

    public static void Init() {
        streaming_patch = Application.streamingAssetsPath + "/";
        switch ( Application.platform ) {
        case RuntimePlatform.Android:
            cache_path = Application.persistentDataPath + "/";
            break;
        case RuntimePlatform.IPhonePlayer:
            cache_path = Application.persistentDataPath + "/";
            break;
        case RuntimePlatform.OSXEditor:
        case RuntimePlatform.WindowsEditor:
            cache_path = string.Format( "{0}/../main.dir", Application.dataPath );
            if ( !Directory.Exists( cache_path ) )
                Directory.CreateDirectory( cache_path );
            cache_path = cache_path + "/";
            break;
        case RuntimePlatform.WindowsPlayer:
            cache_path = string.Format( "{0}/main.dir", Application.dataPath );
            if ( !Directory.Exists( cache_path ) )
                Directory.CreateDirectory( cache_path );
            cache_path = cache_path + "/";
            break;
        }
    }

    /// <summary>
    /// 返回文件路劲（不支持www）
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public static string MakeFilePath(string filename)
    {
        if (File.Exists(MakeCachePath(filename)))
            return MakeCachePath(filename);
        else
            return MakeStreamingPath(filename);
    }

    public static string GetAssetBundlePathByAB(string assetBundleName)
    {
        if (assetBundleName == null)
            return null;
        string ret = assetBundleName;
        ret = string.Format("{0}/{1}", AssetBundlesUpdatePath, assetBundleName);
#if !UNITY_ANDROID

        if (!File.Exists(ret))
        {
            ret = string.Format("{0}/{1}",AssetBundlesStreamPath,assetBundleName);
        }
#else
         if (!File.Exists(ret))
        {
             ret = string.Format("{0}/{1}",AssetBundlesStreamPathAndroid,assetBundleName);
        }
#endif
        return ret;
    }


    /// <summary>
    /// 返回文件路劲(只用于www)
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public static string MakeFullWWWPath(string filename)
    {
        if (File.Exists(MakeCachePath(filename)))
        {
            return string.Format("file://{0}{1}", cache_path, filename);
        }
        else
        {
            return MakeWWWStreamPath(filename);
        }
    }

    public static string MakeWWWStreamPath(string filename)
    {
        switch (Application.platform)
        {
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.IPhonePlayer:
                return string.Format("{0}{1}", "file://", MakeStreamingPath(filename));
            case RuntimePlatform.Android:
                return MakeStreamingPath(filename);
            default:
                return string.Format("{0}{1}", "file:///", MakeStreamingPath(filename));
        }
    }

    public static string MakeCachePath(string filename)
    {
        return string.Format("{0}{1}",cache_path , filename);
    } 

    public static string MakeStreamingPath(string filename)
    {
        return string.Format("{0}{1}", streaming_patch, filename);
    }


    public static string MakeOtherWWWStreamingPath(string filename) {
        if (Application.platform == RuntimePlatform.Android)
            return string.Format("{0}{1}{2}", Application.dataPath, "!assets/", filename);
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
            return string.Format("{0}{1}{2}", Application.dataPath, "/Raw/", filename);
        else
            return string.Format("{0}{1}", streaming_patch, filename);
    }

    public static string AppendDirectoryChar( string dir ) {
        if ( dir == null || dir.Length == 0 ) {
            return string.Empty;
        } else if ( dir[dir.Length - 1] != '\\' ) {
            return dir + '\\';
        } else {
            return dir;
        }
    }

    public static string Standardize( string path ) {
        if ( string.IsNullOrEmpty( path ) ) return null;
        return path.Replace( '\\', '/' );
    }

    public static string res_path {
        get {
            return cache_path + "res/";
        }
    }


    public static string CachePath { get { return cache_path; } }

    internal static void ClearCachePath( string dir ) {
        dir = cache_path + dir;
        if ( !Directory.Exists( dir ) )
            return;
        ClearFolder( dir );
    }

    internal static void ClearFolder( string dir ) {
        foreach ( string d in Directory.GetFileSystemEntries( dir ) ) {
            if ( File.Exists( d ) ) {
                FileInfo fi = new FileInfo( d );
                if ( fi.Attributes.ToString().IndexOf( "ReadOnly" ) != -1 )
                    fi.Attributes = FileAttributes.Normal;
                try {
                    File.Delete( d );
                } catch { }
            } else {
                DirectoryInfo dl = new DirectoryInfo( d );
                if ( dl.GetFiles().Length != 0 )
                    ClearFolder( dl.FullName );
                //Directory.Delete( d );
            }
        }
    }
}