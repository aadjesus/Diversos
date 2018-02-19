using System;
using System.Collections;

namespace testParser
{
	#region Blabla
	using System.IO;
	/// <summary>
	/// Summary description for Class2.
	/// </summary>
	abstract public class Class2: object, System.Collections.ICollection
	{
		
		
		
		private int i,j;
		public static string s="bla";
		protected System.Xml.XmlTextReader reader;
		internal char[][] arr;
		protected internal object bat;
		public static char[,] twodim;
		public char* pointer;
		
		public const int x=1;
		
		
	}

	
	public interface Int1: System.Collections.IList
	{
	}

	public struct Struct1:  System.Collections.IList, System.Xml.IXmlLineInfo
	{
	}
	public enum Enum1: int
	{
	}

	public delegate string Deleg1(out int i, char[] j);
#endregion	
}

