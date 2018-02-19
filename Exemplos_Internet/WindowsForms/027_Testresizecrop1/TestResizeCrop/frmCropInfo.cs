using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestResizeCrop
{
  public delegate void CropInfoEventHandler(object sender, CropInfoEventArgs e);

  public partial class frmCropInfo : Form
  {
    private int cropX;
    private int cropY;
    private int cropHeight;
    private int cropWidth;

    public event CropInfoEventHandler cropOK;

    public frmCropInfo()
    {
      InitializeComponent();
    }

    private void butCrop_Click(object sender, EventArgs e)
    {
      if (this.tbX.Text != "" && this.tbY.Text != "" &&
          this.tbHeight.Text != "" && this.tbWidth.Text != "")
      {
        this.cropX = Convert.ToInt32(this.tbX.Text);
        this.cropY = Convert.ToInt32(this.tbY.Text);
        this.cropHeight = Convert.ToInt32(this.tbHeight.Text);
        this.cropWidth = Convert.ToInt32(this.tbWidth.Text);

        this.Hide();

        CropInfoEventArgs cropEventArgs = new CropInfoEventArgs(this.cropX, this.cropY, this.cropHeight, this.cropWidth);
        this.CropInfoOK(cropEventArgs);
      }
    }

    protected virtual void CropInfoOK(CropInfoEventArgs eventArgs)
    {
      if (this.cropOK != null)
      {
        this.cropOK(this, eventArgs);
      }
    }
  }

  public class CropInfoEventArgs : EventArgs
  {
    public int X;
    public int Y;
    public int Height;
    public int Width;

    public CropInfoEventArgs(int x, int y, int height, int width)
    {
      this.X = x;
      this.Y = y;
      this.Height = height;
      this.Width = width;
    }
  }
}