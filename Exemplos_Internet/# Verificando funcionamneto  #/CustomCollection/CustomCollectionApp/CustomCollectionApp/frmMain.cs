using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CustomCollectionApp
{
    public partial class frmMain : Form
    {
        #region Constructor
        public frmMain()
        {
            InitializeComponent();
        }
        #endregion 

        #region Load Event
        private void frmMain_Load(object sender, EventArgs e)
        {
            myPropertyGrid.SelectedObject = new PropertyGridItems();
        }
        #endregion

        #region ToolStrip Button Clicks
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.Save();

                MessageBox.Show("Your settings were saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void defaultToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to load the default settings?  All customized settings will be lost.",
                    "Load Defaults", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Properties.Settings.Default.Reset();

                    MessageBox.Show("Your default settings have been loaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Button Clicks
        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                lstNames.Items.Clear();

                foreach (Organization.Employee emp in Properties.Settings.Default.Employees)
                {
                    lstNames.Items.Add(emp.LastName + ", " + emp.FirstName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
