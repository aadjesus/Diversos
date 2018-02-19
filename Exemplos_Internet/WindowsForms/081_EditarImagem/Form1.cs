// RGB2RGBA
//===========
// P. Poliakoff 2009
//===================

// allows to use a mask to define the alpha layer of a bitmap

// Functionalities
//==================
/* V resizable window
 * V Load bitmap 
 * V Load Mask
 * V Display bitmap
 * V Display Mask
 * V Display masked bitmap
 * V allow to disable partial opacity
 * V invert mask
 * V Allow to use same bitmap for loaded bitmap and mask
 * V allow to configure opacity threshold
 * V Save masked bitmap
 * V adapt the size of displayed images
 * V display About box
 * V ignore the alpha layer when already already present
 * V report error if mask and bitmap have not the same size

 * Possible imporvements:
 * ======================
 * allow to save the save mask as a grayscale bitmap
 * display images file names
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

namespace RGB2RGBA
{
    public partial class Form1 : Form
    {
        Bitmap loadedImage;
        Bitmap originalMaskImage;
        Bitmap maskImage;
        Bitmap maskedImage;
        public Form1()
        {
            InitializeComponent();
            MessageBox.Show("Alpha Mask Editor 1.0: P. Poliakoff 2009" + Environment.NewLine + "This progam allows to add an Alpha layer to any bitmap", "About");
        }

        private Bitmap Create32bppImageAndClearAlpha(Bitmap tmpImage)
        {
            // declare the new image that will be returned by the function
            Bitmap returnedImage = new Bitmap(tmpImage.Width, tmpImage.Height, PixelFormat.Format32bppArgb);
            
            // create a graphics instance to draw the original image in the new one
            Rectangle rect = new Rectangle(0, 0, tmpImage.Width, tmpImage.Height);
            Graphics g = Graphics.FromImage(returnedImage);

            // create an image attribe to force a clearing of the alpha layer
            ImageAttributes imageAttributes=new ImageAttributes();
            float[][] colorMatrixElements = { 
                        new float[] {1,0,0,0,0},
                        new float[] {0,1,0,0,0},
                        new float[] {0,0,1,0,0},
                        new float[] {0,0,0,0,0},
                        new float[] {0,0,0,1,1}};

            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
            imageAttributes.SetColorMatrix(colorMatrix,ColorMatrixFlag.Default,ColorAdjustType.Bitmap);
            
            // draw the original image 
            g.DrawImage(tmpImage, rect, 0, 0, tmpImage.Width, tmpImage.Height,GraphicsUnit.Pixel,imageAttributes);
            g.Dispose();
            return returnedImage;
        }

        private void PrepareMaskedImage()
        {
            this.Cursor = Cursors.WaitCursor;

            if (this.loadedImage != null && this.maskImage != null)
            {
                if (this.loadedImage.Width != this.maskImage.Width || this.loadedImage.Height != this.maskImage.Height)
                {
                    MessageBox.Show("Error: mask and image must have the same size", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.pictureBox3.Image = null;
                    
                }
                else
                {

                    //allocate the Masked image in ARGB format
                    this.maskedImage = Create32bppImageAndClearAlpha(this.loadedImage);

                    BitmapData bmpData1 = maskedImage.LockBits(new Rectangle(0, 0, maskedImage.Width, maskedImage.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, maskedImage.PixelFormat);
                    byte[] maskedImageRGBAData = new byte[bmpData1.Stride * bmpData1.Height];
                    System.Runtime.InteropServices.Marshal.Copy(bmpData1.Scan0, maskedImageRGBAData, 0, maskedImageRGBAData.Length);

                    BitmapData bmpData2 = maskImage.LockBits(new Rectangle(0, 0, maskImage.Width, maskImage.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, maskImage.PixelFormat);
                    byte[] maskImageRGBAData = new byte[bmpData2.Stride * bmpData2.Height];
                    System.Runtime.InteropServices.Marshal.Copy(bmpData2.Scan0, maskImageRGBAData, 0, maskImageRGBAData.Length);

                    //copy the mask to the Alpha layer
                    for (int i = 0; i + 2 < maskedImageRGBAData.Length; i += 4)
                    {
                        maskedImageRGBAData[i + 3] = maskImageRGBAData[i];

                    }
                    System.Runtime.InteropServices.Marshal.Copy(maskedImageRGBAData, 0, bmpData1.Scan0, maskedImageRGBAData.Length);
                    this.maskedImage.UnlockBits(bmpData1);
                    this.maskImage.UnlockBits(bmpData2);

                    this.pictureBox3.Image = maskedImage;
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void PrepareMaskImage()
        {
            if (originalMaskImage != null)
            {
                this.Cursor = Cursors.WaitCursor;

                this.maskImage = Create32bppImageAndClearAlpha(originalMaskImage);

                BitmapData bmpData = maskImage.LockBits(new Rectangle(0, 0, maskImage.Width, maskImage.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, maskImage.PixelFormat);

                byte[] maskImageRGBData = new byte[bmpData.Stride * bmpData.Height];

                System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, maskImageRGBData, 0, maskImageRGBData.Length);


                byte greyLevel;
                bool opaque = this.checkBoxAllowPartialOpacity.Checked == false;
                int OpacityThreshold = this.trackBar1.Value;
                bool invertedMask = this.checkBoxInvertMask.Checked;
                for (int i = 0; i + 2 < maskImageRGBData.Length; i += 4)
                {
                    //convert to gray scale R:0.30 G=0.59 B 0.11
                    greyLevel = (byte)(0.3 * maskImageRGBData[i + 2] + 0.59 * maskImageRGBData[i + 1] + 0.11 * maskImageRGBData[i]);

                    if (opaque)
                    {
                        greyLevel = (greyLevel < OpacityThreshold) ? byte.MinValue : byte.MaxValue;
                    }
                    if (invertedMask)
                    {
                        greyLevel = (byte)(255 - (int)greyLevel);
                    }

                    maskImageRGBData[i] = greyLevel;
                    maskImageRGBData[i + 1] = greyLevel;
                    maskImageRGBData[i + 2] = greyLevel;

                }
                System.Runtime.InteropServices.Marshal.Copy(maskImageRGBData, 0, bmpData.Scan0, maskImageRGBData.Length);
                this.maskImage.UnlockBits(bmpData);
                this.pictureBox2.Image = maskImage;
                this.Cursor = Cursors.Default;
                // if the loaded image is available, we have everything to compute the masked image
                if (this.loadedImage != null)
                {
                    PrepareMaskedImage();
                }
            }
        }

        private void buttonLoadImage_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                loadedImage = Create32bppImageAndClearAlpha(new Bitmap(openFileDialog1.FileName));

                pictureBox1.Image = loadedImage;
                if (this.checkBoxLoadedImageAsMask.Checked)
                {
                    originalMaskImage = (Bitmap)loadedImage.Clone();
                    PrepareMaskImage();
                }
                else if (this.maskImage != null)
                {
                    PrepareMaskedImage();
                }
            }
        }

        private void buttonLoadMask_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                originalMaskImage = Create32bppImageAndClearAlpha(new Bitmap(openFileDialog1.FileName));
                PrepareMaskImage();
            }
        }

        private void checkBoxUseLoadedImageAsMask_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxLoadedImageAsMask.Checked)
            {
                this.buttonLoadMask.Enabled = false;
                if (this.loadedImage != null)
                {
                    originalMaskImage = (Bitmap)this.loadedImage.Clone();
                    PrepareMaskImage();
                }
            }
            else
            {
                this.buttonLoadMask.Enabled = true;
            }
        }

        private void checkBoxInvertMask_CheckedChanged(object sender, EventArgs e)
        {
            PrepareMaskImage();
        }

        private void checkBoxPartialOpacity_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxAllowPartialOpacity.Checked)
            {
                this.trackBar1.Enabled = false;
                this.textBoxOpacityThreshold.Enabled = false;
            }
            else
            {
                this.trackBar1.Enabled = true;
                this.textBoxOpacityThreshold.Enabled = true;
            }
            PrepareMaskImage();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            PrepareMaskImage();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (maskedImage != null && this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                maskedImage.Save(this.saveFileDialog1.FileName);
            }
        }

        private void buttonSaveMask_Click(object sender, EventArgs e)
        {
            if (maskImage != null && this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                maskImage.Save(this.saveFileDialog1.FileName);
            }
        }
    }
}
