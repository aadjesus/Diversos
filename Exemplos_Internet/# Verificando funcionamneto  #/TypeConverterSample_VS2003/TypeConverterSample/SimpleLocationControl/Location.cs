using System;
using System.ComponentModel;
using System.Globalization;

namespace UIControl
{
	/// <summary>
	/// Latitude can be North or South of the equator
	/// </summary>
	public enum LatitudeDiretcion
	{
		North,
		South
	}

	/// <summary>
	/// latitude consists of degrees, minutes, seconds and North or South
	/// </summary>
	[TypeConverter(typeof(LatitudeTypeConverter))]
	public class Latitude
	{
		private int _Degrees;
		private int _Minutes;
		private int _Seconds;
		private LatitudeDiretcion _Direction;

		/// <summary>
		/// degrees
		/// </summary>
		public int Degrees
		{
			get 
			{
				return _Degrees;
			}
			set 
			{
				_Degrees = value;
			}
		}

		/// <summary>
		/// minutes
		/// </summary>
		public int Minutes
		{
			get 
			{
				return _Minutes;
			}
			set 
			{
				_Minutes = value;
			}
		}

		/// <summary>
		/// seconds
		/// </summary>
		public int Seconds
		{
			get 
			{
				return _Seconds;
			}
			set 
			{
				_Seconds = value;
			}
		}

		/// <summary>
		/// direction
		/// </summary>
		public LatitudeDiretcion Direction
		{
			get 
			{
				return _Direction;
			}
			set 
			{
				_Direction = value;
			}
		}

		/// <summary>
		/// creates Latitude and sets values
		/// </summary>
		/// <param name="Degrees">degrees</param>
		/// <param name="Minutes">minutes</param>
		/// <param name="Seconds">seconds</param>
		/// <param name="Direction">direction</param>
		public Latitude(int Degrees, int Minutes, int Seconds, LatitudeDiretcion Direction)
		{
			_Degrees = Degrees;
			_Minutes = Minutes;
			_Seconds = Seconds;
			_Direction = Direction;
		}

		/// <summary>
		/// default constructor
		/// </summary>
		public Latitude() : this(0, 0, 0, LatitudeDiretcion.North)
		{ 
		}
	}

	/// <summary>
	/// Longitude can be West or East of Greenwitch (Prime Meridian)
	/// </summary>
	public enum LongitudeDirection
	{
		West, 
		East
	}

	/// <summary>
	/// longitude consists of degrees, minutes, seconds and West or East
	/// </summary>
	[TypeConverter(typeof(LongitudeTypeConverter))]
	public class Longitude
	{
		private int _Degrees;
		private int _Minutes;
		private int _Seconds;
		private LongitudeDirection _Direction;

		/// <summary>
		/// degrees
		/// </summary>
		public int Degrees
		{
			get
			{
				return _Degrees;
			}
			set
			{
				_Degrees = value;
			}
		}

		/// <summary>
		/// minutes
		/// </summary>
		public int Minutes
		{
			get
			{
				return _Minutes;
			}
			set
			{
				_Minutes = value;
			}
		}

		/// <summary>
		/// seconds
		/// </summary>
		public int Seconds
		{
			get
			{
				return _Seconds;
			}
			set
			{
				_Seconds = value;
			}
		}

		/// <summary>
		/// direction
		/// </summary>
		public LongitudeDirection Direction
		{
			get
			{
				return _Direction;
			}
			set
			{
				_Direction = value;
			}
		}

		/// <summary>
		/// creates Longitude and sets values
		/// </summary>
		/// <param name="Degrees">degrees</param>
		/// <param name="Minutes">minutes</param>
		/// <param name="Seconds">seconds</param>
		/// <param name="Direction">direction</param>
		public Longitude(int Degrees, int Minutes, int Seconds, LongitudeDirection Direction)
		{
			_Degrees = Degrees;
			_Minutes = Minutes;
			_Seconds = Seconds;
			_Direction = Direction;
		}

		/// <summary>
		/// default constructor
		/// </summary>
		public Longitude() : this(0, 0, 0, LongitudeDirection.West)
		{ 
		}
	}

	/// <summary>
	/// a GPS location consists of a latitude and longitude
	/// </summary>
	[TypeConverter(typeof(GPSLocationTypeConverter))]
	public class GPSLocation
	{
		Longitude _Longitude;
		Latitude _Latitude;

		/// <summary>
		/// longitude
		/// </summary>
		[Browsable(true)]
		[NotifyParentProperty(true)]
		public Longitude GPSLongitude
		{
			get 
			{
				return _Longitude;
			}
			set 
			{
				_Longitude = value;
			}
		}

		/// <summary>
		/// latitude
		/// </summary>
		[Browsable(true)]
		[NotifyParentProperty(true)]
		public Latitude GPSLatitude
		{
			get 
			{
				return _Latitude;
			}
			set 
			{
				_Latitude = value;
			}
		}

		/// <summary>
		/// instantiate a GPS location
		/// </summary>
		/// <param name="GPSLatitue">latitude</param>
		/// <param name="GPSLongitude">longitude</param>
		public GPSLocation(Latitude GPSLatitue, Longitude GPSLongitude)
		{
			_Latitude = GPSLatitue;
			_Longitude = GPSLongitude;
		}

		/// <summary>
		/// def instantiation
		/// </summary>
		public GPSLocation() : this(new Latitude(), new Longitude())
		{ 
		}

		/// <summary>
		/// convert this type to a string using the invariant culture
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return ToString(CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// convert this type to a string
		/// </summary>
		/// <param name="Culture">the culture to use</param>
		/// <returns></returns>
		public string ToString(CultureInfo Culture)
		{
			return TypeDescriptor.GetConverter(GetType()).ConvertToString(null, Culture, this);
		}
	}
}
