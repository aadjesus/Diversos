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

namespace SeeThru
{
    /// <summary>
    /// Interaction logic for CrazyWindow.xaml
    /// </summary>

    public partial class CrazyWindow : System.Windows.Window
    {

        public CrazyWindow()
        {
            InitializeComponent();
        }

        public void DragWindow(object sender, MouseButtonEventArgs args)
        {
            DragMove();
        }
     
        public void WindowClicked(object sender, MouseButtonEventArgs args)
        {
            Close();
        }
    }
}