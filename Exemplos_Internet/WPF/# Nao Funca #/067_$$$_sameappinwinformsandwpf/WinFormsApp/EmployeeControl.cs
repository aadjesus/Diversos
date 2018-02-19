using System.Drawing;
using System.Windows.Forms;
using BusinessObjects;

namespace WinFormsApp
{
    /// <summary>
    /// A WinForms control that displays an Employee object.
    /// </summary>
    public partial class EmployeeControl : UserControl
    {
        public EmployeeControl()
        {
            InitializeComponent();

            // Convert the picture file path to a Bitmap.
            Binding binding = this.employeePicture.DataBindings[0];
            binding.Format += this.ConvertFilePathToBitmap;
        }

        void ConvertFilePathToBitmap(object sender, ConvertEventArgs e)
        {
            e.Value = Bitmap.FromFile(e.Value as string);
        }

        public Employee Employee
        {
            get { return this.employeeBindingSource.DataSource as Employee; }
            set 
            { 
                this.employeeBindingSource.DataSource = value;

                // The employee's picture shows a tooltip of their ID.
                if (value != null)
                {
                    string msg = "Employee ID:  " + value.ID;
                    this.toolTip.SetToolTip(this.employeePicture, msg);
                }
            }
        }
    }
}