using System;
using System.Collections.Generic;
using System.ComponentModel;
using CarApplication.View;

namespace CarApplication.Converters
{
	public class PersonConverter : TypeConverter
	{
		static readonly PropertyDescriptorCollection collection;

		static PersonConverter()
		{
			List<PropertyDescriptor> pdcList = new List<PropertyDescriptor>();
			pdcList.Add( TypeDescriptor.CreateProperty( typeof( ViewPerson ), "Name", typeof( string ), null ) );
			pdcList.Add( TypeDescriptor.CreateProperty( typeof( ViewPerson ), "Street", typeof( string ), null ) );
			pdcList.Add( TypeDescriptor.CreateProperty( typeof( ViewPerson ), "City", typeof( string ), null ) );
			pdcList.Add( TypeDescriptor.CreateProperty( typeof( ViewPerson ), "State", typeof( string ), null ) );
			pdcList.Add( TypeDescriptor.CreateProperty( typeof( ViewPerson ), "Zip", typeof( string ), null ) );
			pdcList.Add( TypeDescriptor.CreateProperty( typeof( ViewPerson ), "Phone", typeof( string ), null ) );

			collection = new PropertyDescriptorCollection( pdcList.ToArray() );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <param name="value"></param>
		/// <param name="attributes"></param>
		/// <returns></returns>
		public override PropertyDescriptorCollection GetProperties( ITypeDescriptorContext context, object value, Attribute[] attributes )
		{
			return ( collection );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetPropertiesSupported( ITypeDescriptorContext context )
		{
			return true;
		}
	}
}