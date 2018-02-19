// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------

namespace System.Collections.ObjectModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;

    [Serializable]
    public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary
    {
        private readonly IDictionary<TKey, TValue> m_dictionary;
        [NonSerialized]
        private Object m_syncRoot;

        public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            m_dictionary = dictionary;
        }

        protected IDictionary<TKey, TValue> Dictionary
        {
            get { return m_dictionary; }
        }

        #region IDictionary<TKey, TValue> Members

        public bool ContainsKey(TKey key)
        {
            return m_dictionary.ContainsKey(key);
        }

        public ICollection<TKey> Keys
        {
            get
            {
                ICollection<TKey> keys = m_dictionary.Keys;
                if (!keys.IsReadOnly)
                {
                    return new ReadOnlyWrapper<TKey>(keys);
                }
                return keys;
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return m_dictionary.TryGetValue(key, out value);
        }

        public ICollection<TValue> Values
        {
            get
            {
                ICollection<TValue> values = m_dictionary.Values;
                if (!values.IsReadOnly)
                {
                    return new ReadOnlyWrapper<TValue>(values);
                }
                return values;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                return m_dictionary[key];
            }
        }

        void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            throw new NotSupportedException();
        }

        bool IDictionary<TKey, TValue>.Remove(TKey key)
        {
            throw new NotSupportedException();
        }

        TValue IDictionary<TKey, TValue>.this[TKey key]
        {
            get
            {
                return m_dictionary[key];
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        #endregion

        #region ICollection<KeyValuePair<TKey, TValue>> Members

        public int Count
        {
            get { return m_dictionary.Count; }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return m_dictionary.Contains(item);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            m_dictionary.CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get { return true; }
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException();
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Clear()
        {
            throw new NotSupportedException();
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region IEnumerable<KeyValuePair<TKey, TValue>> Members

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return m_dictionary.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)m_dictionary).GetEnumerator();
        }

        #endregion

        #region IDictionary Members

        private static bool IsCompatibleKey(object key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            return key is TKey;
        }

        void IDictionary.Add(object key, object value)
        {
            throw new NotSupportedException();
        }

        void IDictionary.Clear()
        {
            throw new NotSupportedException();
        }

        bool IDictionary.Contains(object key)
        {
            return IsCompatibleKey(key) && ContainsKey((TKey)key);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            IDictionary d = m_dictionary as IDictionary;
            if (d != null)
            {
                return d.GetEnumerator();
            }
            return new DictionaryEnumerator(m_dictionary);
        }

        bool IDictionary.IsFixedSize
        {
            get { return true; }
        }

        bool IDictionary.IsReadOnly
        {
            get { return true; }
        }

        ICollection IDictionary.Keys
        {
            get
            {
                ICollection keys = Keys as ICollection;
                if (keys == null)
                {
                    return new ReadOnlyWrapper<TKey>(m_dictionary.Keys);
                }
                return keys;
            }
        }

        void IDictionary.Remove(object key)
        {
            throw new NotSupportedException();
        }

        ICollection IDictionary.Values
        {
            get
            {
                ICollection values = Values as ICollection;
                if (values == null)
                {
                    return new ReadOnlyWrapper<TValue>(m_dictionary.Values);
                }
                return values;
            }
        }

        object IDictionary.this[object key]
        {
            get
            {
                if (IsCompatibleKey(key))
                {
                    return this[(TKey)key];
                }
                return null;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (array.Rank != 1)
            {
                throw new ArgumentException();
            }

            if (array.GetLowerBound(0) != 0)
            {
                throw new ArgumentException();
            }

            if (index < 0 || index > array.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (array.Length - index < Count)
            {
                throw new ArgumentException();
            }

            KeyValuePair<TKey, TValue>[] pairs = array as KeyValuePair<TKey, TValue>[];
            if (pairs != null)
            {
                m_dictionary.CopyTo(pairs, index);
            }
            else
            {
                DictionaryEntry[] dictEntryArray = array as DictionaryEntry[];
                if (dictEntryArray != null)
                {
                    foreach (var item in m_dictionary)
                    {
                        dictEntryArray[index++] = new DictionaryEntry(item.Key, item.Value);
                    }
                }
                else
                {
                    object[] objects = array as object[];
                    if (objects == null)
                    {
                        throw new ArgumentException();
                    }

                    try
                    {
                        foreach (var item in m_dictionary)
                        {
                            objects[index++] = new KeyValuePair<TKey, TValue>(item.Key, item.Value);
                        }
                    }
                    catch (ArrayTypeMismatchException)
                    {
                        throw new ArgumentException();
                    }
                }
            }
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        object ICollection.SyncRoot
        {
            get
            {
                if (m_syncRoot == null)
                {
                    ICollection c = m_dictionary as ICollection;
                    if (c != null)
                    {
                        m_syncRoot = c.SyncRoot;
                    }
                    else
                    {
                        System.Threading.Interlocked.CompareExchange<Object>(ref m_syncRoot, new Object(), null);
                    }
                }
                return m_syncRoot;
            }
        }

        [Serializable]
        private struct DictionaryEnumerator : IDictionaryEnumerator
        {
            private readonly IDictionary<TKey, TValue> m_dictionary;
            private IEnumerator<KeyValuePair<TKey, TValue>> m_enumerator;

            public DictionaryEnumerator(IDictionary<TKey, TValue> dictionary)
            {
                m_dictionary = dictionary;
                m_enumerator = m_dictionary.GetEnumerator();
            }

            public DictionaryEntry Entry
            {
                get { return new DictionaryEntry(m_enumerator.Current.Key, m_enumerator.Current.Value); }
            }

            public object Key
            {
                get { return m_enumerator.Current.Key; }
            }

            public object Value
            {
                get { return m_enumerator.Current.Value; }
            }

            public object Current
            {
                get { return Entry; }
            }

            public bool MoveNext()
            {
                return m_enumerator.MoveNext();
            }

            public void Reset()
            {
                m_enumerator.Reset();
            }
        }

        #endregion

        // For wrapping non-readonly Keys, Values collections
        // Not used for standard dictionaries, which return read-only Keys and Values
        [Serializable]
        private sealed class ReadOnlyWrapper<T> : ICollection<T>, ICollection
        {
            private readonly ICollection<T> m_collection;
            [NonSerialized]
            private Object m_syncRoot;

            internal ReadOnlyWrapper(ICollection<T> collection)
            {
                if (collection == null)
                {
                    throw new ArgumentNullException("collection");
                }
                m_collection = collection;
            }

            #region ICollection<T> Members

            public void Add(T item)
            {
                throw new NotSupportedException();
            }

            public void Clear()
            {
                throw new NotSupportedException();
            }

            public bool Contains(T item)
            {
                return m_collection.Contains(item);
            }

            public void CopyTo(T[] array, int arrayIndex)
            {
                m_collection.CopyTo(array, arrayIndex);
            }

            public int Count
            {
                get { return m_collection.Count; }
            }

            public bool IsReadOnly
            {
                get { return true; }
            }

            public bool Remove(T item)
            {
                throw new NotSupportedException();
            }

            #endregion

            #region IEnumerable<T> Members

            public IEnumerator<T> GetEnumerator()
            {
                return m_collection.GetEnumerator();
            }

            #endregion

            #region IEnumerable Members

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)m_collection).GetEnumerator();
            }

            #endregion

            #region ICollection Members

            public void CopyTo(Array array, int index)
            {
                if (array == null)
                {
                    throw new ArgumentNullException("array");
                }

                if (array.Rank != 1)
                {
                    throw new ArgumentException();
                }

                if (array.GetLowerBound(0) != 0)
                {
                    throw new ArgumentException();
                }

                if (index < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                if (array.Length - index < Count)
                {
                    throw new ArgumentException();
                }

                T[] items = array as T[];
                if (items != null)
                {
                    m_collection.CopyTo(items, index);
                }
                else
                {
                    //
                    // Catch the obvious case assignment will fail.
                    // We can found all possible problems by doing the check though.
                    // For example, if the element type of the Array is derived from T,
                    // we can't figure out if we can successfully copy the element beforehand.
                    //
                    Type targetType = array.GetType().GetElementType();
                    Type sourceType = typeof(T);
                    if (!(targetType.IsAssignableFrom(sourceType) || sourceType.IsAssignableFrom(targetType)))
                    {
                        throw new ArgumentException();
                    }

                    //
                    // We can't cast array of value type to object[], so we don't support 
                    // widening of primitive types here.
                    //
                    object[] objects = array as object[];
                    if (objects == null)
                    {
                        throw new ArgumentException();
                    }

                    try
                    {
                        foreach (var item in m_collection)
                        {
                            objects[index++] = item;
                        }
                    }
                    catch (ArrayTypeMismatchException)
                    {
                        throw new ArgumentException();
                    }
                }

            }

            public bool IsSynchronized
            {
                get { return false; }
            }

            public object SyncRoot
            {
                get
                {
                    if (m_syncRoot == null)
                    {
                        ICollection c = m_collection as ICollection;
                        if (c != null)
                        {
                            m_syncRoot = c.SyncRoot;
                        }
                        else
                        {
                            System.Threading.Interlocked.CompareExchange<Object>(ref m_syncRoot, new Object(), null);
                        }
                    }
                    return m_syncRoot;
                }
            }

            #endregion
        }
    }
}

