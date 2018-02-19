using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace OutlookCalendar.Model
{
    public class Appointments : ObservableCollection<Appointment>
    {
        public Appointments()
        {
            Add(new Appointment() { Subject = "Dummy Appointment #1", StartTime = new DateTime(2008, 10, 21, 12, 00, 00), EndTime = new DateTime(2008, 10, 21, 14, 00, 00) });
            Add(new Appointment() { Subject = "Dummy Appointment #2", StartTime = new DateTime(2008, 10, 22, 11, 30, 00), EndTime = new DateTime(2008, 10, 22, 12, 00, 00) });
            Add(new Appointment() { Subject = "Dummy Appointment #3", StartTime = new DateTime(2008, 10, 21, 16, 00, 00), EndTime = new DateTime(2008, 10, 21, 17, 30, 00) });
        }
    }
}
