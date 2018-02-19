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
using WPFMwiWindows.Controls;
using System.Collections.ObjectModel;
#endregion

namespace WPFMwiWindows
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    /// 
    public partial class Window1 : System.Windows.Window
    {
        #region Constructor

        public Window1()
        {
            InitializeComponent();

            CommandBinding cmdBinding = null;

            // Always show the close command and upon execution close the application
            cmdBinding = new CommandBinding(ApplicationCommands.Close);
            cmdBinding.PreviewExecuted += new ExecutedRoutedEventHandler(delegate(object sender, ExecutedRoutedEventArgs e) { this.Close(); e.Handled = true; });
            cmdBinding.CanExecute += new CanExecuteRoutedEventHandler(delegate(object sender, CanExecuteRoutedEventArgs e) { e.CanExecute = true; }); 
            cmdBinding.PreviewCanExecute += new CanExecuteRoutedEventHandler(delegate(object sender, CanExecuteRoutedEventArgs e) { e.CanExecute = true; });
            CommandBindings.Add(cmdBinding);

            // Always show the New command and upon execution create a new window in the MwiWindow control.
            cmdBinding = new CommandBinding(ApplicationCommands.New);
            cmdBinding.PreviewExecuted += new ExecutedRoutedEventHandler(delegate(object sender, ExecutedRoutedEventArgs e) { this.gMwiWindow.CreateNewMwiChild(); e.Handled = true; });
            cmdBinding.CanExecute += new CanExecuteRoutedEventHandler(delegate(object sender, CanExecuteRoutedEventArgs e) { e.CanExecute = true; });
            cmdBinding.PreviewCanExecute += new CanExecuteRoutedEventHandler(delegate(object sender, CanExecuteRoutedEventArgs e) { e.CanExecute = true; });
            CommandBindings.Add(cmdBinding);

            // Only show the Cascade command when there are child windows. Upon execution rearrange the child windows so that they are cascading.
            cmdBinding = new CommandBinding(MwiWindow.Cascade);
            cmdBinding.PreviewExecuted += new ExecutedRoutedEventHandler(delegate(object sender, ExecutedRoutedEventArgs e) { this.gMwiWindow.CascadeChildren(); e.Handled = true; });
            cmdBinding.CanExecute += new CanExecuteRoutedEventHandler(delegate(object sender, CanExecuteRoutedEventArgs e) { if (this.gMwiWindow.AttachedChildren.Count > 0) { e.CanExecute = true; } });
            cmdBinding.PreviewCanExecute += new CanExecuteRoutedEventHandler(delegate(object sender, CanExecuteRoutedEventArgs e) { if (this.gMwiWindow.AttachedChildren.Count > 0) { e.CanExecute = true; } }); 
            CommandBindings.Add(cmdBinding);

            // Only show the Tile command when there are child windows. Upon execution rearrange the child windows so that they are tiled.
            cmdBinding = new CommandBinding(MwiWindow.Tile);
            cmdBinding.PreviewExecuted += new ExecutedRoutedEventHandler(delegate(object sender, ExecutedRoutedEventArgs e) { this.gMwiWindow.TileChildren(); e.Handled = true; });
            cmdBinding.CanExecute += new CanExecuteRoutedEventHandler(delegate(object sender, CanExecuteRoutedEventArgs e) { if (this.gMwiWindow.AttachedChildren.Count > 0) { e.CanExecute = true; } });
            cmdBinding.PreviewCanExecute += new CanExecuteRoutedEventHandler(delegate(object sender, CanExecuteRoutedEventArgs e) { if (this.gMwiWindow.AttachedChildren.Count > 0) { e.CanExecute = true; } }); 
            CommandBindings.Add(cmdBinding);

            // Only show the Tile Horizontally command when there are child windows. Upon execution rearrange the child windows so that they are tiled horizontally.
            cmdBinding = new CommandBinding(MwiWindow.TileHorizontally);
            cmdBinding.PreviewExecuted += new ExecutedRoutedEventHandler(delegate(object sender, ExecutedRoutedEventArgs e) { this.gMwiWindow.HorizontallyTileChildren(); e.Handled = true; });
            cmdBinding.CanExecute += new CanExecuteRoutedEventHandler(delegate(object sender, CanExecuteRoutedEventArgs e) { if (this.gMwiWindow.AttachedChildren.Count > 0) { e.CanExecute = true; } });
            cmdBinding.PreviewCanExecute += new CanExecuteRoutedEventHandler(delegate(object sender, CanExecuteRoutedEventArgs e) { if (this.gMwiWindow.AttachedChildren.Count > 0) { e.CanExecute = true; } }); 
            CommandBindings.Add(cmdBinding);

            // Only show the Tile Vertically command when there are child windows. Upon execution rearrange the child windows so that they are tiled vertically.
            cmdBinding = new CommandBinding(MwiWindow.TileVertically);
            cmdBinding.PreviewExecuted += new ExecutedRoutedEventHandler(delegate(object sender, ExecutedRoutedEventArgs e) { this.gMwiWindow.VerticallyTileChildren(); e.Handled = true; });
            cmdBinding.CanExecute += new CanExecuteRoutedEventHandler(delegate(object sender, CanExecuteRoutedEventArgs e) { if (this.gMwiWindow.AttachedChildren.Count > 0) { e.CanExecute = true; } });
            cmdBinding.PreviewCanExecute += new CanExecuteRoutedEventHandler(delegate(object sender, CanExecuteRoutedEventArgs e) { if (this.gMwiWindow.AttachedChildren.Count > 0) { e.CanExecute = true; } });
            CommandBindings.Add(cmdBinding);
        }

        #endregion
    }
}