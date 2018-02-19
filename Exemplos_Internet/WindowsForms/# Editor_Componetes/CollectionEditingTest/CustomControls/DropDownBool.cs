using System;
using CustomControls.BaseClasses;
using CustomControls.HelperClasses;
using System.Windows.Forms;
using System.ComponentModel;

namespace CustomControls.Win32Controls
{
	
	public class DropDownBool:DropDownListBox
	{
		
		private DropDownBool.ListItem TrueItem = new DropDownBool.ListItem("True");
		private DropDownBool.ListItem FalseItem = new DropDownBool.ListItem("False");
		private bool _Value= true;
		public event EventHandler ValueChanged;
		
		[DefaultValue(typeof(string),"True")]
		public string TrueValueString
		{
			get{return TrueItem.Text;}
			set
			{
				if(value != TrueItem.Text)
				{
					TrueItem.Text= value;
                    
				}
			}
		}

		[DefaultValue(typeof(string),"False")]
		public string FalseValueString
		{
			get{return FalseItem.Text;}
			set
			{
				if(value != FalseItem.Text)
				{
					FalseItem.Text= value;
					
				}
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ComboBoxStyle DropDownStyle
		{
			get{return ComboBoxStyle.DropDownList;}
		}

		[DefaultValue(typeof(bool),"True")]
		public bool Value
		{
			get{return _Value;}
			set
			{
				if(value!=_Value)
				{
					_Value=value;
					OnValueChanged(new EventArgs());
					Invalidate();
				}
			}
		}


		public DropDownBool()
		{
			this.DropDownStyle=ComboBoxStyle.DropDownList;
			this.List.Items.Add(TrueItem);	
			this.List.Items.Add(FalseItem);	

			this.List.SelectedIndexChanged+=new EventHandler(List_SelectedIndexChanged);

		}


		protected virtual void OnValueChanged(System.EventArgs e)
		{
			
			if(Value)
			{
				List.SelectedItem=TrueItem;
				this.Text=TrueValueString;
			}
			else
			{
				List.SelectedItem=FalseItem;
				this.Text=FalseValueString;
			}

			if(ValueChanged!= null)	{ValueChanged(this,e);}
		}

		private void List_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(List.SelectedItem==TrueItem){Value=true;}
			else{Value=false;}
		}	

		

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if(e.KeyData==Keys.Space){Value=!Value;}
		}

		internal class ListItem:object
		{
			private string _Text=string.Empty;
			
			public string Text
			{
				get{return _Text; }
				set{_Text= value;}
			}

			public ListItem()
			{}

			public ListItem(string Text)
			{
				_Text= Text;
			}

			public override string ToString()
			{
				return _Text;
			}
		}
	}
}

