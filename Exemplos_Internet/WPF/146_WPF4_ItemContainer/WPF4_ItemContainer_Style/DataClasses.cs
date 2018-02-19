using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace WPF4_ItemContainer_Style
{
    public class Employee
    {
        public int EmpNo { get; set; }
        public string EmpName { get; set; }
        public string DeptName { get; set; }
        public string Designation { get; set; }
    }
    public class EmployeeCollection : ObservableCollection<Employee>
    {
        public EmployeeCollection()
        {
            Add(new Employee() { EmpNo = 101, EmpName = "Tejas", DeptName = "TRG", Designation = "Manager" });
            Add(new Employee() { EmpNo = 102, EmpName = "Mahesh", DeptName = "CTD", Designation = "Leader" });
            Add(new Employee() { EmpNo = 103, EmpName = "Karan", DeptName = "ACCT", Designation = "Head" });
            Add(new Employee() { EmpNo = 104, EmpName = "Akash", DeptName = "TRPT", Designation = "Operator" });
            Add(new Employee() { EmpNo = 105, EmpName = "Sameer", DeptName = "TRG", Designation = "Manager" });
        }  
    }
}
