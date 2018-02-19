using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel.Design.Serialization;
using System.Data;
using CustomControls.Win32Controls;
using CustomControls.Enumerations;

namespace CustomControls.CollectionEditor
{
	
	public class CustomCollectionEditorForm : System.Windows.Forms.Form
	{

		#region "Variables"

		public delegate void InstanceEventHandler(object sender, object instance); 
		public event InstanceEventHandler InstanceCreated;
		public event InstanceEventHandler DestroyingInstance;
		public event InstanceEventHandler ItemRemoved;
		public event InstanceEventHandler ItemAdded;
		private IList _Collection=null;
		private Type lastItemType=null;
		private ArrayList backupList=null;
		private EditLevel _EditLevel=EditLevel.FullEdit;
		protected System.Windows.Forms.PropertyGrid pg_PropGrid;
		protected CustomControls.Win32Controls.DropDownListBoxButton btn_Add;
		protected CustomControls.Win32Controls.PushButton btn_Remove;
		protected CustomControls.Win32Controls.PushButton btn_Up;
		protected CustomControls.Win32Controls.PushButton btn_Down;
		private System.Windows.Forms.Panel pan_Items;
		private System.Windows.Forms.Panel pan_MainPan;
		private System.Windows.Forms.Splitter spl_Splitter;
		private System.Windows.Forms.Panel pan_ButtonsPan;
		protected CustomControls.Win32Controls.PushButton btn_OK;
		protected CustomControls.Win32Controls.PushButton btn_Cancel;
		protected System.Windows.Forms.TreeView tv_Items;
		private System.Windows.Forms.Panel pan_PropGridPan;
		private CustomCollectionEditor attachedEditor=null;
		
	
		
		#endregion

		#region "Properties"

		public IList Collection
		{
			get{return _Collection;}
			set
			{
				_Collection=value;
				backupList= new ArrayList(value);
				ProccessCollection(value);
				RefreshValues();
			}
		}

		[Category("Behavior")]
		public EditLevel EditLevel
		{
			get{return _EditLevel;}
			set
			{
				if(value!=_EditLevel)
				{
					_EditLevel= value;
					OnEditLevelChanged(new EventArgs());
				}
			}
		}
		

		[Category("Behavior")]
		public ImageList ImageList
		{
			get{return tv_Items.ImageList;}
			set{tv_Items.ImageList= value;}
		}


		#endregion

		#region "Constructors"

		public CustomCollectionEditorForm()
		{
			InitializeComponent();
			RefreshValues();
		}
		

