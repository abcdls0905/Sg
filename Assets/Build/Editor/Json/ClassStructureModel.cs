using System.Collections.Generic;

namespace CodeDomDemo
{
    /// <summary>
    /// 类结构模型
    /// </summary>
    internal class ClassStructureModel
    {
       public string ClassName { get; set; }
       public List<PropertyModel> PropertyCollection { get; set; }
    }

   public class PropertyModel
   {

       public PropertyModel(string typeStr,string propertyName)
       {
            string name = propertyName;
            name = name.Substring(0, 1).ToLower() + name.Substring(1);
            this.PropertyText = string.Format("public {0} {1};\n",typeStr, name);
       }

       public string PropertyText { get; private set; }
   }
}
