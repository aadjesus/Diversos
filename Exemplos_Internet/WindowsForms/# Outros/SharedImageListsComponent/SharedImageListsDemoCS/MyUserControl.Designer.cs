namespace SharedImageListsDemo
{
  partial class MyUserControl
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.components = new System.ComponentModel.Container();
      this.SharedImageLists11 = new SharedImageListsDemo.SharedImageLists1(this.components);
      this.LargeImageList = this.SharedImageLists11.NewImageList(this.components, ((SharedImageListsDemo.SharedImageLists1)(this.SharedImageLists11.GetSharedImageLists())).LargeImageList);
      this.SuspendLayout();
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Name = "MyUserControl";
      this.ResumeLayout(false);
    }

    #endregion

    internal SharedImageListsDemo.SharedImageLists1 SharedImageLists11;
    protected internal System.Windows.Forms.ImageList LargeImageList;
  }
}
