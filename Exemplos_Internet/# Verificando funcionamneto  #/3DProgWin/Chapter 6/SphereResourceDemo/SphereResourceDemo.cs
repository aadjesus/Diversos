//---------------------------------------------------
// SphereResourceDemo.cs (c) 2007 by Charles Petzold
//---------------------------------------------------
using System;
using System.Windows;

namespace Petzold.SphereResourceDemo
{
    public partial class SphereResourceDemo : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new SphereResourceDemo());
        }

        public SphereResourceDemo()
        {
            InitializeComponent();
        }
    }
}
