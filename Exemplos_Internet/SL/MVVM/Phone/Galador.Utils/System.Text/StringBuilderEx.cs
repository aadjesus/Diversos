using System.Text;

namespace System.Text
{
	public static class StringBuilderEx
	{
		public static void Clear(this StringBuilder sb)
		{
			sb.Length = 0;
		}
	}
}
