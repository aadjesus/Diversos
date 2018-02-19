using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Asaasoft.DigitalMeter
{
    public class CounterAnimation : StringAnimationBase
    {
        #region Dependency Properties
        public string From
        {
            get
            {
                return (string)GetValue(FromProperty);
            }
            set
            {
                SetValue(FromProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for From.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FromProperty =
            DependencyProperty.Register("From", typeof(string), typeof(CounterAnimation), new UIPropertyMetadata("0"));

        public string To
        {
            get
            {
                return (string)GetValue(ToProperty);
            }
            set
            {
                SetValue(ToProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for To.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register("To", typeof(string), typeof(CounterAnimation), new UIPropertyMetadata("0"));
        #endregion

        #region Overridden Methods

        protected override string GetCurrentValueCore( string defaultOriginValue, string defaultDestinationValue, AnimationClock animationClock )
        {
            if ( To.Contains("#") )
                return To;

            TimeSpan? current = animationClock.CurrentTime;
            int precision = To.Length;
            int scalingFactor = 0;
            if ( To.IndexOf('.') > 0 )
            {
                precision--;
                scalingFactor = precision - To.IndexOf('.');
            }

            decimal from = 0;

            if ( !string.IsNullOrEmpty(From) )
            {
                if ( !From.ToString().Contains("#") )
                    from = Convert.ToDecimal(From);
                else
                {
                    string max = "".PadLeft(precision, '9');
                    if ( scalingFactor > 0 )
                        max = max.Insert(precision - scalingFactor, ".");
                    from = Convert.ToDecimal(max);
                }


            }

            decimal to = Convert.ToDecimal(To);
            decimal increase = 0;
            if ( Duration.HasTimeSpan && current.Value.Ticks > 0 )
            {
                decimal factor = (decimal)current.Value.Ticks / (decimal)Duration.TimeSpan.Ticks;
                increase = ( to - from ) * factor;
            }

            from += increase;

            return HelperClass.FormatDecimalValue(from, precision, scalingFactor);
        }

        protected override Freezable CreateInstanceCore()
        {
            return new CounterAnimation();
        }

        #endregion
    }
}
