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
using Telerik.Windows.Controls;
using System.Collections.ObjectModel;

namespace WpfApplication1
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		static Window1()
		{
			EventManager.RegisterClassHandler(typeof(RadTreeViewItem),
				Mouse.MouseDownEvent, new MouseButtonEventHandler(OnTreeViewItemMouseDown), false);
		}

		public static ObservableCollection<Categories> Categories = new ObservableCollection<Categories>();
		public Window1()
		{
			ObservableCollection<Category> cats = new ObservableCollection<Category>();
			cats.Add(new Category("http://images.businessweek.com/ss/06/07/top_brands/image/bmw.jpg",
									"http://images.businessweek.com/ss/06/07/top_brands/image/bmw.jpg",
									"BMW"));

			cats.Add(new Category("http://images.businessweek.com/ss/06/07/top_brands/image/bmw.jpg",
			"http://images.businessweek.com/ss/06/07/top_brands/image/bmw.jpg",
			"BMW"));

			cats.Add(new Category("http://images.businessweek.com/ss/06/07/top_brands/image/bmw.jpg",
									"http://images.businessweek.com/ss/06/07/top_brands/image/bmw.jpg",
									"BMW"));


			Categories.Add(new Categories("Foo", cats));

		}

		public static void OnTreeViewItemMouseDown(object sender, MouseButtonEventArgs e)
		{
			var treeViewitem = sender as RadTreeViewItem;
			if (!(treeViewitem is RadTreeViewItem))
			{
				MessageBox.Show("Item clicked is not RadTreeViewItem");
			}
			if (e.RightButton == MouseButtonState.Pressed)
			{
				treeViewitem.IsSelected = true;
				e.Handled = true;
			}
		}
	}

	public class Category
	{
		public Category(string imageKey, string _src, string displayName)
		{
			ImageKey = imageKey;
			src = _src;
			DisplayName = displayName;
		}
		public string ImageKey { get; set; }
		public string src { get; set; }
		public string DisplayName { get; set; }
	}

	public class Categories
	{
		private string name;
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		private ObservableCollection<Category> children;
		public ObservableCollection<Category> Children
		{
			get
			{
				if (children == null)
					children = new ObservableCollection<Category>();

				return children;
			}
		}

		public Categories() { }

		public Categories(string name, ObservableCollection<Category> children)
		{
			this.name = name;
			this.children = children;
		}
	}
}
