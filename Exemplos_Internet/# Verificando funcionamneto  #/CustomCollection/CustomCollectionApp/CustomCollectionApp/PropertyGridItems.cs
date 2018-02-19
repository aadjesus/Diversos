using System;
using System.ComponentModel;
using System.Text;

namespace CustomCollectionApp
{
    internal class PropertyGridItems
    {
        [Editor(typeof(Organization.EmployeeCollectionEditor),typeof(System.Drawing.Design.UITypeEditor))]
        [Category("Organization")]
        [DisplayName("Employees")]
        [Description("A collection of the employees within the organization")]
        public Organization.EmployeeCollection Employees
        {
            get { return Properties.Settings.Default.Employees; }
            set { Properties.Settings.Default.Employees = value; }
        }
    }
}
