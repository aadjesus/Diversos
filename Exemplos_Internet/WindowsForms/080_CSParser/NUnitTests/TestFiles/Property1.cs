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
		
		
		
		private int i;
		
		protected internal int MethodA(int i, char j)
		{
		}

		public int PropA 
		{
			get
			{
			}
			set
			{
			}
		}

		public virtual string PropB
		{
			get
			{
			}
		}

		

		protected override object PropC
		{
		set 
		{
	}
	}

		public static long PropD 
		{
			get 
			{
			}
		}

		public object this[int i] 
		{
			get 
			{
			}
		}
		internal object this[string s]
		{
			get 
			{
			}
			set
			{
			}
		}

		//explicit implementation
		public object AAA.BBB.this[string s]
		{
			get
			{
			}
		}
		
		
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

