using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Controls;

namespace OutlookWpfCalendar.Windows.Controls
{
    [TemplatePart(Name="PART_Header", Type=typeof(TextBlock))]
    [TemplatePart(Name="PART_TimeScale", Type=typeof(TextBlock))]
    [TemplatePart(Name="PART_ContentPresenter", Type=typeof(TextBlock))]
    public class CalendarView : ViewBase
    {
        public static DependencyProperty BeginProperty = DependencyProperty.RegisterAttached("Begin", typeof(DateTime), typeof(ListViewItem));
        public static DependencyProperty EndProperty = DependencyProperty.RegisterAttached("End", typeof(DateTime), typeof(ListViewItem));

        private CalendarViewPeriodCollection periods;

        public BindingBase ItemBeginBinding { get; set; }

        public BindingBase ItemEndBinding { get; set; }

        public CalendarViewPeriodCollection Periods
        {
            get
            {
                if (periods == null)
                    periods = new CalendarViewPeriodCollection();

                return periods;
            }
        }

        protected override object DefaultStyleKey
        {
            get { return new ComponentResourceKey(GetType(), "DefaultStyleKey"); }
        }

        protected override object ItemContainerDefaultStyleKey
        {
            get { return new ComponentResourceKey(this.GetType(), "ItemContainerDefaultStyleKey"); }
        }

        public static DateTime GetBegin(DependencyObject item)
        {
            return (DateTime)item.GetValue(BeginProperty);
        }

        public static DateTime GetEnd(DependencyObject item)
        {
            return (DateTime)item.GetValue(EndProperty);
        }

        public static void SetBegin(DependencyObject item, DateTime value)
        {
            item.SetValue(BeginProperty, value);
        }

        public static void SetEnd(DependencyObject item, DateTime value)
        {
            item.SetValue(EndProperty, value);
        }

        protected override void PrepareItem(ListViewItem item)
        {
            item.SetBinding(BeginProperty, ItemBeginBinding);
            item.SetBinding(EndProperty, ItemEndBinding);
        }

        public bool PeriodContainsItem(ListViewItem item, CalendarViewPeriod period)
        {
            DateTime itemBegin = (DateTime)item.GetValue(BeginProperty);
            DateTime itemEnd = (DateTime)item.GetValue(EndProperty);

            return (((itemBegin <= period.Begin) && (itemEnd >= period.Begin)) || ((itemBegin <= period.End) && (itemEnd >= period.Begin)));
        }
    }
}
