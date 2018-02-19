using System;
using System.CodeDom;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Windows.Forms;

using Mvc.Components.Controller;
using Mvc.Components.Model;

namespace Mvc.Components.Design
{
	/// <summary>
	/// Provides the custom serialization of <see cref="BaseController"/> inherited 
	/// components at design-time.
	/// </summary>
	/// <remarks>
	/// This class performs the actual code generation for the code-behind page, specifically for  
	/// the InitializeComponent method. It makes extensive use of CodeDom classes.
	/// It is associated with the <see cref="BaseController"/> though the <see cref="DesignerSerializerAttribute"/> attribute.
	/// </remarks>
	internal class ControllerCodeDomSerializer : BaseCodeDomSerializer
	{
		/// <summary>
		/// Serializes the <see cref="BaseController"/> and all its relevant values 
		/// to the code-behind file.
		/// </summary>
		/// <returns>A CodeDom object graph containing the code expressions generated.</returns>
		public override object Serialize(IDesignerSerializationManager manager, object value)
		{
			CodeDomSerializer serial = GetBaseComponentSerializer(manager);
			//Sometimes the UndoManager gets crazy and can't retrieve a serializer for the component.
			if (serial == null) 
				return null;
			CodeStatementCollection statements = (CodeStatementCollection) serial.Serialize(manager, value);

			//Add a sample serializer for web controls.
			if (!(manager.GetSerializer(typeof(System.Web.UI.Control), typeof(CodeDomSerializer)) is WebControlSerializer))
			{
				//EXPLAIN: we can emit code for controls other than our own.
				manager.AddSerializationProvider(new WebControlSerializationProvider());
			}

			//Only serialize the rest if we are not inside the base controller designer.
			//EXPLAIN: this serializer is called if we are the root also.
			IDesignerHost host = (IDesignerHost) manager.GetService(typeof(IDesignerHost));
			if (host.RootComponent == value)
				return statements;

			statements.AddRange(GetCommentHeader("Controller code"));
			BaseController cn = (BaseController) value;
			CodeExpression cnref = SerializeToReferenceExpression(manager, value);

			#region ConfiguredViews property serialization
	
			CodePropertyReferenceExpression propref = new CodePropertyReferenceExpression(cnref, "ConfiguredViews");
			foreach (DictionaryEntry cv in cn.ConfiguredViews)
			{
				ViewInfo info = (ViewInfo) cv.Value;
				if (info.ControlID != String.Empty && info.ControlProperty != null &&
					info.Model != String.Empty && info.ModelProperty != String.Empty)
				{	
					//EXPLAIN: we can emit errors that are shown in the task list.
					//Report errors if necessary
					object ctl = manager.GetInstance(info.ControlID);
					if (ctl == null)
					{
						manager.ReportError(String.Format("Control '{0}' associated with the view mapping in controller '{1}' doesn't exist in the page.", info.ControlID, manager.GetName(value)));
						continue;
					}
					if (ctl.GetType().GetProperty(info.ControlProperty) == null)
					{
						manager.ReportError(String.Format("Control property '{0}' in control '{1}' associated with the view mapping in controller '{2}' doesn't exist.", info.ControlProperty, info.ControlID, manager.GetName(value)));
						continue;
					}					

					//Generates:
					//controller.ConfiguredViews.Add(key, new ViewInfo(controlProperty, model, modelProperty));
					statements.Add(
						new CodeMethodInvokeExpression(
						propref, "Add", 
						new CodeExpression[] { 
												 new CodePrimitiveExpression(cv.Key), 
												 new CodeObjectCreateExpression(
												 typeof(ViewInfo),
												 new CodeExpression[] { 
																		  new CodePrimitiveExpression(info.ControlID),
																		  new CodePrimitiveExpression(info.ControlProperty), 
																		  new CodePrimitiveExpression(info.Model), 
																		  new CodePrimitiveExpression(info.ModelProperty) }
												 ) }
						));
				}
			}
			
			#endregion

			statements.Add(
				new CodeCommentStatement("Connect the controller with the hosting environment."));
				
			//Emit code for the appropriate adapter.
			//new WebFormsAdapter.Connect(controller, this);
			if (host.RootComponent as System.Windows.Forms.Form != null)
			{
				CodeObjectCreateExpression adapter = new CodeObjectCreateExpression(typeof(Adapter.WindowsFormsAdapter), new CodeExpression[0]);
				CodeExpression connect = new CodeMethodInvokeExpression(adapter, "Connect", 
					new CodeExpression[] {
											 cnref, 
											 new CodeThisReferenceExpression(), 
											 new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "components") });
				statements.Add(connect);
			}
			else if (host.RootComponent as System.Web.UI.Page != null)
			{
				CodeObjectCreateExpression adapter = new CodeObjectCreateExpression(typeof(Adapter.WebFormsAdapter), new CodeExpression[0]);
				CodeExpression connect = new CodeMethodInvokeExpression(adapter, "Connect", 
					new CodeExpression[] {
											 cnref, 
											 new CodeThisReferenceExpression(),
											 new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "components") });
				statements.Add(connect);
			}

			//If we had to attach to an event...
			//statements.Add(AttachToEvent(manager, "Init", typeof(EventHandler), value, "OnInit"));

			return statements;
		}
	}
}