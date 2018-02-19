using System;
using System.Collections;
using System.Text;

namespace Organization
{
    public class EmployeeCollection : CollectionBase
    {
        public Employee this[int index]
        {
            get { return (Employee)List[index]; }
        }

        public void Add(Employee emp)
        {
            List.Add(emp);
        }

        public void Remove(Employee emp)
        {
            List.Remove(emp);
        }
    }
}
