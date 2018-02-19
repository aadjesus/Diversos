using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericEntities;
using System.Timers;
using GoogleAdapter;
using System.Collections;

namespace DataSynchronizer
{
    public class SyncService
    {
        SyncManager<GenericEvent> eventManager = new SyncManager<GenericEvent>();
        public string LastUpdateDate;
        public SyncService()
        {
            //Initialize Event Manager
            eventManager.Name = "..Event Synchronizer";
            eventManager.Source1 = new GoogleEventsSyncDataSource();
        }

        public IList  SynchronizeEvents()
        {
            IList list = eventManager.Synchronize(DateTime.Now).ToList();
            return list;
        }
    }
}
