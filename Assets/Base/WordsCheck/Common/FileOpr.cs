using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;

namespace WordsCheck
{
    class TxtOpr
    {
        public static string Load(string strFileName)
        {
            string strFile = Application.persistentDataPath + "/" + RelateUpate + "/" + strFileName + ".txt";
            if (File.Exists(strFile))
            {
                return ReadByIo(strFile);
            }
            else
            {
                strFile = Application.streamingAssetsPath + "/" + RelateStream + "/" + strFileName + ".txt";
#if UNITY_EDITOR
                if (!File.Exists(strFile))
                {
                    strFile = Application.dataPath + "/" + RelateAssets + "/" + strFileName + ".txt";
                }
#endif
#if UNITY_ANDROID
                return ReadByWww(strFile);
#endif
                return ReadByIo(strFile);

            }



        }
        private static string RelateAssets
        {
            get
            {
                if(!string.IsNullOrEmpty(Config.DataRelatePathAssets))
                {
                    return Config.DataRelatePathAssets;
                }
                else
                {
                    return Config.DataRelatePath;
                }
            }
        }
        private static string RelateStream
        {
            get
            {
                if (!string.IsNullOrEmpty(Config.DataRelatePathStream))
                {
                    return Config.DataRelatePathStream;
                }
                else
                {
                    return Config.DataRelatePath;
                }
            }
        }
        private static string RelateUpate
        {
            get
            {
                if (!string.IsNullOrEmpty(Config.DataRelatePathUpdate))
                {
                    return Config.DataRelatePathUpdate;
                }
                else
                {
                    return Config.DataRelatePath;
                }
            }
        }

        private static string ReadByIo(string strFile)
        {
            string strRet = string.Empty;
            try
            {
                FileStream fs = null;
                StreamReader sr = null;
                if (!File.Exists(strFile))
                {
                    Debug.Log("ReadByIo file not find " + strFile);
                    return strRet;
                }
                fs = File.OpenRead(strFile);
                sr = new StreamReader(fs, Encoding.UTF8);
                strRet = sr.ReadToEnd();
                sr.Close();
                fs.Close();
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            return strRet;
        }

        private static string ReadByWww(string strFile)
        {
            string strRet = string.Empty;
            if (!strFile.Contains("file://"))
            {
                strFile = "file://" + strFile;
            }
            try
            {
                WWW www = new WWW(strFile);
                while (!www.isDone) { }

                if (www.error != null)
                {
                    Debug.Log("ReadByWww :" + strFile + " error = " + www.error);
                    return strRet;
                }
                strRet = www.text;
                www.Dispose();
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

            return strRet;
        }

        public static bool Save(List<string> listIn, string strVersion, string strFileName)
        {
            string strPath = Application.persistentDataPath + "/" + RelateUpate;
            string strFile = strPath + "/" + strFileName + ".txt";

            try
            {
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }

                StreamWriter sw = new StreamWriter(strFile, false, Encoding.UTF8);

                sw.WriteLine(strVersion);
                if(listIn != null && listIn.Count > 0)
                {
                    for(int i = 0; i < listIn.Count; i++)
                    {
                        sw.WriteLine(listIn[i]);
                    }
                }
                sw.Flush();

                sw.Close();
                return true;
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            return false;
        }
    }
}
