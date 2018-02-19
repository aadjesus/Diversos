//*****************************************************************
//*****************Generated with SharpDeveloper*******************
//*****************************************************************

using System;
using System.Collections;

using sharpPDF.Elements;

namespace sharpPDF.Collections
{
	/// <summary>
	/// Class that represents a collection of pdfImageReference objects
	/// </summary>
	public class imageHashtable : IDictionary, ICollection, IEnumerable, ICloneable
	{
		protected Hashtable innerHash;
		
		#region "Constructors"
		public  imageHashtable()
		{
			innerHash = new Hashtable();
		}
		
		public imageHashtable(imageHashtable original)
		{
			innerHash = new Hashtable(original.innerHash);
		}
		
		public imageHashtable(IDictionary dictionary)
		{
			innerHash = new Hashtable(dictionary);
		}
		
		public imageHashtable(int capacity)
		{
			innerHash = new Hashtable(capacity);
		}
		
		public imageHashtable(IDictionary dictionary, float loadFactor)
		{
			innerHash = new Hashtable(dictionary, loadFactor);
		}
		
		public imageHashtable(IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable(codeProvider, comparer);
		}
		
		public imageHashtable(int capacity, int loadFactor)
		{
			innerHash = new Hashtable(capacity, loadFactor);
		}
		
		public imageHashtable(IDictionary dictionary, IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable(dictionary, codeProvider, comparer);
		}
		
		public imageHashtable(int capacity, IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable(capacity, codeProvider, comparer);
		}
		
		public imageHashtable(IDictionary dictionary, float loadFactor, IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable(dictionary, loadFactor, codeProvider, comparer);
		}
		
		public imageHashtable(int capacity, float loadFactor, IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable(capacity, loadFactor, codeProvider, comparer);
		}
		#endregion

		#region Implementation of IDictionary
		public imageHashtableEnumerator GetEnumerator()
		{
			return new imageHashtableEnumerator(this);
		}
		
		System.Collections.IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new imageHashtableEnumerator(this);
		}
		
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		
		public void Remove(string key)
		{
			innerHash.Remove(key);
		}
		
		void IDictionary.Remove(object key)
		{
			Remove ((string)key);
		}
		
		public bool Contains(string key)
		{
			return innerHash.Contains(key);
		}
		
		bool IDictionary.Contains(object key)
		{
			return Contains((string)key);
		}
		
		public void Clear()
		{
			innerHash.Clear();		
		}
		
		public void Add(string key, pdfImageReference value)
		{
			innerHash.Add(key, value);
		}
		
		void IDictionary.Add(object key, object value)
		{
			Add ((string)key, (pdfImageReference)value);
		}
		
		public bool IsReadOnly {
			get {
				return innerHash.IsReadOnly;
			}
		}
		
		public pdfImageReference this[string key] {
			get {
				return (pdfImageReference) innerHash[key];
			}
			set {
				innerHash[key] = value;
			}
		}
		
		object IDictionary.this[object key] {
			get {
				return this[(string)key];
			}
			set {
				this[(string)key] = (pdfImageReference)value;
			}
		}
		
		public System.Collections.ICollection Values {
			get {
				return innerHash.Values;
			}
		}
		
		public System.Collections.ICollection Keys {
			get {
				return innerHash.Keys;
			}
		}
		
		public bool IsFixedSize {
			get {
				return innerHash.IsFixedSize;
			}
		}
		#endregion
		
		#region Implementation of ICollection
		public void CopyTo(System.Array array, int index)
		{
			innerHash.CopyTo (array, index);
		}
		
		public bool IsSynchronized {
			get {
				return innerHash.IsSynchronized;
			}
		}
		
		public int Count {
			get {
				return innerHash.Count;
			}
		}
		
		public object SyncRoot {
			get {
				return innerHash.SyncRoot;
			}
		}
		#endregion
		
		#region Implementation of ICloneable
		public imageHashtable Clone()
		{
			imageHashtable clone = new imageHashtable();
			clone.innerHash = (Hashtable) innerHash.Clone();
			return clone;
		}
		
		object ICloneable.Clone()
		{
			return Clone();
		}
		#endregion
		
		#region "HashTable Methods"
		public bool ContainsKey(string key)
		{
			return innerHash.ContainsKey(key);
		}
		
		public bool ContainsValue(pdfImageReference value)
		{
			return innerHash.ContainsValue(value);
		}
		
		public static imageHashtable Synchronized(imageHashtable nonSync)
		{
			imageHashtable sync = new imageHashtable();
			sync.innerHash = Hashtable.Synchronized(nonSync.innerHash);
			return sync;
		}
		#endregion

		internal Hashtable InnerHash {
			get {
				return innerHash;
			}
		}
	}
	
	/// <summary>
	/// Enumerator of the imageHashtable's collection
	/// </summary>
	public class imageHashtableEnumerator : IDictionaryEnumerator
	{
		private IDictionaryEnumerator innerEnumerator;
		
		internal imageHashtableEnumerator(imageHashtable enumerable)
		{
			innerEnumerator = enumerable.InnerHash.GetEnumerator();
		}
		
		#region Implementation of IDictionaryEnumerator
		public string Key {
			get {
				return (string)innerEnumerator.Key;
			}
		}
		
		object IDictionaryEnumerator.Key {
			get {
				return Key;
			}
		}
		
		public pdfImageReference Value {
			get {
				return (pdfImageReference)innerEnumerator.Value;
			}
		}
		
		object IDictionaryEnumerator.Value {
			get {
				return Value;
			}
		}
		
		public System.Collections.DictionaryEntry Entry {
			get {
				return innerEnumerator.Entry;
			}
		}
		#endregion
		
		#region Implementation of IEnumerator
		public void Reset()
		{
			innerEnumerator.Reset();
		}
		
		public bool MoveNext()
		{
			return innerEnumerator.MoveNext();
		}
		
		public object Current {
			get {
				return innerEnumerator.Current;
			}
		}
		#endregion
	}
}
