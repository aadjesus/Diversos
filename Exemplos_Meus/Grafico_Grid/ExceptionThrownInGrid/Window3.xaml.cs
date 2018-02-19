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
using System.Windows.Media.Animation;
using System.Data;
using System.Collections.ObjectModel;
using DevExpress.Wpf.Utils.Themes;

namespace ExceptionThrownInGrid
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        const int clickDelta = 200;
        int mouseDownTime;

        public Window3()
        {
            InitializeComponent();
        }

        void chart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseDownTime = e.Timestamp;
        }
        bool IsClick(int mouseUpTime)
        {
            return mouseUpTime - mouseDownTime < clickDelta;
        }
        void chart_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsClick(e.Timestamp))
                return;

            SeriesPoint point = chart.HitTest(e.GetPosition(chart));
            if (point == null)
                return;

            foreach (var point1 in xisto1010.Points)
                point1.BeginAnimation(PieSeries.ExplodedDistanceProperty, (AnimationTimeline)TryFindResource("CollapseAnimation"), HandoffBehavior.Compose);

            double distance = PieSeries.GetExplodedDistance(point);
            AnimationTimeline animation = distance > 0 ?
                (AnimationTimeline)TryFindResource("CollapseAnimation") :
                (AnimationTimeline)TryFindResource("ExplodeAnimation");

            grid1.Visibility = distance > 0 ? Visibility.Collapsed : Visibility.Visible;

            DataView filteredDataView = new DataView(((DataTable)gridControl1.DataContext));
            filteredDataView.RowFilter = "Status = " + point.Argument;
            gridControl1.DataSource = filteredDataView;

            point.BeginAnimation(PieSeries.ExplodedDistanceProperty, animation, HandoffBehavior.Compose);
        }

        void chart_QueryCursor(object sender, QueryCursorEventArgs e)
        {
            SeriesPoint currentPoint = chart.HitTest(e.GetPosition(chart));
            if (currentPoint != null && e.LeftButton == MouseButtonState.Released)
                e.Cursor = Cursors.Hand;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            double distance = 0;
            foreach (var point in xisto1010.Points)
            {
                distance = PieSeries.GetExplodedDistance(point);
                AnimationTimeline animation = distance > 0 ?
                    (AnimationTimeline)TryFindResource("CollapseAnimation") :
                    (AnimationTimeline)TryFindResource("ExplodeAnimation");
                point.BeginAnimation(PieSeries.ExplodedDistanceProperty, animation, HandoffBehavior.Compose);
            }

            //Palette palette = new Palette();
            Color color = (Color)ColorConverter.ConvertFromString(distance > 0 ? "Red" : "Yellow");
            //palette.Brushes.Add(new SolidColorBrush(color));


            if (this.chart.Palette == null)
                this.chart.Palette = new Palette();
            else
                this.chart.Palette.Brushes.Clear();

            chart.Palette.Brushes.Add(new SolidColorBrush(color));
            chart.UpdateLayout();




            //this.chart.Palette = palette;

        }

    }
}
