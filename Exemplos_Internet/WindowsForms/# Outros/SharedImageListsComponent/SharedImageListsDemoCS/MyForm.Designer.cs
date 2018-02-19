namespace SharedImageListsDemo
{
  partial class MyForm
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
      System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "LargeImageList"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "0", System.Drawing.SystemColors.GrayText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))))}, 0);
      this.ListView1 = new System.Windows.Forms.ListView();
      this.lvcolName = new System.Windows.Forms.ColumnHeader();
      this.lvcolHandle = new System.Windows.Forms.ColumnHeader();
      this.SharedImageLists1 = new SharedImageListsDemo.SharedImageLists1(this.components);
      this.LargeImageList = this.SharedImageLists1.NewImageList(this.components, ((SharedImageListsDemo.SharedImageLists1)(this.SharedImageLists1.GetSharedImageLists())).LargeImageList);
      this.btnShowInheritedForm = new System.Windows.Forms.Button();
      this.MyUserControl1 = new SharedImageListsDemo.MyUserControl();
      this.SuspendLayout();
      // 
      // ListView1
      // 
      this.ListView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
      this.ListView1.Alignment = System.Windows.Forms.ListViewAlignment.Default;
      this.ListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.ListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvcolName,
            this.lvcolHandle});
      listViewGroup1.Header = "ListViewGroup";
      listViewGroup1.Name = "ListViewGroup1";
      this.ListView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
      listViewItem1.Group = listViewGroup1;
      listViewItem1.IndentCount = 1;
      listViewItem1.StateImageIndex = 0;
      listViewItem1.ToolTipText = "aaa";
      this.ListView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
      this.ListView1.LargeImageList = this.LargeImageList;
      this.ListView1.Location = new System.Drawing.Point(12, 12);
      this.ListView1.Name = "ListView1";
      this.ListView1.Size = new System.Drawing.Size(266, 162);
      this.ListView1.TabIndex = 0;
      this.ListView1.TileSize = new System.Drawing.Size(260, 60);
      this.ListView1.UseCompatibleStateImageBehavior = false;
      this.ListView1.View = System.Windows.Forms.View.Tile;
      // 
      // btnShowInheritedForm
      // 
      this.btnShowInheritedForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnShowInheritedForm.Location = new System.Drawing.Point(346, 110);
      this.btnShowInheritedForm.Name = "btnShowInheritedForm";
      this.btnShowInheritedForm.Size = new System.Drawing.Size(133, 64);
      this.btnShowInheritedForm.TabIndex = 1;
      this.btnShowInheritedForm.Text = "Show Inherited Form";
      this.btnShowInheritedForm.UseVisualStyleBackColor = true;
      this.btnShowInheritedForm.Click += new System.EventHandler(this.btnShowInheritedForm_Click);
      // 
      // MyUserControl1
      // 
      this.MyUserControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.MyUserControl1.Location = new System.Drawing.Point(285, 12);
      this.MyUserControl1.Name = "MyUserControl1";
      this.MyUserControl1.Size = new System.Drawing.Size(194, 92);
      this.MyUserControl1.TabIndex = 2;
      // 
      // MyForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(491, 186);
      this.Controls.Add(this.MyUserControl1);
      this.Controls.Add(this.btnShowInheritedForm);
      this.Controls.Add(this.ListView1);
      this.Name = "MyForm";
      this.Text = "MyForm";
      this.ResumeLayout(false);

    }

    #endregion

    protected internal  SharedImageLists1 SharedImageLists1;
    internal System.Windows.Forms.ListView ListView1;
    internal System.Windows.Forms.ColumnHeader lvcolName;
    internal System.Windows.Forms.ColumnHeader lvcolHandle;
    internal System.Windows.Forms.Button btnShowInheritedForm;
    internal MyUserControl MyUserControl1;
    protected internal System.Windows.Forms.ImageList LargeImageList;

  }
}

