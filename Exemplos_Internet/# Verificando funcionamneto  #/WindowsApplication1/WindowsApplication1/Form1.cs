using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors;
using System.Drawing.Printing;
using DevExpress.Utils.Win;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraGrid.Views.Base;
using System.Diagnostics;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Registrator;

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            categoriesTableAdapter.Fill(nwindDataSet.Categories);
            productsTableAdapter.Fill(nwindDataSet.Products);
            colMyDate.ColumnEdit = new RepositoryItemMyTextEdit();

            ((RepositoryItemMyTextEdit)colMyDate.ColumnEdit).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            ((RepositoryItemMyTextEdit)colMyDate.ColumnEdit).DisplayFormat.FormatString = "d";
        }

        private void gridView1_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            e.Value = DateTime.Now;
            if (e.RowHandle > 5)
            {
                e.Value = DateTime.MinValue ;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gridControl1.ExportToXls("test.xls");//,new XlsExportOptions(false,true));
            Process.Start("test.xls");
        }

        private void gridView1_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
        }

     
    }
        [UserRepositoryItem("Register")]
        public class RepositoryItemMyTextEdit : RepositoryItemTextEdit
        {
            static RepositoryItemMyTextEdit()
            {
                Register();
            }
            public RepositoryItemMyTextEdit() { }

            internal const string EditorName = "MyTextEdit";

            public static void Register()
            {
                EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(EditorName, typeof(MyTextEdit),
                    typeof(RepositoryItemMyTextEdit), typeof(DevExpress.XtraEditors.ViewInfo.TextEditViewInfo),
                    new DevExpress.XtraEditors.Drawing.TextEditPainter(), true, null, typeof(DevExpress.Accessibility.TextEditAccessible)));
            }
            public override string EditorTypeName
            {
                get { return EditorName; }
            }
            public override IVisualBrick GetBrick(PrintCellHelperInfo info)
            {
                TextBrick b = base.GetBrick(info) as TextBrick;
                if (Convert.ToDateTime(b.Value) == DateTime.MinValue)
                {
                    b.Text = "";
                    b.TextValue = "";
                }
                return b;
            }
        }

        public class MyTextEdit :TextEdit
        {
            static MyTextEdit()
            {
                RepositoryItemMyTextEdit.Register();
            }
            public MyTextEdit() { }

            public override string EditorTypeName
            {
                get { return RepositoryItemMyTextEdit.EditorName; }
            }

            [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
            public new RepositoryItemMyTextEdit Properties
            {
                get { return base.Properties as RepositoryItemMyTextEdit; }
            }
        }
    
}