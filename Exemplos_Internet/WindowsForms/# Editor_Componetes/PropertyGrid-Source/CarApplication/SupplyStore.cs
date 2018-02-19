using System.Collections;
using System.Collections.Generic;
using DataModel.CarModel.Cars;
using DataModel.CarModel.Wheels;
using DataModel.CarModel.Wheels.Falken;
using DataModel.CarModel.Wheels.Kumho;

namespace CarApplication
{
	public class SupplyStore
	{
		#region Constants

		public enum SupplyParts
		{
			Cars,
			Wheels
		}

		#endregion

		#region Data Members

		private readonly SortedList<object, IList> _supplies;
		private readonly List<IList> _suppliesArray;
		private static SupplyStore _instance;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		public SupplyStore()
		{
			_supplies = new SortedList<object, IList>();
			_suppliesArray = new List<IList>();
		}

		#endregion

		#region Properties

		/// <summary>
		/// Get/Set for Instance
		/// </summary>
		public static SupplyStore Instance
		{
			get
			{
				if ( _instance == null )
				{
					_instance = new SupplyStore();
					LoadSupplies();
				}

				return ( _instance );
			}
		}

		/// <summary>
		/// Get/Set for Supplies
		/// </summary>
		public SortedList<object, IList> Supplies
		{
			get { return ( _supplies ); }
		}

		/// <summary>
		/// Get/Set for Supplies
		/// </summary>
		public List<IList> SuppliesArray
		{
			get { return ( _suppliesArray ); }
		}

		/// <summary>
		/// 
		/// </summary>
		public List<Wheel> Wheels
		{
			get { return ( _instance.Supplies["Wheels"] as List<Wheel> ); }
		}

		/// <summary>
		/// 
		/// </summary>
		public List<Car> Cars
		{
			get { return ( _instance.Supplies["Cars"] as List<Car> ); }
		}

		#endregion

		#region Methods - Private

		/// <summary>
		/// 
		/// </summary>
		private static void LoadSupplies()
		{
			List<Wheel> wheels = new List<Wheel>();
			wheels.Add( new FalkenZE912() );
			wheels.Add( new FalkenZiexSTZ04() );
			wheels.Add( new Kumho716HP4() );
			wheels.Add( new KumhoEcsta711() );

			List<Car> cars = new List<Car>();
			cars.Add( new Cobalt() );
			cars.Add( new Corvett() );
			cars.Add( new Implala() );

			_instance.Supplies.Add( "Wheels", wheels );
			_instance.Supplies.Add( "Cars", cars );

			_instance.SuppliesArray.Add( cars );
			_instance.SuppliesArray.Add( wheels );
		}

		#endregion
	}
}
