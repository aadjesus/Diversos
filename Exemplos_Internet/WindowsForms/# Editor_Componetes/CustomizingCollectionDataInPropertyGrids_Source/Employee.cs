using System;
using System.Text;
using System.ComponentModel;

namespace PropertyGridSample
{
	/// <summary>
	/// Employee is our sample business or domin object. It derives from the general base class Person.
	/// </summary>
	[TypeConverter(typeof(EmployeeConverter))]
	public class Employee : Person
	{
		private string department = "";
		private string role = "";

		public Employee()
		{
		}

		[Category("Required")]
		public string Department
		{
			get { return department; }
			set { department = value; }
		}

		[Category("Required")]
		public string Role
		{
			get { return role; }
			set { role = value; }
		}

		// Meaningful text representation
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(this.LastName);
			sb.Append(",");
			sb.Append(this.FirstName);
			sb.Append(",");
			sb.Append(this.Age);
			sb.Append(",");
			sb.Append(this.Department);
			sb.Append(",");
			sb.Append(this.Role);
			return sb.ToString();
		}
	}

}
