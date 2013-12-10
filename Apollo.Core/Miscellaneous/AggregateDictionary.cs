using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Apollo.Core.Miscellaneous
{
    class AggregateDictionary : IDictionary
    {
        private ICollection dictionaries;

        /// <summary>
        /// A kulcsok listája
        /// </summary>
        public virtual ICollection Keys
        {
            get
            {
                ArrayList list = new ArrayList();
                foreach (IDictionary dictionary in dictionaries)
                {
                    ICollection collection = dictionary.Keys;
                    if (collection != null)
                    {
                        foreach (object obj in collection)
                        {
                            list.Add(obj);
                        }
                        continue;
                    }
                }
                return list;
            }
        }

        public virtual object SyncRoot
        {
            get
            {
                return this;
            }
        }

        /// <summary>
        /// Az értékek listája
        /// </summary>
        public virtual ICollection Values
        {
            get
            {
                ArrayList list = new ArrayList();
                foreach (IDictionary dictionary in dictionaries)
                {
                    ICollection collection = dictionary.Values;
                    if (collection != null)
                    {
                        foreach (object obj in collection)
                        {
                            list.Add(obj);
                        }
                        continue;
                    }
                }
                return list;
            }
        }

        /// <summary>
        /// A szótárlista mérete rögzített
        /// </summary>
        public virtual bool IsFixedSize { get { return true; } }

        /// <summary>
        /// A szótárlista nem csak olvasható
        /// </summary>
        public virtual bool IsReadOnly { get { return false; } }

        /// <summary>
        /// A szótárlista nem szinkronizált
        /// </summary>
        public virtual bool IsSynchronized { get { return false; } }

        /// <summary>
        /// Beállítja vagy visszatér a szótárlista egyik, kulccsal jelzett értékével
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual object this[object key]
        {
            get
            {
                foreach (IDictionary dictionary in dictionaries)
                {
                    if (dictionary.Contains(key))
                    {
                        return dictionary[key];
                    }
                }
                return null;
            }
            set
            {
                foreach (IDictionary dictionary in dictionaries)
                {
                    if (dictionary.Contains(key))
                    {
                        dictionary[key] = value;
                    }
                }
            }
        }
        
        /// <summary>
        /// A szótárlistában összegyűjtött szótárak elemeinek számával tér vissza
        /// </summary>
        public virtual int Count
        {
            get
            {
                int num = 0;
                foreach (IDictionary dictionary in dictionaries)
                {
                    num += dictionary.Count;
                }
                return num;
            }
        }



        /// <summary>
        /// Új szótárlista
        /// </summary>
        /// <param name="dictionaries"></param>
        public AggregateDictionary(ICollection dictionaries)
        {
            this.dictionaries = dictionaries;
        }


        /// <summary>
        /// Hozzáadás - nem támogatott
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public virtual void Add(object key, object value)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Ürítés - nem támogatott
        /// </summary>
        public virtual void Clear()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Megkeresi, hogy az adott kulcsot tartalmazza-e valamelyik szótár
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual bool Contains(object key)
        {
            foreach (IDictionary dictionary in dictionaries)
            {
                if (dictionary.Contains(key))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Tömbbe másolás - nem támogatott
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        public virtual void CopyTo(Array array, int index)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Visszatér a szótárlista számlálójával
        /// </summary>
        /// <returns></returns>
        public virtual IDictionaryEnumerator GetEnumerator()
        {
            return new DictionaryEnumeratorByKeys(this);
        }

        /// <summary>
        /// Törlés - nem támogatott
        /// </summary>
        /// <param name="key"></param>
        public virtual void Remove(object key)
        {
            throw new NotSupportedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new DictionaryEnumeratorByKeys(this);
        }

        class DictionaryEnumeratorByKeys : IDictionaryEnumerator
        {
            internal DictionaryEnumeratorByKeys(IDictionary properties)
            {
                this.properties = properties;
                keyEnum = properties.Keys.GetEnumerator();
            }

            public bool MoveNext()
            {
                return keyEnum.MoveNext();
            }

            public void Reset()
            {
                keyEnum.Reset();
            }


            public object Current
            {
                get
                {
                    return this.Entry;
                }
            }

            public DictionaryEntry Entry
            {
                get
                {
                    return new DictionaryEntry(Key, Value);
                }
            }

            public object Key
            {
                get
                {
                    return this.keyEnum.Current;
                }
            }

            public object Value
            {
                get
                {
                    return this.properties[this.Key];
                }
            }


            private readonly IEnumerator keyEnum;
            private readonly IDictionary properties;
        }
    }
}
