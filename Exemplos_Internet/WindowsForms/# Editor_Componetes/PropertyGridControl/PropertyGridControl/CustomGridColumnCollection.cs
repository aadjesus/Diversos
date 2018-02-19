using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;

namespace PropertyGridControl
{
    public class CustomGridColumnCollection : GridColumnCollection
    {
        public CustomGridColumnCollection(ColumnView view) : base(view) { }
        protected override GridColumn CreateColumn() { return new CustomGridColumn(); }

        public override int Add(GridColumn column)
        {
            return base.Add(column as CustomGridColumn);
        }

    }
}
