namespace DynamicTypeDescriptor
{
  partial class EnumValueEditorUI
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
      System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("sfdsafd");
      System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("asdfsafd");
      this.splitContainer1 = new System.Windows.Forms.SplitContainer( );
      this.listViewSvc = new System.Windows.Forms.ListView( );
      this.columnHeader1 = new System.Windows.Forms.ColumnHeader( );
      this.lblDesc = new System.Windows.Forms.Label( );
      this.lblDispName = new System.Windows.Forms.Label( );
      this.splitContainer1.Panel1.SuspendLayout( );
      this.splitContainer1.Panel2.SuspendLayout( );
      this.splitContainer1.SuspendLayout( );
      this.SuspendLayout( );
      // 
      // splitContainer1
      // 
      this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(3, 3);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.listViewSvc);
      this.splitContainer1.Panel1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewSvc_MouseDoubleClick);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.lblDesc);
      this.splitContainer1.Panel2.Controls.Add(this.lblDispName);
      this.splitContainer1.Panel2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewSvc_MouseDoubleClick);
      this.splitContainer1.Size = new System.Drawing.Size(203, 189);
      this.splitContainer1.SplitterDistance = 114;
      this.splitContainer1.SplitterWidth = 6;
      this.splitContainer1.TabIndex = 1;
      this.splitContainer1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewSvc_MouseDoubleClick);
      // 
      // listViewEnum
      // 
      this.listViewSvc.Alignment = System.Windows.Forms.ListViewAlignment.Left;
      this.listViewSvc.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.listViewSvc.CheckBoxes = true;
      this.listViewSvc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
      this.listViewSvc.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listViewSvc.FullRowSelect = true;
      this.listViewSvc.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
      listViewItem1.StateImageIndex = 0;
      listViewItem2.StateImageIndex = 0;
      this.listViewSvc.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
      this.listViewSvc.Location = new System.Drawing.Point(0, 0);
      this.listViewSvc.MultiSelect = false;
      this.listViewSvc.Name = "listViewEnum";
      this.listViewSvc.ShowGroups = false;
      this.listViewSvc.Size = new System.Drawing.Size(201, 112);
      this.listViewSvc.TabIndex = 0;
      this.listViewSvc.UseCompatibleStateImageBehavior = false;
      this.listViewSvc.View = System.Windows.Forms.View.Details;
      this.listViewSvc.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewSvc_MouseDoubleClick);
      this.listViewSvc.SelectedIndexChanged += new System.EventHandler(this.listViewSvc_SelectedIndexChanged);
      this.listViewSvc.SizeChanged += new System.EventHandler(this.listViewSvc_SizeChanged);
      this.listViewSvc.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listViewSvc_ItemCheck);
      // 
      // columnHeader1
      // 
      this.columnHeader1.Width = 200;
      // 
      // lblDesc
      // 
      this.lblDesc.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblDesc.Location = new System.Drawing.Point(0, 18);
      this.lblDesc.Name = "lblDesc";
      this.lblDesc.Size = new System.Drawing.Size(201, 49);
      this.lblDesc.TabIndex = 1;
      this.lblDesc.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewSvc_MouseDoubleClick);
      // 
      // lblDispName
      // 
      this.lblDispName.Dock = System.Windows.Forms.DockStyle.Top;
      this.lblDispName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblDispName.Location = new System.Drawing.Point(0, 0);
      this.lblDispName.Name = "lblDispName";
      this.lblDispName.Size = new System.Drawing.Size(201, 18);
      this.lblDispName.TabIndex = 0;
      this.lblDispName.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewSvc_MouseDoubleClick);
      // 
      // StandardValueEditorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.splitContainer1);
      this.Name = "StandardValueEditorUI";
      this.Padding = new System.Windows.Forms.Padding(3);
      this.Size = new System.Drawing.Size(209, 195);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.Label lblDispName;
    private System.Windows.Forms.Label lblDesc;
    private System.Windows.Forms.ListView listViewSvc;
    private System.Windows.Forms.ColumnHeader columnHeader1;

  }
}
