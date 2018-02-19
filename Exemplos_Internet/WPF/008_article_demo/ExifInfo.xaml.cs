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


namespace WPFExifInfo
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : System.Windows.Window
    {

        public Window1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog objDialog = new Microsoft.Win32.OpenFileDialog();
            objDialog.Filter = "Only JPG File|*.jpg";
            objDialog.ShowDialog();
            textBox1.Text = objDialog.FileName;
            Uri path = new Uri(textBox1.Text);
            BitmapFrame bFrame = BitmapFrame.Create(path);
            ViewedPhoto.Source = bFrame;
            ExifMetaInfo objMetaInfo = new ExifMetaInfo(path);
            lblBrand.Content = objMetaInfo.EquipmentManufacturer;
            lblModelName.Content = objMetaInfo.CameraModel;
            lblAparature.Content = objMetaInfo.LensAperture;
            lblCreation.Content = objMetaInfo.CreationSoftware;
            lblFocalLength.Content = objMetaInfo.FocalLength;
            lblHeight.Content = objMetaInfo.Height;
            lblWidth.Content = objMetaInfo.Width;
            lblISO.Content = objMetaInfo.IsoSpeed;
        }

    }
}