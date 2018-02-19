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
    /// Interaction logic for WindowTitleButton.xaml
    /// </summary>
    public partial class WindowTitleButton : UserControl
    {
        public event ModeChangedEventHandler ModeChanged;

        public WindowTitleButton()
        {
            InitializeComponent();

            Mode = WindowsContainer.WindowLayoutMode.Tiled;
        }

        WindowsContainer.WindowLayoutMode _Mode;

        public WindowsContainer.WindowLayoutMode Mode
        {
            get { return _Mode; }
            set
            {
                _Mode = value;

                switch (value)
                {
                    case WindowsContainer.WindowLayoutMode.Detailed:
                        OuterSquare.BorderThickness = new Thickness(0.0D);
                        InnerGrid.Visibility = Visibility.Visible;
                        break;
                    case WindowsContainer.WindowLayoutMode.Tiled:
                        OuterSquare.BorderThickness = new Thickness(1.0D);
                        InnerGrid.Visibility = Visibility.Collapsed;
                        break;

                }
            }
        }

        private void ToggleMode()
        {
            if (this.Mode == WindowsContainer.WindowLayoutMode.Tiled)
            {
                this.Mode = WindowsContainer.WindowLayoutMode.Detailed;
            }
            else
            {
                this.Mode = WindowsContainer.WindowLayoutMode.Tiled;
            }
        }

        private void Grid_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ToggleMode();

            if (ModeChanged != null)
            {
                ModeChanged(this, new ModeChangedEventArgs(this.Mode));
            }
        }

    }
}
