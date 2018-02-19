#region Using Region

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
using System.Xml;
using System.IO;
using System.Windows.Markup;

#endregion

namespace WPFControls
{
    /// <summary>
    /// Interaction logic for MenuItems.xaml
    /// </summary>

    public partial class MenuItems : MenuItem
    {
        #region Private Properties

        private ListView mListView = null;

        #endregion

        #region Dependency Property

        public static readonly DependencyProperty HeaderBindingPathProperty = DependencyProperty.Register(
            "HeaderBindingPath", typeof(String), typeof(MenuItems), new PropertyMetadata(""));

        public String HeaderBindingPath
        {
            get { return (String)GetValue(HeaderBindingPathProperty); }
            set { SetValue(HeaderBindingPathProperty, value); }
        }

        public static readonly DependencyProperty IsCheckedBindingPathProperty = DependencyProperty.Register(
            "IsCheckedBindingPath", typeof(String), typeof(MenuItems), new PropertyMetadata(""));

        public String IsCheckedBindingPath
        {
            get { return (String)GetValue(IsCheckedBindingPathProperty); }
            set { SetValue(IsCheckedBindingPathProperty, value); }
        }

        public static readonly DependencyProperty TopSeparatorVisibilityProperty = DependencyProperty.Register(
            "TopSeparatorVisibility", typeof(Visibility), typeof(MenuItems), new PropertyMetadata(Visibility.Collapsed));

        public Visibility TopSeparatorVisibility
        {
            get { return (Visibility)GetValue(TopSeparatorVisibilityProperty); }
            set { SetValue(TopSeparatorVisibilityProperty, value); }
        }

        public static readonly DependencyProperty BottomSeparatorVisibilityProperty = DependencyProperty.Register(
            "BottomSeparatorVisibility", typeof(Visibility), typeof(MenuItems), new PropertyMetadata(Visibility.Collapsed));

        public Visibility BottomSeparatorVisibility
        {
            get { return (Visibility)GetValue(BottomSeparatorVisibilityProperty); }
            set { SetValue(BottomSeparatorVisibilityProperty, value); }
        }

        #endregion

        #region Constructor

        public MenuItems()
        {
            InitializeComponent();
            
            this.Loaded += new RoutedEventHandler(MenuItems_Loaded);
        }

        #endregion

        #region Events

        protected void MenuItems_Loaded(object sender, RoutedEventArgs e)
        {
            // In order to set the header and ischecked properties I needed to add two
            // dependency propeties for the binding path of those properties. Then I 
            // needed to get the listview created by the controls template and 
            // construct an data template for the listview's itemtemplate property.
            if ((VisualTreeHelper.GetChildrenCount(this) > 0) && (mListView == null))
            {
                mListView = (ListView)VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(this, 0), 1);
                    //(ListView)VisualTreeHelper.GetChild(this, 0);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(@"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">");
                sb.AppendLine(@"<MenuItem");
                if (!String.IsNullOrEmpty(HeaderBindingPath))
                    sb.AppendLine(@"Header=""{Binding Path=" + HeaderBindingPath + @"}""");
                sb.AppendLine(@"Background=""{Binding RelativeSource={RelativeSource Mode=FindAncestor, AncestorType={x:Type MenuItem}}, Path=Background}""");
                sb.AppendLine(@"IsCheckable=""{Binding RelativeSource={RelativeSource Mode=FindAncestor, AncestorType={x:Type MenuItem}}, Path=IsCheckable}""");
                if (!String.IsNullOrEmpty(IsCheckedBindingPath))
                    sb.AppendLine(@"IsChecked=""{Binding Path=" + IsCheckedBindingPath + @"}""");
                sb.AppendLine(@"/>");
                sb.AppendLine(@"</DataTemplate>");
                object o = XamlReader.Load(new XmlTextReader(new StringReader(sb.ToString())));

                if (o.GetType().Equals(typeof(DataTemplate)))
                {
                    mListView.ItemTemplate = (DataTemplate)o;
                    mListView.ItemsSource = this.ItemsSource; // add the itemtemplate first, otherwise there will be a visual child error
                }
            }
        }

        #endregion
    }
}