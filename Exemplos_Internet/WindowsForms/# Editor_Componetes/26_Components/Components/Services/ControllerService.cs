using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IO;
using System.Text;

using Mvc.Components.Controller;
using Mvc.Components.Model;

namespace Mvc.Components.Services
{
	/// <summary>
	/// Provides design-time services related to controller mappings.
	/// </summary>
	internal class ControllerService : IControllerService
	{
		IDesignerHost _host;

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerService"/>.
		/// </summary>
		public ControllerService(IDesignerHost host)
		{
			_host = host;
            
			IMenuCommandService mcs = (IMenuCommandService) host.GetService(typeof(IMenuCommandService));
			if (mcs != null)
			{
				//EXPLAIN: global commands vs individual component Verbs property. Collisions.
				mcs.AddCommand(new DesignerVerb("Verify all controllers mappings ...", new EventHandler(OnVerifyAll)));
			}

		}

		#region Verification methods

		/// <summary>
		/// Verifies that mappings for the controller are correct. 
		/// </summary>
		/// <param name="controller">Controller to check.</param>
		public void VerifyMappings(BaseController controller)
		{
			string result = VerifyOne(controller);
			if (result == String.Empty)
				System.Windows.Forms.MessageBox.Show("Verification succeeded.");
			else
				System.Windows.Forms.MessageBox.Show(result);
		}

		/// <summary>
		/// Verifies all mappings in all controllers in the current host. 
		/// </summary>
		void OnVerifyAll(object sender, EventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			
			foreach (IComponent component in _host.Container.Components)
			{
				if (component is BaseController)
				{
					sb.Append(VerifyOne((BaseController) component));
				}
			}

			string result = sb.ToString();
			if (result == String.Empty)
				System.Windows.Forms.MessageBox.Show("Verification succeeded.");
			else
				System.Windows.Forms.MessageBox.Show(result);
		}

		string VerifyOne(BaseController controller)
		{
			IReferenceService svc = (IReferenceService) _host.GetService(typeof(IReferenceService));
			StringWriter w = new StringWriter();

			Hashtable models = new Hashtable(controller.Components.Count);
			ArrayList names = new ArrayList(controller.Components.Count);

			foreach (IComponent comp in controller.Components)
			{
				if (comp is BaseModel)
				{
					BaseModel model = (BaseModel) comp;
					if (names.Contains(model.ModelName))
					{
						w.WriteLine("The model name '{0}' is duplicated in the controller.", model.ModelName);
					}
					else
					{
						models.Add(model.ModelName, model);
					}
				}
			}

			foreach (DictionaryEntry entry in controller.ConfiguredViews)
			{
				ViewInfo info = (ViewInfo) entry.Value;
				object ctl = svc.GetReference(info.ControlID);
				if (ctl == null)
				{
					w.WriteLine("Control '{0}' associated with the view mapping in controller '{1}' doesn't exist in the page.", info.ControlID, svc.GetName(controller));
				}
				else
				{
					if (ctl.GetType().GetProperty(info.ControlProperty) == null)
						w.WriteLine("Control property '{0}' can't be found in control '{1}' in controller '{2}'.", info.ControlProperty, info.ControlID, svc.GetName(controller));
				}
			}

			return w.ToString();
		}

		#endregion

		#region Listing methods

		/// <summary>
		/// Lists all the properties of the control.
		/// </summary>
		public string[] GetControlProperties(object control)
		{
			//EXPLAIN: why we put this converter-specific code here. To reuse it in the modal form.
			PropertyDescriptorCollection props = TypeDescriptor.GetProperties(
				control, new Attribute[] { new BrowsableAttribute(true) });
			
			string[] names = new string[props.Count];
			for (int i = 0; i < props.Count; i++)
				names[i] = props[i].Name;
			Array.Sort(names);

			return names;
		}

		/// <summary>
		/// Lists all the properties of a model.
		/// </summary>
		public string[] GetModelProperties(BaseModel model)
		{
			PropertyDescriptorCollection props = TypeDescriptor.GetProperties(
				model, new Attribute[] { new BindableAttribute(true) });
			
			string[] names = new string[props.Count];
			for (int i = 0; i < props.Count; i++)
				names[i] = props[i].Name;
			Array.Sort(names);

			return names;
		}

		#endregion
	}
}
