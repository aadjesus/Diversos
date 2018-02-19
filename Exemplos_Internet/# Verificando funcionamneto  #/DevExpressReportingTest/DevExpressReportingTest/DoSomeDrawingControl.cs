//----------------------------------------------------------------------------------------
//
// Module name: DoSomeDrawingControl
//
// Author: Markus.Florenz
//
// Created at: 4/2/2009 5:45:24 PM
//
// Description: 
// 
//
//(c) Bruker AXS GmbH
//----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DevExpressReportingTest
{
    public partial class DoSomeDrawingControl : UserControl
    {
        public DoSomeDrawingControl()
        {
            InitializeComponent();
        }

        private void DoSomeDrawingControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            e.Graphics.DrawEllipse(Pens.Black,e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width-1, e.ClipRectangle.Height-1);            
        }

        private void DoSomeDrawingControl_Resize(object sender, EventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("Width:" + ClientSize.Width + " Height:" + ClientSize.Height);
        }


    }
}
