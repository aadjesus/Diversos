using System;
using System.Windows.Forms;
using System.ComponentModel;
using CustomControls.BaseClasses;
using CustomControls.HelperClasses;

namespace CustomControls.Win32Controls
{
	public class DropDownListBox:DropDownListBase
	{

		public delegate void ItemSelectedEventHandler(object sender, ItemSelectedEventArgs e);
		[Category("Action")]
		public event ItemSelectedEventHandler ItemSelected;

		private ListBox _List= new ListBox();
		private int _MaxDropDawnItems=8;
		private int _MinListWidth=140;

		protected override System.Windows.Forms.Control HostedControl
		{
			get
			{
				return List;
			}
		}

		[Browsable(false)]
		[DefaultValue(typeof(Int32),"-1")]
		public int SelectedIndex
		{
			get{return List.SelectedIndex;}
			set
			{
				if(value!=List.SelectedIndex)
				{
					List.SelectedIndex=value;
					OnItemSelected(new ItemSelectedEventArgs(List.SelectedItem, List.SelectedIndex));
				}
			}
		}

		[Category("Behavior")]
		[DefaultValue(typeof(System.Int32),"140")]
		public int MinListWidth
		{
			get{return _MinListWidth;}
			set{_MinListWidth= value;}
		}

		[Browsable(false)]
		public virtual ListBox List
		{
			get{return _List;}
		}

		[Category("Behavior")]
		[DefaultValue(typeof(System.Int32),"8")]
		public int MaxDropDownItems
		{
			get{return _MaxDropDawnItems;}
			set{_MaxDropDawnItems=value;}
		}

		public DropDownListBox():base()
		{
			List.BorderStyle=System.Windows.Forms.BorderStyle.None;
			List.MouseUp+= new MouseEventHandler(List_MouseUp);

			
		}

		protected override void OnDropDown(System.EventArgs e)
		{
			if (Text!=string.Empty)	{List.SelectedIndex=List.FindString(Text);}
			else{List.SelectedIndex=-1;}

			int lHeight,lWidth;
			lHeight=Math.Max(List.ItemHeight,Math.Min(MaxDropDownItems, List.Items.Count)*List.ItemHeight);
			lWidth=Math.Max(this.Width+20,_MinListWidth);
			dropDownForm.Size= new System.Drawing.Size(lWidth,lHeight+4);
			
		}


		
		
		private void List_MouseUp(object sender , MouseEventArgs e)
		{
			DroppedDown=false;
			OnItemSelected(new ItemSelectedEventArgs(List.SelectedItem, List.SelectedIndex));
		}

		

		protected override void OnNextItem()
		{
			if(List.SelectedIndex<List.Items.Count-1)
			{
				SelectedIndex+=1;
			}
		}

		protected override void OnPrevItem()
		{
			if(List.SelectedIndex>0)
			{
				SelectedIndex-=1;
			}
		}


		

		protected virtual void OnItemSelected(ItemSelectedEventArgs e)
		{

			Text=List.Text;
			editControl.SelectAll();
			if(ItemSelected!=null)
			{
				ItemSelected(this,e);
			}
		}

		
	}

}