using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Apollo.Core.Attributes;

namespace Apollo.Core.Extensions
{
    public static class TypeExtensions
    {
        public static bool HasInterface(this Type type, Type interfaceType)
        {
            Type[] interfaces = type.GetInterfaces();
            return (interfaces.Where(x=> x.FullName == interfaceType.FullName).Count() > 0);
        }

        public static Type[] GetInterfaceImplementationTypes(this Assembly assembly, Type interfaceType)
        {
            List<Type> returnValues = new List<Type>();
            
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                if (type.HasInterface(interfaceType))
                    returnValues.Add(type);
            }

            return returnValues.ToArray();
        }
    }
}
