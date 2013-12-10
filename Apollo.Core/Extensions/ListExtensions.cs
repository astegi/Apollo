using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Core.Extensions
{
    public static class ListExtensions
    {

        public static List<List<T>> Partition<T, TKey>(this List<T> list, Func<T, TKey> keySelector)
        {
            List<List<T>> partitions = new List<List<T>>();

            List<T> _list = new List<T>();

            IEnumerable<IGrouping<TKey, T>> groups = list.GroupBy(keySelector);

            foreach (IGrouping<TKey, T> group in groups)
            {
                _list.Clear();
                foreach (T t in group)
                {
                    _list.Add(t);
                }
                
                partitions.Add(_list);
            }

            return partitions;
        }
    }
}
