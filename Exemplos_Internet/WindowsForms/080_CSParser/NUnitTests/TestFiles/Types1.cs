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
		

		private interface Int2: System.Collections.IList
		{
		}

		private struct Struct2:  System.Collections.IList, System.Xml.IXmlLineInfo
		{
		}
		private enum Enum2: int
		{
		}

		private delegate string Deleg2(out int i, char[] j);
		
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

