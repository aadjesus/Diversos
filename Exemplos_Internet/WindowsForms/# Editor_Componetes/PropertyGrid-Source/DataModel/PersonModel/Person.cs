namespace DataModel.PersonModel
{
	public class Person
	{
		#region Data Members

		private string _name;
		private string _street;
		private string _city;
		private string _state;
		private string _zip;
		private string _phone;

		#endregion

		#region Constructor

		public Person()
		{
			this.Name = string.Empty;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Get/Set for Name
		/// </summary>
		public string Name
		{
			get { return ( _name ); }
			set { _name = value; }
		}

		/// <summary>
		/// Get/Set for Street
		/// </summary>
		public string Street
		{
			get { return ( _street ); }
			set { _street = value; }
		}

		/// <summary>
		/// Get/Set for City
		/// </summary>
		public string City
		{
			get { return ( _city ); }
			set { _city = value; }
		}

		/// <summary>
		/// Get/Set for State
		/// </summary>
		public string State
		{
			get { return ( _state ); }
			set { _state = value; }
		}

		/// <summary>
		/// Get/Set for Zip
		/// </summary>
		public string Zip
		{
			get { return ( _zip ); }
			set { _zip = value; }
		}

		/// <summary>
		/// Get/Set for Phone
		/// </summary>
		public string Phone
		{
			get { return ( _phone ); }
			set { _phone = value; }
		}

		#endregion
	}
}