using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPFControlsSample
{
    /// <summary>
    /// Employee class - Represents an Employee
    /// </summary>
    public class Employee
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private Int16 age;

        public Int16 Age
        {
            get { return age; }
            set { age = value; }
        }
    }
}
