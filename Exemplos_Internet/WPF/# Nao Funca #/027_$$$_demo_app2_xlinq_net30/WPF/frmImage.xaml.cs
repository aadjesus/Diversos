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

namespace ImageTest
{
    /// <summary>
    /// Interaction logic for frmImage.xaml
    /// </summary>

    public partial class frmImage : System.Windows.Window
    {
        private ImageSource currentImageURL = null;
        private int max_Image_Size=250;

        public frmImage()
        {
            InitializeComponent();
            this.Loaded +=new RoutedEventHandler(frmImage_Loaded);
            this.SizeChanged +=new SizeChangedEventHandler(frmImage_SizeChanged);
            this.StateChanged+=new EventHandler(frmImage_StateChanged);
        }

        /// <summary>
        /// Image property, set by frmLoader button clicked
        /// </summary>
        public ImageSource CurrentImageURL
        {
            get { return currentImageURL; }
            set { currentImageURL = value; }
        }

        private void frmImage_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                max_Image_Size = 450;
                scaleImage(max_Image_Size);
            }
            else
            {
                max_Image_Size = 250;
                scaleImage(max_Image_Size);
            }
        }

        private void frmImage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.pnlBottom.Width = this.RenderSize.Width;
        }

        private void frmImage_Loaded(object sender, RoutedEventArgs e)
        {
            this.currentImage.Source = currentImageURL;
            this.currentImage.Height = (int)this.currentImage.Source.Height;
            this.currentImage.Width = (int)this.currentImage.Source.Width;
            this.pnlBottom.Width = this.Width;
            scaleImage(max_Image_Size);
            this.opSlider.Value = 1.0;
        }

        private void scaleImage(int limit)
        {
            double width = this.currentImage.Width;
            double height = this.currentImage.Width;
            double scaleFactor = 1.0;

            if (width == height || width > width)
            {
                scaleFactor = limit / width;
            }
            if (height > width)
            {
                scaleFactor = limit / height;
            }

            this.currentImage.Height = this.currentImage.Height * scaleFactor;
            this.currentImage.Width = this.currentImage.Width * scaleFactor;
        }
    }
}