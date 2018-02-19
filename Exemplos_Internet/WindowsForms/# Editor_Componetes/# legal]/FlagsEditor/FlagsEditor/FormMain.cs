using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace FlagsEditor
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			InitializeComponent();

			propertyGrid1.SelectedObject = new ExampleComponent();
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(@"http://www.ozcandegirmenci.com");
		}
	}
}
