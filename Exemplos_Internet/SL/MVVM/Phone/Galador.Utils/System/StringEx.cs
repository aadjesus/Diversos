using System.Collections.Generic;
using System.Linq;

namespace System
{
	public static class StringEx
	{
		public static bool IsNullOrWhiteSpace(string s)
		{
			if (s == null)
				return true;
			foreach (var c in s)
				if (!char.IsWhiteSpace(c))
					return false;
			return true;
		}

		public static string Join<T>(string separator, IEnumerable<T> values)
		{
			return string.Join(separator, (from v in values select v == null ? "" : v.ToString()).ToArray());
		}
 
	}
}
