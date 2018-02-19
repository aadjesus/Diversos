using System;
using System.Collections;
using System.Xml;

namespace testParser
{
	#region Blabla
	using System.IO;
	/// <summary>
	/// Summary description for Class2.
	/// </summary>
	public interface ITest
	{
			
		int methodA(object o);
		new void methodB(string s, out bool b);
		string prop 
		{
			get;
			set;
		}
		event System.Xml.XmlNodeChangedEventHandler evt;

		object this[int i] 
		{
			get;
		}

		

		
		
		
	}

	
	
#endregion	
}

