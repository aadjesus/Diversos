using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid.Handler;
using DevExpress.XtraGrid.Dragging;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.Utils;

namespace PropertyGridControl
{
    public class CustomGridHandler : GridHandler
    {
        public CustomGridHandler(GridView gridView) : base(gridView) { }
        protected override GridDragManager CreateDragManager() { return new CustomGridDragManager(View); }
        public override void DoClickAction(DevExpress.XtraGrid.Views.Base.ViewInfo.BaseHitInfo hitInfo)
        {
            base.DoClickAction(hitInfo);
            GridHitInfo hit = hitInfo as GridHitInfo;
            if (hit.HitTest == GridHitTest.ColumnButton && ((CustomGridView)View).OptionsCustomization.AllowQuickCustomization)
                if (((CustomGridViewInfo)ViewInfo).IsQuickCustomizationButton(hitInfo.HitPoint))
                    ((CustomGridView)View).ShowColumnCustomizationMenu(hit.HitPoint);

        }
        protected override bool OnMouseMove(MouseEventArgs ev)
        {
            DXMouseEventArgs e = DXMouseEventArgs.GetMouseArgs(ev);
            Point p = new Point(e.X, e.Y);
            UpdateQuickCustumizationIconState(p);
            return base.OnMouseMove(ev);
        }
        protected virtual void UpdateQuickCustumizationIconState(Point p)
        {
            CustomGridViewInfo vi = ViewInfo as CustomGridViewInfo;
            if (!vi.AllowQuickCustomization) return;
            GridHitInfo hi = ViewInfo.CalcHitInfo(p);
            if (hi.HitTest == GridHitTest.ColumnButton)
            {
                if (vi.IsQuickCustomizationButton(p))
                {
                    if (vi.QuickCustomizationIconStatus != QuickCustomizationIconStatus.Hot)
                    {
                        vi.QuickCustomizationIconStatus = QuickCustomizationIconStatus.Hot;
                        ViewInfo.View.Invalidate();
                    }
                    return;
                }
                if (vi.QuickCustomizationIconStatus != QuickCustomizationIconStatus.Enabled)
                {
                    vi.QuickCustomizationIconStatus = QuickCustomizationIconStatus.Enabled;
                    ViewInfo.View.Invalidate();
                }
            }
            else
                if (vi.QuickCustomizationIconStatus != QuickCustomizationIconStatus.Hidden)
                {
                    vi.QuickCustomizationIconStatus = QuickCustomizationIconStatus.Hidden;
                    ViewInfo.View.Invalidate();
                }
        }
    }
}
