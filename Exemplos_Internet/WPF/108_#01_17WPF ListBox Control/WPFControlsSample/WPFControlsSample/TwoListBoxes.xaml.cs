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
using System.Windows.Shapes;
using System.Collections;

namespace WPFControlsSample
{
    /// <summary>
    /// Interaction logic for TwoListBoxes.xaml
    /// </summary>
    public partial class TwoListBoxes : Window
    {
        private ArrayList myDataList = null;
        string currentItemText ;
        int currentItemIndex ;

        public TwoListBoxes()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Get data from somewhere and fill in my local ArrayList
            myDataList = LoadListBoxData();
            // Bind ArrayList with the ListBox
            LeftListBox.ItemsSource = myDataList;            
        }

        /// <summary>
        /// Generate data. This method can bring data from a database or XML file
        /// or from a Web service or generate data dynamically
        /// </summary>
        /// <returns></returns>
        private ArrayList LoadListBoxData()
        {
            ArrayList itemsList = new ArrayList();
            itemsList.Add("Coffie");
            itemsList.Add("Tea");
            itemsList.Add("Orange Juice");
            itemsList.Add("Milk");
            itemsList.Add("Mango Shake");
            itemsList.Add("Iced Tea");
            itemsList.Add("Soda");
            itemsList.Add("Water");
            return itemsList;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Find the right item and it's value and index
            currentItemText = LeftListBox.SelectedValue.ToString();
            currentItemIndex = LeftListBox.SelectedIndex;
            
            RightListBox.Items.Add(currentItemText);
            if (myDataList != null)
            {
                myDataList.RemoveAt(currentItemIndex);
            }

            // Refresh data binding
            ApplyDataBinding();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            // Find the right item and it's value and index
            currentItemText = RightListBox.SelectedValue.ToString();
            currentItemIndex = RightListBox.SelectedIndex;
            // Add RightListBox item to the ArrayList
            myDataList.Add(currentItemText);

           // LeftListBox.Items.Add(RightListBox.SelectedItem);
            RightListBox.Items.RemoveAt(RightListBox.Items.IndexOf(RightListBox.SelectedItem));

            // Refresh data binding
            ApplyDataBinding();
        }

        /// <summary>
        /// Refreshes data binding
        /// </summary>
        private void ApplyDataBinding()
        {
            LeftListBox.ItemsSource = null;
            // Bind ArrayList with the ListBox
            LeftListBox.ItemsSource = myDataList;
        }
    }
}
