using System;
using System.Windows.Forms;

/*
  Custom Type Converter Sample 2
  http://cyotek.com/blog/creating-a-custom-typeconverter-part-2
*/

namespace CustomTypeConverter2
{
  internal static class Program
  {
    #region Class Members

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }

    #endregion
  }
}
