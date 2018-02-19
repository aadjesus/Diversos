using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using WinLib.Controls;
using Microsoft.Win32;
using System.IO;

namespace WinLib.Controls.Form {
	public class ImageListAssignerSelector : System.Windows.Forms.Form {
		private System.Windows.Forms.ImageList ILTree;
		private System.Windows.Forms.TreeView TreeFolder;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel lnkFolder;
		public System.Windows.Forms.FolderBrowserDialog FDlgFolder;
		private System.Windows.Forms.LinkLabel lnkApply;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ListView FViewer;
		private System.Windows.Forms.Label lblIL;
		private System.Windows.Forms.ImageList IL;
		private System.Windows.Forms.ListView FPreview;
		private System.Windows.Forms.ImageList ILPreview;
		private System.Windows.Forms.ContextMenu CXPreview;
		private System.Windows.Forms.MenuItem mnuAddToImageList;
		private System.Windows.Forms.ContextMenu CXImageList;
		private System.Windows.Forms.MenuItem mnuDeleteFromImageList;
		private System.Windows.Forms.MenuItem mnuDeleteAllFromImageList;
		private System.Windows.Forms.LinkLabel lnkMoveBack;
		private System.Windows.Forms.LinkLabel lnkMoveNext;
		private System.Windows.Forms.ProgressBar PB;
		private System.ComponentModel.IContainer components;

