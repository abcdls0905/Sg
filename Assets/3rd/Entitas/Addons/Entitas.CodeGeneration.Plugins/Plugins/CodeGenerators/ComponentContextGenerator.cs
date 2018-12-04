using System.Collections.Generic;
using System.IO;
using System.Linq;
using Entitas.Utils;

namespace Entitas.CodeGeneration.Plugins {

    public class ComponentContextGenerator : ICodeGenerator, IConfigurable {

        public string name { get { return "Component (Context API)"; } }
        public int priority { get { return 0; } }
        public bool isEnabledByDefault { get { return true; } }
        public bool runInDryMode { get { return true; } }

        public Dictionary<string, string> defaultProperties { get { return _ignoreNamespacesConfig.defaultProperties; } }

        readonly IgnoreNamespacesConfig _ignoreNamespacesConfig = new IgnoreNamespacesConfig();

        const string STANDARD_COMPONENT_TEMPLATE =
@"public partial class ${ContextName}Context {

    public ${ComponentType} ${componentName} { get { return SingleEntity.${componentName}; } }
    public bool has${ComponentName} { get { return SingleEntity.has${ComponentName}; } }

    public ${ComponentType} Add${ComponentName}(${memberArgs}) {
        var entity = SingleEntity;
        var component = entity.Add${ComponentName}(${methodArgs});
        return component;
    }

    public void Remove${ComponentName}() {
        SingleEntity.Remove${ComponentName}();
    }
}
";

        const string MEMBER_ARGS_TEMPLATE =
@"${MemberType} new${MemberName}";

        const string METHOD_ARGS_TEMPLATE =
@"new${MemberName}";

        const string FLAG_COMPONENT_TEMPLATE =
@"public partial class ${ContextName}Context {

    public bool ${prefixedComponentName} {
        get { return SingleEntity.${prefixedComponentName}; }
        set { SingleEntity.${prefixedComponentName} = value; }
    }
}
";

        public void Configure(Properties properties) {
            _ignoreNamespacesConfig.Configure(properties);
        }

        public CodeGenFile[] Generate(CodeGeneratorData[] data) {
            return data
                .OfType<ComponentData>()
                .Where(d => d.ShouldGenerateMethods())
                .Where(d => d.IsUnique())
                .SelectMany(d => generateExtensions(d))
                .ToArray();
        }

        CodeGenFile[] generateExtensions(ComponentData data) {
            return data.GetContextNames()
                       .Select(contextName => generateExtension(contextName, data))
                       .ToArray();
        }

        CodeGenFile generateExtension(string contextName, ComponentData data) {
            var memberData = data.GetMemberData();
            var componentName = data.GetFullTypeName().ToComponentName(_ignoreNamespacesConfig.ignoreNamespaces);
//             var template = memberData.Length == 0
//                                       ? FLAG_COMPONENT_TEMPLATE
//                                       : STANDARD_COMPONENT_TEMPLATE;
            var template = STANDARD_COMPONENT_TEMPLATE;

            var fileContent = template
                .Replace("${ContextName}", contextName)
                .Replace("${ComponentType}", data.GetFullTypeName())
                .Replace("${ComponentName}", componentName)
                .Replace("${componentName}", componentName.LowercaseFirst())
                .Replace("${prefixedComponentName}", data.GetCustomComponentPrefix().LowercaseFirst() + componentName)
                .Replace("${memberArgs}", getMemberArgs(memberData))
                .Replace("${methodArgs}", getMethodArgs(memberData));

            return new CodeGenFile(
                contextName + Path.DirectorySeparatorChar +
                "Components" + Path.DirectorySeparatorChar +
                contextName + componentName.AddComponentSuffix() + ".cs",
                fileContent,
                GetType().FullName
            );
        }

        string getMemberArgs(MemberData[] memberData) {
            var args = memberData
                .Where(info => info.primary)
                .Select(info => MEMBER_ARGS_TEMPLATE
                        .Replace("${MemberType}", info.type)
                        .Replace("${MemberName}", info.name.UppercaseFirst()))
                .ToArray();

            return string.Join(", ", args);
        }

        string getMethodArgs(MemberData[] memberData) {
            var args = memberData
                .Where(info => info.primary)
                .Select(info => METHOD_ARGS_TEMPLATE.Replace("${MemberName}", info.name.UppercaseFirst()))
                .ToArray();

            return string.Join(", ", args);
        }
    }
}
