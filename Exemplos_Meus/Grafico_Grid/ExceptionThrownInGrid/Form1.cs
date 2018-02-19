using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.XtraPivotGrid;

namespace ExceptionThrownInGrid
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Form1(DataTable dataTable)
        {
            InitializeComponent();
            foreach (DataRow item in dataTable.Rows)
            {
                DataRow linha = dataTable1.NewRow();
                int i = 1;
                linha["CodigoEmpresa"] = item["CodigoEmpresa"];
                linha["CodigoFl"] = item["CodigoFl"];
                linha["CodigoGa"] = item["CodigoGa"];
                linha["Linha"] = item["Linha"];
                linha["Qtde"] = item["Qtde"];

                dataTable1.Rows.Add(linha);
            }

            chartControl1.DataSource = pivotGridControl1;
            chartControl1.SeriesDataMember = "Series";
            chartControl1.SeriesTemplate.ArgumentDataMember = "Arguments";
            chartControl1.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "Values" });
            chartControl1.SeriesTemplate.LegendPointOptions.PointView = PointView.ArgumentAndValues;            
        }
       
        private void checkedComboBoxEdit1_Properties_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            if (e.CloseMode == DevExpress.XtraEditors.PopupCloseMode.ButtonClick)
            {
                chartControl1.SeriesTemplate.Label.Visible = checkedComboBoxEdit1.Properties.Items[0].CheckState == CheckState.Checked;
                pivotGridControl1.OptionsChartDataSource.ShowColumnGrandTotals = checkedComboBoxEdit1.Properties.Items[1].CheckState == CheckState.Checked;
                pivotGridControl1.OptionsChartDataSource.ShowRowGrandTotals = checkedComboBoxEdit1.Properties.Items[2].CheckState == CheckState.Checked;
            }

            //xyDiagram1.AxisY.Title.Text = "US Dollars";
            //AxisX

            ((XYDiagram)(chartControl1.Diagram)).AxisY.Title.Visible = true;
            ((XYDiagram)(chartControl1.Diagram)).AxisY.Title.Text = RetornaCabecalhoColunasSelecionadas(PivotArea.RowArea);
            ((XYDiagram)(chartControl1.Diagram)).AxisY.Label.Visible = false;
            ((XYDiagram)(chartControl1.Diagram)).AxisY.Tickmarks.Visible = false;

            ((XYDiagram)(chartControl1.Diagram)).AxisX.Title.Visible = true;            
            ((XYDiagram)(chartControl1.Diagram)).AxisX.Title.Text = RetornaCabecalhoColunasSelecionadas(PivotArea.ColumnArea);
            ((XYDiagram)(chartControl1.Diagram)).AxisX.Label.Angle = -30;
            ((XYDiagram)(chartControl1.Diagram)).AxisX.Label.Antialiasing = true;

            MessageBox.Show(
                RetornaCabecalhoColunasSelecionadas(PivotArea.ColumnArea)
                +" ## " +
                RetornaCabecalhoColunasSelecionadas(PivotArea.RowArea)
                );
            
        }

        private string RetornaCabecalhoColunasSelecionadas(PivotArea pivotArea)
        {
            string retorno = "";
            foreach (PivotGridField item in pivotGridControl1.GetFieldsByArea(pivotArea))
            {
                retorno += (retorno == "" ? "" : " | ") + item.Caption;
            }            
            return retorno;
        }


    }
}
