//=============================================================================
// System  : EWSoftware Divider Label
// File    : DividerLabel.cs
// Author  : Eric Woodruff
// Updated : 01/29/2007
// Note    : Copyright 2006-2007, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a simple derived label control that draws the label text
// followed by a dividing line to the right of the text.
//
// This code may be used in compiled form in any way you desire.  This file
// may be redistributed unmodified by any means PROVIDING it is not sold for
// profit without the author's written consent, and providing that this notice
// and the author's name and all copyright notices remain intact.
//
// This code is provided "as is" with no warranty either express or implied.
// The author accepts no liability for any damage or loss of business that
// this product may cause.
//
// Version     Date     Who  Comments
// ============================================================================
// 1.0.0.0  08/02/2006  EFW  Created the code
//=============================================================================

using System;
using System.Drawing;
using System.Drawing.Text;
using System.ComponentModel;
using System.Windows.Forms;

// All classes go in the SandcastleBuilder.Utils.Controls namespace
namespace SandcastleBuilder.Utils.Controls
{
	/// <summary>
    /// This is a simple derived label control that draws the label text
    /// followed by a dividing line to the right of the text.
	/// </summary>
    [Description("A label that draws a dividing line to the right of the text")]
    public class DividerLabel : System.Windows.Forms.Label
	{
        /// <summary>
        /// Constructor
        /// </summary>
		public DividerLabel()
		{
		}

        //=====================================================================
        // Methods, etc

        /// <summary>
        /// This is overridden to draw the text and the dividing line
        /// </summary>
        /// <param name="e">The event arguments</param>
		protected override void OnPaint(PaintEventArgs e)
		{
    		Point pt, pt2;
            Rectangle r = base.ClientRectangle;
            SizeF size;
            Graphics g = e.Graphics;

            using(StringFormat sf = new StringFormat())
            {
                sf.Trimming = StringTrimming.Character;
                sf.Alignment = StringAlignment.Near;
                sf.LineAlignment = StringAlignment.Center;

                if(this.RightToLeft == RightToLeft.Yes)
                    sf.FormatFlags |= StringFormatFlags.DirectionRightToLeft;

                if(this.UseMnemonic)
                    sf.HotkeyPrefix = HotkeyPrefix.Show;

                size = g.MeasureString(this.Text, this.Font,
                    (SizeF)r.Size, sf);

                if(base.Enabled)
                {
                    using(Brush fb = new SolidBrush(this.ForeColor))
                    {
                        g.DrawString(this.Text, this.Font, fb,
                            (RectangleF)r, sf);
                    }
                }
                else
                    ControlPaint.DrawStringDisabled(g, this.Text,
                        this.Font, this.BackColor, (RectangleF)r, sf);

                pt = new Point(r.Left, r.Top + r.Height / 2);
                pt2 = new Point(r.Right, pt.Y);

                if(this.RightToLeft == RightToLeft.Yes)
                    pt2.X -= (int) size.Width;
                else
                    pt.X += (int) size.Width;

                using(Pen pb = new Pen(ControlPaint.Dark(this.BackColor),
                    (float)SystemInformation.BorderSize.Height))
                {
                    if(base.FlatStyle == FlatStyle.Flat)
                        g.DrawLine(pb, pt, pt2);
                    else
                    {
                        using(Pen pf = new Pen(
                            ControlPaint.LightLight(this.BackColor),
                            (float)SystemInformation.BorderSize.Height))
                        {
                            g.DrawLine(pb, pt, pt2);

                            int offset = (int)Math.Ceiling((double)
                                SystemInformation.BorderSize.Height / 2.0);
                            pt.Offset(0, offset);
                            pt2.Offset(0, offset);

                            g.DrawLine(pf, pt, pt2);
                        }
                    }
                }
            }
		}
	}
}
