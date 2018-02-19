//------------------------------------------------------
// PrintViewport3DVisual.cs (c) 2007 by Charles Petzold
//------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.PrintViewport3DVisual
{
    public partial class PrintViewport3DVisual
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new PrintViewport3DVisual());
        }

        // Constructor.
        public PrintViewport3DVisual()
        {
            InitializeComponent();
        }

        // Event handler for Print command.
        void PrintOnClick(object sender, RoutedEventArgs args)
        {
            PrintDialog dlg = new PrintDialog();

            if (dlg.ShowDialog().GetValueOrDefault())
            {
                dlg.PrintVisual(visual3d, "PrintViewport3DVisual: 3D Cube");
            }
        }
    }
}
