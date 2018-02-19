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
    /// Palette Control:
    /// Allows user to pick a color from the gradient color palette. For this palette we do all of the color 
    /// interpolation by hand so that we can do four color blends and a fun palette.
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
    ///     <MyNamespace:Palette/>
    ///
    /// </summary>
    public class Palette : Control
    {
        static Palette()
        {
            //This OverrideMetadata call tells the system that this element wants to provide a style that is different than its base class.
            //This style is defined in themes\generic.xaml
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Palette), new FrameworkPropertyMetadata(typeof(Palette)));
        }

        /// <summary>
        /// Create our Palette.
        /// </summary>
        public Palette()
        {
            // This is the setup for the colors that define the four corners of the four color blend.
            upperLeftColor = Brushes.Red;
            upperRightColor = Brushes.Blue;
            lowerLeftColor = Brushes.Yellow;
            lowerRightColor = Brushes.Green;

            // Create the BitmapSource and paint it for the palette.
            MainPalette = CreateMasterPalette();
            FourColorPalette = CreateFourColorPalette();

            // Create our Image controls.  We need them here because we do mouse position look up on them.
            MainPaletteImage = new Image();
            FourColorPaletteImage = new Image();

            MainPaletteImage.MouseLeftButtonDown += new MouseButtonEventHandler(OnSelectedMainPalette);
            FourColorPaletteImage.MouseLeftButtonDown += new MouseButtonEventHandler(OnSelectedFourColorPalette);

            MainPaletteImage.Source = MainPalette;
            FourColorPaletteImage.Source = FourColorPalette;

            // We need to update what our source is.
            CornerControlClick = new RoutedCommand("RadioButtonClick", typeof(Palette));

            CommandBinding radioButtionHandler = new CommandBinding(CornerControlClick);

            radioButtionHandler.CanExecute += new CanExecuteRoutedEventHandler(SetCanExecuteTrue);
            radioButtionHandler.Executed += new ExecutedRoutedEventHandler(OnFourColorCornerControlHit);

            this.CommandBindings.Add(radioButtionHandler);

        }

        /// <summary>
        /// We use Tags in our generic.xaml file to define which control is responsible for which corner of the four color palette.
        /// </summary>
        string ActiveColorCorner = "UpperLeft";

        /// <summary>
        /// This handles figuring out which of the corner controls is active.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnFourColorCornerControlHit(object sender, ExecutedRoutedEventArgs e)
        {
            FrameworkElement fe = e.OriginalSource as FrameworkElement;

            if (fe == null)
                return;

            ActiveColorCorner = fe.Tag.ToString();           
        }

        /// <summary>
        /// Update the correct brush based on which is the active corner control.
        /// </summary>
        /// <param name="brush"></param>
        void UpdateColor(SolidColorBrush brush)
        {
            switch (ActiveColorCorner)
            {
                case "UpperRight":
                    upperRightColor = brush;
                    break;
                case "UpperLeft":
                    upperLeftColor = brush;
                    break;
                case "LowerRight":
                    lowerRightColor = brush;
                    break;
                case "LowerLeft":
                    lowerLeftColor = brush;
                    break;
                default:
                    break;
            }

            // Recreate our palette and assign it to our image control.
            FourColorPalette = CreateFourColorPalette();
            FourColorPaletteImage.Source = FourColorPalette;
        }

        /// <summary>
        /// Sets CanExecute = true;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SetCanExecuteTrue(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

     
        /// <summary>
        /// Our pixels that we index into when a palette is clicked on. They are also used to create the Bitmap Source.
        /// </summary>
        Byte[] pixels, masterPixels;

        /// <summary>
        /// The size of our four color palette.  We need to have a specific size because we index into the palette. 
        /// Resizing would cause the lookup to work incorrectly so don't put the palettes in some control that is going to 
        /// automatically size them.
        /// </summary>
        Size FourColorPaletteSize = new Size(203, 203);

        /// <summary>
        /// Our Master palette size is important.  While it's width is arbitrary it's height must result in (Height Mod 7 == 0) or it will not 
        /// draw correctly.  For example 104 % 7 == 6 which means that we will have blank lines in our palette.
        /// </summary>
        Size MasterPaletteSize = new Size(25, 105);

        /// <summary>
        /// The number of Bytes Per Pixel we use.  Since we use RGB24 bit color, thats 8 bits per color we have 3 bytes per pixel.
        /// </summary>
        int bytesPerPixel = 3;

        /// <summary>
        /// We need to alter the outside world when the color changes.
        /// </summary>
        public event RoutedEventHandler ColorChanged;

        protected void OnColorChanged(object sender, RoutedEventArgs e)
        {
            if (ColorChanged != null)
            {
                ColorChanged(sender, e);
            }
        }

        /// <summary>
        /// The property that tells the outside world what the current selected color is.
        /// </summary>
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
            DependencyProperty.Register("SelectedColor", typeof(SolidColorBrush), typeof(Palette), new UIPropertyMetadata(new SolidColorBrush()));

        /// <summary>
        /// Figure out which color was selected in the four color palette.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnSelectedFourColorPalette(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(FourColorPaletteImage);

            int pixelIndex = (int)((int)(p.Y) * FourColorPaletteSize.Width * bytesPerPixel + (int)(p.X) * bytesPerPixel);

            byte r = pixels[pixelIndex];
            byte g = pixels[pixelIndex + 1];
            byte b = pixels[pixelIndex + 2];

            SelectedColor = new SolidColorBrush(Color.FromRgb(r, g, b));           
        }

        /// <summary>
        /// Figure out which color was picked in our main palette.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnSelectedMainPalette(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(MainPaletteImage);

            int pixelIndex = (int)((int)(p.Y) * MasterPaletteSize.Width * bytesPerPixel + (int)(p.X) * bytesPerPixel);

            byte r = masterPixels[pixelIndex];
            byte g = masterPixels[pixelIndex + 1];
            byte b = masterPixels[pixelIndex + 2];
            
            // We need to update our four color palette with this choice along with the corner controls.
            UpdateColor(new SolidColorBrush(Color.FromRgb(r, g, b)));
        }

        /// <summary>
        /// Command for when one of the corner controls is clicked.
        /// </summary>
        public RoutedCommand CornerControlClick
        {
            get { return (RoutedCommand)GetValue(CornerControlClickProperty); }
            set { SetValue(CornerControlClickProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RadioButtonClick.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerControlClickProperty =
            DependencyProperty.Register("CornerControlClick", typeof(RoutedCommand), typeof(Palette), new UIPropertyMetadata(null));


        /// <summary>
        /// Create Dependency Properties for the four corner control brushes.
        /// </summary>
        #region CornerColor Brushes

        public SolidColorBrush upperLeftColor
        {
            get { return (SolidColorBrush)GetValue(upperLeftColorProperty); }
            set { SetValue(upperLeftColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for upperLeftColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty upperLeftColorProperty =
            DependencyProperty.Register("upperLeftColor", typeof(SolidColorBrush), typeof(Palette), new UIPropertyMetadata(null));

        public SolidColorBrush upperRightColor
        {
            get { return (SolidColorBrush)GetValue(upperRightColorProperty); }
            set { SetValue(upperRightColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for upperRightColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty upperRightColorProperty =
            DependencyProperty.Register("upperRightColor", typeof(SolidColorBrush), typeof(Palette), new UIPropertyMetadata(null));

        public SolidColorBrush lowerLeftColor
        {
            get { return (SolidColorBrush)GetValue(lowerLeftColorProperty); }
            set { SetValue(lowerLeftColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for lowerLeftColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty lowerLeftColorProperty =
            DependencyProperty.Register("lowerLeftColor", typeof(SolidColorBrush), typeof(Palette), new UIPropertyMetadata(null));

        public SolidColorBrush lowerRightColor
        {
            get { return (SolidColorBrush)GetValue(lowerRightColorProperty); }
            set { SetValue(lowerRightColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for lowerRightColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty lowerRightColorProperty =
            DependencyProperty.Register("lowerRightColor", typeof(SolidColorBrush), typeof(Palette), new UIPropertyMetadata(null));
        #endregion

        BitmapSource mainPalette;

        public BitmapSource MainPalette
        {
            get { return mainPalette; }
            set { mainPalette = value; }
        }

        BitmapSource fourColorPalette;

        public BitmapSource FourColorPalette
        {
            get { return fourColorPalette; }
            set { fourColorPalette = value; }
        }

        public Image MainPaletteImage
        {
            get { return (Image)GetValue(MainPaletteImageProperty); }
            set { SetValue(MainPaletteImageProperty, value); }
        }
        
        // Using a DependencyProperty as the backing store for MainPaletteImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainPaletteImageProperty =
            DependencyProperty.Register("MainPaletteImage", typeof(Image), typeof(Palette), new UIPropertyMetadata(null));

        public Image FourColorPaletteImage
        {
            get { return (Image)GetValue(FourColorPaletteImageProperty); }
            set { SetValue(FourColorPaletteImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FourColorPaletteImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FourColorPaletteImageProperty =
            DependencyProperty.Register("FourColorPaletteImage", typeof(Image), typeof(Palette), new UIPropertyMetadata(null));

        /// <summary>
        /// Our structure for dealing with pixels
        /// </summary>
        struct Pixel
        {
            public Pixel(byte r, byte g, byte b)
            {
                this.r = r;
                this.g = g;
                this.b = b;
            }

            public byte r;
            public byte g;
            public byte b;
        }

        /// <summary>
        /// Our Structure for dealing with Pixels, but in double form.  We use this when doing linear interpolation for gradient creation.
        /// </summary>
        struct PixelAsDoubles
        {
            public PixelAsDoubles(double r, double g, double b)
            {
                this.r = r;
                this.g = g;
                this.b = b;
            }

            public double r;
            public double g;
            public double b;
        }

        /*
         *  Color Conversions
         *  RGB - > CMYK
            white = red + green + blue 
            white - green = blue + red = magenta 
            white - red = blue + green = cyan 
            white - blue = green + red = yellow 

            CMYK - > RGB
            Magenta + cyan = blue
            yellow _ cyan = green
            magenta + yellow = red
            magentat + yellow + cyan = black
         * */

      
        
        /// <summary>
        /// Create the four color palette by interpolating from the corners.
        /// Red -> Blue
        ///  ^       ^
        ///  |       |
        /// Green -> White
        /// </summary>
        /// <returns></returns>
        BitmapSource CreateFourColorPalette()
        {
            // Create a place to hold our pixel data.
            pixels = new Byte[(int)(FourColorPaletteSize.Width * FourColorPaletteSize.Height * bytesPerPixel)];

            int i = 0;

            // Find Color Slopes
            Pixel upperLeft = new Pixel(upperLeftColor.Color.R, upperLeftColor.Color.G, upperLeftColor.Color.B);
            Pixel upperRight = new Pixel(upperRightColor.Color.R, upperRightColor.Color.G, upperRightColor.Color.B);
            Pixel lowerLeft = new Pixel(lowerLeftColor.Color.R, lowerLeftColor.Color.G, lowerLeftColor.Color.B);
            Pixel lowerRight = new Pixel(lowerRightColor.Color.R, lowerRightColor.Color.G, lowerRightColor.Color.B);

            // Get the change in color for the distance the color is traveling
            PixelAsDoubles LeftToRightTop = CalculatePixelSlope(upperLeft, upperRight, (int)FourColorPaletteSize.Width);
            PixelAsDoubles LeftToRightBottom = CalculatePixelSlope(lowerLeft, lowerRight, (int)FourColorPaletteSize.Width);

            // We interpolate from left to right and then top to bottom.
            for (int y = 0; y < FourColorPaletteSize.Height; y++)
            {
                for (int x = 0; x < FourColorPaletteSize.Width; x++)
                {
                    Pixel p1, p2;

                    p1 = CalculatePixelGradient(upperLeft, x, LeftToRightTop);
                    p2 = CalculatePixelGradient(lowerLeft, x, LeftToRightBottom);

                    PixelAsDoubles topToBottom = CalculatePixelSlope(p1, p2, (int)FourColorPaletteSize.Height);

                    Pixel p = CalculatePixelGradient(p1, y, topToBottom);

                    pixels[i++] = p.r;
                    pixels[i++] = p.g;
                    pixels[i++] = p.b;
                }
            }
          
            // Figure out the stride.
            int stride = (int)FourColorPaletteSize.Width * bytesPerPixel;

            return BitmapImage.Create((int)FourColorPaletteSize.Width, (int)FourColorPaletteSize.Height, 96, 96, PixelFormats.Rgb24, null, pixels, stride);
        }

        /// <summary>
        /// Create the Master Palette using the same color interpolation to create the gradients as the four color palette.
        /// </summary>
        /// <returns></returns>
        BitmapSource CreateMasterPalette()
        {
            masterPixels = new Byte[(int)(MasterPaletteSize.Width * MasterPaletteSize.Height * bytesPerPixel)];

            int i = 0;

            // Red -> Blue
            //  ^       ^
            //  |       |
            // Green -> White


            // We want to use 8 colors for this palette.
            Pixel g1 = new Pixel(255, 255, 0);  // yellow
            Pixel g2 = new Pixel(0, 255, 0);    // green
            Pixel g3 = new Pixel(0, 0, 255);    // blue
            Pixel g4 = new Pixel(255, 0, 0);    // red
            Pixel g5 = new Pixel(255, 0, 255);  // magenta
            Pixel g6 = new Pixel(0, 255, 255);  // cyan 
            Pixel g7 = new Pixel(0, 0, 0);      // black
            Pixel g8 = new Pixel(255, 255, 255);// white


            // Get the change in color for the distance the color is traveling.
            // mess with the g parameters here to change how the palette is layed out.
            PixelAsDoubles s1 = CalculatePixelSlope(g8, g1, (int)MasterPaletteSize.Width);
            PixelAsDoubles s2 = CalculatePixelSlope(g2, g1, (int)MasterPaletteSize.Width);
            PixelAsDoubles s3 = CalculatePixelSlope(g2, g3, (int)MasterPaletteSize.Width);
            PixelAsDoubles s4 = CalculatePixelSlope(g4, g3, (int)MasterPaletteSize.Width);
            PixelAsDoubles s5 = CalculatePixelSlope(g4, g5, (int)MasterPaletteSize.Width);
            PixelAsDoubles s6 = CalculatePixelSlope(g6, g5, (int)MasterPaletteSize.Width);
            PixelAsDoubles s7 = CalculatePixelSlope(g6, g7, (int)MasterPaletteSize.Width);
            PixelAsDoubles s8 = CalculatePixelSlope(g8, g7, (int)MasterPaletteSize.Width);


            int gradientHeight = (int)MasterPaletteSize.Height / 7;
            int yOffset = 0;

            // We again go left to right then top to bottom.  But we
            // need to do a group of gradients at a time.
            for (int y = 0; y < MasterPaletteSize.Height; y++)
            {
                for (int x = 0; x < MasterPaletteSize.Width; x++)
                {
                    Pixel p1, p2;

                    if (y < gradientHeight)
                    {
                        p1 = CalculatePixelGradient(g1, x, s1);
                        p2 = CalculatePixelGradient(g2, x, s2);
                    }
                    else if (y < gradientHeight * 2)
                    {
                        p1 = CalculatePixelGradient(g2, x, s2);
                        p2 = CalculatePixelGradient(g3, x, s3);
                        yOffset = gradientHeight;
                    }
                    else if (y < gradientHeight * 3)
                    {
                        p1 = CalculatePixelGradient(g3, x, s3);
                        p2 = CalculatePixelGradient(g4, x, s4);
                        yOffset = gradientHeight * 2;
                    }
                    else if (y < gradientHeight * 4)
                    {
                        p1 = CalculatePixelGradient(g4, x, s4);
                        p2 = CalculatePixelGradient(g5, x, s5);
                        yOffset = gradientHeight * 3;
                    }
                    else if (y < gradientHeight * 5)
                    {
                        p1 = CalculatePixelGradient(g5, x, s5);
                        p2 = CalculatePixelGradient(g6, x, s6);
                        yOffset = gradientHeight * 4;
                    }
                    else if (y < gradientHeight * 6)
                    {
                        p1 = CalculatePixelGradient(g6, x, s6);
                        p2 = CalculatePixelGradient(g7, x, s7);
                        yOffset = gradientHeight * 5;
                    }
                    else
                    {
                        p1 = CalculatePixelGradient(g7, x, s7);
                        p2 = CalculatePixelGradient(g7, x, s8);
                        yOffset = gradientHeight * 6;
                    }

                    PixelAsDoubles topToBottom = CalculatePixelSlope(p1, p2, (int)gradientHeight);

                    // Calculate top to bottom gradients.
                    Pixel p = CalculatePixelGradient(p1, y - yOffset, topToBottom);

                    // RGB order is very important.
                    masterPixels[i++] = p.r;
                    masterPixels[i++] = p.g;
                    masterPixels[i++] = p.b;
                }
            }

            // Calculate stride.
            int stride = (int)MasterPaletteSize.Width * bytesPerPixel;

            // Create the BitmapSource from the pixels.
            return BitmapImage.Create((int)MasterPaletteSize.Width, (int)MasterPaletteSize.Height, 96, 96, PixelFormats.Rgb24, null, masterPixels, stride);
        }

        
        /// <summary>
        /// Get the right color for how far we have moved (distance) to the right or down.
        /// </summary>
        /// <param name="startingPixel">What is the starting color</param>
        /// <param name="distance">How many pixels to the right or down this pixel is.</param>
        /// <param name="slope">The color step for this gradient.</param>
        /// <returns></returns>
        private static Pixel CalculatePixelGradient(Pixel startingPixel, int distance, PixelAsDoubles slope)
        {
            Pixel p = new Pixel();

            // We don't want to start at zero so shift.
            distance++;

            p.r = (byte)(startingPixel.r + slope.r * distance);
            p.g = (byte)(startingPixel.g + slope.g * distance);
            p.b = (byte)(startingPixel.b + slope.b * distance);

            return p;
        }

        /// <summary>
        /// Calculated the change needed for each step in 
        /// </summary>
        /// <param name="p1">Pixel 1</param>
        /// <param name="p2">Pixel 2</param>
        /// <param name="distance">The distance between the pixels</param>
        /// <returns></returns>
        PixelAsDoubles CalculatePixelSlope(Pixel p1, Pixel p2, int distance)
        {
            return new PixelAsDoubles((double)(p2.r - p1.r) / distance, (double)(p2.g - p1.g) / distance, (double)(p2.b - p1.b) / distance);
        }

    }
}
