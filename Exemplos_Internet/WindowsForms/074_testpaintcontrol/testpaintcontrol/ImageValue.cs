using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace testpaintcontrol
{
    public class ImageValue
    {
        private Image _image;

        public Image Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
            }
        }
        private string _text;

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        public ImageValue(string text, Image image)
        {
            Image = image;
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
