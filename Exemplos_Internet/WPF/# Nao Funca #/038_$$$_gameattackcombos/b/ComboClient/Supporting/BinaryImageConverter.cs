using System;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GG.GameAttackCombos.Client {

	/// <summary>
	/// A value converter that converts a byte array into an ImageSource containing 
	/// the binary data for the image.
	/// </summary>
	[ValueConversion(typeof(byte[]), typeof(ImageSource))]
	public class BinaryImageConverter : IValueConverter {

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if (value != null) {
				if (value is byte[]) {
					if (targetType == typeof(ImageSource)) {
						// Create a MemoryStream for the binary image data.
						byte[] Data = (byte[])value;
						MemoryStream Stream = new MemoryStream(Data);

						// Create a BitmapImage to hold the stream data and return it.
						BitmapImage Image = new BitmapImage();
						Image.BeginInit();
						Image.CacheOption = BitmapCacheOption.OnLoad;
						Image.StreamSource = Stream;
						Image.EndInit();

						return Image;
					} else {
						throw new ArgumentException("The target type of the value to convert must be a BitmapImage.", "targetType");
					}
				} else {
					throw new ArgumentException("The value to convert must be a byte array.", "value");
				}
			} else {
				return null;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			throw new NotImplementedException();
		}

		#endregion

	}

}