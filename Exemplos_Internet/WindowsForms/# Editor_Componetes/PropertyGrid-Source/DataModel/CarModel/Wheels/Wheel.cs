namespace DataModel.CarModel.Wheels
{
	public abstract class Wheel
	{
		#region Data Members

		private string _brand;
		private string _name;
		private string _size;
		private string _dimensions;
		private string _rating;

		#endregion

		#region Properties

		/// <summary>
		/// Get/Set for Brand
		/// </summary>
		public string Brand
		{
			get { return ( _brand ); }
			protected set { _brand = value; }
		}

		/// <summary>
		/// Get/Set for Name
		/// </summary>
		public string Name
		{
			get { return ( _name ); }
			protected set { _name = value; }
		}

		/// <summary>
		/// Get/Set for Size
		/// </summary>
		public string Size
		{
			get { return ( _size ); }
			protected set { _size = value; }
		}

		/// <summary>
		/// Get/Set for Dimensions
		/// </summary>
		public string Dimensions
		{
			get { return ( _dimensions ); }
			protected set { _dimensions = value; }
		}

		/// <summary>
		/// Get/Set for Rating
		/// </summary>
		public string Rating
		{
			get { return ( _rating ); }
			protected set { _rating = value; }
		}

		#endregion

		#region Methods - Public

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return ( string.Format( "{0} {1}", this.Brand, this.Name ) );
		}

		#endregion
	}
}