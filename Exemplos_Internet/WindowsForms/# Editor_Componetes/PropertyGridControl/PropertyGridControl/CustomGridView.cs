using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using System.ComponentModel;
using DevExpress.Utils.Serializing;
using System.Drawing;
using DevExpress.LookAndFeel.Helpers;
using DevExpress.XtraGrid.Views.Printing;
using DevExpress.XtraGrid.Views.Grid.Customization;
using System.Windows.Forms;

namespace PropertyGridControl
{
    public class CustomGridView : GridView
    {
        QuickHideEdit hideEdit;
        public CustomGridView() : base() { }
        public CustomGridView(GridControl gridControl) : base(gridControl) { }
        protected internal virtual void SetGridControlAccessMethod(GridControl newControl) { SetGridControl(newControl); }
        protected override string ViewName { get { return GridViewName; } }
        protected override GridColumnCollection CreateColumnCollection() { return new CustomGridColumnCollection(this); }
        protected override GridOptionsCustomization CreateOptionsCustomization() { return new CustomGridOptionsCustomization(); }
        [Description("Provides access to the View's customization options."), Category("Options"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        XtraSerializableProperty(XtraSerializationVisibility.Content, XtraSerializationFlags.DefaultValue)]
        public new CustomGridOptionsCustomization OptionsCustomization { get { return base.OptionsCustomization as CustomGridOptionsCustomization; } }
        protected virtual internal EmbeddedLookAndFeel GetLookAndFeel() { return ElementsLookAndFeel; }
        public static string GridViewName { get { return "CustomGridView"; } }
        public override bool IsFocusedView
        {
            get
            {
                if (hideEdit != null)
                    if (hideEdit.Enabled == true)
                        return true;
                return base.IsFocusedView; ;
            }
        }
        protected virtual internal void ShowColumnCustomizationMenu(Point p)
        {
            if (hideEdit == null) CreateHideEdit();
            LocateHideEdit();
            PopulateHideEdit();
            hideEdit.ShowPopup();
        }
        private void LocateHideEdit()
        {
            hideEdit.MakeEnable();
            hideEdit.Location = ((CustomGridViewInfo)ViewInfo).QuickCustomizationBounds.Location;
        }
        private void CreateHideEdit()
        {
            hideEdit = new QuickHideEdit(this);
            LocateHideEdit();
            GridControl.Controls.Add(hideEdit);
            this.Focus();
        }
        protected internal virtual void AcceptQuickHide()
        {
            foreach (GridColumn col in Columns)
            {
                ColumnProperties cp = hideEdit.Properties.Columns[col.ToString()];
                if (cp == null) continue;
                col.VisibleIndex = cp.VisibleIndex;
                col.Visible = cp.Visible;
            }
        }
        protected virtual void PopulateHideEdit()
        {
            hideEdit.Properties.Columns.Clear();
            if (Columns.Count == 0) return;
            foreach (CustomGridColumn col in Columns)
            {
                if (col.Visible || col.OptionsColumn.ShowInCustomizationForm)
                    hideEdit.Properties.Columns.Add(col.ToString(), col.Visible, col.VisibleIndex, GetColumnHideState(col), GetColumnMoveState(col));
            }
            hideEdit.Properties.Columns.Sort();
        }
        protected virtual bool GetColumnHideState(CustomGridColumn column)
        {
            return column.OptionsColumn.AllowQuickHide && OptionsCustomization.AllowQuickHideColumns;
        }
        protected virtual bool GetColumnMoveState(CustomGridColumn column)
        {
            return column.OptionsColumn.AllowMove && OptionsCustomization.AllowColumnMoving;
        }



        bool shouldDrawFooter = false;
        public bool ShouldDrawFooter
        {
            get { return shouldDrawFooter; }
            set { shouldDrawFooter = value; }
        }
        protected override BaseViewPrintInfo CreatePrintInfoInstance(PrintInfoArgs args)
        {
            if (ShouldDrawFooter)
                return base.CreatePrintInfoInstance(args);
            return new CustomGridViewPrintInfo(args);
        }


        protected override CustomizationForm CreateCustomizationForm()
        {
            return new CustomCustomizationForm(this);
        }

        GridColumnCollection Columns;

        [Category("Design"), Description("Adjust the Column collection of the current view and specify total summaries.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CustomGridColumnCollection GridColumns
        {
            get
            {
                return (CustomGridColumnCollection)this.Columns;
            }
        }


    }
}

