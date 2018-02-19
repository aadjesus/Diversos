using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraCharts;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data.PivotGrid;
using DevExpress.Wpf.Charts;

namespace GraficosGrids
{
    public partial class Form2 : Form
    {
        public DataTable _dataTable;

        public Form2()
        {
            InitializeComponent();

            //byte[] byteArray = ;
            //StreamReader stream = new StreamReader(new MemoryStream(Encoding.ASCII.GetBytes(Resource1.ArquivoTeste)));
            try
            {
//                

//		"C:\\Users\\alessandro.augusto\\Documents\\Visual Studio\\Meus_Testes\\GraficosGrids\\GraficosGrids\\bin\\Debug"	string

                StreamReader stream = new StreamReader(Directory.GetCurrentDirectory() + "\\ArquivoTeste.txt");

                List<string> arquivo = new List<string>();
                string linha = null;
                while ((linha = stream.ReadLine()) != null)
                    arquivo.Add(linha.Replace("#�#", "# #"));

                _dataTable = FGlobus.Util.Util.DesConcatenarResultadoLinq(arquivo.ToArray());
                _dataTable.TableName = "Retorno_Lista";

                dataSet1.Tables.Clear();
                dataSet1.Tables.Add(_dataTable);
                gridControl1.Refresh();

                UserControl1 userControl1 = new UserControl1();

                var listaQtdeVeicDias = _dataTable.Rows
                            .Cast<DataRow>()
                            .GroupBy(g => new
                            {
                                CodigoLinha = g.Field<string>("Column3"),
                                Hora = g.Field<string>("Column2")
                            })
                            .Select(s => new
                            {
                                CodigoLinha = s.Key.CodigoLinha,
                                Hora = s.Key.Hora,
                                Qtde = s.Count()
                            })
                            .ToArray();

                #region SeriesRankingDasLinhas

                userControl1.SeriesRankingDasLinhas.Series.Add(
                    new BarStackedSeries2D()
                    {
                        DisplayName = "Peso",
                        ArgumentDataMember = "CodigoLinha",
                        ValueDataMember = "Peso",
                        ArgumentScaleType = DevExpress.Wpf.Charts.ScaleType.Qualitative,
                        Label = new SeriesLabel() { Visible = false },
                        DataSource = _dataTable.Rows
                            .Cast<DataRow>()
                            .GroupBy(g => g.Field<string>("Column3"))
                            .Select(s => new
                            {
                                CodigoLinha = s.Key,
                                Peso = s.Sum(s1 => s1.Field<int>("Column13"))
                            })
                            .Where(w => w.Peso > 0)
                            .Select(s => new
                            {
                                s.CodigoLinha,
                                Peso = s.Peso / listaQtdeVeicDias
                                    .Where(w => w.CodigoLinha.Equals(s.CodigoLinha))
                                    .Sum(sm => sm.Qtde)
                            })
                            .OrderByDescending(o => o.Peso)
                            .ToArray()
                    }
                    );

                #endregion

                #region SeriesRankingDasLinhasDias

                userControl1.SeriesRankingDasLinhasDias.Series.AddRange(
                    _dataTable.Rows
                    .Cast<DataRow>()
                    .GroupBy(g => g.Field<string>("Column2"))
                    .Select(s => s.Key)
                    .ToArray()
                    .Select(s => new BarStackedSeries2D()
                    {
                        DisplayName = s,
                        ArgumentDataMember = "CodigoLinha",
                        ValueDataMember = "Peso",
                        ArgumentScaleType = DevExpress.Wpf.Charts.ScaleType.Qualitative,
                        Label = new SeriesLabel() { Visible = false },
                        DataSource = _dataTable.Rows
                            .Cast<DataRow>()
                            .Where(w => w.Field<string>("Column2").Equals(s))
                            .GroupBy(g => g.Field<string>("Column3"))
                            .Select(s1 => new
                            {
                                CodigoLinha = s1.Key,
                                Peso = s1.Sum(sm => sm.Field<int>("Column13"))
                            })
                            .Where(w => w.Peso > 0)
                            .Select(s2 => new
                            {
                                s2.CodigoLinha,
                                Peso = s2.Peso / listaQtdeVeicDias
                                    .Where(w => w.CodigoLinha.Equals(s2.CodigoLinha) &&
                                                w.Hora.Equals(s))
                                    .SingleOrDefault()
                                    .Qtde
                            })
                            .OrderByDescending(o => o.Peso)
                            .ToArray(),
                    })
                    .ToArray()
                    );

                #endregion

                #region SeriesRankingFaixaHoraria

                var dataSourceFaixaHoraria = _dataTable.Rows
                            .Cast<DataRow>()
                            .GroupBy(g => g.Field<string>("Column2"))
                            .Select(s => new
                            {
                                FaixaHoraria = s.Key,
                                Peso = s.Sum(s1 => s1.Field<int>("Column13"))
                            })
                            .Where(w => w.Peso > 0)
                            .Select(s => new
                            {
                                s.FaixaHoraria,
                                Peso = s.Peso / listaQtdeVeicDias
                                    .Where(w => w.Hora.Equals(s.FaixaHoraria))
                                    .Sum(sm => sm.Qtde)
                            })
                            .OrderBy(o => o.FaixaHoraria)
                            .ToArray();
                userControl1.SeriesRankingFaixaHoraria.Series.Add(
                    new BarStackedSeries2D()
                    {
                        DisplayName = "Peso",
                        ArgumentDataMember = "FaixaHoraria",
                        ValueDataMember = "Peso",
                        ArgumentScaleType = DevExpress.Wpf.Charts.ScaleType.Qualitative,
                        Label = new SeriesLabel() { Visible = false },
                        DataSource = dataSourceFaixaHoraria
                    }
                    );
                userControl1.SeriesRankingFaixaHorariaPie.DataSource = dataSourceFaixaHoraria;

                #endregion

                this.elementHost1.Child = userControl1;

            }
            catch (Exception)
            {

            }

            this.WindowState = FormWindowState.Maximized;


        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {

        }
        
    }
}
