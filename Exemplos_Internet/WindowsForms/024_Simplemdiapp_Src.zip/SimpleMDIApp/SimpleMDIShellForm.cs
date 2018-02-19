using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.Utility;
using Microsoft.Practices.CompositeUI.Services;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI.WinForms;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace SimpleMDIApp
{
    public partial class SimpleMDIShellForm : Form
    {
        private WorkItem m_workItem;
        private IWorkItemTypeCatalogService m_workItemTypeCatalog;
        private IWorkspace m_mdiWorkspace;

        public SimpleMDIShellForm()
        {
            InitializeComponent();
        }

       [InjectionConstructor]
        public SimpleMDIShellForm(WorkItem workItem, IWorkItemTypeCatalogService workItemTypeCatalog) : this()
        {
            this.m_workItem = workItem;
            this.m_workItemTypeCatalog = workItemTypeCatalog;
            m_mdiWorkspace = new MdiWorkspace(this);
        }

        [CommandHandler("FileExit")]
        public void OnFileExit(object sender, EventArgs e)
        {
            Close();
        }

        [CommandHandler("FileNewContact")]
        public void OnFileNew(object sender, EventArgs e)
        {
            ContactWorkItem contactWorkItem = m_workItem.WorkItems.AddNew<ContactWorkItem>();
            contactWorkItem.Show(ContentWorkspace);            
        }
        
        public IWorkspace ContentWorkspace
        {
            get {return this.m_mdiWorkspace;}
        }
    }
}
