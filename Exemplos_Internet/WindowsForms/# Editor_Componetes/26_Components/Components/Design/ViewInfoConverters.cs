using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

using Mvc.Components.Controller;
using Mvc.Components.Model;
using Mvc.Components.Services;

namespace Mvc.Components.Design
{
	#region ControlPropertyConverter implementation
	/// <summary>
	/// This converter lists all the properties of the component it's associated with.
	/// </summary>
	internal class ControlPropertyConverter : StringListConverter
	{
		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			ViewInfo info = (ViewInfo) context.Instance;
			
			//DONE: Hack. We have to go through the host to get the reference service because we're inside the property grid here and the service is not available.
			IDesignerHost host = (IDesignerHost) context.GetService(typeof(IDesignerHost));
			IReferenceService svc = (IReferenceService) host.GetService(typeof(IReferenceService));

			if (svc == null)
				return null;

			object ctl = svc.GetReference(info.ControlID);
			if (ctl == null)
			{
				throw new ArgumentException("The control '" + info.ControlID + "' wasn't found in the current container.");
			}
			else
			{
				IControllerService cont = (IControllerService) context.GetService(typeof(IControllerService));
				return new StandardValuesCollection(cont.GetControlProperties(ctl));
			}
		}
	}
	#endregion

	#region ModelNameConverter implementation
	/// <summary>
	/// This converter lists all names of the models in the controller.
	/// </summary>
	internal class ModelNameConverter : StringListConverter
	{
		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			ViewInfo info = (ViewInfo) context.Instance;

			ArrayList names = new ArrayList();
			foreach (Component comp in info.Controller.Components)
				if (comp is BaseModel) names.Add(((BaseModel)comp).ModelName);			
			
			return new StandardValuesCollection(names);
		}
	}
	#endregion

	#region ModelPropertyConverter implementation

	/// <summary>
	/// This converter lists all the properties of 
	/// the model associated with the control.
	/// </summary>
	internal class ModelPropertyConverter : StringListConverter
	{
		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			ViewInfo info = (ViewInfo) context.Instance;
			BaseModel model = info.Controller.FindModel(info.Model);
			if (model == null) return null;

			IControllerService cont = (IControllerService) context.GetService(typeof(IControllerService));
			return new StandardValuesCollection(cont.GetModelProperties(model));
		}
	}

	#endregion
}
