using System.Drawing;
using DataModel.CarModel.Wheels;

namespace DataModel.CarModel.Cars
{
	public class Car
	{
		#region Data Members

		private Wheel _wheel;
		private Engine _engine;
		private Color _bodyColor;
		private Color _interiorColor;
		private BodyStyle _bodyStyle;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		public Car()
		{
			_wheel = null;
			_engine = Engine.FourCylinder;
			_bodyColor = Color.Black;
			_interiorColor = Color.Gray;
			_bodyStyle = BodyStyle.Sedan;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Get/Set for Wheel
		/// </summary>
		public Wheel Wheel
		{
			get { return ( _wheel ); }
			set { _wheel = value; }
		}

		/// <summary>
		/// Get/Set for Engine
		/// </summary>
		public Engine Engine
		{
			get { return ( _engine ); }
			set { _engine = value; }
		}

		/// <summary>
		/// Get/Set for BodyColor
		/// </summary>
		public Color BodyColor
		{
			get { return ( _bodyColor ); }
			set { _bodyColor = value; }
		}

		/// <summary>
		/// Get/Set for InteriorColor
		/// </summary>
		public Color InteriorColor
		{
			get { return ( _interiorColor ); }
			set { _interiorColor = value; }
		}

		/// <summary>
		/// Get/Set for BodyStyle
		/// </summary>
		public BodyStyle BodyStyle
		{
			get { return ( _bodyStyle ); }
			set { _bodyStyle = value; }
		}

		#endregion
	}
}