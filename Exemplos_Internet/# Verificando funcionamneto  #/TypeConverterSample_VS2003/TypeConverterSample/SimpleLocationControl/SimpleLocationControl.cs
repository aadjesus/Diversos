using System;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UIControl
{
	[DefaultProperty("GPSLocation")]
	[ParseChildren(true)]
	[ToolboxData("<{0}:LocationControl runat=server></{0}:LocationControl>")]
	public class LocationControl : WebControl
	{
		private GPSLocation _Location = null;

		/// <summary>
		/// the GPS location
		/// </summary>
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue(typeof(GPSLocation), "")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public GPSLocation Location
		{
			get
			{
				// if no instance created yet do so
				if (_Location == null)
					_Location = new GPSLocation();

				return _Location;
			}
		}

		/// <summary>
		/// render the GPS location
		/// </summary>
		/// <param name="output">the HTML output stream</param>
		protected override void Render(HtmlTextWriter output)
		{
			if (_Location != null)
				output.Write(_Location.ToString());
		}
	}
}
