using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Linq;
using System.Reflection;

/*
  Custom Type Converter Sample 2
  http://cyotek.com/blog/creating-a-custom-typeconverter-part-2
*/

namespace CustomTypeConverter2
{
    /// <summary>
    /// Converts lengths from one data type to another. Access this class through the <see cref="TypeDescriptor"/>.
    /// </summary>
    internal class LengthConverter : TypeConverter
    {
        #region Overridden Members

        /// <summary>
        /// Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
        /// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from.</param>
        /// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        ///// <summary>
        ///// Returns whether this converter can convert the object to the specified type, using the specified context.
        ///// </summary>
        ///// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
        ///// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to.</param>
        ///// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Converts the given object to the type of this converter, using the specified context and culture information.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
        /// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
        /// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
        /// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string stringValue;
            object result;

            result = null;
            stringValue = value as string;

            if (!string.IsNullOrEmpty(stringValue))
            {
                int nonDigitIndex;

                nonDigitIndex = stringValue.IndexOf(stringValue.FirstOrDefault(char.IsLetter));

                if (nonDigitIndex > 0)
                {
                    result = new Length
                    {
                        Value = Convert.ToSingle(stringValue.Substring(0, nonDigitIndex)),
                        Unit = (Unit)Enum.Parse(typeof(Unit), stringValue.Substring(nonDigitIndex), true)
                    };
                }
            }

            return result ?? base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Converts the given value object to the specified type, using the specified context and culture information.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
        /// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed.</param>
        /// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
        /// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to.</param>
        /// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
        /// <exception cref="System.ArgumentNullException">destinationType</exception>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            Length length;
            object result;

            result = null;
            length = value as Length;

            if (length != null)
            {
                if (destinationType == typeof(string))
                    result = length.ToString();
                else if (destinationType == typeof(InstanceDescriptor))
                {
                    ConstructorInfo constructorInfo;

                    constructorInfo = typeof(Length).GetConstructor(new[]
                    {
                        typeof(float), typeof(Unit)
                    });
                    result = new InstanceDescriptor(constructorInfo, new object[]
                    {
                        length.Value, length.Unit
                    });
                }
            }

            return result ?? base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// Returns a collection of properties for the type of array specified by the value parameter, using the specified context and attributes.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
        /// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array for which to get properties.</param>
        /// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
        /// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for this data type, or null if there are no properties.</returns>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(value, attributes);
        }

        /// <summary>
        /// Returns whether this object supports properties, using the specified context.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
        /// <returns>true if <see cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)" /> should be called to find the properties of this object; otherwise, false.</returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Returns a collection of standard values for the data type this type converter is designed for when provided with a format context.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be null.</param>
        /// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid values, or null if the data type does not support a standard set of values.</returns>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<Length> values;

            values = new List<Length>();
            values.Add(new Length(16, Unit.px));
            values.Add(new Length(32, Unit.px));
            values.Add(new Length(64, Unit.px));
            values.Add(new Length(128, Unit.px));

            return new StandardValuesCollection(values);
        }

        /// <summary>
        /// Returns whether this object supports a standard set of values that can be picked from a list, using the specified context.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
        /// <returns>true if <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> should be called to find a common set of values the object supports; otherwise, false.</returns>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        #endregion
    }
}
