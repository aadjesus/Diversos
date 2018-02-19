using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors.Drawing;
using System.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors;

namespace _testpaintcontrol
{
    public class ListEditPainter : ButtonEditPainter
    {
        protected override void DrawText(ControlGraphicsInfoArgs info)
        {
            TextEditViewInfo vi = info.ViewInfo as ButtonEditViewInfo;
			if(vi.AllowDrawText && !vi.MaskBoxRect.IsEmpty)
            {
                Rectangle r = new Rectangle(vi.MaskBoxRect.X + 20, vi.MaskBoxRect.Y, vi.MaskBoxRect.Width - 20, vi.MaskBoxRect.Height);

				bool textDrawn = false;
				if(vi.IsSupportIncrementalSearch && vi.MatchedString.Length > 0) {
					string text = vi.DisplayText.ToLower();
					string matched = vi.MatchedString.ToLower();
					if(text == matched || BaseEdit.StringStartsWidth(text, matched)) {
						textDrawn = true;
						DrawMatchedString(info, r, vi.DisplayText, vi.MatchedString, vi.PaintAppearance, vi.IsInvertIncrementalSearchString);
					}

				}
				
				if(!textDrawn) {
					if(BaseEditViewInfo.ShowFieldBindings && vi.GetDataBindingText() != "") return;
					DrawString(info, r, vi.DisplayText, vi.PaintAppearance);
				}
			}
        }

        protected override void DrawFocusRect(ControlGraphicsInfoArgs info)
        {
            base.DrawFocusRect(info);
            //  info.Graphics.DrawImage(Properties.Resources.barcode16, new Point(2, 2));
            info.Cache.Paint.DrawImage(info.Cache.Graphics, Properties.Resources.barcode16, new Rectangle(2, 2, 16, 16),
                new Rectangle(0, 0, Properties.Resources.barcode16.Width, Properties.Resources.barcode16.Height),
                true);
        }
    }
}
