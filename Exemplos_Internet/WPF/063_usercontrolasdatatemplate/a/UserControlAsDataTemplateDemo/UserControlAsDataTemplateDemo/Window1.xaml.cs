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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        ObservableCollection<DataItem> items = new ObservableCollection<DataItem>();

        public Window1()
        {
            InitializeComponent();
            PopulateDataItems();
            peopleListBox.ItemsSource = items;
        }

        void PopulateDataItems()
        {
            items.Add(new DataItem("Jammer", 32));
            items.Add(new DataItem("John", 44));
            items.Add(new DataItem("Jane", 25));
            items.Add(new DataItem("Robert", 30));
            items.Add(new DataItem("Jezzer", 50));
            items.Add(new DataItem("James", 40));
            items.Add(new DataItem("Rebecca", 25));
            items.Add(new DataItem("Mark", 35));
            items.Add(new DataItem("Leah", 20));
        }
    }
}