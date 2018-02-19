using System.Linq;

namespace System
{
	public static class EnumEx
	{
		public static Array GetValues(Type enumType)
		{
			var list =
				from field in enumType.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
				select field.GetValue(null);
			return list.ToArray();
		}

		public static bool HasFlag(this Enum value, Enum flags)
		{
			return (value.GetHashCode() & flags.GetHashCode()) == flags.GetHashCode();
		}
	}
}
