using System;
using System.ComponentModel;
using System.Collections.Specialized;

namespace Testproperty
{		
	//The class to show in the propertygrid must inherit CustomClass
	//test Class with three public properties
	public class Class1:ICustomClass
	{

		int Test;
		int Test2;
		int Test3;

		PropertyList publicproperties=new PropertyList();

		public int test
		{

			get
			{
				return Test;
			}
			set
			{
				Test=value;
			}
		}

		public int test2
		{
			get
			{
				return Test2;
			}
			set
			{
				Test2=value;
			}
		}

		public int test3
		{
			get
			{
				return Test3;
			}
			set
			{
				Test3=value;
			}
		}


		//ICustomClass implementation
		public PropertyList PublicProperties
		{
			get
			{
				return publicproperties;
			}
			set
			{
				publicproperties=value;
			}
		}		
	}
}
