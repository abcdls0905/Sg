using System;
using System.Reflection;
using System.Collections.Generic;

namespace GameUtil
{
    public class ClassSearch
    {
        public Type AttributeType;
        public Type InterfaceType;
        public bool InheritAttribute;
        public Assembly SearchAssembly;
        public ClassSearch(Assembly InAssembly, Type InAttributeType, Type InInterfaceType, bool InInheritAttribute = false)
        {
            SearchAssembly = InAssembly == null ? this.GetType().Assembly : InAssembly;
            AttributeType = InAttributeType;
            InterfaceType = InInterfaceType;
            InheritAttribute = InInheritAttribute;
        }

        public bool Filter(Type type)
        {
            if ((AttributeType == null || type.GetCustomAttributes(this.AttributeType, InheritAttribute).Length > 0) &&
                (InterfaceType == null || InterfaceType.IsAssignableFrom(type)))
                return true;
            return false;
        }

        public IEnumerator<Type> GetEnumerator()
        {
            Type[] types = SearchAssembly.GetTypes();
            if (types != null)
            {
                for (int i = 0; i < types.Length; i++)
                {
                    if (Filter(types[i]))
                        yield return types[i];
                }
            }
        }
    }
}