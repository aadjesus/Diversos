using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace TestResizeCrop
{
  public partial class frmResizeCrop : Form
  {
    private Image img;

    public frmResizeCrop()
    {
      InitializeComponent();
    }

    private void btnOpenImage_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDia = new OpenFileDialog();
      if (openFileDia.ShowDialog() == DialogResult.OK)
      {
        this.img = Image.FromFile(openFileDia.FileName);
        this.setPicBoxImage();
        this.butCrop.Enabled = true;
        this.butResize.Enabled = true;
        this.butSave.Enabled = true;
      }
    }

    private void butResize_Click(object sender, EventArgs e)
    {
      frmResizeInfo infoForm = new frmResizeInfo();
      infoForm.resizeOK += new ResizeInfoEventHandler(infoForm_resizeOK);
      infoForm.Show();
    }

    void infoForm_resizeOK(object sender, ResizeInfoEventArgs e)
    {
      img = this.resizeImage(img, new Size(e.Width, e.Height));
      this.setPicBoxImage();
    }

    private void butCrop_Click(object sender, EventArgs e)
    {
      frmCropInfo infoForm = new frmCropInfo();
      infoForm.cropOK += new CropInfoEventHandler(infoForm_cropOK);
      infoForm.Show();
    }

    void infoForm_cropOK(object sender, CropInfoEventArgs e)
    {
      img = this.cropImage(img, new Rectangle(e.X, e.Y, e.Width, e.Height));
      this.setPicBoxImage();
    }

    private void setPicBoxImage()
    {
      this.pbImage.Image = img;
      if (img.Height < pbImage.Height && img.Width < pbImage.Width)
      {
        this.pbImage.SizeMode = PictureBoxSizeMode.CenterImage;
        this.lblPicMode.Text = "Centered";
      }
      else
      {
        this.lblPicMode.Text = "Zoom";
      }
      this.updateImgInfo();
    }

    private void lblPicMode_Click(object sender, EventArgs e)
    {
      if (this.pbImage.SizeMode == PictureBoxSizeMode.CenterImage)
      {
        this.pbImage.SizeMode = PictureBoxSizeMode.Zoom;
        this.lblPicMode.Text = "Zoom";
        return;
      }
      this.pbImage.SizeMode = PictureBoxSizeMode.CenterImage;
      this.lblPicMode.Text = "Centered";
    }

    private void updateImgInfo()
    {
      this.lblImgHeightAct.Text = this.img.Height.ToString();
      this.lblImgWidthAct.Text = this.img.Width.ToString();
    }

    private void butSave_Click(object sender, EventArgs e)
    {
      SaveFileDialog saveFileDia = new SaveFileDialog();
      saveFileDia.Filter = "jpeg pictures|*.jpg";
      if (saveFileDia.ShowDialog() == DialogResult.OK)
      {
        this.saveJpeg(saveFileDia.FileName, new Bitmap(this.img), 85L);
      }
    }

    /*
     * Image editing code below 
     * 
     * 
     */

    private Image cropImage(Image img, Rectangle cropArea)
    {
      Bitmap bmpImage = new Bitmap(img);
      Bitmap bmpCrop = bmpImage.Clone(cropArea,
                                      bmpImage.PixelFormat);
      return (Image)(bmpCrop);
    }

    private Image resizeImage(Image imgToResize, Size size)
    {
      int sourceWidth = imgToResize.Width;
      int sourceHeight = imgToResize.Height;

      float nPercent = 0;
      float nPercentW = 0;
      float nPercentH = 0;

      nPercentW = ((float)size.Width / (float)sourceWidth);
      nPercentH = ((float)size.Height / (float)sourceHeight);

      if (nPercentH < nPercentW)
        nPercent = nPercentH;
      else
        nPercent = nPercentW;

      int destWidth = (int)(sourceWidth * nPercent);
      int destHeight = (int)(sourceHeight * nPercent);

      Bitmap b = new Bitmap(destWidth, destHeight);
      Graphics g = Graphics.FromImage((Image)b);
      g.InterpolationMode = InterpolationMode.HighQualityBicubic;

      g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
      g.Dispose();

      return (Image)b;
    }

    private void saveJpeg(string path, Bitmap img, long quality)
    {
      // Encoder parameter for image quality
      EncoderParameter qualityParam =
          new EncoderParameter(Encoder.Quality, quality);

      // Jpeg image codec
      ImageCodecInfo jpegCodec = getEncoderInfo("image/jpeg");

      if (jpegCodec == null)
        return;

      EncoderParameters encoderParams = new EncoderParameters(1);
      encoderParams.Param[0] = qualityParam;

      img.Save(path, jpegCodec, encoderParams);
    }

    private ImageCodecInfo getEncoderInfo(string mimeType)
    {
      // Get image codecs for all image formats
      ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

      // Find the correct image codec
      for (int i = 0; i < codecs.Length; i++)
        if (codecs[i].MimeType == mimeType)
          return codecs[i];
      return null;
    }
  }
}