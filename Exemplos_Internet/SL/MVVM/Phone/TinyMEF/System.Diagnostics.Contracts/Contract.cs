
using System.Collections.Generic;

namespace System.Diagnostics.Contracts
{
	public static class Contract
	{
		public static bool ForAll<T>(IEnumerable<T> collection, Predicate<T> predicate)
		{
			foreach (var item in collection)
				if (!predicate(item))
					return false;
			return true;
		}

		public static void Ensures(bool p)
		{
			if (!p)
				throw new InvalidProgramException();
		}
	}
}
