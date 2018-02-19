using System;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Controls;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //ResourceWriter myResource = new ResourceWriter("Images.resources");
            //myResource.AddResource("flash", new Bitmap("flashScreen.png"));
            System.Windows.Controls.Image simpleImage = new System.Windows.Controls.Image();
            simpleImage.Margin = new System.Windows.Thickness(0);

            System.Windows.Media.Imaging.BitmapImage bi = new System.Windows.Media.Imaging.BitmapImage();
            //BitmapImage.UriSource must be in a BeginInit/EndInit block
            bi.BeginInit();





            bi.UriSource = new Uri(@"pack://siteoforigin:,,,/alarm3.png");
            bi.EndInit();
            //set image source
            simpleImage.Source = bi;
            //        simpleImage.Stretch = Stretch.None;
            simpleImage.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            simpleImage.Visibility = System.Windows.Visibility.Hidden;
            simpleImage.Name = "AlarmIndicator";
            simpleImage.Width = 13;

            
            //myResource.AddResource("alarm", new System.Windows.Controls.Image("alarm3.png"));
            //myResource.Close(); 


        }
    }
}
