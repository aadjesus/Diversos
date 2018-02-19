using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClockControlLibrary
{
	partial class DigitalTimeFormatEditorForm : Form
	{
		private DateTime _exampleDateTime1 = new DateTime(2002, 12, 31, 12, 59, 59);
		private DateTime _exampleDateTime2 = new DateTime(2002, 1, 1, 1, 1, 1);
		private string _digitalTimeFormat = "dd/MM/yyyy hh:mm:ss tt";

		public DigitalTimeFormatEditorForm()
		{
			InitializeComponent();
		}

		public string DigitalTimeFormat
		{
			get { return _digitalTimeFormat; }
			set { _digitalTimeFormat = value; }
		}

		private void DigitalTimeFormatEditorForm_Load(object sender, EventArgs e)
		{

			// Set the textbox from the property
			formatText.Text = _digitalTimeFormat;

			// Build the examples based on the format
			example1Label.Text = _exampleDateTime1.ToString(formatText.Text);
			example2Label.Text = _exampleDateTime2.ToString(formatText.Text);

			// Build the list
			DataTable lst = new DataTable("List");
			lst.Columns.Add(new DataColumn("Value", typeof(string)));
			lst.Columns.Add(new DataColumn("Display", typeof(string)));
			lst.Rows.Add(new Object[] { "/", "/  (Date Separator)" });
			lst.Rows.Add(new Object[] { ":", ":  (Time Separator)" });
			lst.Rows.Add(new Object[] { "d", "d  (Day: 1-31)" });
			lst.Rows.Add(new Object[] { "dd", "dd  (Day: 01-31)" });
			lst.Rows.Add(new Object[] { "ddd", "ddd  (Day Mon-Sun)" });
			lst.Rows.Add(new Object[] { "H", "H  (Hour: 0-23)" });
			lst.Rows.Add(new Object[] { "h", "h  (Hour: 1-12)" });
			lst.Rows.Add(new Object[] { "HH", "HH  (Hour: 00-23)" });
			lst.Rows.Add(new Object[] { "hh", "hh  (Hour: 01-12)" });
			lst.Rows.Add(new Object[] { "M", "M   (Month: 1-12)" });
			lst.Rows.Add(new Object[] { "m", "m  (Minute: 0-59)" });
			lst.Rows.Add(new Object[] { "mm", "mm  (Minute: 00-59)" });
			lst.Rows.Add(new Object[] { "MM", "MM  (Month: 01-12)" });
			lst.Rows.Add(new Object[] { "MMM", "MMM  (Month: Jan-Dec)" });
			lst.Rows.Add(new Object[] { "s", "s  (Second: 0-59)" });
			lst.Rows.Add(new Object[] { "ss", "ss  (Second: 00-59)" });
			lst.Rows.Add(new Object[] { "tt", "tt  (AM/PM)" });
			lst.Rows.Add(new Object[] { "y", "y  (Year: 1-99)" });
			lst.Rows.Add(new Object[] { "yy", "yy  (Year: 01-99)" });
			lst.Rows.Add(new Object[] { "yyyy", "yyyy  (Year: 2001-2099)" });
			lstFormatSpecifiers.DataSource = lst;
			lstFormatSpecifiers.ValueMember = "Value";
			lstFormatSpecifiers.DisplayMember = "Display";
		}

		private void okButton_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
			_digitalTimeFormat = formatText.Text;
			this.Close();
		}

		private void addFormatSpecifierButton_Click(object sender, System.EventArgs e)
		{
			// Add selected format specifier
			if (lstFormatSpecifiers.Text != "")
			{
				formatText.Text += lstFormatSpecifiers.SelectedValue;
				formatText.Select(formatText.Text.Length, 0);
				formatText.Focus();
			}
		}

		private void formatText_TextChanged(object sender, System.EventArgs e)
		{
			example1Label.Text = _exampleDateTime1.ToString(formatText.Text);
			example2Label.Text = _exampleDateTime2.ToString(formatText.Text);
		}
	}
}