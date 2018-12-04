using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Game
{
    public enum enFileOperation
    {
        ReadFile,
        WriteFile,
        DeleteFile,
        CreateDirectory,
        DeleteDirectory
    }

    public static class FileManager
    {
        public delegate void DelegateOnOperateFileFail(string fullPath, enFileOperation fileOperation);
        private static string s_cachePath = null;
        public static string s_ifsExtractFolder = "Resources";
        private static string s_ifsExtractPath = null;
        private static MD5CryptoServiceProvider s_md5Provider = new MD5CryptoServiceProvider();
        private static string persistDataPath = Application.persistentDataPath + "/";

        public static DelegateOnOperateFileFail s_delegateOnOperateFileFail = delegate
        {
        };
        public static bool IsFileExist(string filePath)
        {
            return File.Exists(filePath);
        }
        public static bool IsDirectoryExist(string directory)
        {
            return Directory.Exists(directory);
        }
        public static bool CreateDirectory(string directory)
        {
            if (IsDirectoryExist(directory))
            {
                return true;
            }
            int num = 0;
            bool result;
            while (true)
            {
                try
                {
                    Directory.CreateDirectory(directory);
                    result = true;
                    break;
                }
                catch (Exception ex)
                {
                    num++;
                    if (num >= 3)
                    {
                        Debug.Log("Create Directory " + directory + " Error! Exception = " + ex.ToString());
                        s_delegateOnOperateFileFail(directory, enFileOperation.CreateDirectory);
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }
        public static bool DeleteDirectory(string directory)
        {
            if (!IsDirectoryExist(directory))
            {
                return true;
            }
            int num = 0;
            bool result;
            while (true)
            {
                try
                {
                    Directory.Delete(directory, true);
                    result = true;
                    break;
                }
                catch (Exception ex)
                {
                    num++;
                    if (num >= 3)
                    {
                        Debug.Log("Delete Directory " + directory + " Error! Exception = " + ex.ToString());
                        s_delegateOnOperateFileFail(directory, enFileOperation.DeleteDirectory);
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }
        public static int GetFileLength(string filePath)
        {
            if (!IsFileExist(filePath))
            {
                return 0;
            }
            int num = 0;
            int result;
            while (true)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    result = (int)fileInfo.Length;
                    break;
                }
                catch (Exception ex)
                {
                    num++;
                    if (num >= 3)
                    {
                        Debug.Log("Get FileLength of " + filePath + " Error! Exception = " + ex.ToString());
                        result = 0;
                        break;
                    }
                }
            }
            return result;
        }

        public static byte[] ReadFile(string filePath, bool persist = false)
        {
#if UNITY_ANDROID
            if (persist)
                return ReadFileByDirect(filePath);
            else
                return ReadFileByWWW(filePath);
#else
            return ReadFileByDirect(filePath);
#endif
        }
        public static byte[] ReadFileByWWW(string filePath)
        {
            WWW www = new WWW(filePath);
            while (!www.isDone) { }

            if (www.error != null)
            {
                Debug.Log("CheckFileByWWW Fail :" + filePath + " -- " + www.error);
                s_delegateOnOperateFileFail(filePath, enFileOperation.ReadFile);
                return null;
            }

            return www.bytes;
        }

        public static byte[] ReadFileByDirect(string filePath)
        {
            if (!IsFileExist(filePath))
            {
                return null;
            }
            byte[] array = null;
            int num = 0;
            do
            {
                try
                {
                    array = File.ReadAllBytes(filePath);
                }
                catch (Exception ex)
                {
                    Debug.Log(string.Concat(new object[]
                    {
                    "Read File ",
                    filePath,
                    " Error! Exception = ",
                    ex.ToString(),
                    ", TryCount = ",
                    num
                    }));
                    array = null;
                }
                if (array != null && array.Length > 0)
                {
                    return array;
                }
                num++;
            }
            while (num < 3);
            Debug.Log(string.Concat(new object[]
            {
            "Read File ",
            filePath,
            " Fail!, TryCount = ",
            num
            }));
            s_delegateOnOperateFileFail(filePath, enFileOperation.ReadFile);
            return null;
        }
        public static bool WriteFile(string filePath, byte[] data)
        {
            int num = 0;
            bool result;
            while (true)
            {
                try
                {
                    File.WriteAllBytes(filePath, data);
                    result = true;
                    break;
                }
                catch (Exception ex)
                {
                    num++;
                    if (num >= 3)
                    {
                        Debug.Log("Write File " + filePath + " Error! Exception = " + ex.ToString());
                        DeleteFile(filePath);
                        s_delegateOnOperateFileFail(filePath, enFileOperation.WriteFile);
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }
        public static bool WriteFile(string filePath, byte[] data, int offset, int length)
        {
            FileStream fileStream = null;
            int num = 0;
            bool result;
            while (true)
            {
                try
                {
                    fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                    fileStream.Write(data, offset, length);
                    fileStream.Close();
                    result = true;
                    break;
                }
                catch (Exception ex)
                {
                    if (fileStream != null)
                    {
                        fileStream.Close();
                    }
                    num++;
                    if (num >= 3)
                    {
                        Debug.Log("Write File " + filePath + " Error! Exception = " + ex.ToString());
                        DeleteFile(filePath);
                        s_delegateOnOperateFileFail(filePath, enFileOperation.WriteFile);
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }
        public static bool DeleteFile(string filePath)
        {
            if (!IsFileExist(filePath))
            {
                return true;
            }
            int num = 0;
            bool result;
            while (true)
            {
                try
                {
                    File.Delete(filePath);
                    result = true;
                    break;
                }
                catch (Exception ex)
                {
                    num++;
                    if (num >= 3)
                    {
                        Debug.Log("Delete File " + filePath + " Error! Exception = " + ex.ToString());
                        s_delegateOnOperateFileFail(filePath, enFileOperation.DeleteFile);
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }
        public static void CopyFile(string srcFile, string dstFile)
        {
            File.Copy(srcFile, dstFile, true);
        }
        public static string GetFileMd5(string filePath)
        {
            if (!IsFileExist(filePath))
            {
                return string.Empty;
            }
            return BitConverter.ToString(s_md5Provider.ComputeHash(ReadFile(filePath))).Replace("-", string.Empty);
        }
        public static string GetMd5(byte[] data)
        {
            return BitConverter.ToString(s_md5Provider.ComputeHash(data)).Replace("-", string.Empty);
        }
        public static string GetMd5(string str)
        {
            return BitConverter.ToString(s_md5Provider.ComputeHash(Encoding.UTF8.GetBytes(str))).Replace("-", string.Empty);
        }
        public static string CombinePath(string path1, string path2)
        {
            if (path1.LastIndexOf('/') != path1.Length - 1)
            {
                path1 += "/";
            }
            if (path2.IndexOf('/') == 0)
            {
                path2 = path2.Substring(1);
            }
            return path1 + path2;
        }
        public static string CombinePaths(params string[] values)
        {
            if (values.Length <= 0)
            {
                return string.Empty;
            }
            if (values.Length == 1)
            {
                return CombinePath(values[0], string.Empty);
            }
            if (values.Length > 1)
            {
                string text = CombinePath(values[0], values[1]);
                for (int i = 2; i < values.Length; i++)
                {
                    text = CombinePath(text, values[i]);
                }
                return text;
            }
            return string.Empty;
        }
        public static string GetStreamingAssetsPathWithHeader(string fileName)
        {
            return Path.Combine(Application.streamingAssetsPath, fileName);
        }
        public static string GetCachePath()
        {
            if (s_cachePath == null)
            {
                s_cachePath = Application.persistentDataPath;
            }
            return s_cachePath;
        }
        public static string GetCachePath(string fileName)
        {
            return CombinePath(GetCachePath(), fileName);
        }

        public static string GetIFSExtractPath()
        {
            if (s_ifsExtractPath == null)
            {
                s_ifsExtractPath = CombinePath(GetCachePath(), s_ifsExtractFolder);
            }
            return s_ifsExtractPath;
        }
        public static string GetFullName(string fullPath)
        {
            if (fullPath == null)
            {
                return null;
            }
            int num = fullPath.LastIndexOf("/");
            if (num > 0)
            {
                return fullPath.Substring(num + 1, fullPath.Length - num - 1);
            }
            return fullPath;
        }
        public static string EraseExtension(string fullName)
        {
            if (fullName == null)
            {
                return null;
            }
            int num = fullName.LastIndexOf('.');
            if (num > 0)
            {
                return fullName.Substring(0, num);
            }
            return fullName;
        }
        public static string AddExtension(string fullName, string extension)
        {
            if (fullName == null)
            {
                return null;
            }
            int num = fullName.LastIndexOf('.');
            if (num > 0)
            {
                return fullName;
            }
            return fullName + extension;
        }
        public static string GetExtension(string fullName)
        {
            int num = fullName.LastIndexOf('.');
            if (num > 0 && num + 1 < fullName.Length)
            {
                return fullName.Substring(num);
            }
            return string.Empty;
        }
        public static string GetFullDirectory(string fullPath)
        {
            return Path.GetDirectoryName(fullPath);
        }
        public static bool ClearDirectory(string fullPath)
        {
            bool result;
            try
            {
                string[] files = Directory.GetFiles(fullPath);
                for (int i = 0; i < files.Length; i++)
                {
                    File.Delete(files[i]);
                }
                string[] directories = Directory.GetDirectories(fullPath);
                for (int j = 0; j < directories.Length; j++)
                {
                    Directory.Delete(directories[j], true);
                }
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        public static bool ClearDirectory(string fullPath, string[] fileExtensionFilter, string[] folderFilter)
        {
            bool result;
            try
            {
                if (fileExtensionFilter != null)
                {
                    string[] files = Directory.GetFiles(fullPath);
                    for (int i = 0; i < files.Length; i++)
                    {
                        if (fileExtensionFilter != null && fileExtensionFilter.Length > 0)
                        {
                            for (int j = 0; j < fileExtensionFilter.Length; j++)
                            {
                                if (files[i].Contains(fileExtensionFilter[j]))
                                {
                                    DeleteFile(files[i]);
                                    break;
                                }
                            }
                        }
                    }
                }
                if (folderFilter != null)
                {
                    string[] directories = Directory.GetDirectories(fullPath);
                    for (int k = 0; k < directories.Length; k++)
                    {
                        if (folderFilter != null && folderFilter.Length > 0)
                        {
                            for (int l = 0; l < folderFilter.Length; l++)
                            {
                                if (directories[k].Contains(folderFilter[l]))
                                {
                                    DeleteDirectory(directories[k]);
                                    break;
                                }
                            }
                        }
                    }
                }
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public delegate bool FileFilter(string path, bool isDirectory);
        public static void CopyDirectory(string srcPath, string tarPath, FileFilter filter = null)
        {
            if (!Directory.Exists(srcPath))
            {
                Directory.CreateDirectory(srcPath);
            }
            if (!Directory.Exists(tarPath))
            {
                Directory.CreateDirectory(tarPath);
            }
            CopyFiles(srcPath, tarPath, filter);
            string[] directionName = Directory.GetDirectories(srcPath);
            foreach (string dirPath in directionName)
            {
                if (filter != null && !filter(dirPath, true))
                    continue;
                string directionPathTemp = Path.Combine(tarPath, dirPath.Substring(srcPath.Length + 1));
                CopyDirectory(dirPath, directionPathTemp, filter);
            }
        }

        public static void CopyFiles(string srcPath, string tarPath, FileFilter filter)
        {
            string[] filesList = Directory.GetFiles(srcPath);
            foreach (string f in filesList)
            {
                if (filter != null && !filter(f, false))
                    continue;
                string fTarPath = Path.Combine(tarPath, f.Substring(srcPath.Length + 1));
                if (File.Exists(fTarPath))
                {
                    File.Copy(f, fTarPath, true);
                }
                else
                {
                    File.Copy(f, fTarPath);
                }
            }
        }

        public static string[] SearchResFiles(string path)
        {
            if (Directory.Exists(path))
            {
                int pathLen = path.Length;
                if (!path.EndsWith("/"))
                {
                    pathLen += 1;
                }
                string[] res = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
                List<string> lst = new List<string>();
                for (int i = 0; i < res.Length; i++)
                {
                    if (res[i].EndsWith(".meta"))
                    {
                        continue;
                    }
                    lst.Add(res[i].Remove(0, pathLen));
                }
                return lst.ToArray();
            }
            return null;
        }

        //PersistentDirHelper...............................................................................................................
        public static void SaveToFile(string path, byte[] bytes)
        {
            //Debug.LogFormat("SaveFile: {0}", path);
            path = persistDataPath + path;
            string filePath = Path.GetDirectoryName(path);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            try
            {
                if (File.Exists(path))
                {
                    Debug.Log(path);
                    File.Delete(path);
                }

                File.WriteAllBytes(path, bytes);
            }
            catch (Exception)
            {
                Debug.LogWarning("Save To File Conflict£¡");
            }
        }

        public static bool UnZipFile(string fileToUnZip, string zipedFolder)
        {
            return CSharpZip.ZipHelper.UnZip(persistDataPath + fileToUnZip, persistDataPath + zipedFolder);
        }
        //PersistentDirHelper...............................................................................................................
    }
}