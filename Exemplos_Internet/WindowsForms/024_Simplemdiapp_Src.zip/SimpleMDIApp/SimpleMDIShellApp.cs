using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.WinForms;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.Services;

namespace SimpleMDIApp
{
    public class SimpleMDIShellApp : FormShellApplication<WorkItem, SimpleMDIShellForm>
    {
        [STAThread]
        public static void Main()
        {
            new SimpleMDIShellApp().Run();
        }

        protected override void AfterShellCreated()
        {
            base.AfterShellCreated();

            // Register the UIExtensionSites
            ToolStripMenuItem fileItem = (ToolStripMenuItem)Shell.MainMenuStrip.Items["fileToolStripMenuItem"];
            RootWorkItem.UIExtensionSites.RegisterSite("FileDropDown", fileItem.DropDownItems);

            StatusStrip statusStrip = (StatusStrip)Shell.statusStrip;
            RootWorkItem.UIExtensionSites.RegisterSite("StatusStrip", statusStrip);
            
            // Load the menu structure from App.config
            UIElementBuilder.LoadFromConfig(RootWorkItem);
        }
    }
}