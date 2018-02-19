//        Another Demo from Andy L. & MissedMemo.com
// Borrow whatever code seems useful - just don't try to hold
// me responsible for any ill effects. My demos sometimes use
// licensed images which CANNOT legally be copied and reused.

using System.Windows.Data;
using System.Windows;


namespace CustomControls
{
    public class HighlightCornerRadiusConverter : IValueConverter
    {
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
    }
}
