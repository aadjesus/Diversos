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
    /// Interaction logic for TitleButton.xaml
    /// </summary>
    public partial class WindowTitle : UserControl
    {
        public event ModeChangedEventHandler ModeChanged;

        public WindowTitle()
        {
            InitializeComponent();

            TitleButton.ModeChanged += new ModeChangedEventHandler(TitleButton_ModeChanged);
        }

        void TitleButton_ModeChanged(object sender, ModeChangedEventArgs args)
        {
            if (ModeChanged != null)
            {
                ModeChanged(sender, args);
            }
        }

        private void TitleButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //TitleButton.ToggleMode();
            //if (ModeChanged != null)
            //{
            //    ModeChanged(sender, new ModeChangedEventArgs(TitleButton.Mode));
            //}
        }

        //private void OuterSquare_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    if (ModeChanged != null)
        //    {
        //        ModeChanged(this, new ModeChangedEventArgs(this.Mode));
        //    }
        //}
    }
}
