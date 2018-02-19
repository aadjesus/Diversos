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


namespace SeeThru
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : System.Windows.Window
    {

        public Window1()
        {
            InitializeComponent();
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Close,
                new ExecutedRoutedEventHandler(delegate(object sender, ExecutedRoutedEventArgs args) { this.Close(); })));
        }

        public void DragWindow(object sender, MouseButtonEventArgs args)
        {
            DragMove();
        }

        public void ButtonClicked(object sender, RoutedEventArgs args)
        {
            CrazyWindow win2 = new CrazyWindow();
            win2.ShowDialog();
        }
    }
}