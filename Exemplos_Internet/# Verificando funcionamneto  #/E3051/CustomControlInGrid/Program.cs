// Developer Express Code Central Example:
// How to put a custom UserControl in a GridView cell
// 
// This example demonstrates how a custom UserControl can be used as an in-place
// editor in GridView. As described in the http://www.devexpress.com/scid=A128
// Knowledge Base, it is not possible to just place a control within a cell,
// because cells are not controls. When a cell's editor is not activated, its
// content is drawn via a painter. So, in our example, we have created a painter to
// draw the entire UserControl's content. All cells in GridView will be drawn using
// this painter until an end-user clicks a cell. In this case, an actual instance
// of the UserControl class will be created. Controls inherited from the BaseEdit
// class are drawn via their painters, other controls are drawn via the
// DrawToBitmap function. In case of 3rd-party controls, you need to draw them
// manually. If you want to use your custom control in GridView or other controls,
// you need to implement the IEditValue interface in it.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E3051

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CustomControlInGrid
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
