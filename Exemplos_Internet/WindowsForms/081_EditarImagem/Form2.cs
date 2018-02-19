using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RGB2RGBA
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Bitmap bmp = (Bitmap)Image.FromFile(@"c:\Users\alessandro.augusto\Pictures\grafite\2457969370-2b4857f568.jpg");

            //int imageHeigh = bmp.Height;
            //int imageWidth = bmp.Width;
            //string imageFormat = bmp.PixelFormat.ToString();
            //MessageBox.Show(imageFormat);


            Bitmap image1 = (Bitmap)Image.FromFile(@"c:\Users\alessandro.augusto\Pictures\grafite\2457969370-2b4857f568.jpg");
            int x, y;

            // Loop through the images pixels to reset color.
            for (x = 0; x < image1.Width; x++)
            {
                for (y = 0; y < image1.Height; y++)
                {
                    Color pixelColor = image1.GetPixel(x, y);
                    Color newColor = Color.FromArgb(pixelColor.R, 0, 0);
                    image1.SetPixel(x, y, newColor);
                }
            }

            MessageBox.Show(image1.PixelFormat.ToString());
        }
    }
}
