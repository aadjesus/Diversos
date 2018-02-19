using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.Drawing;
using DevExpress.XtraGrid.Columns;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid.Drawing;

namespace DevExpress.XtraGrid.Helpers
{

    public class GridViewQuickColumnCustomization
    {

        enum ColumnCustomizationState { None, Pressed, Shown }

        GridView view;
        ColumnCustomizationState state = ColumnCustomizationState.None;
        GridViewQuickColumnCustomizationContainerEdit containerEdit;
        Size popupSize = Size.Empty;

        public GridViewQuickColumnCustomization(GridView view)
        {
            this.view = view;
            View.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(View_CustomDrawRowIndicator);
            View.MouseDown += new System.Windows.Forms.MouseEventHandler(View_MouseDown);
            View.MouseUp += new System.Windows.Forms.MouseEventHandler(View_MouseUp);
        }
        public GridView View { get { return view; } }
        public Size PopupSize { get { return popupSize; } set { popupSize = value; } }

        ColumnCustomizationState State
        {
            get { return state; }
            set
            {
                if (State == value) return;
                state = value;
                InvalidateIndicator();
                if (State == ColumnCustomizationState.Shown)
                {
                    ShowCustomization();
                }
            }
        }

        void View_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            State = IsCursorOnColumnButton(e) ? ColumnCustomizationState.Pressed : ColumnCustomizationState.None;
        }

