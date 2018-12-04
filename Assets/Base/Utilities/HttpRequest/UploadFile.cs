using System.Collections.Generic;
using UnityEngine;

namespace GameUtil
{
    public static class UploadFile
    {
        static string _crashFileFormName;
        public static bool LocalNet { get; set; }
        public static string crashFileFormName
        {
            get
            {
                return _crashFileFormName;
            }
            set
            {
                _crashFileFormName = value;
            }
        }
        static string _crashServerAddress;
        public static string crashServerAddress
        {
            get
            {
                return _crashServerAddress;
            }
            set
            {
                _crashServerAddress = value;
            }
        }

        public static Dictionary<string, string> crashPostParams = new Dictionary<string, string>();

        static UploadFile()
        {
            crashFileFormName = "crashrpt";
            crashServerAddress = "http://log.2980.com:28081/upload.php?name=X6";
            LocalNet = false;
        }

        public static string UploadString(string data, string saveName)
        {
          string res = HttpUtil.ProcessRequestData(crashServerAddress, crashFileFormName, data, saveName, crashPostParams, LocalNet);
          Debug.LogFormat("UploadCrashFile [{0}], res : {1}", saveName, res);
          return res;
        }

    }
}
