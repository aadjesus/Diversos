using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

using Mvc.Components.Design;
using Mvc.Components.Model;
using Mvc.Components.Services;

namespace Mvc.Components.Controller
{
	/// <summary>
	/// Base class for controllers, and the container for models too.
	/// </summary>
	[ToolboxItem(false)]
	[ProvideProperty("WinViewMapping", typeof(System.Windows.Forms.Control))]
	[ProvideProperty("WebViewMapping", typeof(System.Web.UI.Control))]
	[Designer(typeof(ControllerDesigner))]
	[Designer(typeof(ControllerRootDesigner), typeof(IRootDesigner))]
	[DesignerSerializer(typeof(ControllerCodeDomSerializer), typeof(CodeDomSerializer))]
	public class BaseController : Component, IContainer, IExtenderProvider
	{
		#region Implementation of IContainer
		/// <summary>
		/// Always initialize the container because we will host components from inheriting controllers.
		/// </summary>
		private Container components = new Container();

		public void Remove(IComponent component)
		{
			components.Remove(component);
		}

		public void Add(IComponent component, string name)
		{
			components.Add(component, name);
		}

		public void Add(IComponent component)
		{
			components.Add(component);
		}

		[Browsable(false)]
		public ComponentCollection Components
		{
			get
			{
				return components.Components;
			}
		}

		#endregion

		#region ViewMapping extended property

		/// <summary>
		/// Keeps the list of view mappings related to this controller.
		/// </summary>
		/// This property also takes advantage of the <see cref="BrowsableAttribute"/>, 
		/// <see cref="EditorBrowsableAttribute"/> and <see cref="DesignerSerializationVisibilityAttribute"/> 
		/// attributes to become invisible to the property browser, Intellisense and the 
		/// VS .NET serialization mechanism.
		/// </para>
		/// </remarks>
		[Browsable(true)]
		[Category("MVC")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Editor(typeof(ViewMappingsEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Hashtable ConfiguredViews
		{
			get { return _views; }
			set { _views = value; }
		} Hashtable _views = new Hashtable();

		#region Win version
		
		/// <summary>
		/// Gets/Sets the view mapping that is used with the control.
		/// </summary>
		[Category("MVC")]
		[Description("Gets/Sets the view mapping that is used with the control.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MergableProperty(true)]
		public ViewInfo GetWinViewMapping(object target)
		{
			return GetViewMapping(target);
		}

		/// <summary>
		/// Sets the mapping that applies to this control.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SetWinViewMapping(object target, ViewInfo value)
		{
			SetViewMapping(target, value);
		}

		#endregion

		#region Web version
		/// <summary>
		/// Gets/Sets the view mapping that is used with the control.
		/// </summary>
		[Category("MVC")]
		[Description("Gets/Sets the view mapping that is used with the control.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MergableProperty(true)]
		public ViewInfo GetWebViewMapping(object target)
		{
			return GetViewMapping(target);
		}

		/// <summary>
		/// Sets the mapping that applies to this control.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SetWebViewMapping(object target, ViewInfo value)
		{
			SetViewMapping(target, value);
		}

		#endregion
		
		#region Common implementation

		/// <summary>
		/// Gets/Sets the view mapping that is used with the control.
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ViewInfo GetViewMapping(object target)
		{
			//EXPLAIN: we rely on the adapter to resolve "platform" inconsistencies.
			IAdapterService svc = (IAdapterService) GetService(typeof(IAdapterService));
			string id = svc.GetControlID(target);
			ViewInfo info = _views[id] as ViewInfo;
			if (info == null)
			{
				info = new ViewInfo(this, id);
				_views[id] = info;
			}

			info.Controller = this;

		//Verify that we are trapping the changes. 
		if (!info.IsHooked)
		{
			PropertyDescriptorCollection props = TypeDescriptor.GetProperties(info);
			props["ControlProperty"].AddValueChanged(info, new EventHandler(RaiseViewInfoChanged));
			props["Model"].AddValueChanged(info, new EventHandler(RaiseViewInfoChanged));
			props["ModelProperty"].AddValueChanged(info, new EventHandler(RaiseViewInfoChanged));
			info.IsHooked = true;
		}

			return info;
		}

		/// <summary>
		/// Sets the mapping that applies to this control.
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SetViewMapping(object target, ViewInfo value)
		{
			IAdapterService svc = (IAdapterService) GetService(typeof(IAdapterService));
			_views[svc.GetControlID(target)] = value;
		}

		#endregion

		#region IExtenderProvider Implementation
		/// <summary>
		/// Determine controls that can be mapped to the model.
		/// </summary>
		/// <param name="extendee">The object being evaluated for extensibility.</param>
		/// <returns>The support for the object.</returns>
		bool IExtenderProvider.CanExtend(object extendee)
		{
			//EXPLAIN: As both Form and Page extend from their base Control class, we must check 
			//that the current extendee is not the root component.
			IDesignerHost host = (IDesignerHost) GetService(typeof(IDesignerHost));
			//Never allow mappings at the root component level (Page or Form).
			if (extendee == host.RootComponent) 
			{
				return false;
			}
			else if (extendee is System.Windows.Forms.Control || extendee is System.Web.UI.Control)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion
		#endregion

		#region Internal members

		/// <summary>
		/// Called when changes occur in child <see cref="ViewInfo"/> objects, 
		/// forces code generation to reflect new values.
		/// </summary>
		internal void RaiseViewInfoChanged(object sender, EventArgs e)
		{
			IComponentChangeService svc = (IComponentChangeService)
				GetService(typeof(IComponentChangeService));
			if (svc != null)
			{
				svc.OnComponentChanged(this, 
					TypeDescriptor.GetProperties(this)["ConfiguredViews"], null, this.ConfiguredViews);
			}
		}

		/// <summary>
		/// Locates a model by its name in the controller.
		/// </summary>
		internal BaseModel FindModel(string modelName)
		{
			foreach (IComponent comp in Components)
			{
				if (comp is BaseModel && ((BaseModel)comp).ModelName == modelName)
					return (BaseModel) comp;
			}
			return null;
		}

		#endregion

		#region Lifecycle members

		internal event EventHandler ModelChanged;

		/// <summary>
		/// Allows inheritors to initialize the context as necessary.
		/// </summary>
		protected virtual void InitContext(object sender, EventArgs e)
		{
		}

		internal void Init(object sender, EventArgs e)
		{
			InitContext(sender, e);
		}

		internal void RefreshModels(object sender, EventArgs e)
		{
			if (this.Site != null)
			{
				IAdapterService svc = (IAdapterService) this.Site.GetService(typeof(IAdapterService));
				svc.RefreshModels(this);
			}
		}

		internal void RefreshView(object sender, EventArgs e)
		{
			if (this.Site != null)
			{
				IAdapterService svc = (IAdapterService) this.Site.GetService(typeof(IAdapterService));
				svc.RefreshView(this);
			}
		}

		protected void RaiseModelChanged(BaseModel model)
		{
			if (ModelChanged != null)
				ModelChanged(model, EventArgs.Empty);
		}

		#endregion
	}
}
