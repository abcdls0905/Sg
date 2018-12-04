/*
    根据QA提供的配置文件，判断当前机型的高中低配置
*/

using System.Collections.Generic;
using UnityEngine;

namespace GameUtil
{
    public class RenderQuality
    {

        public delegate string ReadFile(string fileName);

        /// <summary>
        /// 读取文件接口，项目可以根据自己的读取文件的方式注册读取文件的接口
        /// 文件名称是不含后缀的文件名
        /// </summary>
        public static ReadFile LoadFile = null;

        /// <summary>
        /// 读取配置，转换为正在表达式
        /// </summary>
        /// <param name="fileName">配置文件的名称</param>
        /// <param name="dicConfig">写入的字典实例</param>
        private static void ReadConfig(string fileName, out Dictionary<int, List<string>> dicConfig)
        {
            dicConfig = new Dictionary<int, List<string>>();
            dicConfig.Clear();

            //项目可以根据自己的读取文件的方式修改读取文件的接口
            string text;
            if (LoadFile == null)
                text = WordsCheck.TxtOpr.Load(fileName);
            else
                text = LoadFile(fileName);

            if (!string.IsNullOrEmpty(text))
            {


                var allLine = text.Replace("\r\n", "\n").Split('\n');
                if (allLine != null && allLine.Length > 0)
                {
                    //获取一共分为多少挡，以配置文档为准
                    var level = allLine[0].Split('\t');
                    Dictionary<int, int> dicLevel = new Dictionary<int, int>();
                    for (int i = 0; i < level.Length; ++i)
                    {
                        if (!string.IsNullOrEmpty(level[i]))
                        {
                            int l = int.Parse(level[i]);
                            if (l > 0)
                            {
                                dicConfig.Add(l, new List<string>());
                                dicLevel.Add(i, l);
                            }
                        }
                    }

                    //判断，如果配置读取的分挡为空，就退出
                    if (dicConfig.Count <= 0)
                    {
                        Debug.Log("RenderQuality ReadConfig fileName = " + fileName + " config is empty");
                        return;
                    }

                    for (int i = allLine.Length - 1; i > 0; --i)
                    {
                        if (!string.IsNullOrEmpty(allLine[i]))
                        {
                            var oneLine = allLine[i].Split('\t');
                            if (oneLine != null)
                            {
                                for (int j = oneLine.Length - 1; j >= 0; --j)
                                {
                                    string str = oneLine[j];
                                    if (!string.IsNullOrEmpty(str))
                                        dicConfig[dicLevel[j]].Add(str.ToLower().Replace(" ", "(.*)"));
                                }
                            }
                        }
                    }
                }
            }
            else
                Debug.Log("RenderQuality ReadConfig fileName = " + fileName + " load text is null or empty");
        }

        private static int CheckMemoryLevel()
        {
            //项目可以根据自己的读取文件的方式修改读取文件的接口
            string text;
            if (LoadFile == null)
                text = WordsCheck.TxtOpr.Load("Memory");
            else
                text = LoadFile("Memory");

            // 如果配置为空，返回最低挡
            if (string.IsNullOrEmpty(text))
            {
                Debug.Log("RenderQuality CheckMemoryLevel text is null or empty");
                return 1;
            }

            var allLine = text.Replace("\r\n", "\n").Split('\n');
            if (allLine == null || allLine.Length < 2)
                return 1;

            var level = allLine[0].Split('\t');
            var memory = allLine[1].Split('\t');
            float ownMemory = SystemInfo.systemMemorySize / (float)1000.0;
            Debug.Log("RenderQuality CheckMemoryLevel is ownMemory = " + ownMemory);
            for (int i = 0; i < level.Length; ++i)
            {
                float m = float.Parse(memory[i]);
                if (ownMemory < m)
                {
                    Debug.Log("RenderQuality CheckMemoryLevel is match return " + int.Parse(level[i]));
                    return int.Parse(level[i]);
                }
            }

            Debug.Log("RenderQuality CheckMemoryLevel is not match");
            return 1;
        }

        /// <summary>
        /// 获取设备的配置
        /// android和ios需要读取配置来判断
        /// 其他平台都默认设置为高配置
        /// </summary>
        /// <returns>返回配置，1 到 x, 从低到高</returns>
        public static int GetQuality()
        {
            int retQuality = 5;
    #if UNITY_ANDROID
            retQuality = GetAndroidQuality();
    #elif UNITY_IPHONE
            retQuality = GetIOSQuality();
    #endif
            return retQuality;
        }

        /// <summary>
        /// 调用java代码获取cpu信息
        /// </summary>
        /// <returns>cpu信息，小写</returns>
        public static string GetCPUInfo()
        {
            string cpu = "";
            try
            {
                var javaCpuName = new AndroidJavaClass("com.duoyi.getcpuname.CallMain");
                var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                cpu = javaCpuName.CallStatic<string>("GetCPU", activity);
            }
            catch (System.Exception ex)
            {
                Debug.Log("RenderQuality GetCPUInfo is error exption = " + ex.ToString());
            }
            
            return cpu.ToLower();
        }

        /// <summary>
        /// 根据提供的手机信息，判断手机质量
        /// </summary>
        /// <param name="mobileInfo">手机信息字符串，（cpu或者gpu信息）</param>
        /// <param name="config">配表信息</param>
        /// <returns></returns>
        private static int GetQuality(string mobileInfo, Dictionary<int, List<string>> config)
        {
            if (string.IsNullOrEmpty(mobileInfo))
            {
                Debug.Log("RenderQuality GetQuality mobileInfo is null");
                return 0;
            }

            for (int q = config.Count; q > 0; --q)
            {
                List<string> list;
                if (config.TryGetValue(q, out list) && list != null && list.Count > 0)
                {
                    for (int i = list.Count - 1; i >= 0; --i)
                        if (System.Text.RegularExpressions.Regex.IsMatch(mobileInfo, list[i], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            return q;
                }
            }

            Debug.Log("RenderQuality GetQuality mobileInfo is " + mobileInfo + " not match");
            return 0;
        }

        /// <summary>
        /// 获取Android设备的配置高低
        /// </summary>
        /// <returns></returns>
        private static int GetAndroidQuality()
        {
            if (LoadFile != null)
            {
                Debug.Log("RenderQuality Android LoadFile Function = " + LoadFile.ToString());
            }
            else
            {
                Debug.Log("RenderQuality Android LoadFile Function wans't register");
            }
            //检查cpu类型
            string cpuInfo = GetCPUInfo();

            Debug.Log("RenderQuality Android cpuInfo = " + cpuInfo);

            Dictionary<int, List<string>> cpuConfig = null;
            ReadConfig("CPU", out cpuConfig);

            Debug.Log("RenderQuality Android cpuConfig = " + cpuConfig.ToString());

            var quality = GetQuality(cpuInfo, cpuConfig);

            Debug.Log("RenderQuality Android quality = " + quality);

            if (quality != 0)
                return quality;


            return CheckMemoryLevel();
        }

        /// <summary>
        /// 获取IOS的档次
        /// 如果没有出现在配表中，就默认设置为高
        /// </summary>
        /// <returns></returns>
        private static int GetIOSQuality()
        {
            Dictionary<int, List<string>> iosConfig;
            ReadConfig("IOS", out iosConfig);

            Debug.Log("RenderQuality IOS cpuConfig = " + iosConfig.ToString());

            var quality = GetQuality(SystemInfo.deviceModel.ToLower(), iosConfig);

            Debug.Log("RenderQuality IOS quality = " + quality);

            if (quality == 0)
                return iosConfig.Count;
            else
                return quality;
        }
    }
}
