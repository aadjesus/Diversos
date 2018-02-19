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

namespace OutlookCalendar.Controls
{
    public class CalendarDay : ItemsControl
    {
        static CalendarDay()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CalendarDay), new FrameworkPropertyMetadata(typeof(CalendarDay)));
        }

        #region ItemsControl Container Override

        protected override DependencyObject GetContainerForItemOverride()
        {            
            return new CalendarAppointmentItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return (item is CalendarAppointmentItem);
        }

        #endregion
    }
}
