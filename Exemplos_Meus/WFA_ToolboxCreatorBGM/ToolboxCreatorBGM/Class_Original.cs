using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using EnvDTE;
using System.Reflection;
using Microsoft.Win32;
using System.Drawing;

namespace appRadiusControlToolboxInstaller
{
    class ToolboxInstaller
    {
        private string _tmpDir;
        private string _tmpFile;
        private string _assemblyPath;
        private string _tabName;
        private const string TEMPLATENAME = "WindowsApplication.zip";
        private const string DUMMYPROJECTNAME = "dummyproj";

        internal delegate void CreateProjectDelegate(DTE dte);

        internal ToolboxInstaller(string AssemblyPath, string TabName)
        {
            this._assemblyPath = AssemblyPath;
            this._tabName = TabName;

        }

        internal void InstallControls()
        {
            if (!this.IsVisualStudioRunning())
            {
                if (this.IsVS2005Installed())
                    this.InstallControlsToVisualStudioToolbox(this.GetVisualStudio2005Project, System.Type.GetTypeFromProgID("VisualStudio.DTE.8.0"));
                if (this.IsVS2008Installed())
                    this.InstallControlsToVisualStudioToolbox(this.GetVisualStudio2008Project, System.Type.GetTypeFromProgID("VisualStudio.DTE.9.0"));
                if (this.IsVS2010Installed())
                    this.InstallControlsToVisualStudioToolbox(this.GetVisualStudio2010Project, System.Type.GetTypeFromProgID("VisualStudio.DTE.10.0"));
            }
        }

        private void InstallControlsToVisualStudioToolbox(CreateProjectDelegate CreateProject, Type DTEType)
        {
            DTE dte = (DTE)System.Activator.CreateInstance(DTEType, true);
            try
            {
                CreateProject.Invoke(dte);
                MessageFilter.Register();

                //EnvDTE.Window window = dte.Windows.Item(EnvDTE.Constants.vsWindowKindToolbox);
                EnvDTE.Window window = dte.Windows.Item("{B1E99781-AB81-11D0-B683-00AA00A3EE26}");
                
                EnvDTE.ToolBox toolbox = (EnvDTE.ToolBox)window.Object;
                EnvDTE.ToolBoxTab myTab = toolbox.ToolBoxTabs.Add(this._tabName);

                myTab.Activate();

                if (File.Exists(this._assemblyPath))
                {
                    myTab.ToolBoxItems.Add("dummy", this._assemblyPath, vsToolBoxItemFormat.vsToolBoxItemFormatDotNETComponent);
                }
            }
            catch
            {
            }
            finally
            {
                if (dte != null)
                {
                    dte.Solution.Close(false);
                    dte.Quit();
                    Marshal.ReleaseComObject(dte);
                    MessageFilter.Revoke();
                }
            }
        }

        internal void UninstallControls()
        {
            if (!this.IsVisualStudioRunning())
            {
                if (this.IsVS2005Installed())
                    this.UninstallControlsFromVisualStudioToolbox(this.GetVisualStudio2005Project, System.Type.GetTypeFromProgID("VisualStudio.DTE.8.0"));
                if (this.IsVS2008Installed())
                    this.UninstallControlsFromVisualStudioToolbox(this.GetVisualStudio2008Project, System.Type.GetTypeFromProgID("VisualStudio.DTE.9.0"));
                if (this.IsVS2010Installed())
                    this.UninstallControlsFromVisualStudioToolbox(this.GetVisualStudio2010Project, System.Type.GetTypeFromProgID("VisualStudio.DTE.10.0"));
            }
        }

        private void UninstallControlsFromVisualStudioToolbox(CreateProjectDelegate CreateProject, Type DTEType)
        {
            DTE dte = (DTE)System.Activator.CreateInstance(DTEType, true);
            try
            {

                CreateProject.Invoke(dte);
                MessageFilter.Register();

                EnvDTE.Window window = dte.Windows.Item("{B1E99781-AB81-11D0-B683-00AA00A3EE26}");
                EnvDTE.ToolBox toolbox = (EnvDTE.ToolBox)window.Object;
                EnvDTE.ToolBoxTab myTab = toolbox.ToolBoxTabs.Item(this._tabName);

                myTab.Activate();
                myTab.Delete();



            }
            catch
            {
            }
            finally
            {
                if (dte != null)
                {
                    dte.Solution.Close(false);
                    dte.Quit();
                    Marshal.ReleaseComObject(dte);
                    MessageFilter.Revoke();
                }
            }
        }

        private void GetVisualStudio2010Project(DTE dte)
        {
            this._tmpFile = Path.GetFileNameWithoutExtension(Path.GetTempFileName());
            this._tmpDir = string.Format("{0}{1}", Path.GetTempPath(), this._tmpFile);
            EnvDTE100.Solution4 solution = dte.Solution as EnvDTE100.Solution4;

            string templatePath = solution.GetProjectTemplate(TEMPLATENAME, "CSharp");
            solution.AddFromTemplate(templatePath, this._tmpDir, DUMMYPROJECTNAME, false);
        }

        private void GetVisualStudio2008Project(DTE dte)
        {
            this._tmpFile = Path.GetFileNameWithoutExtension(Path.GetTempFileName());
            this._tmpDir = string.Format("{0}{1}", Path.GetTempPath(), this._tmpFile);
            EnvDTE90.Solution3 solution = dte.Solution as EnvDTE90.Solution3;

            string templatePath = solution.GetProjectTemplate(TEMPLATENAME, "CSharp");
            solution.AddFromTemplate(templatePath, this._tmpDir, DUMMYPROJECTNAME, false);
        }

