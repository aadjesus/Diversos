#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#endregion

namespace ClockControlLibrary
{

	public class DigitalTimeFormatEditor : System.Drawing.Design.UITypeEditor
	{

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			if (context != null)
			{
				return UITypeEditorEditStyle.Modal;
			}
			return base.GetEditStyle(context);
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{

			if ((context != null) && (provider != null))
			{
				// Access the property browser’s UI display service, IWindowsFormsEditorService
				IWindowsFormsEditorService editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

				if (editorService != null)
				{
					// Create an instance of the UI editor dialog
					DigitalTimeFormatEditorForm modalEditor = new DigitalTimeFormatEditorForm();

					// Pass the UI editor dialog the current property value
					modalEditor.DigitalTimeFormat = (string)value;

					// Display the UI editor dialog
					if (editorService.ShowDialog(modalEditor) == DialogResult.OK)
					{
						// Return the new property value from the UI editor dialog
						return modalEditor.DigitalTimeFormat;
					}
				}
			}
			return base.EditValue(context, provider, value);
		}
	}
}
