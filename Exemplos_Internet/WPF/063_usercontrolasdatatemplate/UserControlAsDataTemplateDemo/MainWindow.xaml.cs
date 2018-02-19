using System;
using System.Collections.ObjectModel;
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

namespace UserControlAsDataTemplateDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// An observable collection composed of DataItem objects
        /// </summary>
        ObservableCollection<DataItem> items = new ObservableCollection<DataItem>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLoadData_Click(object sender, RoutedEventArgs e)
        {
            // Reset the Items Collection
            items.Clear();

            // Populate the Items Collection
            PopulateDataItems();

            // Bind the items Collection to the List Box
            peopleListBox.ItemsSource = items;
        }

        void PopulateDataItems()
        {
            items.Add(new DataItem("Jammer", 32, 10.0));
            items.Add(new DataItem("John", 44, 20.0));
            items.Add(new DataItem("Jane", 25, 30.0));
            items.Add(new DataItem("Robert", 30, 40.0));
            items.Add(new DataItem("Jezzer", 50, 50.0));
            items.Add(new DataItem("James", 40, 60.0));
            items.Add(new DataItem("Rebecca", 25, 70.0));
            items.Add(new DataItem("Mark", 35, 80.0));
            items.Add(new DataItem("Leah", 20, 90.0));
            items.Add(new DataItem("WallE", 700, 100.0));
        }

        private void btnClearData_Click(object sender, RoutedEventArgs e)
        {
            // Clear the items Collection
            items.Clear();
        }
    }
}