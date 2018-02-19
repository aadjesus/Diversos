using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using EnvDTE;
using EnvDTE80;
using System.IO;

namespace InstallToolboxControls
{

    // Definition of the IMessageFilter interface which we need to implement and 

    // register with the CoRegisterMessageFilter API.

    [ComImport(), Guid("00000016-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]

    interface IOleMessageFilter // Renamed to avoid confusion w/ System.Windows.Forms.IMessageFilter
    {
        [PreserveSig]
        int HandleInComingCall(int dwCallType, IntPtr hTaskCaller, int dwTickCount, IntPtr lpInterfaceInfo);

        [PreserveSig]
        int RetryRejectedCall(IntPtr hTaskCallee, int dwTickCount, int dwRejectType);

        [PreserveSig]
        int MessagePending(IntPtr hTaskCallee, int dwTickCount, int dwPendingType);
    }


    public class Program2 : IOleMessageFilter
    {
        [DllImport("ole32.dll")]

        private static extern int CoRegisterMessageFilter(IOleMessageFilter newFilter, out IOleMessageFilter oldFilter);

        static string ctrlPath = "<Path to the control dll>";

        //[STAThread]
        static void Main1(string[] args)
        {

            Program2 program2 = new Program2();
            program2.Register();

            if (args[0].Equals("-Install", StringComparison.CurrentCultureIgnoreCase))
                InstallControl();


            else if (args[0].Equals("-UnInstall", StringComparison.CurrentCultureIgnoreCase))
                program2.UninstallControl();
            program2.Revoke();


            // to ensure the dte object is actually released, and the devenv.exe process terminates.

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }


        public static int InstallControl()
        {

            // Create an instance of the VS IDE,
            Type type = System.Type.GetTypeFromProgID("VisualStudio.DTE.10.0");

            DTE dte = (DTE)System.Activator.CreateInstance(type, true);

            // create a temporary winform project;

            //string tmpFile = Path.GetFileNameWithoutExtension(Path.GetTempFileName());

            //string tmpDir = string.Format("{0}{1}", Path.GetTempPath(), tmpFile);

            //Solution2 solution = dte.Solution as Solution2;

            //string templatePath = solution.GetProjectTemplate("WindowsApplication.zip", "CSharp");

            //Project proj = solution.AddFromTemplate(templatePath, tmpDir, "dummyproj", false);

            // add the control to the toolbox.

            try
            {
                EnvDTE.Window window = dte.Windows.Item(EnvDTE.Constants.vsWindowKindToolbox);
                EnvDTE.ToolBox toolbox = (EnvDTE.ToolBox)window.Object;
                EnvDTE.ToolBoxTab myTab = toolbox.ToolBoxTabs.Add("xxxxxxxxxxxxxxxxxx");
                myTab.Activate();
                myTab.ToolBoxItems.Add("xxxxxxxxxxxxxxxxxx", @"c:\GlobusMais\Frameworks\FGlobus\Distribuicao\BgmRodotec.FGlobus.Componentes.WinForms.dll", vsToolBoxItemFormat.vsToolBoxItemFormatDotNETComponent);
                dte.Solution.Close(false);
                Marshal.ReleaseComObject(dte);

                Console.WriteLine("Control Installed!!!");

            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Concat(ex));
            }
            return 0;
        }


        void UninstallControl()
        {


            Type type = System.Type.GetTypeFromProgID("VisualStudio.DTE.8.0");

            DTE dte = (DTE)System.Activator.CreateInstance(type, true);

            EnvDTE.Window window = dte.Windows.Item(EnvDTE.Constants.vsWindowKindToolbox);
            EnvDTE.ToolBox toolbox = (EnvDTE.ToolBox)window.Object;
            EnvDTE.ToolBoxTab myTab = toolbox.ToolBoxTabs.Item("<Toolbox Tabname>");
            myTab.Activate();
            myTab.Delete();


            Marshal.ReleaseComObject(dte);

            Console.WriteLine("Control Uninstalled!!!");
        }


        void Register()
        {
            IOleMessageFilter oldFilter;
            CoRegisterMessageFilter(this, out oldFilter);
        }


        void Revoke()
        {
            IOleMessageFilter oldFilter;
            CoRegisterMessageFilter(null, out oldFilter);
        }


        #region IOleMessageFilter Members

        public int HandleInComingCall(int dwCallType, IntPtr hTaskCaller, int dwTickCount, IntPtr lpInterfaceInfo)
        {

            return 0; //SERVERCALL_ISHANDLED

        }

        public int RetryRejectedCall(IntPtr hTaskCallee, int dwTickCount, int dwRejectType)
        {


            if (dwRejectType == 2) // SERVERCALL_RETRYLATER

                return 200; // wait 2 seconds and try again

            return -1; // cancel call

        }

        public int MessagePending(IntPtr hTaskCallee, int dwTickCount, int dwPendingType)
        {


            return 2; //PENDINGMSG_WAITDEFPROCESS

        }

        #endregion

    }
}