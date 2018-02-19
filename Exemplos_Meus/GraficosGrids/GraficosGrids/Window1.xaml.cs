using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Wpf.Charts;
using System.Data;
using System.Windows.Media.Animation;
using System.Windows.Controls.Primitives;

namespace GraficosGrids
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private DataTable _dataTableOcorrencias = null;
        private SeriesPoint point;
        private const int clickDelta = 200;
        private int mouseDownTime;

        public object DataSourceGraficos
        {
            get
            {
                return this.Grafico2.DataSource;
            }
            set
            {
                {
                    var faixa = ((DataTable)value).Rows.Cast<DataRow>()
                        .GroupBy(g => g.Field<string>("Column10"))
                        .Select(s => new
                        {
                            FaixaEtaria = s.Key,
                            Valor = s.Sum(a => a.Field<Decimal>("Column19"))
                        });

                    this.Grafico2.DataSource = faixa;

                    this.gridControl1.DataSource = ((DataTable)value).Rows
                        .Cast<DataRow>()
                        .Select(s => new
                        {
                            Column1 = s.Field<string>("Column1"),
                            Column2 = s.Field<string>("Column2"),
                            Column3 = s.Field<string>("Column3"),
                            Column4 = s.Field<string>("Column4"),
                            Column5 = s.Field<string>("Column5"),
                            Column6 = s.Field<string>("Column6"),
                            Column7 = s.Field<string>("Column7"),
                            Column8 = s.Field<string>("Column8"),
                            Column9 = s.Field<string>("Column9"),
                            Column10 = s.Field<string>("Column10"),
                            Column11 = s.Field<string>("Column11"),
                            Column12 = s.Field<string>("Column11"),
                            Column13 = s.Field<string>("Column12"),
                            Column14 = s.Field<string>("Column13"),
                            Column15 = s.Field<string>("Column14"),
                            Column16 = s.Field<string>("Column15"),
                            Column17 = s.Field<string>("Column16"),
                            Column18 = s.Field<string>("Column17"),
                            Column19 = s.Field<string>("Column16")
                        })
                        .ToArray();

                    this.pivotGridControl1.DataSource = this.gridControl1;

                    var faixa2 = ((DataTable)value).Rows.Cast<DataRow>()
                        .GroupBy(g => new
                        {
                            FaixaEtaria = g.Field<string>("Column10"),
                            Departamento = g.Field<string>("Column6"),
                        })
                        .Select(s => new
                        {
                            FaixaEtaria = s.Key.FaixaEtaria,
                            Departamento = s.Key.Departamento,
                            Valor = s.Sum(a => a.Field<Decimal>("Column19"))
                        }).ToArray();


                    BarStackedSeries2D[] aa = faixa
                        .Select(s => new BarStackedSeries2D()
                        {
                            DisplayName = s.FaixaEtaria,
                            ArgumentDataMember = "Departamento",
                            ValueDataMember = "Valor",
                            DataSource = faixa2,
                            Name = "Series_",
                            Label = new SeriesLabel() { Visible = false },
                            ArgumentScaleType = ScaleType.Qualitative,
                        }).ToArray();

                    chartContro10MaisCriticas.Diagram.Series.Clear();
                    chartContro10MaisCriticas.Diagram.Series.AddRange(aa);
                    
                }

            }
        }

        private bool IsClick(int mouseUpTime)
        {
            return mouseUpTime - mouseDownTime < clickDelta;
        }

        private void chartControlQtdeVeiculos_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsClick(e.Timestamp))
                return;
            SeriesPoint point = chartControlQtdeVeiculos.HitTest(e.GetPosition(chartControlQtdeVeiculos));
            if (point == null)
                return;
            double distance = PieSeries.GetExplodedDistance(point);
            AnimationTimeline animation = distance > 0 ?
                (AnimationTimeline)TryFindResource("CollapseAnimation") :
                (AnimationTimeline)TryFindResource("ExplodeAnimation");
            point.BeginAnimation(PieSeries.ExplodedDistanceProperty, animation, HandoffBehavior.Compose);
        }

        private void chartControlQtdeVeiculos_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseDownTime = e.Timestamp;
        }

        private void chartControlQtdeVeiculos_QueryCursor(object sender, QueryCursorEventArgs e)
        {
            SeriesPoint currentPoint = chartControlQtdeVeiculos.HitTest(e.GetPosition(chartControlQtdeVeiculos));
            if (currentPoint != null && e.LeftButton == MouseButtonState.Released)
                e.Cursor = Cursors.Hand;
        }

        private void chartContro10MaisCriticas_MouseMove(object sender, MouseEventArgs e)
        {
            SeriesPoint point = chartContro10MaisCriticas.HitTest(e.GetPosition(chartContro10MaisCriticas));
            if (point != null)
            {
                ttContent.Text = string.Format("{0} = {2}",
                    point.Series.DisplayName,
                    point.Argument,
                    point.NonAnimatedValue);
                tooltip.Placement = PlacementMode.Mouse;
                tooltip.IsOpen = true;
                Cursor = Cursors.Hand;
            }
            else
            {
                tooltip.IsOpen = false;
                Cursor = Cursors.Arrow;
            }
        }

        private void chart_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            e.LegendText = ((DataRowView)e.SeriesPoint.Tag)["OfficialName"].ToString();
        }

        private void chartContro10MaisCriticas_MouseLeave(object sender, MouseEventArgs e)
        {
            tooltip.IsOpen = false;
        }

    }
}
