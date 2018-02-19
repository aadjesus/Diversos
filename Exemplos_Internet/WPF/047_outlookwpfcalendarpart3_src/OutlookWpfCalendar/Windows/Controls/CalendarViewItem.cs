using System;
using System.Windows;
using System.Windows.Controls;

namespace Catalyst.Windows.Controls
{
    /// <summary>
    /// Represents an individual item in a CalendarView control.
    /// </summary>
    public class CalendarViewItem: ContentControl
    {
        public static readonly DependencyProperty BeginProperty = DependencyProperty.Register("Begin", typeof(DateTime), typeof(CalendarViewItem));

        public static readonly DependencyProperty EndProperty = DependencyProperty.Register("End", typeof(DateTime), typeof(CalendarViewItem));

        static CalendarViewItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CalendarViewItem), new FrameworkPropertyMetadata(typeof(CalendarViewItem)));
        }

        /// <summary>
        /// This is a dependency property.
        /// </summary>
        public DateTime Begin
        {
            get { return (DateTime)this.GetValue(BeginProperty); }
            set { this.SetValue(BeginProperty, value); }
        }

        /// <summary>
        /// This is a dependency property.
        /// </summary>
        public DateTime End
        {
            get { return (DateTime)this.GetValue(EndProperty); }
            set { this.SetValue(EndProperty, value); }
        }
    }
}
