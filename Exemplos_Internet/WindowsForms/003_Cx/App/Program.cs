using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using Cx;
using Cx.Designer;

namespace App
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			CxApp cx = CxApp.Initialize(@"..\..\..\Cx.DataService\bin\debug\Cx.DataService.dll", Path.GetFullPath("cx.xml"));
			Application.Run(new Form1(cx.VisualComponents));
		}
	}
}
