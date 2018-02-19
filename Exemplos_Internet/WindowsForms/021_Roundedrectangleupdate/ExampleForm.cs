using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Plasmoid.Extensions
{
	public partial class ExampleForm : Form
	{
		public ExampleForm()
		{
			InitializeComponent();
		}

		private void ExampleForm_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.FillRoundedRectangle(new SolidBrush(ControlPaint.Dark(SystemColors.GradientActiveCaption, 0.2f)), 10, 10, this.Width - 40, this.Height - 60, 10);
			LinearGradientBrush brush = new LinearGradientBrush(
				new Point(this.Width/2, 0),
				new Point(this.Width/2, this.Height),
				SystemColors.GradientInactiveCaption,
				ControlPaint.Dark(SystemColors.GradientActiveCaption, 0.5f)
				);
			g.FillRoundedRectangle(brush, 12, 12, this.Width - 44, this.Height - 64, 10);
			g.DrawRoundedRectangle(new Pen(ControlPaint.Light(SystemColors.InactiveBorder, 0.00f)), 12, 12, this.Width - 44, this.Height - 64, 10);
			g.FillRoundedRectangle(new SolidBrush(Color.FromArgb(100, 70, 130, 180)), 12, 12 + ((this.Height - 64) / 2), this.Width - 44, (this.Height - 64)/2, 10);

		}

		private void ExampleForm_Resize(object sender, EventArgs e)
		{
			this.Invalidate();
		}
	}
}
