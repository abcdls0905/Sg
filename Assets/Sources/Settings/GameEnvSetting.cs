using System.IO;
using System.Text;
using UnityEngine;
using XLua;
using GameUtil;
//using UnityEngine.Experimental.Rendering;

namespace Game
{
    [LuaCallCSharp]
    public static class GameEnvSetting
    {
        public static bool EnablePersistentPath;
        public static bool EnableAssetBundle;
        public static bool EnableStreamerPool = true;
        public static string[] AssetBundleDir =
        {
            "ArtRes/Resources_/",
        };
        public static string basePath = Application.dataPath + "/../StreamingAssets";
        public static string AssetBundleOutputPath = Application.streamingAssetsPath + "/assetbundles";
        public const string AssetBundleReadPath = "assetbundles";
        public static int ResolutionX = 1334;
        public static int ResolutionY = 750;
        public static string dataPath = Application.dataPath + "/GameData";
        public static string dataUpdatePath = Application.persistentDataPath + "/assets/gamedata";
        public static string dataStreamPath = Application.streamingAssetsPath + "/gamedata";

        public static string protoPath = Application.dataPath + "/Protobuf";
        public static string protoUpdatePath = Application.persistentDataPath + "/assets/protobuf";
        public static string protoStreamPath = Application.streamingAssetsPath + "/protobuf";

        public static string rootUpdatePath = Application.persistentDataPath + "/assets/";
        public static string rootStreamPath = Application.streamingAssetsPath;
        public static bool UseLigthingMap = false;
        public static bool RealTimeLighting = false;
        public static SGameRenderQuality DeviceLevel = SGameRenderQuality.Medium;
        public static SGameRenderQuality nowLevel = SGameRenderQuality.Medium;
        public static int frameRate = 20;
        public static float frameTime = 0.05f;
        public static bool UseMultiThread = false;
        public static bool UseShadowThread = false;
        public static bool UseShadowLerp = true;
        public static bool debugShowLog = false;

        public static void ApplySetting()
        {
#if UNITY_EDITOR

            //Screen.SetResolution(1334, 750, false);
            Screen.sleepTimeout = SleepTimeout.NeverSleep; // 锁屏
            EnablePersistentPath = false;
            EnableAssetBundle = false;
            GameUtil.UploadFile.crashServerAddress = "http://192.168.112.53:8080/testHttp/X6/"; // 上传路径
            GameUtil.UploadFile.LocalNet = true;
            GameUtil.HttpUtil.Encode = Encoding.Default;
            VersionManager.Instance.PatchLua = false;
            QualitySettings.shadows = ShadowQuality.All;
            QualitySettings.shadowResolution = ShadowResolution.High;
            Light[] ligths = Light.GetLights(LightType.Directional, 0);
            for (int i = 0; i < ligths.Length; i++)
            {
                Light light = ligths[i];
                light.shadows = LightShadows.Soft;
            }
            
#elif UNITY_STANDALONE_WIN
            ResolutionSetting.SetDesignResolution(1334, 750);
            ResolutionSetting.SetResolution(false, false);
            Screen.sleepTimeout = SleepTimeout.NeverSleep; // 锁屏
            EnablePersistentPath = true;
            EnableAssetBundle = true;
            GameUtil.UploadFile.crashServerAddress = "http://192.168.112.53:8080/testHttp/X6/"; // 上传路径
            GameUtil.UploadFile.LocalNet = true;
            GameUtil.HttpUtil.Encode = Encoding.Default;
            VersionManager.Instance.PatchLua = false;

#elif UNITY_IPHONE

            ResolutionSetting.SetDesignResolution(1334, 750);
            ResolutionSetting.SetResolution(false, true);
            Screen.sleepTimeout = SleepTimeout.NeverSleep; // 锁屏
            EnablePersistentPath = true;
            EnableAssetBundle = true;
            GameUtil.UploadFile.crashServerAddress = "http://log.2980.com:28081/upload.php?name=X6"; // 上传路径
            //Debug.logger.logEnabled = false; // 去掉print信息
            VersionManager.Instance.PatchLua = true;

#elif UNITY_ANDROID

            ResolutionSetting.SetDesignResolution(1334, 750);
            ResolutionSetting.SetResolution(false, true);
            Screen.sleepTimeout = SleepTimeout.NeverSleep; // 锁屏
            EnablePersistentPath = true;
            EnableAssetBundle = true;
            GameUtil.UploadFile.crashServerAddress = "http://log.2980.com:28081/upload.php?name=X6"; // 上传路径
            //Debug.logger.logEnabled = false; // 去掉print信息
            VersionManager.Instance.PatchLua = true;

#endif
            ApplyRenderQuality();
        }

        // 画质调节
        public static void ApplyRenderQuality()
        {
            Shader.globalMaximumLOD = 200;
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            DeviceLevel = SGameRenderQuality.High;
#elif UNITY_IPHONE
            DeviceLevel = DetectRenderQuality.check_iOS();
#elif UNITY_ANDROID
            DeviceLevel = DetectRenderQuality.check_Android();
#endif
            switch (DeviceLevel)
            {
                case SGameRenderQuality.High:
                    {
#if UNITY_IPHONE || UNITY_ANDROID
                        //Shader.globalMaximumLOD = 400;
#endif
                        QualitySettings.shadows = ShadowQuality.HardOnly;
                        QualitySettings.shadowResolution = ShadowResolution.Medium;
                        QualitySettings.shadowCascades = 0;
                        UseMultiThread = false;
                        UseShadowThread = false;
                    }
                    break;
                case SGameRenderQuality.Medium:
                    {
                        //Shader.globalMaximumLOD = 300;
                        QualitySettings.shadows = ShadowQuality.HardOnly;
                        QualitySettings.shadowResolution = ShadowResolution.Low;
                        UseMultiThread = false;
                        UseShadowThread = false;
                    }
                    break;
                case SGameRenderQuality.Low:
                    {
                        //Shader.globalMaximumLOD = 200;
                        //QualitySettings.shadows = ShadowQuality.Disable;
                        QualitySettings.shadows = ShadowQuality.HardOnly;
                        UseMultiThread = true;
                        UseShadowThread = true;
                    }
                    break;
            }
            nowLevel = DeviceLevel;
            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
                UnityEngine.Application.targetFrameRate = 60;
            else
                UnityEngine.Application.targetFrameRate = 30;
        }
    }
}