using System;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Text;

namespace Organization
{
    public class EmployeeCollectionEditor : CollectionEditor
    {
        public EmployeeCollectionEditor(Type type)
            : base(type)
        {
        }

        protected override string GetDisplayText(object value)
        {
            Employee item = new Employee();
            item = (Employee)value;

            return base.GetDisplayText(string.Format("{0}, {1}",item.LastName,item.FirstName));
        }
    }
}
