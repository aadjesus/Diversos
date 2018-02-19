using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cx.Exceptions
{
	public static class Verify
	{
		public static void IsTrue(bool b, string message)
		{
			if (!b)
			{
				throw new CxException(message);
			}
		}

		public static void IsNotNull(object foo, string message)
		{
			if (foo == null)
			{
				throw new CxException(message);
			}
		}

		public static void ValNotIn(ICollection<string> list, string val, string message)
		{
			if (list.Contains(val))
			{
				throw new CxException(message);
			}
		}

		public static void IsOfType<T>(object obj, string message)
		{
			if (!(obj is T))
			{
				throw new CxException(message);
			}
		}
	}
}
