namespace ExceptionThrownInGrid
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraCharts.FullStackedBarSeriesLabel fullStackedBarSeriesLabel1 = new DevExpress.XtraCharts.FullStackedBarSeriesLabel();
            DevExpress.XtraCharts.FullStackedBarSeriesView fullStackedBarSeriesView1 = new DevExpress.XtraCharts.FullStackedBarSeriesView();
            this.dataSet1 = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.CodigoEmpresa = new System.Data.DataColumn();
            this.CodigoFl = new System.Data.DataColumn();
            this.CodigoGa = new System.Data.DataColumn();
            this.Linha = new System.Data.DataColumn();
            this.Qtde = new System.Data.DataColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.table1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.checkedComboBoxEdit1 = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.pivotGridControl1 = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.fieldCodigoEmpresa = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldCodigoFl = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldCodigoGa = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldLinha = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldQtde = new DevExpress.XtraPivotGrid.PivotGridField();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.table1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(fullStackedBarSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(fullStackedBarSeriesView1)).BeginInit();
            this.SuspendLayout();
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
            this.CodigoEmpresa,
            this.CodigoFl,
            this.CodigoGa,
            this.Linha,
            this.Qtde});
            this.dataTable1.TableName = "Table1";
            // 
            // CodigoEmpresa
            // 
            this.CodigoEmpresa.ColumnName = "CodigoEmpresa";
            this.CodigoEmpresa.DataType = typeof(int);
            // 
            // CodigoFl
            // 
            this.CodigoFl.ColumnName = "CodigoFl";
            this.CodigoFl.DataType = typeof(int);
            // 
            // CodigoGa
            // 
            this.CodigoGa.ColumnName = "CodigoGa";
            this.CodigoGa.DataType = typeof(int);
            // 
            // Linha
            // 
            this.Linha.ColumnName = "Linha";
            // 
            // Qtde
            // 
            this.Qtde.ColumnName = "Qtde";
            this.Qtde.DataType = typeof(int);
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = this.dataSet1;
            this.bindingSource1.Position = 0;
            // 
            // table1BindingSource
            // 
            this.table1BindingSource.DataMember = "Table1";
            this.table1BindingSource.DataSource = this.bindingSource1;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.checkedComboBoxEdit1);
            this.splitContainerControl1.Panel1.Controls.Add(this.pivotGridControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.chartControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(771, 345);
            this.splitContainerControl1.SplitterPosition = 151;
            this.splitContainerControl1.TabIndex = 3;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // checkedComboBoxEdit1
            // 
            this.checkedComboBoxEdit1.EditValue = "";
            this.checkedComboBoxEdit1.Location = new System.Drawing.Point(470, 12);
            this.checkedComboBoxEdit1.Name = "checkedComboBoxEdit1";
            this.checkedComboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.checkedComboBoxEdit1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "Legenda"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "Total coluna"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem(null, "Total linha")});
            this.checkedComboBoxEdit1.Properties.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.checkedComboBoxEdit1_Properties_Closed);
            this.checkedComboBoxEdit1.Size = new System.Drawing.Size(100, 20);
            this.checkedComboBoxEdit1.TabIndex = 5;
            // 
            // pivotGridControl1
            // 
            this.pivotGridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pivotGridControl1.DataSource = this.table1BindingSource;
            this.pivotGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pivotGridControl1.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.fieldCodigoEmpresa,
            this.fieldCodigoFl,
            this.fieldCodigoGa,
            this.fieldLinha,
            this.fieldQtde});
            this.pivotGridControl1.Location = new System.Drawing.Point(0, 0);
            this.pivotGridControl1.Name = "pivotGridControl1";
            this.pivotGridControl1.OptionsOLAP.UseAggregateForSingleFilterValue = true;
            this.pivotGridControl1.OptionsView.ShowCustomTotalsForSingleValues = true;
            this.pivotGridControl1.OptionsView.ShowGrandTotalsForSingleValues = true;
            this.pivotGridControl1.OptionsView.ShowTotalsForSingleValues = true;
            this.pivotGridControl1.Size = new System.Drawing.Size(771, 151);
            this.pivotGridControl1.TabIndex = 3;
            // 
            // fieldCodigoEmpresa
            // 
            this.fieldCodigoEmpresa.AreaIndex = 0;
            this.fieldCodigoEmpresa.Caption = "Codigo Empresa";
            this.fieldCodigoEmpresa.FieldName = "CodigoEmpresa";
            this.fieldCodigoEmpresa.Name = "fieldCodigoEmpresa";
            this.fieldCodigoEmpresa.RunningTotal = true;
            this.fieldCodigoEmpresa.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Count;
            // 
            // fieldCodigoFl
            // 
            this.fieldCodigoFl.AreaIndex = 4;
            this.fieldCodigoFl.Caption = "Codigo Fl";
            this.fieldCodigoFl.FieldName = "CodigoFl";
            this.fieldCodigoFl.Name = "fieldCodigoFl";
            this.fieldCodigoFl.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Count;
            // 
            // fieldCodigoGa
            // 
            this.fieldCodigoGa.AreaIndex = 3;
            this.fieldCodigoGa.Caption = "Codigo Ga";
            this.fieldCodigoGa.FieldName = "CodigoGa";
            this.fieldCodigoGa.Name = "fieldCodigoGa";
            this.fieldCodigoGa.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Count;
            // 
            // fieldLinha
            // 
            this.fieldLinha.AreaIndex = 2;
            this.fieldLinha.Caption = "Linha";
            this.fieldLinha.FieldName = "Linha";
            this.fieldLinha.Name = "fieldLinha";
            this.fieldLinha.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Count;
            // 
            // fieldQtde
            // 
            this.fieldQtde.AreaIndex = 1;
            this.fieldQtde.Caption = "Qtde";
            this.fieldQtde.FieldName = "Qtde";
            this.fieldQtde.Name = "fieldQtde";
            this.fieldQtde.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Count;
            // 
            // chartControl1
            // 
            this.chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl1.Location = new System.Drawing.Point(0, 0);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl1.SeriesTemplate.Label = fullStackedBarSeriesLabel1;
            this.chartControl1.SeriesTemplate.View = fullStackedBarSeriesView1;
            this.chartControl1.Size = new System.Drawing.Size(771, 188);
            this.chartControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 345);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.table1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(fullStackedBarSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(fullStackedBarSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Data.DataColumn CodigoEmpresa;
        private System.Data.DataColumn CodigoFl;
        private System.Data.DataColumn CodigoGa;
        private System.Data.DataColumn Linha;
        private System.Data.DataColumn Qtde;
        public System.Data.DataSet dataSet1;
        public System.Data.DataTable dataTable1;
        private System.Windows.Forms.BindingSource bindingSource1;
        public System.Windows.Forms.BindingSource table1BindingSource;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraPivotGrid.PivotGridControl pivotGridControl1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldCodigoEmpresa;
        private DevExpress.XtraPivotGrid.PivotGridField fieldCodigoFl;
        private DevExpress.XtraPivotGrid.PivotGridField fieldCodigoGa;
        private DevExpress.XtraPivotGrid.PivotGridField fieldLinha;
        private DevExpress.XtraPivotGrid.PivotGridField fieldQtde;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private DevExpress.XtraEditors.CheckedComboBoxEdit checkedComboBoxEdit1;
    }
}