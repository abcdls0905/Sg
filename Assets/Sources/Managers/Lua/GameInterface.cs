using GameUtil;
using Game;
using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using UnityEngine;
using XLua;
using System.Security.Cryptography;
using System.Collections.Generic;

[LuaCallCSharp]
public static class GameInterface
{
    private static string persistDataPath = Application.persistentDataPath + "/";
    public static Dictionary<string, string> m_cacheKey = new Dictionary<string, string>();

    public static string GetPrefabKey(string prefabFullPath)
    {
        string prefabKey = string.Empty;
        if (!m_cacheKey.TryGetValue(prefabFullPath, out prefabKey))
        {
            prefabKey = FileManager.EraseExtension(prefabFullPath).ToLower();
            m_cacheKey.Add(prefabFullPath, prefabKey);
        }
        return prefabKey;
    }

    public static string GetMD5Hash(byte[] buffer)
    {
        try
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(buffer);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("GetMD5HashFromFile() fail, error:" + ex.Message);
        }
    }
    //--------------------------- 系统信息相关

    // iOS8后无法获取mac地址
    public static string GetMAC()
    {
        string macAddresses = "";
#if UNITY_ANDROID
            //临时数据
            macAddresses = "000000000000";
#else
        NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
        for (int i = 0; i < adapters.Length; ++i)
        {
            NetworkInterface adapter = adapters[i];
            if (adapter.Description == "en0")
            {
                macAddresses = adapter.GetPhysicalAddress().ToString();
                break;
            }
            else
            {
                macAddresses = adapter.GetPhysicalAddress().ToString();
                if (macAddresses != "")
                {
                    break;
                }
            }
        }
#endif
        return macAddresses;
    }

    public static string GetIP()
    {
        string ipAddresses = "";
#if UNITY_ANDROID
            //临时数据
            ipAddresses = "192.168.1.1";
#else
        NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
        for (int i = 0; i < adapters.Length; ++i)
        {
            NetworkInterface adapter = adapters[i];
            if (adapter.Supports(NetworkInterfaceComponent.IPv4))
            {
                UnicastIPAddressInformationCollection uniCast = adapter.GetIPProperties().UnicastAddresses;
                for (int j = 0; j < uniCast.Count; ++j)
                {
                    UnicastIPAddressInformation uni = uniCast[j];
                    if (uni.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        ipAddresses = uni.Address.ToString();
                    }
                }
            }
        }
#endif
        return ipAddresses;
    }

    public static string GetIdentifierForVendor()
    {
#if UNITY_IPHONE
            return UnityEngine.iOS.Device.vendorIdentifier;
#else
        return "";
#endif
    }

    public static string GetSysVersion()
    {
#if UNITY_IPHONE
            return UnityEngine.iOS.Device.systemVersion;
#else
        return SystemInfo.operatingSystem;
#endif
    }

    public static string GetDeviceModel()
    {
        return SystemInfo.deviceModel;
    }

    public static bool IsiPhoneX()
    {
        return SystemInfo.deviceModel == "iPhone10,3" || SystemInfo.deviceModel == "iPhone10,6";
    }

    public static void FitiPhoneX(FairyGUI.Window window, bool needFit = true)
    {
        bool isiPhoneX = IsiPhoneX();
        if (needFit && isiPhoneX)
        {
            window.width = 1448;
            window.x += 88;
            var resolution = window.contentPane.GetController("resolution");
            if (resolution != null)
                resolution.selectedPage = "iphonex";
        }
    }

    public static void SetSortingOrder(FairyGUI.Window window, Game.UIPageLayer layer)
    {
        window.sortingOrder = (int)layer;
    }

    public static string GetBundleIdentifier()
    {
#if UNITY_5
        return Application.bundleIdentifier;
#else
        return Application.identifier;
#endif
    }
    //------------------------------------KeyChain相关

    public static string GetKeyChain(string key)
    {
        return Keychain.GetKeyChain(key);
    }

    public static void SaveKeyChain(string key, string value)
    {
        Keychain.SaveKeyChain(key, value);
    }

    public static void DeleteKeyChain(string key)
    {
        Keychain.DeleteKeyChain(key);
    }

    // 加密解密
    public static string StrDesEncrypt(string plaintxt, string key)
    {
        return DES8Bit.DESEncrypt(plaintxt, key);
    }

    public static string StrDesDecrypt(string ciphertext, string sKey)
    {
        return DES8Bit.DESDecryptt(ciphertext, sKey);
    }

    public static string StrDesEncryptEx(string text, string key, string iv)
    {
        var des = DES.Create();
        des.IV = Encoding.UTF8.GetBytes(iv);
        des.Key = Encoding.UTF8.GetBytes(key);
        des.Mode = CipherMode.CBC;
        des.Padding = PaddingMode.PKCS7;
        var mStream = new MemoryStream();
        using (var cStream = new CryptoStream(mStream, des.CreateEncryptor(), CryptoStreamMode.Write))
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            cStream.Write(bytes, 0, bytes.Length);
            cStream.FlushFinalBlock();
            cStream.Close();
        }
        var ret = Convert.ToBase64String(mStream.ToArray());
        mStream.Close();
        return ret;
    }

    public static string StrDesDecryptEx(string text, string key, string iv)
    {
        var des = DES.Create();
        des.IV = Encoding.UTF8.GetBytes(iv);
        des.Key = Encoding.UTF8.GetBytes(key);
        des.Mode = CipherMode.CBC;
        des.Padding = PaddingMode.PKCS7;
        var mStream = new MemoryStream();
        using (var cStream = new CryptoStream(mStream, des.CreateDecryptor(), CryptoStreamMode.Write))
        {
            var bytes = Convert.FromBase64String(text);
            cStream.Write(bytes, 0, bytes.Length);
            cStream.FlushFinalBlock();
            cStream.Close();
        }
        var ret = Encoding.UTF8.GetString(mStream.ToArray());
        mStream.Close();
        return ret;
    }

    // 网络请求
    public static string GetHttpReq(string url, int timeout)
    {
        string ret = "";
        try
        {
            using (HttpWebResponse response = HttpUtil.CreateGetHttpResponse(url, timeout, null, null))
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        ret = sr.ReadToEnd();
                        sr.Close();
                    }
                    stream.Flush();
                    stream.Close();
                }
                response.Close();
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning(ex.ToString());
        }
        return ret;
    }

    public static byte[] ReadGameData(string path)
    {
        string fullPath = string.Empty;
#if UNITY_EDITOR
        fullPath = Path.Combine(GameEnvSetting.dataPath, path);
#else
        bool fileExist = false;
        if (GameEnvSetting.EnablePersistentPath)
        {
            fullPath = Path.Combine(GameEnvSetting.dataUpdatePath, path);
            fileExist = (File.Exists(fullPath));
        }
        if (!fileExist)
        {
            fullPath = Path.Combine(GameEnvSetting.dataStreamPath, path);
        }
#endif
        return FileManager.ReadFile(fullPath);
    }

    public static byte[] ReadProtoData(string path)
    {
        string fullPath = string.Empty;
#if UNITY_EDITOR
        fullPath = Path.Combine(GameEnvSetting.protoPath, path);
#else
        bool fileExist = false;
        if (GameEnvSetting.EnablePersistentPath)
        {
            fullPath = Path.Combine(GameEnvSetting.protoUpdatePath, path);
            fileExist = (File.Exists(fullPath));
        }
        if (!fileExist)
        {
            fullPath = Path.Combine(GameEnvSetting.protoStreamPath, path);
        }
#endif
        return FileManager.ReadFile(fullPath);
    }

    public static byte[] ReadRootData(string path)
    {
#if UNITY_EDITOR
        return null;
#else
        string fullPath = string.Empty;
        bool fileExist = false;
        bool persist = false;
        if (GameEnvSetting.EnablePersistentPath)
        {
            fullPath = Path.Combine(GameEnvSetting.rootUpdatePath, path);
            fileExist = (File.Exists(fullPath));
            persist = true;
        }
        if (!fileExist)
        {
            fullPath = Path.Combine(GameEnvSetting.rootStreamPath, path);
            persist = false;
        }
        return FileManager.ReadFile(fullPath, persist);
#endif
    }

    public static string GenDeviceId()
    {
        ushort value = (ushort)UnityEngine.Random.Range(ushort.MinValue, ushort.MaxValue);
        return string.Format("{0:D16}", value);
    }

    public static string[] SearchFilesEx(string path, string searchPattern)
    {
        path = persistDataPath + path;
        if (Directory.Exists(path))
        {
            string[] res = Directory.GetFiles(path, searchPattern);
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = Path.GetFileName(res[i]);
            }
            return res;
        }
        return null;
    }

    public static void SendErrorLog()
    {
        string dir = "logs/";
        string[] files = SearchFilesEx(dir, "*.*");
        for (int i = 0; i < files.Length; i++)
        {
            string file = files[i];
            SendLogFile(file);
        }
    }

    public static void SendLogFile(string path)
    {
        GameUtil.FileSendUtil.SendFile(path);
    }

    public static void SetPortName(string name)
    {
        FileSendUtil.PortName = name;
    }

    public static uint GetCRC32(byte[] bytes)
    {
        return Force.Crc32.Crc32Algorithm.Compute(bytes, 0, bytes.Length);
    }

    public static void StopGame()
    {
        if (ECSManager.HasInstance())
            ECSManager.Instance.Stop();
    }

    public static void ResumeGame()
    {
        if (ECSManager.HasInstance())
            ECSManager.Instance.Resume();
    }

    public static bool EnterAutoTest()
    {
        return false;
    }
}