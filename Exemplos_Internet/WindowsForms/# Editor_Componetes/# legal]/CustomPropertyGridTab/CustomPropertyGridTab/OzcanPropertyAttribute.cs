using System;
using System.Collections.Generic;
using System.Text;

namespace CustomPropertyGridTab
{
	/// <summary>
	///  This attribute is using to indicate that a property is a filtered property for us.
	///  
	///  Properties which has this attribute will be shown in our Custom Property Tab Page
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class OzcanPropertyAttribute : Attribute
	{

	}
}
