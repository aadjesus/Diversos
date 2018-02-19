using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Sysbytes.ControlLibrary
{
    public partial class SBGradientPanel : Panel
    {
        private Color _gradientColor1 = SystemColors.ControlLight;

        [Category("Appearance")]
        public Color GradientColor1
        {
            get { return _gradientColor1; }
            set
            {
                _gradientColor1 = value;
                this.Invalidate();
            }
        }

        private Color _gradientColor2 = SystemColors.Control;

        [Category("Appearance")]
        public Color GradientColor2
        {
            get { return _gradientColor2; }
            set
            {
                _gradientColor2 = value;
                this.Invalidate();
            }
        }

        private LinearGradientMode _gradientDirection = LinearGradientMode.Vertical;

        [Category("Appearance")]
        public LinearGradientMode GradientDirection
        {
            get { return _gradientDirection; }
            set
            {
                _gradientDirection = value;
                this.Invalidate();
            }
        }

        public SBGradientPanel()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            LinearGradientBrush brBackground = new LinearGradientBrush(
                this.ClientRectangle, this.GradientColor1, this.GradientColor2,
                this.GradientDirection);
            e.Graphics.FillRectangle(brBackground, this.ClientRectangle);
        }
    }
}
