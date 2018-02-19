using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SharedImageListsDemo
{
  public partial class MyUserControl : UserControl
  {
    public MyUserControl()
    {
      InitializeComponent();
    }
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (this.LargeImageList.Images.Count > 0)
      {
        e.Graphics.DrawString(this.Name, this.Font, SystemBrushes.ControlText, 0, 0);
        Size sz = System.Windows.Forms.TextRenderer.MeasureText("yY", this.Font);
        this.LargeImageList.Draw(e.Graphics, 0, sz.Height + 2, 0);
        e.Graphics.DrawString("LargeImageList.Handle: " + this.LargeImageList.Handle.ToString("x").ToUpper(), this.Font, SystemBrushes.ControlText, 0, sz.Height + this.LargeImageList.ImageSize.Height + 4);
      }
    }
  }

}
