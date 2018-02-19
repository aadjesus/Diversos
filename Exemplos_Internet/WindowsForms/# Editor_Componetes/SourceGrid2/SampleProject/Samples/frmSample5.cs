using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SampleProject
{
	/// <summary>
	/// Summary description for frmSample5.
	/// </summary>
	public class frmSample5 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panelGrid;
		private System.Windows.Forms.Button btLoad;
		private System.Windows.Forms.TextBox txtCols;
		private System.Windows.Forms.TextBox txtRows;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmSample5()
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
			this.panelGrid = new System.Windows.Forms.Panel();
			this.btLoad = new System.Windows.Forms.Button();
			this.txtCols = new System.Windows.Forms.TextBox();
			this.txtRows = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// panelGrid
			// 
			this.panelGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panelGrid.Location = new System.Drawing.Point(4, 36);
			this.panelGrid.Name = "panelGrid";
			this.panelGrid.Size = new System.Drawing.Size(544, 464);
			this.panelGrid.TabIndex = 0;
			// 
			// btLoad
			// 
			this.btLoad.Location = new System.Drawing.Point(296, 8);
			this.btLoad.Name = "btLoad";
			this.btLoad.TabIndex = 6;
			this.btLoad.Text = "Load";
			this.btLoad.Click += new System.EventHandler(this.btLoad_Click);
			// 
			// txtCols
			// 
			this.txtCols.Location = new System.Drawing.Point(220, 8);
			this.txtCols.Name = "txtCols";
			this.txtCols.Size = new System.Drawing.Size(72, 20);
			this.txtCols.TabIndex = 5;
			this.txtCols.Text = "10000";
			// 
			// txtRows
			// 
			this.txtRows.Location = new System.Drawing.Point(72, 8);
			this.txtRows.Name = "txtRows";
			this.txtRows.Size = new System.Drawing.Size(72, 20);
			this.txtRows.TabIndex = 4;
			this.txtRows.Text = "10000";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(160, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 23);
			this.label1.TabIndex = 7;
			this.label1.Text = "Columns:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 23);
			this.label2.TabIndex = 8;
			this.label2.Text = "Rows:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// frmSample5
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(552, 507);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btLoad);
			this.Controls.Add(this.txtCols);
			this.Controls.Add(this.txtRows);
			this.Controls.Add(this.panelGrid);
			this.Name = "frmSample5";
			this.Text = "ReadOnly Full Virtual Grid";
			this.Load += new System.EventHandler(this.frmSample5_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void btLoad_Click(object sender, System.EventArgs e)
		{
			m_GridSample5.Redim(int.Parse(txtRows.Text), int.Parse(txtCols.Text));
		}

		private GridSample5 m_GridSample5;
		private void frmSample5_Load(object sender, System.EventArgs e)
		{
			m_GridSample5 = new GridSample5();
			m_GridSample5.Dock = DockStyle.Fill;
			m_GridSample5.BorderStyle = BorderStyle.FixedSingle;
			panelGrid.Controls.Add(m_GridSample5);
		}

		public class GridSample5 : SourceGrid2.GridVirtual
		{
			private CellSample5 m_VirtualCell;

			public GridSample5()
			{
				m_VirtualCell = new CellSample5();
				//N.B. remember to call this method to link the cell at the grid
				m_VirtualCell.BindToGrid(this);
			}

			public override SourceGrid2.Cells.ICellVirtual GetCell(int p_iRow, int p_iCol)
			{
				return m_VirtualCell;
			}

			public override void SetCell(int p_iRow, int p_iCol, SourceGrid2.Cells.ICellVirtual p_Cell)
			{
				throw new ApplicationException("Not Implemented");
			}

		}
		public class CellSample5 : SourceGrid2.Cells.Virtual.CellVirtual
		{
			public override object GetValue(SourceGrid2.Position p_Position)
			{
				return p_Position.ToString();
			}

			public override void SetValue(SourceGrid2.Position p_Position, object p_Value)
			{
				throw new ApplicationException("Not Implemented");
			}
		}
	}
}
