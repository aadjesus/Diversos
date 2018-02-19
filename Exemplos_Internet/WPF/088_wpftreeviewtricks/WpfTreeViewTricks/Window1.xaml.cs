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
using System.Collections.ObjectModel;

namespace WpfTreeViewTricks
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private SomeDataObject _dataRoot;
        private ObservableCollection<SomeDataObject> _dataList;
        private int _currentId;
        private Random _random;

        public Window1()
        {
            InitializeComponent();
            this._random = new Random(DateTime.Now.Millisecond);
            this._currentId = 0;

            this._dataRoot = new SomeDataObject(0);
            this._dataList = new ObservableCollection<SomeDataObject>();

            SomeDataObject firstChild = new SomeDataObject(this._currentId);
            this._dataRoot.Children.Add(firstChild);
            this._dataList.Add(firstChild);
            this._currentId++;

            this.MainTreeView.ItemsSource = this._dataRoot.Children;
            this.MainListBox.ItemsSource = this._dataList;
        }

        void ListViewSelectionChanged(object sender, RoutedEventArgs e)
        {
            this.MainTreeView.SelectItem(this.MainListBox.SelectedItem);
        }

        void ExpandAll(object sender, RoutedEventArgs e)
        {
            this.MainTreeView.ExpandAll();
        }

        void AddNode(object sender, RoutedEventArgs e)
        {
            CreateAndAddNode();
        }

        private void CreateAndAddNode()
        {
            SomeDataObject someDataObject = new SomeDataObject(this._currentId);

            //adds the item to a child of one of the elements in the tree
            this._dataList[this._random.Next(this._dataList.Count)].Children.Add(someDataObject);

            //also add the item to the end of the list
            this._dataList.Add(someDataObject);

            this._currentId++;
        }
    }
}
