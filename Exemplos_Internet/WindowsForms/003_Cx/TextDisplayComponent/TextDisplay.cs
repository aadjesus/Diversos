using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Cx.Attributes;
using Cx.EventArgs;
using Cx.Interfaces;
using Cx.WinForm;

namespace TextDisplayComponent
{							  
	[CxComponentName("Numeric Display")]
	[CxExplicitEvent("DisplayTextChanged")]
	public partial class TextDisplay : UserControl, ICxVisualComponentClass
	{
		protected enum State
		{
			NewText,
			AddChar,
		}

		protected State state;
		protected bool disposed;

		public TextDisplay()
		{
			InitializeComponent();
			EventHelpers.Transform(this, tbDisplay, "TextChanged", "Text").To("DisplayTextChanged");
			state = State.NewText;
		}

		[CxConsumer]
		public void OnChar(object sender, CxEventArgs<char> args)
		{
			switch (state)
			{
				case State.AddChar:
					tbDisplay.Text = tbDisplay.Text + args.Data;
					break;

				case State.NewText:
					tbDisplay.Text = args.Data.ToString();
					state = State.AddChar;
					break;
			}
		}

		[CxConsumer]
		public void OnText(object sender, CxEventArgs<string> args)
		{
			tbDisplay.Text = args.Data;
			state = State.NewText;
		}

		[CxConsumer]
		public void StateIsNewText(object sender, EventArgs args)
		{
			state = State.NewText;
		}

		public void Register(object form, ICxVisualComponent comp)
		{
			this.RegisterControl((Form)form, comp);
		}

		/// <summary>
		/// We need to remove our event transformations from the helper's dictionary.
		/// </summary>
		/// <param name="disposing"></param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}

			EventHelpers.Remove(this);

			base.Dispose(disposing);
		}
	}
}
