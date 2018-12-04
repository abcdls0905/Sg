using Game;
using System;
using System.IO;
using UnityEngine;

namespace GameBuild
{
    public static class ProtobufBuild
    {
        static string[] _protos =
        {
            "gamemsg",
            "battlemsg",
            "gatewaypb",
            "replaymsg",
        };
        static string DestDir = Application.streamingAssetsPath + "/protobuf";

        static string build_csharp =
@"cd Tools/Protobuf
protoc -I=${SRC_PATH} --csharp_out=${DST_PATH} ${PROTO_PATH}.proto";

        static string build_pb =
@"cd Assets/Protobuf
..\..\Tools\Protobuf\protoc -o ${PROTO_NAME}.pb ${PROTO_NAME}.proto";


        static string[] ReplaceStr = {
            "[(gogoproto.nullable) = false]",
            "import \"github.com/gogo/protobuf/gogoproto/gogo.proto\"",
            "option (gogoproto.gostring_all) = true;",
            "option (gogoproto.marshaler_all) = true;",
            "option (gogoproto.sizer_all) = true;",
            "option (gogoproto.unmarshaler_all) = true;",
            "option (gogoproto.goproto_getters_all) = false;",
            "option (gogoproto.goproto_enum_prefix_all) = false;",
        };

        static string cacheStr = string.Empty;
        public static void BeforeGen(string path)
        {
            cacheStr = File.ReadAllText(path + ".proto");
            string str = cacheStr;
            foreach(var r in ReplaceStr)
                str = str.Replace(r, "");
            File.WriteAllText(path + ".proto", str);
        }

        public static void AfterGen(string path)
        {
            File.WriteAllText(path + ".proto", cacheStr);
        }

        public static void GenCSharp()
        {
            BuildUtility.ClearConsole();

            string path = Path.Combine(Application.dataPath, "Protobuf");
            foreach (var proto in _protos)
            {
                BeforeGen(Path.Combine(path, proto));

                // Build CSharp File
                BuildUtility.ExecuteCmd(build_csharp
                    .Replace("${SRC_PATH}", path)
                    .Replace("${DST_PATH}", path)
                    .Replace("${PROTO_PATH}", Path.Combine(path, proto)));
                // Build Pb File
                BuildUtility.ExecuteCmd(build_pb
                    .Replace("${PROTO_NAME}", proto));

                AfterGen(Path.Combine(path, proto));
            }
        }

        public static void Build()
        {
            FileManager.DeleteDirectory(DestDir);
            FileManager.CreateDirectory(DestDir);
            string path = Path.Combine(Application.dataPath, "Protobuf");
            foreach (var proto in _protos)
            {
                string src = Path.Combine(path, proto + ".pb");
                string dst = Path.Combine(DestDir, proto + ".pb");
                FileManager.CopyFile(src, dst);
            }
        }
    }
}