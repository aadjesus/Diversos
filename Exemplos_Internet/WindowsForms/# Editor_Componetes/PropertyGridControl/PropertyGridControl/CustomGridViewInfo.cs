using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Drawing;

namespace PropertyGridControl
{
    public class CustomGridViewInfo : GridViewInfo
    {
        public QuickCustomizationIconStatus QuickCustomizationIconStatus;
        static int QuickCustomizationWidth = 10, QuickCustomizationHeight = 11, QuickCustomizationSpacing = 2;
        Rectangle quickCustumisationBounds;
        public CustomGridViewInfo(GridView gridView)
            : base(gridView)
        {
            quickCustumisationBounds = Rectangle.Empty;
            QuickCustomizationIconStatus = QuickCustomizationIconStatus.Hidden;
        }
        public virtual Rectangle QuickCustomizationBounds
        {
            get
            {
                Rectangle rec = new Rectangle();
                rec.Location = new Point(ColumnsInfo[0].Bounds.Right - QuickCustomizationWidth - QuickCustomizationSpacing,
                    ColumnsInfo[0].Bounds.Top + QuickCustomizationSpacing);
                rec.Size = new Size(QuickCustomizationWidth, QuickCustomizationHeight);
                return rec;
            }
        }
        public virtual bool IsQuickCustomizationButton(Point p) { return QuickCustomizationBounds.Contains(p); }
        public bool AllowQuickCustomization { get { return ((CustomGridView)View).OptionsCustomization.AllowQuickCustomization; } }

        public virtual QuickCustomizationIcon QuickCustomizationIcon { get { return ((CustomGridView)View).OptionsCustomization.QuickCustomizationIcons; } }
    }
}
