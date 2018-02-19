using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms.Design;
using System.Windows.Forms;

using Mvc.Components.Controller;

namespace Mvc.Components.Design
{
	/// <summary>
	/// Provides the visual editor for the mappings contained in an <see cref="BaseController"/> component.
	/// </summary>
	/// <remarks>
	/// In order to provide visual forms, this editor uses the <see cref="IWindowsFormsEditorService"/> 
	/// that is requested to the service provider (the IDE). With a reference to that service, 
	/// we can call <see cref="IWindowsFormsEditorService.ShowDialog"/> to display the form.
	/// </remarks>
	internal class ViewMappingsEditor : UITypeEditor
	{
		/// <summary>
		/// Specifies the type of editor we are.
		/// </summary>
		/// <returns>Returns <see cref="UITypeEditorEditStyle.Modal"/> as this is a modal form editor.</returns>
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		/// <summary>
		/// Starts property edition.
		/// </summary>
		/// <returns>The new value.</returns>
		public override object EditValue(ITypeDescriptorContext context, 
			IServiceProvider provider, object value)
		{
			//Always return a valid value.
			object retvalue = value;

			try
			{			
				IWindowsFormsEditorService srv = null;
				IDesignerHost host = (IDesignerHost) context.GetService(typeof(IDesignerHost));

				//Get the forms editor service from the provider, to display the form.
				if (provider != null)
					srv = (IWindowsFormsEditorService)
						context.GetService(typeof(IWindowsFormsEditorService));

				if (srv != null && host != null)
				{
					BaseController controller = (BaseController) context.Instance;

					//Get the designer so we can pass it to the form.
					IDesigner designer = host.GetDesigner(controller);
					ViewMappingsEditorForm form = new ViewMappingsEditorForm(designer);
					//Add configured items to the form.
					foreach (DictionaryEntry entry in controller.ConfiguredViews)
					{
						form.lstMappings.Items.Add(entry.Value);
					}

					//Show form.
					if (srv.ShowDialog(form) == DialogResult.OK)
					{
						if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
						{
							Hashtable mappings = new Hashtable(form.lstMappings.Items.Count);
							foreach (ViewInfo info in form.lstMappings.Items)
								mappings.Add(info.ControlID, info);

							//Set the value through the descriptor as usual.
							context.PropertyDescriptor.SetValue(context.Instance, mappings);
						}
					}
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(
					"An exception occurred during edition: " + ex.ToString());
			}
			
			return retvalue;
		}
	}
}
