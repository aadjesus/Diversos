using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using EnvDTE;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using DevExpress.XtraWaitForm;
using System.Threading;

namespace ToolboxCreatorBGM
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            bool verificaMutex;
            Mutex mutex = new Mutex(true, "A7CEEF1F-D8C5-43B4-B395-F122ECA014BE", out verificaMutex);
            GC.KeepAlive(mutex);

            if (verificaMutex)
            {

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                FrmToolboxCreatorBGM frmToolboxCreatorBGM = new FrmToolboxCreatorBGM();
                frmToolboxCreatorBGM.Visible = false;
                Application.Run(frmToolboxCreatorBGM);
                GC.KeepAlive(mutex);
            }
            else
                MessageBox.Show("A aplicação já esta aberta.");

        }

    }
}
