using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Galador.Utils
{
	[AttributeUsageAttribute(AttributeTargets.Field, AllowMultiple = true)]
	public class EnumTag : Attribute
	{
		public EnumTag(object o)
		{
			Tag = o;
		}
		public EnumTag(object o, string cat)
			: this(o)
		{
			Category = cat;
		}

		public object Tag { get; private set; }
		public string Category { get; set; }

		// == static helper method below ==

		public static KeyValuePair<string, T>[] GetEnumInfo<T>()
		{
			var efields = typeof(T).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
			return (from e in efields select new KeyValuePair<string, T>(e.Name, (T)e.GetValue(null))).ToArray();
		}

		public static KeyValuePair<string, object>[] GetEnumInfo(Type eType)
		{
			var efields = eType.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
			return (from e in efields select new KeyValuePair<string, object>(e.Name, e.GetValue(null))).ToArray();
		}

		public static EnumTag[] GetTags(object value)
		{
			var fi = value.GetType().GetField(value.ToString());
			var list =
				from a in Attribute.GetCustomAttributes(fi, typeof(EnumTag))
				select (EnumTag)a;
			return list.ToArray();
		}

		public static T GetValue<T>(object tag, string category = null)
		{
			var list =
				from value in GetEnumInfo<T>()
				from atag in GetTags(value.Value)
				where Equals(atag.Tag, tag) && atag.Category == category
				select value.Value;
			return list.First();
		}

		public static bool TryGetValue<T>(ref T value, object tag, string category = null)
		{
			var list =
				from avalue in GetEnumInfo<T>()
				from atag in GetTags(avalue.Value)
				where Equals(atag.Tag, tag) && atag.Category == category
				select avalue.Value;
			var a = list.ToArray();
			if (a.Length == 0)
			{
				return false;
			}
			else
			{
				value = a[0];
				return true;
			}
		}

		#region backward compatibility methods: just a LINQ query on the other methods

		public static object GetTag(Enum value, string category = null)
		{
			var list =
				from tag in GetTags(value)
				where tag.Category == category
				select tag.Tag;
			return list.FirstOrDefault();
		}

		public static object GetValue(Type tenum, object tag, string category = null)
		{
			var list =
				from avalue in GetEnumInfo(tenum)
				from atag in GetTags(avalue.Value)
				where Equals(atag.Tag, tag) && atag.Category == category
				select avalue.Value;
			return list.FirstOrDefault();
		}

		public static bool TryParse<T>(string value, out T result)
		{
			var list =
				from avalue in GetEnumInfo<T>()
				where value == avalue.Key
				select avalue;
			var a = list.ToArray();
			if (a.Length == 0)
			{
				result = default(T);
				return false;
			}
			else
			{
				result = a[0].Value;
				return true;
			}
		}

		public static object Parse(Type enumType, string svalue)
		{
			var list =
				from avalue in GetEnumInfo(enumType)
				where svalue == avalue.Key
				select avalue;
			var a = list.ToArray();
			if (a.Length == 0)
				throw new ArgumentOutOfRangeException();
			return a[0].Value;
		}

		#endregion
	}
}
