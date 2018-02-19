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
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;


namespace MarsaX
{
    /// <summary>
    /// The host application Window, which simply hosts a new
    /// <see cref="ucSlider3DViewPort">ucSlider3DViewPort</see>
    /// control and reacts to the ModelSelected event of the 
    /// ucSlider3DViewPort control, byt showing a modal window
    /// with the currently selected image
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Ctor
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// On loaded wire up the ModelSelected event of the ucSlider3DViewPort control
        /// so that we can be notify when a user clicks an image in the ViewPort3D
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            imageResults.ModelSelected += imageResults_ModelSelected;
        }

        /// <summary>
        /// Show a new Window (Modally) with the selected image in it
        /// </summary>
        private void imageResults_ModelSelected(object sender, ModelSelectedEventArgs e)
        {
            SelectedImagePopupWindow win = new SelectedImagePopupWindow();
            win.Owner = this;
            win.ShowInTaskbar = false;
            win.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            BitmapImage bmpImage = new BitmapImage(new Uri(e.ImageUrl, UriKind.RelativeOrAbsolute));
            win.CurrentImageSource = bmpImage;

            win.ShowDialog();


        }
        #endregion
    }
}
