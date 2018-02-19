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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.Utils.Drawing;
using DevExpress.XtraBars;

namespace CustomControlInGrid
{
    public partial class Form1 : Form
    {

        DataTable dt = new DataTable();
        public Form1()
        {
            InitializeComponent();
            dt.Columns.Add("TestCol", typeof(int));
			dt.Columns.Add("TestCol2", typeof(int));
			dt.Rows.Add(10, 12);
			dt.Rows.Add(13, 18);
			dt.Rows.Add(14, 20);

            customRepositoryItem1.ControlType = typeof(ChildControl);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
			gridControl1.DataSource = dt;
        }
    }
}
