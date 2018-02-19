using System;
using System.Collections.Generic;
using System.Text;
using SCSFContrib.CompositeUI.WPF;
using Microsoft.Practices.CompositeUI;
using Framework.Shell.Constants;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace Framework.Shell
{
    public sealed class Program : ApplicationShellApplication<WorkItem, App> 
    {

        [STAThread]
        public static void Main()
        {
            new Program().Run();
        }

        protected override void AfterShellCreated()
        {
            base.AfterShellCreated();
            CreateMainShellWindow();
        }

        private void CreateMainShellWindow()
        {
            ShellWindow shellWindow = new ShellWindow();
            Shell.MainWindow = shellWindow;
            shellWindow.Show();

            RootWorkItem.Workspaces.Add(shellWindow._mainShellWorkspace, WorkspaceNames.MAIN_WORKSPACE);
        }
    }
}
