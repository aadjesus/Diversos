using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace SpottyDogSoftware.Controls
{
	/// <summary>
	/// Used with the Visual Studio property browser so group items
	/// can be specified at design time.
	/// </summary>
	public class XPGroupBoxItemConvertor : ExpandableObjectConverter
	{
		/// <summary>
		/// Returns whether this converter can convert the object to the specified 
		/// type, using the specified context.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="destinationType"></param>
		/// <returns></returns>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
			{
				return true;
			}
			else
			{
				return base.CanConvertTo(context, destinationType);
			}
		}

		/// <summary>
		/// Converts the given value object to the specified type, using the arguments.
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a format context. </param>
		/// <param name="culture">A CultureInfo object. If a null reference (Nothing in Visual Basic) is passed, the current culture is assumed. </param>
		/// <param name="value">The Object to convert. </param>
		/// <param name="destinationType">The Type to convert the value parameter to. </param>
		/// <returns>An Object that represents the converted value.</returns>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
			{
				XPGroupBoxItem item = (XPGroupBoxItem)value;
				ConstructorInfo ci = typeof(XPGroupBoxItem).GetConstructor(new Type[]{typeof(System.Drawing.Icon), typeof(string), typeof(string), typeof(bool)});
				return new InstanceDescriptor(ci, new object[]{item.Icon, item.Name, item.Text, item.Enabled});
			}
			else
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
}
