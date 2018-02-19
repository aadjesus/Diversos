// -----------------------------------------------------------------------
// <copyright file="DteToolboxInstaller.cs" company="ComponentOwl.com">
//     Copyright © 2010-2012 ComponentOwl.com. All rights reserved.
// </copyright>
// <author>Libor Tinka</author>
// -----------------------------------------------------------------------

namespace ComponentOwl.ToolboxIntegration
{
    #region Usings

    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;

    using EnvDTE;

    using EnvDTE100;

    using EnvDTE80;

    using EnvDTE90;

    using Microsoft.Win32;

    using Process = System.Diagnostics.Process;
    using Thread = System.Threading.Thread;
    using System.Windows.Forms;

    #endregion

    /// <summary>
    ///   Adds or removes control from Visual Studio Toolbox using DTE.
    /// </summary>
    public sealed partial class DteToolboxInstaller : IDisposable
    {
        #region Private Constants

        private const string TemplateName = "WindowsApplication.zip";
        private const string DummyProjectName = "DummyProject";
        private const int VISUAL_STUDIO_PROCESS_TIME_OUT = 2000;
        private const int VISUAL_STUDIO_PROCESS_TRY_COUNT = 4;

        #endregion

        #region Private Fields

        private readonly string assemblyPath;
        private readonly string tabName;

        #endregion

        #region Public Constructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="DteToolboxInstaller" /> class.
        /// </summary>
        /// <param name="assemblyPath"> Path of DLL to install/uninstall. </param>
        /// <param name="tabName"> Toolbox tab name. </param>
        public DteToolboxInstaller(string assemblyPath, string tabName)
        {
            this.assemblyPath = assemblyPath;
            this.tabName = tabName;

            // hook into OLE messages
            NativeMethods.CoRegisterMessageFilter(this, out this.oldOleFilter);
        }

        #endregion

        #region Public Methods

        
        /// <summary>
        ///   Install controls in the Visual Studio Toolbox.
        /// </summary>
        /// <param name="version"> Version of Visual Studio to integrate controls with. </param>
        /// <returns> Exit code for the application. </returns>
        public ExitCode InstallControls(Form formulario, VisualStudioVersion version)
        {
            
            ExitCode exitCode;
            MessageBox.Show("1");
            
            if (IsVisualStudioRunning())
            {
                MessageBox.Show(String.Concat(2));
                exitCode = ExitCode.VisualStudioRunning;
            }
            else if (IsVisualStudioInstalled(version) == false)
            {
                exitCode = ExitCode.Fail;
                MessageBox.Show(String.Concat(3));
            }
            else
            {
                DTE dte = GetDTE(version);
                MessageBox.Show(String.Concat(4));
                try
                {
                    GetVisualStudioProject(dte, version);

                    Window window = dte.Windows.Item(Constants.vsWindowKindToolbox);
                    ToolBox toolBox = (ToolBox)window.Object;
                    ToolBoxTab toolBoxTab = (GetToolBoxTab(toolBox.ToolBoxTabs) ?? toolBox.ToolBoxTabs.Add(this.tabName));

                    toolBoxTab.Activate();

                    MessageBox.Show(String.Concat(5));
                    GetToolBoxItem(toolBoxTab, true);

                    exitCode = ExitCode.Success;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(String.Concat("Failed to install controls in Visual Studio Toolbox.", Environment.NewLine, exception));

                    exitCode = ExitCode.Fail;
                }
                finally
                {
                    MessageBox.Show(String.Concat(6));
                    dte.Solution.Close(false);
                    dte.Quit();
                    Marshal.ReleaseComObject(dte);

                    MessageBox.Show(String.Concat(7));
                    WaitForVisualStudioExit();
                }
            }

            return exitCode;
        }

        /// <summary>
        ///   Uninstall controls from the Visual Studio Toolbox.
        /// </summary>
        /// <param name="version"> Version of Visual Studio to remove controls from. </param>
        /// <returns> Exit code for the application. </returns>
        public ExitCode UninstallControls(VisualStudioVersion version)
        {
            ExitCode exitCode;

            if (IsVisualStudioRunning())
            {
                exitCode = ExitCode.VisualStudioRunning;
            }
            else if (
                IsVisualStudioInstalled(version) == false)
            {
                exitCode = ExitCode.Fail;
            }
            else
            {
                DTE dte = GetDTE(version);

                try
                {
                    GetVisualStudioProject(dte, version);

                    Window window = dte.Windows.Item(Constants.vsWindowKindToolbox);
                    ToolBox toolBox = (ToolBox)window.Object;
                    ToolBoxTab toolBoxTab = GetToolBoxTab(toolBox.ToolBoxTabs);

                    if (toolBoxTab != null)
                    {
                        toolBoxTab.Activate();

                        ToolBoxItem toolBoxItem = GetToolBoxItem(toolBoxTab, false);

                        if (toolBoxItem != null)
                        {
                            toolBoxItem.Delete();
                        }

                        if (toolBoxTab.ToolBoxItems.Count == 0 ||
                            (toolBoxTab.ToolBoxItems.Count == 1 && toolBoxTab.ToolBoxItems.Item(1).Name == "Pointer"))
                        {
                            toolBoxTab.Delete();
                        }
                    }

                    exitCode = ExitCode.Success;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(String.Concat("Failed to uninstall controls in Visual Studio Toolbox.", Environment.NewLine, exception));

                    exitCode = ExitCode.Fail;
                }
                finally
                {
                    dte.Solution.Close(false);
                    dte.Quit();
                    Marshal.ReleaseComObject(dte);

                    WaitForVisualStudioExit();
                }
            }

            return exitCode;
        }

