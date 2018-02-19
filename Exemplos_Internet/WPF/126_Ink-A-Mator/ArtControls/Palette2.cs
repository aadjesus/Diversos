/****************************************************************************
 *                                                                          *
 *      Created By: Ernie Booth                                             *
 *      Contact: ebooth@microsoft.com - http://blogs.msdn.com/ebooth        *
 *      Last Modified: 6/23/2006                                            *
 *                                                                          *
 * **************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ArtControls
{
    /// <summary>
    /// ========================================
    /// WinFX Custom Control
    /// ========================================
    ///
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ArtControls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ArtControls;assembly=ArtControls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file. Note that Intellisense in the
    /// XML editor does not currently work on custom controls and its child elements.
    ///
    ///     <MyNamespace:Palette2/>
    ///
    /// </summary>
    public class Palette2 : Control
    {
        static Palette2()
        {
            //This OverrideMetadata call tells the system that this element wants to provide a style that is different than its base class.
            //This style is defined in themes\generic.xaml
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Palette2), new FrameworkPropertyMetadata(typeof(Palette2)));
        }

        /// <summary>
        /// Create the palette.
        /// </summary>
        public Palette2()
        {

            MainPaletteImage = new Rectangle();

            MainPaletteImage.Width = 50;
            MainPaletteImage.Height = 203;

            MainPaletteImage.MouseLeftButtonDown += new MouseButtonEventHandler(OnSelectedColor);

            CreateGradient();          
        }

        public event RoutedEventHandler ColorChanged;

        protected void OnColorChanged(object sender, RoutedEventArgs e)
        {
            if (ColorChanged != null)
            {
                ColorChanged(sender, e);
            }
        }

        /// <summary>
        /// Figures out which color was selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnSelectedColor(object sender, MouseButtonEventArgs e)
        {
            CreatePalette();

            Point p = e.GetPosition(MainPaletteImage);

            int pixelIndex = (int)((int)(p.Y) * MainPaletteImage.Width * 4 + (int)(p.X) * 4);

            byte b = Pixels[pixelIndex];
            byte g = Pixels[pixelIndex + 1];
            byte r = Pixels[pixelIndex + 2];
            

            SelectedColor = new SolidColorBrush(Color.FromRgb(r, g, b));         
        }

        /// <summary>
        /// We use a LinearGradientBrush to create our brush.
        /// </summary>
        private void CreateGradient()
        {
            LinearGradientBrush brush = new LinearGradientBrush();

            brush.StartPoint = new Point(0.5, 0);
            brush.EndPoint = new Point(0.5, 1);

            brush.GradientStops.Add(new GradientStop(Colors.Orange, 0));
            brush.GradientStops.Add(new GradientStop(Colors.Yellow, 0.15));
            brush.GradientStops.Add(new GradientStop(Colors.Green, 0.25));
            brush.GradientStops.Add(new GradientStop(Colors.Blue, 0.5));
            brush.GradientStops.Add(new GradientStop(Colors.Red, 0.75));
            brush.GradientStops.Add(new GradientStop(Colors.Black, 0.9));
            brush.GradientStops.Add(new GradientStop(Colors.White, 1));

            MainPaletteImage.Fill = brush;
        }

        

        byte[] Pixels;
        
        /// <summary>
        /// Create the Palette from the gradient colored visual.
        /// </summary>
        private void CreatePalette()
        {
            if (Pixels == null)
            {
                RenderTargetBitmap rt = new RenderTargetBitmap((int)MainPaletteImage.Width, (int)MainPaletteImage.Height, 96, 96, PixelFormats.Pbgra32);

                rt.Render(MainPaletteImage);

                int bytesPerPixel = 4;
                int stride = (int)MainPaletteImage.Width * bytesPerPixel;

                Pixels = new byte[(int)MainPaletteImage.Height * (int)MainPaletteImage.Width * bytesPerPixel];

                rt.CopyPixels(Pixels, stride, 0);
            }
        }


        public Rectangle MainPaletteImage
        {
            get { return (Rectangle)GetValue(MainPaletteImageProperty); }
            set { SetValue(MainPaletteImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MainPaletteImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainPaletteImageProperty =
            DependencyProperty.Register("MainPaletteImage", typeof(Rectangle), typeof(Palette2), new UIPropertyMetadata(null));





        public SolidColorBrush SelectedColor
        {
            get { return (SolidColorBrush)GetValue(SelectedColorProperty); }
            set 
            { 
                SetValue(SelectedColorProperty, value);
                OnColorChanged(MainPaletteImage, new RoutedEventArgs());
            }
        }

        // Using a DependencyProperty as the backing store for PickedColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(SolidColorBrush), typeof(Palette2), new UIPropertyMetadata(new SolidColorBrush()));



    }
}
