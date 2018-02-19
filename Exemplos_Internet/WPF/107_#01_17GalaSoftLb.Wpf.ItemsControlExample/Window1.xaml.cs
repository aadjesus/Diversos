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

namespace GalaSoftLb.Wpf.ItemsControlExample
{
  public partial class Window1 : System.Windows.Window
  {
    private ObservableCollection<MyDataItem> dataItems;

    public ObservableCollection<MyDataItem> DataItems
    {
      get
      {
        if ( dataItems == null )
        {
          return new ObservableCollection<MyDataItem>();
        }
        return dataItems;
      }
    }

    public Window1()
    {
      // Initialize collection first, then parse the XAML
      dataItems = new ObservableCollection<MyDataItem>();
      for ( int index = 0; index < 4; index++ )
      {
        MyDataItem item = new MyDataItem( index.ToString() );
        dataItems.Add( item );
      }

      InitializeComponent();

      buttonItemPlus.Click += new RoutedEventHandler( buttonItemPlus_Click );
      buttonItemMinus.Click += new RoutedEventHandler( buttonItemMinus_Click );
      buttonIndexPlus.Click += new RoutedEventHandler( buttonIndexPlus_Click );
      buttonIndexMinus.Click += new RoutedEventHandler( buttonIndexMinus_Click );
      buttonClose.Click += new RoutedEventHandler( buttonClose_Click );
      this.MouseDown += new MouseButtonEventHandler( Window1_MouseDown );
    }

    void Window1_MouseDown( object sender, MouseButtonEventArgs e )
    {
      this.DragMove();
    }

    void buttonClose_Click( object sender, RoutedEventArgs e )
    {
      this.Close();
      Application.Current.Shutdown();
      e.Handled = true;
    }

    // Business Logic Layer. In reality, this is done by a business object ++++

    void buttonIndexMinus_Click( object sender, RoutedEventArgs e )
    {
      // Note how the action is done on the collection, NOT on the UI element.
      // The action is automatically propagated to the UI element through binding.
      if ( dataItems.Count >= 2 )
      {
        MyDataItem item = dataItems[ 1 ];
        item.Title = ( Int32.Parse( item.Title ) - 1 ).ToString();
      }
      e.Handled = true;
    }

    void buttonIndexPlus_Click( object sender, RoutedEventArgs e )
    {
      // Note how the action is done on the collection, NOT on the UI element.
      // The action is automatically propagated to the UI element through binding.
      if ( dataItems.Count >= 2 )
      {
        MyDataItem item = dataItems[ 1 ];
        item.Title = ( Int32.Parse( item.Title ) + 1 ).ToString();
      }
      e.Handled = true;
    }

    void buttonItemMinus_Click( object sender, RoutedEventArgs e )
    {
      // Note how the action is done on the collection, NOT on the UI element.
      // The action is automatically propagated to the UI element through binding.
      if ( dataItems.Count > 0 )
      {
        dataItems.RemoveAt( dataItems.Count - 1 );
      }
      if ( dataItems.Count == 0 )
      {
        buttonItemMinus.IsEnabled = false;
      }
      if ( dataItems.Count < 2 )
      {
        buttonIndexPlus.IsEnabled = false;
        buttonIndexMinus.IsEnabled = false;
      }
      e.Handled = true;
    }

    void buttonItemPlus_Click( object sender, RoutedEventArgs e )
    {
      // Note how the action is done on the collection, NOT on the UI element.
      // The action is automatically propagated to the UI element through binding.
      MyDataItem item = new MyDataItem( dataItems.Count.ToString() );
      dataItems.Add( item );
      buttonItemMinus.IsEnabled = true;
      if ( dataItems.Count >= 2 )
      {
        buttonIndexPlus.IsEnabled = true;
        buttonIndexMinus.IsEnabled = true;
      }
      e.Handled = true;
    }
  }
}