using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsFormsApplication1
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
            Application.Run(new WindowsFormsApplication1.FiltroRelatorio.FormFiltroRelatorio());
            //Application.Run(new FormTelaAguarde());



        }

        private static void frmMain_Load()
        {
            Process[] processos = Process.GetProcesses();
            foreach (Process processo in processos)
            {
                if (processo.ProcessName == Process.GetCurrentProcess().ProcessName &&
                !Process.GetCurrentProcess().Equals(processo))
                {
                    processo.StartInfo.FileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    processo.Start();
                    Process.GetCurrentProcess().Kill();
                }
            }
        }
    }
}
