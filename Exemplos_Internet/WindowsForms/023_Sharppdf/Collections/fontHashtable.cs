//*****************************************************************
//*****************Generated with SharpDeveloper*******************
//*****************************************************************

using System;
using System.Collections;

using sharpPDF.Fonts;

namespace sharpPDF.Collections
{
	/// <summary>
	/// Class that represents a collection of pdfAbstractFont objects
	/// </summary>
	public class fontHashtable : IDictionary, ICollection, IEnumerable, ICloneable
	{

		protected Hashtable innerHash;
		
		#region "Constructors"

		public  fontHashtable()
		{
			innerHash = new Hashtable();
		}

		public fontHashtable(fontHashtable original)
		{
			innerHash = new Hashtable(original.innerHash);
		}

		public fontHashtable(IDictionary dictionary)
		{
			innerHash = new Hashtable(dictionary);
		}

		public fontHashtable(int capacity)
		{
			innerHash = new Hashtable(capacity);
		}

		public fontHashtable(IDictionary dictionary, float loadFactor)
		{
			innerHash = new Hashtable(dictionary, loadFactor);
		}
		
		public fontHashtable(IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable(codeProvider, comparer);
		}
		
		public fontHashtable(int capacity, int loadFactor)
		{
			innerHash = new Hashtable(capacity, loadFactor);
		}
		
		public fontHashtable(IDictionary dictionary, IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable(dictionary, codeProvider, comparer);
		}
		
		public fontHashtable(int capacity, IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable(capacity, codeProvider, comparer);
		}
		
		public fontHashtable(IDictionary dictionary, float loadFactor, IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable(dictionary, loadFactor, codeProvider, comparer);
		}
		
		public fontHashtable(int capacity, float loadFactor, IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable(capacity, loadFactor, codeProvider, comparer);
		}
		#endregion

		#region Implementation of IDictionary
		public fontHashtableEnumerator GetEnumerator()
		{
			return new fontHashtableEnumerator(this);
		}
		
		System.Collections.IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new fontHashtableEnumerator(this);
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
		
		public void Add(string key, pdfAbstractFont value)
		{
			innerHash.Add(key, value);
		}
		
		void IDictionary.Add(object key, object value)
		{
			Add ((string)key, (pdfAbstractFont)value);
		}
		
		public bool IsReadOnly {
			get {
				return innerHash.IsReadOnly;
			}
		}
		
		public pdfAbstractFont this[string key] {
			get {
				return (pdfAbstractFont) innerHash[key];
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
				this[(string)key] = (pdfAbstractFont)value;
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
		public fontHashtable Clone()
		{
			fontHashtable clone = new fontHashtable();
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
		
		public bool ContainsValue(pdfAbstractFont value)
		{
			return innerHash.ContainsValue(value);
		}
		
		public static fontHashtable Synchronized(fontHashtable nonSync)
		{
			fontHashtable sync = new fontHashtable();
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
	/// Enumerator of the fontHashtable's collection
	/// </summary>
	public class fontHashtableEnumerator : IDictionaryEnumerator
	{
		private IDictionaryEnumerator innerEnumerator;
		
		internal fontHashtableEnumerator(fontHashtable enumerable)
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
		
		public pdfAbstractFont Value {
			get {
				return (pdfAbstractFont)innerEnumerator.Value;
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
