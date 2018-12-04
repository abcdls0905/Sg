using System.IO;
using System.Text;
using UnityEngine;

namespace GameUtil
{
    public static class CrashReportUtil
    {
        public static void LaunchCrashReport()
        {
            if (CrashReport.lastReport != null)
            {
                string outDir = Application.persistentDataPath + "/logs";
                string outPath = outDir + "/crashlog.txt";

                if (!Directory.Exists(outDir))
                    Directory.CreateDirectory(outDir);
                if (System.IO.File.Exists(outPath))
                    File.Delete(outPath);

                try
                {
                    // 写入文件
                    using (var Writer = new StreamWriter(outPath, false, Encoding.UTF8))
                    {
                        Writer.WriteLine(CrashReport.lastReport.time.ToLongDateString());
                        Writer.WriteLine(CrashReport.lastReport.text);
                        Writer.Flush();
                        Writer.Close();
                    }
                }
                catch
                {
                    // do nothing
                }
                CrashReport.RemoveAll();
                GameUtil.FileSendUtil.SendFile(outPath);
            }
        }
    }
}