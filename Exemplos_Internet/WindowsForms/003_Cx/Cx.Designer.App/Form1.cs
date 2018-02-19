using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Cx.Interfaces;

namespace Cx.Designer.App
{
	public partial class Form1 : Form
	{
		public Form1(List<ICxVisualComponent> visualComponents)
		{
			InitializeComponent();
			Size extents=new Size(0, 0);
			Activated += new EventHandler(OnActivated);

			foreach (ICxVisualComponent comp in visualComponents)
			{
				ICxVisualComponentClass instance = (ICxVisualComponentClass)((ICxComponent)comp).Instance;
				instance.Register(this, comp);
				extents = Program.UpdateExtents(comp, extents);
			}

			Size = extents + new Size(25, 40);
		}

		protected void OnActivated(object sender, System.EventArgs e)
		{
			Program.currentForm = this;
		}
	}
}
