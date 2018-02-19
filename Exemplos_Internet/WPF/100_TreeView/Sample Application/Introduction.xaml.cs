using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Hardcodet.Wpf.Samples
{
  /// <summary>
  /// Interaction logic for Introduction.xaml
  /// </summary>
  public partial class Introduction : UserControl
  {
    public Introduction()
    {
      InitializeComponent();
    }

    private void OnNavigationRequest(object sender, RequestNavigateEventArgs e)
    {
      Process.Start(e.Uri.ToString());
      e.Handled = true;
    }
  }
}
