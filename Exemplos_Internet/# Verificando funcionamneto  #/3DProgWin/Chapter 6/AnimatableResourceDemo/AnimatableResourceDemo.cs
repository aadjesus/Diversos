//-------------------------------------------------------
// AnimatableResourceDemo.cs (c) 2007 by Charles Petzold
//-------------------------------------------------------
using System;
using System.Windows;

namespace Petzold.AnimatableResourceDemo
{
    public partial class AnimatableResourceDemo : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new AnimatableResourceDemo());
        }

        public AnimatableResourceDemo()
        {
            InitializeComponent();
        }
    }
}
