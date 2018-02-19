using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SharedImageListsDemo
{
  public partial class MyForm : Form
  {
    public MyForm()
    {
      InitializeComponent();
    }
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (this.ListView1.Items.Count > 0 && this.ListView1.Items[0].SubItems.Count > 0)
      {
        this.ListView1.Items[0].SubItems[1].Text = "Handle: " + this.ListView1.LargeImageList.Handle.ToString("x").ToUpper();
      }
    }
    private void btnShowInheritedForm_Click(object sender, System.EventArgs e)
    {
      MyInheritedForm frm = new MyInheritedForm();
      frm.Show();
    }
  }
}