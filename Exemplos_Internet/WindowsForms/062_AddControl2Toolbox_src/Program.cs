using System;
using System.Runtime.InteropServices;

using EnvDTE;
using EnvDTE80;
using System.Windows.Forms;
using EnvDTE90;
using InstallToolboxControls;
using Microsoft.Win32;
using System.IO;


namespace RegComp2Tbox2003
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    class AddControl2Toolbox
    {
        [STAThread]
        static void Main(string[] args)
        {
            //NewMethod();

            //ToolboxExample();

            //AddToolboxItem("XXAle", "Special Test Component", @"c:\GlobusMais\Frameworks\FGlobus\Distribuicao\BgmRodotec.FGlobus.Componentes.WinForms.dll");

            //InstallToolboxControls.Program2.InstallControl();

            //Program2.InstallControl();
            // AddMyNewToolbar(@"c:\GlobusMais\Frameworks\FGlobus\Distribuicao\BgmRodotec.FGlobus.Componentes.WinForms.dll");

            //if (args == null)
            //{

            //}
            //new AddControl2Toolbox().xx2();

            //ToolboxExample();
            //xxx();

            Console.WriteLine("FIM");
            MessageBox.Show("fim");
            Console.ReadLine();
        }

        #region Anteriores

        static void xxx()
        {
            //Getting design time environment 
            DTE designTimeEnvironment = (DTE)Activator.CreateInstance(Type.GetTypeFromProgID("VisualStudio.DTE.10.0"), true);
            //Getting tool box object      
            ToolBox VSToolBox = (ToolBox)designTimeEnvironment.Windows.Item("{B1E99781-AB81-11D0-B683-00AA00A3EE26}").Object;
            ToolBoxTab MyTab = null;
            string TabName = "xxxxxxxxxxxxx";
            //checkin if Tab already exists   
            foreach (ToolBoxTab VSTab in VSToolBox.ToolBoxTabs)
            {
                if (VSTab.Name == TabName)
                {
                    MyTab = VSTab;
                    break;
                }
            }

            //If tab doesn't exist, creating new one
            if (null == MyTab)
                MyTab = VSToolBox.ToolBoxTabs.Add(TabName);

            MyTab.Activate();

            //ToolBoxItem tbi = MyTab.ToolBoxItems.Add("ButtonEditBGM,FGlobus.Componentes.WinForms", @"c:\Frameworks\FGlobus\Distribuicao\FGlobus.Componentes.WinForms.dll",vsToolBoxItemFormat.vsToolBoxItemFormatDotNETComponent);
            ToolBoxItem tbi = MyTab.ToolBoxItems.Add(
                TabName,
                @"c:\GlobusMais\Frameworks\FGlobus\Distribuicao\BgmRodotec.FGlobus.Componentes.WinForms.dll",
                vsToolBoxItemFormat.vsToolBoxItemFormatDotNETComponent);

            //tlbTab.ToolBoxItems.Add(txtTabName.Text, txtComponentPath.Text, vsToolBoxItemFormat.vsToolBoxItemFormatDotNETComponent);

            if (tbi == null)
            {
            }

            designTimeEnvironment.Quit();
        }

        private static void NewMethod()
        {
            //Getting design time environment
            DTE DesignTimeEnvironment = (DTE)Activator.CreateInstance(Type.GetTypeFromProgID("VisualStudio.DTE.10.0"), true);
            //Getting tool box object
            ToolBox VSToolBox = (ToolBox)DesignTimeEnvironment.Windows.Item("{B1E99781-AB81-11D0-B683-00AA00A3EE26}").Object;
            ToolBoxTab MyTab = null;
            string TabName = "MyComponents";

            //checkin if Tab already exists
            foreach (ToolBoxTab VSTab in VSToolBox.ToolBoxTabs)
            {
                if (VSTab.Name == TabName)
                {
                    MyTab = VSTab;
                    break;
                }
            }

            //If tab doesn't exist, creating new one
            if (null == MyTab)
                MyTab = VSToolBox.ToolBoxTabs.Add(TabName);
            MyTab.Activate();

            ToolBoxItem tbi = MyTab.ToolBoxItems.Add("ButtonEditBGM", @"c:\Frameworks\FGlobus\Distribuicao\FGlobus.Componentes.WinForms.dll", vsToolBoxItemFormat.vsToolBoxItemFormatDotNETComponent);

            if (tbi == null)
            {

            }

            DesignTimeEnvironment.Quit();


        }


        public static void ToolboxExample()
        {
            DTE dte = (DTE)Activator.CreateInstance(Type.GetTypeFromProgID("VisualStudio.DTE.10.0"), true);

            ToolBox tlBox = null;
            ToolBoxTabs tbxTabs = null;
            ToolBoxTab3 tbxTab = null;
            ToolBoxItems tbxItems = null;
            ToolBoxItem2 tbxItem = null;

            try
            {
                // Create an object reference to the IDE's ToolBox object and
                // its tabs.
                tlBox = (ToolBox)(dte.Windows.Item(Constants.vsWindowKindToolbox).Object);
                tbxTabs = tlBox.ToolBoxTabs;

                // Add a new tab to the Toolbox and select it.
                tbxTab = (ToolBoxTab3)tbxTabs.Add("New ToolBox Tab");
                tbxTab.Activate();

                // Add new items to the new Toolbox tab. This shows two
                // different ways to index the Toolbox tabs. The third item
                // added is a .NET component that contains a number of 
                // Web-related controls.
                tbxTab.ToolBoxItems.Add("Text Item", "Hello world", (EnvDTE.vsToolBoxItemFormat.vsToolBoxItemFormatText));
                tbxTab.ToolBoxItems.Add("HTML Item", "Hello world", vsToolBoxItemFormat.vsToolBoxItemFormatHTML);
                // Replace the <Path and name of a .NET dll>
                // with a path to a .NET dll file.
                tbxTabs.Item("New Toolbox Tab").ToolBoxItems.Add(@"c:\Frameworks\FGlobus\Distribuicao\FGlobus.Componentes.WinForms.dll", vsToolBoxItemFormat.vsToolBoxItemFormatDotNETComponent);

                // Use the ToolboxItems collection to access all the 
                // items under a ToolBox tab.
                tbxItems = tbxTab.ToolBoxItems;

                // List the number of ToolboxItems in a ToolBoxTab.
                MessageBox.Show("Number of items in " + tbxTabs.Item(1).Name + " tab: " + tbxItems.Count);

                // Select the second item in the ToolboxItems collection and 
                // delete it.
                // Comment the following lines out, if you do not want to
                // delete the controls.
                tbxItems.Item(2).Select();

                //tbxItems.SelectedItem.Delete();
                //MessageBox.Show("Number of items in " + tbxTabs.Item(1).Name + " tab: " + tbxItems.Count);
                //tbxTabs.Item("New ToolBox Tab").Delete();
                //MessageBox.Show("Tab deleted.");

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        public static void AddToolboxItem(string toolboxTabName, string toolBoxItemName, string toolBoxItemPath)
        {
            Type dteReference;
            EnvDTE.ToolBox toolBox;
            EnvDTE80.ToolBoxItem2 addedToolBoxItem;

            // Get a reference to the visual studio development environment.
            dteReference = Type.GetTypeFromProgID("VisualStudio.DTE.10.0");

            if (dteReference != null)
            {
                // Get a reference to the toolbox of the development environment.
                EnvDTE80.DTE2 dte = (EnvDTE80.DTE2)Activator.CreateInstance(dteReference);
                toolBox = (EnvDTE.ToolBox)dte.Windows.Item("{B1E99781-AB81-11D0-B683-00AA00A3EE26}").Object;

                // Loop through all tab pages to find the toolbox tab page that was specified
                // in the toolboxTabName parameter.
                foreach (EnvDTE80.ToolBoxTab2 toolBoxTab in toolBox.ToolBoxTabs)
                {

                    // Is this the right toolbox?
                    if (string.Compare(toolBoxTab.Name, toolboxTabName, true) == 0)
                    {

                        // First check if the component is not already in the toolbox:
                        bool found = false;
                        foreach (EnvDTE80.ToolBoxItem2 toolBoxItem in toolBoxTab.ToolBoxItems)
                        {
                            if (string.Compare(toolBoxItem.Name, toolBoxItemName, true) == 0)
                            {
                                found = true;
                                break;
                            }
                        }

                        // The toolbox item is not in the toolbox tab, add it:
                        if (!found)
                        {
                            addedToolBoxItem = (EnvDTE80.ToolBoxItem2)toolBoxTab.ToolBoxItems.Add(
                              toolBoxItemName,
                              toolBoxItemPath,
                              EnvDTE.vsToolBoxItemFormat.vsToolBoxItemFormatDotNETComponent);
                        }
                        break;
                    }
                }
            }
        }

        public static string AddMyNewToolbar(string dllPath)
        {

            try
            {

                // reference:
                //http://weblogs.asp.net/savanness/archive/2003/04/10/5352.aspx
                string result = "";
                Type latestDTE = Type.GetTypeFromProgID("VisualStudio.DTE.10.0");

                EnvDTE.DTE env = Activator.CreateInstance(latestDTE) as
                EnvDTE.DTE;
                try
                {
                    //dllPath = RuntimeEnvironment.GetRuntimeDirectory() + "bin\\MyControls.dll";
#if DEBUG
        Console.WriteLine("DLL Path is: " + dllPath);
#endif
                    MessageBox.Show("1");

                    // Prepare to munge the toolbox -- get toolbox window,
                    //object, and tabs collection
                    EnvDTE.Window toolboxWindow = env.Windows.Item(EnvDTE.Constants.vsWindowKindToolbox);
                    EnvDTE.ToolBox toolbox = (EnvDTE.ToolBox)toolboxWindow.Object;
                    EnvDTE.ToolBoxTabs toolboxTabs = toolbox.ToolBoxTabs;

                    // Careful to check if our tab already exists
                    foreach (EnvDTE.ToolBoxTab tab in toolboxTabs)
                    {
                        if (tab.Name == "MyNew")
                        {
                            Console.WriteLine("Toolbar exists!");
                            return result;
                        }
                    }

                    // No, so add it
                    EnvDTE.ToolBoxTab newtab = toolboxTabs.Add("MyNew");

                    MessageBox.Show(newtab.Name);

                    // WinBug: gotta show the Properties window, first
                    env.ExecuteCommand("View.PropertiesWindow", "");

                    // WinBug2: gotta activate the tab and select the first
                    //item
                    newtab.Activate();
                    newtab.ToolBoxItems.Item(0).Select();

                    MessageBox.Show("sss");

                    // Finally: add the controls
                    newtab.ToolBoxItems.Add(
                    "MyNew",
                    dllPath,
                    EnvDTE.vsToolBoxItemFormat.vsToolBoxItemFormatDotNETComponent
                    );

                    MessageBox.Show("sss = 1");

                    Console.WriteLine("Toolbar added successfully.");

                    // not sure if this line works, caveat emptor
                    env.Quit();

                    // env.Application.MainWindow.Close(EnvDTE.vsSaveChanges.vsSaveChangesNo);

                    MessageBox.Show("sss = 2");
                }
                catch (Exception exc)
                {

                    MessageBox.Show(String.Concat(exc));
                    Console.WriteLine("Add toolbar failed.\r\n\r\n" +
                    exc.Message);
                    return "Exception: " + exc.Message;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Concat(ex));
            }

            return "";
        }

        public static void xx1()
        {

            DTE DesignTimeEnvironment = (DTE)Activator.CreateInstance(Type.GetTypeFromProgID("VisualStudio.DTE.10.0"), true);
            //Getting tool box object      
            ToolBox VSToolBox = (ToolBox)DesignTimeEnvironment.Windows.Item("{B1E99781-AB81-11D0-B683-00AA00A3EE26}").Object;
            ToolBoxTab MyTab = null;
            string TabName = "MyComponents";
            //checkin if Tab already exists   
            foreach (ToolBoxTab VSTab in VSToolBox.ToolBoxTabs)
            {
                if (VSTab.Name == TabName)
                {
                    MyTab = VSTab;
                    break;
                }
            }      //If tab doesn't exist, creating new one 
            if (null == MyTab)
                MyTab = VSToolBox.ToolBoxTabs.Add(TabName);
            MyTab.Activate();
            ToolBoxItem tbi = MyTab.ToolBoxItems.Add("FileBrowser",
                @"MyComponents.FileBrowser, MyTestComps, Version=1.0.0.1, Culture=neutral, PublicKeyToken=2283de3658476795",
                vsToolBoxItemFormat.vsToolBoxItemFormatDotNETComponent);
            DesignTimeEnvironment.Quit();
        }

        #endregion

        public void xx2()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            _pathFileName = openFileDialog.FileName;

            this.InstallControlsToVisualStudioToolbox(this.GetVisualStudio2010Project);
        }

        internal delegate void CreateProjectDelegate(DTE dte);

        private string _pathFileName = "";
        private string _tabName = "BGMRodotec";

        private void GetVisualStudio2010Project(DTE dte)
        {
            const string TEMPLATENAME = "WindowsApplication.zip";
            const string DUMMYPROJECTNAME = "dummyproj";

            string tmpFile = Path.GetFileNameWithoutExtension(Path.GetTempFileName());
            string tmpDir = string.Format("{0}{1}", Path.GetTempPath(), tmpFile);


            EnvDTE100.Solution4 solution = dte.Solution as EnvDTE100.Solution4;

            string templatePath = solution.GetProjectTemplate(TEMPLATENAME, "CSharp");


            solution.AddFromTemplate(templatePath, tmpDir, DUMMYPROJECTNAME, false);
        }

        private void InstallControlsToVisualStudioToolbox(CreateProjectDelegate CreateProject)
        {
            string visualStudio = "VisualStudio.DTE.10.0";
            RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(visualStudio);

            if (registryKey != null)
            {

                DTE dte = (DTE)System.Activator.CreateInstance(System.Type.GetTypeFromProgID(visualStudio), true);
                try
                {
                    CreateProject.Invoke(dte);
                    MessageFilter.Register();

                    EnvDTE.Window window = dte.Windows.Item(EnvDTE.Constants.vsWindowKindToolbox);
                    EnvDTE.ToolBox toolbox = (EnvDTE.ToolBox)window.Object;
                    EnvDTE.ToolBoxTab toolBoxTab = toolbox.ToolBoxTabs.Add(this._tabName);
                    toolBoxTab.Activate();
                    toolBoxTab.ToolBoxItems.Add("dummy", this._pathFileName, vsToolBoxItemFormat.vsToolBoxItemFormatDotNETComponent);

                    //toolBoxTab.Delete();
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
}
