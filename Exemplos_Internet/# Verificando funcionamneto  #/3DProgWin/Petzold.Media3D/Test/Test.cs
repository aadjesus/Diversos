//-------------------------------------
// Test.cs (c) 2007 by Charles Petzold
//-------------------------------------
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Petzold.Media3D;

namespace Petzold.Test
{
    public partial class Test
    {
        public Test()
        {
            InitializeComponent();
        }

        protected override void OnMouseMove(MouseEventArgs args)
        {
            Point pt = args.GetPosition(viewport);
        }
    }
}
