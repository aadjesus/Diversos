using DevExpress.XtraGrid;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Printing;
using DevExpress.XtraPrinting;
using System.Collections;
using DevExpress.Data;
namespace PropertyGridControl
{
    public class CustomGridViewPrintInfo : GridViewPrintInfo
    {
        public CustomGridViewPrintInfo(PrintInfoArgs args) : base(args) { }

        protected override void PrintGroupRow(int rowHandle, int level)
        {
            Rectangle r = Rectangle.Empty;
            foreach (GridColumnInfoArgs agrs in ColumnsInfo)
            {
                int indent = agrs.Column.VisibleIndex == 0 ? Indent + level * ViewViewInfo.LevelIndent : 0;
                r.X = agrs.Bounds.X + indent;
                r.Width = agrs.Bounds.Width - indent;
                r.Y = Y;
                r.Height = CurrentRowHeight;
                SetDefaultBrickStyle(Graph, Bricks["GroupRow"]);
                Brick brick = (Brick)DrawTextBrick(Graph, agrs.Column.VisibleIndex == 0 ? View.GetGroupRowDisplayText(rowHandle) : string.Empty, r, true);
            }
            Y += r.Height;
        }

        protected override void PrintRowFooterCore(int prevRowHandle, int level, int fcount)
        {
            Rectangle r = Rectangle.Empty;
            foreach (PrintColumnInfo agrs in Columns)
            {
                int indent = agrs.Column.VisibleIndex == 0 ? Indent + level * ViewViewInfo.LevelIndent : 0;
                r.X = agrs.Bounds.X + indent;
                r.Width = agrs.Bounds.Width - indent;
                r.Y = Y;
                r.Height = CurrentRowHeight;
                SetDefaultBrickStyle(Graph, Bricks["GroupFooter"]);

                DictionaryEntry de = View.GetRowSummaryItem(prevRowHandle, agrs.Column);
                GridGroupSummaryItem gsi = de.Key as GridGroupSummaryItem;
                TextBrick brick = (TextBrick)DrawTextBrick(Graph, View.GetRowFooterCellText(prevRowHandle, agrs.Column), r, true);
                if (gsi != null && agrs.Column.SummaryItem.SummaryType != SummaryItemType.None)
                {
                    brick.TextValue = brick.Value = de.Value;
                    brick.TextValueFormatString = gsi.DisplayFormat;
                }
                else
                {
                    brick.Text = string.Empty;
                }
            }
            Y += r.Height;
        }

        public override void PrintFooterPanel(IBrickGraphics graph)
        {
            Rectangle r = Rectangle.Empty;
            foreach (PrintColumnInfo agrs in Columns)
            {
                r.X = agrs.Bounds.X + Indent;
                r.Width = agrs.Bounds.Width - Indent;
                r.Y = Y;
                r.Height = CurrentRowHeight;
                SetDefaultBrickStyle(graph, Bricks["FooterPanel"]);

                //DictionaryEntry de = View.GetRowSummaryItem(ViewViewInfo.FooterInfo.RowHandle, agrs.Column);
                GridColumnSummaryItem gsi = agrs.Column.SummaryItem as GridColumnSummaryItem;
                string brickText = agrs.Column.SummaryItem.SummaryType != SummaryItemType.None && agrs.Column.SummaryItem.SummaryType != SummaryItemType.Count && agrs.Column.ColumnType.Equals(typeof(System.String)) && agrs.Column.SummaryItem.SummaryValue != null ? agrs.Column.SummaryItem.SummaryValue.ToString() : string.Empty;
                TextBrick brick = (TextBrick)DrawTextBrick(graph, brickText, r, true);
                if (gsi != null && agrs.Column.SummaryItem.SummaryType != SummaryItemType.None)
                {
                    brick.TextValue = brick.Value = gsi.SummaryValue;
                    brick.TextValueFormatString = gsi.DisplayFormat;
                }
            }
            Y += r.Height;
        }
    }
}
