using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

using Mvc.Components.Design;

namespace Mvc.Components.Model
{
	/// <summary>
	/// Base class for models.
	/// </summary>
	[ToolboxItem(false)]
	[ToolboxItemFilter("Mvc.Components.Controller", ToolboxItemFilterType.Require)]
	//DONE: the special serializer to properly attach this model to the controller container.
	[DesignerSerializer(typeof(ModelCodeDomSerializer), typeof(CodeDomSerializer))]
	public class BaseModel : Component
	{
		public override string ToString()
		{
			return _name;
		}
		
		//DONE: we use BindableAttribute to show filtered properties in the mappings editor.
		//EXPLAIN: RecommendedAsConfigurable(false) takes the entry out of the DynamicProperties root but still available through the dialog.
		/// <summary>
		/// A friendly name for the model.
		/// </summary>
		[Bindable(false)]
		[Description("Gets/Sets a friendly name to expose the model to view consumers.")]
		[RecommendedAsConfigurable(false)]
		public virtual string ModelName
		{
			get { return _name; } 
			set { _name = value; }
		} string _name = "BaseModel";
	}
}