        #endregion

        #region Private Methods

        private void GetVisualStudioProject(DTE dte, VisualStudioVersion version)
        {
            string tempFile = Path.GetFileNameWithoutExtension(Path.GetTempFileName());
            string tempDirectory = string.Format("{0}{1}", Path.GetTempPath(), tempFile);

            switch (version)
            {
                case VisualStudioVersion.Vs2005:
                {
                    Solution2 solution = (dte.Solution as Solution2);

                    string templatePath = solution.GetProjectTemplate(TemplateName, "CSharp");

                    solution.AddFromTemplate(templatePath, tempDirectory, DummyProjectName, false);
                }
                    break;
                case VisualStudioVersion.Vs2008:
                {
                    Solution3 solution = (dte.Solution as Solution3);

                    string templatePath = solution.GetProjectTemplate(TemplateName, "CSharp");

                    solution.AddFromTemplate(templatePath, tempDirectory, DummyProjectName, false);
                }
                    break;
                case VisualStudioVersion.Vs2010:
                case VisualStudioVersion.Vs2012:
                {
                    Solution4 solution = (dte.Solution as Solution4);

                    string templatePath = solution.GetProjectTemplate(TemplateName, "CSharp");

                    solution.AddFromTemplate(templatePath, tempDirectory, DummyProjectName, false);
                }
                    break;
                default:
                    throw (new ApplicationException(String.Format("Unknown Visual Studio version: '{0}'.", version)));
            }
        }

        private void WaitForVisualStudioExit()
        {
            for (int tryIndex = 0; tryIndex < VISUAL_STUDIO_PROCESS_TRY_COUNT; tryIndex++)
            {
                if (IsVisualStudioRunning())
                {
                    Thread.Sleep(VISUAL_STUDIO_PROCESS_TIME_OUT);
                }
                else
                {
                    break;
                }
            }
        }

        private bool IsVisualStudioRunning()
        {
            Process[] processList = Process.GetProcesses();

            foreach (Process process in processList)
            {
                if (process.ProcessName.StartsWith("devenv"))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsVisualStudioInstalled(VisualStudioVersion version)
        {
            string keyName;

            switch (version)
            {
                case VisualStudioVersion.Vs2005:
                    keyName = "VisualStudio.DTE.8.0";
                    break;
                case VisualStudioVersion.Vs2008:
                    keyName = "VisualStudio.DTE.9.0";
                    break;
                case VisualStudioVersion.Vs2010:
                    keyName = "VisualStudio.DTE.10.0";
                    break;
                case VisualStudioVersion.Vs2012:
                    keyName = "VisualStudio.DTE.11.0";
                    break;
                default:
                    throw (new ApplicationException(String.Format("Unknown Visual Studio version: '{0}'.", version)));
            }

            return (Registry.ClassesRoot.OpenSubKey(keyName) != null);
        }

        private ToolBoxTab GetToolBoxTab(ToolBoxTabs toolBoxTabs)
        {
            foreach (ToolBoxTab toolBoxTab in toolBoxTabs)
            {
                if (String.Equals(toolBoxTab.Name, this.tabName, StringComparison.Ordinal))
                {
                    return toolBoxTab;
                }
            }

            return null;
        }

        private ToolBoxItem GetToolBoxItem(ToolBoxTab toolBoxTab, bool replace)
        {
            string assemblyName = AssemblyName.GetAssemblyName(this.assemblyPath).Name;

            ToolBoxItem toolBoxItem = null;

            foreach (ToolBoxItem toolBoxItemCurrent in toolBoxTab.ToolBoxItems)
            {
                if (toolBoxItemCurrent.Name.Equals(assemblyName, StringComparison.Ordinal))
                {
                    toolBoxItem = toolBoxItemCurrent;

                    break;
                }
            }

            if (replace)
            {
                if (toolBoxItem != null)
                {
                    // delete the component first, because we may want to replace it with newer version (or same component with different path)
                    toolBoxItem.Delete();
                }

                toolBoxItem = toolBoxTab.ToolBoxItems.Add(assemblyName, this.assemblyPath, vsToolBoxItemFormat.vsToolBoxItemFormatDotNETComponent);
            }

            return toolBoxItem;
        }

        private DTE GetDTE(VisualStudioVersion version)
        {
            Type typeDTE;

            switch (version)
            {
                case VisualStudioVersion.Vs2005:
                    typeDTE = Type.GetTypeFromProgID("VisualStudio.DTE.8.0");
                    break;
                case VisualStudioVersion.Vs2008:
                    typeDTE = Type.GetTypeFromProgID("VisualStudio.DTE.9.0");
                    break;
                case VisualStudioVersion.Vs2010:
                    typeDTE = Type.GetTypeFromProgID("VisualStudio.DTE.10.0");
                    break;
                case VisualStudioVersion.Vs2012:
                    typeDTE = Type.GetTypeFromProgID("VisualStudio.DTE.11.0");
                    break;
                default:
                    throw (new ApplicationException(String.Format("Unknown Visual Studio version: '{0}'.", version)));
            }

            return (DTE)Activator.CreateInstance(typeDTE, true);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);

            // stop listening for OLE messages
            NativeMethods.CoRegisterMessageFilter(null, out this.oldOleFilter);

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        #endregion
    }
}