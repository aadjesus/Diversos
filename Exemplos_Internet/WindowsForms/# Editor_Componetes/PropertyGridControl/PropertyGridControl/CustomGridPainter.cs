using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using System.Drawing;
using DevExpress.Utils.Drawing;
using System.Drawing.Imaging;
using DevExpress.XtraGrid.Drawing;

namespace PropertyGridControl
{
    public class CustomGridPainter : GridPainter
    {
        public CustomGridPainter(GridView view) : base(view) { }
        protected override void DrawIndicatorCore(GridViewDrawArgs e, IndicatorObjectInfoArgs info, int rowHandle, IndicatorKind kind)
        {
            base.DrawIndicatorCore(e, info, rowHandle, kind);
            DrawQuickCustomizationIcon(e, info, kind);
        }
        protected virtual void DrawQuickCustomizationIcon(GridViewDrawArgs e, IndicatorObjectInfoArgs info, IndicatorKind kind)
        {
            if (kind == DevExpress.Utils.Drawing.IndicatorKind.Header && ((CustomGridViewInfo)e.ViewInfo).QuickCustomizationIconStatus != QuickCustomizationIconStatus.Hidden)
                DrawQuickCustomizationIconCore(e, info, ((CustomGridViewInfo)e.ViewInfo).QuickCustomizationIcon, ((CustomGridViewInfo)e.ViewInfo).QuickCustomizationBounds, ((CustomGridViewInfo)e.ViewInfo).QuickCustomizationIconStatus);
        }
        protected virtual void DrawQuickCustomizationIconCore(GridViewDrawArgs e, IndicatorObjectInfoArgs info, QuickCustomizationIcon icon, Rectangle bounds, QuickCustomizationIconStatus status)
        {
            Rectangle patchedRec = new Rectangle(bounds.X + 1, bounds.Y, bounds.Width - 1, bounds.Height);
            GridColumnInfoArgs args = new GridColumnInfoArgs(e.Cache, e.ViewInfo.ColumnsInfo[0].Column);
            args.Cache = e.Cache;
            args.Bounds = patchedRec;
            ((HeaderObjectInfoArgs)args).HeaderPosition = HeaderPositionKind.Center;
            if (status == QuickCustomizationIconStatus.Hot)
                ((HeaderObjectInfoArgs)args).State = ObjectState.Hot;
            ElementsPainter.Column.DrawObject(args);

            if (icon.Image != null)
            {
                Rectangle rec = new Rectangle();
                rec.Location = new Point(bounds.Left + 1, bounds.Top + 1);
                rec.Size = new Size(bounds.Width - 2, bounds.Height - 2);
                ImageAttributes attr = new ImageAttributes();
                attr.SetColorKey(icon.TransperentColor, icon.TransperentColor);
                e.Graphics.DrawImage(icon.Image, rec, 0, 0, icon.Image.Width, icon.Image.Height, GraphicsUnit.Pixel, attr);
            }
        }
    }
}
