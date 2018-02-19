namespace PropertyGridControl {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.gridControl1 = new PropertyGridControl.CustomGridControl();
            this.dataSet1 = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dataColumn7 = new System.Data.DataColumn();
            this.gridView1 = new PropertyGridControl.CustomGridView();
            this.colAccount = new PropertyGridControl.CustomGridColumn();
            this.colSymbol = new PropertyGridControl.CustomGridColumn();
            this.colProfit = new PropertyGridControl.CustomGridColumn();
            this.colValue = new PropertyGridControl.CustomGridColumn();
            this.colQuarterEndDate = new PropertyGridControl.CustomGridColumn();
            this.colFiscalYearEndDate = new PropertyGridControl.CustomGridColumn();
            this.colIsPublic = new PropertyGridControl.CustomGridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.propertyGridControl1 = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataMember = "Table1";
            this.gridControl1.DataSource = this.dataSet1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gridControl1.Size = new System.Drawing.Size(730, 559);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            this.dataSet1.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1});
            // 
            // dataTable1
            // 
            this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5,
            this.dataColumn6,
            this.dataColumn7});
            this.dataTable1.TableName = "Table1";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "Account";
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "Symbol";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "Profit";
            this.dataColumn3.DataType = typeof(short);
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "Value";
            this.dataColumn4.DataType = typeof(short);
            // 
            // dataColumn5
            // 
            this.dataColumn5.ColumnName = "QuarterEndDate";
            // 
            // dataColumn6
            // 
            this.dataColumn6.ColumnName = "FiscalYearEndDate";
            // 
            // dataColumn7
            // 
            this.dataColumn7.ColumnName = "IsPublic";
            this.dataColumn7.DataType = typeof(bool);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAccount,
            this.colSymbol,
            this.colProfit,
            this.colValue,
            this.colQuarterEndDate,
            this.colFiscalYearEndDate,
            this.colIsPublic});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupCount = 1;
            this.gridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.gridView1.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Profit", this.colProfit, "{0:n2}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Value", this.colValue, "{0:n2}"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Max, "Symbol", this.colSymbol, "")});
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gridView1.OptionsBehavior.SummariesIgnoreNullValues = true;
            this.gridView1.OptionsCustomization.QuickCustomizationIcons.Image = ((System.Drawing.Image)(resources.GetObject("gridView1.OptionsCustomization.QuickCustomizationIcons.Image")));
            this.gridView1.OptionsCustomization.QuickCustomizationIcons.TransperentColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.ShouldDrawFooter = false;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colAccount, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colAccount
            // 
            this.colAccount.Caption = "Account";
            this.colAccount.FieldName = "Account";
            this.colAccount.Name = "colAccount";
            this.colAccount.OptionsColumn.AllowEdit = false;
            this.colAccount.OptionsColumn.ReadOnly = true;
            this.colAccount.ShowUnboundExpressionMenu = true;
            // 
            // colSymbol
            // 
            this.colSymbol.Caption = "Symbol";
            this.colSymbol.FieldName = "Symbol";
            this.colSymbol.Name = "colSymbol";
            this.colSymbol.OptionsColumn.AllowEdit = false;
            this.colSymbol.OptionsColumn.AllowMove = false;
            this.colSymbol.OptionsColumn.AllowQuickHide = false;
            this.colSymbol.OptionsColumn.AllowShowHide = false;
            this.colSymbol.OptionsColumn.ReadOnly = true;
            this.colSymbol.Visible = true;
            this.colSymbol.VisibleIndex = 0;
            // 
            // colProfit
            // 
            this.colProfit.Caption = "Profit";
            this.colProfit.DisplayFormat.FormatString = "{0:n2}";
            this.colProfit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colProfit.FieldName = "Profit";
            this.colProfit.GroupFormat.FormatString = "{0:n2}";
            this.colProfit.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colProfit.Name = "colProfit";
            this.colProfit.OptionsColumn.AllowEdit = false;
            this.colProfit.OptionsColumn.ReadOnly = true;
            this.colProfit.ShowUnboundExpressionMenu = true;
            this.colProfit.SummaryItem.DisplayFormat = "{0:n2}";
            this.colProfit.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Min;
            this.colProfit.Visible = true;
            this.colProfit.VisibleIndex = 1;
            // 
            // colValue
            // 
            this.colValue.Caption = "Value";
            this.colValue.DisplayFormat.FormatString = "{0:n2}";
            this.colValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colValue.FieldName = "Value";
            this.colValue.GroupFormat.FormatString = "{0:n2}";
            this.colValue.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colValue.Name = "colValue";
            this.colValue.OptionsColumn.AllowEdit = false;
            this.colValue.OptionsColumn.ReadOnly = true;
            this.colValue.SummaryItem.DisplayFormat = "{0:n2}";
            this.colValue.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colValue.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.colValue.Visible = true;
            this.colValue.VisibleIndex = 3;
            // 
            // colQuarterEndDate
            // 
            this.colQuarterEndDate.Caption = "Quarter End Date";
            this.colQuarterEndDate.FieldName = "QuarterEndDate";
            this.colQuarterEndDate.Name = "colQuarterEndDate";
            this.colQuarterEndDate.OptionsColumn.AllowEdit = false;
            this.colQuarterEndDate.OptionsColumn.ReadOnly = true;
            this.colQuarterEndDate.Visible = true;
            this.colQuarterEndDate.VisibleIndex = 2;
            // 
            // colFiscalYearEndDate
            // 
            this.colFiscalYearEndDate.Caption = "Fiscal Year End Date";
            this.colFiscalYearEndDate.FieldName = "FiscalYearEndDate";
            this.colFiscalYearEndDate.Name = "colFiscalYearEndDate";
            this.colFiscalYearEndDate.OptionsColumn.AllowEdit = false;
            this.colFiscalYearEndDate.OptionsColumn.ReadOnly = true;
            this.colFiscalYearEndDate.Visible = true;
            this.colFiscalYearEndDate.VisibleIndex = 4;
            // 
            // colIsPublic
            // 
            this.colIsPublic.Caption = "Public?";
            this.colIsPublic.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colIsPublic.FieldName = "IsPublic";
            this.colIsPublic.Name = "colIsPublic";
            this.colIsPublic.OptionsColumn.AllowEdit = false;
            this.colIsPublic.OptionsColumn.ReadOnly = true;
            this.colIsPublic.Visible = true;
            this.colIsPublic.VisibleIndex = 5;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.DisplayValueChecked = "True";
            this.repositoryItemCheckEdit1.DisplayValueUnchecked = "False";
            this.repositoryItemCheckEdit1.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.DisplayText;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // propertyGridControl1
            // 
            this.propertyGridControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.propertyGridControl1.Location = new System.Drawing.Point(736, 0);
            this.propertyGridControl1.Name = "propertyGridControl1";
            this.propertyGridControl1.SelectedObject = this.gridView1;
            this.propertyGridControl1.Size = new System.Drawing.Size(328, 559);
            this.propertyGridControl1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 559);
            this.Controls.Add(this.propertyGridControl1);
            this.Controls.Add(this.gridControl1);
            this.Name = "Form1";
            this.Text = "Grid";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PropertyGridControl.CustomGridControl gridControl1;
        private System.Data.DataSet dataSet1;
        private System.Data.DataTable dataTable1;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn5;
        private System.Data.DataColumn dataColumn6;
        private CustomGridColumn colAccount;
        private CustomGridColumn colSymbol;
        private CustomGridColumn colProfit;
        private CustomGridColumn colValue;
        private CustomGridColumn colQuarterEndDate;
        private CustomGridColumn colFiscalYearEndDate;
        //private DevExpress.XtraGrid.Columns.GridColumn colAccount;
        //private DevExpress.XtraGrid.Columns.GridColumn colSymbol;
        //private DevExpress.XtraGrid.Columns.GridColumn colProfit;
        //private DevExpress.XtraGrid.Columns.GridColumn colValue;
        //private DevExpress.XtraGrid.Columns.GridColumn colQuarterEndDate;
        //private DevExpress.XtraGrid.Columns.GridColumn colFiscalYearEndDate;
        private PropertyGridControl.CustomGridView gridView1;
        private System.Data.DataColumn dataColumn7;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private CustomGridColumn colIsPublic;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControl1;
    }
}

