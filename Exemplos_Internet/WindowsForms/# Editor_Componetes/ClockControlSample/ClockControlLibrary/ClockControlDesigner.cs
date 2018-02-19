using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ClockControlLibrary
{

	public class ClockControlDesigner : ControlDesigner
	{
		private bool _showBorder = true;

		public override DesignerActionListCollection ActionLists
		{
			get
			{
				// Create action list collection
				DesignerActionListCollection actionLists = new DesignerActionListCollection();

				// Add custom action list
				actionLists.Add(new ClockControlDesignerActionList(this.Component));

				// Return to the designer action service
				return actionLists;
			}
		}

		protected override void OnPaintAdornments(PaintEventArgs e)
		{

			base.OnPaintAdornments(e);

			// Don’t show border if hidden or does not have an Analog face
			if ((!_showBorder) || (this.ClockControl.Face == ClockFace.Digital)) return;

			// Draw border
			Graphics g = e.Graphics;
			using (Pen pen = new Pen(Color.Gray, 1))
			{
				pen.DashStyle = DashStyle.Dash;
				g.DrawRectangle(pen, 0, 0, this.ClockControl.Width - 1, this.ClockControl.Height - 1);
			}
		}

		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);

			// Create design-time-only property entry and add it to the
			// property browser’s Design category
			properties["ShowBorder"] = TypeDescriptor.CreateProperty(typeof(ClockControlDesigner),
																	  "ShowBorder",
																	  typeof(bool),
																	  null);
		}

		// Provide implementation of ShowBorder to provide 
		// storage for created ShowBorder property
		[CategoryAttribute("Design")]
		[DesignOnlyAttribute(true)]
		[DefaultValueAttribute(true)]
		[DescriptionAttribute("Show/Hide a border at design-time.")]
		public bool ShowBorder
		{
			get { return _showBorder; }
			set
			{
				// Change property value
				PropertyDescriptor property = TypeDescriptor.GetProperties(typeof(ClockControl))["ShowBorder"];
				this.RaiseComponentChanging(property);
				_showBorder = value;
				this.RaiseComponentChanged(property, !_showBorder, _showBorder);

				// Update clock UI
				this.ClockControl.Invalidate();
			}
		}

		private void ShowBorderClicked(object sender, EventArgs e)
		{
			// Toggle property value
			ShowBorder = !ShowBorder;
		}

		// Helper property to acquire a ClockControl reference
		private ClockControl ClockControl
		{
			get { return (ClockControl)this.Component; }
		}
	}
}


