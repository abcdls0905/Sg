using GameUtil;
using XLua;

namespace Game
{
    [LuaCallCSharp]
    public class VersionManager : Singleton<VersionManager>
    {
        public const string SvnVersionPath = "version.txt";
        public string SvnVersion = "100000";
        public string PackVersion = "0.1.0.0";
        public int PackVersion0 = 0;
        public int PackVersion1 = 1;
        public int PackVersion2 = 0;
        public int PackVersion3 = 0;
        public bool TestPackage = true;
        public bool Proxy = false;
        public string ProxyIP = "";
        public int ProxyPort = 0;
        public bool PatchLua = false;

        public override void Init()
        {
            var binary = GameInterface.ReadRootData(SvnVersionPath);
            if (binary != null)
                SvnVersion = System.Text.Encoding.UTF8.GetString(binary);
        }

        public override void UnInit()
        {

        }
    }
}