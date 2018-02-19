using System;
using System.Collections;

namespace testParser
{
	using System.IO;
	/// <summary>
	/// Summary description for Class2.
	/// </summary>
	abstract public class Class2: object, IList, System.Collections.ICollection
	{
		
		private class Inner
		{
			private class InnerInner: int, Something*, Other[,,,]
			{
			}
		}

		protected internal class OtherInner
		{
		}

		abstract public void AbstractMethod(int i, string s);
		
	}

	
		public class OtherClass
		{
		}
	
}
namespace OtherNameSpace
{
	using System.Xml;

	public class ClastInOtherNS
	{
	}
}
