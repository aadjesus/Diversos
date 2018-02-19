using System;
using System.Collections.Generic;
using System.Text;

namespace CustomPropertyGridTab
{
	class User
	{
		string _Field1 = string.Empty;
		string _Field2 = string.Empty;
		int _Field3 = 0;
		int _Hidden = 0;

		public override string ToString()
		{
			return "User";
		}

		[OzcanProperty]
		public string Name
		{
			get { return _Field1; }
			set { _Field1 = value; }
		}

		[OzcanProperty]
		public string Surname
		{
			get { return _Field2; }
			set { _Field2 = value; }
		}

		[OzcanProperty]
		public int Age
		{
			get { return _Field3; }
			set { _Field3 = value; }
		}

		// This will be hidden in Ozcan Property Tab
		public int LifeDays
		{
			get { return _Hidden; }
			set { _Hidden = value; }
		}
	}

	class Address
	{
		string _Field1 = string.Empty;
		string _Field2 = string.Empty;
		int _Field3 = 0;
		int _Hidden = 0;

		public override string ToString()
		{
			return "Address";
		}

		public string City
		{
			get { return _Field1; }
			set { _Field1 = value; }
		}

		public string Country
		{
			get { return _Field2; }
			set { _Field2 = value; }
		}

		// only this will be shown
		[OzcanProperty]
		public int PostCode
		{
			get { return _Field3; }
			set { _Field3 = value; }
		}

		public int PhoneNumber
		{
			get { return _Hidden; }
			set { _Hidden = value; }
		}
	}

	class DataItem
	{
		string _Field1 = string.Empty;
		string _Field2 = string.Empty;
		int _Field3 = 0;
		int _Hidden = 0;

		public override string ToString()
		{
			return "Data Item";
		}

		[OzcanProperty]
		public string Value1
		{
			get { return _Field1; }
			set { _Field1 = value; }
		}

		public string Value2
		{
			get { return _Field2; }
			set { _Field2 = value; }
		}

		// only this will be shown
		[OzcanProperty]
		public int Value3
		{
			get { return _Field3; }
			set { _Field3 = value; }
		}

		public int Value4
		{
			get { return _Hidden; }
			set { _Hidden = value; }
		}
	}
}
