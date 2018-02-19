using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Dragging;
namespace PropertyGridControl
{
    public class CustomGridDragManager : GridDragManager
    {
        public CustomGridDragManager(GridView view) : base(view) { }
        protected override PositionInfo CalcColumnDrag(GridHitInfo hit, GridColumn column)
        {
            PositionInfo patchedPI = new PositionInfo();
            patchedPI = base.CalcColumnDrag(hit, column);
            if (patchedPI.Index == HideElementPosition && patchedPI.Valid)
            {
                CustomGridColumn col = column as CustomGridColumn;
                if (col != null)
                    if (!col.OptionsColumn.AllowQuickHide)
                    {
                        patchedPI = new PositionInfo();
                        patchedPI.Valid = false;
                    }
            }
            return patchedPI;
        }
    }
}
