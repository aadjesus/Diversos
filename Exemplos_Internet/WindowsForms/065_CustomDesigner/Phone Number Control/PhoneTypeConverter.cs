using System;
using System.ComponentModel;

namespace Phone_Number_Control
{
	/// <summary>
	/// Summary description for PhoneTypeConverter.
	/// </summary>
	public class PhoneTypeConverter : TypeConverter
	{
		public PhoneTypeConverter()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		/// <summary>
		/// allows us to display the + symbol near the property name
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <param name="value"></param>
		/// <param name="attributes"></param>
		/// <returns></returns>
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(typeof(PhoneNumber));
		}


	}
}
