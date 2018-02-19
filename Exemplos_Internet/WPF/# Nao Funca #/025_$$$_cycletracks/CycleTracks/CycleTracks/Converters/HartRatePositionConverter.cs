using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using GarminData;

namespace CycleTracks.Converters
{
    [ValueConversion(typeof(TrackPoint), typeof(double))]
    class HartRatePositionConverter : IValueConverter
    {
        public HartRatePositionConverter()
        {
            this.DistanceFactor = this.HartRateFactor = 1.0;
        }

        Random rnd = new Random();

        public double TotalDistance { get; set; }

        public double DistanceFactor { get; set; }

        public double HartRateFactor { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var tp = value as TrackPoint;

            if (parameter != null)
            {
                return DistanceFactor * tp.Distance / TotalDistance;
            }

            return HartRateFactor * (tp.HeartRate);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
