using System;
using System.ComponentModel;

namespace Phone_Number_Control
{
	/// <summary>
	/// Summary description for PhoneNumber.
	/// </summary>
	/// 
	[TypeConverter(typeof(PhoneTypeConverter))]
	public class PhoneNumber
	{

		#region User Defined Variables
		private string name;
		/// <summary>
		/// This variable is used to maintain the name value.
		/// </summary>
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if(null != value)
				{
					this.name = value;
					PropertyChanged("name");
				}
			}

		}

		private string countrycode;
		/// <summary>
		/// This variable is used to maintain the countrycode value.
		/// </summary>
		public string CountryCode
		{
			get
			{
				return this.countrycode;
			}
			set
			{
				if(null != value)
				{
					this.countrycode = value;
					PropertyChanged("countrycode");
				}
			}
		}
		private string areacode;
		/// <summary>
		/// This variable is used to maintain the areacode value.
		/// </summary>
		public string AreaCode
		{
			get
			{
				return this.areacode;
			}
			set
			{
				if(null != value)
				{
					this.areacode = value;
					PropertyChanged("areacode");
				}
			}
		}
		private string phonenumber;
		/// <summary>
		/// This variable is used to maintain the phonenumber value.
		/// 
		/// </summary>
		public string PhoneNum
		{
			get
			{
				return this.phonenumber;
			}
			set
			{
				if(null != value)
				{
					this.phonenumber = value;
					PropertyChanged("phonenumber");
				}
			}
		}
		#endregion

		#region Events
		public event PropertyChangedEventHandler PropertyChanged; 
		#endregion
		public PhoneNumber()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public override string ToString()
		{
			return "Address";
		}


	}
	/// <summary>
	/// Deelegate delc
	/// </summary>
	public delegate void PropertyChangedEventHandler(string propertyname);
}
