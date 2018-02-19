using System;
using System.Windows.Data;
using System.Windows.Media;

namespace CycleTracks.Converters
{
  [ValueConversion(typeof(int), typeof(Brush))]
  public class HartRateToColorConverter : IValueConverter
  {
    public Brush SafeColor { get; set; }
    public Brush DangerColor { get; set; }
    public int DangerLevel { get; set; }

    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return ((int)value >= DangerLevel)
                 ? DangerColor
                 : SafeColor;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
