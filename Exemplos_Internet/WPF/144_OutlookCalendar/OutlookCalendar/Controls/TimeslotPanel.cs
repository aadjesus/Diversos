using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace OutlookCalendar.Controls
{
    public class TimeslotPanel : Panel
    {
        #region StartTime

        /// <summary>
        /// StartTime Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty StartTimeProperty =
            DependencyProperty.RegisterAttached("StartTime", typeof(DateTime), typeof(TimeslotPanel),
                new FrameworkPropertyMetadata((DateTime)DateTime.Now));

        /// <summary>
        /// Gets the StartTime property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static DateTime GetStartTime(DependencyObject d)
        {
            return (DateTime)d.GetValue(StartTimeProperty);
        }

        /// <summary>
        /// Sets the StartTime property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetStartTime(DependencyObject d, DateTime value)
        {
            d.SetValue(StartTimeProperty, value);
        }

        #endregion

        #region EndTime

        /// <summary>
        /// EndTime Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty EndTimeProperty =
            DependencyProperty.RegisterAttached("EndTime", typeof(DateTime), typeof(TimeslotPanel),
                new FrameworkPropertyMetadata((DateTime)DateTime.Now));

        /// <summary>
        /// Gets the EndTime property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static DateTime GetEndTime(DependencyObject d)
        {
            return (DateTime)d.GetValue(EndTimeProperty);
        }

        /// <summary>
        /// Sets the EndTime property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetEndTime(DependencyObject d, DateTime value)
        {
            d.SetValue(EndTimeProperty, value);
        }

        #endregion

        
        protected override Size MeasureOverride(Size availableSize)
        {
            // Calculate size based on duration?
            Size size = new Size(double.PositiveInfinity, double.PositiveInfinity);

            foreach (UIElement element in this.Children)
            {
                element.Measure(size);
            }

            return new Size();
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double top = 0;
            double left = 0;
            double width = 0;
            double height = 0;

            foreach (UIElement element in this.Children)
            {
                Nullable<DateTime> startTime = element.GetValue(TimeslotPanel.StartTimeProperty) as Nullable<DateTime>;
                Nullable<DateTime> endTime = element.GetValue(TimeslotPanel.EndTimeProperty) as Nullable<DateTime>;

                double start_minutes = (startTime.Value.Hour * 60) + startTime.Value.Minute;
                double end_minutes = (endTime.Value.Hour * 60) + endTime.Value.Minute;
                double start_offset = (finalSize.Height / (24 * 60)) * start_minutes;
                double end_offset = (finalSize.Height / (24 * 60)) * end_minutes;

                top = start_offset+1;

                width = finalSize.Width;
                height = (end_offset - start_offset)-2;

                element.Arrange(new Rect(left, top, width, height));
            }

            return finalSize;
        }
    }
}
