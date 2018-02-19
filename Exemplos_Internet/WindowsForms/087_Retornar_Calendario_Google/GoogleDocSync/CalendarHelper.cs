using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Calendar;
using Google.GData.Extensions;
using Google.GData.Client;

namespace GoogleCalendarHelper
{
    public class CalendarHelper
    {
        public string ApplicationName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public static CalendarService GetService(string applicationName, string userName, string password)
        {
            CalendarService service = new CalendarService(applicationName);
            service.setUserCredentials(userName, password);
            return service;
        }

        public static IEnumerable<EventEntry> GetAllEvents(CalendarService service, DateTime? startData)
        {
            // Create the query object:
            EventQuery query = new EventQuery();
            query.Uri = new Uri("http://www.google.com/calendar/feeds/" + service.Credentials.Username + "/private/full");
            if (startData != null)
                query.StartTime = startData.Value;

            // Tell the service to query:
            EventFeed calFeed = service.Query(query);
            return calFeed.Entries.Cast<EventEntry>();
        }

        public static void AddEvent(CalendarService service,  string title, string contents, string location, DateTime startTime, DateTime endTime)
        {
            EventEntry entry = new EventEntry();

            // Set the title and content of the entry.
            entry.Title.Text = title;
            entry.Content.Content = contents;

            // Set a location for the event.
            Where eventLocation = new Where();
            eventLocation.ValueString = location;
            entry.Locations.Add(eventLocation);

            When eventTime = new When(startTime, endTime);
            entry.Times.Add(eventTime);

            Uri postUri = new Uri("http://www.google.com/calendar/feeds/default/private/full");

            // Send the request and receive the response:
            AtomEntry insertedEntry = service.Insert(postUri, entry);
        }

        public static void ClearAll(CalendarService service)
        {
            var events = GetAllEvents(service, null);
            foreach (var eventEntry in events)
            {
                service.Delete(eventEntry);
            }
        }
    }
}
