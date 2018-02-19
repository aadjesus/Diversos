

using System;
using System.Collections;
using System.Reflection;

[assembly: AssemblyTitle("XXX"), AssemblyName("YYYY")]

namespace testParser
{
	//using System.Xml.Serialization;
	
	[XmlRoot("Bla")]
    [XmlNameSpace("NS")]
	[PermissionSet(SecurityAction.LinkDemand, Name="FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name="FullTrust")]
	 public class Class2
	{
		
		 [OtherAttribute("bla", 1, true)]
		 string val1="bla";
		 
		 [SecAttribute(level=1, name="Jim")]
		 public Class2() {}
			
		[property: PropAttribute(true)]
		 public string prop 
		 {
			[GetterAttribute] 
			get { return "";}
		 }

		[WebServiceMethod] 
		public int methodA( 
			[FirstPar("xy"), Other]
			int i, 
			[SecondPar(2.1)]
			int j) {
		 return 0;
		 }

		[NewAttribute(true, "author", new DateTime("22.2.2002"))]
		public event EventHandler evt;

		[OneAttribute]
	    [SecondAttribute(true, false, x=1, y=2)]
		public object this[int i]
		 {
			 get {return null;}

		 }


	}

	
	

}



