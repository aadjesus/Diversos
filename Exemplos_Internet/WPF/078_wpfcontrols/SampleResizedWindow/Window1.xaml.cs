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
using DNBSoft.WPF;

namespace SampleResizedWindow
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            WindowResizer wr = new WindowResizer(this);
            wr.addResizerRight(rightSizeGrip);
            wr.addResizerLeft(leftSizeGrip);
            wr.addResizerUp(topSizeGrip);
            wr.addResizerDown(bottomSizeGrip);
            wr.addResizerLeftUp(topLeftSizeGrip);
            wr.addResizerRightUp(topRightSizeGrip);
            wr.addResizerLeftDown(bottomLeftSizeGrip);
            wr.addResizerRightDown(bottomRightSizeGrip);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
