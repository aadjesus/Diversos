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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace GalaSoftLb.Wpf.SynchronizedAnimations.Xbap
{
    public partial class Page0 : System.Windows.Controls.Page
    {
        // Create the ObservableCollection containing the DataItems
        private ObservableCollection<DataItem> data
            = new ObservableCollection<DataItem>();
        public ObservableCollection<DataItem> Data
        {
            get { return data; }
        }

        // Populate the ObservableCollection.
        public Page0()
        {
            for (int index = 0; index < 4; index++)
            {
                DataItem item = new DataItem();
                item.Description = "Item # " + index;
                data.Add(item);
            }

            InitializeComponent();
        }

        // Handle the Click event.
        private void Item_Click(object sender, RoutedEventArgs e)
        {
            // Get the clicked button.
            Button source = e.OriginalSource as Button;
            // Get the DataItems for this button.
            DataItem item = source.DataContext as DataItem;
            // Start/Stop the animation by setting the DP.
            item.IsBlink = !item.IsBlink;
        }
    }
}