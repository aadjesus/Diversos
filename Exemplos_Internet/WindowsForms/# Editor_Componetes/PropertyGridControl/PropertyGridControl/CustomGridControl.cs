using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base;

namespace PropertyGridControl
{
    public class CustomGridControl : GridControl
    {
        public CustomGridControl() : base() { }
        protected override void RegisterAvailableViewsCore(InfoCollection collection)
        {
            base.RegisterAvailableViewsCore(collection);
            collection.Add(new CustomGridInfoRegistrator());
        }
        protected override BaseView CreateDefaultView() { return CreateView("CustomGridView"); }
    }
}