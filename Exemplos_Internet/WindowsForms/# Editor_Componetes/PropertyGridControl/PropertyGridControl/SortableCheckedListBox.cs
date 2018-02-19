using DevExpress.XtraEditors;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Drawing;
using System.Collections.Generic;
using DevExpress.XtraEditors.Controls;
using System;
using DevExpress.Utils;
using System.Reflection;
using System.Collections;

namespace PropertyGridControl
{
    class CustomCheckedListBox : CheckedListBoxControl
    {
        //int dragSourceIndex, dragTargetIndex;
        //bool isDraging;
        bool[] allowMove;

        //Rectangle dragBeginRect;
        public CustomCheckedListBox()
            : base()
        {
            //SelectionMode = SelectionMode.None;
            TabStop = false;
            CheckOnClick = true;
            //isDraging = false;
            this.MultiColumn = true;
            //this.SelectionMode = SelectionMode.MultiExtended;
            this.AllowDrop = true;
        }

        protected virtual internal void SetAllowMoving(int index, bool value)
        {
            allowMove[index] = value;
        }
        protected virtual internal void CreateAllowMovingArray() { allowMove = new bool[Items.Count]; }
        //protected virtual bool IsDraging { get { return isDraging; } set { if (isDraging != value) isDraging = value; } }
        //protected override CheckedListBoxItemCollection CreateItemCollection() { return new CustomCheckedListBoxItemCollection(); }
        protected override BaseStyleControlViewInfo CreateViewInfo() { return new CustomCheckedListBoxViewInfo(this); }
        protected override BaseControlPainter CreatePainter() { return new CustomPainterCheckedListBox(); }
        protected new virtual CustomCheckedListBoxViewInfo ViewInfo { get { return base.ViewInfo as CustomCheckedListBoxViewInfo; } }

        public bool EnableDragDropSort { get; set; }

        CustomCheckedListBoxItem itemInitiatingMove = null;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (EnableDragDropSort)
            {
                int itemInitiatingMoveIndex = this.IndexFromPoint(new Point(e.X, e.Y));

                if (itemInitiatingMoveIndex != -1)
                    itemInitiatingMove = this.GetItem(itemInitiatingMoveIndex) as CustomCheckedListBoxItem;
            }
        }

        List<CustomCheckedListBoxItem> itemsToMove = new List<CustomCheckedListBoxItem>();
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (EnableDragDropSort && e.Button == MouseButtons.Left)
                if (itemInitiatingMove != null)
                {
                    Point newPoint = new Point(e.X, e.Y);
                    BaseListBoxViewInfo.ItemInfo info = this.ViewInfo.GetItemInfoByPoint(this.PointToClient(newPoint));
                    int insertIndex = -1;
                    
                    if (info != null)
                        insertIndex = info.Index;

                    if (itemInitiatingMove.Index != insertIndex)
                    {
                        itemsToMove.Clear();

                        if (this.SelectedItems.IndexOf(itemInitiatingMove) == -1)
                            itemsToMove.Add(itemInitiatingMove);

                        foreach (CustomCheckedListBoxItem item in SelectedItems)
                            itemsToMove.Add(item);

                        this.DoDragDrop(itemsToMove, DragDropEffects.Move);
                    }
                }
        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            base.OnDragOver(drgevent);

