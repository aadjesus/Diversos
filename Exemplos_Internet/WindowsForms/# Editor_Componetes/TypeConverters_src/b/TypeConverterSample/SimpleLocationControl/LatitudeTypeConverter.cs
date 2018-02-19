using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace UIControl
{
	/// <summary>
	/// type converter used for the Latitude type
	/// </summary>
	class LatitudeTypeConverter : TypeConverter
	{
		const string WrongType = "The value type is not of type Latitude!";
		const string NoDegrees = "The degrees value is missing!";
		const string NoMinutes = "The minutes value is missing!";
		const string NoSeconds = "The seconds value is missing!";
		const string NoDirection = "The latitude direction is missing! Can be N for North or S for South.";
		const string MinutesUnit = "'";
		const string SecondsUnit = "\"";

		/// <summary>
		/// we can convert from a string to this type
		/// </summary>
		/// <param name="context">context descriptor</param>
		/// <param name="sourceType">source type</param>
		/// <returns></returns>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
				return true;
			else
				return base.CanConvertFrom(context, sourceType);
		}

		/// <summary>
		/// we can convert to a string and a InstanceDescriptor
		/// </summary>
		/// <param name="context">context descriptor</param>
		/// <param name="destinationType">the destination type</param>
		/// <returns></returns>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if ((destinationType == typeof(string)) |
				(destinationType == typeof(InstanceDescriptor)))
				return true;
			else
				return base.CanConvertTo(context, destinationType);
		}

		/// <summary>
		/// convert from a string to this type
		/// </summary>
		/// <param name="context">context descriptor</param>
		/// <param name="culture">culture info to use</param>
		/// <param name="value">the source value</param>
		/// <returns></returns>
		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			// if no value passed along then return a new Latitute instance
			if (value == null)
				return new Latitude();

			// if the source is a string then convert to our type
			if (value is string)
			{
				// get strongly typed value
				string StringValue = value as string;

				// if empty string then return again a new instance of Latitude
				if (StringValue.Length <= 0)
					return new Latitude();

				// get the type converters we need
				TypeConverter IntConverter = TypeDescriptor.GetConverter(typeof(int));

				// search of the North latitude key
				int DirectionPos = StringValue.IndexOf(LatitudeDiretcion.North.ToString().Substring(0,1));
				LatitudeDiretcion Direction = LatitudeDiretcion.North;

				// if not found search for the South latitude key
				if (DirectionPos == -1)
				{
					DirectionPos = StringValue.IndexOf(LatitudeDiretcion.South.ToString().Substring(0, 1));
					Direction = LatitudeDiretcion.South;
				}

				// get the position of the seconds and minutes unit character
				int MinutesPos = StringValue.IndexOf(MinutesUnit);
				int SecondsPos = StringValue.IndexOf(SecondsUnit);

				// the minutes are missing
				if (MinutesPos == -1)
					throw new Exception(NoMinutes);

				// the seconds are missing
				if (SecondsPos == -1)
					throw new Exception(NoSeconds);

				// the Latitude direction is missing
				if (DirectionPos == -1)
					throw new Exception(NoDirection);

				// the degrees are missing
				if (DirectionPos == 0)
					throw new Exception(NoDegrees);

				// get the degrees, minutes and seconds value
				int Degrees = (int)IntConverter.ConvertFromString(context, culture, StringValue.Substring(0, DirectionPos));
				int Minutes = (int)IntConverter.ConvertFromString(context, culture, StringValue.Substring(DirectionPos + 1, MinutesPos - DirectionPos - 1));
				int Seconds = (int)IntConverter.ConvertFromString(context, culture, StringValue.Substring(MinutesPos + 1, SecondsPos - MinutesPos - 1));

				// create a new Latitude instance with these values and return it
				return new Latitude(Degrees, Minutes, Seconds, Direction);
			}

			// otherwise call the base converter
			else
				return base.ConvertFrom(context, culture, value);
		}

		/// <summary>
		/// convert from Latitude type to a string or instance descriptor
		/// </summary>
		/// <param name="context">conext descriptor</param>
		/// <param name="culture">culture info</param>
		/// <param name="value">source value</param>
		/// <param name="destinationType">destination type</param>
		/// <returns></returns>
		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
		{
			// check that the value passed along is of our type
			if (value != null)
				if (!(value is Latitude))
					throw new Exception(WrongType);

			// convert to a string
			if (destinationType == typeof(string))
			{
				// no Latitude instance so we return an empty string
				if (value == null)
					return String.Empty;

				// get strongly type value
				Latitude LatitudeValue = value as Latitude;

				// get the type converters we need
				TypeConverter IntConverter = TypeDescriptor.GetConverter(typeof(Int32));
				TypeConverter EnumConverter = TypeDescriptor.GetConverter(typeof(LatitudeDiretcion));

				// return string representation of Latitude
				return IntConverter.ConvertToString(context, culture, LatitudeValue.Degrees) +
					   EnumConverter.ConvertToString(context, culture, LatitudeValue.Direction).Substring(0, 1) +
					   IntConverter.ConvertToString(context, culture, LatitudeValue.Minutes) + MinutesUnit +
					   IntConverter.ConvertToString(context, culture, LatitudeValue.Seconds) + SecondsUnit;
			}

			// convert to a instance descriptor
			if (destinationType == typeof(InstanceDescriptor))
			{
				// no Latitude instance
				if (value == null)
					return null;

				// get strongly type value
				Latitude LatitudeValue = value as Latitude;

				// used to describe the constructor
				MemberInfo Member = null;
				object[] Arguments = null;

				// get the constructor of our Latitude type
				Member = typeof(Latitude).GetConstructor(new Type[] { typeof(int), typeof(int), typeof(int), typeof(LatitudeDiretcion) });

				// the arguments to pass along to the Latitude constructor
				Arguments = new object[] { LatitudeValue.Degrees, LatitudeValue.Minutes, LatitudeValue.Seconds, LatitudeValue.Direction };

				// return the instance descriptor or null if we could not find a constructor
				if (Member != null)
					return new InstanceDescriptor(Member, Arguments);
				else
					return null;
			}

			// call base converter to convert
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
