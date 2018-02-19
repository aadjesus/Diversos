using System;
using System.Text;
using System.ComponentModel;

namespace Organization
{
    public class Employee
    {
        #region Private Variables
        private string firstName;
        private string lastName;
        private DateTime dateOfHire;
        #endregion

        #region Public Properties
        [Category("Employee")]
        [DisplayName("First Name")]
        [Description("The first name of the employee.")]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        [Category("Employee")]
        [DisplayName("Last Name")]
        [Description("The last name of the employee.")]
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        [Category("Employee")]
        [DisplayName("Date of Hire")]
        [Description("The hire date of the employee.")]
        public DateTime DateOfHire
        {
            get { return dateOfHire; }
            set { dateOfHire = value; }
        }
        #endregion
    }
}
