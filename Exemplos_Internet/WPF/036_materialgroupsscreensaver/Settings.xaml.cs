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
using System.ComponentModel;

namespace MaterialGroupsScreenSaver
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>

    public partial class Settings : System.Windows.Window
    {

        public Settings()
        {
            InitializeComponent();
        }


        void OnLoaded(object sender, EventArgs e)
        {
            //initialize color picker with BackgroundColor application setting
            TypeConverter colorTypeConverter = TypeDescriptor.GetConverter(typeof(Color));
            MyColorPicker.Color = (Color) colorTypeConverter.ConvertFrom(Properties.Settings.Default.BackgroundColor) ;
            
        }

        void CancelSettings(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        void SaveSettings(object sender, EventArgs e)
        {

            //set BackgroundColor application setting to color picker's color
            TypeConverter colorTypeConverter = TypeDescriptor.GetConverter(typeof(Color));
            Properties.Settings.Default.BackgroundColor = colorTypeConverter.ConvertTo(MyColorPicker.Color, typeof(string)) as string;
            
            //flush application settings
            Properties.Settings.Default.Save();

            //shutdown app
            Application.Current.Shutdown();
        }
    }
}