using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Cells = SourceGrid2.Cells.Real;

namespace SampleProject
{
	/// <summary>
	/// Summary description for frmSample2.
	/// </summary>
	public class frmSample2 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button cmdExportHTML;
		private SourceGrid2.Grid grid;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmSample2()
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
			this.grid = new SourceGrid2.Grid();
			this.cmdExportHTML = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// grid
			// 
			this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grid.AutoSizeMinHeight = 10;
			this.grid.AutoSizeMinWidth = 10;
			this.grid.AutoStretchColumnsToFitWidth = false;
			this.grid.AutoStretchRowsToFitHeight = false;
			this.grid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.grid.CustomSort = false;
			this.grid.GridToolTipActive = true;
			this.grid.Location = new System.Drawing.Point(4, 32);
			this.grid.Name = "grid";
			this.grid.Size = new System.Drawing.Size(888, 392);
			this.grid.SpecialKeys = SourceGrid2.GridSpecialKeys.Default;
			this.grid.TabIndex = 0;
			// 
			// cmdExportHTML
			// 
			this.cmdExportHTML.Location = new System.Drawing.Point(4, 4);
			this.cmdExportHTML.Name = "cmdExportHTML";
			this.cmdExportHTML.Size = new System.Drawing.Size(96, 23);
			this.cmdExportHTML.TabIndex = 1;
			this.cmdExportHTML.Text = "Export HTML";
			this.cmdExportHTML.Click += new System.EventHandler(this.cmdExportHTML_Click);
			// 
			// frmSample2
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(900, 431);
			this.Controls.Add(this.cmdExportHTML);
			this.Controls.Add(this.grid);
			this.Name = "frmSample2";
			this.Text = "Campionato Italiano";
			this.Load += new System.EventHandler(this.frmSample2_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmSample2_Load(object sender, System.EventArgs e)
		{
			string[] l_Teams = new string[]{"Milan",
												 "Roma",
												 "Juventus",
												 "Inter",
												 "Parma",
												 "Lazio",
												 "Udinese",
												 "Sampdoria",
												 "Chievo",
												 "Brescia",
												 "Siena",
												 "Bologna",
												 "Reggina",
												 "Modena",
												 "Lecce",
												 "Empoli",
												 "Perugia",
												 "Ancona"};

			int[] l_Points = new int[]{48,
										  43,
										  43,
										  35,
										  33,
										  33,
										  30,
										  28,
										  25,
										  22,
										  21,
										  21,
										  20,
										  18,
										  15,
										  13,
										  11,
										  5};

			grid.Redim(l_Teams.Length+1,3);
			grid.BackColor = Color.White;

			SourceGrid2.VisualModels.Common l_NoBorder = new SourceGrid2.VisualModels.Common();
			l_NoBorder.Border = SourceGrid2.RectangleBorder.NoBorder;
			
			grid.FixedRows = 1;
			grid[0,0] = new Cells.ColumnHeader("Teams");
			grid[0,1] = new Cells.ColumnHeader("Points");
			grid[0,2] = new Cells.ColumnHeader();

			for (int r = 0; r < l_Teams.Length; r++)
			{
				grid[r+1,0] = new Cells.Cell(l_Teams[r],null, l_NoBorder);
				grid[r+1,1] = new CellPoints(l_Points[r]);
				((CellPoints)grid[r+1,1]).RefreshBalls();
			}

			((SourceGrid2.Cells.Real.Cell)grid[0,2]).ColumnSpan = grid.ColumnsCount-2;

			grid.AutoSizeAll();
		}

		private void cmdExportHTML_Click(object sender, System.EventArgs e)
		{
			try
			{
				string l_Path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "tmpSourceGridExport.htm");

				using (System.IO.FileStream l_Stream = new System.IO.FileStream(l_Path,System.IO.FileMode.Create,System.IO.FileAccess.Write))
				{
					grid.ExportHTML(new SourceGrid2.HTMLExport(SourceGrid2.ExportHTMLMode.Default, System.IO.Path.GetTempPath(), "", l_Stream));
					l_Stream.Close();
				}

				SourceLibrary.Utility.Shell.OpenFile(l_Path);
			}
			catch(Exception err)
			{
				SourceLibrary.Windows.Forms.ErrorDialog.Show(this,err,"HTML Export Error");
			}		
		}

		public class CellPoints : SourceGrid2.Cells.Real.Cell
		{
			private static 	SourceGrid2.VisualModels.Common s_NoBorderInt;
			static CellPoints()
			{
				s_NoBorderInt= new SourceGrid2.VisualModels.Common();
				s_NoBorderInt.Border = SourceGrid2.RectangleBorder.NoBorder;
				s_NoBorderInt.TextAlignment = ContentAlignment.MiddleRight;
				s_NoBorderInt.MakeReadOnly();
			}

			public CellPoints(int p_Value):base(p_Value, typeof(int))
			{
				VisualModel = s_NoBorderInt;
			}

			public override void OnValueChanged(SourceGrid2.PositionEventArgs e)
			{
				base.OnValueChanged (e);

				RefreshBalls();
			}

			public void RefreshBalls()
			{
				if (Grid != null)
				{
					int l_NewCols = Points - (Grid.ColumnsCount-2);
					if (l_NewCols > 0)
					{
						Grid.Columns.InsertRange(Grid.ColumnsCount, l_NewCols);
					}

					for (int c = 2; c < Grid.ColumnsCount; c++)
					{
						if ((c-2) < Points)
						{
							if (Grid.GetCell(Row, c) == null)
							{
								Grid.SetCell(Row, c, new CellBall());
							}
						}
						else
							Grid.SetCell(Row, c, null);
					}

					Grid.AutoSizeAll();
				}			
			}

			private int Points
			{
				get{return (int)Value;}
			}
		}

		public class CellBall : SourceGrid2.Cells.Real.Cell
		{
			private static 	SourceGrid2.VisualModels.Common s_Properties;
			static CellBall()
			{
				s_Properties = new SourceGrid2.VisualModels.Common();
				s_Properties.Border = SourceGrid2.RectangleBorder.NoBorder;
				System.IO.Stream l_Stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("SampleProject.Samples.CalcioSmall.bmp");
				s_Properties.Image = Image.FromStream(l_Stream);
				s_Properties.ImageAlignment = ContentAlignment.MiddleCenter;
				s_Properties.MakeReadOnly();
			}

			public CellBall():base()
			{
				VisualModel = s_Properties;
			}			
		}
	}
}
