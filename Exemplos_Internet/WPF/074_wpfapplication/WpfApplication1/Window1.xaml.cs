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
using System.Runtime.InteropServices;
using CustomToolTip;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            state = true;
            customToolTip.Visibility = Visibility.Hidden;
        }

        bool state = true;
        Random rand = new Random(DateTime.Now.Millisecond);

        private void rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (state)
            {
                customToolTip.Visibility = Visibility.Visible;
                customToolTip.UserControlToolTipTitle = (sender as Rectangle).Name.ToUpperInvariant();
                customToolTip.UserControlTextBlockToolTip = "";
                for (int i = 0; i < rand.Next(1, 30); i++)
                    customToolTip.UserControlTextBlockToolTip += (sender as Rectangle).Name + "\t" + i.ToString() + "\n";
            }
            customToolTip.UserControlToolTipX = Mouse.GetPosition(this).X + 10;
            customToolTip.UserControlToolTipY = Mouse.GetPosition(this).Y + 10;
            state = false;
        }
    }
}
