using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericEntities;
using DataSynchronizer;
using GoogleCalendarHelper;
using Google.GData.Calendar;

namespace GoogleAdapter
{
    public class GoogleEventsSyncDataSource : ISyncDataSource<GenericEvent>
    {
        CalendarService service;

        public GoogleEventsSyncDataSource()
        {
            service = CalendarHelper.GetService("AAA", "aadjesus@hotmail.com", "9251ajic");
        }

        #region ISyncDataSource<GenericEvent> Members

        public string Id
        {
            get { return "Google Calander Sync Data Source"; }
        }

        public IEnumerable<GenericEvent> GetItemHeaders(DateTime? lastSyncTime)
        {
            var googleEvents = CalendarHelper.GetAllEvents(service, lastSyncTime);
            List<GenericEvent> genericEvents = new List<GenericEvent>();
            foreach (var googleEvent in googleEvents)
            {
                GenericEvent genericEvent = new GenericEvent();
                genericEvent.Title = googleEvent.Title.Text;
                genericEvent.Contents = googleEvent.Content.Content;
                genericEvent.Location = googleEvent.Locations.First().ValueString;
                genericEvent.StartTime = googleEvent.Times.First().StartTime;
                genericEvent.EndTime = googleEvent.Times.First().EndTime;
                genericEvents.Add(genericEvent);
            }
            return genericEvents;
        }

        public void LoadItemContents(IEnumerable<GenericEvent> items)
        {
            //Nothing to load here
        }

        public void WriteItems(IEnumerable<GenericEvent> items)
        {
            foreach (var item in items)
            {
                CalendarHelper.AddEvent(service, item.Title, item.Contents, item.Location, item.StartTime, item.EndTime);
            }
        }

        #endregion
    }
}
