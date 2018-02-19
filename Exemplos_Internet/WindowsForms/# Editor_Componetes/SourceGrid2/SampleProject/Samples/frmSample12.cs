using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Cells = SourceGrid2.Cells.Virtual;

namespace SampleProject
{
	/// <summary>
	/// Summary description for frmSample12.
	/// </summary>
	public class frmSample12 : System.Windows.Forms.Form
	{
		private SourceGrid2.GridVirtual gridVirtual1;
		private System.Windows.Forms.Button btSave;
		private System.Windows.Forms.Button btLoad;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmSample12()
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
			this.btSave = new System.Windows.Forms.Button();
			this.btLoad = new System.Windows.Forms.Button();
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
			this.gridVirtual1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.gridVirtual1.GridToolTipActive = true;
			this.gridVirtual1.Location = new System.Drawing.Point(8, 32);
			this.gridVirtual1.Name = "gridVirtual1";
			this.gridVirtual1.Size = new System.Drawing.Size(616, 352);
			this.gridVirtual1.SpecialKeys = SourceGrid2.GridSpecialKeys.Default;
			this.gridVirtual1.TabIndex = 0;
			this.gridVirtual1.GettingCell += new SourceGrid2.PositionEventHandler(this.gridVirtual1_GettingCell);
			// 
			// btSave
			// 
			this.btSave.Location = new System.Drawing.Point(8, 4);
			this.btSave.Name = "btSave";
			this.btSave.Size = new System.Drawing.Size(96, 23);
			this.btSave.TabIndex = 1;
			this.btSave.Text = "Save To File";
			this.btSave.Click += new System.EventHandler(this.btSave_Click);
			// 
			// btLoad
			// 
			this.btLoad.Location = new System.Drawing.Point(112, 4);
			this.btLoad.Name = "btLoad";
			this.btLoad.Size = new System.Drawing.Size(96, 23);
			this.btLoad.TabIndex = 2;
			this.btLoad.Text = "Load From File";
			this.btLoad.Click += new System.EventHandler(this.btLoad_Click);
			// 
			// frmSample12
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(632, 390);
			this.Controls.Add(this.btLoad);
			this.Controls.Add(this.btSave);
			this.Controls.Add(this.gridVirtual1);
			this.Name = "frmSample12";
			this.Text = "Xml Binding";
			this.Load += new System.EventHandler(this.frmSample12_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private CellColumnTemplate[] m_ColumnsHeader;
		private Cells.CellVirtual[] m_ColumnsData;

		private void frmSample12_Load(object sender, System.EventArgs e)
		{
			//read xml
			System.IO.StreamReader l_Reader = new System.IO.StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("SampleProject.Samples.SampleData.xml"));
			System.Xml.XmlDocument l_XmlDoc = new System.Xml.XmlDocument();
			l_XmlDoc.LoadXml(l_Reader.ReadToEnd());
			l_Reader.Close();

			LoadXmlDocument(l_XmlDoc);
		}

