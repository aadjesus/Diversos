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
using System.Windows.Media.Media3D;

namespace Cube3D
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

        private void StereoBase_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (LeftCamera == null) return;
            LeftCamera.Position = new Point3D(-StereoBase.Value / 2, LeftCamera.Position.Y, LeftCamera.Position.Z);
            RightCamera.Position = new Point3D(StereoBase.Value / 2, RightCamera.Position.Y, RightCamera.Position.Z);
        }
    }
}
