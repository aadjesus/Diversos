using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace WPFExifInfo
{
    public class ExifMetaInfo
    {
        private BitmapMetadata _metaInfo;

        public ExifMetaInfo(Uri imageUri)
        {
            BitmapFrame bFrame = BitmapFrame.Create(imageUri, BitmapCreateOptions.DelayCreation, BitmapCacheOption.None);
            _metaInfo = (BitmapMetadata)bFrame.Metadata;
        }

        /*
         #########################################################################
         GetMetaInfo() Method will return the EXIF info Based on parameter passed
         #########################################################################
         */
        private object GetMetaInfo(string infoQuery)
        {
            if (_metaInfo.ContainsQuery(infoQuery))
                return _metaInfo.GetQuery(infoQuery);
            else
                return null;
        }

        public uint? Width
        {
            get
            {
                object obj = GetMetaInfo("/app1/ifd/exif/subifd:{uint=40962}");
                if (obj == null)
                {
                    return null;
                }
                else
                {
                    if (obj.GetType() == typeof(UInt32))
                        return (uint)obj;
                    else
                        return Convert.ToUInt32(obj);
                }
            }
        }
        public uint? Height
        {
            get
            {
                object obj = GetMetaInfo("/app1/ifd/exif/subifd:{uint=40963}");
                if (obj == null)
                {
                    return null;
                }
                else
                {
                    if (obj.GetType() == typeof(UInt32))
                        return (uint)obj;
                    else
                        return Convert.ToUInt32(obj);
                }
            }
        }
        public string EquipmentManufacturer
        {
            get
            {
                object val = GetMetaInfo("/app1/ifd/exif:{uint=271}");
                return (val != null ? (string)val : String.Empty);
            }
        }

        public string CameraModel
        {
            get
            {
                object val = GetMetaInfo("/app1/ifd/exif:{uint=272}");
                return (val != null ? (string)val : String.Empty);
            }
        }

        public string CreationSoftware
        {
            get
            {
                object val = GetMetaInfo("/app1/ifd/exif:{uint=305}");
                return (val != null ? (string)val : String.Empty);
            }
        }

        public ColorRepresentation ColorRepresentation
        {
            get
            {
                if ((ushort)GetMetaInfo("/app1/ifd/exif/subifd:{uint=40961}") == 1)
                    return ColorRepresentation.sRGB;
                else
                    return ColorRepresentation.Uncalibrated;
            }
        }

        public decimal? ExposureTime
        {
            get
            {
                object val = GetMetaInfo("/app1/ifd/exif/subifd:{uint=33434}");
                if (val != null)
                {
                    return ParseUnsigned((ulong)val);
                }
                else
                {
                    return null;
                }
            }
        }


        public decimal? LensAperture
        {
            get
            {
                object val = GetMetaInfo("/app1/ifd/exif/subifd:{uint=33437}");
                if (val != null)
                {
                    return ParseUnsigned((ulong)val);
                }
                else
                {
                    return null;
                }
            }
        }

        public decimal? FocalLength
        {
            get
            {
                object val = GetMetaInfo("/app1/ifd/exif/subifd:{uint=37386}");
                if (val != null)
                {
                    return ParseUnsigned((ulong)val);
                }
                else
                {
                    return null;
                }
            }
        }

        public ushort? IsoSpeed
        {
            get
            {
                return (ushort?)GetMetaInfo("/app1/ifd/exif/subifd:{uint=34855}");
            }
        }

        public FlashMode FlashMode
        {
            get
            {
                if ((ushort)GetMetaInfo("/app1/ifd/exif/subifd:{uint=37385}") % 2 == 1)
                    return FlashMode.FlashFired;
                else
                    return FlashMode.FlashNotFire;
            }
        }

        private decimal ParseUnsigned(ulong exifValue)
        {
            return (decimal)(exifValue & 0xFFFFFFFFL) / (decimal)((exifValue & 0xFFFFFFFF00000000L) >> 32);
        }
    }
    public enum ColorRepresentation
    {
        sRGB,
        Uncalibrated
    }
    public enum FlashMode
    {
        FlashFired,
        FlashNotFire
    }
}
