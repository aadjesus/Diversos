namespace DragNDrop
{
    partial class DragNDrop
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
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Item1.txt", 0);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Item2.txt", 0);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DragNDrop));
            this.ItemsListView = new System.Windows.Forms.ListView();
            this.imagesForList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // ItemsListView
            // 
            this.ItemsListView.AllowDrop = true;
            this.ItemsListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ItemsListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem3,
            listViewItem4});
            this.ItemsListView.LargeImageList = this.imagesForList;
            this.ItemsListView.Location = new System.Drawing.Point(13, 13);
            this.ItemsListView.Name = "ItemsListView";
            this.ItemsListView.Size = new System.Drawing.Size(437, 257);
            this.ItemsListView.SmallImageList = this.imagesForList;
            this.ItemsListView.TabIndex = 0;
            this.ItemsListView.UseCompatibleStateImageBehavior = false;
            this.ItemsListView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ItemsListView_MouseMove);
            this.ItemsListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ItemsListView_MouseDown);
            // 
            // imagesForList
            // 
            this.imagesForList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagesForList.ImageStream")));
            this.imagesForList.TransparentColor = System.Drawing.Color.Transparent;
            this.imagesForList.Images.SetKeyName(0, "textdoc.ico");
            // 
            // DragNDrop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 282);
            this.Controls.Add(this.ItemsListView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DragNDrop";
            this.Text = "DragNDrop";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ItemsListView;
        private System.Windows.Forms.ImageList imagesForList;
    }
}