        private void GetVisualStudio2005Project(DTE dte)
        {
            this._tmpFile = Path.GetFileNameWithoutExtension(Path.GetTempFileName());
            this._tmpDir = string.Format("{0}{1}", Path.GetTempPath(), this._tmpFile);
            EnvDTE80.Solution2 solution = dte.Solution as EnvDTE80.Solution2;

            string templatePath = solution.GetProjectTemplate(TEMPLATENAME, "CSharp");
            solution.AddFromTemplate(templatePath, this._tmpDir, DUMMYPROJECTNAME, false);
        }

        private bool IsVisualStudioRunning()
        {
            System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process process in processList)
            {
                if (process.ProcessName.StartsWith("devenv"))
                    return true;
            }
            return false;
        }

        private bool RegistryKeyExists(string KeyName)
        {
            RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(KeyName);
            return (registryKey != null);

        }

        private bool IsVS2010Installed()
        {
            return this.RegistryKeyExists("VisualStudio.DTE.10.0");
        }

        private bool IsVS2008Installed()
        {
            return this.RegistryKeyExists("VisualStudio.DTE.9.0");
        }

        private bool IsVS2005Installed()
        {
            return this.RegistryKeyExists("VisualStudio.DTE.8.0");
        }

    }

    public class MessageFilter : IOleMessageFilter
    {
        //
        // Class containing the IOleMessageFilter
        // thread error-handling functions.

        // Start the filter.
        public static void Register()
        {
            IOleMessageFilter newFilter = new MessageFilter();
            IOleMessageFilter oldFilter = null;
            CoRegisterMessageFilter(newFilter, out oldFilter);
        }

        // Done with the filter, close it.
        public static void Revoke()
        {
            IOleMessageFilter oldFilter = null;
            CoRegisterMessageFilter(null, out oldFilter);
        }

        //
        // IOleMessageFilter functions.
        // Handle incoming thread requests.
        int IOleMessageFilter.HandleInComingCall(int dwCallType,
          System.IntPtr hTaskCaller, int dwTickCount, System.IntPtr
          lpInterfaceInfo)
        {
            //Return the flag SERVERCALL_ISHANDLED.
            return 0;
        }

        // Thread call was rejected, so try again.
        int IOleMessageFilter.RetryRejectedCall(System.IntPtr
          hTaskCallee, int dwTickCount, int dwRejectType)
        {
            if (dwRejectType == 2)
            // flag = SERVERCALL_RETRYLATER.
            {
                // Retry the thread call immediately if return >=0 & 
                // <100.
                return 99;
            }
            // Too busy; cancel call.
            return -1;
        }

        int IOleMessageFilter.MessagePending(System.IntPtr hTaskCallee,
          int dwTickCount, int dwPendingType)
        {
            //Return the flag PENDINGMSG_WAITDEFPROCESS.
            return 2;
        }

        // Implement the IOleMessageFilter interface.
        [DllImport("Ole32.dll")]
        private static extern int
          CoRegisterMessageFilter(IOleMessageFilter newFilter, out 
          IOleMessageFilter oldFilter);
    }

    [ComImport(), Guid("00000016-0000-0000-C000-000000000046"),
    InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    interface IOleMessageFilter
    {
        [PreserveSig]
        int HandleInComingCall(
            int dwCallType,
            IntPtr hTaskCaller,
            int dwTickCount,
            IntPtr lpInterfaceInfo);

        [PreserveSig]
        int RetryRejectedCall(
            IntPtr hTaskCallee,
            int dwTickCount,
            int dwRejectType);

        [PreserveSig]
        int MessagePending(
            IntPtr hTaskCallee,
            int dwTickCount,
            int dwPendingType);
    }
}



//namespace appRadiusControlToolboxInstaller
//{
//    class ProgramX
//    {
//        //[STAThread]
//        static void MainX(string[] args)
//        {
//            string assemblyPath = "";
//            string tabName = "";
//            bool Install = true;
//            foreach (string arg in args)
//            {
//                if (arg.StartsWith("AssemblyPath=", StringComparison.OrdinalIgnoreCase))
//                {
//                    string[] arrAssemblyPath = arg.Split(new char[] { '='}, 2);
//                    if (arrAssemblyPath != null && arrAssemblyPath.Length > 0)
//                    {
//                        assemblyPath = arrAssemblyPath[1];
//                        continue;
//                    }
//                }
//                if (arg.StartsWith("TabName=", StringComparison.OrdinalIgnoreCase))
//                {
//                    string[] arrTabName = arg.Split(new char[] { '=' }, 2);
//                    if (arrTabName != null && arrTabName.Length > 0)
//                    {
//                        tabName = arrTabName[1];
//                        continue;
//                    }
//                }
//                if (arg.Equals("install", StringComparison.OrdinalIgnoreCase))
//                {
//                    Install = true;
//                    continue;
//                }
//                if (arg.Equals("uninstall", StringComparison.OrdinalIgnoreCase))
//                {
//                    Install = false;
//                    continue;
//                }
//            }
 
//            if (!String.IsNullOrEmpty(tabName) && !String.IsNullOrEmpty(assemblyPath))
//            {
//                ToolboxInstaller toolBoxInstaller = new ToolboxInstaller(assemblyPath, tabName);
//                if (Install)
//                {
//                    toolBoxInstaller.InstallControls();
//                }
//                else
//                {
//                    toolBoxInstaller.UninstallControls();
//                }
//            }
//        }
//    }
//}