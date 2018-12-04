using System.Linq;
using Entitas.Utils;

namespace Entitas.CodeGeneration.Plugins {

    public class ContextsGenerator : ICodeGenerator {

        public string name { get { return "Contexts"; } }
        public int priority { get { return 0; } }
        public bool isEnabledByDefault { get { return true; } }
        public bool runInDryMode { get { return true; } }

        const string CONTEXTS_TEMPLATE =
@"public partial class Contexts : Entitas.IContexts {

    public static Contexts Instance {
        get {
            if (_instance == null) {
                _instance = new Contexts();
            }

            return _instance;
        }
        set { _instance = value; }
    }

    static Contexts _instance;

${contextProperties}

    public Entitas.IContext[] allContexts { get { return new Entitas.IContext [] { ${contextList} }; } }

    public Contexts() {
${contextAssignments}

        var postConstructors = System.Linq.Enumerable.Where(
            GetType().GetMethods(),
            method => System.Attribute.IsDefined(method, typeof(Entitas.CodeGeneration.Attributes.PostConstructorAttribute))
        );

        foreach (var postConstructor in postConstructors) {
            postConstructor.Invoke(this, null);
        }
    }

    public void Reset() {
        var contexts = allContexts;
        for (int i = 0; i < contexts.Length; i++) {
            contexts[i].Reset();
        }
        var postResets = System.Linq.Enumerable.Where(
            GetType().GetMethods(),
            method => System.Attribute.IsDefined(method, typeof(Entitas.CodeGeneration.Attributes.PostResetAttribute))
        );

        foreach (var postReset in postResets) {
            postReset.Invoke(this, null);
        }
    }
}
";

        const string CONTEXT_PROPERTY_TEMPLATE = @"    public ${ContextName}Context ${contextName} { get; set; }";
        const string CONTEXT_LIST_TEMPLATE = @"${contextName}";
        const string CONTEXT_ASSIGNMENT_TEMPLATE = @"        ${contextName} = new ${ContextName}Context();";

        public CodeGenFile[] Generate(CodeGeneratorData[] data) {
            var contextNames = data
                .OfType<ContextData>()
                .Select(d => d.GetContextName())
                .OrderBy(contextName => contextName)
                .ToArray();

            return new [] { new CodeGenFile(
                "Contexts.cs",
                generateContextsClass(contextNames),
                GetType().FullName)
            };
        }

        string generateContextsClass(string[] contextNames) {
            var contextProperties = string.Join("\n", contextNames
                .Select(contextName => CONTEXT_PROPERTY_TEMPLATE
                        .Replace("${ContextName}", contextName)
                        .Replace("${contextName}", contextName.LowercaseFirst())
                       ).ToArray());

            var contextList = string.Join(", ", contextNames
                .Select(contextName => CONTEXT_LIST_TEMPLATE
                        .Replace("${contextName}", contextName.LowercaseFirst())
                       ).ToArray());

            var contextAssignments = string.Join("\n", contextNames
                .Select(contextName => CONTEXT_ASSIGNMENT_TEMPLATE
                        .Replace("${ContextName}", contextName)
                        .Replace("${contextName}", contextName.LowercaseFirst())
                       ).ToArray());

            return CONTEXTS_TEMPLATE
                .Replace("${contextProperties}", contextProperties)
                .Replace("${contextList}", contextList)
                .Replace("${contextAssignments}", contextAssignments);
        }
    }
}
