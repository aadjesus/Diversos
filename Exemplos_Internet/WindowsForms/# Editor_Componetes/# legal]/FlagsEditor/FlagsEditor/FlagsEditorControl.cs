using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace FlagsEditor
{
	internal class FlagsEditorControl : UserControl
	{
		CheckedListBox lvwItems = null;
		ToolStrip bottomMenu = null;
		FlagsEditor Editor = null;
		ToolStripButton buttonReset = null;
		ToolStripButton cancelButton = null;
		ToolStripButton okeyButton = null;
		IWindowsFormsEditorService _Service = null;
		object _Value;
		long leftOver = 0;
		bool cancelFlag = false;

		public FlagsEditorControl()
		{
			lvwItems = new System.Windows.Forms.CheckedListBox();
			SuspendLayout();
			
			lvwItems.Dock = System.Windows.Forms.DockStyle.Fill;
			lvwItems.FormattingEnabled = true;
			lvwItems.IntegralHeight = false;
			lvwItems.BorderStyle = BorderStyle.None;
			Controls.Add(this.lvwItems);

			bottomMenu = new ToolStrip();
			bottomMenu.Dock = DockStyle.Bottom;
			bottomMenu.GripStyle = ToolStripGripStyle.Hidden;
			Controls.Add(bottomMenu);

			buttonReset = new ToolStripButton();
			buttonReset.Image = Properties.Resources.refresh_16;
			buttonReset.Click += new EventHandler(buttonReset_Click);
			buttonReset.Text = "Reset Value";
			buttonReset.DisplayStyle = ToolStripItemDisplayStyle.Image;
			bottomMenu.Items.Add(buttonReset);

			cancelButton = new ToolStripButton();
			cancelButton.Image = Properties.Resources.cancel_16;
			cancelButton.Click += new EventHandler(cancelButton_Click);
			cancelButton.Text = "Cancel";
			cancelButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			bottomMenu.Items.Add(cancelButton);

			okeyButton = new ToolStripButton();
			okeyButton.Image = Properties.Resources.confirm_16;
			okeyButton.Click += new EventHandler(okeyButton_Click);
			okeyButton.Text = "Okey";
			okeyButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			bottomMenu.Items.Add(okeyButton);
			
			Font = new System.Drawing.Font("Tahoma", 8.25F);
			ResumeLayout(false);
		}

		public FlagsEditorControl(FlagsEditor editor)
			: this()
		{
			Editor = editor;
		}

		void okeyButton_Click(object sender, EventArgs e)
		{
			// okey button pressed so CloseDropDown, this will finish the edit operation
			_Service.CloseDropDown();
		}

		void buttonReset_Click(object sender, EventArgs e)
		{
			// reset button pressed, so reload values
			Begin(_Service, _Value);
		}

		void cancelButton_Click(object sender, EventArgs e)
		{
			// cancel button pressed, close drop down but
			// also set cancelFlag to true
			cancelFlag = true;
			_Service.CloseDropDown();
		}

		// begin edit operation
		public void Begin(IWindowsFormsEditorService service, object value)
		{
			_Service = service;
			lvwItems.Items.Clear();

			Type enumType = value.GetType();
			Array values = Enum.GetValues(enumType);

			// prepare list
			long current = Convert.ToInt64(value);
			for (int i = 0; i < values.Length; i++)
			{
				long val = Convert.ToInt64(values.GetValue(i));
				bool check = false;
				if (val == 0)
					check = (current == 0);
				else
				{
					check = ((current & val) == val);
					if (check)
						current &= ~val;
				}

				lvwItems.Items.Add(new EnumValueContainer(enumType, val), check);
			}
			leftOver = current;
			_Value = value;
		}

		// end edit operation
		public void End()
		{
			cancelFlag = false;
			_Service = null;
			_Value = 0;
			leftOver = 0;
		}

		// value which will be calculated from the checked items list
		public object Value
		{
			get
			{
				// if cancel flag set, return original value
				if (cancelFlag)
					return _Value;

				long value = 0;
				for (int i = 0; i < lvwItems.CheckedItems.Count; i++)
				{
					EnumValueContainer container = lvwItems.CheckedItems[i] as EnumValueContainer;
					value |= Convert.ToInt64(container.Value);
				}

				return value | leftOver;
			}
		}

		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (((keyData & Keys.KeyCode) == Keys.Return) 
				&& ((keyData & (Keys.Alt | Keys.Control)) == Keys.None))
			{
				_Service.CloseDropDown();
				return true;
			}
			if (((keyData & Keys.KeyCode) == Keys.Escape) 
				&& ((keyData & (Keys.Alt | Keys.Control)) == Keys.None))
			{
				cancelFlag = true;
				_Service.CloseDropDown();
				return true;
			}

			return base.ProcessDialogKey(keyData);
		}

		// container class for the enum values
		private class EnumValueContainer
		{
			Type _Type;
			object _Value = 0;

			public EnumValueContainer(Type type, object value)
			{
				_Type = type;
				_Value = value;
			}

			// return the Name of the Enum member according to its value
			public override string ToString()
			{
				return Enum.GetName(_Type, _Value);
			}

			public object Value
			{
				get { return _Value; }
			}
		}
	}
}
