using System;
using System.ComponentModel;
using System.Globalization;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Design;
using System.Drawing;

namespace GradeTypeSample
{
	/// <summary>
	/// Summary description for Grade.
	/// </summary>
	[Editor(typeof(GradeEditor), typeof(System.Drawing.Design.UITypeEditor))]
	[TypeConverter(typeof(GradeConverter))]
	public struct Grade
	{
		private int grade; 
		
		public Grade(int grade)
		{
			this.grade = grade;
		}

		public int Value 
		{
			get 
			{
				return grade;
			}
		}		
	}

	internal class GradeConverter : TypeConverter
	{

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type destType) 
		{
			if (destType == typeof(string)) return true;
			return base.CanConvertFrom(context, destType);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destType) 
		{
			if (destType == typeof(InstanceDescriptor) || destType == typeof(string)) 
			{
				return true;
			}
			return base.CanConvertTo(context, destType);
		}		

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
			if (value is string) 
			{
				return new Grade(Int32.Parse((string)value));
			}
			return base.ConvertFrom(context, culture, value);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType) 
		{
			if (destType == typeof(InstanceDescriptor)) 
			{
				return new InstanceDescriptor(typeof(Grade).GetConstructor(new Type[]{typeof(int)}), new object[]{((Grade)value).Value});
			}
			else if (destType == typeof(string)) 
			{				
				return ((Grade)value).Value.ToString();
			}
			return base.ConvertTo(context, culture, value, destType);
		}

	}

}
