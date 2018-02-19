using System;
using System.Windows.Controls;

namespace Microsoft.Samples.KMoore.WPFSamples.VisualTreeViewer
{
    public partial class VisualTreeViewerPage : Page
    {
        public VisualTreeViewerPage()
        {
            InitializeComponent();
        }

        private void load_click(object sender, EventArgs args)
        {
            VisualTreeItem vti = new VisualTreeItem(listBox);

            treeView.ItemsSource = vti.Children;
        }
    }
}