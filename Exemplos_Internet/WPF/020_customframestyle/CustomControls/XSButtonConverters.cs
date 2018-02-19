//        Another Demo from Andy L. & MissedMemo.com
// Borrow whatever code seems useful - just don't try to hold
// me responsible for any ill effects. My demos sometimes use
// licensed images which CANNOT legally be copied and reused.

using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;


namespace CustomControls
{

    public class BrightnessToColorConverter : IValueConverter
    {
        #region Implementation

        public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {
            byte brightness = (byte)value;

            return Color.FromArgb( brightness, 255, 255, 255 );
        }


        public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {
            throw new NotImplementedException();
        }

        #endregion
    }


    public class ColorToAlphaColorConverter : IValueConverter
    {
        #region Implementation

        public object Convert( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {
            SolidColorBrush brush = (SolidColorBrush)value;

            if( brush != null )
            {
                Color color = brush.Color;
                color.A = byte.Parse( parameter.ToString() );

                return color;
            }

            return Colors.Black; // make error obvious
        }


        public object ConvertBack( object value, Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {
            throw new NotImplementedException();
        }

        #endregion
    }


    public class HighlightCornerRadiusConverter : IValueConverter
    {
        #region Implementation

        public object Convert( object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {
            CornerRadius corners = (CornerRadius)value;

            if( corners != null )
            {
                corners.BottomLeft = 0;
                corners.BottomRight = 0;
                return corners;
            }

            return null;
        }


        public object ConvertBack( object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture )
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
