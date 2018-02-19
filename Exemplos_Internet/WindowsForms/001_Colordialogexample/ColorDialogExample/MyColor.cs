using System;                   // BitConverter
using System.ComponentModel;    // TypeConverter

namespace ColorDialogExample
{
    // This like tells it to use our custom type converter instead of the default.
    [TypeConverter(typeof(MyColorConverter))]
    public class MyColor
    {
        #region " The color channel variables w/ accessors/mutators. "
        private byte _Red;
        public byte Red
        {
            get { return _Red; }
            set { _Red = value; }
        }

        private byte _Green;
        public byte Green
        {
            get { return _Green; }
            set { _Green = value; }
        }

        private byte _Blue;
        public byte Blue
        {
            get { return _Blue; }
            set { _Blue = value; }
        }
        #endregion

        #region " Constructors "
        public MyColor()
        {
            _Red = 0;
            _Green = 0;
            _Blue = 0;
        }
        public MyColor(byte red, byte green, byte blue)
        {
            _Red = red;
            _Green = green;
            _Blue = blue;
        }
        public MyColor(byte[] rgb)
        {
            if (rgb.Length != 3)
                throw new Exception("Array must have a length of 3.");
            _Red = rgb[0];
            _Green = rgb[1];
            _Blue = rgb[2];
        }
        public MyColor(int argb)
        {
            byte[] bytes = BitConverter.GetBytes(argb);
            _Red = bytes[2];
            _Green = bytes[1];
            _Blue = bytes[0];
        }
        public MyColor(string rgb)
        {
            string[] parts = rgb.Split(' ');
            if (parts.Length != 3)
                throw new Exception("Array must have a length of 3.");
            _Red = Convert.ToByte(parts[0]);
            _Green = Convert.ToByte(parts[1]);
            _Blue = Convert.ToByte(parts[2]);
        }
        #endregion

        #region " Methods "
        public new string ToString()
        {
            return String.Format("{0} {1} {2}", _Red, _Green, _Blue); ;
        }
        public byte[] GetRGB()
        {
            return new byte[] { _Red, _Green, _Blue };
        }
        public int GetARGB()
        {
            byte[] temp = new byte[] { _Blue, _Green, _Red, 255 };
            return BitConverter.ToInt32(temp, 0);
        }
        #endregion
    }
}
