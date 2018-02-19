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
using Microsoft.Win32;
using System.Windows.Markup;
using System.IO;

namespace TestXamlSerializer
{

   //****************************************************************************
 public partial class MainWindow 
    : Window
  {

    //==========================================================================
    public MainWindow()
    {
      InitializeComponent();
    }

    //==========================================================================
    private void LoadButton_Click(object sender, RoutedEventArgs e)
    {
      OpenFileDialog dialog = new OpenFileDialog();

      dialog.Title  = "Load XAML";
      dialog.Filter = "XAML Files (*.xaml)|*.xaml|All Files (*.*)|*.*";

      if(dialog.ShowDialog() == true)
        try
        {
          using(Stream stream = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
            XamlFrame.Content = XamlSerializer.Load(stream);

          StatusTextBlock.Text = String.Format("Successfully loaded {0}.", dialog.FileName);
        }
        catch(Exception exception)
        {
          StatusTextBlock.Text = exception.Message;
        }
    }

    //==========================================================================
    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
      SaveFileDialog dialog = new SaveFileDialog();

      dialog.Title  = "Save XAML";
      dialog.Filter = "XAML Files (*.xaml)|*.xaml|All Files (*.*)|*.*";

      if(dialog.ShowDialog() == true)
        try
        {
          using(Stream stream = new FileStream(dialog.FileName, FileMode.CreateNew, FileAccess.Write))
            XamlSerializer.Save(XamlFrame.Content, stream);

          StatusTextBlock.Text = String.Format("Successfully save {0}.", dialog.FileName);
        }
        catch(Exception exception)
        {
          StatusTextBlock.Text = exception.Message;
        }
    }

  } // class MainWindow

}
