namespace SharedImageListsDemo
{
  partial class SharedImageLists1 : ForwardSoftware.Windows.Forms.SharedImageLists
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SharedImageLists1));
      this.components = new System.ComponentModel.Container();  
      this.LargeImageList = new System.Windows.Forms.ImageList(this.components);
      this.SmallImageList = new System.Windows.Forms.ImageList(this.components);
      this.LargeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("LargeImageList.ImageStream")));
      this.LargeImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.LargeImageList.Images.SetKeyName(0, "users.ico");
      this.LargeImageList.Images.SetKeyName(1, "install.ico");
      this.SmallImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SmallImageList.ImageStream")));
      this.SmallImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.SmallImageList.Images.SetKeyName(0, "printer.ico");
      this.SmallImageList.Images.SetKeyName(1, "camera.ico");
    }

    #endregion

    static ForwardSoftware.Windows.Forms.SharedImageLists sharedImageLists;

    [System.Diagnostics.DebuggerNonUserCode()]
    static SharedImageLists1()
    {
      sharedImageLists = new SharedImageLists1();
    }
    /// This Method returns the SharedImageLists1 component that is used to share
    /// the ImageLists.
    [System.Diagnostics.DebuggerNonUserCode()]
    public override ForwardSoftware.Windows.Forms.SharedImageLists GetSharedImageLists()
    {
      return sharedImageLists;
    }

    public System.Windows.Forms.ImageList LargeImageList;
    public System.Windows.Forms.ImageList SmallImageList;
  }
}
