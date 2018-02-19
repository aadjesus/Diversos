using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;

namespace TestProgressListCtrl
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private static DataSet m_oDataSet = null;
		private CProgressControls.CProgressDataGrid cProgressDataGrid1;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			m_oDataSet = new DataSet();
			m_oDataSet.ReadXml("testDataSet.xml");

			cProgressDataGrid1.DataSource = m_oDataSet;
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
			this.cProgressDataGrid1 = new CProgressControls.CProgressDataGrid();
			this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
			this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.cProgressDataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// cProgressDataGrid1
			// 
			this.cProgressDataGrid1.AllowSorting = false;
			this.cProgressDataGrid1.DataMember = "";
			this.cProgressDataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cProgressDataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.cProgressDataGrid1.Name = "cProgressDataGrid1";
			this.cProgressDataGrid1.ProgressColor = System.Drawing.SystemColors.Control;
			this.cProgressDataGrid1.ProgressColumns = "3";
			this.cProgressDataGrid1.ProgressDataTableName = "Table1";
			this.cProgressDataGrid1.ProgressPercentageColor = System.Drawing.Color.Black;
			this.cProgressDataGrid1.ProgressShowPercentage = true;
			this.cProgressDataGrid1.ProgressStyle = CProgressControls.EProgressDataGridStyle.e3DSet;
			this.cProgressDataGrid1.Size = new System.Drawing.Size(576, 273);
			this.cProgressDataGrid1.TabIndex = 0;
			this.cProgressDataGrid1.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
																										   this.dataGridTableStyle1});
			// 
			// dataGridTableStyle1
			// 
			this.dataGridTableStyle1.DataGrid = this.cProgressDataGrid1;
			this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
																												  this.dataGridTextBoxColumn1,
																												  this.dataGridTextBoxColumn2,
																												  this.dataGridTextBoxColumn3,
																												  this.dataGridTextBoxColumn4});
			this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridTableStyle1.MappingName = "";
			// 
			// dataGridTextBoxColumn1
			// 
			this.dataGridTextBoxColumn1.Format = "";
			this.dataGridTextBoxColumn1.FormatInfo = null;
			this.dataGridTextBoxColumn1.HeaderText = "oid";
			this.dataGridTextBoxColumn1.MappingName = "";
			this.dataGridTextBoxColumn1.Width = 75;
			// 
			// dataGridTextBoxColumn2
			// 
			this.dataGridTextBoxColumn2.Format = "";
			this.dataGridTextBoxColumn2.FormatInfo = null;
			this.dataGridTextBoxColumn2.HeaderText = "name";
			this.dataGridTextBoxColumn2.MappingName = "";
			this.dataGridTextBoxColumn2.Width = 75;
			// 
			// dataGridTextBoxColumn3
			// 
			this.dataGridTextBoxColumn3.Format = "";
			this.dataGridTextBoxColumn3.FormatInfo = null;
			this.dataGridTextBoxColumn3.HeaderText = "description";
			this.dataGridTextBoxColumn3.MappingName = "";
			this.dataGridTextBoxColumn3.Width = 75;
			// 
			// dataGridTextBoxColumn4
			// 
			this.dataGridTextBoxColumn4.Format = "";
			this.dataGridTextBoxColumn4.FormatInfo = null;
			this.dataGridTextBoxColumn4.HeaderText = "progess";
			this.dataGridTextBoxColumn4.MappingName = "";
			this.dataGridTextBoxColumn4.Width = 175;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(576, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.cProgressDataGrid1});
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.cProgressDataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		#region events
		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			m_oDataSet.WriteXml("testDataSet.xml");		
		}
		#endregion
	}
}
