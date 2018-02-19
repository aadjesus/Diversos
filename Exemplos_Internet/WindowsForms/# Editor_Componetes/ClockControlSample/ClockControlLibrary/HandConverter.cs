#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Text;

#endregion

namespace ClockControlLibrary
{
	public class HandConverter : ExpandableObjectConverter
	{
		// Don't need to override CanConvertTo if converting to string, as that's what base TypeConverter does
		// Do need to override CanConvertFrom since it's base converts from InstanceDescriptor

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			// We can be converted to an InstanceDescriptor
			if (destinationType == typeof(InstanceDescriptor)) return true;
			return base.CanConvertTo(context, destinationType);
		}

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			// We can convert from a string to a Hand type
			if (sourceType == typeof(string)) { return true; }
			return base.CanConvertFrom(context, sourceType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo info, object value)
		{

			// If converting from a string
			if (value is string)
			{
				// Build a Hand type
				try
				{
					// Get Hand properties
					string propertyList = (string)value;
					string[] properties = propertyList.Split(';');
					return new Hand(Color.FromName(properties[0].Trim()),
									Convert.ToInt32(properties[1]));
				}
				catch { }
				throw new ArgumentException("The arguments were not valid.");
			}
			return base.ConvertFrom(context, info, value);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{

			// If source value is a Hand type
			if (value is Hand)
			{
				// Convert to string
				if ((destinationType == typeof(string)))
				{
					Hand hand = (Hand)value;
					string color;
					if (hand.Color.IsNamedColor)
					{
						color = hand.Color.Name;
					}
					else
					{
						color = hand.Color.R + ", " + hand.Color.G + ", " + hand.Color.B;
					}
					return string.Format("{0}; {1}", color, hand.Width.ToString());
				}
				// Convert to InstanceDescriptor
				if ((destinationType == typeof(InstanceDescriptor)))
				{
					Hand hand = (Hand)value;
					object[] properties = new object[2];
					Type[] types = new Type[2];

					// Color
					types[0] = typeof(Color);
					properties[0] = hand.Color;

					// Width
					types[1] = typeof(int);
					properties[1] = hand.Width;

					// Build constructor
					ConstructorInfo ci = typeof(Hand).GetConstructor(types);
					return new InstanceDescriptor(ci, properties);
				}
			}
			// Base ConvertTo if neither string or InstanceDescriptor required
			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{

			// Always force a new instance
			return true;
		}

		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{

			// Use the dictionary to create a new instance
			return new Hand((Color)propertyValues["Color"], (int)propertyValues["Width"]);
		}
	}
}
