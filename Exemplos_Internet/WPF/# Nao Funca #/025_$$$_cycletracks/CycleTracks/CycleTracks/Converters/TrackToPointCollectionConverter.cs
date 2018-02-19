using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GarminData;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows;

namespace CycleTracks.Converters
{
    class TrackToPointCollectionConverter : IValueConverter
    {
        public IValueConverter HartRatePositionConverter { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var trackPoints = value as IEnumerable<TrackPoint>;
            if (trackPoints == null || trackPoints.Count() == 0)
                return null;

            var query = from trackPoint in trackPoints
                        select new Point() {
                            X = (double) HartRatePositionConverter.Convert( trackPoint, typeof( double ), 1, null ),
                            Y = (double) HartRatePositionConverter.Convert( trackPoint, typeof( double ), null, null )
                        };
            PointCollection pc = new PointCollection( query );
            return pc;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
