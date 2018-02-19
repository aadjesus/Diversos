using System;
using System.Data;
using System.Configuration;
using System.Windows;

namespace GalaSoftLb.Wpf.ItemsControlExample
{
  public class MyDataItem : DependencyObject
  {
    // Note how the TitleProperty is a DependencyProperty, and the Title CLR property is
    // here just for convenience. The string itself is not stored in the object, it's
    // stored in the WPF framework.

    public string Title
    {
      get
      {
        return (string) GetValue( TitleProperty );
      }
      set
      {
        SetValue( TitleProperty, value );
      }
    }

    // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register( "Title", typeof( string ), typeof( MyDataItem ), new UIPropertyMetadata( "" ) );

    public MyDataItem( string title )
    {
      Title = title;
    }
  }
}
