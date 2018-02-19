using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestResizeCrop
{

  public delegate void ResizeInfoEventHandler(object sender, ResizeInfoEventArgs e);

  public partial class frmResizeInfo : Form
  {
    private int resizeWidth;
    private int resizeHeight;

    public event ResizeInfoEventHandler resizeOK;

    public frmResizeInfo()
    {
      InitializeComponent();
    }

    private void butOK_Click(object sender, EventArgs e)
    {
      if (tbHeight.Text != "" && tbWidth.Text != "")
      {
        this.resizeHeight = Convert.ToInt32(tbHeight.Text);
        this.resizeWidth = Convert.ToInt32(tbWidth.Text);

        this.Hide();

        ResizeInfoEventArgs resizeEventArgs = new ResizeInfoEventArgs(this.resizeHeight, this.resizeWidth);
        this.ResizeInfoOK(resizeEventArgs);
      }
    }

    protected virtual void ResizeInfoOK(ResizeInfoEventArgs eventArgs)
    {
      if (this.resizeOK != null)
      {
        this.resizeOK(this, eventArgs);
      }
    }
  }

  public class ResizeInfoEventArgs : EventArgs
  {
    public int Height;
    public int Width;

    public ResizeInfoEventArgs(int height, int width)
    {
      this.Height = height;
      this.Width = width;
    }
  }
}