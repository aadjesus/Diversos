using System;
using System.ComponentModel;

namespace PropertyGridSample
{
	/// <summary>
	/// Summary description for Organization.
	/// </summary>
	public class Organization
	{
		EmployeeCollection employees = new EmployeeCollection(); 
		Employee[] emps = new Employee[2];

		public Organization()
		{	
			// Instantiate test data objects and fill employee collection

			Employee emp1 = new Employee();
			emp1.FirstName = "Max";
			emp1.LastName = "Headroom";
			emp1.Age = 42;
			emp1.Department = "Sales";
			emp1.Role = "Manager";
			this.employees.Add(emp1);

			Employee emp2 = new Employee();
			emp2.FirstName = "Lara";
			emp2.LastName = "Croft";
			emp2.Age = 24;
			emp2.Department = "Accounting";
			emp2.Role = "Manager";
			this.employees.Add(emp2);

			emps[0] = emp1;
			emps[1] = emp2;
		}

		[TypeConverter(typeof(EmployeeCollectionConverter))]
		public EmployeeCollection Employees
		{
			get { return employees; }
		}
	}
}
