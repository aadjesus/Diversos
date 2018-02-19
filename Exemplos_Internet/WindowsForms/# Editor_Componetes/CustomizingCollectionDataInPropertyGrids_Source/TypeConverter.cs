using System;
using System.ComponentModel; 

namespace PropertyGridSample
{
	// This is a special type converter which will be associated with the Employee class.
	// It converts an Employee object to string representation for use in a property grid.
	internal class EmployeeConverter : ExpandableObjectConverter
	{
		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType )
		{
			if( destType == typeof(string) && value is Employee )
			{
				// Cast the value to an Employee type
				Employee emp = (Employee)value;

				// Return department and department role separated by comma.
				return emp.Department + ", " + emp.Role;
			}
			return base.ConvertTo(context,culture,value,destType);
		}
	}

	// This is a special type converter which will be associated with the EmployeeCollection class.
	// It converts an EmployeeCollection object to a string representation for use in a property grid.
	internal class EmployeeCollectionConverter : ExpandableObjectConverter
	{
		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType )
		{
			if( destType == typeof(string) && value is EmployeeCollection )
			{
				// Return department and department role separated by comma.
				return "Company's employee data";
			}
			return base.ConvertTo(context,culture,value,destType);
		}
	}

}
