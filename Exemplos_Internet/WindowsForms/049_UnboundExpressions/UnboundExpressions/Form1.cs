using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors.Design;


namespace UnboundExpressions
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        RootColumnInfo root;
        private void Form1_Load(object sender, EventArgs e)
        {
            root = new RootColumnInfo();
            foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(typeof(MyDataObject)))
                root.Columns.Add(new DataColumnInfo(pd));
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            using (UnboundExpressionEditor form = new UnboundExpressionEditor(root))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    labelControl1.Text = form.Expression;
                }
            }
        }
    }

    public class MyDataObject
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public class UnboundExpressionEditor : ExpressionEditorFormEx
    {
        public UnboundExpressionEditor(IDataColumnInfo columns) : base(columns, null) { }
    }

    public class RootColumnInfo : IDataColumnInfo
    {

        public RootColumnInfo() { }

        #region IDataColumnInfo Members

        public string Caption
        {
            get { return "root"; }
        }

        List<IDataColumnInfo> columns = new List<IDataColumnInfo>();
        public List<IDataColumnInfo> Columns
        {
            get { return columns; }
        }

        public DataControllerBase Controller
        {
            get { return null; }
        }

        public string FieldName
        {
            get { return "field"; }
        }

        public Type FieldType
        {
            get { return typeof(object); }
        }

        public string Name
        {
            get { return "name"; }
        }

        public string UnboundExpression
        {
            get { return ""; }
        }

        #endregion
    }
}
