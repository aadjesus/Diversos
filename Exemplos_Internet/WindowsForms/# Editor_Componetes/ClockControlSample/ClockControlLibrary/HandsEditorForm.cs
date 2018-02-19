using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClockControlLibrary
{
	partial class HandsEditorForm : Form
	{
		public HandsEditorForm()
		{
			InitializeComponent();
		}

		private Hand _hourHand;

		public Hand HourHand
		{
			get { return new Hand(this.hhColorPanel.BackColor, (int)this.hhNumericUpDown.Value); }
			set
			{
				_hourHand = value;
				this.hhColorPanel.BackColor = _hourHand.Color;
				this.hhNumericUpDown.Value = _hourHand.Width;
			}
		}
		private Hand _minuteHand;

		public Hand MinuteHand
		{
			get { return new Hand(this.mhColorPanel.BackColor, (int)this.mhNumericUpDown.Value); }
			set
			{
				_minuteHand = value;
				this.mhColorPanel.BackColor = _minuteHand.Color;
				this.mhNumericUpDown.Value = _minuteHand.Width;
			}
		}
		private Hand _secondHand;

		public Hand SecondHand
		{
			get { return new Hand(this.shColorPanel.BackColor, (int)this.shNumericUpDown.Value); }
			set
			{
				_secondHand = value;
				this.shColorPanel.BackColor = _secondHand.Color;
				this.shNumericUpDown.Value = _secondHand.Width;
			}
		}

		private void hhColorButton_Click(object sender, EventArgs e)
		{
			this.colorDialog.Color = this.hhColorPanel.BackColor;
			this.colorDialog.ShowDialog();
			this.hhColorPanel.BackColor = this.colorDialog.Color;
		}

		private void mhColorButton_Click(object sender, EventArgs e)
		{
			this.colorDialog.Color = this.mhColorPanel.BackColor;
			this.colorDialog.ShowDialog();
			this.mhColorPanel.BackColor = this.colorDialog.Color;
		}

		private void shColorButton_Click(object sender, EventArgs e)
		{
			this.colorDialog.Color = this.shColorPanel.BackColor;
			this.colorDialog.ShowDialog();
			this.shColorPanel.BackColor = this.colorDialog.Color;
		}
	}
}