// ******************************************************************
//
//	If this code works it was written by:
//		Henry Minute
//		MamSoft / Manniff Computers
//		© 2008 - 2009...
//
//	if not, I have no idea who wrote it.
//
// ******************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NestedControlDesignerTest
{
	public partial class NestedControlTestForm : Form
	{
		public NestedControlTestForm()
		{
			InitializeComponent();
            this.lineShape2.X1 = panelControl1.Left;
            this.lineShape2.X2 = panelControl1.Width;
            this.lineShape2.X2 = panelControl1.Width;
            this.lineShape2.Y1 = panelControl1.Height - 2;
            this.lineShape2.Y2 = lineShape2.Y1;
            this.lineShape2.BorderColor = this.checkEdit1.ForeColor;
            this.checkEdit1.Checked = true;
		}

		private void NestedControlTestForm_Load(object sender, EventArgs e)
		{
			this.cboxNumbers.SelectedIndex = 0;
		}

        private void checkEdit1_EditValueChanged(object sender, EventArgs e)
        {
            this.checkEdit1.Text = String.Concat("F8 ",(this.checkEdit1.Checked
                ? "Sair"
                : "Entar"));
            this.panelControl1.Visible = this.checkEdit1.Checked;
        }
	}
}
