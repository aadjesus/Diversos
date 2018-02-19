using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;

namespace testpaintcontrol
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //listEditor1.Text = "this is a test";
            ImageValue iv = new ImageValue("yo",Properties.Resources.barcode16);

            listEditor1.EditValue = iv;
            listEditor2.EditValue = iv;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            DataTable table = new DataTable();
            table.Columns.Add("test", typeof(ImageValue));

            ImageValue iv1 = new ImageValue("test1", Properties.Resources.application_link);
            ImageValue iv2 = new ImageValue("test2", Properties.Resources.application_osx);
            ImageValue iv3 = new ImageValue("test3", Properties.Resources.application_put);

            repositoryItemListEditor1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            table.BeginLoadData();

            table.Rows.Add(iv1);
            table.Rows.Add(iv2);
            table.Rows.Add(iv3);

            table.EndLoadData();

            gridControl1.DataSource = table;
        }
    }

    public class PedeEmpresa : GridLookUpEdit
    {
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //public new RepositoryItemGridLookUpEdit Properties
        //{
        //    get
        //    {
        //        return base.Properties as RepositoryItemGridLookUpEdit;
        //    }
        //}
    }

    public class PedeEmpresa2 : ComboBoxEdit
    { 

    }
}