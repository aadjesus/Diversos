using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;

namespace Galador.Applications
{
	/// <summary>
	/// A UI Element tagged with such an attribute would be used to display data
	/// </summary>
	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class DataViewAttribute : ExportAttribute
	{
		internal const string DataViewContract = "Composition DataView";
		internal const string DataTypeProperty = "DataType";
		internal const string LocationProperty = "Location";

		public DataViewAttribute(Type dataType)
			: base(DataViewContract)
		{
			if (dataType == null)
				throw new ArgumentNullException("dataType");
			DataType = dataType;
		}
		public DataViewAttribute(Type dataType, object location)
			: this(dataType)
		{
			Location = location;
		}

		public Type DataType { get; private set; }
		public object Location { get; set; }

		public override bool Equals(object obj)
		{
			var other = obj as DataViewAttribute;
			if (other == null)
				return false;

			if (!Equals(Location, other.Location))
				return false;
			if (!Equals(DataType, other.DataType))
				return false;

			return true;
		}

		public override int GetHashCode()
		{
			int loc = Location == null ? 0 : Location.GetHashCode();
			return loc ^ DataType.GetHashCode();
		}

		public bool Candidate(Type dataType, object location)
		{
			if (!Equals(Location, location))
				return false;
			return DataType.IsAssignableFrom(dataType);
		}
	}
}
