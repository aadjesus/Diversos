using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Reflection;

namespace Treeview_Rearrange
{
	public class Form1 : System.Windows.Forms.Form
	{
		#region Private Fields
		private int NodeCount, FolderCount;
		private string NodeMap;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Button btnAddNode;
		private System.Windows.Forms.Button btnAddFolder;
		private System.Windows.Forms.Button btnEnable;
		private System.ComponentModel.Container components = null;
		private const int MAPSIZE = 128;
		private StringBuilder NewNodeMap = new StringBuilder(MAPSIZE);
		#endregion
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.btnAddNode = new System.Windows.Forms.Button();
			this.btnAddFolder = new System.Windows.Forms.Button();
			this.btnEnable = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.AllowDrop = true;
			this.treeView1.ImageIndex = -1;
			this.treeView1.Location = new System.Drawing.Point(8, 8);
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = -1;
			this.treeView1.Size = new System.Drawing.Size(216, 224);
			this.treeView1.TabIndex = 0;
			// 
			// btnAddNode
			// 
			this.btnAddNode.Location = new System.Drawing.Point(8, 240);
			this.btnAddNode.Name = "btnAddNode";
			this.btnAddNode.Size = new System.Drawing.Size(104, 23);
			this.btnAddNode.TabIndex = 1;
			this.btnAddNode.Text = "Add Node";
			this.btnAddNode.Click += new System.EventHandler(this.btnAddNode_Click);
			// 
			// btnAddFolder
			// 
			this.btnAddFolder.Location = new System.Drawing.Point(120, 240);
			this.btnAddFolder.Name = "btnAddFolder";
			this.btnAddFolder.Size = new System.Drawing.Size(104, 23);
			this.btnAddFolder.TabIndex = 2;
			this.btnAddFolder.Text = "Add Folder";
			this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
			// 
			// btnEnable
			// 
			this.btnEnable.Location = new System.Drawing.Point(8, 272);
			this.btnEnable.Name = "btnEnable";
			this.btnEnable.Size = new System.Drawing.Size(216, 23);
			this.btnEnable.TabIndex = 3;
			this.btnEnable.Text = "Enable drag drop rearrange support";
			this.btnEnable.Click += new System.EventHandler(this.btnEnable_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(232, 301);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btnEnable,
																		  this.btnAddFolder,
																		  this.btnAddNode,
																		  this.treeView1});
			this.Name = "Form1";
			this.Text = "Treeview Rearrange";
			this.ResumeLayout(false);

		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}
		#endregion

		public Form1()
		{

			InitializeComponent();

			this.NodeCount = 0;
			this.FolderCount= 0;

			ImageList TreeviewIL = new ImageList();
			TreeviewIL.Images.Add(System.Drawing.Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Treeview_Rearrange.resources.folder.png")));
			TreeviewIL.Images.Add(System.Drawing.Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Treeview_Rearrange.resources.node.png")));
			this.treeView1.ImageList = TreeviewIL;
			this.treeView1.HideSelection = false;
			this.treeView1.ItemHeight = this.treeView1.ItemHeight + 3;
			this.treeView1.Indent = this.treeView1.Indent + 3;
			this.btnAddNode_Click(null, EventArgs.Empty);
			this.btnAddNode_Click(null, EventArgs.Empty);
			this.btnAddNode_Click(null, EventArgs.Empty);
			this.btnAddFolder_Click(null, EventArgs.Empty);
			this.btnAddFolder_Click(null, EventArgs.Empty);
			this.btnAddFolder_Click(null, EventArgs.Empty);
			this.btnEnable_Click(null, EventArgs.Empty);
			
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		private void btnAddNode_Click(object sender, System.EventArgs e)
		{
			++this.NodeCount;
			this.treeView1.Nodes.Add(new TreeNode("Node #" + this.NodeCount.ToString(), 1, 1));
		}

		private void btnAddFolder_Click(object sender, System.EventArgs e)
		{
			++this.FolderCount;
			this.treeView1.Nodes.Add(new TreeNode("Folder #" + this.FolderCount.ToString(), 0, 0));
		}

		private void btnEnable_Click(object sender, System.EventArgs e)
		{
			this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
			this.treeView1.DragOver += new System.Windows.Forms.DragEventHandler(this.treeView1_DragOver);
			this.treeView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView1_DragEnter);
			this.treeView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView1_ItemDrag);
			this.treeView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView1_DragDrop);
			this.btnEnable.Enabled = false;
		}

		private void treeView1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			//this.treeView1.SelectedNode = this.treeView1.GetNodeAt(e.X, e.Y);
		}
		private void treeView1_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
		{
			DoDragDrop(e.Item, DragDropEffects.Move);			
		}

		private void treeView1_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			e.Effect = DragDropEffects.Move;
		}
		private void treeView1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			if(e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false) && this.NodeMap != "")
			{				
				TreeNode MovingNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
				string[] NodeIndexes = this.NodeMap.Split('|');
				TreeNodeCollection InsertCollection = this.treeView1.Nodes;
				for(int i = 0; i < NodeIndexes.Length - 1; i++)
				{
					InsertCollection = InsertCollection[Int32.Parse(NodeIndexes[i])].Nodes;
				}

				if(InsertCollection != null)
				{
					InsertCollection.Insert(Int32.Parse(NodeIndexes[NodeIndexes.Length - 1]), (TreeNode)MovingNode.Clone());
					this.treeView1.SelectedNode = InsertCollection[Int32.Parse(NodeIndexes[NodeIndexes.Length - 1])];
					MovingNode.Remove();
				}
			}            
		}

		private void treeView1_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
		{
			TreeNode NodeOver = this.treeView1.GetNodeAt(this.treeView1.PointToClient(Cursor.Position));
			TreeNode NodeMoving = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");


			// A bit long, but to summarize, process the following code only if the nodeover is null
			// and either the nodeover is not the same thing as nodemoving UNLESSS nodeover happens
			// to be the last node in the branch (so we can allow drag & drop below a parent branch)
			if(NodeOver != null && (NodeOver != NodeMoving || (NodeOver.Parent != null && NodeOver.Index == (NodeOver.Parent.Nodes.Count - 1))))
			{
				int OffsetY = this.treeView1.PointToClient(Cursor.Position).Y - NodeOver.Bounds.Top;
				int NodeOverImageWidth = this.treeView1.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;
				Graphics g = this.treeView1.CreateGraphics();
                
				// Image index of 1 is the non-folder icon
				if(NodeOver.ImageIndex == 1)
				{
					#region Standard Node
					if(OffsetY < (NodeOver.Bounds.Height / 2))
					{
						//this.lblDebug.Text = "top";
						
						#region If NodeOver is a child then cancel
						TreeNode tnParadox = NodeOver;
						while(tnParadox.Parent != null)
						{
							if(tnParadox.Parent == NodeMoving)
							{
								this.NodeMap = "";
								return;
							}

							tnParadox = tnParadox.Parent;
						}
						#endregion
						#region Store the placeholder info into a pipe delimited string
						SetNewNodeMap(NodeOver, false);
						if(SetMapsEqual() == true)
							return;
						#endregion
						#region Clear placeholders above and below
						this.Refresh();
						#endregion
						#region Draw the placeholders
						this.DrawLeafTopPlaceholders(NodeOver);
						#endregion
					}
					else
					{
						//this.lblDebug.Text = "bottom";
						
						#region If NodeOver is a child then cancel
						TreeNode tnParadox = NodeOver;
						while(tnParadox.Parent != null)
						{
							if(tnParadox.Parent == NodeMoving)
							{
								this.NodeMap = "";
								return;
							}

							tnParadox = tnParadox.Parent;
						}
						#endregion
						#region Allow drag drop to parent branches
						TreeNode ParentDragDrop = null;
						// If the node the mouse is over is the last node of the branch we should allow
						// the ability to drop the "nodemoving" node BELOW the parent node
						if(NodeOver.Parent != null && NodeOver.Index == (NodeOver.Parent.Nodes.Count - 1))
						{
							int XPos = this.treeView1.PointToClient(Cursor.Position).X;
							if(XPos < NodeOver.Bounds.Left)
							{
								ParentDragDrop = NodeOver.Parent;

								if(XPos < (ParentDragDrop.Bounds.Left - this.treeView1.ImageList.Images[ParentDragDrop.ImageIndex].Size.Width))
								{
									if(ParentDragDrop.Parent != null)
										ParentDragDrop = ParentDragDrop.Parent;
								}
							}
						}
						#endregion
						#region Store the placeholder info into a pipe delimited string
						// Since we are in a special case here, use the ParentDragDrop node as the current "nodeover"
						SetNewNodeMap(ParentDragDrop != null ? ParentDragDrop : NodeOver, true);
						if(SetMapsEqual() == true)
							return;
						#endregion
						#region Clear placeholders above and below
						this.Refresh();
						#endregion
						#region Draw the placeholders
						DrawLeafBottomPlaceholders(NodeOver, ParentDragDrop);
						#endregion
					}
					#endregion
				}
				else
				{
					#region Folder Node
					if(OffsetY < (NodeOver.Bounds.Height / 3))
					{
						//this.lblDebug.Text = "folder top";

						#region If NodeOver is a child then cancel
						TreeNode tnParadox = NodeOver;
						while(tnParadox.Parent != null)
						{
							if(tnParadox.Parent == NodeMoving)
							{
								this.NodeMap = "";
								return;
							}

							tnParadox = tnParadox.Parent;
						}
						#endregion
						#region Store the placeholder info into a pipe delimited string
						SetNewNodeMap(NodeOver, false);
						if(SetMapsEqual() == true)
							return;
						#endregion
						#region Clear placeholders above and below
						this.Refresh();
						#endregion
						#region Draw the placeholders
						this.DrawFolderTopPlaceholders(NodeOver);
						#endregion
					}
					else if((NodeOver.Parent != null && NodeOver.Index == 0) && (OffsetY > (NodeOver.Bounds.Height - (NodeOver.Bounds.Height / 3))))
					{
						//this.lblDebug.Text = "folder bottom";
						
						#region If NodeOver is a child then cancel
						TreeNode tnParadox = NodeOver;
						while(tnParadox.Parent != null)
						{
							if(tnParadox.Parent == NodeMoving)
							{
								this.NodeMap = "";
								return;
							}

							tnParadox = tnParadox.Parent;
						}
						#endregion
						#region Store the placeholder info into a pipe delimited string
						SetNewNodeMap(NodeOver, true);
						if(SetMapsEqual() == true)
							return;
						#endregion
						#region Clear placeholders above and below
						this.Refresh();
						#endregion
						#region Draw the placeholders
						DrawFolderTopPlaceholders(NodeOver);
						#endregion
					}
					else
					{
						//this.lblDebug.Text = "folder over";
					
						if(NodeOver.Nodes.Count > 0)
						{
							NodeOver.Expand();
							//this.Refresh();
						}
						else
						{
							#region Prevent the node from being dragged onto itself
							if(NodeMoving == NodeOver)
								return;
							#endregion
							#region If NodeOver is a child then cancel
							TreeNode tnParadox = NodeOver;
							while(tnParadox.Parent != null)
							{
								if(tnParadox.Parent == NodeMoving)
								{
									this.NodeMap = "";
									return;
								}

								tnParadox = tnParadox.Parent;
							}
							#endregion
							#region Store the placeholder info into a pipe delimited string
							SetNewNodeMap(NodeOver, false);
							NewNodeMap = NewNodeMap.Insert(NewNodeMap.Length, "|0");

							if(SetMapsEqual() == true)
								return;
							#endregion
							#region Clear placeholders above and below
							this.Refresh();
							#endregion
							#region Draw the "add to folder" placeholder
							DrawAddToFolderPlaceholder(NodeOver);
							#endregion
						}
					}
					#endregion
				}
			}
		}



		#region Helper Methods
		private void DrawLeafTopPlaceholders(TreeNode NodeOver)
		{
			Graphics g = this.treeView1.CreateGraphics();

			int NodeOverImageWidth = this.treeView1.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;
			int LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
			int RightPos = this.treeView1.Width - 4;

			Point[] LeftTriangle = new Point[5]{
												   new Point(LeftPos, NodeOver.Bounds.Top - 4),
												   new Point(LeftPos, NodeOver.Bounds.Top + 4),
												   new Point(LeftPos + 4, NodeOver.Bounds.Y),
												   new Point(LeftPos + 4, NodeOver.Bounds.Top - 1),
												   new Point(LeftPos, NodeOver.Bounds.Top - 5)};

			Point[] RightTriangle = new Point[5]{
													new Point(RightPos, NodeOver.Bounds.Top - 4),
													new Point(RightPos, NodeOver.Bounds.Top + 4),
													new Point(RightPos - 4, NodeOver.Bounds.Y),
													new Point(RightPos - 4, NodeOver.Bounds.Top - 1),
													new Point(RightPos, NodeOver.Bounds.Top - 5)};


			g.FillPolygon(System.Drawing.Brushes.Black, LeftTriangle);
			g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle);
			g.DrawLine(new System.Drawing.Pen(Color.Black, 2), new Point(LeftPos, NodeOver.Bounds.Top), new Point(RightPos, NodeOver.Bounds.Top));

		}//eom

		private void DrawLeafBottomPlaceholders(TreeNode NodeOver, TreeNode ParentDragDrop)
		{
			Graphics g = this.treeView1.CreateGraphics();

			int NodeOverImageWidth = this.treeView1.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;
			// Once again, we are not dragging to node over, draw the placeholder using the ParentDragDrop bounds
			int LeftPos, RightPos;
			if(ParentDragDrop != null)
				LeftPos = ParentDragDrop.Bounds.Left - (this.treeView1.ImageList.Images[ParentDragDrop.ImageIndex].Size.Width + 8);
			else
				LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
			RightPos = this.treeView1.Width - 4;

			Point[] LeftTriangle = new Point[5]{
												   new Point(LeftPos, NodeOver.Bounds.Bottom - 4),
												   new Point(LeftPos, NodeOver.Bounds.Bottom + 4),
												   new Point(LeftPos + 4, NodeOver.Bounds.Bottom),
												   new Point(LeftPos + 4, NodeOver.Bounds.Bottom - 1),
												   new Point(LeftPos, NodeOver.Bounds.Bottom - 5)};

			Point[] RightTriangle = new Point[5]{
													new Point(RightPos, NodeOver.Bounds.Bottom - 4),
													new Point(RightPos, NodeOver.Bounds.Bottom + 4),
													new Point(RightPos - 4, NodeOver.Bounds.Bottom),
													new Point(RightPos - 4, NodeOver.Bounds.Bottom - 1),
													new Point(RightPos, NodeOver.Bounds.Bottom - 5)};


			g.FillPolygon(System.Drawing.Brushes.Black, LeftTriangle);
			g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle);
			g.DrawLine(new System.Drawing.Pen(Color.Black, 2), new Point(LeftPos, NodeOver.Bounds.Bottom), new Point(RightPos, NodeOver.Bounds.Bottom));
		}//eom

		private void DrawFolderTopPlaceholders(TreeNode NodeOver)
		{
			Graphics g = this.treeView1.CreateGraphics();
			int NodeOverImageWidth = this.treeView1.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;

			int LeftPos, RightPos;
			LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
			RightPos = this.treeView1.Width - 4;

			Point[] LeftTriangle = new Point[5]{
												   new Point(LeftPos, NodeOver.Bounds.Top - 4),
												   new Point(LeftPos, NodeOver.Bounds.Top + 4),
												   new Point(LeftPos + 4, NodeOver.Bounds.Y),
												   new Point(LeftPos + 4, NodeOver.Bounds.Top - 1),
												   new Point(LeftPos, NodeOver.Bounds.Top - 5)};

			Point[] RightTriangle = new Point[5]{
													new Point(RightPos, NodeOver.Bounds.Top - 4),
													new Point(RightPos, NodeOver.Bounds.Top + 4),
													new Point(RightPos - 4, NodeOver.Bounds.Y),
													new Point(RightPos - 4, NodeOver.Bounds.Top - 1),
													new Point(RightPos, NodeOver.Bounds.Top - 5)};


			g.FillPolygon(System.Drawing.Brushes.Black, LeftTriangle);
			g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle);
			g.DrawLine(new System.Drawing.Pen(Color.Black, 2), new Point(LeftPos, NodeOver.Bounds.Top), new Point(RightPos, NodeOver.Bounds.Top));

		}//eom
		private void DrawAddToFolderPlaceholder(TreeNode NodeOver)
		{
			Graphics g = this.treeView1.CreateGraphics();
			int RightPos = NodeOver.Bounds.Right + 6;
			Point[] RightTriangle = new Point[5]{
													new Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) + 4),
													new Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) + 4),
													new Point(RightPos - 4, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2)),
													new Point(RightPos - 4, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) - 1),
													new Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) - 5)};

			this.Refresh();
			g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle);
		}//eom

		private void SetNewNodeMap(TreeNode tnNode, bool boolBelowNode)
		{
			NewNodeMap.Length = 0;

			if(boolBelowNode)
				NewNodeMap.Insert(0, (int)tnNode.Index + 1);
			else
				NewNodeMap.Insert(0, (int)tnNode.Index);
			TreeNode tnCurNode = tnNode;

			while(tnCurNode.Parent != null)
			{
				tnCurNode = tnCurNode.Parent;

				if(NewNodeMap.Length == 0 && boolBelowNode == true)
				{
					NewNodeMap.Insert(0, (tnCurNode.Index + 1) + "|");
				}
				else
				{
					NewNodeMap.Insert(0, tnCurNode.Index + "|");
				}
			}
		}//oem

		private bool SetMapsEqual()
		{
			if(this.NewNodeMap.ToString() == this.NodeMap)
				return true;
			else
			{
				this.NodeMap = this.NewNodeMap.ToString();
				return false;
			}
		}//oem
		#endregion

	}
}
