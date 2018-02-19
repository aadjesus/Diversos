using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SampleProject
{
	/// <summary>
	/// Summary description for frmSample13.
	/// </summary>
	public class frmSample13 : System.Windows.Forms.Form
	{
		private SourceGrid2.Grid grid1;
		private System.Windows.Forms.Label label1;
		private SourceLibrary.Windows.Forms.ColorPicker cpVisualModelForeColor;
		private System.Windows.Forms.CheckBox chkDataModelReadOnly;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmSample13()
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
			this.cpVisualModelForeColor = new SourceLibrary.Windows.Forms.ColorPicker();
			this.label1 = new System.Windows.Forms.Label();
			this.chkDataModelReadOnly = new System.Windows.Forms.CheckBox();
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
			this.grid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.grid1.CustomSort = false;
			this.grid1.GridToolTipActive = true;
			this.grid1.Location = new System.Drawing.Point(8, 56);
			this.grid1.Name = "grid1";
			this.grid1.Size = new System.Drawing.Size(296, 216);
			this.grid1.SpecialKeys = SourceGrid2.GridSpecialKeys.Default;
			this.grid1.TabIndex = 0;
			// 
			// cpVisualModelForeColor
			// 
			this.cpVisualModelForeColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.cpVisualModelForeColor.Location = new System.Drawing.Point(8, 24);
			this.cpVisualModelForeColor.Name = "cpVisualModelForeColor";
			this.cpVisualModelForeColor.SelectedColor = System.Drawing.Color.Black;
			this.cpVisualModelForeColor.Size = new System.Drawing.Size(160, 24);
			this.cpVisualModelForeColor.TabIndex = 1;
			this.cpVisualModelForeColor.SelectedColorChanged += new System.EventHandler(this.cpVisualModelForeColor_SelectedColorChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(136, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "VisualModel ForeColor";
			// 
			// chkDataModelReadOnly
			// 
			this.chkDataModelReadOnly.Location = new System.Drawing.Point(168, 24);
			this.chkDataModelReadOnly.Name = "chkDataModelReadOnly";
			this.chkDataModelReadOnly.Size = new System.Drawing.Size(136, 24);
			this.chkDataModelReadOnly.TabIndex = 3;
			this.chkDataModelReadOnly.Text = "DataModel ReadOnly";
			this.chkDataModelReadOnly.CheckedChanged += new System.EventHandler(this.chkDataModelReadOnly_CheckedChanged);
			// 
			// frmSample13
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(312, 283);
			this.Controls.Add(this.chkDataModelReadOnly);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cpVisualModelForeColor);
			this.Controls.Add(this.grid1);
			this.Name = "frmSample13";
			this.Text = "Basic Grid Operations";
			this.Load += new System.EventHandler(this.frmSample13_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private SourceGrid2.VisualModels.Common m_VisualModel1;
		private SourceGrid2.VisualModels.Common m_VisualModel2;
		private SourceGrid2.DataModels.EditorTextBox m_DataModel;
		private SourceGrid2.BehaviorModels.CustomEvents m_BehaviorModel;

		private void frmSample13_Load(object sender, System.EventArgs e)
		{
			//Create a VisualModel with an image
			m_VisualModel1 = new SourceGrid2.VisualModels.Common();
			m_VisualModel1.Image = SampleImages.FACE01;
			m_VisualModel1.ImageAlignment = ContentAlignment.MiddleRight;

			//Create another VisualModel with an image
			m_VisualModel2 = new SourceGrid2.VisualModels.Common();
			m_VisualModel2.Image = SampleImages.FACE02;
			m_VisualModel2.ImageAlignment = ContentAlignment.MiddleRight;

			cpVisualModelForeColor.SelectedColor = m_VisualModel1.ForeColor;

			//Now Create a DataModel
			m_DataModel = new SourceGrid2.DataModels.EditorTextBox(typeof(string));

			//Create a behavior that change the VIsualModel of a cell when the user move the Mouse over the cell
			m_BehaviorModel = new SourceGrid2.BehaviorModels.CustomEvents();
			m_BehaviorModel.MouseEnter += new SourceGrid2.PositionEventHandler(m_BehaviorModel_MouseEnter);
			m_BehaviorModel.MouseLeave += new SourceGrid2.PositionEventHandler(m_BehaviorModel_MouseLeave);


			//Populate the grid
			grid1.Redim(2,2);
			for (int r = 0; r < grid1.RowsCount;r++)
				for (int c = 0; c < grid1.ColumnsCount; c++)
				{
					SourceGrid2.Cells.Real.Cell l_Cell = new SourceGrid2.Cells.Real.Cell();
					l_Cell.Value = r.ToString() + " " + c.ToString();
					l_Cell.VisualModel = m_VisualModel1;
					l_Cell.DataModel = m_DataModel;
					l_Cell.Behaviors.Add(m_BehaviorModel);
					grid1[r,c] = l_Cell;
				}

			//Now Set the width of the column
			grid1.Columns[0].Width = grid1.DisplayRectangle.Width/2;
			grid1.Columns[1].Width = grid1.DisplayRectangle.Width/2;
		}

		private void m_BehaviorModel_MouseEnter(object sender, SourceGrid2.PositionEventArgs e)
		{
			e.Cell.VisualModel = m_VisualModel2;
		}

		private void m_BehaviorModel_MouseLeave(object sender, SourceGrid2.PositionEventArgs e)
		{
			e.Cell.VisualModel = m_VisualModel1;
		}

		private void cpVisualModelForeColor_SelectedColorChanged(object sender, System.EventArgs e)
		{
			m_VisualModel1.ForeColor = cpVisualModelForeColor.SelectedColor;
			m_VisualModel2.ForeColor = cpVisualModelForeColor.SelectedColor;
			grid1.InvalidateCells();
		}

		private void chkDataModelReadOnly_CheckedChanged(object sender, System.EventArgs e)
		{
			m_DataModel.EnableEdit = !chkDataModelReadOnly.Checked;
		}
	}
}
