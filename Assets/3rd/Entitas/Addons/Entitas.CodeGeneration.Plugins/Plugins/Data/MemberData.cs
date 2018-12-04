namespace Entitas.CodeGeneration.Plugins {

    public class MemberData {

        public readonly string type;
        public readonly string name;
        public readonly bool primary;

        public MemberData(string type, string name, bool primary) {
            this.type = type;
            this.name = name;
            this.primary = primary;
        }
    }
}
