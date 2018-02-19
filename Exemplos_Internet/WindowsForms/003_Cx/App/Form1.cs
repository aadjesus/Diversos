using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;		   
using System.Text;
using System.Windows;
using System.Windows.Forms;

using Cx.Interfaces;

namespace App
{
	public partial class Form1 : Form
	{
		public Form1(List<ICxVisualComponent> visualComponents)
		{
			InitializeComponent();

			foreach (ICxVisualComponent comp in visualComponents)
			{
				string[] loc=comp.Location.Split(',');
				Point p = new Point(Convert.ToInt32(loc[0].Trim()), Convert.ToInt32(loc[1].Trim()));
				Control ctrl = (Control)((ICxComponent)comp).Instance;
				ctrl.Location = p;
				Controls.Add(ctrl);
			}
		}
	}
}
