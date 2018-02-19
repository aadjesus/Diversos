using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using OutlookWpfCalendar.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Collections.Generic;

namespace OutlookWpfCalendar.Windows.Primitives
{
    public class CalendarViewPeriodPresenter: Panel
    {
        private bool visualChildrenGenerated;
        private UIElementCollection visualChildren;

        static CalendarViewPeriodPresenter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CalendarViewPeriodPresenter), new FrameworkPropertyMetadata(typeof(CalendarViewPeriodPresenter)));
        }

        public CalendarViewPeriod Period { get; set; }

        public ListView ListView { get; set; }

        public CalendarView CalendarView { get; set; }

        private CalendarViewContentPresenter ContentPresenter
        {
            get
            {
                return (CalendarViewContentPresenter)this.Parent;
            }
        }

        protected override int VisualChildrenCount
        {
            get
            {
                if (this.visualChildren == null)
                    return base.VisualChildrenCount;

                return this.visualChildren.Count;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            if ((index < 0) || (index >= this.VisualChildrenCount))
                throw new ArgumentOutOfRangeException("index", index, "Index out of range");

            if (this.visualChildren == null)
                return base.GetVisualChild(index);

            return this.visualChildren[index];
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (UIElement element in this.visualChildren)
                element.Arrange(new Rect(new Point(0, 0), finalSize));

            return finalSize;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            this.GenerateVisualChildren();

            return constraint;
        }

        protected void GenerateVisualChildren()
        {
            if (visualChildrenGenerated)
                return;

            if (this.visualChildren == null)
                this.visualChildren = this.CreateUIElementCollection(null);
            else
                this.visualChildren.Clear();

            RangePanel panel = new RangePanel();
            panel.SetBinding(RangePanel.MinimumProperty, new Binding("Begin.Ticks") { Source = Period });
            panel.SetBinding(RangePanel.MaximumProperty, new Binding("End.Ticks") { Source = Period });

            foreach (ListViewItem item in this.ContentPresenter.ListViewItemVisuals)
            {
                if (this.CalendarView.PeriodContainsItem(item, this.Period))
                {
                    item.SetValue(RangePanel.BeginProperty, Convert.ToDouble(((DateTime)item.GetValue(CalendarView.BeginProperty)).Ticks));
                    item.SetValue(RangePanel.EndProperty, Convert.ToDouble(((DateTime)item.GetValue(CalendarView.EndProperty)).Ticks));
                    panel.Children.Add(item);
                }
            }

            Border border = new Border() { BorderBrush = Brushes.Blue, BorderThickness = new Thickness(1.0) };
            border.Child = panel;
            visualChildren.Add(border);

            this.visualChildrenGenerated = true;
        }
    }
}
