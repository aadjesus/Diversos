using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Owf.Controls
{
	public partial class VistaProgressBar : UserControl
	{
		Color _border = Color.FromArgb(178, 178, 178);
		Color _backRemain1 = Color.FromArgb(202, 202, 202);
		Color _backRemain2 = Color.FromArgb(234, 234, 234);
		Color _backRemain3 = Color.FromArgb(219, 219, 219);
		Color _backRemain4 = Color.FromArgb(243, 243, 243);

		Color _backActive1 = Color.FromArgb(180, 0, 0);
		Color _backActive2 = Color.FromArgb(252, 0, 0);
		Color _backActive3 = Color.FromArgb(255, 127, 127);
		Color _backActive4 = Color.FromArgb(255, 205, 205);

		float _value = 50.0F;
		[Browsable(true)]
		public float Value
		{
			get { return _value; }
			set
			{
				if (_value > 100.0F) _value = 100.0F;
				if (_value < 1.0F) _value = 1.0F;
				_value = value;
				Invalidate();
			}
		}

		VistaProgressBarTheme _theme = VistaProgressBarTheme.Default;
		[Browsable(true)]
		public VistaProgressBarTheme Theme
		{
			get { return _theme; }
			set
			{
				_theme = value;
				CalculateThems();
				Invalidate();
			}
		}

		public VistaProgressBar()
		{
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.ResizeRedraw, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			InitializeComponent();
			this.BackColor = Color.Transparent;
			CalculateThems();
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);

			Rectangle rectUpper = new Rectangle(0, 0, this.Width, this.Height / 2 + 2);
			Rectangle rectLower = new Rectangle(0, this.Height / 2, this.Width, this.Height - (this.Height / 2));

			GraphicsPath pathLower = OwfGraphics.GetRoundPath(rectLower, 2);
			GraphicsPath pathUpper = OwfGraphics.GetRoundPath(rectUpper, 2);

			using (Brush brushUpper = new LinearGradientBrush(rectUpper, _backRemain4, _backRemain3, LinearGradientMode.Vertical))
			{
				e.Graphics.FillPath(brushUpper, pathUpper);
			}

			using (Brush brushLower = new LinearGradientBrush(rectLower, _backRemain1, _backRemain2, LinearGradientMode.Vertical))
			{
				e.Graphics.FillPath(brushLower, pathLower);
			}
		}

		private void VistaProgressBar_Paint(object sender, PaintEventArgs e)
		{
			float width = (((float)this.Width - 2) * _value) / 100.0F;

			Rectangle rectFull = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
			GraphicsPath pathFull = OwfGraphics.GetRoundPath(rectFull, 2);

			Rectangle rectUpper = new Rectangle(1, 1, (int)width, this.Height / 2 + 1);
			GraphicsPath pathUpper = OwfGraphics.GetRoundPath(rectUpper, 1);

			Rectangle rectLower = new Rectangle(1, this.Height / 2, (int)width, this.Height - (this.Height / 2) - 1);
			GraphicsPath pathLower = OwfGraphics.GetRoundPath(rectLower, 1);

			using (Brush brushUpper = new LinearGradientBrush(rectUpper, _backActive4, _backActive3, LinearGradientMode.Vertical))
			{
				e.Graphics.FillPath(brushUpper, pathUpper);
			}

			using (Brush brushLower = new LinearGradientBrush(rectLower, _backActive1, _backActive2, LinearGradientMode.Vertical))
			{
				e.Graphics.FillPath(brushLower, pathLower);
			}

			using (Pen pen = new Pen(_border))
			{
				e.Graphics.DrawPath(pen, pathFull);
			}
		}

		void CalculateThems()
		{
			switch (_theme)
			{
				case VistaProgressBarTheme.Red:
					_backActive1 = Color.FromArgb(180, 0, 0);
					_backActive2 = Color.FromArgb(252, 0, 0);
					_backActive3 = Color.FromArgb(255, 127, 127);
					_backActive4 = Color.FromArgb(255, 205, 205);
					break;

				case VistaProgressBarTheme.Default:
				case VistaProgressBarTheme.Green:
					_backActive1 = Color.FromArgb(12, 182, 20);
					_backActive2 = Color.FromArgb(55, 217, 60);
					_backActive3 = Color.FromArgb(117, 226, 119);
					_backActive4 = Color.FromArgb(171, 237, 171);
					break;

				case VistaProgressBarTheme.Blue:
					_backActive1 = Color.FromArgb(8, 49, 216);
					_backActive2 = Color.FromArgb(22, 106, 238);
					_backActive3 = Color.FromArgb(102, 171, 255);
					_backActive4 = Color.FromArgb(140, 192, 255);
					break;
			}
		}
	}

	public enum VistaProgressBarTheme
	{
		Default,
		Green,
		Blue,
		Red,
	}
}