		public ImageListAssignerSelector() {
			InitializeComponent();
			InitForm();
		}

		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ImageListAssignerSelector));
			this.TreeFolder = new System.Windows.Forms.TreeView();
			this.ILTree = new System.Windows.Forms.ImageList(this.components);
			this.IL = new System.Windows.Forms.ImageList(this.components);
			this.FViewer = new System.Windows.Forms.ListView();
			this.CXPreview = new System.Windows.Forms.ContextMenu();
			this.mnuAddToImageList = new System.Windows.Forms.MenuItem();
			this.lnkFolder = new System.Windows.Forms.LinkLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.FPreview = new System.Windows.Forms.ListView();
			this.CXImageList = new System.Windows.Forms.ContextMenu();
			this.mnuDeleteFromImageList = new System.Windows.Forms.MenuItem();
			this.mnuDeleteAllFromImageList = new System.Windows.Forms.MenuItem();
			this.ILPreview = new System.Windows.Forms.ImageList(this.components);
			this.lblIL = new System.Windows.Forms.Label();
			this.FDlgFolder = new System.Windows.Forms.FolderBrowserDialog();
			this.lnkApply = new System.Windows.Forms.LinkLabel();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label3 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lnkMoveBack = new System.Windows.Forms.LinkLabel();
			this.lnkMoveNext = new System.Windows.Forms.LinkLabel();
			this.PB = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// TreeFolder
			// 
			this.TreeFolder.HideSelection = false;
			this.TreeFolder.ImageList = this.ILTree;
			this.TreeFolder.Location = new System.Drawing.Point(5, 20);
			this.TreeFolder.Name = "TreeFolder";
			this.TreeFolder.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
																				   new System.Windows.Forms.TreeNode("", 3, 3)});
			this.TreeFolder.SelectedImageIndex = 4;
			this.TreeFolder.ShowRootLines = false;
			this.TreeFolder.Size = new System.Drawing.Size(160, 245);
			this.TreeFolder.TabIndex = 1;
			this.TreeFolder.DoubleClick += new System.EventHandler(this.TreeFolder_DoubleClick);
			this.TreeFolder.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeFolder_AfterSelect);
			// 
			// ILTree
			// 
			this.ILTree.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
			this.ILTree.ImageSize = new System.Drawing.Size(16, 16);
			this.ILTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ILTree.ImageStream")));
			this.ILTree.TransparentColor = System.Drawing.Color.Fuchsia;
			// 
			// IL
			// 
			this.IL.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
			this.IL.ImageSize = new System.Drawing.Size(24, 24);
			this.IL.TransparentColor = System.Drawing.Color.Fuchsia;
			// 
			// FViewer
			// 
			this.FViewer.ContextMenu = this.CXPreview;
			this.FViewer.HideSelection = false;
			this.FViewer.LargeImageList = this.IL;
			this.FViewer.Location = new System.Drawing.Point(170, 20);
			this.FViewer.Name = "FViewer";
			this.FViewer.Size = new System.Drawing.Size(535, 245);
			this.FViewer.SmallImageList = this.IL;
			this.FViewer.TabIndex = 2;
			this.FViewer.DoubleClick += new System.EventHandler(this.FViewer_DoubleClick);
			// 
			// CXPreview
			// 
			this.CXPreview.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mnuAddToImageList});
			this.CXPreview.Popup += new System.EventHandler(this.CXPreview_Popup);
			// 
			// mnuAddToImageList
			// 
			this.mnuAddToImageList.Index = 0;
			this.mnuAddToImageList.Text = "Add to your ImageList";
			this.mnuAddToImageList.Click += new System.EventHandler(this.mnuAddToImageList_Click);
			// 
			// lnkFolder
			// 
			this.lnkFolder.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.lnkFolder.Location = new System.Drawing.Point(5, 5);
			this.lnkFolder.Name = "lnkFolder";
			this.lnkFolder.Size = new System.Drawing.Size(100, 15);
			this.lnkFolder.TabIndex = 0;
			this.lnkFolder.TabStop = true;
			this.lnkFolder.Text = "Look in...";
			this.lnkFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkFolder_LinkClicked);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(170, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(345, 15);
			this.label1.TabIndex = 3;
			this.label1.Text = "Images (double-click or use shortcut menu for more options)";
			// 
			// FPreview
			// 
			this.FPreview.ContextMenu = this.CXImageList;
			this.FPreview.HideSelection = false;
			this.FPreview.LargeImageList = this.ILPreview;
			this.FPreview.Location = new System.Drawing.Point(170, 285);
			this.FPreview.Name = "FPreview";
			this.FPreview.Size = new System.Drawing.Size(535, 115);
			this.FPreview.SmallImageList = this.ILPreview;
			this.FPreview.TabIndex = 3;
			this.FPreview.DoubleClick += new System.EventHandler(this.FPreview_DoubleClick);
			this.FPreview.SelectedIndexChanged += new System.EventHandler(this.FPreview_SelectedIndexChanged);
			// 
			// CXImageList
			// 
			this.CXImageList.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.mnuDeleteFromImageList,
																						this.mnuDeleteAllFromImageList});
			this.CXImageList.Popup += new System.EventHandler(this.CXImageList_Popup);
			// 
			// mnuDeleteFromImageList
			// 
			this.mnuDeleteFromImageList.Index = 0;
			this.mnuDeleteFromImageList.Text = "Remove";
			this.mnuDeleteFromImageList.Click += new System.EventHandler(this.mnuDeleteFromImageList_Click);
			// 
			// mnuDeleteAllFromImageList
			// 
			this.mnuDeleteAllFromImageList.Index = 1;
			this.mnuDeleteAllFromImageList.Text = "Clear all";
			this.mnuDeleteAllFromImageList.Click += new System.EventHandler(this.mnuDeleteAllFromImageList_Click);
			// 
			// ILPreview
			// 
			this.ILPreview.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
			this.ILPreview.ImageSize = new System.Drawing.Size(24, 24);
			this.ILPreview.TransparentColor = System.Drawing.Color.Fuchsia;
			// 
			// lblIL
			// 
			this.lblIL.Location = new System.Drawing.Point(170, 270);
			this.lblIL.Name = "lblIL";
			this.lblIL.Size = new System.Drawing.Size(385, 15);
			this.lblIL.TabIndex = 5;
			this.lblIL.Text = "Your ImageList";
			// 
			// FDlgFolder
			// 
			this.FDlgFolder.Description = "Select root directory of images directories...";
			this.FDlgFolder.ShowNewFolderButton = false;
			// 
			// lnkApply
			// 
			this.lnkApply.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lnkApply.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.lnkApply.Location = new System.Drawing.Point(590, 407);
			this.lnkApply.Name = "lnkApply";
			this.lnkApply.Size = new System.Drawing.Size(45, 20);
			this.lnkApply.TabIndex = 6;
			this.lnkApply.TabStop = true;
			this.lnkApply.Text = "Apply";
			this.lnkApply.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkApply_LinkClicked);
			// 
			// linkLabel1
			// 
			this.linkLabel1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabel1.Location = new System.Drawing.Point(640, 407);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(55, 20);
			this.linkLabel1.TabIndex = 7;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Cancel";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(5, 270);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(155, 15);
			this.label3.TabIndex = 8;
			this.label3.Text = "Hey God, You are great!";
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.White;
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(5, 285);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(160, 115);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBox1.TabIndex = 10;
			this.pictureBox1.TabStop = false;
			// 
			// lnkMoveBack
			// 
			this.lnkMoveBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lnkMoveBack.ImageIndex = 5;
			this.lnkMoveBack.ImageList = this.ILTree;
			this.lnkMoveBack.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.lnkMoveBack.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(64)), ((System.Byte)(0)));
			this.lnkMoveBack.Location = new System.Drawing.Point(170, 405);
			this.lnkMoveBack.Name = "lnkMoveBack";
			this.lnkMoveBack.Size = new System.Drawing.Size(85, 16);
			this.lnkMoveBack.TabIndex = 4;
			this.lnkMoveBack.TabStop = true;
			this.lnkMoveBack.Text = "Move to left";
			this.lnkMoveBack.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lnkMoveBack.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkMoveBack_LinkClicked);
			// 
			// lnkMoveNext
			// 
			this.lnkMoveNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lnkMoveNext.ImageIndex = 6;
			this.lnkMoveNext.ImageList = this.ILTree;
			this.lnkMoveNext.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.lnkMoveNext.LinkColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(64)), ((System.Byte)(0)));
			this.lnkMoveNext.Location = new System.Drawing.Point(265, 405);
			this.lnkMoveNext.Name = "lnkMoveNext";
			this.lnkMoveNext.Size = new System.Drawing.Size(90, 16);
			this.lnkMoveNext.TabIndex = 5;
			this.lnkMoveNext.TabStop = true;
			this.lnkMoveNext.Text = "Move to right";
			this.lnkMoveNext.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lnkMoveNext.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkMoveNext_LinkClicked);
			// 
			// PB
			// 
			this.PB.Location = new System.Drawing.Point(190, 120);
			this.PB.Name = "PB";
			this.PB.Size = new System.Drawing.Size(500, 23);
			this.PB.TabIndex = 13;
			this.PB.Visible = false;
			// 
			// ImageListAssignerSelector
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(709, 428);
			this.Controls.Add(this.PB);
			this.Controls.Add(this.lnkMoveNext);
			this.Controls.Add(this.lnkMoveBack);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.lnkApply);
			this.Controls.Add(this.lblIL);
			this.Controls.Add(this.FPreview);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lnkFolder);
			this.Controls.Add(this.FViewer);
			this.Controls.Add(this.TreeFolder);
			this.Controls.Add(this.pictureBox1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ImageListAssignerSelector";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ImageListAssigner";
			this.Load += new System.EventHandler(this.ImageListAssignerSelector_Load);
			this.ResumeLayout(false);

		}
		#endregion

		class NodeData {
			public string Path;
			public bool Scanned = false;

			public string Folder {
				get {
					int Index = Path.LastIndexOf("\\");
					if (Index==-1) return "(?)";
					return Path.Substring(Index+1);
				}
			}

			public NodeData(string APath) {
				Path = APath;
			}
		}

		public ImageListAssigner Assigner;
		private const string Identity = "ImageList Assigner";
		private string FGlyphPath = string.Empty;
		private const int FolderImageIndex = 4;

		private void InitForm() {
			RegistryKey AppKey = Registry.CurrentUser.OpenSubKey(Info.RegImageListAssigner);
			if (AppKey!=null) FGlyphPath = AppKey.GetValue("GlyphPath",String.Empty).ToString();
			if (FGlyphPath!=String.Empty) FDlgFolder.SelectedPath = FGlyphPath;
			NodeData Data = new NodeData(FGlyphPath);
			TreeFolder.Nodes[0].Text = Data.Folder;
			TreeFolder.Nodes[0].Tag = Data;
			BrowseNode(TreeFolder.Nodes[0]);
		}

		private void lnkFolder_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			if (FDlgFolder.ShowDialog()!=DialogResult.OK) return;
			FGlyphPath = FDlgFolder.SelectedPath;
			RegistryKey AppKey = Registry.CurrentUser.CreateSubKey(Info.RegImageListAssigner);
			if (AppKey!=null) AppKey.SetValue("GlyphPath",FGlyphPath);	
			TreeFolder.Nodes[0].Nodes.Clear();
			NodeData Data = (NodeData)TreeFolder.Nodes[0].Tag;
			Data.Scanned = false;
			Data.Path = FGlyphPath;
			TreeFolder.Nodes[0].Text = Data.Folder;
			BrowseNode(TreeFolder.SelectedNode);
		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			Close();
			TreeFolder.Focus();
		}

		private void ImageListAssignerSelector_Load(object sender, System.EventArgs e) {
			if (Assigner.Destination==null) {
				MessageBox.Show("Please specify an ImageList in Destination property.",Identity);
				Close();
				return;
			} else {
				int iW = Assigner.Destination.ImageSize.Width;
				int iH = Assigner.Destination.ImageSize.Height;
				lblIL.Text = String.Format("ImageList Size {0}x{1}, TransparentColor = {2}",
					iW,iH,Assigner.Destination.TransparentColor.ToString());
				FPreview.Items.Clear();
				ILPreview.Images.Clear();
				IL.ColorDepth = Assigner.Destination.ColorDepth;
				IL.ImageSize = Assigner.Destination.ImageSize;
				IL.TransparentColor = Assigner.Destination.TransparentColor;
				ILPreview.ColorDepth = IL.ColorDepth;
				ILPreview.ImageSize = IL.ImageSize;
				ILPreview.TransparentColor = IL.TransparentColor;
				for (int i=0; i < Assigner.Destination.Images.Count; i++) {
					ILPreview.Images.Add(Assigner.Destination.Images[i]);
					FPreview.Items.Add(i.ToString(),i);
				}
				if (!TreeFolder.Nodes[0].IsExpanded) {
					TreeFolder.Nodes[0].Expand();
					TreeFolder.SelectedNode = TreeFolder.Nodes[0];
				}
				FPreview_SelectedIndexChanged(null,null);
			}

		}

		private void BrowseNode(TreeNode Node) {
			if (Node==null||Node.Tag==null) return;
			NodeData Data = (NodeData)Node.Tag;
			if (Data.Scanned) return;
			if (!Directory.Exists(Data.Path)) return;

			string[] Dirs = Directory.GetDirectories(Data.Path);
			Array.Sort(Dirs);
			foreach (string FName in Dirs) {
				NodeData SubData = new NodeData(FName);
				TreeNode SubNode = new TreeNode(SubData.Folder,FolderImageIndex,FolderImageIndex);
				SubNode.Tag = SubData;
				Node.Nodes.Add(SubNode);
			}
			Data.Scanned = true;
			if (Node.Parent==null) Node.Expand();
		}

		private string ExtractFileName(string FName) {
			int Index = FName.LastIndexOf("\\");
			if (Index==-1) return FName;
			return FName.Substring(Index+1);
		}

		private void BrowseFile(TreeNode Node) {
			if (Node==null||Node.Tag==null) return;
			NodeData Data = (NodeData)Node.Tag;
			if (!Directory.Exists(Data.Path)) return;
			FViewer.Items.Clear();
			IL.Images.Clear();
			string[] FileList = Directory.GetFiles(Data.Path,"*.bmp");
			Array.Sort(FileList);
			int Index = 0;
			FViewer.Visible = false;
			PB.Value = 0;
			PB.Maximum = FileList.Length;
			PB.Visible = true;
			try {
				foreach (string FName in FileList) {
					Image ILImage = Image.FromFile(FName);
					IL.Images.Add(ILImage, Assigner.Destination.TransparentColor);
					ListViewItem LVItem = new ListViewItem(ExtractFileName(FName),Index++);
					FViewer.Items.Add(LVItem);
					PB.Value = PB.Value + 1;
				}
			} catch {
			}
			PB.Visible = false;
			FViewer.Visible = true;
		}

		private void TreeFolder_DoubleClick(object sender, System.EventArgs e) {
			if (TreeFolder.SelectedNode!=null) {
				BrowseNode(TreeFolder.SelectedNode);
				TreeFolder.SelectedNode.Expand();
			}
		}

		private void TreeFolder_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			if (TreeFolder.SelectedNode!=null) {
				BrowseNode(TreeFolder.SelectedNode);
				BrowseFile(TreeFolder.SelectedNode);
			}
		}

		private void FViewer_DoubleClick(object sender, System.EventArgs e) {
			foreach (ListViewItem LVItem in FViewer.SelectedItems) {
				int Index = LVItem.ImageIndex;
				Image Img = IL.Images[Index];
				ILPreview.Images.Add(Img);
				int AddedIndex = ILPreview.Images.Count - 1;
				FPreview.Items.Add(new ListViewItem(AddedIndex.ToString(),AddedIndex));
			}
		}

		private void FPreview_DoubleClick(object sender, System.EventArgs e) {
			int FirstRemoved = 0;
			foreach (ListViewItem Item in FPreview.SelectedItems) {
				int Index = Item.Index;
				ILPreview.Images.RemoveAt(Index);
				FPreview.Items.RemoveAt(Index);
				if (FirstRemoved > Index) FirstRemoved = Index;
			}
			for (int i=FirstRemoved; i < FPreview.Items.Count; i++) {
				FPreview.Items[i].ImageIndex = FPreview.Items[i].Index;
				FPreview.Items[i].Text = FPreview.Items[i].Index.ToString();
			}
		}

		private void CXImageList_Popup(object sender, System.EventArgs e) {
			mnuDeleteFromImageList.Enabled = (FPreview.SelectedItems.Count > 0);
			mnuDeleteAllFromImageList.Enabled = (FPreview.Items.Count > 0);
		}

		private void CXPreview_Popup(object sender, System.EventArgs e) {
			mnuAddToImageList.Enabled = (FViewer.SelectedItems.Count > 0);
		}

		private void mnuAddToImageList_Click(object sender, System.EventArgs e) {
			FViewer_DoubleClick(null,null);
		}

		private void mnuDeleteFromImageList_Click(object sender, System.EventArgs e) {
			FPreview_DoubleClick(null,null);
		}

		private void mnuDeleteAllFromImageList_Click(object sender, System.EventArgs e) {
			FPreview.Items.Clear();
			ILPreview.Images.Clear();
		}

		private void lnkApply_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			ImageList Destination = Assigner.Destination;
			Destination.Images.Clear();
			foreach (Image Img in ILPreview.Images) {
				Destination.Images.Add(Img);
			}
			bool Alert = true;
			RegistryKey AppKey = Registry.CurrentUser.CreateSubKey(Info.RegImageListAssigner);
			Alert = Convert.ToBoolean(AppKey.GetValue("Alert",Alert));
			if (Alert) {
				DialogResult R = MessageBox.Show("Please save your Form, close and re-open to update ImageList's rerources. Click No to prevent this message in furture.",Identity,MessageBoxButtons.YesNo);
				if (R == DialogResult.No) {
					AppKey = Registry.CurrentUser.CreateSubKey(Info.RegImageListAssigner);
					AppKey.SetValue("Alert",false);
				}
			}
			Close();
		}

		private void lnkMoveBack_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			int Index = FPreview.SelectedItems[0].Index;
			Image Img1 = (Image)ILPreview.Images[Index].Clone();
			Image Img2 = (Image)ILPreview.Images[Index-1].Clone();
			ILPreview.Images[Index] = Img2;
			ILPreview.Images[Index-1] = Img1;
			FPreview.SelectedItems.Clear();
			FPreview.Items[Index-1].Selected = true;
			FPreview.Refresh();
		}

		private void lnkMoveNext_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			int Index = FPreview.SelectedItems[0].Index;
			Image Img1 = (Image)ILPreview.Images[Index].Clone();
			Image Img2 = (Image)ILPreview.Images[Index+1].Clone();
			ILPreview.Images[Index] = Img2;
			ILPreview.Images[Index+1] = Img1;
			FPreview.SelectedItems.Clear();
			FPreview.Items[Index+1].Selected = true;
			FPreview.Refresh();
		}

		private void FPreview_SelectedIndexChanged(object sender, System.EventArgs e) {
			lnkMoveBack.Enabled = ((FPreview.SelectedItems.Count==1)&&(FPreview.SelectedItems[0].Index > 0));
			lnkMoveNext.Enabled = ((FPreview.SelectedItems.Count==1)&&(FPreview.SelectedItems[0].Index < FPreview.Items.Count-1));
		}

	}
}
