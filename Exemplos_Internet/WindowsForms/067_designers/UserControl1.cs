using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace CSharpTest
{
	[Designer(typeof(MyControlDesigner))]
	public class UserControl1 : System.Windows.Forms.UserControl
	{
		public UserControl1()
		{
			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, true);

			this.Paint += new PaintEventHandler(UserControl1_Paint);
		}

		private void UserControl1_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawEllipse(Pens.Red, ClientRectangle);
		}
	}

	internal class MyControlDesigner : System.Windows.Forms.Design.ControlDesigner
	{

		public override System.Windows.Forms.Design.SelectionRules SelectionRules
		{
			get
			{
				return System.Windows.Forms.Design.SelectionRules.LeftSizeable |
					System.Windows.Forms.Design.SelectionRules.RightSizeable |
					System.Windows.Forms.Design.SelectionRules.Visible |
					System.Windows.Forms.Design.SelectionRules.Moveable;
			}
		}

		public override bool CanBeParentedTo(System.ComponentModel.Design.IDesigner parentDesigner)
		{
			if (parentDesigner.Component is Panel)
				return false;
			else
				return true;
		}

		public override System.ComponentModel.Design.DesignerVerbCollection Verbs
		{
			get
			{
				DesignerVerbCollection v = new DesignerVerbCollection();

				v.Add(new DesignerVerb("Sample Verb", new EventHandler(SampleVerbHandler)));
				return v;
			}
		}

		private void SampleVerbHandler(object sender, System.EventArgs e)
		{
			MessageBox.Show("You clicked the test designer verb!");
		}
	}
}
