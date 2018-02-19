using System;

namespace MyLib
{
	/// <summary>Address sub-entity.</summary>
    public class Address
    {
        private int doorNumber;
        private string street;

		/// <summary>Door number.</summary>
        public int DoorNumber
        {
            get { return doorNumber; }
            set { doorNumber = value; }
        }

		/// <summary>Street name.</summary>
        public string Street
        {
            get { return street; }
            set { street = value; }
        }
    }
}