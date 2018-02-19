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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Reflection;
using DevExpress.Data.PivotGrid;
using DevExpress.Wpf.Charts;
using System.Windows.Media.Animation;
using System.Windows.Controls.Primitives;

namespace GraficosGrids
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private int _mouseDownTime;
        private const int _clickDelta = 200;

        private void chartContro10MaisCriticas_MouseMove(object sender, MouseEventArgs e)
        {
            SeriesPoint point = ((ChartControl)sender).HitTest(e.GetPosition(((ChartControl)sender)));
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

        private void chartContro10MaisCriticas_MouseLeave(object sender, MouseEventArgs e)
        {
            tooltip.IsOpen = false;
        }

        private void chart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _mouseDownTime = e.Timestamp;
        }

        private void chart_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsClick(e.Timestamp))
                return;
            SeriesPoint point = chrtCte.HitTest(e.GetPosition(chrtCte));
            if (point == null)
                return;

            double distance = PieSeries3D.GetExplodedDistance(point);
            AnimationTimeline animation = distance > 0 ?
                (AnimationTimeline)TryFindResource("CollapseAnimation") :
                (AnimationTimeline)TryFindResource("ExplodeAnimation");

            point.BeginAnimation(PieSeries3D.ExplodedDistanceProperty, animation, HandoffBehavior.Compose);
        }

        private void chart_GetCursor(object sender, QueryCursorEventArgs e)
        {
            SeriesPoint currentPoint = chrtCte.HitTest(e.GetPosition(chrtCte));
            if (currentPoint != null && e.LeftButton == MouseButtonState.Released)
                e.Cursor = Cursors.Hand;
        }

        bool IsClick(int mouseUpTime)
        {
            return mouseUpTime - _mouseDownTime < _clickDelta;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            dockLayoutManager1.DockController.Close(aaaaa);

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            dockLayoutManager1.DockController.Restore(aaaaa);
        }

    }
}