using System;
using System.CodeDom;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace Mvc.Components.Design
{
	/// <summary>
	/// This is the base class for our custom serializers. It provides helper methods and 
	/// also implements required base class abstract members.
	/// </summary>
	internal abstract class BaseCodeDomSerializer : CodeDomSerializer
	{
		/// <summary>
		/// The comment header to signal custom code section.
		/// </summary>
		CodeCommentStatementCollection _headercomment = new CodeCommentStatementCollection(
			new CodeCommentStatement[] { 
										   new CodeCommentStatement(String.Empty), 
										   new CodeCommentStatement("------------- ClariuS Custom Code -------------"), 
										   new CodeCommentStatement(String.Empty)
									   });

		/// <summary>
		/// Constructs a header to mark our custom code sections.
		/// </summary>
		/// <param name="sectionTitle">The title of the section.</param>
		/// <returns>The collection of comments to send to the output.</returns>
		protected CodeStatement[] GetCommentHeader(string sectionTitle)
		{
			CodeCommentStatementCollection comments = new CodeCommentStatementCollection(_headercomment);
            comments.Insert(2, new CodeCommentStatement(sectionTitle));
			CodeCommentStatement[] statements = new CodeCommentStatement[comments.Count];
			comments.CopyTo(statements, 0);
			return statements;
		}

		/// <summary>
		/// Retrieves the serializer originally assigned to the type through its <see cref="DesignerSerializerAttribute"/>.
		/// </summary>
		protected CodeDomSerializer GetConfiguredSerializer(IDesignerSerializationManager manager, object value)
		{
			object[] attrs = value.GetType().GetCustomAttributes(typeof(DesignerSerializerAttribute), true);
			if (attrs.Length == 0) return null;
			DesignerSerializerAttribute serializer = (DesignerSerializerAttribute) attrs[0];
			//EXPLAIN: Note that type loading must be done through the resolution service.
			return (CodeDomSerializer)
				Activator.CreateInstance(((ITypeResolutionService)manager.GetService(typeof(ITypeResolutionService))).GetType(serializer.SerializerTypeName));
		}

		/// <summary>
		/// Retrieves a typed serializer for a component from the base class (Component).
		/// </summary>
		protected CodeDomSerializer GetBaseComponentSerializer(IDesignerSerializationManager manager)
		{
			return (CodeDomSerializer) manager.GetSerializer(typeof(Component), typeof(CodeDomSerializer));
		}

		/// <summary>
		/// Builds an event attach code expression with the received parameters.
		/// </summary>
		/// <param name="manager">A serialization manager interface that is used during the deserialization process.</param>
		/// <param name="eventName">The name of the event to attach to. This must be an event available on the class 
		/// containing the component, such as Init or PreRender if the component is hosted on a <see cref="System.Web.UI.Page"/>.</param>
		/// <param name="delegateType">The type of the event handler.</param>
		/// <param name="connectingComponent">The component that will handle the event, containing the <paramref name="componentMethod" /> specified.</param>
		/// <param name="componentMethod">The method on the component matching the signature specified by the <paramref name="delegateType" /> 
		/// and which will handle the event.</param>
		protected CodeStatement AttachToEvent(IDesignerSerializationManager manager,
			string eventName, Type delegateType, object connectingComponent, string componentMethod)
		{
			return new CodeAttachEventStatement(
				new CodeThisReferenceExpression(), eventName, 
				new CodeDelegateCreateExpression(
				new CodeTypeReference(delegateType), 
				new CodeVariableReferenceExpression(manager.GetName(connectingComponent)),
				componentMethod));
		}

		/// <summary>
		/// Implements abstract base member. Just passes deserialization responsibility to the component serializer.
		/// </summary>
		public override object Deserialize(IDesignerSerializationManager manager, object codeObject)
		{
			return GetBaseComponentSerializer(manager).Deserialize(manager, codeObject);
		}
	}
}