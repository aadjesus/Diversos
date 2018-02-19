#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text;
using System.Windows.Forms.Design;

#endregion

namespace ClockControlLibrary
{
	public class FaceEditor : UITypeEditor
	{
		// If the UI editor control is resizable, override this 
		// property to include a sizing grip on the Property
		// Browser drop down
		public override bool IsDropDownResizable
		{
			get { return true; }
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			if (context != null)
			{
				// Specify a drop-down UITypeEditor
				return UITypeEditorEditStyle.DropDown;
			}
			return base.GetEditStyle(context);
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{

			if ((context != null) && (provider != null))
			{
				// Access the property browser’s UI display service
				IWindowsFormsEditorService editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

				if (editorService != null)
				{
					// Create an instance of the UI editor control
					// passing a reference to the editor service
					FaceEditorControl dropDownEditor = new FaceEditorControl(editorService);

					// Pass the UI editor control the current property value
					dropDownEditor.Face = (ClockFace)value;

					// Display the UI editor control
					editorService.DropDownControl(dropDownEditor);

					// Return the new property value from the UI editor control
					return dropDownEditor.Face;
				}
			}
			return base.EditValue(context, provider, value);
		}
	}
}
