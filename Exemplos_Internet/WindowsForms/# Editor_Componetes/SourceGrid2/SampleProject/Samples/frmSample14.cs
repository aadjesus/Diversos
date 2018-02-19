using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SampleProject
{
	/// <summary>
	/// Summary description for frmSample14.
	/// </summary>
	public class frmSample14 : System.Windows.Forms.Form
	{
		private SourceGrid2.Grid grid1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmSample14()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.grid1 = new SourceGrid2.Grid();
			this.SuspendLayout();
			// 
			// grid1
			// 
			this.grid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grid1.AutoSizeMinHeight = 10;
			this.grid1.AutoSizeMinWidth = 10;
			this.grid1.AutoStretchColumnsToFitWidth = false;
			this.grid1.AutoStretchRowsToFitHeight = false;
			this.grid1.CustomSort = false;
			this.grid1.GridToolTipActive = true;
			this.grid1.Location = new System.Drawing.Point(8, 8);
			this.grid1.Name = "grid1";
			this.grid1.Size = new System.Drawing.Size(276, 256);
			this.grid1.SpecialKeys = SourceGrid2.GridSpecialKeys.Default;
			this.grid1.TabIndex = 0;
			// 
			// frmSample14
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 271);
			this.Controls.Add(this.grid1);
			this.Name = "frmSample14";
			this.Text = "My First Grid";
			this.Load += new System.EventHandler(this.frmSample14_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmSample14_Load(object sender, System.EventArgs e)
		{
			grid1.BorderStyle = BorderStyle.FixedSingle;

			grid1.ColumnsCount = 3;
			//grid1.FixedRows = 1;
			grid1.Rows.Insert(0);
			grid1[0,0] = new SourceGrid2.Cells.Real.ColumnHeader("String");
			grid1[0,1] = new SourceGrid2.Cells.Real.ColumnHeader("DateTime");
			grid1[0,2] = new SourceGrid2.Cells.Real.ColumnHeader("CheckBox");
			for (int r = 1; r < 10; r++)
			{
				grid1.Rows.Insert(r);
				grid1[r,0] = new SourceGrid2.Cells.Real.Cell("Hello " + r.ToString(), typeof(string));
				grid1[r,1] = new SourceGrid2.Cells.Real.Cell(DateTime.Today, typeof(DateTime));
				grid1[r,2] = new SourceGrid2.Cells.Real.CheckBox(true);
			}

			grid1.AutoSizeAll();
		}
	}
}
