using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Design;
using DevExpress.Data;
using System.Collections;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dataSourceClassBindingSource.AddNew();
            dataSourceClassBindingSource.AddNew();
            dataSourceClassBindingSource.AddNew();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ConditionExpressionEditorForm form = new ConditionExpressionEditorForm(new MyColumnInfo(gridView1.Columns[0]), null);
            form.Show();
        }
    }

    public class MyColumnInfo : IDataColumnInfo
    {
        public MyColumnInfo(IDataColumnInfo column) : this(column, new string[0]) { }
        public MyColumnInfo(IDataColumnInfo column, IList invisibleFields)
        {
            caption = column.Caption;
            columns = new List<IDataColumnInfo>();
            columns.AddRange(column.Columns);
            columns.Add(column);
            controller = column.Controller;
            fieldName = column.FieldName;
            fieldType = column.FieldType;
            name = column.Name;
            unboundExpression = column.UnboundExpression;
        }
        #region IDataColumnInfo Members
        private string caption;
        public string Caption
        {
            get { return caption; }
        }
        private List<IDataColumnInfo> columns;
        public List<IDataColumnInfo> Columns
        {
            get { return columns; }
        }
        private DataControllerBase controller;
        public DataControllerBase Controller
        {
            get { return controller; }
        }
        private string fieldName;
        public string FieldName
        {
            get { return fieldName; }
        }
        private Type fieldType;
        public Type FieldType
        {
            get { return fieldType; }
        }
        private string name;
        public string Name
        {
            get { return name; }
        }
        private string unboundExpression;
        public string UnboundExpression
        {
            get { return unboundExpression; }
        }
        #endregion
    }

    public class DataSourceClass
    {
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
            }
        }
        private int? _ID;
        public int? ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
            }
        }
    }
}