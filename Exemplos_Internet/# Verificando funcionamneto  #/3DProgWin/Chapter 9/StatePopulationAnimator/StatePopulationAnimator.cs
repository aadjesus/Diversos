//--------------------------------------------------------
// StatePopulationAnimator.cs (c) 2007 by Charles Petzold
//--------------------------------------------------------
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Resources;
using System.Xml.Serialization;
using Petzold.Media3D;

namespace Petzold.StatePopulationAnimator
{
    public partial class StatePopulationAnimator: Page
    {
        StatePopulationData data;
        Cylinder[] cyls;

        // Constructor.
        public StatePopulationAnimator()
        {
            // Get XML data.
            Uri uri = new Uri("pack://application:,,/StatePopulationData.xml");
            StreamResourceInfo info = Application.GetResourceStream(uri);
            XmlSerializer xml = new XmlSerializer(typeof(StatePopulationData));
            data = xml.Deserialize(info.Stream) as StatePopulationData;
            info.Stream.Close();

            // Initialization will cause slider event.
            InitializeComponent();
        }

        // Event handler for slider.
        void YearSliderOnValueChanged(object sender, 
                                RoutedPropertyChangedEventArgs<double> args)
        {
            int year = (int)Math.Round(args.NewValue);
            txtblkYear.Text = year.ToString();
            bool willNormalize = false;

            if (chkboxNormalize != null)
                willNormalize = (bool)chkboxNormalize.IsChecked;

            Recalc(year, willNormalize);
        }

        // Event handler for CheckBox.
        void NormalizeCheckBoxOnChecked(object sender, RoutedEventArgs args)
        {
            Recalc((int)Math.Round(sliderYear.Value), 
                                (bool)chkboxNormalize.IsChecked);
        }

        // Calculates heights of cylinders.
        void Recalc(int year, bool willNormalize)
        {
            // Create cylinders for first time.
            if (cyls == null)
            {
                cyls = new Cylinder[50];

                for (int i = 0; i < data.States.Count; i++)
                {
                    State state = data.States[i];
                    cyls[i] = new Cylinder();
                    cyls[i].Material = new DiffuseMaterial(Brushes.Pink);

                    double x = 1 - state.Longitude / 90;
                    double z = -state.Latitude / 90;

                    cyls[i].Radius1 = cyls[i].Radius2 = 0.005;
                    cyls[i].Point1 = new Point3D(x, 0, z);
                    cyls[i].Point2 = new Point3D(x, 0, z);

                    viewport3d.Children.Add(cyls[i]);
                }
            }

            int popTotal = 0;

            // Find total population for normalization.
            if (willNormalize)
            {
                for (int i = 0; i < data.States.Count; i++)
                    popTotal += InterpolatePopulation(i, year);
            }

            // Redo cylinder Point2 values with data.
            for (int i = 0; i < data.States.Count; i++)
            {
                int pop = InterpolatePopulation(i, year);
                double y;

                if (willNormalize)
                {
                    y = 10.0 * pop / popTotal;
                    cyls[i].Name = String.Format("{0}\n{1:P0}",
                                                 data.States[i].Name, 
                                                 (double)pop / popTotal);
                }
                else
                {
                    y = pop / 10000000.0;
                    cyls[i].Name = String.Format("{0}\n{1:N0}", 
                                                 data.States[i].Name, pop);
                }

                Point3D point = cyls[i].Point2;
                point.Y = y;
                cyls[i].Point2 = point;
            }
        }

        // Returns interpolated population for iState (0 to 49)
        //  and year (1790 through 2000).
        int InterpolatePopulation(int iState, int year)
        {
            State state = data.States[iState];
            int i1 = (year - 1790) / 10;
            int i2 = (year - 1781) / 10;
            double weight = (year % 10) / 10.0;
            double pop = (1 - weight) * state.Populations[i1].Count +
                            weight * state.Populations[i2].Count;
            return (int)pop;
        }

        // Monitor MouseMove messages to display fake tool tips.
        protected override void OnPreviewMouseMove(MouseEventArgs args)
        {
            txtblkTip.Visibility = Visibility.Hidden;
            Point ptMouse = args.GetPosition(viewport3d);
            HitTestResult result =
                            VisualTreeHelper.HitTest(viewport3d, ptMouse);

            if (result is RayMeshGeometry3DHitTestResult)
            {
                RayMeshGeometry3DHitTestResult result3d =
                    result as RayMeshGeometry3DHitTestResult;

                if (result3d.VisualHit is Cylinder)
                {
                    Canvas.SetLeft(txtblkTip, ptMouse.X + 12);
                    Canvas.SetTop(txtblkTip, ptMouse.Y + 12);
                    txtblkTip.Text = (result3d.VisualHit as Cylinder).Name;
                    txtblkTip.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
