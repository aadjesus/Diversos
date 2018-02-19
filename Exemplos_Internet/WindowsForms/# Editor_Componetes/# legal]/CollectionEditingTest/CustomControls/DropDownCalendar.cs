using System;
using System.Windows.Forms;
using System.ComponentModel;
using CustomControls.BaseClasses;
using System.Globalization;


namespace CustomControls.Win32Controls
{

	public class DropDownCalendar:DropDownListBase
	{
		private MonthCalendar _Calendar;
		private  string _CustomFormat="d";
		public event EventHandler DropDown;
		public event EventHandler DateChanged;
		private bool _SkipTab=false;
	

		[Browsable(false)]	
		[DefaultValue(typeof(bool),"False")]	
		public bool SkipTab
		{
			get{return _SkipTab;}
			set{_SkipTab= value;}
		}

		protected override System.Windows.Forms.Control HostedControl
		{
			get
			{
				return Calendar;
			}
		}

	
		public DateTime Value
		{
			get{return Calendar.SelectionStart;}
			set
			{
				if(value>=MinDate && value<=MaxDate)
				{
					Calendar.SelectionStart=value; 
					this.Text=value.ToString(CustomFormat);
					editControl.SelectAll();
					OnDateChanged(new EventArgs());
				}
				else
				{
					if(value<MinDate){Value=MinDate;}
					else{Value=MaxDate;}
				}
			}
		}

		public DateTime MaxDate
		{
			get{return Calendar.MaxDate;}
			set{Calendar.MaxDate=value;}
		}

		public DateTime MinDate
		{
			get{return Calendar.MinDate;}
			set{Calendar.MinDate=value;}
		}

		[Category("Behavior")]
		[DefaultValue(typeof(String),"d")]
		public string CustomFormat
		{
			get{return _CustomFormat;}
			set{_CustomFormat= value;}
		}

		[Browsable(false)]
		public virtual MonthCalendar Calendar
		{
			get
			{
				if(_Calendar==null)
				{
					_Calendar=new MonthCalendar();
					_Calendar.MouseUp+=new MouseEventHandler(Calendar_MouseUp);
				}
				return _Calendar;
			}
		}


		public DropDownCalendar()
		{
			Calendar.MaxSelectionCount=1;
		}

		protected override void OnDropDown(System.EventArgs e)
		{
			
			dropDownForm.Size= new System.Drawing.Size(196,159);

			if(DropDown!=null)
			{
				DropDown(this,e);
			}
		
		}

		protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg,System.Windows.Forms.Keys keyData)
		{
			if ( SkipTab && keyData== Keys.Tab){return true;}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private DateTime GetDefaultDate()
		{
			if(DateTime.Today>Calendar.MaxDate || DateTime.Today<Calendar.MinDate)
			{
				return Calendar.MinDate;
			}
			return DateTime.Today;
		}

		private void Calendar_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			System.Windows.Forms.MonthCalendar.HitTestInfo hitTest;
			hitTest=Calendar.HitTest(e.X,e.Y);
			if (hitTest.HitArea==MonthCalendar.HitArea.Date && hitTest.Time==Calendar.SelectionStart)
			{
				DroppedDown=false;
				Value=Calendar.SelectionStart;
			}
		}

		

		protected override void OnDropDownChanged(System.EventArgs e)
		{
			
			if (DroppedDown)
			{
				
				editControl.ReadOnly=true;
				editControl.BackColor=System.Drawing.Color.White;
				
			}
			else
			{
				
				editControl.ReadOnly=false;
			}
			base.OnDropDownChanged(e);
		}

		protected override void OnItemSelected(bool Dropped)
		{
			if(Dropped)
			{
				if(Calendar.SelectionStart!=DateTime.MinValue)
				{
					Value=Calendar.SelectionStart;
				}
				else
				{
					Calendar.SelectionStart=DateTime.Today;
					Value=DateTime.Today;
				}
			}
			else
			{
				SetDate();
			}
		}

		private void SetDate()
		{

			if( ValidateInputText()){Value=Convert.ToDateTime(this.Text);}
			else if(this.Text==string.Empty)
			{
				this.Value=GetDefaultDate();
			}
			else 
			{
				this.Text=Value.ToString(CustomFormat);
				this.editControl.SelectAll();
			}
		}


		private bool ValidateInputText()
		{
			try
			{
				DateTime date= Convert.ToDateTime(this.Text);
				return true;
			}
			catch
			{
				
				return false;
			}
		}

		
		protected override void OnNextItem()
		{
			if(Calendar.SelectionStart>Calendar.MinDate)
			{
				Calendar.SelectionStart=Calendar.SelectionStart.AddDays(-1);
				Value=Calendar.SelectionStart;
			}
		}

		protected override void OnPrevItem()
		{
			if(Calendar.SelectionStart<Calendar.MaxDate)
			{
				Calendar.SelectionStart=Calendar.SelectionStart.AddDays(1);
				Value=Calendar.SelectionStart;
			}
		}

		protected virtual void OnDateChanged(EventArgs e)
		{
			if(DateChanged!=null)
			{
				DateChanged(this,e);
			}
		}

	}
}
