using System;
using System.Data;
using System.Drawing;
using DevExpress.Utils;
using System.Windows.Forms;
using DevExpress.XtraPivotGrid;
using DevExpress.Data.PivotGrid;
using DevExpress.XtraEditors.Controls;
using DXSample;
using DevExpress.Data.Filtering.Helpers;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using System.Diagnostics;
using DevExpress.Data;
using System.Collections.Generic;

namespace PivotGrid
{
    public partial class Form1 : Form
    {

        int iView = -1;
        public Form1()
        {
            InitializeComponent();
        }

        private void loadGrid(DataSet oDs)
        {
            pgvObjects.DataSource = oDs.Tables[0];
            pgvObjects.Fields.Clear();

            //pgvObjects.Fields.Add("ITEM_NO", DevExpress.XtraPivotGrid.PivotArea.DataArea);
            //pgvObjects.Fields["ITEM_NO"].Caption = "Document Number";
            //pgvObjects.Fields["ITEM_NO"].Visible = true;
            //pgvObjects.Fields["ITEM_NO"].SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Count;

            pgvObjects.Fields.Add("AMOUNT", DevExpress.XtraPivotGrid.PivotArea.DataArea);
            pgvObjects.Fields["AMOUNT"].Caption = "Amount";
            pgvObjects.Fields["AMOUNT"].Visible = true;
            pgvObjects.Fields["AMOUNT"].SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Sum;

            pgvObjects.Fields.Add("CREATED_ON", DevExpress.XtraPivotGrid.PivotArea.ColumnArea);
            pgvObjects.Fields["CREATED_ON"].Caption = "Created On";
            pgvObjects.Fields["CREATED_ON"].Visible = true;

            pgvObjects.Fields.Add("CREATED_BY", DevExpress.XtraPivotGrid.PivotArea.RowArea);
            pgvObjects.Fields["CREATED_BY"].Caption = "Created By";
            pgvObjects.Fields["CREATED_BY"].Visible = true;



        }
        private void FillDataSet(ref DataSet oDs)
        {

            oDs.Tables.Add();

            oDs.Tables[0].Columns.Add("ITEM_NO", typeof(string));
            oDs.Tables[0].Columns.Add("CREATED_ON", typeof(string));
            oDs.Tables[0].Columns.Add("CREATED_BY", typeof(string));
            oDs.Tables[0].Columns.Add("AMOUNT", typeof(int));

            oDs.Tables[0].Rows.Add();
            oDs.Tables[0].Rows[0]["ITEM_NO"] = "D1";
            oDs.Tables[0].Rows[0]["CREATED_ON"] = "10/JAN/2010";
            oDs.Tables[0].Rows[0]["CREATED_BY"] = "ADMIN";
            oDs.Tables[0].Rows[0]["AMOUNT"] = 0.5;

            oDs.Tables[0].Rows.Add();
            oDs.Tables[0].Rows[1]["ITEM_NO"] = "D2";
            oDs.Tables[0].Rows[1]["CREATED_ON"] = "10/JAN/2010";
            oDs.Tables[0].Rows[1]["CREATED_BY"] = "ADMIN";
            oDs.Tables[0].Rows[1]["AMOUNT"] = 1.3;

            oDs.Tables[0].Rows.Add();
            oDs.Tables[0].Rows[2]["ITEM_NO"] = "D3";
            oDs.Tables[0].Rows[2]["CREATED_ON"] = "11/JAN/2010";
            oDs.Tables[0].Rows[2]["CREATED_BY"] = "ARUN";
            oDs.Tables[0].Rows[2]["AMOUNT"] = 10.0;

            oDs.Tables[0].Rows.Add();
            oDs.Tables[0].Rows[3]["ITEM_NO"] = "D4";
            oDs.Tables[0].Rows[3]["CREATED_ON"] = "11/JAN/2010";
            oDs.Tables[0].Rows[3]["CREATED_BY"] = "ARUN";
            oDs.Tables[0].Rows[3]["AMOUNT"] = 15.5;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataSet oDs = new DataSet();
            FillDataSet(ref oDs);
            loadGrid(oDs);
            InitPivotComboBoxes();
        }

        private void InitPivotComboBoxes()
        {
            try
            {
                if (cboSummaryType.Properties.Items.Count == 0)
                {
                    Array arr = System.Enum.GetValues(typeof(PivotSummaryType));
                    cboSummaryType.Properties.Items.Clear();
                    foreach (PivotSummaryType type in arr)
                        cboSummaryType.Properties.Items.Add(new ImageComboBoxItem(type.ToString(), type, -1));
                    cboSummaryType.SelectedIndex = (int)PivotSummaryType.Count;
                }
                cboField.Properties.Items.Clear();
                foreach (PivotGridField field in pgvObjects.Fields)
                    if (field.Area == PivotArea.DataArea && field.Visible)
                    {
                        cboField.Properties.Items.Add(new ImageComboBoxItem(field.Caption.ToString(), field, -1));
                        try
                        {
                            if (field.SummaryType == PivotSummaryType.Sum || field.SummaryType == PivotSummaryType.Average)
                                field.CellFormat.FormatType = FormatType.Numeric;
                        }
                        catch { }
                    }
                cboField.SelectedIndex = 0;
            }
            catch { }
        }

