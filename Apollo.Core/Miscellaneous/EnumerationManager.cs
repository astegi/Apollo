using System;
using System.Collections.Generic;
using System.Linq;

namespace Apollo.Core.Miscellaneous
{
    public class EnumerationManager
    {
        private static EnumerationManager instance;
        public static EnumerationManager Instance
        {
            get { return instance ?? (instance = new EnumerationManager()); }
        }

        private readonly static Dictionary<Type, Enumeration[]> enumStore = new Dictionary<Type, Enumeration[]>();
        internal static void AddToStore(Type type, int length)
        {
            if (enumStore.ContainsKey(type))
            {
                if (enumStore[type].Length != length)
                    throw new ArgumentException();

                return;
            }
            enumStore.Add(type, new Enumeration[length]);
        }
        internal static void AddEnumToStore(Type type, Enumeration enumeration)
        {
            enumStore[type][enumeration.EnumValue] = enumeration;
        }
        internal static void AddEnumsToStore(Type type, IEnumerable<Enumeration> enumerations)
        {
            enumStore[type] = enumerations.ToArray();
        }


        private EnumerationManager()
        {
            ServiceProvider.Instance.FillEnums();
        }


        public int GetIDFromEnum(Enum value)
        {
            return enumStore[value.GetType()][(int)Convert.ChangeType(value, typeof(int))].StoredValue;
        }
        
        public T GetEnumFromID<T>(int id)
        {
            Type t = typeof(T);
            if (enumStore.ContainsKey(t))
            {
                Enumeration[] enums = enumStore[t];
                Enumeration enumeration = enums.First(x => x.StoredValue == id);
                return (T)Enum.Parse(t, enumeration.EnumValue.ToString());
            }

            throw new ArgumentException();
        }         
    }
}
