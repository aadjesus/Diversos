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
using System.Drawing;

namespace WpfBrowserCube
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();

            KnownColor t = new KnownColor();
            foreach (KnownColor kc in System.Enum.GetValues(t.GetType()))
            {
                System.Drawing.ColorConverter cc = new System.Drawing.ColorConverter();
                System.Drawing.Color c = System.Drawing.Color.FromName(kc.ToString());

                if (!c.IsSystemColor)
                    cbColors.Items.Add(c);
            }

            cbColors.SelectedItem = System.Drawing.Color.Tomato;
            cubeControl1.Render();

        }

        private void rotateX_ValueChanged(object sender, 
            RoutedPropertyChangedEventArgs<double> e)
        {
            cubeControl1.RotateX(e.NewValue);
        }

        private void rotationY_ValueChanged(object sender, 
            RoutedPropertyChangedEventArgs<double> e)
        {
            cubeControl1.RotateY(e.NewValue);
        }

        private void rotationZ_ValueChanged(object sender, 
            RoutedPropertyChangedEventArgs<double> e)
        {
            cubeControl1.RotateZ(e.NewValue);
        }

        private void cbColors_SelectionChanged(object sender, 
            SelectionChangedEventArgs e)
        {
            System.Drawing.Color color = (System.Drawing.Color)cbColors.SelectedItem;

            cubeControl1.CubeColor = System.Windows.Media.Color.FromRgb(
                color.R, color.G, color.B);
            cubeControl1.Render();

        }
    }
}