        private void cboSummaryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PivotGridField field = cboField.EditValue as PivotGridField;
                if (field == null) return;
                field.SummaryType = (PivotSummaryType)cboSummaryType.EditValue;
            }
            catch { }
        }

        private void cboField_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PivotGridField field = cboField.EditValue as PivotGridField;
                if (field == null) return;
                cboSummaryType.EditValue = field.SummaryType;
                SetFieldFont(field);
                field.Appearance.ValueTotal.BackColor = Color.LightSkyBlue;
            }
            catch { }
        }
        private void SetFieldFont(PivotGridField field)
        {
            foreach (PivotGridField fld in pgvObjects.Fields)
                if (fld != field)
                {
                    if (fld.Appearance.Header.Font.Bold)
                        fld.Appearance.Header.Font = new Font(fld.Appearance.Header.Font, FontStyle.Regular);
                }
                else fld.Appearance.Header.Font = new Font(fld.Appearance.Header.Font, FontStyle.Bold);
        }

        private void pgvObjects_CustomSummary(object sender, PivotGridCustomSummaryEventArgs e)
        {
            try
            {
                CustomSummaryExpressionInfo expressionInfo = GetExpressionInfo();
                if (string.IsNullOrEmpty(expressionInfo.Expression)) e.CustomValue = 0;
                else
                {
                    PivotDrillDownDataSource dataSource = e.CreateDrillDownDataSource();
                    ExpressionEvaluator evaluator = new ExpressionEvaluator(((ITypedList)dataSource).GetItemProperties(null),
                        expressionInfo.Expression);
                    object result = null;
                    int cnt = 0;
                    foreach (PivotDrillDownDataRow row in dataSource)
                    {
                        object temp = evaluator.Evaluate(row);
                        if (result == null) result = new NumericObject(temp);
                        else if (temp != null)
                        {
                            NumericObject v1 = new NumericObject(result);
                            NumericObject v2 = new NumericObject(temp);
                            switch (expressionInfo.Aggregate)
                            {
                                case Aggregate.Avg:
                                    result = v1 + v2;
                                    cnt++;
                                    break;
                                case Aggregate.Sum:
                                    result = v1 + v2;
                                    break;
                                case Aggregate.Count:
                                case Aggregate.Exists:
                                    cnt++;
                                    break;
                                case Aggregate.Max: result = NumericObject.Max(v1, v2); break;
                                case Aggregate.Min: result = NumericObject.Min(v1, v2); break;
                            }
                        }
                    }
                    NumericObject numObj = result as NumericObject;
                    if (numObj == null) e.CustomValue = null;
                    else
                    {
                        cnt++;
                        switch (expressionInfo.Aggregate)
                        {
                            case Aggregate.Avg: e.CustomValue = (numObj / new NumericObject(cnt)).Value; break;
                            case Aggregate.Count: e.CustomValue = cnt; break;
                            case Aggregate.Exists: e.CustomValue = cnt > 0 ? 1 : 0; break;
                            case Aggregate.Sum: e.CustomValue = numObj.Value; break;
                            default: e.CustomValue = numObj.Value; break;
                        }
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex); }
        }

        private CustomSummaryExpressionInfo GetExpressionInfo()
        {
            CustomSummaryExpressionInfo expressionInfo = new CustomSummaryExpressionInfo(string.Empty, Aggregate.Sum);
            if (customFormulaList.EditValue != null)
                expressionInfo = (CustomSummaryExpressionInfo)customFormulaList.EditValue;
            return expressionInfo;
        }

        private void OnCustomFormulaListAddButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind != ButtonPredefines.Plus) return;
            List<IDataColumnInfo> columns = new List<IDataColumnInfo>();
            foreach (PivotGridField field in pgvObjects.Fields)
                columns.Add(field);
            CustomSummaryExpressionInfo expressionInfo = GetExpressionInfo();
            using (ExpressionBuilderDialog builder = new ExpressionBuilderDialog(new ExpressionBuilderColumnInfo(columns,
                expressionInfo.Expression), expressionInfo.Aggregate))
            {
                if (builder.ShowDialog() == DialogResult.OK)
                {
                    customFormulaList.SelectedIndex =
                        customFormulaList.Properties.Items.Add(new ImageComboBoxItem(builder.ExpressionInfo.ToString(),
                        builder.ExpressionInfo));
                    pgvObjects.RefreshData();
                }
            }
        }

        private void OnCustomFormulaListSelectedIndexChanged(object sender, EventArgs e)
        {
            pgvObjects.RefreshData();
        }
    }
}