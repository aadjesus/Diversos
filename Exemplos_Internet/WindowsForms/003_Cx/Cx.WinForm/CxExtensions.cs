using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Cx.Interfaces;

namespace Cx.WinForm
{
	public static class CxWinFormExtensions
	{
		public static void RegisterControl(this Control uc, Form form, ICxVisualComponent comp)
		{
			string[] loc = comp.Location.Split(',');
			Point p = new Point(Convert.ToInt32(loc[0].Trim()), Convert.ToInt32(loc[1].Trim()));

			uc.Location = p;

			if (!String.IsNullOrEmpty(comp.Size))
			{
				string[] size = comp.Size.Split(',');
				Size s = new Size(Convert.ToInt32(size[0].Trim()), Convert.ToInt32(size[1].Trim()));
				uc.Size = s;
			}

			form.Controls.Add(uc);
		}
	}
}
