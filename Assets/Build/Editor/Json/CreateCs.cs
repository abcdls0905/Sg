using System.CodeDom;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.IO;

namespace CodeDomDemo
{
    /// <summary>
    /// 生成代码类
    /// <remarks>一定不能用单例，否则会发生代码结构重复的情况</remarks>
    /// </summary>
    internal class CreateCs
    {

        private CodeCompileUnit unit = new CodeCompileUnit();
        CodeNamespace theNamespace;
        public void Prepare(string NamespeceName)
        {
            theNamespace = new CodeNamespace(NamespeceName);
            unit.Namespaces.Add(theNamespace);

            CodeNamespaceImport SystemImport = new CodeNamespaceImport("System");
            theNamespace.Imports.Add(SystemImport);

            CodeNamespaceImport collectionsImport = new CodeNamespaceImport("System.Collections.Generic");
            theNamespace.Imports.Add(collectionsImport);
        }

        public void AddClass(ClassStructureModel model)
        {
            CodeTypeDeclaration mClass = new CodeTypeDeclaration(model.ClassName);
            mClass.IsClass = true;
            mClass.TypeAttributes = TypeAttributes.Public;
            theNamespace.Types.Add(mClass);

            //通过snippet 生成属性
            if (model.PropertyCollection != null)
            {
                foreach (var item in model.PropertyCollection)
                {
                    CodeSnippetTypeMember snippet = new CodeSnippetTypeMember();

                    //snippet.Comments.Add(new CodeCommentStatement("this is integer property", false));
                    snippet.Text = item.PropertyText;
                    mClass.Members.Add(snippet);
                }
            }
        }

        public void Produce(string ClassName, string FileDirPath)
        {
            if (!System.IO.Directory.Exists(FileDirPath))
            {
                Directory.CreateDirectory(FileDirPath);
            }

            if (!FileDirPath.EndsWith("/"))
            {
                FileDirPath = FileDirPath + "/";
            }

            IndentedTextWriter tw = new IndentedTextWriter(new StreamWriter(string.Format("{0}{1}.cs", FileDirPath, ClassName), false), "    ");
            CodeDomProvider provide = new CSharpCodeProvider();

            provide.GenerateCodeFromCompileUnit(unit, tw, new CodeGeneratorOptions());
            tw.Close();
        }

    }
}
