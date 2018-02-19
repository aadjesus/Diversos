using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base.Handler;
using DevExpress.XtraGrid.Views.Base.ViewInfo;

namespace PropertyGridControl
{
    public class CustomGridInfoRegistrator : GridInfoRegistrator
    {
        public CustomGridInfoRegistrator() : base() { }
        //public override string ViewName { get { return "CustomGridView"; } }
        public override string ViewName { get { return CustomGridView.GridViewName; } }
        public override BaseViewHandler CreateHandler(BaseView view) { return new CustomGridHandler(view as GridView); }
        public override BaseViewPainter CreatePainter(BaseView view) { return new CustomGridPainter(view as GridView); }
        public override BaseViewInfo CreateViewInfo(BaseView view) { return new CustomGridViewInfo(view as GridView); }
        public override BaseView CreateView(GridControl grid)
        {
            CustomGridView view = new CustomGridView();
            view.SetGridControlAccessMethod(grid);
            return view;
        }
    }
}
