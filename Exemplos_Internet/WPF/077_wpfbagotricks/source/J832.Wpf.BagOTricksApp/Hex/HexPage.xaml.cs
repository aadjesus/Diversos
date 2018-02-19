using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Microsoft.Samples.KMoore.WPFSamples.Hex
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class HexPage : Page
    {
        public HexPage()
        {
            InitializeComponent();
        }

        private void PlayRandom(object sender, RoutedEventArgs args)
        {
            (new RandomPlayer()).PlayGame(hbe.Board);
        }
        private void Reset(object sender, RoutedEventArgs args)
        {
            hbe.Reset();
        }
    }
}