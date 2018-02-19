using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Markup;
using Catalyst.Windows.Primitives;

namespace Catalyst.Windows.Controls
{
    /// <summary>
    /// Displays calendar items (such as appointments) in a view similar to Microsoft Outlook
    /// </summary>
    /// <example>
    ///     <![CDATA[
    ///         <CalendarView ItemBeginBinding="{Binding Path=AppointmentStart}" ItemEndBinding="{Binding Path=AppointmentEnd}">
    ///             <CalendarView.Periods>
    ///                 <CalendarViewPeriod Header="Monday" Begin="03/02/2009 12:00 AM" End="03/03/2009 12:00 AM" />    
    ///                 <CalendarViewPeriod Header="Tuesday" Begin="03/03/2009 12:00 AM" End="03/04/2009 12:00 AM" />    
    ///                 <CalendarViewPeriod Header="Wednesday" Begin="03/04/2009 12:00 AM" End="03/05/2009 12:00 AM" />    
    ///                 <CalendarViewPeriod Header="Thursday" Begin="03/05/2009 12:00 AM" End="03/06/2009 12:00 AM" />    
    ///                 <CalendarViewPeriod Header="Friday" Begin="03/06/2009 12:00 AM" End="03/07/2009 12:00 AM" />    
    ///             </CalendarView.Periods>             
    ///         </CalendarView>
    ///     ]]>
    /// </example>
    [ContentPropertyAttribute("Periods")]
    [TemplatePart(Name="PART_Container", Type=typeof(CalendarViewPanel))]
    [TemplatePart(Name="PART_Header", Type=typeof(CalendarViewHeader))]
    public class CalendarView2 : Selector
    {
        private CalendarViewPeriodCollection periods;

        static CalendarView2()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CalendarView), new FrameworkPropertyMetadata(typeof(CalendarView)));
        }

        /// <summary>
        /// Gets or sets the data item to bind to for determining the the beginning of a calendar item.
        /// </summary>
        public BindingBase ItemBeginBinding { get; set; }

        /// <summary>
        /// Gets or sets the data item to bind to for determining the the end of a calendar item.
        /// </summary>
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

        public CalendarViewPanel Container
        {
            get { return (CalendarViewPanel)this.Template.FindName("PART_Container", this); }
        }

        public CalendarViewHeader Header
        {
            get { return (CalendarViewHeader)this.Template.FindName("PART_Header", this); }
        }

        public CalendarViewPeriod GetPeriodForElement(UIElement element)
        {
            if (!(element is CalendarViewItem))
                throw new ArgumentOutOfRangeException("element");

            CalendarViewItem item = (CalendarViewItem)element;

            foreach (CalendarViewPeriod period in Periods)
            {
                if ((item.Begin.Ticks >= period.Begin.Ticks) && (item.End.Ticks <= period.End.Ticks))
                    return period;
                else if ((item.Begin.Ticks < period.Begin.Ticks) && (item.End.Ticks >= period.Begin.Ticks))
                    return period;
            }

            return null;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new CalendarViewItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return (item is CalendarViewItem);
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            if (element is CalendarViewItem)
            {
                CalendarViewItem calendarViewItem = (CalendarViewItem)element;
                calendarViewItem.SetBinding(CalendarViewItem.BeginProperty, this.ItemBeginBinding);
                calendarViewItem.SetBinding(CalendarViewItem.EndProperty, this.ItemEndBinding);
            }
        }
    }
}
