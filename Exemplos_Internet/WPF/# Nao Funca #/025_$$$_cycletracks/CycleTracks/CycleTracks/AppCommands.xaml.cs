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
using System.Windows.Shapes;
using Microsoft.Windows.Controls.Ribbon;

namespace CycleTracks
{
  public static class AppCommands
  {
    public static RibbonCommand Help
    {
      get { return (RibbonCommand)Application.Current.Resources["HelpCommand"]; }
    }
    public static RibbonCommand Cut
    {
      get { return (RibbonCommand)Application.Current.Resources["CutCommand"]; }
    }
    public static RibbonCommand Copy
    {
      get { return (RibbonCommand)Application.Current.Resources["CopyCommand"]; }
    }
    public static RibbonCommand Paste
    {
      get { return (RibbonCommand)Application.Current.Resources["PasteCommand"]; }
    }
    public static RibbonCommand AddNew
    {
      get { return (RibbonCommand)Application.Current.Resources["AddNewCommand"]; }
    }
    public static RibbonCommand AddMesh
    {
      get { return (RibbonCommand)Application.Current.Resources["AddMeshCommand"]; }
    }
    public static RibbonCommand Clear
    {
      get { return (RibbonCommand)Application.Current.Resources["ClearCommand"]; }
    }
    public static RibbonCommand Share
    {
      get { return (RibbonCommand)Application.Current.Resources["ShareCommand"]; }
    }
    public static RibbonCommand Compare
    {
      get { return (RibbonCommand)Application.Current.Resources["CompareCommand"]; }
    }
    public static RibbonCommand Register
    {
      get { return (RibbonCommand)Application.Current.Resources["RegisterCommand"]; }
    }
    public static RibbonCommand Options
    {
      get { return (RibbonCommand)Application.Current.Resources["OptionsCommand"]; }
    }
    public static RibbonCommand U2U
    {
      get { return (RibbonCommand)Application.Current.Resources["U2UCommand"]; }
    }
  }

}
