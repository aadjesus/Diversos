using System;
using System.Windows.Forms;
using System.ComponentModel;
using CustomControls.BaseClasses;
using CustomControls.HelperClasses;


using System.Drawing.Design;

namespace CustomControls.Win32Controls
{
	public class DropDownListBoxButton:DropDownButtonBase 
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
				return _List;
			}
		}

		[Category("Behavior")]
		[DefaultValue(typeof(System.Int32),"140")]
		public int MinListWidth
		{
			get{return _MinListWidth;}
			set{_MinListWidth= value;}
		}



		[Category("Behavior")]
		[DefaultValue(typeof(System.Int32),"8")]
		public int MaxDropDownItems
		{
			get{return _MaxDropDawnItems;}
			set{_MaxDropDawnItems=value;}
		}


		[Browsable(false)]
		public ListBox List
		{
			get{return _List;}
		}

		public DropDownListBoxButton()
		{
			_List.BorderStyle=System.Windows.Forms.BorderStyle.None;
			_List.MouseUp+= new MouseEventHandler(List_MouseUp);
		}



		protected override void OnDropDown(EventArgs e)
		{
			base.OnDropDown(e);

			if (Text!=string.Empty)	{_List.SelectedIndex=_List.FindString(Text);}
			else{_List.SelectedIndex=-1;}

			int lHeight,lWidth;
			lHeight=Math.Max(List.ItemHeight,Math.Min(MaxDropDownItems, List.Items.Count)*List.ItemHeight);
			lWidth=Math.Max(this.Width+20,MinListWidth);
			dropDownForm.Size= new System.Drawing.Size(lWidth,lHeight+4);
			
		}


				
		private void List_MouseUp(object sender , MouseEventArgs e)
		{
			State=Enumerations.ButtonState.Normal;
			OnItemSelected(new ItemSelectedEventArgs(List.SelectedItem,List.SelectedIndex));
		}

	
		

		protected override void OnNextItem()
		{
			if(_List.SelectedIndex<_List.Items.Count-1)
			{
				_List.SelectedIndex+=1;
			}
		}

		protected override void OnPrevItem()
		{
			if(_List.SelectedIndex>0)
			{
				_List.SelectedIndex-=1;
			}
		}

	


		protected override void OnSelectItem(bool DefaultValue)
		{
			if(DefaultValue)
			{
				if(List.Items.Count>0)
				{
					OnItemSelected(new ItemSelectedEventArgs(List.Items[0],0));
				}
				else
				{
					OnItemSelected(new ItemSelectedEventArgs(null,-1));
				}
			}
			else
			{
				OnItemSelected(new ItemSelectedEventArgs(_List.SelectedItem,_List.SelectedIndex));
			}
		}



		protected virtual void OnItemSelected(ItemSelectedEventArgs e)
		{
			if (ItemSelected!=null)
			{
				ItemSelected(this,e);
			}
		}

		

	}
}