            if (EnableDragDropSort && drgevent.Data.GetDataPresent(typeof(List<CustomCheckedListBoxItem>)))
            {
                drgevent.Effect = DragDropEffects.Move;
                Point newPoint = new Point(drgevent.X, drgevent.Y);
                BaseListBoxViewInfo.ItemInfo info = this.ViewInfo.GetItemInfoByPoint(this.PointToClient(newPoint));

                if (info != null)
                    ViewInfo.MarkItem(info.Index, itemInitiatingMove.Index);
            }
            else
                drgevent.Effect = DragDropEffects.None;
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);

            if (EnableDragDropSort && itemsToMove != null && itemsToMove.Count > 0 && drgevent.Data.GetDataPresent(typeof(List<CustomCheckedListBoxItem>)))
            {
                Point newPoint = new Point(drgevent.X, drgevent.Y);
                BaseListBoxViewInfo.ItemInfo info = this.ViewInfo.GetItemInfoByPoint(this.PointToClient(newPoint));

                if (info != null)
                {
                    int insertIndex = info.Index - 1;
                    itemsToMove.Sort();

                    foreach (CustomCheckedListBoxItem itemToMove in itemsToMove)
                    {
                        if (itemToMove.Index > insertIndex)
                            insertIndex++;

                        Items.Remove(itemToMove);
                        if (insertIndex == -1 || insertIndex >= Items.Count)
                            Items.Add(itemToMove);
                        else
                            Items.Insert(insertIndex, itemToMove);
                    }

                    this.UnSelectAll();
                }
            }
        }

        //Works with just one item
        //protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        //{
        //    base.OnMouseDown(e);
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        int index = this.IndexFromPoint(e.Location);
        //        if (index >= 0 && index < Items.Count && !allowMove[index]) return;
        //        IsDraging = false;
        //        dragSourceIndex = index;
        //        dragTargetIndex = dragSourceIndex;
        //        if (dragSourceIndex != -1)
        //        {
        //            Size dragSize = SystemInformation.DragSize;
        //            dragBeginRect = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
        //        }
        //        else
        //            dragBeginRect = Rectangle.Empty;
        //    }
        //}
        //protected override void OnMouseUp(MouseEventArgs e)
        //{
        //    if (!IsDraging || dragBeginRect == Rectangle.Empty) base.OnMouseUp(e);
        //    if (dragBeginRect == Rectangle.Empty) return;
        //    if (dragSourceIndex != -1 && dragTargetIndex != dragSourceIndex)
        //        ChangeItemsPositionCore(dragSourceIndex, dragTargetIndex);

        //    dragBeginRect = Rectangle.Empty;
        //}
        //protected override void OnMouseMove(MouseEventArgs e)
        //{
        //    base.OnMouseMove(e);
        //    if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
        //        if (dragBeginRect != Rectangle.Empty && !dragBeginRect.Contains(e.X, e.Y))
        //        {
        //            IsDraging = true;
        //            dragTargetIndex = this.IndexFromPoint(e.Location);
        //            if (dragTargetIndex == -1)
        //                if (e.Y < this.ViewInfo.GetItemRectangle(0).Bottom) dragTargetIndex = 0;
        //            CheckedListBoxViewInfo.CheckedItemInfo info = ViewInfo.GetItemByIndex(dragSourceIndex);
        //            if (info != null) info.PaintAppearance.ForeColor = Color.Red;
        //            ViewInfo.MarkItem(dragTargetIndex, dragSourceIndex);
        //        }
        //}
        //protected virtual void ChangeItemsPositionCore(int source, int target)
        //{
        //    CorrectAllowMove(source, target);
        //    if (target == -1)
        //    {
        //        Items.Add(Items[source]);
        //    }
        //    else
        //    {
        //        Items.Insert(target, Items[source]);
        //        if (source > target) source++;
        //    }
        //    Items.RemoveAt(source);
        //}

        //protected virtual void CorrectAllowMove(int source, int target)
        //{
        //    bool b = allowMove[source];
        //    if (target == -1) target = Items.Count - 1;
        //    if (target > source)
        //        for (int i = source; i < target; i++) allowMove[i] = allowMove[i + 1];
        //    else
        //        for (int i = source; i > target; i--) allowMove[i] = allowMove[i - 1];
        //    allowMove[target] = b;
        //}
    }
    public class CustomCheckedListBoxItemCollection : CheckedListBoxItemCollection
    {
        public CustomCheckedListBoxItemCollection() : base() { }

        public override int Add(object value)
        {
            if (value is CustomCheckedListBoxItem) return base.Add(new CustomCheckedListBoxItem(this, value));
            CheckedListBoxItem item = value as CheckedListBoxItem;
            if (item == null) return base.Add(new CustomCheckedListBoxItem(this, value, CheckState.Unchecked));
            else return base.Add(new CustomCheckedListBoxItem(this, item.Value, item.Description, item.CheckState, item.Enabled));
        }
    }
    public class CustomCheckedListBoxItem : CheckedListBoxItem, IComparable
    {
        public CustomCheckedListBoxItem() : base() { }
        public CustomCheckedListBoxItem(CustomCheckedListBoxItemCollection items, object value) : base(value) { this.ParentList = items; }
        public CustomCheckedListBoxItem(CustomCheckedListBoxItemCollection items, object value, bool isChecked) : base(value, isChecked) { this.ParentList = items; }
        public CustomCheckedListBoxItem(CustomCheckedListBoxItemCollection items, object value, string description) : base(value, description) { this.ParentList = items; }
        public CustomCheckedListBoxItem(CustomCheckedListBoxItemCollection items, object value, CheckState checkState) : base(value, checkState) { this.ParentList = items; }
        public CustomCheckedListBoxItem(CustomCheckedListBoxItemCollection items, object value, CheckState checkState, bool enabled) : base(value, checkState, enabled) { this.ParentList = items; }
        public CustomCheckedListBoxItem(CustomCheckedListBoxItemCollection items, object value, string description, CheckState checkState)
            : base(value, description, checkState) { this.ParentList = items; }
        public CustomCheckedListBoxItem(CustomCheckedListBoxItemCollection items, object value, string description, CheckState checkState, bool enabled)
            : base(value, description, checkState, enabled) { this.ParentList = items; }

        private CustomCheckedListBoxItemCollection ParentList { get; set; }

        public int Index
        {
            get
            {
                return this.ParentList.IndexOf(this);
            }
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj is CustomCheckedListBoxItem)
                return this.Index.CompareTo(((CustomCheckedListBoxItem)obj).Index);
            else
                return this.CompareTo(obj);
        }

        #endregion
    }
    public class CustomCheckedListBoxViewInfo : CheckedListBoxViewInfo
    {
        Color dragDropLineColor;
        public CustomCheckedListBoxViewInfo(CheckedListBoxControl listBox) : base(listBox) { }
        protected override ItemInfo CreateItemInfo(Rectangle bounds, object item, string text, int index)
        {
            CheckedItemInfo info = base.CreateItemInfo(bounds, item, text, index) as CheckedItemInfo;
            CustomCheckedItemInfo patchedInfo = new CustomCheckedItemInfo(info);
            return patchedInfo;
        }
        public virtual Color DragDropLineColor { get { return dragDropLineColor; } set { if (dragDropLineColor != value) dragDropLineColor = value; } }
        protected internal virtual void UnderlineItem(int index)
        {
            CustomCheckedItemInfo info = base.GetItemByIndex(index) as CustomCheckedItemInfo;
            if (info != null)
                info.IsUnderLine = true;
        }
        protected internal virtual void OverlineItem(int index)
        {
            CustomCheckedItemInfo info = base.GetItemByIndex(index) as CustomCheckedItemInfo;
            if (info != null)
                info.IsOverLine = true;
        }
        protected virtual internal int ItemCountAccessMethod() { return ItemCount; }
        protected internal virtual void DropLine()
        {
            foreach (CustomCheckedItemInfo info in VisibleItems)
                info.DropLine();
        }
        protected internal virtual void MarkItem(int targetIndex, int sourceIndex)
        {
            DropLine();
            dragDropLineColor = Color.Red;
            if ((targetIndex == sourceIndex) || (targetIndex == sourceIndex + 1) ||
                (sourceIndex == ItemCount - 1 && targetIndex == -1)) dragDropLineColor = Color.LightGray;
            if (targetIndex == -1)
                UnderlineItem(ItemCount - 1);
            else
                OverlineItem(targetIndex);
            if (targetIndex > 0)
                UnderlineItem(targetIndex - 1);
            OwnerControl.Invalidate();
        }

        public class CustomCheckedItemInfo : CheckedItemInfo
        {
            bool isUnderLine, isOverLine;
            public CustomCheckedItemInfo(Rectangle rect, object item, string text, int index, CheckState checkState, bool enabled)
                : base(rect, item, text, index, checkState, enabled)
            {
                DropLine();
            }
            public CustomCheckedItemInfo(CheckedItemInfo info)
                : this(info.Bounds, info.Item, info.Text, info.Index, info.CheckArgs.CheckState, info.Enabled)
            {
                this.CheckArgs.Assign(info.CheckArgs);
                this.TextRect = info.TextRect;
            }
            protected virtual internal bool IsUnderLine { get { return isUnderLine; } set { isUnderLine = value; } }
            protected virtual internal bool IsOverLine { get { return isOverLine; } set { isOverLine = value; } }
            protected virtual internal void DropLine()
            {
                isUnderLine = false;
                isOverLine = false;
            }
        }
    }
    public class CustomPainterCheckedListBox : PainterCheckedListBox
    {
        const int lineWidth = 1;
        protected override void DrawItemCore(ControlGraphicsInfoArgs info, BaseListBoxViewInfo.ItemInfo itemInfo, ListBoxDrawItemEventArgs e)
        {
            base.DrawItemCore(info, itemInfo, e);
            CustomCheckedListBoxViewInfo.CustomCheckedItemInfo customInfo =
                itemInfo as CustomCheckedListBoxViewInfo.CustomCheckedItemInfo;
            if (customInfo == null) return;
            Rectangle rec = new Rectangle(itemInfo.Bounds.Location, new Size(itemInfo.Bounds.Width, lineWidth));
            Color lineColor = ((CustomCheckedListBoxViewInfo)info.ViewInfo).DragDropLineColor;
            if (customInfo.IsOverLine)
            {
                if (customInfo.Index == 0) rec.Height++;
                info.Graphics.FillRectangle(info.Cache.GetSolidBrush(lineColor), rec);
            }
            if (customInfo.IsUnderLine)
            {
                rec.Offset(0, itemInfo.Bounds.Height - lineWidth);
                if (customInfo.Index == ((CustomCheckedListBoxViewInfo)info.ViewInfo).ItemCountAccessMethod() - 1) rec.Height++;
                info.Graphics.FillRectangle(info.Cache.GetSolidBrush(lineColor), rec);
            }
        }
    }
}