		private System.Xml.XmlDocument m_XmlDocument;
		private void LoadXmlDocument(System.Xml.XmlDocument p_Document)
		{
			m_XmlDocument = p_Document;
			System.Xml.XmlNodeList l_Rows = m_XmlDocument.SelectNodes("//row");

			gridVirtual1.FixedRows = 1;
			gridVirtual1.FixedColumns = 0;
			gridVirtual1.Selection.SelectionMode = SourceGrid2.GridSelectionMode.Row;

			m_ColumnsHeader = new CellColumnTemplate[14];
			m_ColumnsData = new Cells.CellVirtual[14];

			gridVirtual1.Redim(l_Rows.Count+gridVirtual1.FixedRows, m_ColumnsData.Length + gridVirtual1.FixedColumns);

			//Load Columns

			m_ColumnsHeader[0] = new CellColumnTemplate("ID (int)");
			m_ColumnsData[0] = new CellDataTemplate("ID", l_Rows, typeof(int));
			m_ColumnsData[0].TextAlignment = ContentAlignment.MiddleRight;

			m_ColumnsHeader[1] = new CellColumnTemplate("CustomerId (string)");
			m_ColumnsData[1] = new CellDataTemplate("CustomerId", l_Rows, typeof(string));

			m_ColumnsHeader[2] = new CellColumnTemplate("CompanyName (string)");
			m_ColumnsData[2] = new CellDataTemplate("CompanyName", l_Rows, typeof(string));

			m_ColumnsHeader[3] = new CellColumnTemplate("ContactName (string)");
			m_ColumnsData[3] = new CellDataTemplate("ContactName", l_Rows, typeof(string));

			m_ColumnsHeader[4] = new CellColumnTemplate("ContactTitle (string)");
			m_ColumnsData[4] = new CellDataTemplate("ContactTitle", l_Rows, typeof(string));

			m_ColumnsHeader[5] = new CellColumnTemplate("Address (string)");
			m_ColumnsData[5] = new CellDataTemplate("Address", l_Rows, typeof(string));

			m_ColumnsHeader[6] = new CellColumnTemplate("City (string)");
			m_ColumnsData[6] = new CellDataTemplate("City", l_Rows, typeof(string));

			m_ColumnsHeader[7] = new CellColumnTemplate("PostalCode (string)");
			m_ColumnsData[7] = new CellDataTemplate("PostalCode", l_Rows, typeof(string));

			m_ColumnsHeader[8] = new CellColumnTemplate("Country (string)");
			m_ColumnsData[8] = new CellDataTemplate("Country", l_Rows, typeof(string));
			string[] l_CountryList = new string[]{"Italy","France","Spain","UK","Argentina","Mexico", "Switzerland", "Brazil", "Germany","Portugal","Sweden","Austria"};
			SourceGrid2.DataModels.EditorComboBox l_EditorCountry = new SourceGrid2.DataModels.EditorComboBox(typeof(string), l_CountryList, false);
			m_ColumnsData[8].DataModel = l_EditorCountry;

			m_ColumnsHeader[9] = new CellColumnTemplate("Phone (string)");
			m_ColumnsData[9] = new CellDataTemplate("Phone", l_Rows, typeof(string));

			m_ColumnsHeader[10] = new CellColumnTemplate("Birthday (DateTime)");
			m_ColumnsData[10] = new CellDataTemplate("Birthday", l_Rows, typeof(DateTime));

			m_ColumnsHeader[11] = new CellColumnTemplate("Selected (bool)");
			m_ColumnsData[11] = new CellDataTemplateCheckBox("Selected", l_Rows);
			
			m_ColumnsHeader[12] = new CellColumnTemplate("website (string)");
			m_ColumnsData[12] = new CellDataTemplateLink("website", l_Rows);
			
			m_ColumnsHeader[13] = new CellColumnTemplate("Price (double)");
			m_ColumnsData[13] = new CellDataTemplate("Price", l_Rows, typeof(double));
			SourceGrid2.DataModels.EditorTextBox l_EditorPrice = new SourceGrid2.DataModels.EditorTextBox(typeof(double));
			l_EditorPrice.TypeConverter = new SourceLibrary.ComponentModel.Converter.CurrencyTypeConverter(typeof(double));
			m_ColumnsData[13].DataModel = l_EditorPrice;
			m_ColumnsData[13].TextAlignment = ContentAlignment.MiddleRight;

			for (int i = 0; i < m_ColumnsData.Length;i++)
			{
				m_ColumnsData[i].BindToGrid(gridVirtual1);
				m_ColumnsHeader[i].BindToGrid(gridVirtual1);
			}

			gridVirtual1.AutoSizeView(true);
		}

		private void gridVirtual1_GettingCell(object sender, SourceGrid2.PositionEventArgs e)
		{
			if (e.Position.Row == 0)
				e.Cell = m_ColumnsHeader[e.Position.Column];
			else
				e.Cell = m_ColumnsData[e.Position.Column];
		}