        void View_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (State != ColumnCustomizationState.Pressed) return;
            State = IsCursorOnColumnButton(e) ? ColumnCustomizationState.Shown : ColumnCustomizationState.None;
        }
        void View_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle == GridControl.InvalidRowHandle)
            {
                // You may assign your own image list e.Info.Images to show the custom image.
                e.Info.ImageIndex = GridPainter.IndicatorNewItemRow;
                e.Info.Appearance.ForeColor = Color.Blue;
                if (State != ColumnCustomizationState.None)
                {
                    e.Info.State = DevExpress.Utils.Drawing.ObjectState.Pressed;
                }
            }
        }
        void ShowCustomization()
        {
            SetupContainerEdit();
            ContainerEdit.ShowPopup();
        }
        Rectangle GetColumnButtonBounds()
        {
            GridViewInfo vi = View.GetViewInfo() as GridViewInfo;
            for (int i = 0; i < vi.ColumnsInfo.Count; i++)
            {
                if (vi.ColumnsInfo[i].Type == GridColumnInfoType.Indicator)
                    return vi.ColumnsInfo[i].Bounds;
            }
            return Rectangle.Empty;
        }
        void InvalidateIndicator()
        {
            View.InvalidateRect(GetColumnButtonBounds());
        }
        Point columnButtonLoation = Point.Empty;
        bool IsCursorOnColumnButton(System.EventArgs e)
        {
            DXMouseEventArgs de = DXMouseEventArgs.GetMouseArgs(View.GridControl, e);
            object o = View.CalcHitInfo(de.Location);
            return View.CalcHitInfo(de.Location).HitTest == GridHitTest.ColumnButton;
        }
        protected GridViewQuickColumnCustomizationContainerEdit ContainerEdit { get { return containerEdit; } }
        void SetupContainerEdit()
        {
            this.containerEdit = new GridViewQuickColumnCustomizationContainerEdit(View);
            ContainerEdit.Text = string.Empty;
            ContainerEdit.Properties.AutoHeight = false;
            ContainerEdit.Properties.LookAndFeel.ParentLookAndFeel = View.GridControl.LookAndFeel;
            ContainerEdit.Properties.Appearance.BackColor = Color.Transparent;
            ContainerEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            ContainerEdit.Properties.Buttons.Clear();
            ContainerEdit.Closed += new ClosedEventHandler(OnClosed);
            ContainerEdit.Bounds = GetColumnButtonBounds();
            if (!PopupSize.IsEmpty)
            {
                ContainerEdit.Properties.PopupStartSize = PopupSize;
            }
            ContainerEdit.Parent = View.GridControl;
        }

        void OnClosed(object sender, ClosedEventArgs e)
        {
            ContainerEdit.Dispose();
            this.containerEdit = null;
            State = ColumnCustomizationState.None;
        }
    }

    public class GridViewQuickColumnCustomizationContainerEdit : BlobBaseEdit
    {
        GridView view;
        public GridViewQuickColumnCustomizationContainerEdit(GridView view)
        {
            this.view = view;
        }
        protected override PopupBaseForm CreatePopupForm()
        {
            return new GridViewQuickColumnCustomizationPopup(this, view);
        }
        protected override void OnPopupClosing(CloseUpEventArgs e)
        {
            if (e.AcceptValue)
            {
                ((GridViewQuickColumnCustomizationPopup)PopupForm).Apply();
            }
            base.OnPopupClosing(e);
        }
    }
    public class GridViewQuickColumnCustomizationPopup : BlobBasePopupForm
    {
        GridView view;
        CheckedListBoxControl checkListBox;

        public GridViewQuickColumnCustomizationPopup(BlobBaseEdit ownerEdit, GridView view)
            : base(ownerEdit)
        {
            this.view = view;
            this.checkListBox = new CheckedListBoxControl();
            this.checkListBox.BorderStyle = BorderStyles.Simple;
            this.checkListBox.Appearance.Assign(ownerEdit.Properties.AppearanceDropDown);
            this.checkListBox.LookAndFeel.ParentLookAndFeel = OwnerEdit.LookAndFeel;
            this.checkListBox.Visible = false;
            this.checkListBox.CheckOnClick = true;
            this.checkListBox.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(OnCheckListBoxItemCheck);
            this.Controls.Add(checkListBox);
            UpdateCheckListBox();
            FillList();
            OkButton.Enabled = true;
        }
        public void Apply()
        {
            View.BeginUpdate();
            try
            {
                for (int i = 0; i < CheckListBox.Items.Count; i++)
                {
                    GridColumn column = (GridColumn)CheckListBox.Items[i].Value;
                    column.Visible = CheckListBox.Items[i].CheckState == CheckState.Checked;
                }
            }
            finally
            {
                View.EndUpdate();
            }
        }
        protected GridView View { get { return view; } }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (CheckListBox != null)
                {
                    this.checkListBox.Dispose();
                    this.checkListBox = null;
                }
            }
            base.Dispose(disposing);
        }
        protected override Control EmbeddedControl { get { return CheckListBox; } }
        public CheckedListBoxControl CheckListBox { get { return checkListBox; } }
        protected void UpdateCheckListBox()
        {
            OkButton.Enabled = true;
            CheckListBox.BeginUpdate();
            try
            {
                CheckListBox.Appearance.Assign(ViewInfo.PaintAppearanceContent);
            }
            finally
            {
                CheckListBox.EndUpdate();
            }
        }
        public override void ProcessKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                OwnerEdit.ClosePopup();
                return;
            }
            base.ProcessKeyDown(e);
        }

        public override void ShowPopupForm()
        {
            BeginControlUpdate();
            try
            {
                UpdateCheckListBox();
            }
            finally
            {
                EndControlUpdate();
            }
            base.ShowPopupForm();
            FocusFormControl(CheckListBox);
            OkButton.Enabled = true;
        }
        void FillList()
        {
            CheckListBox.BeginUpdate();
            try
            {
                foreach (GridColumn column in View.Columns)
                {
                    if (column.OptionsColumn.ShowInCustomizationForm)
                    {
                        CheckListBox.Items.Add(column, column.GetTextCaption(), column.Visible ? CheckState.Checked : CheckState.Unchecked, true);
                    }
                }
            }
            finally
            {
                CheckListBox.EndUpdate();
            }
        }
        void OnCheckListBoxItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            bool hasVisibleButton = false;
            for (int i = 0; i < CheckListBox.Items.Count; i++)
            {
                if (CheckListBox.Items[i].CheckState == System.Windows.Forms.CheckState.Checked)
                {
                    hasVisibleButton = true;
                    break;
                }
            }
            OkButton.Enabled = hasVisibleButton;
        }
    }
}