using System;
using System.IO;
using UnityEngine;

namespace GameUtil
{
    public static class FileSendUtil
    {
        private static string persistDataPath = Application.persistentDataPath + "/";
        public static string PortName = Network.player.ipAddress;
        private static string GetTimeString()
        {
            var now = DateTime.Now;
            return string.Format("{0}.{1}.{2}", now.Hour, now.Minute, now.Second);
        }
        public static int SendFile(string path)
        {
            try
            {
                // path = persistDataPath + path;
                if (File.Exists(path))
                {
                    string fileName = Path.GetFileNameWithoutExtension(path);
                    string extName = Path.GetExtension(path);
                    string saveName = String.Format("{0}_{1}_{2}.{3}", fileName, PortName, GetTimeString(), extName);
                    string data = File.ReadAllText(path);
                    GameUtil.UploadFile.UploadString(data, saveName);
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("SendFile Error£¡: " + e.ToString());
            }
            return 0;
        }

        public static int SendString(string name, string data)
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(name);
                string extName = Path.GetExtension(name);
                string saveName = String.Format("{0}_{1}_{2}.{3}", fileName, PortName, GetTimeString(), extName);
                GameUtil.UploadFile.UploadString(data, saveName);
            }
            catch (Exception e)
            {
                Debug.LogWarning("SendString Error£¡: " + e.ToString());
            }
            return 0;
        }
    }
}