		private void btSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				using (SaveFileDialog l_dg = new SaveFileDialog())
				{
					l_dg.DefaultExt = "xml";
					l_dg.Filter = "xml Files|*.xml|All Files|*.*";
					if (l_dg.ShowDialog(this) == DialogResult.OK)
					{
						m_XmlDocument.Save(l_dg.FileName);
					}
				}
			}
			catch(Exception err)
			{
				SourceLibrary.Windows.Forms.ErrorDialog.Show(this, err, "Error");
			}
		}

		private void btLoad_Click(object sender, System.EventArgs e)
		{
			try
			{
				using (OpenFileDialog l_dg = new OpenFileDialog())
				{
					l_dg.DefaultExt = "xml";
					l_dg.Filter = "xml Files|*.xml|All Files|*.*";
					if (l_dg.ShowDialog(this) == DialogResult.OK)
					{
						System.Xml.XmlDocument l_Xml = new System.Xml.XmlDocument();
						l_Xml.Load(l_dg.FileName);
						LoadXmlDocument(l_Xml);
					}
				}
			}
			catch(Exception err)
			{
				SourceLibrary.Windows.Forms.ErrorDialog.Show(this, err, "Error");
			}		
		}

		private class CellColumnTemplate : Cells.ColumnHeader
		{
			private string m_Caption;
			public CellColumnTemplate(string p_Caption)
			{
				m_Caption = p_Caption;
			}

			public override object GetValue(SourceGrid2.Position p_Position)
			{
				return m_Caption;
			}
			public override void SetValue(SourceGrid2.Position p_Position, object p_Value)
			{

			}
			public override SourceGrid2.SortStatus GetSortStatus(SourceGrid2.Position p_Position)
			{
				return new SourceGrid2.SortStatus (SourceGrid2.GridSortMode.None, false);
			}
			public override void SetSortMode(SourceGrid2.Position p_Position, SourceGrid2.GridSortMode p_Mode)
			{
				
			}
		}

		private class CellDataTemplate : Cells.CellVirtual
		{
			private string m_ColumnAttribute;
			private System.Xml.XmlNodeList m_Rows;
			private Type m_Type;
			public CellDataTemplate(string p_ColumnAttribute, System.Xml.XmlNodeList p_Rows, Type p_Type)
			{
				m_ColumnAttribute = p_ColumnAttribute;
				m_Rows = p_Rows;
				DataModel = SourceGrid2.Utility.CreateDataModel(p_Type);
				m_Type = p_Type;
			}

			public override object GetValue(SourceGrid2.Position p_Position)
			{
				string l_XmlValue = m_Rows[p_Position.Row-Grid.FixedRows].Attributes[m_ColumnAttribute].InnerText;
				if (m_Type == typeof(string))
					return l_XmlValue;
				if (m_Type == typeof(double))
					return double.Parse(l_XmlValue, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
				if (m_Type == typeof(int))
					return int.Parse(l_XmlValue, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
				if (m_Type == typeof(DateTime))
					return DateTime.Parse(l_XmlValue, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);

				throw new ApplicationException("Type not supported");
			}

			public override void SetValue(SourceGrid2.Position p_Position, object p_Value)
			{
				string l_XmlValue;
				if (m_Type == typeof(string))
					l_XmlValue = (string)p_Value;
				else if (m_Type == typeof(double))
					l_XmlValue = ((double)p_Value).ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
				else if (m_Type == typeof(int))
					l_XmlValue = ((int)p_Value).ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
				else if (m_Type == typeof(DateTime))
					l_XmlValue = ((DateTime)p_Value).ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
				else
					throw new ApplicationException("Type not supported");

				m_Rows[p_Position.Row-Grid.FixedRows].Attributes[m_ColumnAttribute].InnerText = l_XmlValue;

				OnValueChanged(new SourceGrid2.PositionEventArgs(p_Position, this));
			}
		}

		private class CellDataTemplateCheckBox : Cells.CheckBox
		{
			private string m_ColumnAttribute;
			private System.Xml.XmlNodeList m_Rows;
			public CellDataTemplateCheckBox(string p_ColumnAttribute, System.Xml.XmlNodeList p_Rows)
			{
				m_ColumnAttribute = p_ColumnAttribute;
				m_Rows = p_Rows;
			}

			public override object GetValue(SourceGrid2.Position p_Position)
			{
				string l_XmlValue = m_Rows[p_Position.Row-Grid.FixedRows].Attributes[m_ColumnAttribute].InnerText;
				
				return bool.Parse(l_XmlValue);
			}

			public override void SetValue(SourceGrid2.Position p_Position, object p_Value)
			{
				string l_XmlValue = ((bool)p_Value).ToString();

				m_Rows[p_Position.Row-Grid.FixedRows].Attributes[m_ColumnAttribute].InnerText = l_XmlValue;

				OnValueChanged(new SourceGrid2.PositionEventArgs(p_Position, this));
			}
		}

		private class CellDataTemplateLink : Cells.Link
		{
			private string m_ColumnAttribute;
			private System.Xml.XmlNodeList m_Rows;
			public CellDataTemplateLink(string p_ColumnAttribute, System.Xml.XmlNodeList p_Rows)
			{
				m_ColumnAttribute = p_ColumnAttribute;
				m_Rows = p_Rows;
			}

			public override object GetValue(SourceGrid2.Position p_Position)
			{
				string l_XmlValue = m_Rows[p_Position.Row-Grid.FixedRows].Attributes[m_ColumnAttribute].InnerText;
				return l_XmlValue;
			}

			public override void SetValue(SourceGrid2.Position p_Position, object p_Value)
			{
				throw new ApplicationException("Edit not supported");
			}

			public override void OnClick(SourceGrid2.PositionEventArgs e)
			{
				base.OnClick (e);

				try
				{
					SourceLibrary.Utility.Shell.OpenFile(GetDisplayText(e.Position));
				}
				catch(Exception err)
				{
					SourceLibrary.Windows.Forms.ErrorDialog.Show(Grid, err, "Error");
				}
			}

		}
	}
}