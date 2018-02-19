using System;
using System.Collections;
using System.Reflection;



namespace testParser
{
	//using System.Xml.Serialization;
	
	
	[PermissionSet(SecurityAction.InheritanceDemand, Name="FullTrust")]
	 public class Class2
	{
		 
		 string val1="bla";		 
		 public Class2() {}		
		
		 public string prop 
		 {		
			get { return "";}
		 }

		
		public  int methodA( int i, int j) {
		 return 0;
		 }

		
		public event EventHandler evt;

		
		public object this[int i]
		 {
			 get {return null;}

		 }

	}

    [return: DlgAttribute(true, "mfbu")]
	public delegate int MyDelegate([PAttribute]object o);

	[IsReallyNothing]
	public interface INothing
	{}

	[FlagsAttribute]
	public enum En1
	{
		[V1Attribute("mfbu")]
		val1=1,
		[V2Attribute("mfbu")]
		val2=2
	}

	[ValAttribute(true, loc="somewhere", mode="auto")]
	public struct Str1 {}

	
	

}



