using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace Tray_minimizer
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
            Mutex mt = null;
            try
            {
                mt = Mutex.OpenExisting("Tray minimizer");
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                
            }
            if (mt == null)
            {
                mt = new Mutex(true, "Tray minimizer");
                Application.Run(new Form1());
                GC.KeepAlive(mt);
                mt.ReleaseMutex();
            }
            else
            {
                mt.Close();
                MessageBox.Show("Application already running");
                Application.Exit();
            }
        }
    }
}