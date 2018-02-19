#region Using

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

#endregion

namespace UserControlAsDataTemplateDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Members

        /// <summary>
        /// An observable collection composed of DataItem objects
        /// </summary>
        ObservableCollection<DataItem> items = new ObservableCollection<DataItem>();

        /// <summary>
        /// A random number generator
        /// </summary>
        Random _random = new Random();

        #endregion

        #region Constructor

        /// <summary>
        /// The MainWindow constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Code

        private void btnLoadData_Click(object sender, RoutedEventArgs e)
        {
            // Reset the Items Collection
            items.Clear();

            // Populate the Items Collection
            PopulateDataItems();

            // Bind the items Collection to the List Box
            peopleListBox.ItemsSource = items;

            // Alter the buttons text
            btnLoadData.Content = "Change Data";
        }

        private void btnClearData_Click(object sender, RoutedEventArgs e)
        {
            // Clear the items Collection
            items.Clear();

            // Alter the buttons text
            btnLoadData.Content = "Load Data";
        }

        #endregion

        #region Data Creation

        private void PopulateDataItems()
        {
            items.Add(new DataItem("Jammer", 32, MakeRandomDouble()));
            items.Add(new DataItem("John", 44, MakeRandomDouble()));
            items.Add(new DataItem("Jane", 25, MakeRandomDouble()));
            items.Add(new DataItem("Robert", 30, MakeRandomDouble()));
            items.Add(new DataItem("Jezzer", 50, MakeRandomDouble()));
            items.Add(new DataItem("James", 40, MakeRandomDouble()));
            items.Add(new DataItem("Rebecca", 25, MakeRandomDouble()));
            items.Add(new DataItem("Mark", 35, MakeRandomDouble()));
            items.Add(new DataItem("Leah", 20, MakeRandomDouble()));
            items.Add(new DataItem("WallE", 700, MakeRandomDouble()));
        }

        private double MakeRandomDouble()
        {
            int randomnumber = _random.Next(1, 100);
            return (double)randomnumber;
        }

        #endregion

    }
}