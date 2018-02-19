using System;
using System.Collections;
using System.Windows.Forms;

namespace PropertyGridControl
{
    public class ColumnProperties : IComparable
    {
        bool visible, allowQuickHide, allowMove;
        int visibleIndex;
        string caption;
        public ColumnProperties(string caption, bool visible, int visibleIndex, bool allowQuickHide, bool allowMove)
        {
            this.visible = visible;
            this.visibleIndex = visibleIndex;
            this.allowQuickHide = allowQuickHide;
            this.allowMove = allowMove;
            this.caption = caption;
        }
        public bool Visible { get { return visible; } set { visible = value; } }
        public int VisibleIndex { get { return visibleIndex; } set { visibleIndex = value; } }
        public bool AllowQuickHide { get { return allowQuickHide; } set { allowQuickHide = value; } }
        public bool AllowMove { get { return allowMove; } set { allowMove = value; } }
        public string Caption { get { return caption; } set { caption = value; } }
        public CheckState CheckState
        {
            get
            {
                if (Visible)
                    return CheckState.Checked;
                else
                    return CheckState.Unchecked;
            }
            set
            {
                if (value == CheckState.Checked)
                    Visible = true;
                else
                    Visible = false;
            }
        }


        #region IComparable Members

        public int CompareTo(object obj)
        {
            ColumnProperties column = obj as ColumnProperties;
            if (column == null) return 0;
            if (visibleIndex < 0 && column.VisibleIndex >= 0) return 1;
            if (visibleIndex >= 0 && column.VisibleIndex < 0) return -1;
            if (visibleIndex < 0 && column.VisibleIndex < 0) return Caption.CompareTo(column.Caption);
            if (VisibleIndex > column.VisibleIndex) return 1;
            else if (VisibleIndex < column.VisibleIndex) return -1;
            else return 0;
        }

        #endregion
    }
    public class ColumnPropertiesCollection : ArrayList
    {
        public new virtual ColumnProperties this[int index]
        {
            get
            {
                if (index < 0 || index > Count - 1) return null;
                return base[index] as ColumnProperties;
            }
            set
            {
                if (index < 0 || index > Count - 1) return;
                base[index] = value;
            }
        }
        public new virtual ColumnProperties this[string caption] { get { return this[IndexFromCaption(caption)]; } }

        private int IndexFromCaption(string caption)
        {
            for (int i = 0; i < Count; i++)
                if (this[i].Caption == caption) return i;
            return -1;
        }
        public void Add(string caption, bool visible, int visibleIndex, bool allowQuickHide, bool allowMove) { base.Add(new ColumnProperties(caption, visible, visibleIndex, allowQuickHide, allowMove)); }
        public void Add(string caption, bool visible, int visibleIndex, bool allowQuickHide) { base.Add(new ColumnProperties(caption, visible, visibleIndex, allowQuickHide, true)); }

        public ColumnPropertiesCollection GetCopy()
        {
            ColumnPropertiesCollection collection = new ColumnPropertiesCollection();
            foreach (ColumnProperties column in this)
                collection.Add(column);
            return collection;
        }
    }
}
