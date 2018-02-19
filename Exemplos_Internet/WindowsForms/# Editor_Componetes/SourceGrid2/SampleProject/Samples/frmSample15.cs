using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SampleProject
{
	/// <summary>
	/// Summary description for frmSample15.
	/// </summary>
	public class frmSample15 : System.Windows.Forms.Form
	{
		private SourceGrid2.GridVirtual gridVirtual1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmSample15()
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
			this.gridVirtual1 = new SourceGrid2.GridVirtual();
			this.SuspendLayout();
			// 
			// gridVirtual1
			// 
			this.gridVirtual1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.gridVirtual1.AutoSizeMinHeight = 10;
			this.gridVirtual1.AutoSizeMinWidth = 10;
			this.gridVirtual1.AutoStretchColumnsToFitWidth = false;
			this.gridVirtual1.AutoStretchRowsToFitHeight = false;
			this.gridVirtual1.GridToolTipActive = true;
			this.gridVirtual1.Location = new System.Drawing.Point(4, 4);
			this.gridVirtual1.Name = "gridVirtual1";
			this.gridVirtual1.Size = new System.Drawing.Size(284, 260);
			this.gridVirtual1.SpecialKeys = SourceGrid2.GridSpecialKeys.Default;
			this.gridVirtual1.TabIndex = 0;
			// 
			// frmSample15
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Controls.Add(this.gridVirtual1);
			this.Name = "frmSample15";
			this.Text = "Simple Virtual Grid";
			this.Load += new System.EventHandler(this.frmSample15_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmSample15_Load(object sender, System.EventArgs e)
		{
			gridVirtual1.GettingCell += new SourceGrid2.PositionEventHandler(gridVirtual1_GettingCell);
			gridVirtual1.Redim(1000,1000);
			string[,] l_Array = new string[gridVirtual1.RowsCount, gridVirtual1.ColumnsCount];
			m_CellStringArray = new CellStringArray(l_Array);
			m_CellStringArray.BindToGrid(gridVirtual1);
		}

		private CellStringArray m_CellStringArray;
		private void gridVirtual1_GettingCell(object sender, SourceGrid2.PositionEventArgs e)
		{
			e.Cell = m_CellStringArray;
		}

		public class CellStringArray : SourceGrid2.Cells.Virtual.CellVirtual
		{
			private string[,] m_Array;
			public CellStringArray(string[,] p_Array):base(typeof(string))
			{
				m_Array = p_Array;
			}
			public override object GetValue(SourceGrid2.Position p_Position)
			{
				return m_Array[p_Position.Row, p_Position.Column];
			}
			public override void SetValue(SourceGrid2.Position p_Position, object p_Value)
			{
				m_Array[p_Position.Row, p_Position.Column] = (string)p_Value;
				OnValueChanged(new SourceGrid2.PositionEventArgs(p_Position, this));
			}
		}
	}
}
