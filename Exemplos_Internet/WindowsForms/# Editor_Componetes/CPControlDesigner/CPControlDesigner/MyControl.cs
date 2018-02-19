/*
 * Erstellt mit SharpDevelop.
 * Benutzer: hmurray
 * Datum: 01.07.2010
 * Zeit: 23:19
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace CPControlDesigner
{
	/// <summary>
	/// Description of MyControl.
	/// </summary>
	[Designer(typeof(Design.MyControlDesigner))]
	[ToolboxItem(true)]
	public class MyControl : UserControl
	{
		public MyControl()
		{
		}
		
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			
			try{
				e.Graphics.FillRectangle(Brushes.YellowGreen,e.ClipRectangle);
				e.Graphics.DrawRectangle(Pens.Black,e.ClipRectangle);
				e.Graphics.DrawString("MyControl is prompting for its Name by drop on DesignerHost",this.Font,new SolidBrush(this.ForeColor),new PointF(2,2));
			}
			catch{}
		}
	}
}
