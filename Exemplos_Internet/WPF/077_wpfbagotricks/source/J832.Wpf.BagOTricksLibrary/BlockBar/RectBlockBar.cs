using System;
using System.Windows;
using System.Windows.Media;
using J832.Common;

namespace Microsoft.Samples.KMoore.WPFSamples.BlockBarSample
{
    public class RectBlockBar : BlockBar
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            Rect rect;
            int blockCount = BlockCount;
            Size renderSize = this.RenderSize;
            double blockMargin = this.BlockMargin;
            double value = Value;
            for (int i = 0; i < blockCount; i++)
            {
                rect = GetRect(renderSize, blockCount, blockMargin, i, ThePen.Thickness);

                if (!rect.IsEmpty)
                {
                    int threshold = GetThreshold(value, blockCount);
                    drawingContext.DrawRectangle((i < threshold) ? EnabledBrush : DisabledBrush, ThePen, rect);
                }
            }
        }

        private static Rect GetRect(Size targetSize, int blockCount, double blockMargin, int blockNumber, double penThickness)
        {
            Util.RequireArgument(!targetSize.IsEmpty, "targetSize");
            Util.RequireArgumentRange(blockCount > 0, "blockCount");
            Util.RequireArgumentRange(blockCount > blockNumber, "blockNumber");

            double width = (targetSize.Width - (blockCount - 1) * blockMargin - penThickness) / blockCount;
            double left = penThickness / 2 + (width + blockMargin) * blockNumber;
            double height = targetSize.Height - penThickness;

            if (width > 0 && height > 0)
            {
                return new Rect(left, penThickness / 2, width, height);
            }
            else
            {
                return Rect.Empty;
            }
        }
    }
}
