using System;
using System.CodeDom;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Web.UI;

using Mvc.Components.Controller;

namespace Mvc.Components.Design
{
	/// <summary>
	/// Emits custom code-behind code for mapped controls in a web form.
	/// </summary>
	internal class WebControlSerializationProvider : IDesignerSerializationProvider
	{
		/// <summary>
		/// Returns our custom serializer only if the type received implements <see cref="IAttributeAccessor"/>, 
		/// the only common ground (for attribute settings) for HtmlControls and WebControls.
		/// </summary>
		public object GetSerializer(IDesignerSerializationManager manager, object currentSerializer, Type objectType, Type serializerType)
		{
			if (typeof(IAttributeAccessor).IsAssignableFrom(objectType) && serializerType == typeof(CodeDomSerializer))
				return new WebControlSerializer();

			return null;
		}
	}

	/// <summary>
	/// Emits all Mvc mappings as attributes of the emitted HTML at run-time.
	/// </summary>
	internal class WebControlSerializer : BaseCodeDomSerializer
	{
		public override object Serialize(IDesignerSerializationManager manager, object value)
		{
			CodeDomSerializer serial = GetConfiguredSerializer(manager, value);
			if (serial == null) 
				return null;

			CodeStatementCollection statements = (CodeStatementCollection) serial.Serialize(manager, value);

			PropertyDescriptor prop = TypeDescriptor.GetProperties(value)["WebViewMapping"];
			ViewInfo info = (ViewInfo) prop.GetValue(value);

			//EXPLAIN: Note that this property is extended, and doesn't actually exist in the control.
			//We can access the controller (extender) providing the property, using the
			//Provider property retrieved by reflection in DesignUtils:
			BaseController controller = (BaseController)
				DesignUtils.ProviderProperty.GetValue(prop, new object[0]);

			//Attach the view mappings to the control attributes.
			if (info.ControlProperty != String.Empty && 
				info.Model != String.Empty && info.ModelProperty != String.Empty)
			{
				CodeExpression ctlref = SerializeToReferenceExpression(manager, value);
				CodeCastExpression cast = new CodeCastExpression(typeof(IAttributeAccessor), ctlref);
				//Emits:
				//((IAttributeAccessor)control).SetAttribute("MVC_" + key, value);
				statements.Add(new CodeMethodInvokeExpression(
					cast, "SetAttribute", new CodeExpression[] {
																   new CodePrimitiveExpression("MVC_Controller"), 
																   new CodePrimitiveExpression(manager.GetName(controller)) 
															   }));
				statements.Add(new CodeMethodInvokeExpression(
					cast, "SetAttribute", new CodeExpression[] {
																   new CodePrimitiveExpression("MVC_Model"), 
																   new CodePrimitiveExpression(info.Model) 
															   }));
				statements.Add(new CodeMethodInvokeExpression(
					cast, "SetAttribute", new CodeExpression[] {
																   new CodePrimitiveExpression("MVC_ModelProperty"), 
																   new CodePrimitiveExpression(info.ModelProperty) 
															   }));
				statements.Add(new CodeMethodInvokeExpression(
					cast, "SetAttribute", new CodeExpression[] {
																   new CodePrimitiveExpression("MVC_ControlProperty"), 
																   new CodePrimitiveExpression(info.ControlProperty) 
															   }));
			}
			return statements;
		}
	}
}
