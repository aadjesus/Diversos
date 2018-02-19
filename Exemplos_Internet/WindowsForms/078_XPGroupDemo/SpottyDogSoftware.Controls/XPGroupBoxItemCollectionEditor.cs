using System;
using System.Diagnostics;

namespace SpottyDogSoftware.Controls
{
	/// <summary>
	/// A CollectionEditor for the XPGroupBoxCollection that uses the GetPaintValueSupported
	/// method as a means to detecting the collection has changed and causing the associated
	/// XPGroupBox control to repaint in the designer
	/// </summary>
	public class XPGroupBoxItemCollectionEditor : System.ComponentModel.Design.CollectionEditor
	{
		/// <summary>
		/// Pass through to the base constructor
		/// </summary>
		/// <param name="type"></param>
		public XPGroupBoxItemCollectionEditor(Type type) : base(type)
		{
		}

		/// <summary>
		/// Allows me to hook into any change to a collection and cause a control to repaint in the designer
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
		{
			if (context != null)
			{
				if (context.Instance is XPGroupBox)
				{
					((XPGroupBox)context.Instance).Invalidate();
				}
				else if (context.Instance is XPGroupBoxContainer)
				{
					((XPGroupBoxContainer)context.Instance).RepositionControls();
				}
			}
			return base.GetPaintValueSupported (context);
		}

	}
}
