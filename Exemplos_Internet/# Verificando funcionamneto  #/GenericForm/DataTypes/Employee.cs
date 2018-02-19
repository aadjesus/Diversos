using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTypes
{
    public enum Department
    {
        Engineering,
        Finance,
        Operations,
        HumanResource,
        Management
    }
    public class Employee
    {
        private long employeeID;
        private string firstName;
        private string lastName;
        private double salary;
        private Role employeeRole;
        private Employee patnerEmployee;
        private bool isActive;
        private Department employeeDepartment;

        public Department EmployeeDepartment
        {
            get { return employeeDepartment; }
            set { employeeDepartment = value; }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public long EmployeeID
        {
            get { return employeeID; }
            set { employeeID = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public double Salary
        {
            get { return salary; }
            set { salary = value; }
        }

        public Role EmployeeRole
        {
            get { return employeeRole; }
            set { employeeRole = value; }
        }


        public Employee PatnerEmployee
        {
            get { return patnerEmployee; }
            set { patnerEmployee = value; }
        }

        public Employee()
        {
            this.employeeID = 0;
            this.employeeRole = new Role();
            this.firstName = "MyName";
            this.lastName = "MyLastName";

        }
    }

    public class Manager : Employee
    {
        private Employee[] directReports;

        public Employee[] DirectReports
        {
            get { return directReports; }
            set { directReports = value; }
        }


    }

    public class Role
    {
        private string responsbility;

        public string Responsbility
        {
            get { return responsbility; }
            set { responsbility = value; }
        }
        private long weekWorkHours;

        public long WeekWorkHours
        {
            get { return weekWorkHours; }
            set { weekWorkHours = value; }
        }

    }
}
