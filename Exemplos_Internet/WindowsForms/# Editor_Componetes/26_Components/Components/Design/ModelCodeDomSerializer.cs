using System;
using System.CodeDom;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

using Mvc.Components.Model;

namespace Mvc.Components.Design
{
	/// <summary>
	/// Emits special constructor for a model so that it constructs itself using the 
	/// containing controller as the container, not the usual private "components" field.
	/// </summary>
	internal class ModelCodeDomSerializer : BaseCodeDomSerializer
	{
		public override object Serialize(IDesignerSerializationManager manager, object value)
		{
			CodeDomSerializer serializer = GetBaseComponentSerializer(manager);
			if (serializer == null) 
				return null;

			CodeStatementCollection statements = (CodeStatementCollection)
				serializer.Serialize(manager, value);

			//DONE: First statement is always the object creation statement, 
			//and it's an assignment to the container variable.
			CodeAssignStatement assign = (CodeAssignStatement) statements[0];
			//The expression at the right is the actual constructor call.
			CodeObjectCreateExpression create = (CodeObjectCreateExpression) assign.Right;
			//We want to change the first paramenter of the ctor to point to the containing component, 
			//(this) which is BTW a container because of the BaseController implementation.
			if (create.Parameters.Count > 0)
			{
				create.Parameters[0] = new CodeThisReferenceExpression();
			}
			else
			{
				create.Parameters.Add(new CodeThisReferenceExpression());
			}

			return statements;
		}
	}
}
