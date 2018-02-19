using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

namespace OutlookCalendar.Controls
{
    public class CalendarTimeslotItem : ButtonBase
    {
        static CalendarTimeslotItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CalendarTimeslotItem), new FrameworkPropertyMetadata(typeof(CalendarTimeslotItem)));
        }

        #region AddAppointment

        private void RaiseAddAppointmentEvent()
        {
            OnAddAppointment(new RoutedEventArgs(AddAppointmentEvent, this));
        }

        public static readonly RoutedEvent AddAppointmentEvent = 
            EventManager.RegisterRoutedEvent("AddAppointment", RoutingStrategy.Bubble, 
            typeof(RoutedEventHandler), typeof(CalendarTimeslotItem));

        public event RoutedEventHandler AddAppointment
        {
            add
            {
                AddHandler(AddAppointmentEvent, value);
            }
            remove
            {
                RemoveHandler(AddAppointmentEvent, value);
            }
        }

        protected virtual void OnAddAppointment(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        #endregion

        protected override void OnClick()
        {
            base.OnClick();

            RaiseAddAppointmentEvent();
        }

    }
}
