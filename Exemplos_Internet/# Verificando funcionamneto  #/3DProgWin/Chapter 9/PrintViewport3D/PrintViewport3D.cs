//------------------------------------------------
// PrintViewport3D.cs (c) 2007 by Charles Petzold
//------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.PrintViewport3D
{
    public partial class PrintViewport3D
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new PrintViewport3D());
        }

        // Constructor.
        public PrintViewport3D()
        {
            InitializeComponent();
        }

        // Event handler for Print command.
        void PrintOnClick(object sender, RoutedEventArgs args)
        {
            PrintDialog dlg = new PrintDialog();

            if (dlg.ShowDialog().GetValueOrDefault())
            {
                dlg.PrintVisual(viewport3d, "PrintViewport3D: 3D Cube");
            }
        }
    }
}
