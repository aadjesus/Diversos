using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using Framework.MP3PlayerModule.WorkItems;

namespace Framework.MP3PlayerModule
{
    public class Module :ModuleInit
    {
        private WorkItem _rootWorkItem;

        [InjectionConstructor]
        public Module([ServiceDependency] WorkItem rootWorkItem)
        {
            _rootWorkItem = rootWorkItem;
        }

        public override void Load()
        {
            base.Load();
            CreateMP3PlayerWorkItem();
        }

        private void CreateMP3PlayerWorkItem()
        {
            MP3PlayerWorkItem mp3PlayerWorkItem = _rootWorkItem.WorkItems.AddNew<MP3PlayerWorkItem>();
            mp3PlayerWorkItem.Run();
        }

    }
}
