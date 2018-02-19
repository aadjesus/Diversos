// ******************************************************************
//
//	If this code works it was written by:
//		Henry Minute
//		MamSoft / Manniff Computers
//		© 2008 - 2009...
//
//	if not, I have no idea who wrote it.
//
// ******************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace NestedControlDesignerTest
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
			Application.Run(new NestedControlTestForm());
		}
	}
}
