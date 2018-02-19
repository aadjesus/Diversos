using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using Hardcodet.Wpf.GenericTreeView;

namespace Hardcodet.Wpf.Samples
{
  /// <summary>
  /// Interaction logic for AboutDialog.xaml
  /// </summary>
  public partial class AboutDialog : Window
  {
    public AboutDialog()
    {
      InitializeComponent();

      //set version number
      Version version = typeof(TreeViewBase<object>).Assembly.GetName().Version;
      txtVersion.Text = version.ToString();
    }


    private void btnOk_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = true;
    }


    private void OnNavigationRequest(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
    {
      Process.Start(e.Uri.ToString());
      e.Handled = true;
    }
  }
}