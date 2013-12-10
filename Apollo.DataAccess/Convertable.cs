using System;
using System.Reflection;

namespace Apollo.DataAccess
{
    [Serializable]
    public abstract class Convertable<T>
        where T: DataCore
    {
        private T data;
        public T Data
        {
            get { return data; }
            set { data = value; }
        }

        public Convertable()
        {

        }

        public Convertable(T item)
        {
            data = item;
        }

        public static implicit operator T(Convertable<T> item)
        {
            return item.Data;
        }

        public static implicit operator Convertable<T>(T item)
        {
            return item;
        }

        public static TDataType Convert<TDataType>(T item)
            where TDataType: Convertable<T>
        {
            Assembly asm = typeof(TDataType).Assembly;

            Type dataType = typeof(T);
            Type convertableType = typeof(Convertable<T>);
            Type[] types = asm.GetTypes();
            
            object obj = null;
            foreach (Type type in types)
            {
                if (!type.IsSubclassOf(convertableType))
                    continue;

                PropertyInfo pi = type.GetProperty("Data");
                if (pi.PropertyType.FullName == dataType.FullName)
                {
                    obj = Activator.CreateInstance(type, item);
                    break;
                }
            }
            if (obj == null)
                throw new NotImplementedException();
            

            return (TDataType)obj;
        }

        public static Type BaseType
        {
            get { return typeof(T); }
        }
    }
}
