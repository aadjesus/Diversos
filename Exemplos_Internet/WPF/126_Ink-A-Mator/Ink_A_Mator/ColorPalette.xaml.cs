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

namespace Ink_A_Mator
{
    /// <summary>
    /// Interaction logic for ColorPalette.xaml
    /// </summary>

    public partial class ColorPalette : Window
    {

        public ColorPalette()
        {
            InitializeComponent();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;

            this.Hide();
            
            base.OnClosing(e);
        }

        void OnColorChanged(object sender, RoutedEventArgs e)
        {
            ColorChangedEventArgs args = new ColorChangedEventArgs();

            args.Color = this.Palette.SelectedColor.Color;

            MainWindow.ChangeColor.Execute(args, Application.Current.MainWindow);
        }

        void OnColorChanged2(object sender, RoutedEventArgs e)
        {
            ColorChangedEventArgs args = new ColorChangedEventArgs();

            args.Color = this.Palette2.SelectedColor.Color;

            MainWindow.ChangeColor.Execute(args, Application.Current.MainWindow);
        }
    }
}