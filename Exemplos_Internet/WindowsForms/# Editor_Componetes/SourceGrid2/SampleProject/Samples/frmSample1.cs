using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Cells = SourceGrid2.Cells.Real;

namespace SampleProject
{
	/// <summary>
	/// Summary description for frmSampleGrid1.
	/// </summary>
	public class frmSample1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button brAddRow;
		private System.Windows.Forms.Button btRemoveRow;
		private System.Windows.Forms.Panel panel1;
		private SourceGrid2.Grid grid1;
		private System.Windows.Forms.Button btMoveColumn;
		private System.Windows.Forms.Button btMoveRow;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btExportHTML;
		private System.Windows.Forms.CheckBox chkReadOnly;
		private System.Windows.Forms.CheckBox chkEditOnDoubleClick;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton rdNromalBorder;
		private System.Windows.Forms.RadioButton rdLine;
		private System.Windows.Forms.RadioButton rdNone;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmSample1()
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
			this.brAddRow = new System.Windows.Forms.Button();
			this.btRemoveRow = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.grid1 = new SourceGrid2.Grid();
			this.btMoveColumn = new System.Windows.Forms.Button();
			this.btMoveRow = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.btExportHTML = new System.Windows.Forms.Button();
			this.chkReadOnly = new System.Windows.Forms.CheckBox();
			this.chkEditOnDoubleClick = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.rdNone = new System.Windows.Forms.RadioButton();
			this.rdLine = new System.Windows.Forms.RadioButton();
			this.rdNromalBorder = new System.Windows.Forms.RadioButton();
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// brAddRow
			// 
			this.brAddRow.Location = new System.Drawing.Point(8, 8);
			this.brAddRow.Name = "brAddRow";
			this.brAddRow.TabIndex = 1;
			this.brAddRow.Text = "AddRow";
			this.brAddRow.Click += new System.EventHandler(this.brAddRow_Click);
			// 
			// btRemoveRow
			// 
			this.btRemoveRow.Location = new System.Drawing.Point(96, 8);
			this.btRemoveRow.Name = "btRemoveRow";
			this.btRemoveRow.Size = new System.Drawing.Size(88, 23);
			this.btRemoveRow.TabIndex = 2;
			this.btRemoveRow.Text = "RemoveRow";
			this.btRemoveRow.Click += new System.EventHandler(this.btRemoveRow_Click);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.grid1);
			this.panel1.Location = new System.Drawing.Point(8, 40);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(748, 316);
			this.panel1.TabIndex = 3;
			// 
			// grid1
			// 
			this.grid1.AutoSizeMinHeight = 20;
			this.grid1.AutoSizeMinWidth = 20;
			this.grid1.AutoStretchColumnsToFitWidth = false;
			this.grid1.AutoStretchRowsToFitHeight = false;
			this.grid1.BackColor = System.Drawing.Color.White;
			this.grid1.ContextMenuStyle = ((SourceGrid2.ContextMenuStyle)(((((SourceGrid2.ContextMenuStyle.ColumnResize | SourceGrid2.ContextMenuStyle.RowResize) 
				| SourceGrid2.ContextMenuStyle.AutoSize) 
				| SourceGrid2.ContextMenuStyle.ClearSelection) 
				| SourceGrid2.ContextMenuStyle.CopyPasteSelection)));
			this.grid1.CustomSort = false;
			this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grid1.GridToolTipActive = true;
			this.grid1.Location = new System.Drawing.Point(0, 0);
			this.grid1.Name = "grid1";
			this.grid1.Size = new System.Drawing.Size(746, 314);
			this.grid1.SpecialKeys = SourceGrid2.GridSpecialKeys.Default;
			this.grid1.TabIndex = 1;
			// 
			// btMoveColumn
			// 
			this.btMoveColumn.Location = new System.Drawing.Point(320, 8);
			this.btMoveColumn.Name = "btMoveColumn";
			this.btMoveColumn.Size = new System.Drawing.Size(88, 23);
			this.btMoveColumn.TabIndex = 4;
			this.btMoveColumn.Text = "Move Column";
			this.btMoveColumn.Click += new System.EventHandler(this.btMoveColumn_Click);
			// 
			// btMoveRow
			// 
			this.btMoveRow.Location = new System.Drawing.Point(224, 8);
			this.btMoveRow.Name = "btMoveRow";
			this.btMoveRow.Size = new System.Drawing.Size(88, 23);
			this.btMoveRow.TabIndex = 5;
			this.btMoveRow.Text = "Move Row";
			this.btMoveRow.Click += new System.EventHandler(this.btMoveRow_Click);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.Location = new System.Drawing.Point(8, 368);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(192, 23);
			this.label1.TabIndex = 6;
			this.label1.Text = "Click column header to sort the grid";
			// 
			// btExportHTML
			// 
			this.btExportHTML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btExportHTML.Location = new System.Drawing.Point(668, 368);
			this.btExportHTML.Name = "btExportHTML";
			this.btExportHTML.Size = new System.Drawing.Size(84, 23);
			this.btExportHTML.TabIndex = 7;
			this.btExportHTML.Text = "Export HTML";
			this.btExportHTML.Click += new System.EventHandler(this.btExportHTML_Click);
			// 
			// chkReadOnly
			// 
			this.chkReadOnly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.chkReadOnly.Location = new System.Drawing.Point(536, 360);
			this.chkReadOnly.Name = "chkReadOnly";
			this.chkReadOnly.Size = new System.Drawing.Size(128, 20);
			this.chkReadOnly.TabIndex = 8;
			this.chkReadOnly.Text = "Read Only Cells";
			this.chkReadOnly.CheckedChanged += new System.EventHandler(this.chkReadOnly_CheckedChanged);
			// 
			// chkEditOnDoubleClick
			// 
			this.chkEditOnDoubleClick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.chkEditOnDoubleClick.Location = new System.Drawing.Point(536, 376);
			this.chkEditOnDoubleClick.Name = "chkEditOnDoubleClick";
			this.chkEditOnDoubleClick.Size = new System.Drawing.Size(128, 20);
			this.chkEditOnDoubleClick.TabIndex = 9;
			this.chkEditOnDoubleClick.Text = "Edit On Double Click";
			this.chkEditOnDoubleClick.CheckedChanged += new System.EventHandler(this.chkEditOnDoubleClick_CheckedChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rdNone);
			this.groupBox1.Controls.Add(this.rdLine);
			this.groupBox1.Controls.Add(this.rdNromalBorder);
			this.groupBox1.Location = new System.Drawing.Point(424, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(276, 36);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Border Style";
			// 
			// rdNone
			// 
			this.rdNone.Location = new System.Drawing.Point(188, 16);
			this.rdNone.Name = "rdNone";
			this.rdNone.Size = new System.Drawing.Size(68, 16);
			this.rdNone.TabIndex = 2;
			this.rdNone.Text = "None";
			this.rdNone.CheckedChanged += new System.EventHandler(this.rdNone_CheckedChanged);
			// 
			// rdLine
			// 
			this.rdLine.Location = new System.Drawing.Point(96, 16);
			this.rdLine.Name = "rdLine";
			this.rdLine.Size = new System.Drawing.Size(68, 16);
			this.rdLine.TabIndex = 1;
			this.rdLine.Text = "Line";
			this.rdLine.CheckedChanged += new System.EventHandler(this.rdLine_CheckedChanged);
			// 
			// rdNromalBorder
			// 
			this.rdNromalBorder.Checked = true;
			this.rdNromalBorder.Location = new System.Drawing.Point(12, 16);
			this.rdNromalBorder.Name = "rdNromalBorder";
			this.rdNromalBorder.Size = new System.Drawing.Size(80, 16);
			this.rdNromalBorder.TabIndex = 0;
			this.rdNromalBorder.TabStop = true;
			this.rdNromalBorder.Text = "Normal";
			this.rdNromalBorder.CheckedChanged += new System.EventHandler(this.rdNromalBorder_CheckedChanged);
			// 
			// frmSample1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(764, 395);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.chkEditOnDoubleClick);
			this.Controls.Add(this.chkReadOnly);
			this.Controls.Add(this.btExportHTML);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btMoveRow);
			this.Controls.Add(this.btMoveColumn);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.btRemoveRow);
			this.Controls.Add(this.brAddRow);
			this.Name = "frmSample1";
			this.Text = "Sample Grid 1";
			this.Load += new System.EventHandler(this.frmSampleGrid1_Load);
			this.panel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private SourceGrid2.DataModels.IDataModel m_CellEditor_Id;
		private SourceGrid2.DataModels.IDataModel m_CellEditor_Name;
		private SourceGrid2.DataModels.IDataModel m_CellEditor_Address;
		private SourceGrid2.DataModels.IDataModel m_CellEditor_City;
		private SourceGrid2.DataModels.IDataModel m_CellEditor_BirthDay;
		private SourceGrid2.DataModels.IDataModel m_CellEditor_Country;
		private SourceGrid2.DataModels.EditorTextBox m_CellEditor_Price;
		private void frmSampleGrid1_Load(object sender, System.EventArgs e)
		{
			string[] l_CountryList = new string[]{"Italy","France","Spain","UK","Argentina","Mexico", "Switzerland", "Brazil", "Germany","Portugal","Sweden","Austria"};

			grid1.RowsCount = 1;
			grid1.ColumnsCount = 10;
			grid1.FixedRows = 1;
			grid1.FixedColumns = 1;
			grid1.Selection.SelectionMode = SourceGrid2.GridSelectionMode.Row;
			grid1.AutoStretchColumnsToFitWidth = true;
			grid1.Columns[0].AutoSizeMode = SourceGrid2.AutoSizeMode.None;
			grid1.Columns[0].Width = 30;

			#region Create Header Row and Editor
			Cells.Header l_00Header = new Cells.Header();
			grid1[0,0] = l_00Header;

			m_CellEditor_Id = SourceGrid2.Utility.CreateDataModel(typeof(int));
			m_CellEditor_Id.EditableMode = SourceGrid2.EditableMode.Focus|SourceGrid2.EditableMode.AnyKey|SourceGrid2.EditableMode.SingleClick;
			grid1[0,1] = new Cells.ColumnHeader("ID (int)");

			m_CellEditor_Name = SourceGrid2.Utility.CreateDataModel(typeof(string));
			m_CellEditor_Name.EditableMode = SourceGrid2.EditableMode.Focus|SourceGrid2.EditableMode.AnyKey|SourceGrid2.EditableMode.SingleClick;
			grid1[0,2] = new Cells.ColumnHeader("NAME (string)");

			m_CellEditor_Address = SourceGrid2.Utility.CreateDataModel(typeof(string));
			m_CellEditor_Address.EditableMode = SourceGrid2.EditableMode.Focus|SourceGrid2.EditableMode.AnyKey|SourceGrid2.EditableMode.SingleClick;
			grid1[0,3] = new Cells.ColumnHeader("ADDRESS (string)");

			m_CellEditor_City = SourceGrid2.Utility.CreateDataModel(typeof(string));
			m_CellEditor_City.EditableMode = SourceGrid2.EditableMode.Focus|SourceGrid2.EditableMode.AnyKey|SourceGrid2.EditableMode.SingleClick;
			grid1[0,4] = new Cells.ColumnHeader("CITY (string)");

			m_CellEditor_BirthDay = SourceGrid2.Utility.CreateDataModel(typeof(DateTime));
			m_CellEditor_BirthDay.EditableMode = SourceGrid2.EditableMode.Focus|SourceGrid2.EditableMode.AnyKey|SourceGrid2.EditableMode.SingleClick;
			grid1[0,5] = new Cells.ColumnHeader("BIRTHDATE (DateTime)");

			m_CellEditor_Country = new SourceGrid2.DataModels.EditorComboBox(typeof(string),l_CountryList,false);
			m_CellEditor_Country.EditableMode = SourceGrid2.EditableMode.Focus|SourceGrid2.EditableMode.AnyKey|SourceGrid2.EditableMode.SingleClick;
			grid1[0,6] = new Cells.ColumnHeader("COUNTRY (string + combobox)");

			m_CellEditor_Price = new SourceGrid2.DataModels.EditorTextBox(typeof(double));
			m_CellEditor_Price.TypeConverter = new SourceLibrary.ComponentModel.Converter.CurrencyTypeConverter(typeof(double));
			m_CellEditor_Price.EditableMode = SourceGrid2.EditableMode.Focus|SourceGrid2.EditableMode.AnyKey|SourceGrid2.EditableMode.SingleClick;
			grid1[0,7] = new Cells.ColumnHeader("$ PRICE (double)");

			grid1[0,8] = new Cells.ColumnHeader("Selected");

			grid1[0,9] = new Cells.ColumnHeader("WebSite");
			#endregion


			#region Visual Properties
			//set Cells style
			m_VisualProperties = new SourceGrid2.VisualModels.Common(false);

			m_VisualPropertiesPrice = (SourceGrid2.VisualModels.Common)m_VisualProperties.Clone(false);
			m_VisualPropertiesPrice.TextAlignment = ContentAlignment.MiddleRight;

			m_VisualPropertiesCheckBox = (SourceGrid2.VisualModels.CheckBox)SourceGrid2.VisualModels.CheckBox.Default.Clone(false);

			m_VisualPropertiesLink = (SourceGrid2.VisualModels.Common)SourceGrid2.VisualModels.Common.LinkStyle.Clone(false);
			#endregion

			//read xml
			System.IO.StreamReader l_Reader = new System.IO.StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("SampleProject.Samples.SampleData.xml"));
			System.Xml.XmlDocument l_XmlDoc = new System.Xml.XmlDocument();
			l_XmlDoc.LoadXml(l_Reader.ReadToEnd());
			l_Reader.Close();
			System.Xml.XmlNodeList l_Rows = l_XmlDoc.SelectNodes("//row");
			grid1.RowsCount = l_Rows.Count+1;
			int l_RowsCount = 1;
			foreach(System.Xml.XmlNode l_Node in l_Rows)
			{
				#region Pupulate RowsCount
				grid1[l_RowsCount,0] = new Cells.RowHeader();

				grid1[l_RowsCount,1] = new Cells.Cell(l_RowsCount);
				grid1[l_RowsCount,1].DataModel = m_CellEditor_Id;
				grid1[l_RowsCount,1].VisualModel = m_VisualProperties;

				grid1[l_RowsCount,2] = new Cells.Cell(l_Node.Attributes["ContactName"].InnerText);
				grid1[l_RowsCount,2].DataModel = m_CellEditor_Name;
				grid1[l_RowsCount,2].VisualModel = m_VisualProperties;

				grid1[l_RowsCount,3] = new Cells.Cell(l_Node.Attributes["Address"].InnerText);
				grid1[l_RowsCount,3].DataModel = m_CellEditor_Address;
				grid1[l_RowsCount,3].VisualModel = m_VisualProperties;

				grid1[l_RowsCount,4] = new Cells.Cell(l_Node.Attributes["City"].InnerText);
				grid1[l_RowsCount,4].DataModel = m_CellEditor_City;
				grid1[l_RowsCount,4].VisualModel = m_VisualProperties;

				grid1[l_RowsCount,5] = new Cells.Cell(DateTime.Today);
				grid1[l_RowsCount,5].DataModel = m_CellEditor_BirthDay;
				grid1[l_RowsCount,5].VisualModel = m_VisualProperties;

				grid1[l_RowsCount,6] = new Cells.Cell(l_Node.Attributes["Country"].InnerText);
				grid1[l_RowsCount,6].DataModel = m_CellEditor_Country;
				grid1[l_RowsCount,6].VisualModel = m_VisualProperties;

				grid1[l_RowsCount,7] = new Cells.Cell(25.0);
				grid1[l_RowsCount,7].DataModel = m_CellEditor_Price;
				grid1[l_RowsCount,7].VisualModel = m_VisualPropertiesPrice;

				grid1[l_RowsCount,8] = new Cells.CheckBox(false);
				grid1[l_RowsCount,8].VisualModel = m_VisualPropertiesCheckBox;

				grid1[l_RowsCount,9] = new Cells.Link(l_Node.Attributes["website"].InnerText);
				grid1[l_RowsCount,9].VisualModel = m_VisualPropertiesLink;
				((Cells.Link)grid1[l_RowsCount,9]).Click += new SourceGrid2.PositionEventHandler(CellLink_Click);
				#endregion

				l_RowsCount++;
			}

			grid1.AutoSizeAll();
		}

		private SourceGrid2.VisualModels.Common m_VisualProperties;
		private SourceGrid2.VisualModels.Common m_VisualPropertiesPrice;
		private SourceGrid2.VisualModels.Common m_VisualPropertiesCheckBox;
		private SourceGrid2.VisualModels.Common m_VisualPropertiesLink;

		private void brAddRow_Click(object sender, System.EventArgs e)
		{
			Cells.CheckBox l_CheckBox = new Cells.CheckBox(false);
			l_CheckBox.VisualModel = m_VisualPropertiesCheckBox;

			Cells.Link l_Link = new Cells.Link("http://www.codeproject.com");
			l_Link.VisualModel = m_VisualPropertiesLink;
			l_Link.Click += new SourceGrid2.PositionEventHandler(CellLink_Click);

			grid1.Rows.Insert(grid1.RowsCount,
				new Cells.RowHeader(),
				new Cells.Cell(grid1.RowsCount,m_CellEditor_Id,m_VisualProperties),
				new Cells.Cell(m_CellEditor_Name.DefaultValue,m_CellEditor_Name,m_VisualProperties),
				new Cells.Cell(m_CellEditor_Address.DefaultValue,m_CellEditor_Address,m_VisualProperties),
				new Cells.Cell(m_CellEditor_City.DefaultValue,m_CellEditor_City,m_VisualProperties),
				new Cells.Cell(m_CellEditor_BirthDay.DefaultValue, m_CellEditor_BirthDay,m_VisualProperties),
				new Cells.Cell(m_CellEditor_Country.DefaultValue, m_CellEditor_Country,m_VisualProperties),
				new Cells.Cell(m_CellEditor_Price.DefaultValue, m_CellEditor_Price,m_VisualPropertiesPrice),
				l_CheckBox,
				l_Link);

			grid1.Rows[grid1.RowsCount-1].Focus();
		}

		private void btRemoveRow_Click(object sender, System.EventArgs e)
		{
			SourceGrid2.RowInfo[] l_Rows = grid1.Selection.SelectedRows;
			foreach (SourceGrid2.RowInfo r in l_Rows)
				grid1.Rows.Remove(r.Index);

			if (grid1.RowsCount > 1)
				grid1.Rows[1].Focus();
		}

		private void btMoveRow_Click(object sender, System.EventArgs e)
		{
			if (grid1.RowsCount>1)
				grid1.Rows.Move(grid1.RowsCount-1,1);
		}

		private void btMoveColumn_Click(object sender, System.EventArgs e)
		{
			brAddRow.Enabled = false; //disable the add button to prevent adding new row when column are out of position
			if (grid1.ColumnsCount>1)
				grid1.Columns.Move(grid1.ColumnsCount-1,1);
		}

		private void btExportHTML_Click(object sender, System.EventArgs e)
		{
			try
			{
				string l_Path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "tmpSourceGridExport.htm");

				using (System.IO.FileStream l_Stream = new System.IO.FileStream(l_Path,System.IO.FileMode.Create,System.IO.FileAccess.Write))
				{
					grid1.ExportHTML(new SourceGrid2.HTMLExport(SourceGrid2.ExportHTMLMode.Default, System.IO.Path.GetTempPath(), "", l_Stream));
					l_Stream.Close();
				}

				SourceLibrary.Utility.Shell.OpenFile(l_Path);
			}
			catch(Exception err)
			{
				SourceLibrary.Windows.Forms.ErrorDialog.Show(this,err,"HTML Export Error");
			}
		}

		private void chkReadOnly_CheckedChanged(object sender, System.EventArgs e)
		{
			for (int r = 0; r < grid1.RowsCount; r++)
				for (int c = 0; c < grid1.ColumnsCount; c++)
				{
					if (grid1[r,c].DataModel != null)
						grid1[r,c].DataModel.EnableEdit = !chkReadOnly.Checked;
				}
		}

		private void CellLink_Click(object sender, SourceGrid2.PositionEventArgs e)
		{
			try
			{
				SourceLibrary.Utility.Shell.OpenFile( ((Cells.Link)sender).Value.ToString());
			}
			catch(Exception)
			{
			}
		}

		private void chkEditOnDoubleClick_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chkEditOnDoubleClick.Checked)
			{
				m_CellEditor_Id.EditableMode = SourceGrid2.EditableMode.AnyKey | SourceGrid2.EditableMode.DoubleClick | SourceGrid2.EditableMode.F2Key;
				m_CellEditor_Name.EditableMode = SourceGrid2.EditableMode.AnyKey | SourceGrid2.EditableMode.DoubleClick | SourceGrid2.EditableMode.F2Key;
				m_CellEditor_Address.EditableMode = SourceGrid2.EditableMode.AnyKey | SourceGrid2.EditableMode.DoubleClick | SourceGrid2.EditableMode.F2Key;
				m_CellEditor_City.EditableMode = SourceGrid2.EditableMode.AnyKey | SourceGrid2.EditableMode.DoubleClick | SourceGrid2.EditableMode.F2Key;
				m_CellEditor_BirthDay.EditableMode = SourceGrid2.EditableMode.AnyKey | SourceGrid2.EditableMode.DoubleClick | SourceGrid2.EditableMode.F2Key;
				m_CellEditor_Country.EditableMode = SourceGrid2.EditableMode.AnyKey | SourceGrid2.EditableMode.DoubleClick | SourceGrid2.EditableMode.F2Key;
				m_CellEditor_Price.EditableMode = SourceGrid2.EditableMode.AnyKey | SourceGrid2.EditableMode.DoubleClick | SourceGrid2.EditableMode.F2Key;
			}
			else
			{
				m_CellEditor_Id.EditableMode = SourceGrid2.EditableMode.Focus|SourceGrid2.EditableMode.AnyKey|SourceGrid2.EditableMode.SingleClick;
				m_CellEditor_Name.EditableMode = SourceGrid2.EditableMode.Focus|SourceGrid2.EditableMode.AnyKey|SourceGrid2.EditableMode.SingleClick;
				m_CellEditor_Address.EditableMode = SourceGrid2.EditableMode.Focus|SourceGrid2.EditableMode.AnyKey|SourceGrid2.EditableMode.SingleClick;
				m_CellEditor_City.EditableMode = SourceGrid2.EditableMode.Focus|SourceGrid2.EditableMode.AnyKey|SourceGrid2.EditableMode.SingleClick;
				m_CellEditor_BirthDay.EditableMode = SourceGrid2.EditableMode.Focus|SourceGrid2.EditableMode.AnyKey|SourceGrid2.EditableMode.SingleClick;
				m_CellEditor_Country.EditableMode = SourceGrid2.EditableMode.Focus|SourceGrid2.EditableMode.AnyKey|SourceGrid2.EditableMode.SingleClick;
				m_CellEditor_Price.EditableMode = SourceGrid2.EditableMode.Focus|SourceGrid2.EditableMode.AnyKey|SourceGrid2.EditableMode.SingleClick;
			}
		}

		private void rdNromalBorder_CheckedChanged(object sender, System.EventArgs e)
		{
			SetBorder();
		}

		private void rdLine_CheckedChanged(object sender, System.EventArgs e)
		{
			SetBorder();
		}
		private void rdNone_CheckedChanged(object sender, System.EventArgs e)
		{
			SetBorder();
		}
		private void SetBorder()
		{
			if (rdLine.Checked)
			{
				m_VisualProperties.Border = new SourceGrid2.RectangleBorder(new SourceGrid2.Border(Color.Black,0), new SourceGrid2.Border(Color.Black,1));
				m_VisualPropertiesCheckBox.Border = m_VisualProperties.Border;
				m_VisualPropertiesPrice.Border = m_VisualProperties.Border;
				m_VisualPropertiesLink.Border = m_VisualProperties.Border;
			}
			else if (rdNromalBorder.Checked)
			{
				m_VisualProperties.Border = SourceGrid2.RectangleBorder.Default;
				m_VisualPropertiesCheckBox.Border = SourceGrid2.RectangleBorder.Default;
				m_VisualPropertiesPrice.Border = SourceGrid2.RectangleBorder.Default;
				m_VisualPropertiesLink.Border = SourceGrid2.RectangleBorder.Default;
			}
			else
			{
				m_VisualProperties.Border = SourceGrid2.RectangleBorder.NoBorder;
				m_VisualPropertiesCheckBox.Border = SourceGrid2.RectangleBorder.NoBorder;
				m_VisualPropertiesPrice.Border = SourceGrid2.RectangleBorder.NoBorder;
				m_VisualPropertiesLink.Border = SourceGrid2.RectangleBorder.NoBorder;
			}
			grid1.InvalidateCells();
		}
	}
}
