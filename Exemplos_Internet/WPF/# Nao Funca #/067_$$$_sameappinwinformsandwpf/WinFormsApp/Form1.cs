using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessObjects;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Create and initialize a usercontrol for each employee.
            foreach(Employee emp in Employee.GetEmployees())
            {
                EmployeeControl empCtrl = new EmployeeControl();
                empCtrl.Employee = emp;
                this.flowLayoutPanel.Controls.Add(empCtrl);
            }
        }
    }
}