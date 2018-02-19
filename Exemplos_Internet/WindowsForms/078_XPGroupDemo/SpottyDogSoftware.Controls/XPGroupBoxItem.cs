using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace SpottyDogSoftware.Controls
{	
	/// <summary>
	/// Delegate invoked when an enable state changed 
	/// </summary>
	public delegate void ItemEnableChangedEventHandler(object sender, EventArgs e);

	/// <summary>
	/// A class that encapsulates the data for a XPGroupBox item
	/// </summary>
	[TypeConverterAttribute(typeof(XPGroupBoxItemConvertor))]
	public class XPGroupBoxItem
	{
		static int count = 0;

		private Icon _icon;
		/// <summary>
		/// The item Icon
		/// </summary>
		public Icon Icon
		{
			get { return _icon; }
			set { _icon = value; }
		}

		private string _name="";
		/// <summary>
		/// The name of the item
		/// </summary>
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		private string _text;
		/// <summary>
		/// The item link button text
		/// </summary>
		public string Text
		{
			get { return _text; }
			set { _text = value; }
		}

		private System.Drawing.Region _region;
		/// <summary>
		/// The region for the XPGroupBoxItem
		/// </summary>
		public System.Drawing.Region Region
		{
			get { return _region; }
			set { _region = value; }
		}

		private bool _enabled = true;
		/// <summary>
		/// Is the XPGroupBoxItem enabled
		/// </summary>
		public bool Enabled
		{
			get { return _enabled; }
			set { 
				_enabled = value; 
				if (EnableChanged != null)
				{
					EnableChanged(this, new EventArgs());
				}
			}
		}

		/// <summary>
		/// Event raised when the XPGroupBoxItem EnableChanged 
		/// </summary>
		public event ItemEnableChangedEventHandler EnableChanged;


		/// <summary>
		/// Default constructor
		/// </summary>
		public XPGroupBoxItem()
		{
			if ( _name == null || _name == "" ||_name == string.Empty)
				_name = string.Format("item{0}", count.ToString());
			count++;
		}

		/// <summary>
		/// XPGroupBoxItem constructor that instantiates the member values
		/// </summary>
		/// <param name="icon"></param>
		/// <param name="name"></param>
		/// <param name="text"></param>
		public XPGroupBoxItem(Icon icon, string name, string text) : this()
		{
			_icon = icon;
			_name = name;
			_text = text;
		}

		/// <summary>
		/// XPGroupBoxItem constructor that instantiates the member values
		/// </summary>
		/// <param name="icon"></param>
		/// <param name="name"></param>
		/// <param name="text"></param>
		/// <param name="enabled"></param>
		public XPGroupBoxItem(Icon icon, string name, string text, bool enabled) : this(icon, name, text)
		{
			_enabled = enabled;
		}
	}
}
