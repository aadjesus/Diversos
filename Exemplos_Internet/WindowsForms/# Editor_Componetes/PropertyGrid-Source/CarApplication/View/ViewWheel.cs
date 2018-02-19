using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using CustomControls;
using CustomControls.ComboBox;
using CustomControls.Data;
using DataModel.CarModel.Cars;
using DataModel.CarModel.Wheels;

namespace CarApplication.View
{
	public class ViewWheel
	{
		#region Properties

		private Car _car;
		private readonly Wheel _wheel;
		private ViewPerson2 _person;
		private SupplyStore.SupplyParts _supplyPartsEnum;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="wheel"></param>
		public ViewWheel( Wheel wheel )
		{
			this._wheel = wheel;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Get/Set for Person
		/// </summary>
		public ViewPerson2 Person
		{
			get { return ( _person ); }
			set { _person = value; }
		}

		/// <summary>
		/// Get/Set for Car
		/// </summary>
		[DefaultValue( "" )]
		[Editor( typeof( ListGridComboBox ), typeof( UITypeEditor ) )]
		[DataList( "CarApplication", "SupplyStore", "Instance.Cars", false, "OnAddedEventHandler" )]
		[Category( "Automobile" ), Description( "Description of Car" ), DisplayName( "Car Type" )]
		public Car Car
		{
			get { return ( _car ); }
			set { _car = value; }
		}

		/// <summary>
		/// Get for Wheel
		/// </summary>
		[Category( "Automobile" ), Description( "Description of Wheel" ), DisplayName( "Wheel Type" )]
		public Wheel Wheel
		{
			get { return _wheel; }
		}

		/// <summary>
		/// Get for Wheel
		/// </summary>
		[Editor( typeof( EnumGridComboBox ), typeof( UITypeEditor ) )]
		[EnumList( typeof(SupplyStore.SupplyParts))]
		[Category( "Misc" ), Description( "Description of Supply Parts Enumeration " ), DisplayName( "Supply Parts Enumeration" )]
		public SupplyStore.SupplyParts SupplyParts
		{
			get { return _supplyPartsEnum; }
			set { _supplyPartsEnum = value; }
		}


		#endregion

		#region Event Handler

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="arg"></param>
		private void OnAddedEventHandler( object sender, ObjectCreatedEventArgs arg )
		{
			if ( arg != null )
			{
				MessageBox.Show( "You just created a " + arg.DataValue.GetType().Name,
					"Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
		}

		#endregion
	}
}