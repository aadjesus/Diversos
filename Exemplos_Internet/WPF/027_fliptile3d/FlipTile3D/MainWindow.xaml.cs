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

namespace FlipTile3D
{
    /// <summary>
    /// The main window
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Ctor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Private Methods
        private void Tile3D_TileClicked(object sender, TileClickedEventArgs args)
        {
            blogDisplayer.Collapse = !blogDisplayer.Collapse;
            blogDisplayer.NavigateToUrl = args.Url;
            blogDisplayer.DoAnimation();
        }
        #endregion
    }
}
