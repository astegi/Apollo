using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apollo.Core.Entities;
using Apollo.DataAccess;

namespace Apollo.Core.Cache
{
    public abstract class Cache<TDataType, TItemType>: IEnumerable<TItemType>, ICollection<TItemType>, IList<TItemType>
        where TDataType: DataCore
        where TItemType: Convertable<TDataType>
    {
        Dictionary<int, TItemType> items;

        public Cache()
        {
            items = new Dictionary<int, TItemType>();
        }

        public void Add(TDataType item)
        {
            TItemType guiItem = Convertable<TDataType>.Convert<TItemType>(item);
            items.Add(item.PrimaryID, guiItem);
        }

        public void AddRange(IEnumerable<TDataType> items)
        {
            foreach (TDataType item in items)
            {
                Add(item);
            }
        }

        public IEnumerator<TItemType> GetEnumerator()
        {
            return items.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return items.Values.GetEnumerator();
        }

        public void Add(TItemType item)
        {
            items.Add(item.Data.PrimaryID, item);
        }

        public void Clear()
        {
            items.Clear();
        }

        public bool Contains(TItemType item)
        {
            return items.ContainsKey(item.Data.PrimaryID);
        }

        public void CopyTo(TItemType[] array, int arrayIndex)
        {
            
        }

        public int Count
        {
            get { return items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(TItemType item)
        {
            return items.Remove(item.Data.PrimaryID);
        }

        public int IndexOf(TItemType item)
        {
            int i=0;
            while (i< items.Count && items.ElementAt(i).Key != item.Data.PrimaryID)
                i++;
            return (i < items.Count ? i : -1);
        }

        public void Insert(int index, TItemType item)
        {
            Add(item);
        }

        public void RemoveAt(int index)
        {
            KeyValuePair<int, TItemType> item = items.ElementAt(index);
            items.Remove(item.Key);
        }

        public TItemType this[int index]
        {
            get
            {
                return items[index];
            }
            set
            {
                items[index] = value;
            }
        }


        public TItemType Find(Predicate<TDataType> selector)
        {
            int i = 0;
            while (i < items.Count && !selector.Invoke(items.ElementAt(i).Value.Data))
                i++;
            return (i < items.Count ? items.ElementAt(i).Value : null);
                
        }
        public IEnumerable<TItemType> FindAll(Predicate<TDataType> selector)
        {
            foreach (TItemType item in items.Values)
            {
                if (selector.Invoke(item.Data))
                    yield return item;
            }
        }

        public bool Any(Predicate<TDataType> selector)
        {
            int i = 0;
            while (i < items.Count && !selector.Invoke(items.ElementAt(i).Value.Data))
                i++;

            return (i < items.Count);

        }
        public bool All(Predicate<TDataType> selector)
        {
            bool isAll = true;
            foreach (TItemType item in items.Values)
            {
                if (!selector.Invoke(item.Data))
                    isAll = false;
            }
            return isAll;
        }
    }
}
