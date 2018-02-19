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
using System.Windows.Shapes;
using System.Collections.ObjectModel;

#endregion

namespace MenuItems
{
    #region Widget Class

    public class Widget : Control
    {
        #region Properties

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(String), typeof(Widget), new PropertyMetadata(""));

        public String Title
        {
            get { return (String)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            "IsActive", typeof(bool), typeof(Widget), new PropertyMetadata(true));

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        #endregion

        #region Constructor

        public Widget(String title)
        {
            Title = title;
        }

        #endregion
    }

    #endregion

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : System.Windows.Window
    {
        #region Properties 

        public static readonly DependencyProperty WidgetsProperty = DependencyProperty.Register(
            "Widgets", typeof(ObservableCollection<Widget>), typeof(Window1), new PropertyMetadata(new ObservableCollection<Widget>()));

        public ObservableCollection<Widget> Widgets
        {
            get { return (ObservableCollection<Widget>)GetValue(WidgetsProperty); }
            set { SetValue(WidgetsProperty, value); }
        }

        #endregion

        #region Constructor

        public Window1()
        {
            InitializeComponent();

            this.DataContext = this;

            for (int i = 0; i < 4; i++)
            {
                Widgets.Add(new Widget(String.Format("Widget {0}", i)));
            }
        }

        #endregion
    }
}