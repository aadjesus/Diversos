using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Cells = SourceGrid2.Cells.Real;
namespace SampleProject
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class StartForm : System.Windows.Forms.Form
	{
		private SourceGrid2.Grid grid;
        private Extensions.GridArray gridArray1;
        private Extensions.GridBarChart gridBarChart1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public StartForm()
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
				if (components != null) 
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
            this.grid = new SourceGrid2.Grid();
            this.gridArray1 = new SampleProject.Extensions.GridArray();
            this.gridBarChart1 = new SampleProject.Extensions.GridBarChart();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.AutoSizeMinHeight = 10;
            this.grid.AutoSizeMinWidth = 10;
            this.grid.AutoStretchColumnsToFitWidth = false;
            this.grid.AutoStretchRowsToFitHeight = false;
            this.grid.ContextMenuStyle = SourceGrid2.ContextMenuStyle.None;
            this.grid.CustomSort = false;
            this.grid.FocusStyle = SourceGrid2.FocusStyle.None;
            this.grid.GridToolTipActive = true;
            this.grid.Location = new System.Drawing.Point(188, 201);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(368, 410);
            this.grid.SpecialKeys = ((SourceGrid2.GridSpecialKeys)(((((((((SourceGrid2.GridSpecialKeys.Ctrl_C | SourceGrid2.GridSpecialKeys.Ctrl_V) 
            | SourceGrid2.GridSpecialKeys.Ctrl_X) 
            | SourceGrid2.GridSpecialKeys.Delete) 
            | SourceGrid2.GridSpecialKeys.Arrows) 
            | SourceGrid2.GridSpecialKeys.Tab) 
            | SourceGrid2.GridSpecialKeys.PageDownUp) 
            | SourceGrid2.GridSpecialKeys.Enter) 
            | SourceGrid2.GridSpecialKeys.Escape)));
            this.grid.TabIndex = 0;
            // 
            // gridArray1
            // 
            this.gridArray1.AutoSizeMinHeight = 10;
            this.gridArray1.AutoSizeMinWidth = 10;
            this.gridArray1.AutoStretchColumnsToFitWidth = false;
            this.gridArray1.AutoStretchRowsToFitHeight = false;
            this.gridArray1.ContextMenuStyle = SourceGrid2.ContextMenuStyle.None;
            this.gridArray1.EnableEdit = true;
            this.gridArray1.FocusStyle = SourceGrid2.FocusStyle.None;
            this.gridArray1.GridToolTipActive = true;
            this.gridArray1.Location = new System.Drawing.Point(37, 118);
            this.gridArray1.Name = "gridArray1";
            this.gridArray1.Size = new System.Drawing.Size(200, 200);
            this.gridArray1.SpecialKeys = ((SourceGrid2.GridSpecialKeys)(((((((((SourceGrid2.GridSpecialKeys.Ctrl_C | SourceGrid2.GridSpecialKeys.Ctrl_V) 
            | SourceGrid2.GridSpecialKeys.Ctrl_X) 
            | SourceGrid2.GridSpecialKeys.Delete) 
            | SourceGrid2.GridSpecialKeys.Arrows) 
            | SourceGrid2.GridSpecialKeys.Tab) 
            | SourceGrid2.GridSpecialKeys.PageDownUp) 
            | SourceGrid2.GridSpecialKeys.Enter) 
            | SourceGrid2.GridSpecialKeys.Escape)));
            this.gridArray1.TabIndex = 1;
            // 
            // gridBarChart1
            // 
            this.gridBarChart1.Location = new System.Drawing.Point(177, 137);
            this.gridBarChart1.Name = "gridBarChart1";
            this.gridBarChart1.Size = new System.Drawing.Size(265, 444);
            this.gridBarChart1.StepNumber = 20;
            this.gridBarChart1.TabIndex = 2;
            // 
            // StartForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(556, 611);
            this.Controls.Add(this.gridBarChart1);
            this.Controls.Add(this.gridArray1);
            this.Controls.Add(this.grid);
            this.Name = "StartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SourceGrid Sample Application";
            this.Load += new System.EventHandler(this.StartForm_Load);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new StartForm());
		}

		private void StartForm_Load(object sender, System.EventArgs e)
		{
			grid.Redim(39, 3);
			grid.BackColor = Color.WhiteSmoke;

			#region VisualProperties
			SourceGrid2.VisualModels.Common l_PropertiesCaption = new SourceGrid2.VisualModels.Common();
			l_PropertiesCaption.Border = SourceGrid2.RectangleBorder.NoBorder;
			l_PropertiesCaption.BackColor = grid.BackColor;
			l_PropertiesCaption.Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);
			l_PropertiesCaption.TextAlignment = ContentAlignment.MiddleCenter;

			SourceGrid2.VisualModels.Common l_PropertiesLink = (SourceGrid2.VisualModels.Common)SourceGrid2.VisualModels.Common.LinkStyle.Clone(false);
			l_PropertiesLink.Border = new SourceGrid2.RectangleBorder(new SourceGrid2.Border(Color.Gray, 1));
			l_PropertiesLink.TextAlignment = ContentAlignment.MiddleCenter;

			SourceGrid2.VisualModels.Common l_PropertiesDescription = new SourceGrid2.VisualModels.Common();
			l_PropertiesDescription.Border = SourceGrid2.RectangleBorder.NoBorder;
			l_PropertiesDescription.BackColor = grid.BackColor;

			SourceGrid2.VisualModels.Common l_PropertiesVersion = (SourceGrid2.VisualModels.Common)(SourceGrid2.VisualModels.Common.LinkStyle.Clone(false));
			l_PropertiesVersion.Border = SourceGrid2.RectangleBorder.NoBorder;
			l_PropertiesVersion.BackColor = grid.BackColor;
			l_PropertiesVersion.TextAlignment = ContentAlignment.TopRight;

			SourceGrid2.VisualModels.Common l_PropertiesRegion = new SourceGrid2.VisualModels.Common();
			l_PropertiesRegion.Border = SourceGrid2.RectangleBorder.RectangleBlack1Width;
			l_PropertiesRegion.BackColor = Color.SteelBlue;
			l_PropertiesRegion.ForeColor = Color.White;
			l_PropertiesRegion.TextAlignment = ContentAlignment.MiddleCenter;
			#endregion

			#region Caption
			grid[1,1] = new Cells.Cell("SourceGrid 2");
			grid[1,1].VisualModel = l_PropertiesCaption;
			grid[1,1].ColumnSpan = 2;

			Cells.Link l_CellVersion = new Cells.Link("V." + GetSourceGridFileVersion());
			grid[0,2] = l_CellVersion;
			grid[0,2].VisualModel = l_PropertiesVersion;
			l_CellVersion.ToolTipText = "www.devage.com";
			l_CellVersion.Click += new SourceGrid2.PositionEventHandler(l_CellVersion_Click);
			#endregion

			int l_CurrentRow = 3;

			#region Basic Operations
			grid[l_CurrentRow,1] = new SourceGrid2.Cells.Real.Cell("Basic Operations", null, l_PropertiesRegion);
			grid[l_CurrentRow,1].ColumnSpan = 2;
			l_CurrentRow++;

			#region Sample 14
			grid[l_CurrentRow,1] = new CellLinkSample("Sample 14", typeof(frmSample14));
			grid[l_CurrentRow,1].VisualModel = l_PropertiesLink;

			grid[l_CurrentRow,2] = new Cells.Cell("First Example Grid");
			grid[l_CurrentRow,2].VisualModel = l_PropertiesDescription;

			l_CurrentRow += 2;
			#endregion

			#region Sample 13
			grid[l_CurrentRow,1] = new CellLinkSample("Sample 13", typeof(frmSample13));
			grid[l_CurrentRow,1].VisualModel = l_PropertiesLink;

			grid[l_CurrentRow,2] = new Cells.Cell("Basic Grid Operations - Grid, Cell, DataModel, VisualModel, BehaviorModel");
			grid[l_CurrentRow,2].VisualModel = l_PropertiesDescription;

			l_CurrentRow += 2;
			#endregion

			#region Sample 3
			grid[l_CurrentRow,1] = new CellLinkSample("Sample 3", typeof(frmSample3));
			grid[l_CurrentRow,1].VisualModel = l_PropertiesLink;

			grid[l_CurrentRow,2] = new Cells.Cell("Cell Editors, Special Cells, Formatting and Image");
			grid[l_CurrentRow,2].VisualModel = l_PropertiesDescription;

			l_CurrentRow += 2;
			#endregion
			#endregion

			#region Real Grids
			grid[l_CurrentRow,1] = new SourceGrid2.Cells.Real.Cell("Real Grids", null, l_PropertiesRegion);
			grid[l_CurrentRow,1].ColumnSpan = 2;
			l_CurrentRow++;

			#region Sample 1
			grid[l_CurrentRow,1] = new CellLinkSample("Sample 1", typeof(frmSample1));
			grid[l_CurrentRow,1].VisualModel = l_PropertiesLink;

			grid[l_CurrentRow,2] = new Cells.Cell("Fixed Cells, Sort, Resize, Editors, MoveColumns, Add/Remove Rows (Standard Grid)");
			grid[l_CurrentRow,2].VisualModel = l_PropertiesDescription;

			l_CurrentRow += 2;
			#endregion

			#region Sample 4
			grid[l_CurrentRow,1] = new CellLinkSample("Sample 4", typeof(frmSample4));
			grid[l_CurrentRow,1].VisualModel = l_PropertiesLink;

			grid[l_CurrentRow,2] = new Cells.Cell("Grid Performance (static Cell + Shared VisualProperties and Editor)");
			grid[l_CurrentRow,2].VisualModel = l_PropertiesDescription;

			l_CurrentRow += 2;
			#endregion
			#endregion

			#region Virtual Grids
			grid[l_CurrentRow,1] = new SourceGrid2.Cells.Real.Cell("Virtual Grids", null, l_PropertiesRegion);
			grid[l_CurrentRow,1].ColumnSpan = 2;
			grid[l_CurrentRow,0] = new CellNew();
			l_CurrentRow++;

			#region Sample 15
			grid[l_CurrentRow,1] = new CellLinkSample("Sample 15", typeof(frmSample15));
			grid[l_CurrentRow,1].VisualModel = l_PropertiesLink;

			grid[l_CurrentRow,2] = new Cells.Cell("Virtual Grid - Simple Array Binding");
			grid[l_CurrentRow,2].VisualModel = l_PropertiesDescription;

			l_CurrentRow += 2;
			#endregion

			#region Sample 5
			grid[l_CurrentRow,1] = new CellLinkSample("Sample 5", typeof(frmSample5));
			grid[l_CurrentRow,1].VisualModel = l_PropertiesLink;

			grid[l_CurrentRow,2] = new Cells.Cell("Virtual Grid - ReadOnly Grid with auto generated values");
			grid[l_CurrentRow,2].VisualModel = l_PropertiesDescription;

			l_CurrentRow += 2;
			#endregion

			#region Sample 6
			grid[l_CurrentRow,1] = new CellLinkSample("Sample 6", typeof(frmSample6));
			grid[l_CurrentRow,1].VisualModel = l_PropertiesLink;

			grid[l_CurrentRow,2] = new Cells.Cell("Virtual Grid - Array Binding");
			grid[l_CurrentRow,2].VisualModel = l_PropertiesDescription;

			l_CurrentRow += 2;
			#endregion

			#region Sample 9
			grid[l_CurrentRow,1] = new CellLinkSample("Sample 9", typeof(frmSample9));
			grid[l_CurrentRow,1].VisualModel = l_PropertiesLink;

			grid[l_CurrentRow,2] = new Cells.Cell("Virtual Grid - DataTable Binding");
			grid[l_CurrentRow,2].VisualModel = l_PropertiesDescription;

			l_CurrentRow += 2;
			#endregion

			#region Sample 11
			grid[l_CurrentRow,1] = new CellLinkSample("Sample 11", typeof(frmSample11));
			grid[l_CurrentRow,1].VisualModel = l_PropertiesLink;

			grid[l_CurrentRow,2] = new Cells.Cell("Virtual Grid - WorkSheet Style Grid");
			grid[l_CurrentRow,2].VisualModel = l_PropertiesDescription;

			l_CurrentRow += 2;
			#endregion

			#region Sample 12
			grid[l_CurrentRow,1] = new CellLinkSample("Sample 12", typeof(frmSample12));
			grid[l_CurrentRow,1].VisualModel = l_PropertiesLink;

			grid[l_CurrentRow,2] = new Cells.Cell("Virtual Grid - Xml Binding and Virtuals Cells");
			grid[l_CurrentRow,2].VisualModel = l_PropertiesDescription;

			l_CurrentRow += 2;
			#endregion
			#endregion

			#region Unusual Grids
			grid[l_CurrentRow,1] = new SourceGrid2.Cells.Real.Cell("Unusual Grids", null, l_PropertiesRegion);
			grid[l_CurrentRow,1].ColumnSpan = 2;
			l_CurrentRow++;

			#region Sample 7
			grid[l_CurrentRow,1] = new CellLinkSample("Sample 7", typeof(frmSample7));
			grid[l_CurrentRow,1].VisualModel = l_PropertiesLink;

			grid[l_CurrentRow,2] = new Cells.Cell("Generic List Editor + Chart Bar Simulation 2");
			grid[l_CurrentRow,2].VisualModel = l_PropertiesDescription;

			l_CurrentRow += 2;
			#endregion

			#region Sample 2
			grid[l_CurrentRow,1] = new CellLinkSample("Sample 2", typeof(frmSample2));
			grid[l_CurrentRow,1].VisualModel = l_PropertiesLink;

			grid[l_CurrentRow,2] = new Cells.Cell("Image, Custom Cells, Editor (Bar Chart Simulation)");
			grid[l_CurrentRow,2].VisualModel = l_PropertiesDescription;

			l_CurrentRow += 2;
			#endregion
			#endregion

			#region Other Features
			grid[l_CurrentRow,1] = new SourceGrid2.Cells.Real.Cell("Other Features", null, l_PropertiesRegion);
			grid[l_CurrentRow,1].ColumnSpan = 2;
			l_CurrentRow++;
			#region Sample 8
			grid[l_CurrentRow,1] = new CellLinkSample("Sample 8", typeof(frmSample8));
			grid[l_CurrentRow,1].VisualModel = l_PropertiesLink;

			grid[l_CurrentRow,2] = new Cells.Cell("Cell Events and Behaviors");
			grid[l_CurrentRow,2].VisualModel = l_PropertiesDescription;

			l_CurrentRow += 2;
			#endregion

			#region Sample 10
			grid[l_CurrentRow,1] = new CellLinkSample("Sample 10", typeof(frmSample10));
			grid[l_CurrentRow,1].VisualModel = l_PropertiesLink;

			grid[l_CurrentRow,2] = new Cells.Cell("ContextMenu");
			grid[l_CurrentRow,2].VisualModel = l_PropertiesDescription;

			l_CurrentRow += 2;
			#endregion

			#region Sample 16
			grid[l_CurrentRow,1] = new CellLinkSample("Sample 16", typeof(frmSample16));
			grid[l_CurrentRow,1].VisualModel = l_PropertiesLink;

			grid[l_CurrentRow,2] = new Cells.Cell("SpecialKeys and FocusStyle");
			grid[l_CurrentRow,2].VisualModel = l_PropertiesDescription;

			l_CurrentRow += 2;
			#endregion
			#endregion


			grid.AutoSizeAll();
			grid.AutoStretchColumnsToFitWidth = true;
			grid.StretchColumnsToFitWidth();
		}
		

		private class CellLinkSample : Cells.Link
		{
			private Type m_FormType;
			public CellLinkSample(string p_Caption, Type p_FormType):base(p_Caption)
			{
				m_FormType = p_FormType;
			}

			public override void OnClick(SourceGrid2.PositionEventArgs e)
			{
				base.OnClick (e);

				using (Form f = (Form)Activator.CreateInstance(m_FormType))
				{
					f.ShowDialog(Grid);
				}
			}
		}

		private void l_CellVersion_Click(object sender, SourceGrid2.PositionEventArgs e)
		{
			try
			{
				SourceLibrary.Utility.Shell.OpenFile(@"http://www.devage.com/");
			}
			catch(Exception err)
			{
				SourceLibrary.Windows.Forms.ErrorDialog.Show(this, err, "Error");
			}
		}

		private string GetSourceGridFileVersion()
		{
			try
			{
				return System.Diagnostics.FileVersionInfo.GetVersionInfo(grid.GetType().Assembly.Location).FileVersion;
			}
			catch(Exception )
			{
				return "NA";
			}
		}
	}
}
