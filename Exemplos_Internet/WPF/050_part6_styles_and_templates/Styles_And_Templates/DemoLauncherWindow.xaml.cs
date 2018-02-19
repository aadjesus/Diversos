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

using PlanetsListBox;

namespace Styles_And_Templates
{
    /// <summary>
    /// Simply launches the reqested demo window, oh and
    /// also demonstrates the use of a DataTemplate within
    /// the associated DemoLauncherWindow.xaml
    /// </summary>
    public partial class DemoLauncherWindow : Window
    {
        /// <summary>
        /// Constructs a new DemoLauncherWindow, and
        /// creates a bunch of <see cref="DemoListItem">
        /// DemoListItem</see> items to be used within the
        /// contained ListBox
        /// </summary>
        public DemoLauncherWindow()
        {
            InitializeComponent();

            //Create the demo list items

            lstDemos.Items.Add(
                new DemoListItem { DemoName = "Hierarchical Template Example", 
                                   WindowName = "HierarchicalDataTemplateWindow.xaml"
                                 });

            lstDemos.Items.Add(
                new DemoListItem
                {
                    DemoName = "Planets ListBox Template Example",
                    WindowName = "PlanetsListBoxWindow.xaml"
                });

            lstDemos.Items.Add(
                new DemoListItem
                {
                    DemoName = "Various Control Templates Example",
                    WindowName = "VariousControlTemplatesWindow.xaml"
                });

        }

        /// <summary>
        /// Show the demo window requested
        /// </summary>
        private void lstDemos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String winName = (lstDemos.SelectedItem as DemoListItem).WindowName;
            Window win=null;

            switch (winName)
            {
                case "HierarchicalDataTemplateWindow.xaml" :
                    win = new HierarchicalDataTemplateWindow();
                    break;
                case "PlanetsListBoxWindow.xaml":
                    win = new PlanetsListBoxWindow();
                    break;
                case "VariousControlTemplatesWindow.xaml":
                    win = new VariousControlTemplatesWindow();
                    break;

            }
            win.Owner = this;
            win.Width = this.Width;
            win.Height = this.Height;
            win.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            win.ShowInTaskbar = false;
            //win.ResizeMode = ResizeMode.NoResize;
            win.ShowDialog();
        }
    }
}
