using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSynchronizer;
using GenericEntities;
using GoogleAdapter;


namespace TestCalendar
{
    class Program
    {
        
        static void Main(string[] args)
        {
            SyncServiceManager service = new SyncServiceManager();
            service.SyncService();
        }
    }

    class SyncServiceManager
    {
        SyncManager<GenericEvent> eventManager = new SyncManager<GenericEvent>();
        public void SyncService()
        {
            //Initialize Event Manager
            eventManager.Name = "..Event Synchronizer";
            eventManager.Source1 = new GoogleEventsSyncDataSource();

            //change the time if required
            //eventManager.Synchronize(DateTime.Now);
        }
    }
}
