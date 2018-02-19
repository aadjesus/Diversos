using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GraficosGrids
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pt-BR");

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ca-ES");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ca");

            Application.EnableVisualStyles();

            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MasterFormRelatorio1());
            Application.Run(new Form2());
        }
    }
}

