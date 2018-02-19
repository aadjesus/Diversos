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

namespace AnaglyphImage
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            // Could also add the visual brush as:
            //Effect1.LeftInput = new VisualBrush(LeftCanvas);
        }

        Point lastPosition;
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(RightImage);
            lastPosition=e.GetPosition(this);
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.Captured == RightImage)
            {
                Point newPosition = e.GetPosition(this);
                Vector delta=newPosition - lastPosition;
                double left=Canvas.GetLeft(RightImage);
                double top=Canvas.GetTop(RightImage);
                Canvas.SetLeft(RightImage, left + delta.X);
                // Canvas.SetTop(RightImage, top + delta.Y);
                lastPosition = newPosition;
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            ReleaseMouseCapture();
        }
    }
}