		#endregion
			
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CustomCollectionEditorForm));
			this.pg_PropGrid = new System.Windows.Forms.PropertyGrid();
			this.btn_Add = new CustomControls.Win32Controls.DropDownListBoxButton();
			this.btn_Remove = new CustomControls.Win32Controls.PushButton();
			this.btn_Up = new CustomControls.Win32Controls.PushButton();
			this.btn_Down = new CustomControls.Win32Controls.PushButton();
			this.pan_Items = new System.Windows.Forms.Panel();
			this.tv_Items = new System.Windows.Forms.TreeView();
			this.pan_MainPan = new System.Windows.Forms.Panel();
			this.spl_Splitter = new System.Windows.Forms.Splitter();
			this.pan_PropGridPan = new System.Windows.Forms.Panel();
			this.pan_ButtonsPan = new System.Windows.Forms.Panel();
			this.btn_Cancel = new CustomControls.Win32Controls.PushButton();
			this.btn_OK = new CustomControls.Win32Controls.PushButton();
			((System.ComponentModel.ISupportInitialize)(this.btn_Add)).BeginInit();
			this.pan_Items.SuspendLayout();
			this.pan_MainPan.SuspendLayout();
			this.pan_PropGridPan.SuspendLayout();
			this.pan_ButtonsPan.SuspendLayout();
			this.SuspendLayout();
			// 
			// pg_PropGrid
			// 
			this.pg_PropGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pg_PropGrid.BackColor = System.Drawing.SystemColors.Control;
			this.pg_PropGrid.CommandsVisibleIfAvailable = true;
			this.pg_PropGrid.LargeButtons = false;
			this.pg_PropGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.pg_PropGrid.Location = new System.Drawing.Point(7, 6);
			this.pg_PropGrid.Name = "pg_PropGrid";
			this.pg_PropGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
			this.pg_PropGrid.Size = new System.Drawing.Size(224, 346);
			this.pg_PropGrid.TabIndex = 3;
			this.pg_PropGrid.Text = "propertyGrid1";
			this.pg_PropGrid.ToolbarVisible = false;
			this.pg_PropGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.pg_PropGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			this.pg_PropGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pg_PropertyValueChanged);
			this.pg_PropGrid.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.pg_PropGrid_SelectedGridItemChanged);
			// 
			// btn_Add
			// 
			this.btn_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btn_Add.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.btn_Add.Location = new System.Drawing.Point(8, 329);
			this.btn_Add.Name = "btn_Add";
			this.btn_Add.Size = new System.Drawing.Size(80, 24);
			this.btn_Add.TabIndex = 4;
			this.btn_Add.Text = "Add";
			this.btn_Add.ItemSelected += new CustomControls.Win32Controls.DropDownListBoxButton.ItemSelectedEventHandler(this.btn_Add_ItemSelected);
			// 
			// btn_Remove
			// 
			this.btn_Remove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_Remove.BackColor = System.Drawing.SystemColors.Desktop;
			this.btn_Remove.ForeColor = System.Drawing.SystemColors.ControlLight;
			this.btn_Remove.Location = new System.Drawing.Point(108, 329);
			this.btn_Remove.Name = "btn_Remove";
			this.btn_Remove.Size = new System.Drawing.Size(80, 24);
			this.btn_Remove.TabIndex = 6;
			this.btn_Remove.Text = "Remove";
			this.btn_Remove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn_Remove.Click += new System.EventHandler(this.btn_Remove_Click);
			// 
			// btn_Up
			// 
			this.btn_Up.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_Up.Image = ((System.Drawing.Image)(resources.GetObject("btn_Up.Image")));
			this.btn_Up.Location = new System.Drawing.Point(196, 8);
			this.btn_Up.Name = "btn_Up";
			this.btn_Up.Size = new System.Drawing.Size(23, 32);
			this.btn_Up.TabIndex = 1;
			this.btn_Up.Click += new System.EventHandler(this.btn_Up_Click);
			// 
			// btn_Down
			// 
			this.btn_Down.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_Down.Image = ((System.Drawing.Image)(resources.GetObject("btn_Down.Image")));
			this.btn_Down.Location = new System.Drawing.Point(196, 48);
			this.btn_Down.Name = "btn_Down";
			this.btn_Down.Size = new System.Drawing.Size(23, 32);
			this.btn_Down.TabIndex = 2;
			this.btn_Down.Click += new System.EventHandler(this.btn_Down_Click);
			// 
			// pan_Items
			// 
			this.pan_Items.Controls.Add(this.tv_Items);
			this.pan_Items.Controls.Add(this.btn_Down);
			this.pan_Items.Controls.Add(this.btn_Remove);
			this.pan_Items.Controls.Add(this.btn_Add);
			this.pan_Items.Controls.Add(this.btn_Up);
			this.pan_Items.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pan_Items.Location = new System.Drawing.Point(0, 0);
			this.pan_Items.Name = "pan_Items";
			this.pan_Items.Size = new System.Drawing.Size(228, 360);
			this.pan_Items.TabIndex = 9;
			// 
			// tv_Items
			// 
			this.tv_Items.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tv_Items.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tv_Items.FullRowSelect = true;
			this.tv_Items.HideSelection = false;
			this.tv_Items.ImageIndex = -1;
			this.tv_Items.Indent = 25;
			this.tv_Items.Location = new System.Drawing.Point(7, 7);
			this.tv_Items.Name = "tv_Items";
			this.tv_Items.SelectedImageIndex = -1;
			this.tv_Items.Size = new System.Drawing.Size(182, 314);
			this.tv_Items.TabIndex = 0;
			this.tv_Items.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_Items_AfterSelect);
			this.tv_Items.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tv_Items_BeforeSelect);
			// 
			// pan_MainPan
			// 
			this.pan_MainPan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pan_MainPan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pan_MainPan.Controls.Add(this.spl_Splitter);
			this.pan_MainPan.Controls.Add(this.pan_Items);
			this.pan_MainPan.Controls.Add(this.pan_PropGridPan);
			this.pan_MainPan.Location = new System.Drawing.Point(6, 6);
			this.pan_MainPan.Name = "pan_MainPan";
			this.pan_MainPan.Size = new System.Drawing.Size(468, 362);
			this.pan_MainPan.TabIndex = 11;
			// 
			// spl_Splitter
			// 
			this.spl_Splitter.BackColor = System.Drawing.SystemColors.ControlDark;
			this.spl_Splitter.Dock = System.Windows.Forms.DockStyle.Right;
			this.spl_Splitter.Location = new System.Drawing.Point(226, 0);
			this.spl_Splitter.MinExtra = 216;
			this.spl_Splitter.MinSize = 208;
			this.spl_Splitter.Name = "spl_Splitter";
			this.spl_Splitter.Size = new System.Drawing.Size(2, 360);
			this.spl_Splitter.TabIndex = 10;
			this.spl_Splitter.TabStop = false;
			// 
			// pan_PropGridPan
			// 
			this.pan_PropGridPan.Controls.Add(this.pg_PropGrid);
			this.pan_PropGridPan.Dock = System.Windows.Forms.DockStyle.Right;
			this.pan_PropGridPan.Location = new System.Drawing.Point(228, 0);
			this.pan_PropGridPan.Name = "pan_PropGridPan";
			this.pan_PropGridPan.Size = new System.Drawing.Size(238, 360);
			this.pan_PropGridPan.TabIndex = 10;
			// 
			// pan_ButtonsPan
			// 
			this.pan_ButtonsPan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pan_ButtonsPan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pan_ButtonsPan.Controls.Add(this.btn_Cancel);
			this.pan_ButtonsPan.Controls.Add(this.btn_OK);
			this.pan_ButtonsPan.Location = new System.Drawing.Point(6, 376);
			this.pan_ButtonsPan.Name = "pan_ButtonsPan";
			this.pan_ButtonsPan.Size = new System.Drawing.Size(468, 42);
			this.pan_ButtonsPan.TabIndex = 12;
			// 
			// btn_Cancel
			// 
			this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_Cancel.Location = new System.Drawing.Point(378, 8);
			this.btn_Cancel.Name = "btn_Cancel";
			this.btn_Cancel.Size = new System.Drawing.Size(80, 24);
			this.btn_Cancel.TabIndex = 7;
			this.btn_Cancel.Text = "Cancel";
			this.btn_Cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
			// 
			// btn_OK
			// 
			this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btn_OK.Location = new System.Drawing.Point(8, 8);
			this.btn_OK.Name = "btn_OK";
			this.btn_OK.Size = new System.Drawing.Size(80, 24);
			this.btn_OK.TabIndex = 6;
			this.btn_OK.Text = "Ok";
			this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
			// 
			// CustomCollectionEditorForm
			// 
			this.AcceptButton = this.btn_OK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.CancelButton = this.btn_Cancel;
			this.ClientSize = new System.Drawing.Size(480, 424);
			this.ControlBox = false;
			this.Controls.Add(this.pan_ButtonsPan);
			this.Controls.Add(this.pan_MainPan);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MinimumSize = new System.Drawing.Size(480, 300);
			this.Name = "CustomCollectionEditorForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "CustomCollectionEditor";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.btn_Add)).EndInit();
			this.pan_Items.ResumeLayout(false);
			this.pan_MainPan.ResumeLayout(false);
			this.pan_PropGridPan.ResumeLayout(false);
			this.pan_ButtonsPan.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
	
		#region "Collection Item"

		/// <summary>
		/// Gets the data type of each item in the collection.
		/// </summary>
		/// <param name="coll"> The collection for which to get the item's type</param>
		/// <returns>The data type of the collection items.</returns>
		protected virtual Type GetItemType(IList coll)
		{
			PropertyInfo pi= coll.GetType().GetProperty("Item",new Type[]{typeof(int)});
			return pi.PropertyType;
		}

		/// <summary>
		/// Gets the data types that this collection editor can contain
		/// </summary>
		/// <param name="coll">The collection for which to return the available types</param>
		/// <returns>An array of data types that this collection can contain.</returns>
		protected virtual Type[] CreateNewItemTypes(IList coll) 
		{
			return new Type[]{ GetItemType(coll)};
		} 
		
		/// <summary>
		/// Creates a new instance of the specified collection item type.
		/// </summary>
		/// <param name="itemType">The type of item to create. </param>
		/// <returns>A new instance of the specified object.</returns>
		protected virtual object CreateInstance(Type itemType)
		{	
					
			/* 
			//This is just another way of how to remotely  create an object
			
			// Try to get a parameterless constructor
			ConstructorInfo ci = itemType.GetConstructor(new Type[0]);
			InstanceDescriptor id =  new InstanceDescriptor(ci,null,false);
			return id.Invoke();
			*/
			

			object instance=Activator.CreateInstance(itemType,true);
			OnInstanceCreated(instance);
			return instance ;
		}

		/// <summary>
		/// Destroys the specified instance of the object.
		/// </summary>
		/// <param name="instance">The object to destroy. </param>
		protected virtual void DestroyInstance(	object instance	)
		{	
			OnDestroyingInstance(instance);
			if(instance is IDisposable){((IDisposable)instance).Dispose();}
			instance=null;
		}


		protected virtual void OnDestroyingInstance(object instance)
		{
			if( DestroyingInstance!=null)
			{
				DestroyingInstance(this,instance);
			}
		}
	

		protected virtual void OnInstanceCreated(object instance)
		{
			if (InstanceCreated!=null)
			{
				InstanceCreated(this,instance);
			}
		}
		
		protected virtual void OnItemRemoved(object item)
		{
			if(ItemRemoved!=null)
			{
				ItemRemoved(this,item);
			}
		}

		protected virtual void OnItemAdded(object Item)
		{
			if(ItemAdded!=null)
			{
				ItemAdded(this,Item);
			}
		}


		#endregion
		
		#region "TItem Related"		

		private void MoveItem(IList list,int  index, int step)
		{

			if(index>-1 && index <list.Count && index+step >-1 && index+step<list.Count)
			{
				int poss=index+step;

				object possObject=list[poss];
				list[poss]=list[index];
				list[index]=possObject;
				possObject=null;
			}
			
		}
 
	
		protected internal  TItem[] GenerateTItemArray(IList collection)
		{
			TItem[] ti=new TItem[0];

			if (collection !=null && collection.Count>0)
			{
				ti= new TItem[collection.Count];
				
				for(int i=0;i<collection.Count;i++)
				{
					ti[i]=CreateTItem(collection[i]);
				}
			}
			return ti;
		}

		/// <summary>
		/// Creates a new TItem objectfor a collection item.
		/// </summary>
		/// <param name="reffObject">The collection item for wich to create an TItem object.</param>
		/// <returns>A TItem object referencing the collection item received as parameter.</returns>
		protected virtual TItem CreateTItem(object reffObject)
		{
			TItem ti= new TItem(this,reffObject);
			SetProperties(ti,reffObject);
			return ti;
		}

		/// <summary>
		/// When implemented by a class, customize a TItem object in respect to it's corresponding collection item.
		/// </summary>
		/// <param name="titem">The TItem object to be customized in respect to it's corresponding collection item.</param>
		/// <param name="reffObject">The collection item for which it customizes the TItem object.</param>
		protected virtual void SetProperties(TItem titem,object reffObject)
		{
			// set the display name 
			PropertyInfo pi=titem.Value.GetType().GetProperty("Name");

			if(pi!=null )
			{
				titem.Text= pi.GetValue(titem.Value,null).ToString();
			}
			else
			{
				titem.Text= titem.Value.GetType().Name;
			}
						
		}
	
	
	
		#endregion

		#region "Implementation"

		protected virtual void RefreshValues()
		{
			tv_Items.BeginUpdate();
			tv_Items.Nodes.Clear();
			tv_Items.Nodes.AddRange(GenerateTItemArray(this.Collection));

			tv_Items.EndUpdate();
		}

		protected virtual EditLevel SetEditLevel(IList collection)
		{
			return EditLevel.FullEdit;
		}
	
		private void SetCollectionEditLevel(IList collection)
		{
			EditLevel el=SetEditLevel(collection);

			switch(el)
			{
				case EditLevel.FullEdit:
				{
					this.btn_Remove.Enabled=Remove_CanEnable() ;
					this.btn_Add.Enabled=Add_CanEnable();
					break;
				}
				case EditLevel.AddOnly:
				{
					this.btn_Remove.Enabled=Remove_CanEnable() && false ;
					this.btn_Add.Enabled=Add_CanEnable();
					break;
				}
				case EditLevel.RemoveOnly:
				{
					this.btn_Add.Enabled=Add_CanEnable() && false;
					this.btn_Remove.Enabled=Remove_CanEnable() ;
					break;
				}
				case EditLevel.ReadOnly:
				{
					this.btn_Remove.Enabled=Remove_CanEnable() && false ;
					this.btn_Add.Enabled=Add_CanEnable() && false;
					break;
				}
			}

			
		}

		private bool Add_CanEnable()
		{
			if(this.EditLevel==EditLevel.FullEdit ||this.EditLevel== EditLevel.AddOnly){return true;}
			return false;
		}

		private bool Remove_CanEnable()
		{
			if(this.EditLevel==EditLevel.FullEdit || this.EditLevel==EditLevel.RemoveOnly){return true;}
			return false;
		}
		
		protected virtual void RefreshAvailableTypes(IList collection)
		{
			btn_Add.List.Items.Clear();
			btn_Add.List.Items.AddRange(CreateNewItemTypes(collection));
			if(btn_Add.List.Items.Count<2){btn_Add.CanDropDown=false;}
			else{btn_Add.CanDropDown=true;}
			btn_Add.MinListWidth=CalculateDDListWidth();
		}

		private void ProccessCollection(IList collection)
		{
			RefreshAvailableTypes(collection);
			SetCollectionEditLevel(collection);
		}
	
		private int CalculateDDListWidth()
		{
			int width=0;
			Graphics g=btn_Add.List.CreateGraphics();
				
			foreach (object item in this.btn_Add.List.Items)
			{
				int itemWidth=(int)g.MeasureString(item.ToString(),btn_Add.List.Font).Width;
				width= Math.Max(width,itemWidth);
			}
			width=Math.Max(width,btn_Add.Width+20);
			return width;
		}

		private void btn_OK_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

	
		private void btn_Cancel_Click(object sender, System.EventArgs e)
		{
			UndoChanges(backupList, Collection);
			this.Close();
		
		}

		
		private void btn_Add_ItemSelected(object sender, CustomControls.HelperClasses.ItemSelectedEventArgs e)
		{
			tv_Items.BeginUpdate();
			if(Collection!=null && e.SelectedItem!=null)
			{
				//create a new item to add to the Collection and a corespondent TItem to add to the treeview nodes
				Type type=(Type)e.SelectedItem;
				object newCollItem=CreateInstance(type);
				TItem  newTItem=CreateTItem(newCollItem);
				
				//get the current  possition  and the parent collections to insert into
				TItem selTItem= (TItem)tv_Items.SelectedNode;

				if(selTItem!=null)
				{
					int position =selTItem.Index+1;

					IList coll;
					TreeNodeCollection TItemColl;

					if(selTItem.Parent!=null)
					{
						coll=(((TItem)selTItem.Parent).SubItems);
						TItemColl=selTItem.Parent.Nodes;
					}
					else
					{
						coll=Collection;
						TItemColl=tv_Items.Nodes;
					}
		

					coll.Insert(position,newCollItem);
					TItemColl.Insert(position, newTItem);

												
				}
				else //empty collection
				{
					Collection.Add(newCollItem);
					tv_Items.Nodes.Add(newTItem);

				}

				OnItemAdded(newCollItem);
				tv_Items.SelectedNode=newTItem;
								
			}
			tv_Items.EndUpdate();	
		}

		
		private void btn_Remove_Click(object sender, System.EventArgs e)
		{
			tv_Items.BeginUpdate();
			TItem selTItem= (TItem)tv_Items.SelectedNode;
			if(selTItem!=null)
			{
				int selIndex=selTItem.Index;
				TItem parentTitem= (TItem)selTItem.Parent;
				if(parentTitem!=null)
				{
					parentTitem.Nodes.Remove(selTItem);
					parentTitem.SubItems.Remove(selTItem.Value);
					if(parentTitem.Nodes.Count>selIndex){tv_Items.SelectedNode=parentTitem.Nodes[selIndex];}
					else if(parentTitem.Nodes.Count>0){tv_Items.SelectedNode=parentTitem.Nodes[selIndex-1];}
					else{ tv_Items.SelectedNode=parentTitem;}
				}
				else
				{
					tv_Items.Nodes.Remove(selTItem);
					Collection.Remove(selTItem.Value);
					if(tv_Items.Nodes.Count>selIndex){tv_Items.SelectedNode=tv_Items.Nodes[selIndex];}
					else if(tv_Items.Nodes.Count>0){tv_Items.SelectedNode=tv_Items.Nodes[selIndex-1];}
					else{ this.pg_PropGrid.SelectedObject=null;}
				}
				
				OnItemRemoved(selTItem.Value);
			}
			tv_Items.EndUpdate();	
		}
	

		private void btn_Up_Click(object sender, System.EventArgs e)
		{
			tv_Items.BeginUpdate();	
			TItem selTItem=(TItem)tv_Items.SelectedNode;
			if(selTItem!=null && selTItem.PrevNode!=null)
			{
				int prevIndex=selTItem.PrevNode.Index;
				TItem fatherTitem=(TItem)selTItem.Parent;
				if(fatherTitem!=null)
				{
					

					MoveItem(fatherTitem.SubItems,fatherTitem.SubItems.IndexOf(selTItem.Value),-1);
					SetProperties(fatherTitem,fatherTitem.Value);
					tv_Items.SelectedNode=fatherTitem.Nodes[prevIndex];
									
				}
				else
				{
					
					MoveItem(Collection,Collection.IndexOf(selTItem.Value),-1);
					tv_Items.Nodes.Clear();
					tv_Items.Nodes.AddRange(GenerateTItemArray(this.Collection));
					tv_Items.SelectedNode=tv_Items.Nodes[prevIndex];
				}
			}
			tv_Items.EndUpdate();	
		}

		
		private void btn_Down_Click(object sender, System.EventArgs e)
		{
			tv_Items.BeginUpdate();	
			TItem selTItem=(TItem)tv_Items.SelectedNode;
			if(selTItem!=null && selTItem.NextNode!=null)
			{
				int nextIndex=selTItem.NextNode.Index;
				TItem fatherTitem=(TItem)selTItem.Parent;

				if(fatherTitem!=null)
				{
					MoveItem(fatherTitem.SubItems,fatherTitem.SubItems.IndexOf(selTItem.Value),1);
					SetProperties(fatherTitem,fatherTitem.Value);
					tv_Items.SelectedNode=fatherTitem.Nodes[nextIndex];
										
				}
				else
				{
					MoveItem(Collection,Collection.IndexOf(selTItem.Value),1);
					tv_Items.Nodes.Clear();
					tv_Items.Nodes.AddRange(GenerateTItemArray(this.Collection));
					tv_Items.SelectedNode=tv_Items.Nodes[nextIndex];
				}
			}
			tv_Items.EndUpdate();	
		}
	

		private   void pg_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			tv_Items.BeginUpdate();	
			TItem selTItem=(TItem)tv_Items.SelectedNode;
			
			SetProperties(selTItem,selTItem.Value);

			tv_Items.EndUpdate();	
		}
		

		private void tv_Items_BeforeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			pg_PropGrid.SelectedObject=((TItem)e.Node).Value;
		}

		private void tv_Items_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			TItem ti=(TItem)e.Node;
			if(ti.Value.GetType()!=lastItemType)
			{
				lastItemType=ti.Value.GetType();
				IList coll;
				if(ti.Parent!=null)	{coll=((TItem)ti.Parent).SubItems;}
				else {coll=Collection;}
				ProccessCollection(coll);
				
			}
		}

	
		private void pg_PropGrid_SelectedGridItemChanged(object sender, System.Windows.Forms.SelectedGridItemChangedEventArgs e)
		{
		
			if (attachedEditor!=null)
			{
				attachedEditor.CollectionChanged-= new CustomCollectionEditor.CollectionChangedEventHandler(ValChanged);
				attachedEditor=null;
			}

			if ( e.NewSelection.Value is IList )
			{
				attachedEditor=(CustomCollectionEditor)e.NewSelection.PropertyDescriptor.GetEditor(typeof(System.Drawing.Design.UITypeEditor )) as CustomCollectionEditor;
				if (attachedEditor!=null)
				{
					attachedEditor.CollectionChanged+= new CustomCollectionEditor.CollectionChangedEventHandler(ValChanged);
				}
			}
				
				

		}


		private void ValChanged(object sender,object instance, object value )
		{
			tv_Items.BeginUpdate();	
			TItem ti= (TItem)tv_Items.SelectedNode;
			SetProperties(ti,instance);	
			tv_Items.EndUpdate();	
		}

		private void UndoChanges(IList source, IList dest)
		{
			foreach(object o in dest )
			{
				if(!source.Contains(o))
				{
					DestroyInstance(o);
					OnItemRemoved(o);
				}			
			
			}
	
			dest.Clear();
			CopyItems(source,dest);
		}

		private void CopyItems(IList source, IList dest)
		{			
			foreach (object o in source)
			{
				dest.Add(o);
				OnItemAdded(o);
			}
		}

		protected virtual void OnEditLevelChanged(EventArgs e)
		{
			switch(EditLevel)
			{
				case EditLevel.FullEdit:
				{
					this.btn_Add.Enabled=true;
					this.btn_Remove.Enabled=true;
					break;
				}
				case EditLevel.AddOnly:
				{
					this.btn_Add.Enabled=true;
					this.btn_Remove.Enabled=false;
					break;
				}
				case EditLevel.RemoveOnly:
				{
					this.btn_Add.Enabled=false;
					this.btn_Remove.Enabled=true;
					break;
				}
				case EditLevel.ReadOnly:
				{
					this.btn_Add.Enabled=false;
					this.btn_Remove.Enabled=false;
					break;
				}
			}
		}


		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);
			this.spl_Splitter.SplitPosition=240;
		}

		#endregion

	
	}

	#region "TItem"

	public class TItem:TreeNode
	{
		private object _Value;
			private CustomCollectionEditorForm ced=null;
		private IList _SubItems=null;
	
		
		public object Value
		{
			get{return _Value;}
			set{_Value= value;}
		}

		public IList SubItems
		{
			get{return _SubItems;}
			set
			{
				_SubItems= value;
				this.Nodes.Clear();
				if (value !=null)
				{
					this.Nodes.AddRange(ced.GenerateTItemArray(value));
				}
						
			}
		}

		
		public TItem(CustomCollectionEditorForm  ced,object Value)
		{ 
			this.ced=ced;
			this._Value=Value;					
		}
		
		
		public TItem(CustomCollectionEditorForm  ced,object Value, int ImageIndex)
		{
			this.ced=ced;
			this._Value=Value;
			this.ImageIndex=ImageIndex;
		}
		public TItem(CustomCollectionEditorForm  ced,object Value, int ImageIndex, int SelectedImageIndex)
		{
			this.ced=ced;
			this._Value=Value;
			this.ImageIndex=ImageIndex;
			this.SelectedImageIndex=SelectedImageIndex;
		}

		
		
	}

	#endregion
}

