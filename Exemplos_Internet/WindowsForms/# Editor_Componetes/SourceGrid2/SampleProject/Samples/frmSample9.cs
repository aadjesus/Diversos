using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SampleProject
{
	/// <summary>
	/// Summary description for frmSample9.
	/// </summary>
	public class frmSample9 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox txtConnectionString;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtSqlQuery;
		private System.Windows.Forms.Button btLoad;
		private System.Windows.Forms.CheckBox chkEdit;
		private System.Windows.Forms.CheckBox chkDelete;
		private System.Windows.Forms.Button btAdd;
		private System.Windows.Forms.Button btDelete;
		private System.Windows.Forms.Label lbRowsNumber;
		private SampleProject.Extensions.GridDataTable gridDataTable1;
		private System.Windows.Forms.Button cmdSelectColumns;
		private System.Windows.Forms.Label label3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmSample9()
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
			this.txtConnectionString = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtSqlQuery = new System.Windows.Forms.TextBox();
			this.btLoad = new System.Windows.Forms.Button();
			this.chkEdit = new System.Windows.Forms.CheckBox();
			this.chkDelete = new System.Windows.Forms.CheckBox();
			this.btAdd = new System.Windows.Forms.Button();
			this.btDelete = new System.Windows.Forms.Button();
			this.lbRowsNumber = new System.Windows.Forms.Label();
			this.gridDataTable1 = new SampleProject.Extensions.GridDataTable();
			this.cmdSelectColumns = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtConnectionString
			// 
			this.txtConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtConnectionString.Location = new System.Drawing.Point(4, 20);
			this.txtConnectionString.Name = "txtConnectionString";
			this.txtConnectionString.Size = new System.Drawing.Size(564, 20);
			this.txtConnectionString.TabIndex = 1;
			this.txtConnectionString.Text = @"Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Northwind;Data Source=LOCALHOST;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;Workstation ID=LOCALHOST;Use Encryption for Data=False;Tag with column collation when possible=False";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(4, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(380, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Connection String OLEDB (Default: SQLServer LOCALHOST.Northwind)";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(4, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(380, 16);
			this.label2.TabIndex = 4;
			this.label2.Text = "SQL Query";
			// 
			// txtSqlQuery
			// 
			this.txtSqlQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSqlQuery.Location = new System.Drawing.Point(4, 60);
			this.txtSqlQuery.Multiline = true;
			this.txtSqlQuery.Name = "txtSqlQuery";
			this.txtSqlQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtSqlQuery.Size = new System.Drawing.Size(368, 56);
			this.txtSqlQuery.TabIndex = 3;
			this.txtSqlQuery.Text = "select * from [Orders Qry]";
			// 
			// btLoad
			// 
			this.btLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btLoad.Location = new System.Drawing.Point(496, 92);
			this.btLoad.Name = "btLoad";
			this.btLoad.TabIndex = 5;
			this.btLoad.Text = "Load";
			this.btLoad.Click += new System.EventHandler(this.btLoad_Click);
			// 
			// chkEdit
			// 
			this.chkEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkEdit.Checked = true;
			this.chkEdit.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkEdit.Location = new System.Drawing.Point(380, 84);
			this.chkEdit.Name = "chkEdit";
			this.chkEdit.Size = new System.Drawing.Size(112, 16);
			this.chkEdit.TabIndex = 7;
			this.chkEdit.Text = "Enable Edit";
			this.chkEdit.CheckedChanged += new System.EventHandler(this.chkEdit_CheckedChanged);
			// 
			// chkDelete
			// 
			this.chkDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkDelete.Checked = true;
			this.chkDelete.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkDelete.Location = new System.Drawing.Point(380, 100);
			this.chkDelete.Name = "chkDelete";
			this.chkDelete.Size = new System.Drawing.Size(112, 16);
			this.chkDelete.TabIndex = 8;
			this.chkDelete.Text = "Enable Delete";
			this.chkDelete.CheckedChanged += new System.EventHandler(this.chkDelete_CheckedChanged);
			// 
			// btAdd
			// 
			this.btAdd.Enabled = false;
			this.btAdd.Location = new System.Drawing.Point(4, 120);
			this.btAdd.Name = "btAdd";
			this.btAdd.TabIndex = 9;
			this.btAdd.Text = "Add";
			this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
			// 
			// btDelete
			// 
			this.btDelete.Enabled = false;
			this.btDelete.Location = new System.Drawing.Point(84, 120);
			this.btDelete.Name = "btDelete";
			this.btDelete.TabIndex = 10;
			this.btDelete.Text = "Delete";
			this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
			// 
			// lbRowsNumber
			// 
			this.lbRowsNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lbRowsNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbRowsNumber.Location = new System.Drawing.Point(268, 124);
			this.lbRowsNumber.Name = "lbRowsNumber";
			this.lbRowsNumber.Size = new System.Drawing.Size(168, 16);
			this.lbRowsNumber.TabIndex = 11;
			this.lbRowsNumber.Text = "Rows Number: YYYY";
			// 
			// gridDataTable1
			// 
			this.gridDataTable1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.gridDataTable1.AutoSizeMinHeight = 10;
			this.gridDataTable1.AutoSizeMinWidth = 10;
			this.gridDataTable1.AutoStretchColumnsToFitWidth = false;
			this.gridDataTable1.AutoStretchRowsToFitHeight = false;
			this.gridDataTable1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.gridDataTable1.ContextMenuStyle = SourceGrid2.ContextMenuStyle.CopyPasteSelection;
			this.gridDataTable1.EnableDelete = true;
			this.gridDataTable1.EnableEdit = true;
			this.gridDataTable1.GridToolTipActive = true;
			this.gridDataTable1.Location = new System.Drawing.Point(8, 172);
			this.gridDataTable1.Name = "gridDataTable1";
			this.gridDataTable1.Size = new System.Drawing.Size(564, 284);
			this.gridDataTable1.SpecialKeys = SourceGrid2.GridSpecialKeys.Default;
			this.gridDataTable1.TabIndex = 12;
			// 
			// cmdSelectColumns
			// 
			this.cmdSelectColumns.Enabled = false;
			this.cmdSelectColumns.Location = new System.Drawing.Point(184, 120);
			this.cmdSelectColumns.Name = "cmdSelectColumns";
			this.cmdSelectColumns.TabIndex = 13;
			this.cmdSelectColumns.Text = "Columns...";
			this.cmdSelectColumns.Click += new System.EventHandler(this.cmdSelectColumns_Click);
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(8, 148);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(564, 20);
			this.label3.TabIndex = 14;
			this.label3.Text = "Changing this grid affect only the DataTable, the original database is never upda" +
				"ted.";
			// 
			// frmSample9
			// 
			this.AcceptButton = this.btLoad;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(580, 463);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cmdSelectColumns);
			this.Controls.Add(this.gridDataTable1);
			this.Controls.Add(this.lbRowsNumber);
			this.Controls.Add(this.btDelete);
			this.Controls.Add(this.btAdd);
			this.Controls.Add(this.chkDelete);
			this.Controls.Add(this.chkEdit);
			this.Controls.Add(this.btLoad);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtSqlQuery);
			this.Controls.Add(this.txtConnectionString);
			this.Controls.Add(this.label1);
			this.Name = "frmSample9";
			this.Text = "Virtual DataSet Load";
			this.Load += new System.EventHandler(this.frmSample9_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void btLoad_Click(object sender, System.EventArgs e)
		{
			try
			{
				cmdSelectColumns.Enabled = false;
				btAdd.Enabled = false;
				btDelete.Enabled = false;

				System.Data.OleDb.OleDbDataAdapter l_Adapter = new System.Data.OleDb.OleDbDataAdapter(txtSqlQuery.Text, txtConnectionString.Text);
				System.Data.DataSet l_DataSet = new System.Data.DataSet();
				l_Adapter.Fill(l_DataSet);

				LoadDataTable(l_DataSet.Tables[0]);

				cmdSelectColumns.Enabled = true;
				btAdd.Enabled = true;
				btDelete.Enabled = true;
			}
			catch(Exception err)
			{
				SourceLibrary.Windows.Forms.ErrorDialog.Show(this, err, "Error loading dataset");
			}
		}

		private Extensions.GridDataTable.CellColumnTemplate[] m_TemplateColumns;
		private void LoadDataTable(System.Data.DataTable p_DataTable)
		{
			lbRowsNumber.Text = "Rows: " + p_DataTable.Rows.Count;
			m_TemplateColumns = Extensions.GridDataTable.GetColumnsFromDataTable(p_DataTable, chkEdit.Checked);
			gridDataTable1.LoadDataSource(p_DataTable, Extensions.GridDataTable.GridDataTableStyle.Default, m_TemplateColumns);
			gridDataTable1.AutoSizeView(true,gridDataTable1.AutoSizeMinHeight, 40);
		}

		private void chkEdit_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chkEdit.Checked)
				gridDataTable1.EnableEdit = true;
			else
				gridDataTable1.EnableEdit = false;
		}

		private void chkDelete_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chkDelete.Checked)
				gridDataTable1.EnableDelete = true;
			else
				gridDataTable1.EnableDelete = false;
		}

		private void cmdSelectColumns_Click(object sender, System.EventArgs e)
		{
			using (Extensions.DataColumnDialog l_dg = new SampleProject.Extensions.DataColumnDialog())
			{
				l_dg.TemplateColumns = m_TemplateColumns;
				l_dg.DataTable = gridDataTable1.DataTable;
				if (l_dg.ShowDialog(this) == DialogResult.OK)
				{
					m_TemplateColumns = l_dg.TemplateColumns;
					gridDataTable1.LoadDataSource(gridDataTable1.DataTable, gridDataTable1.GridStyle, m_TemplateColumns);
				}
			}
		}

		private void btDelete_Click(object sender, System.EventArgs e)
		{
			gridDataTable1.DeleteFocusDataRow();
		}

		private void btAdd_Click(object sender, System.EventArgs e)
		{
			gridDataTable1.AddDataRow();
		}

		private void frmSample9_Load(object sender, System.EventArgs e)
		{
			txtSqlQuery.Text = "select * from [Orders Qry] \r\n /*select TOP 50000 * from [Orders Qry] a CROSS JOIN [Orders Qry]  b*/";
		}
	}
}