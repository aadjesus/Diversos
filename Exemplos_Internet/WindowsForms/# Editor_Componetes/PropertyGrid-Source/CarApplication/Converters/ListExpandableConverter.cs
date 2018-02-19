using System;
using System.ComponentModel;
using System.Globalization;
using DataModel;

namespace CarApplication.Converters
{
	public class ListExpandableConverter : ExpandableObjectConverter
	{
		public override object ConvertTo( ITypeDescriptorContext context, CultureInfo culture, object value, Type destType )
		{
			if ( value is IDisplay )
				return ( ( (IDisplay)value ).Text );

			return ( string.Empty );
		}
	}
}