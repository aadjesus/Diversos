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
    public class CalendarLedgerItem : Control
    {
        static CalendarLedgerItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CalendarLedgerItem), new FrameworkPropertyMetadata(typeof(CalendarLedgerItem)));
        }

        #region TimeslotA

        public static readonly DependencyProperty TimeslotAProperty =
            DependencyProperty.Register("TimeslotA", typeof(string), typeof(CalendarLedgerItem),
                new FrameworkPropertyMetadata((string)string.Empty));

        public string TimeslotA
        {
            get { return (string)GetValue(TimeslotAProperty); }
            set { SetValue(TimeslotAProperty, value); }
        }

        #endregion

        #region TimeslotB

        public static readonly DependencyProperty TimeslotBProperty =
            DependencyProperty.Register("TimeslotB", typeof(string), typeof(CalendarLedgerItem),
                new FrameworkPropertyMetadata((string)string.Empty));

        public string TimeslotB
        {
            get { return (string)GetValue(TimeslotBProperty); }
            set { SetValue(TimeslotBProperty, value); }
        }

        #endregion
    }
}
