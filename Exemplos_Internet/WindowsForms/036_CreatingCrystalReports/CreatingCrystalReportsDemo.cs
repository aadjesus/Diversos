
/*
 *		Author : Ishara Gunarathna 
 *		Date   : 10 January 2006
 *		e-mail : isharasjc@yahoo.com
 *		Class  : CreatingCrystalReportsDemo
 */


using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace CreatingCrystalReports
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class CreatingCrystalReportsDemo : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage DataGridView;
		private System.Windows.Forms.TabPage CrystalReportView;
		private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
		private CreatingCrystalReports.myDataset myDataset1;
		private System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1;
		private System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
		private System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
		private System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
		private System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
		private System.Data.OleDb.OleDbConnection oleDbConnection1;
		private System.Windows.Forms.DataGrid dataGrid1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CreatingCrystalReportsDemo()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreatingCrystalReportsDemo));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.DataGridView = new System.Windows.Forms.TabPage();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.myDataset1 = new CreatingCrystalReports.myDataset();
            this.CrystalReportView = new System.Windows.Forms.TabPage();
            this.oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter();
            this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbConnection1 = new System.Data.OleDb.OleDbConnection();
            this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
            this.tabControl1.SuspendLayout();
            this.DataGridView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myDataset1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.DataGridView);
            this.tabControl1.Controls.Add(this.CrystalReportView);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(512, 302);
            this.tabControl1.TabIndex = 0;
            // 
            // DataGridView
            // 
            this.DataGridView.Controls.Add(this.dataGrid1);
            this.DataGridView.Location = new System.Drawing.Point(4, 22);
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.Size = new System.Drawing.Size(504, 276);
            this.DataGridView.TabIndex = 0;
            this.DataGridView.Text = "Data Grid View";
            // 
            // dataGrid1
            // 
            this.dataGrid1.DataMember = "";
            this.dataGrid1.DataSource = this.myDataset1.myPersonalInfoTable;
            this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGrid1.Location = new System.Drawing.Point(0, 0);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(504, 276);
            this.dataGrid1.TabIndex = 0;
            // 
            // myDataset1
            // 
            this.myDataset1.DataSetName = "myDataset";
            this.myDataset1.Locale = new System.Globalization.CultureInfo("en-US");
            this.myDataset1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // CrystalReportView
            // 
            this.CrystalReportView.Location = new System.Drawing.Point(4, 22);
            this.CrystalReportView.Name = "CrystalReportView";
            this.CrystalReportView.Size = new System.Drawing.Size(504, 276);
            this.CrystalReportView.TabIndex = 1;
            this.CrystalReportView.Text = "Crystal Report View";
            // 
            // oleDbDataAdapter1
            // 
            this.oleDbDataAdapter1.DeleteCommand = this.oleDbDeleteCommand1;
            this.oleDbDataAdapter1.InsertCommand = this.oleDbInsertCommand1;
            this.oleDbDataAdapter1.SelectCommand = this.oleDbSelectCommand1;
            this.oleDbDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "studentInfo", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("address", "address"),
                        new System.Data.Common.DataColumnMapping("birthDate", "birthDate"),
                        new System.Data.Common.DataColumnMapping("contactNo", "contactNo"),
                        new System.Data.Common.DataColumnMapping("firstName", "firstName"),
                        new System.Data.Common.DataColumnMapping("lastName", "lastName"),
                        new System.Data.Common.DataColumnMapping("studentID", "studentID")})});
            this.oleDbDataAdapter1.UpdateCommand = this.oleDbUpdateCommand1;
            // 
            // oleDbDeleteCommand1
            // 
            this.oleDbDeleteCommand1.CommandText = resources.GetString("oleDbDeleteCommand1.CommandText");
            this.oleDbDeleteCommand1.Connection = this.oleDbConnection1;
            this.oleDbDeleteCommand1.Parameters.AddRange(new System.Data.OleDb.OleDbParameter[] {
            new System.Data.OleDb.OleDbParameter("Original_studentID", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "studentID", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("Original_address", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "address", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("Original_address1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "address", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("Original_birthDate", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "birthDate", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("Original_birthDate1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "birthDate", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("Original_contactNo", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "contactNo", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("Original_contactNo1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "contactNo", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("Original_firstName", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "firstName", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("Original_firstName1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "firstName", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("Original_lastName", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "lastName", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("Original_lastName1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "lastName", System.Data.DataRowVersion.Original, null)});
            // 
            // oleDbConnection1
            // 
            this.oleDbConnection1.ConnectionString = resources.GetString("oleDbConnection1.ConnectionString");
            // 
            // oleDbInsertCommand1
            // 
            this.oleDbInsertCommand1.CommandText = "INSERT INTO studentInfo(address, birthDate, contactNo, firstName, lastName, stude" +
    "ntID) VALUES (?, ?, ?, ?, ?, ?)";
            this.oleDbInsertCommand1.Connection = this.oleDbConnection1;
            this.oleDbInsertCommand1.Parameters.AddRange(new System.Data.OleDb.OleDbParameter[] {
            new System.Data.OleDb.OleDbParameter("address", System.Data.OleDb.OleDbType.VarWChar, 50, "address"),
            new System.Data.OleDb.OleDbParameter("birthDate", System.Data.OleDb.OleDbType.DBDate, 0, "birthDate"),
            new System.Data.OleDb.OleDbParameter("contactNo", System.Data.OleDb.OleDbType.VarWChar, 50, "contactNo"),
            new System.Data.OleDb.OleDbParameter("firstName", System.Data.OleDb.OleDbType.VarWChar, 50, "firstName"),
            new System.Data.OleDb.OleDbParameter("lastName", System.Data.OleDb.OleDbType.VarWChar, 50, "lastName"),
            new System.Data.OleDb.OleDbParameter("studentID", System.Data.OleDb.OleDbType.VarWChar, 50, "studentID")});
            // 
            // oleDbSelectCommand1
            // 
            this.oleDbSelectCommand1.CommandText = "SELECT address, birthDate, contactNo, firstName, lastName, studentID FROM student" +
    "Info";
            this.oleDbSelectCommand1.Connection = this.oleDbConnection1;
            // 
            // oleDbUpdateCommand1
            // 
            this.oleDbUpdateCommand1.CommandText = resources.GetString("oleDbUpdateCommand1.CommandText");
            this.oleDbUpdateCommand1.Connection = this.oleDbConnection1;
            this.oleDbUpdateCommand1.Parameters.AddRange(new System.Data.OleDb.OleDbParameter[] {
            new System.Data.OleDb.OleDbParameter("address", System.Data.OleDb.OleDbType.VarWChar, 50, "address"),
            new System.Data.OleDb.OleDbParameter("birthDate", System.Data.OleDb.OleDbType.DBDate, 0, "birthDate"),
            new System.Data.OleDb.OleDbParameter("contactNo", System.Data.OleDb.OleDbType.VarWChar, 50, "contactNo"),
            new System.Data.OleDb.OleDbParameter("firstName", System.Data.OleDb.OleDbType.VarWChar, 50, "firstName"),
            new System.Data.OleDb.OleDbParameter("lastName", System.Data.OleDb.OleDbType.VarWChar, 50, "lastName"),
            new System.Data.OleDb.OleDbParameter("studentID", System.Data.OleDb.OleDbType.VarWChar, 50, "studentID"),
            new System.Data.OleDb.OleDbParameter("Original_studentID", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "studentID", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("Original_address", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "address", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("Original_address1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "address", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("Original_birthDate", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "birthDate", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("Original_birthDate1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "birthDate", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("Original_contactNo", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "contactNo", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("Original_contactNo1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "contactNo", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("Original_firstName", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "firstName", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("Original_firstName1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "firstName", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("Original_lastName", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "lastName", System.Data.DataRowVersion.Original, null),
            new System.Data.OleDb.OleDbParameter("Original_lastName1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((byte)(0)), ((byte)(0)), "lastName", System.Data.DataRowVersion.Original, null)});
            // 
            // CreatingCrystalReportsDemo
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(512, 302);
            this.Controls.Add(this.tabControl1);
            this.Name = "CreatingCrystalReportsDemo";
            this.Text = "CreatingCrystalReportsDemo";
            this.Load += new System.EventHandler(this.CreatingCrystalReportsDemo_Load);
            this.tabControl1.ResumeLayout(false);
            this.DataGridView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myDataset1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new CreatingCrystalReportsDemo());
		}


		private void CreatingCrystalReportsDemo_Load(object sender, System.EventArgs e)
		{
			fillDataGrid();
			createReport();
		}

		/*
		 *	to populate data in the DataGrid
		 */
		private void fillDataGrid()
		{
			this.myDataset1.Clear();
			this.oleDbDataAdapter1.Fill(this.myDataset1,"myPersonalInfoTable");
			this.dataGrid1.DataSource=this.myDataset1.Tables["myPersonalInfoTable"].DefaultView;
		}


		/*
		 *	to create the report using Crystal Report
		 */
		private void createReport()
		{
			DBConnection DBConn = new DBConnection();
			OleDbDataAdapter myDataAdapter = DBConn.getDataFromDB();

			DataSet dataReport = new DataSet();
			myDataAdapter.Fill(dataReport,"myPersonalInfoTable");

			myDataReport myDataReport = new myDataReport();
			myDataReport.SetDataSource(dataReport);
			crystalReportViewer1.ReportSource = myDataReport;
		}
	}
}
