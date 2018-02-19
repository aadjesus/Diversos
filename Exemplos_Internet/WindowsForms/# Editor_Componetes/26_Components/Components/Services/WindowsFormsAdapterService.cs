using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;
using System.Windows.Forms;

using Mvc.Components.Controller;
using Mvc.Components.Model;

namespace Mvc.Components.Services
{
	/// <summary>
	/// Adapts requests for Windows Forms-hosted components.
	/// </summary>
	public class WindowsFormsAdapterService : IAdapterService
	{
		Form _container;
		IContainer _components;

		public WindowsFormsAdapterService(object controlsContainer, IContainer componentsContainer)
		{
			_container = (Form) controlsContainer;
			_components = componentsContainer;
		}

		#region Implementation of IAdapterService
		public object FindControl(string controlId)
		{
			return FindControl(controlId, _container);
		}

		private Control FindControl(string controlToFind, Control currentControl)
		{
			if (currentControl.Name == controlToFind)
				return currentControl;
			if (currentControl.HasChildren)
			{
				foreach (Control ctl in currentControl.Controls)
				{
					if (ctl.Name == controlToFind)
						return ctl;
					else if (ctl.HasChildren)
						return FindControl(controlToFind, ctl);
				}
			}
			return null;
		}

		public string GetControlID(object control)
		{
			return ((Control)control).Name;
		}

		public object[] GetControls()
		{
			if (_container.Site.DesignMode)
			{
				IDesignerHost host = (IDesignerHost)
					_container.Site.GetService(typeof(IDesignerHost));
				ArrayList controls = new ArrayList(host.Container.Components.Count);
				foreach (IComponent component in host.Container.Components)
				{
					if (component != host.RootComponent && component is Control)
						controls.Add(component);
				}
				return controls.ToArray();
			}
			else
			{
				return GetControls(_container);
			}
		}

		public object[] GetControls(Control parent)
		{
			ArrayList controls = new ArrayList(parent.Controls.Count);
			controls.AddRange(parent.Controls);
			foreach (Control ctl in controls)
			{
				if (ctl.HasChildren) 
					controls.AddRange(GetControls(ctl));
			}
			return controls.ToArray();
		}

		public System.ComponentModel.ComponentCollection GetComponents()
		{
			return _components.Components;
		}

		public void RefreshModels(BaseController controller)
		{
			//Build a keyed list of models in the controller. We don't check for exceptions...
			Hashtable models = new Hashtable(controller.Components.Count);
			foreach (BaseModel model in controller.Components)
				models.Add(model.ModelName, model);

			//We make extensive use of reflection here. This can be improved.
			foreach (DictionaryEntry entry in controller.ConfiguredViews)
			{
				ViewInfo info = (ViewInfo) entry.Value;
				//Retrieve model object and property.
				object model = models[info.Model];
				PropertyInfo modelprop = model.GetType().GetProperty(info.ModelProperty);
				//Locate control and property. Is there any more efficient method?
				Control ctl = (Control) FindControl(info.ControlID);
				if (ctl == null)
				{
					throw new ArgumentException("The control '" + info.ControlID + "' wasn't found in the current container.");
				}
				else
				{
					PropertyInfo ctlprop = ctl.GetType().GetProperty(info.ControlProperty);
					if (ctlprop == null)
					{
						throw new ArgumentException("The property '" + info.ControlProperty + "' wasn't found in the control '" + info.ControlID + "'.");
					}					
					else
					{
						//Set model property.
						TypeConverter converter = TypeDescriptor.GetConverter(modelprop.PropertyType);
						if (!converter.CanConvertFrom(ctlprop.PropertyType))
							throw new ArgumentException("Control property '" + info.ControlProperty + "' has type '" + ctlprop.PropertyType.Name + "' that is incompatible with model property '" + modelprop.Name + "' which is of type '" + modelprop.PropertyType.Name + "'.");
						object value = ctlprop.GetValue(ctl, new object[0]);
						if (!converter.IsValid(value))
							throw new ArgumentException("Control property '" + info.ControlProperty + "' has value '" + value + "' that is invalid for model property '" + modelprop.Name + "'.");
						if (!(value is string && (string)value == String.Empty))
						{
							modelprop.SetValue(model,
								converter.ConvertFrom(value),
								new object[0]);
						}
					}
				}
			}		
		}

		public void RefreshView(BaseController controller)
		{
			//Build a keyed list of models in the controller. We don't check for exceptions...
			Hashtable models = new Hashtable(controller.Components.Count);
			foreach (BaseModel model in controller.Components)
				models.Add(model.ModelName, model);

			//We make extensive use of reflection here. This can be improved.
			foreach (DictionaryEntry entry in controller.ConfiguredViews)
			{
				ViewInfo info = (ViewInfo) entry.Value;
				//Retrieve model object and property.
				object model = models[info.Model];
				PropertyInfo modelprop = model.GetType().GetProperty(info.ModelProperty);
				//Locate control and property.
				Control ctl = (Control) FindControl(info.ControlID);
				if (ctl == null)
				{
					throw new ArgumentException("The control '" + info.ControlID + "' wasn't found in the current container.");
				}
				else
				{
					PropertyInfo ctlprop = ctl.GetType().GetProperty(info.ControlProperty);
					if (ctlprop == null)
					{
						throw new ArgumentException("The property '" + info.ControlProperty + "' wasn't found in the control '" + info.ControlID + "'.");
					}
					//Set control property.
					TypeConverter converter = TypeDescriptor.GetConverter(modelprop.PropertyType);
					if (!converter.CanConvertTo(ctlprop.PropertyType))
						throw new ArgumentException("Model property '" + modelprop.Name + "' has type '" + modelprop.PropertyType.Name + "' that can not be converted to control property '" + info.ControlProperty + "' which is of type '" + ctlprop.PropertyType.Name + "'.");
					object value = modelprop.GetValue(model, new object[0]);
					ctlprop.SetValue(ctl,
						converter.ConvertTo(value, ctlprop.PropertyType),
						new object[0]);
				}
			}		
		}
		#endregion

	}
}
