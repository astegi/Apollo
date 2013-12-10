using System;
using System.Collections.Generic;
using System.Linq;
using Apollo.Business.Attributes;

namespace Apollo.Business
{
    /// <summary>
    /// Business Logic kezelő osztályok közötti kapcsolattartó entitás
    /// </summary>
    public static class InnerProxy
    {
        private static Dictionary<Type, object> entities;

        public static void Initialize()
        {
            entities = new Dictionary<Type, object>();
            Type[] types = typeof(InnerProxy).Assembly.GetTypes();
            types = types.Where(x => x.GetCustomAttributes(typeof(ProxyImplementationAttribute), false).Length > 0).ToArray();
            foreach (Type type in types)
            {
                entities.Add(type, Activator.CreateInstance(type));
            }
        }

        public static T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        private static object GetService(Type type)
        {
            return entities[type];
        }
    }
}
