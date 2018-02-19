using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using DevExpress.Utils;
using DevExpress.XtraPivotGrid;
using DevExpress.LookAndFeel;

namespace WindowsApplication4
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
      private System.Data.DataSet dataSet1;
      private System.Data.DataTable dataTable1;
      private System.Data.DataColumn dataColumn1;
      private System.Data.DataColumn dataColumn2;
      private System.Data.DataColumn dataColumn3;
      private DevExpress.XtraPivotGrid.PivotGridControl pivotGridControl2;
      private System.Windows.Forms.Button button1;
      private DevExpress.XtraPrinting.PrintingSystem printingSystem1;
      private DevExpress.XtraPrinting.PrintableComponentLink printableComponentLink1;
      private System.Windows.Forms.ProgressBar progressBar1;
      private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
      private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
      private System.Data.SqlClient.SqlConnection sqlConnection1;
      private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
      private WindowsApplication4.DataSet1 dataSet11;
      private DevExpress.XtraPivotGrid.PivotGridField fieldCountry;
      private DevExpress.XtraPivotGrid.PivotGridField fieldOrderDate;
      private DevExpress.XtraPivotGrid.PivotGridField fieldDiscount;
      private DevExpress.XtraPivotGrid.PivotGridField fieldOrderDate1;
      private System.ComponentModel.IContainer components;

		public Form1()
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
         this.components = new System.ComponentModel.Container();
         DevExpress.XtraPivotGrid.PivotGridCustomTotal pivotGridCustomTotal1 = new DevExpress.XtraPivotGrid.PivotGridCustomTotal();
         this.dataSet1 = new System.Data.DataSet();
         this.dataTable1 = new System.Data.DataTable();
         this.dataColumn1 = new System.Data.DataColumn();
         this.dataColumn2 = new System.Data.DataColumn();
         this.dataColumn3 = new System.Data.DataColumn();
         this.pivotGridControl2 = new DevExpress.XtraPivotGrid.PivotGridControl();
         this.dataSet11 = new WindowsApplication4.DataSet1();
         this.fieldCountry = new DevExpress.XtraPivotGrid.PivotGridField();
         this.fieldOrderDate = new DevExpress.XtraPivotGrid.PivotGridField();
         this.fieldDiscount = new DevExpress.XtraPivotGrid.PivotGridField();
         this.button1 = new System.Windows.Forms.Button();
         this.printingSystem1 = new DevExpress.XtraPrinting.PrintingSystem(this.components);
         this.printableComponentLink1 = new DevExpress.XtraPrinting.PrintableComponentLink(this.components);
         this.progressBar1 = new System.Windows.Forms.ProgressBar();
         this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
         this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
         this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
         this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
         this.fieldOrderDate1 = new DevExpress.XtraPivotGrid.PivotGridField();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl2)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet11)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.printingSystem1)).BeginInit();
         this.SuspendLayout();
         // 
         // dataSet1
         // 
         this.dataSet1.DataSetName = "NewDataSet";
         this.dataSet1.Locale = new System.Globalization.CultureInfo("en-US");
         this.dataSet1.Tables.AddRange(new System.Data.DataTable[] {
                                                                      this.dataTable1});
         // 
         // dataTable1
         // 
         this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
                                                                          this.dataColumn1,
                                                                          this.dataColumn2,
                                                                          this.dataColumn3});
         this.dataTable1.TableName = "Table1";
         // 
         // dataColumn1
         // 
         this.dataColumn1.ColumnName = "Columns";
         // 
         // dataColumn2
         // 
         this.dataColumn2.ColumnName = "Rows";
         this.dataColumn2.DataType = typeof(System.DateTime);
         // 
         // dataColumn3
         // 
         this.dataColumn3.ColumnName = "Data";
         this.dataColumn3.DataType = typeof(int);
         // 
         // pivotGridControl2
         // 
         this.pivotGridControl2.Cursor = System.Windows.Forms.Cursors.Arrow;
         this.pivotGridControl2.DataSource = this.dataSet11.Invoices;
         this.pivotGridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
         this.pivotGridControl2.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
                                                                                                 this.fieldCountry,
                                                                                                 this.fieldOrderDate,
                                                                                                 this.fieldDiscount,
                                                                                                 this.fieldOrderDate1});
         this.pivotGridControl2.Location = new System.Drawing.Point(0, 0);
         this.pivotGridControl2.Name = "pivotGridControl2";
         this.pivotGridControl2.OptionsLayout.Columns.StoreAllOptions = true;
         this.pivotGridControl2.OptionsLayout.Columns.StoreAppearance = true;
         this.pivotGridControl2.OptionsLayout.StoreAllOptions = true;
         this.pivotGridControl2.OptionsLayout.StoreAppearance = true;
         this.pivotGridControl2.Size = new System.Drawing.Size(756, 498);
         this.pivotGridControl2.TabIndex = 13;
         this.pivotGridControl2.CustomSummary += new DevExpress.XtraPivotGrid.PivotGridCustomSummaryEventHandler(this.pivotGridControl2_CustomSummary);
         // 
         // dataSet11
         // 
         this.dataSet11.DataSetName = "DataSet1";
         this.dataSet11.Locale = new System.Globalization.CultureInfo("en-US");
         // 
         // fieldCountry
         // 
         this.fieldCountry.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
         this.fieldCountry.AreaIndex = 0;
         this.fieldCountry.FieldName = "Country";
         this.fieldCountry.Name = "fieldCountry";
         // 
         // fieldOrderDate
         // 
         this.fieldOrderDate.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
         this.fieldOrderDate.AreaIndex = 0;
         pivotGridCustomTotal1.Appearance.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(192)), ((System.Byte)(192)));
         pivotGridCustomTotal1.Appearance.Options.UseBackColor = true;
         pivotGridCustomTotal1.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Custom;
         this.fieldOrderDate.CustomTotals.AddRange(new DevExpress.XtraPivotGrid.PivotGridCustomTotal[] {
                                                                                                          pivotGridCustomTotal1});
         this.fieldOrderDate.FieldName = "OrderDate";
         this.fieldOrderDate.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear;
         this.fieldOrderDate.Name = "fieldOrderDate";
         this.fieldOrderDate.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.CustomTotals;
         // 
         // fieldDiscount
         // 
         this.fieldDiscount.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
         this.fieldDiscount.AreaIndex = 0;
         this.fieldDiscount.CellFormat.FormatString = "c";
         this.fieldDiscount.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
         this.fieldDiscount.FieldName = "Discount";
         this.fieldDiscount.Name = "fieldDiscount";
         // 
         // button1
         // 
         this.button1.Location = new System.Drawing.Point(298, 364);
         this.button1.Name = "button1";
         this.button1.TabIndex = 14;
         this.button1.Text = "button1";
         // 
         // printingSystem1
         // 
         this.printingSystem1.Links.AddRange(new object[] {
                                                             this.printableComponentLink1});
         // 
         // printableComponentLink1
         // 
         this.printableComponentLink1.Component = this.pivotGridControl2;
         this.printableComponentLink1.PrintingSystem = this.printingSystem1;
         // 
         // progressBar1
         // 
         this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.progressBar1.Location = new System.Drawing.Point(0, 492);
         this.progressBar1.Name = "progressBar1";
         this.progressBar1.Size = new System.Drawing.Size(756, 6);
         this.progressBar1.TabIndex = 15;
         // 
         // sqlSelectCommand1
         // 
         this.sqlSelectCommand1.CommandText = @"SELECT ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, ShipCountry, CustomerID, CustomerName, Address, City, Region, PostalCode, Country, Salesperson, OrderID, OrderDate, RequiredDate, ShippedDate, ShipperName, ProductID, ProductName, UnitPrice, Quantity, Discount, ExtendedPrice, Freight FROM Invoices";
         this.sqlSelectCommand1.Connection = this.sqlConnection1;
         // 
         // sqlConnection1
         // 
         this.sqlConnection1.ConnectionString = "workstation id=PLATON;packet size=4096;user id=sa;data source=blackbox;persist se" +
            "curity info=False;initial catalog=Northwind";
         // 
         // sqlInsertCommand1
         // 
         this.sqlInsertCommand1.CommandText = @"INSERT INTO Invoices(ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, ShipCountry, CustomerID, CustomerName, Address, City, Region, PostalCode, Country, Salesperson, OrderDate, RequiredDate, ShippedDate, ShipperName, ProductID, ProductName, UnitPrice, Quantity, Discount, ExtendedPrice, Freight) VALUES (@ShipName, @ShipAddress, @ShipCity, @ShipRegion, @ShipPostalCode, @ShipCountry, @CustomerID, @CustomerName, @Address, @City, @Region, @PostalCode, @Country, @Salesperson, @OrderDate, @RequiredDate, @ShippedDate, @ShipperName, @ProductID, @ProductName, @UnitPrice, @Quantity, @Discount, @ExtendedPrice, @Freight); SELECT ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, ShipCountry, CustomerID, CustomerName, Address, City, Region, PostalCode, Country, Salesperson, OrderID, OrderDate, RequiredDate, ShippedDate, ShipperName, ProductID, ProductName, UnitPrice, Quantity, Discount, ExtendedPrice, Freight FROM Invoices";
         this.sqlInsertCommand1.Connection = this.sqlConnection1;
         this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ShipName", System.Data.SqlDbType.NVarChar, 40, "ShipName"));
         this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ShipAddress", System.Data.SqlDbType.NVarChar, 60, "ShipAddress"));
         this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ShipCity", System.Data.SqlDbType.NVarChar, 15, "ShipCity"));
         this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ShipRegion", System.Data.SqlDbType.NVarChar, 15, "ShipRegion"));
         this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ShipPostalCode", System.Data.SqlDbType.NVarChar, 10, "ShipPostalCode"));
         this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ShipCountry", System.Data.SqlDbType.NVarChar, 15, "ShipCountry"));
         this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CustomerID", System.Data.SqlDbType.NVarChar, 5, "CustomerID"));
         this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CustomerName", System.Data.SqlDbType.NVarChar, 40, "CustomerName"));
         this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Address", System.Data.SqlDbType.NVarChar, 60, "Address"));
         this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@City", System.Data.SqlDbType.NVarChar, 15, "City"));
         this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Region", System.Data.SqlDbType.NVarChar, 15, "Region"));
         this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PostalCode", System.Data.SqlDbType.NVarChar, 10, "PostalCode"));
         this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Country", System.Data.SqlDbType.NVarChar, 15, "Country"));
         this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Salesperson", System.Data.SqlDbType.NVarChar, 31, "Salesperson"));
         this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OrderDate", System.Data.SqlDbType.DateTime, 8, "OrderDate"));
         this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RequiredDate", System.Data.SqlDbType.DateTime, 8, "RequiredDate"));
         this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ShippedDate", System.Data.SqlDbType.DateTime, 8, "ShippedDate"));
         this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ShipperName", System.Data.SqlDbType.NVarChar, 40, "ShipperName"));
         this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProductID", System.Data.SqlDbType.Int, 4, "ProductID"));
         this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProductName", System.Data.SqlDbType.NVarChar, 40, "ProductName"));
         this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UnitPrice", System.Data.SqlDbType.Money, 8, "UnitPrice"));
         this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Quantity", System.Data.SqlDbType.SmallInt, 2, "Quantity"));
         this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Discount", System.Data.SqlDbType.Real, 4, "Discount"));
         this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ExtendedPrice", System.Data.SqlDbType.Money, 8, "ExtendedPrice"));
         this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Freight", System.Data.SqlDbType.Money, 8, "Freight"));
         // 
         // sqlDataAdapter1
         // 
         this.sqlDataAdapter1.InsertCommand = this.sqlInsertCommand1;
         this.sqlDataAdapter1.SelectCommand = this.sqlSelectCommand1;
         this.sqlDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
                                                                                                  new System.Data.Common.DataTableMapping("Table", "Invoices", new System.Data.Common.DataColumnMapping[] {
                                                                                                                                                                                                             new System.Data.Common.DataColumnMapping("ShipName", "ShipName"),
                                                                                                                                                                                                             new System.Data.Common.DataColumnMapping("ShipAddress", "ShipAddress"),
                                                                                                                                                                                                             new System.Data.Common.DataColumnMapping("ShipCity", "ShipCity"),
                                                                                                                                                                                                             new System.Data.Common.DataColumnMapping("ShipRegion", "ShipRegion"),
                                                                                                                                                                                                             new System.Data.Common.DataColumnMapping("ShipPostalCode", "ShipPostalCode"),
                                                                                                                                                                                                             new System.Data.Common.DataColumnMapping("ShipCountry", "ShipCountry"),
                                                                                                                                                                                                             new System.Data.Common.DataColumnMapping("CustomerID", "CustomerID"),
                                                                                                                                                                                                             new System.Data.Common.DataColumnMapping("CustomerName", "CustomerName"),
                                                                                                                                                                                                             new System.Data.Common.DataColumnMapping("Address", "Address"),
                                                                                                                                                                                                             new System.Data.Common.DataColumnMapping("City", "City"),
                                                                                                                                                                                                             new System.Data.Common.DataColumnMapping("Region", "Region"),
                                                                                                                                                                                                             new System.Data.Common.DataColumnMapping("PostalCode", "PostalCode"),
                                                                                                                                                                                                             new System.Data.Common.DataColumnMapping("Country", "Country"),
                                                                                                                                                                                                             new System.Data.Common.DataColumnMapping("Salesperson", "Salesperson"),
                                                                                                                                                                                                             new System.Data.Common.DataColumnMapping("OrderID", "OrderID"),
                                                                                                                                                                                                             new System.Data.Common.DataColumnMapping("OrderDate", "OrderDate"),
                                                                                                                                                                                                             new System.Data.Common.DataColumnMapping("RequiredDate", "RequiredDate"),
                                                                                                                                                                                                             new System.Data.Common.DataColumnMapping("ShippedDate", "ShippedDate"),
                                                                                                                                                                                                             new System.Data.Common.DataColumnMapping("ShipperName", "ShipperName"),
                                                                                                                                                                                                             new System.Data.Common.DataColumnMapping("ProductID", "ProductID"),
                                                                                                                                                                                                             new System.Data.Common.DataColumnMapping("ProductName", "ProductName"),
                                                                                                                                                                                                             new System.Data.Common.DataColumnMapping("UnitPrice", "UnitPrice"),
                                                                                                                                                                                                             new System.Data.Common.DataColumnMapping("Quantity", "Quantity"),
                                                                                                                                                                                                             new System.Data.Common.DataColumnMapping("Discount", "Discount"),
                                                                                                                                                                                                             new System.Data.Common.DataColumnMapping("ExtendedPrice", "ExtendedPrice"),
                                                                                                                                                                                                             new System.Data.Common.DataColumnMapping("Freight", "Freight")})});
         // 
         // fieldOrderDate1
         // 
         this.fieldOrderDate1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
         this.fieldOrderDate1.AreaIndex = 1;
         this.fieldOrderDate1.FieldName = "OrderDate";
         this.fieldOrderDate1.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth;
         this.fieldOrderDate1.Name = "fieldOrderDate1";
         // 
         // Form1
         // 
         this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
         this.ClientSize = new System.Drawing.Size(756, 498);
         this.Controls.Add(this.progressBar1);
         this.Controls.Add(this.button1);
         this.Controls.Add(this.pivotGridControl2);
         this.Name = "Form1";
         this.Text = "Form1";
         this.Load += new System.EventHandler(this.Form1_Load);
         ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl2)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dataSet11)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.printingSystem1)).EndInit();
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

      private void Form1_Load(object sender, System.EventArgs e) {
         sqlDataAdapter1.Fill(dataSet11);
      }

      private void pivotGridControl2_CustomSummary(object sender, DevExpress.XtraPivotGrid.PivotGridCustomSummaryEventArgs e) 
      {
         e.CustomValue = 12345;
      }
	}
}
