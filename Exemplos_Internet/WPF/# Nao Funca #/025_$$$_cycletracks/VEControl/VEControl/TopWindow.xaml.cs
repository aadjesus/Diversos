using System.Windows;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Controls;

namespace VEControl
{
    /// <summary>
    /// Interaction logic for TopmostSurfaceWindow.xaml
    /// </summary>
    public partial class TopWindow : Window
    {
        public TopWindow()
        {
            InitializeComponent();
        }

        public UserControl InfoBox
        {
            get
            {
                return this.infoBox.Content as UserControl;
            }

            set
            {
                value.Visibility = Visibility.Collapsed;
                this.infoBox.Content = value;
            }
        }
    }
}
