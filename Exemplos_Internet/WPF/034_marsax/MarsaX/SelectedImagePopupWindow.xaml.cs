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
using System.Windows.Shapes;
using Microsoft.Win32;

namespace MarsaX
{
    /// <summary>
    /// Allows user to view and save the current image
    /// </summary>
    public partial class SelectedImagePopupWindow : Window
    {

        #region Public Properties
        /// <summary>
        /// The BitmapImage to use for the contained Image
        /// </summary>
        public BitmapImage CurrentImageSource
        {
            set 
            {
                selectedImage.Source = value;
            }
            get
            {
                return selectedImage.Source as BitmapImage;
            }
        }
        #endregion

        #region Ctor
        public SelectedImagePopupWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Allows the user to pick a file name and location to 
        /// save the selected image to
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = @Constants.savedImageLocation;
            sfd.Filter = "Jpg files (*.jpg)|*.jpg|Jpg files (*.jpeg)|*.jpeg|Bmp files (*.bmp)|*.bmp" +
                         "|Png files (*.png)|*.png|Tif files (*.tif)|*.tif|Tif files (*.tiff)|*.tiff" +
                         "|Gif files (*.gif)|*.gif|Wmp files (*.wmp)|*.wmp|All files (*.*)|*.*";
            if (sfd.ShowDialog(this) == true)
            {
                if (sfd.FileName != string.Empty)
                {
                    string errorMessage;
                    //TODO : need to make save path available in App.Config
                    if (ImageHelper.SaveImageToDisk(CurrentImageSource.UriSource.AbsoluteUri, sfd.FileName, out errorMessage))
                    {
                        MessageBox.Show("Sucessfully saved file", "Save Ok",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                        MessageBox.Show(errorMessage, "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                    MessageBox.Show("You need to enter a filename", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
