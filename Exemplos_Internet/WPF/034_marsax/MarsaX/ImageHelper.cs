using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace MarsaX
{
    /// <summary>
    /// This class provides the ability to save a web based image to disk
    /// </summary>
    public class ImageHelper
    {
        #region Data
        private static List<string> allowableFileTypes = new List<string>();
        #endregion

        #region Static Ctor
        /// <summary>
        /// Static consructor, populates the allowableFileTypes
        /// list with allowable image types
        /// </summary>
        static ImageHelper()
        {
            allowableFileTypes.Clear();
            allowableFileTypes.Add(".jpg");
            allowableFileTypes.Add(".jpeg");
            allowableFileTypes.Add(".bmp");
            allowableFileTypes.Add(".png");
            allowableFileTypes.Add(".tif");
            allowableFileTypes.Add(".tiff");
            allowableFileTypes.Add(".gif");
            allowableFileTypes.Add(".wmp");
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Saves the image to the filename specified, and returns truie if successful. The errorMessage
        /// is also populated with an error message if one occurs during this operation
        /// </summary>
        public static bool SaveImageToDisk(string originalUrl, string filename, out string errorMessage)
        {
            try
            {
                System.Net.WebClient client = new System.Net.WebClient();
                byte[] data = client.DownloadData(originalUrl);
                client.Dispose();


                string extension = filename.LastIndexOf(".") > -1 ?
                    filename.Substring(filename.LastIndexOf(".")) : ".jpg";

                if (!allowableFileTypes.Contains(extension))
                {
                    errorMessage = "The file type you picked is not supported\r\n" +
                                    "Or you didn't enter a file extension, please retry";
                    return false;
                }

                // decode the image to its natural size
                BitmapSource imageSource = CreateImage(data, 0, 0);

                // encode the image using the original format
                byte[] encodedBytes = GetEncodedImageData(imageSource, extension);

                //save the image
                SaveImageData(encodedBytes, filename);
                errorMessage = string.Empty;
                return true;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message, "ERROR");
                errorMessage = "An error occurred\r\n" + e.Message;
                return false;
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Save image data to the filePath specified
        /// </summary>
        private static void SaveImageData(byte[] imageData, string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(imageData);
            bw.Close();
            fs.Close();
        }


        /// <summary>
        /// Returns a new BitmapSource using the parameters provided
        /// </summary>
        private static BitmapSource CreateImage(byte[] imageData,
            int decodePixelWidth, int decodePixelHeight)
        {
            if (imageData == null) return null;

            BitmapImage result = new BitmapImage();
            result.BeginInit();
            if (decodePixelWidth > 0)
            {
                result.DecodePixelWidth = decodePixelWidth;
            }
            if (decodePixelHeight > 0)
            {
                result.DecodePixelHeight = decodePixelHeight;
            }
            result.StreamSource = new MemoryStream(imageData);
            result.CreateOptions = BitmapCreateOptions.None;
            result.CacheOption = BitmapCacheOption.Default;
            result.EndInit();
            return result;
        }


        /// <summary>
        /// Returns a byte[] array for the image data which is encoded using one of the
        /// BitmapEncoder subclasses. The actual encoder used is driven by what the
        /// preferredFormat value is
        /// </summary>
        private static byte[] GetEncodedImageData(ImageSource image, string preferredFormat)
        {
            byte[] result = null;
            BitmapEncoder encoder = null;
            switch (preferredFormat.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    encoder = new JpegBitmapEncoder();
                    break;

                case ".bmp":
                    encoder = new BmpBitmapEncoder();
                    break;

                case ".png":
                    encoder = new PngBitmapEncoder();
                    break;

                case ".tif":
                case ".tiff":
                    encoder = new TiffBitmapEncoder();
                    break;

                case ".gif":
                    encoder = new GifBitmapEncoder();
                    break;

                case ".wmp":
                    encoder = new WmpBitmapEncoder();
                    break;
            }

            if (image is BitmapSource)
            {
                MemoryStream stream = new MemoryStream();
                encoder.Frames.Add(BitmapFrame.Create(image as BitmapSource));
                encoder.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);
                result = new byte[stream.Length];
                BinaryReader br = new BinaryReader(stream);
                br.Read(result, 0, (int)stream.Length);
                br.Close();
                stream.Close();
            }
            return result;
        }
        #endregion
    }

}
