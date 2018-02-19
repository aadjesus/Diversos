using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Sample
{
	public class SampleControl : System.Windows.Forms.UserControl
	{
		private System.ComponentModel.Container components = null;

		public SampleControl()
		{
			InitializeComponent();

			base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			base.SetStyle(ControlStyles.ResizeRedraw, true);
		}

		/// <summary> 
		/// Die verwendeten Ressourcen bereinigen.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Vom Komponenten-Designer generierter Code
		/// <summary> 
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

		[Editor(typeof(Design.ColorEditorEx), typeof(System.Drawing.Design.UITypeEditor))]
		public override Color BackColor
		{
			get { return base.BackColor; }
			set { base.BackColor = value; }
		}

		[Editor(typeof(Design.ColorEditorEx), typeof(System.Drawing.Design.UITypeEditor))]
		public override Color ForeColor
		{
			get { return base.ForeColor; }
			set { base.ForeColor = value; }
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			using (SolidBrush brush = new SolidBrush(base.ForeColor))
			{
				e.Graphics.DrawString("Hello World!", base.Font, brush, new RectangleF(0, 0, this.Width, this.Height), new StringFormat());
			}
		}

	}
}
