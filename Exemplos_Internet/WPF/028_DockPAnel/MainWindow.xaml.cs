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

namespace FlyingWindows
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton && (sender as RadioButton).Tag != null)
            {
                MainContainer.LayoutPattern = (WindowsContainer.WindowLayoutPattern)Int32.Parse((sender as RadioButton).Tag.ToString());
            }
        }

        private void SidePanelWidth_TextChanged(object sender, TextChangedEventArgs e)
        {
            double width = 35.00D;
            Double.TryParse(SidePanelWidth.Text, out width);
            if (MainContainer != null)
            {
                MainContainer.SidePanelWidth = width;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Size size = new Size(MainGrid.RenderSize.Width, MainContainerContainerRow.ActualHeight);
            MainContainer.Resize(size);
        }

       
    }
}
