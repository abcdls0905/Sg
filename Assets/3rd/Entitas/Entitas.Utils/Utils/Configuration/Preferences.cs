using System.IO;

namespace Entitas.Utils {

    public static class Preferences {

        public static string PATH = UnityEngine.Application.dataPath + "/Entitas.properties";

        public static bool HasProperties() {
            return File.Exists(PATH);
        }

        public static Properties LoadProperties() {
            return new Properties(File.ReadAllText(PATH));
        }

        public static void SaveProperties(Properties properties) {
            File.WriteAllText(PATH, properties.ToString());
        }
    }
}
