using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace PropertyGridSample
{

	/// <summary>
	/// Person is the test class defining two properties: first name and last name .
	/// By deriving from GlobalizedObject the displaying of property names are language aware.
	/// GlobalizedObject implements the interface ICustomTypeDescriptor. 
	/// </summary>
	public class Person
	{
		private string firstName = "";
		private string lastName = "";
		private int age = 0;

		public Person() {}

		[Category("Required")]
		public string FirstName
		{
			get { return firstName; }
			set { firstName = value; }
		}

		// Uncomment the next line to see the attribute in action: 
		[Category("Required")]
		public string LastName
		{
			get { return lastName; }
			set { lastName = value; }
		}

		[Category("Optional")]
		public int Age
		{
			get { return age; }
			set { age = value; }
		}
	}
}

