using System;
using System.Collections.Generic;

namespace Cx.Common
{
	/// <summary>
	/// A dictionary with an indexer that produces an informative KeyNotFoundException message.
	/// </summary>
	public class DiagnosticDictionary<TKey, TValue> : Dictionary<TKey, TValue>
	{
		protected object tag;
		protected string name = "unknown";

		/// <summary>
		/// Gets/sets an object that you can associate with the dictionary.
		/// </summary>
		public object Tag
		{
			get { return tag; }
			set { tag = value; }
		}

		/// <summary>
		/// The dictionary name.  The default is "unknown".  Used to enhance the KeyNotFoundException.
		/// </summary>
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		/// <summary>
		/// Parameterless constructor.
		/// </summary>
		public DiagnosticDictionary()
		{
		}

		/// <summary>
		/// Constructor that takes a name.
		/// </summary>
		public DiagnosticDictionary(string name)
		{
			this.name = name;
		}

		public DiagnosticDictionary(DiagnosticDictionary<TKey, TValue> source)
			: base(source)
		{
		}

		/// <summary>
		/// Indexer that produces a more useful KeyNotFoundException.
		/// </summary>
		public new TValue this[TKey key]
		{
			get
			{
				try
				{
					return base[key];
				}
				catch (KeyNotFoundException)
				{
					throw new KeyNotFoundException("The key '" + key.ToString() + "' was not found in the dictionary '"+name+"'");
				}
			}

			set	{ base[key] = value; }
		}

		/// <summary>
		/// This method is provided because the Dictionary<> before is to return the default value if the key is not found.
		/// This is of course unavoidable because of the use of the "out" parameter, but there are many times when what we
		/// really want is to leave to value instance untouched when the key can't be found, which is what this method does.
		/// </summary>
		public bool TryGetValue(TKey key, ref TValue val)
		{
			TValue valOut;
			bool ret;
			
			if (ret = TryGetValue(key, out valOut))
			{
				val=valOut;
			}

			return ret;
		}
	}

/*
	// extension method example:
	public static class DiagnosticDictionary
	{
		public static string SafeItem(this Dictionary<string, string> d, string key)
	    {
	        try
	        {
	            return d[key];
	        }
	        catch (KeyNotFoundException)
	        {
	            throw new KeyNotFoundException("The key '" + key + "' was not found in the dictionary.");
	        }
	    }
	}
 */ 
}
