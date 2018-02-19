using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestDisableWindowManagement
{
	public partial class TestDisableWindowManagement : Form
	{
		DisableWindowManagement.DisableWindowManagement windowManagement;

		public TestDisableWindowManagement()
		{
			InitializeComponent();
			windowManagement = new DisableWindowManagement.DisableWindowManagement();

		}

		private void checkBoxTaskBar_CheckedChanged(object sender, EventArgs e)
		{
			if (this.checkBoxTaskBar.Checked) 
                windowManagement.DisableTaskBar();
			else 
                windowManagement.EnableTaskBar();
		}
	}
}
