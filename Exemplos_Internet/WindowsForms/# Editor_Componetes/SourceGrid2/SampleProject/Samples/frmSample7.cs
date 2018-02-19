using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Cells = SourceGrid2.Cells.Real;

namespace SampleProject
{
	/// <summary>
	/// Summary description for frmSample7.
	/// </summary>
	public class frmSample7 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		private SampleProject.Extensions.ListEditor listEditor1;
		private System.Windows.Forms.Splitter splitter1;
		private SampleProject.Extensions.GridBarChart gridBarChart1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmSample7()
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.gridBarChart1 = new SampleProject.Extensions.GridBarChart();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.listEditor1 = new SampleProject.Extensions.ListEditor();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.gridBarChart1);
			this.panel1.Controls.Add(this.splitter1);
			this.panel1.Controls.Add(this.listEditor1);
			this.panel1.Location = new System.Drawing.Point(4, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(760, 492);
			this.panel1.TabIndex = 1;
			// 
			// gridBarChart1
			// 
			this.gridBarChart1.BackColor = System.Drawing.SystemColors.Window;
			this.gridBarChart1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridBarChart1.Location = new System.Drawing.Point(259, 0);
			this.gridBarChart1.Name = "gridBarChart1";
			this.gridBarChart1.Size = new System.Drawing.Size(499, 490);
			this.gridBarChart1.StepNumber = 20;
			this.gridBarChart1.TabIndex = 4;
			// 
			// splitter1
			// 
			this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitter1.Location = new System.Drawing.Point(256, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 490);
			this.splitter1.TabIndex = 2;
			this.splitter1.TabStop = false;
			// 
			// listEditor1
			// 
			this.listEditor1.Dock = System.Windows.Forms.DockStyle.Left;
			this.listEditor1.Editors = null;
			this.listEditor1.ItemType = null;
			this.listEditor1.List = null;
			this.listEditor1.Location = new System.Drawing.Point(0, 0);
			this.listEditor1.Name = "listEditor1";
			this.listEditor1.Properties = null;
			this.listEditor1.Size = new System.Drawing.Size(256, 490);
			this.listEditor1.TabIndex = 1;
			this.listEditor1.ListChanged += new System.EventHandler(this.listEditor1_ListChanged);
			// 
			// frmSample7
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(768, 499);
			this.Controls.Add(this.panel1);
			this.Name = "frmSample7";
			this.Text = "Bar Chart";
			this.Load += new System.EventHandler(this.frmSample7_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmSample7_Load(object sender, System.EventArgs e)
		{
			gridBarChart1.Bars.Add(new Extensions.ChartBar("Bar 1", 65, Color.Red, Color.White));
			gridBarChart1.Bars.Add(new Extensions.ChartBar("Bar 2", 70, Color.Silver, Color.Black));
			gridBarChart1.Bars.Add(new Extensions.ChartBar("Bar 3", 100, Color.Green, Color.White));
			gridBarChart1.Bars.Add(new Extensions.ChartBar("Bar 4", 80, Color.SteelBlue, Color.White));
			gridBarChart1.Bars.Add(new Extensions.ChartBar("Bar 5", 60, Color.LightGreen, Color.Black));
			gridBarChart1.LoadChart();

			listEditor1.List = new ArrayList(gridBarChart1.Bars);
			listEditor1.ItemType = typeof(Extensions.ChartBar);
			listEditor1.LoadList();
		}

		private void listEditor1_ListChanged(object sender, System.EventArgs e)
		{
			gridBarChart1.Bars.Clear();
			foreach (Extensions.ChartBar bar in listEditor1.List)
				gridBarChart1.Bars.Add(bar);

			gridBarChart1.LoadChart();
		}
	}
}
