using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using CustomControls.ComboBox;
using CustomControls.Data;
using DataModel.CarModel;
using DataModel.CarModel.Cars;
using DataModel.CarModel.Wheels;

namespace CarApplication.View
{
	public class ViewCar
	{
		#region Data Members

		private readonly Car _car;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="car"></param>
		public ViewCar( Car car )
		{
			_car = car;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Get/Set for Wheel
		/// </summary>
		[Editor( typeof( ListGridComboBox ), typeof( UITypeEditor ) )]
		[DataList( "CarApplication", "CarApplication.SupplyStore", "Instance.Wheels", false, "OnAddedEventHandler" )]
		[CategoryAttribute( "Auto" ), DescriptionAttribute( "Description of Wheel" ), DisplayName( "SupplyStore.Instance.Wheels" )]
		public Wheel Wheel
		{
			get { return ( _car.Wheel ); }
			set { _car.Wheel = value; }
		}

		/// <summary>
		/// Get/Set for Wheel
		/// </summary>
		[Editor( typeof( ListGridComboBox ), typeof( UITypeEditor ) )]
		[DataList( "CarApplication", "CarApplication.SupplyStore", "Instance.Supplies[Wheels]", false, "OnAddedEventHandler" )]
		[CategoryAttribute( "Auto" ), DescriptionAttribute( "Description of Wheel" ), DisplayName( "SupplyStore.Instance.Supplies[Wheels]" )]
		public Wheel Wheel2
		{
			get { return ( _car.Wheel ); }
			set { _car.Wheel = value; }
		}

		/// <summary>
		/// Get/Set for Wheel
		/// </summary>
		[Editor( typeof( ListGridComboBox ), typeof( UITypeEditor ) )]
		[DataList( "CarApplication", "CarApplication.SupplyStore", "Instance.SuppliesArray[1]" )]
		[CategoryAttribute( "Auto" ), DescriptionAttribute( "Description of Wheel" ), DisplayName( "SupplyStore.Instance.SuppliesArray[1]" )]
		public Wheel Wheel3
		{
			get { return ( _car.Wheel ); }
			set { _car.Wheel = value; }
		}

		/// <summary>
		/// Get/Set for Wheel
		/// </summary>
		[Editor( typeof( ListGridComboBox ), typeof( UITypeEditor ) )]
		[DataList( "CarApplication", "CarApplication.SupplyStore", "Instance.SuppliesArray[CarApplication.SupplyStore+SupplyParts.Wheels,CarApplication]" )]
		[CategoryAttribute( "Auto" ), DescriptionAttribute( "Description of Wheel" ), DisplayName( "SupplyStore.Instance.SuppliesArray[SupplyParts.Wheels]" )]
		public Wheel Wheel4
		{
			get { return ( _car.Wheel ); }
			set { _car.Wheel = value; }
		}

		/// <summary>
		/// Get/Set for Engine
		/// </summary>
		[CategoryAttribute( "Auto" ), DescriptionAttribute( "Description of " ), DisplayName( "" )]
		public Engine Engine
		{
			get { return ( _car.Engine ); }
			set { _car.Engine = value; }
		}

		/// <summary>
		/// Get/Set for BodyColor
		/// </summary>
		[CategoryAttribute( "Auto" ), DescriptionAttribute( "Description of " ), DisplayName( "" )]
		public Color BodyColor
		{
			get { return ( _car.BodyColor ); }
			set { _car.BodyColor = value; }
		}

		/// <summary>
		/// Get/Set for InteriorColor
		/// </summary>
		[CategoryAttribute( "Auto" ), DescriptionAttribute( "Description of " ), DisplayName( "" )]
		public Color InteriorColor
		{
			get { return ( _car.InteriorColor ); }
			set { _car.InteriorColor = value; }
		}

		/// <summary>
		/// Get/Set for BodyStyle
		/// </summary>
		[CategoryAttribute( "Auto" ), DescriptionAttribute( "Description of " ), DisplayName( "" )]
		public BodyStyle BodyStyle
		{
			get { return ( _car.BodyStyle ); }
			set { _car.BodyStyle = value; }
		}

		#endregion
	}
}