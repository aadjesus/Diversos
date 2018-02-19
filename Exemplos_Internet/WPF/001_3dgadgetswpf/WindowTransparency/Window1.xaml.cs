using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using System.Windows.Media.Animation;

//
namespace WindowTransparency
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : Window
    {

        public Window1()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(Window1_Loaded);
            MouseLeftButtonDown += new MouseButtonEventHandler(Window1_MouseLeftButtonDown);
            mainViewport.MouseLeftButtonDown += new MouseButtonEventHandler(mainViewport_MouseLeftButtonDown);
        }

        void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard s;

            s = (Storyboard)this.FindResource("RotateStoryboard");
            this.BeginStoryboard(s);
        }

        void mainViewport_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            System.Windows.Point mouseposition = e.GetPosition(mainViewport);
            Point3D testpoint3D = new Point3D(mouseposition.X, mouseposition.Y, 0);
            Vector3D testdirection = new Vector3D(mouseposition.X, mouseposition.Y, 10);
            PointHitTestParameters pointparams = new PointHitTestParameters(mouseposition);
            RayHitTestParameters rayparams = new RayHitTestParameters(testpoint3D, testdirection);

            VisualTreeHelper.HitTest(mainViewport, null, HTResult, pointparams);

        }

        private HitTestResultBehavior HTResult(System.Windows.Media.HitTestResult rawresult)
        {
            RayHitTestResult rayResult = rawresult as RayHitTestResult;
            if (rayResult != null)
            {

                if (cube == rayResult.ModelHit)
                {

                    string targetURL = @"http://www.codeproject.com";
                    System.Diagnostics.Process.Start(targetURL);
                }

                if (cross == rayResult.ModelHit)
                {
                    this.Close();
                }

                if (pyramid == rayResult.ModelHit)
                {

                    DragMove();
                }


            }


            return HitTestResultBehavior.Stop;
        }


        void Window1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}