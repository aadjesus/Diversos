using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;

namespace SimpleMDIApp
{
    public class SimpleMDIModuleInit : ModuleInit
    {
        private WorkItem workItem;

        [InjectionConstructor]
        public SimpleMDIModuleInit([ServiceDependency] WorkItem workItem)
        {
            this.workItem = workItem;
        }

        public override void AddServices()
        {
            base.AddServices();
        }

        public override void Load()
        {
            base.Load();
        }
    }
}