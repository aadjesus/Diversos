using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;

namespace MdiTest
{
    public partial class Form2 : DevExpress.XtraEditors.XtraForm
    {
        public Form2()
        {
            InitializeComponent();

            Localizer loc = new MyLocalizer();
            Localizer.Active = loc;

            GridView view = gridControl1.MainView as GridView;
            view.OptionsBehavior.AutoPopulateColumns = false;

            DataTable tbl = new DataTable();
            tbl.Columns.Add("date", typeof(DateTime));
            tbl.Rows.Add(DateTime.Now);
            tbl.Rows.Add(DateTime.Now);
            tbl.Rows.Add(DateTime.Now);
            tbl.Rows.Add(DateTime.Now);
            gridControl1.DataSource = tbl;
        }
    }
}