using System;

namespace MyLib
{
	/// <summary>A person entity.</summary>
    public class Person
    {
        private string name;
        private int age;
        private Address address;

		/// <summary>Name of the person.</summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

		/// <summary>Age of the person.</summary>
        public int Age
        {
            get { return age; }
            set { age = value; }
        }

		/// <summary>Address of the person.</summary>
        public Address Address
        {
            get { return address; }
            set { address = value; }
        }
    }
}