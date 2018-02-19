using System.Windows;
using System.Windows.Controls;

namespace OutlookWpfCalendar.Windows.Controls
{
    public class RangePanel : Panel
    {
        public static DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(double), typeof(RangePanel), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsArrange));
        public static DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(double), typeof(RangePanel), new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.AffectsArrange));
        public static DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(RangePanel), new FrameworkPropertyMetadata(Orientation.Vertical, FrameworkPropertyMetadataOptions.AffectsArrange));

        public static DependencyProperty BeginProperty = DependencyProperty.RegisterAttached("Begin", typeof(double), typeof(UIElement), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsArrange));
        public static DependencyProperty EndProperty = DependencyProperty.RegisterAttached("End", typeof(double), typeof(UIElement), new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.AffectsArrange));

        public static void SetBegin(UIElement element, double value)
        {
            element.SetValue(BeginProperty, value);
        }

        public static double GetBegin(UIElement element)
        {
            return (double)element.GetValue(BeginProperty);
        }

        public static void SetEnd(UIElement element, double value)
        {
            element.SetValue(EndProperty, value);
        }

        public static double GetEnd(UIElement element)
        {
            return (double)element.GetValue(EndProperty);
        }

        public double Maximum
        {
            get { return (double)this.GetValue(MaximumProperty); }
            set { this.SetValue(MaximumProperty, value); }
        }

        public double Minimum
        {
            get { return (double)this.GetValue(MinimumProperty); }
            set { this.SetValue(MinimumProperty, value); }
        }

        public Orientation Orientation
        {
            get { return (Orientation)this.GetValue(OrientationProperty); }
            set { this.SetValue(OrientationProperty, value); }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double containerRange = (this.Maximum - this.Minimum);

            foreach (UIElement element in this.Children)
            {
                double begin = (double)element.GetValue(RangePanel.BeginProperty);
                double end = (double)element.GetValue(RangePanel.EndProperty);
                double elementRange = end - begin;

                Size size = new Size();
                size.Width = (Orientation == Orientation.Vertical) ? finalSize.Width : elementRange / containerRange * finalSize.Width;
                size.Height = (Orientation == Orientation.Vertical) ? elementRange / containerRange * finalSize.Height : finalSize.Height;

                Point location = new Point();
                location.X = (Orientation == Orientation.Vertical) ? 0 : (begin - this.Minimum) / containerRange * finalSize.Width;
                location.Y = (Orientation == Orientation.Vertical) ? (begin - this.Minimum) / containerRange * finalSize.Height : 0;

                element.Arrange(new Rect(location, size));
            }

            return finalSize;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement element in this.Children)
            {
                element.Measure(availableSize);
            }

            return availableSize;
        }
    }
}