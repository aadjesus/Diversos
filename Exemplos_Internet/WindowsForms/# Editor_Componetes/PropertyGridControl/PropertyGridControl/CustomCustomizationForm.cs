using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid.Views.Grid.Customization;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Customization;
using System.Collections;
using System.Windows.Forms;

namespace PropertyGridControl
{
    public class CustomCustomizationForm : CustomizationForm
    {
        public CustomCustomizationForm(GridView view) : base(view) { }

        private string _pattern;
        public string Pattern
        {
            get { return _pattern; }
            set
            {
                _pattern = value;
                this.ActiveListBox.Populate();
            }
        }

        private TextEdit fPatternEdit;

        protected override void CreateListBox()
        {
            base.CreateListBox();
            ActiveListBox.Dock = DockStyle.Fill;
            ActiveListBox.Populate();
            Controls.Add(ActiveListBox);
            fPatternEdit = new TextEdit();
            fPatternEdit.Dock = DockStyle.Top;
            Controls.Add(fPatternEdit);
            fPatternEdit.DataBindings.Add("EditValue", this, "Pattern", true, DataSourceUpdateMode.OnPropertyChanged);

        }

        //protected override CustomCustomizationListBoxBase CreateCustomizationListBox()
        //{
        //    CustomColumnCustomizationListBox fActiveListBox = new CustomColumnCustomizationListBox(this);
        //    return fActiveListBox;
        //}
    }

    public class CustomColumnCustomizationListBox : ColumnCustomizationListBox
    {
        public CustomColumnCustomizationListBox(CustomizationForm form) : base(form) { }
        public override void Populate()
        {
            string pattern = (this.CustomizationForm as CustomCustomizationForm).Pattern;
            base.Items.BeginUpdate();
            try
            {
                base.Items.Clear();
                ArrayList list = new ArrayList();
                foreach (CustomGridColumn column in base.View.Columns)
                {
                    if (column.CanShowInCustomizationForm)
                    {
                        if (string.IsNullOrEmpty(pattern) || GetCustomizationCaption(column).StartsWith(pattern, StringComparison.CurrentCultureIgnoreCase))
                        {
                            list.Add(column);
                        }
                    }
                }
                list.Sort(new ColumnComparer());
                foreach (CustomGridColumn column in list)
                {
                    base.Items.Add(column);
                }
            }
            finally
            {
                base.Items.EndUpdate();
            }

        }

        private static string GetCustomizationCaption(CustomGridColumn col)
        {
            string s = col.CustomizationCaption;
            if (string.IsNullOrEmpty(s))
                s = col.Caption;
            if (string.IsNullOrEmpty(s))
                s = col.FieldName;
            return s;
        }

        private class ColumnComparer : IComparer
        {
            int IComparer.Compare(object a, object b)
            {
                CustomGridColumn c1 = (CustomGridColumn)a;
                CustomGridColumn c2 = (CustomGridColumn)b;
                int res = Comparer.Default.Compare(GetCustomizationCaption(c1), GetCustomizationCaption(c2));
                if (res == 0)
                {
                    res = Comparer.Default.Compare(c1.AbsoluteIndex, c2.AbsoluteIndex);
                }
                return res;
            }
        }
    }
}
