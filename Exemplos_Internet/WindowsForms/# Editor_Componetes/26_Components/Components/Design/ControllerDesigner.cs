using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Design;
using System.IO;
using System.Text;

using Mvc.Components.Controller;
using Mvc.Components.Model;
using Mvc.Components.Services;

namespace Mvc.Components.Design
{
	/// <summary>
	/// Provides a designer for the <see cref="BaseController"/> component.
	/// </summary>
	internal class ControllerDesigner : ComponentDesigner
	{
		/// <summary>
		/// Helper converter property.
		/// </summary>
		BaseController CurrentController
		{
			get { return (BaseController) this.Component; }
		}

		#region Verbs

		/// <summary>
		///	The collection of actions that the designer provides. 
		///	Displayed at the bottom of the property browser and on the popup menu.
		/// </summary>
		public override DesignerVerbCollection Verbs
		{
			get 
			{
				DesignerVerb[] verbs = new DesignerVerb[] { 
															  //EXPLAIN: Placeholder for first global command to avoid collisions.
															  new DesignerVerb(String.Empty, null), 
															  new DesignerVerb("Verify this controller mappings ...", new EventHandler(OnVerifyOne)), 
															  new DesignerVerb("Edit View mappings ...", new EventHandler(OnEditMappings)) 
														  };
				return new DesignerVerbCollection(verbs); }
		}

		/// <summary>
		/// Handles the verb action of editing the mappings.
		/// </summary>
		void OnEditMappings(object sender, EventArgs e)
		{
			//EXPLAIN: why we can't call the editor from here, even if create a custom ITypeDescriptorContext.
			//IWindowsFormsEditorService is provided by the property grid itself.
			/*
			UITypeEditor editor = new ViewMappingsEditor();
			editor.EditValue(
				new DesignerContext(
					this, TypeDescriptor.GetProperties(CurrentController)["ConfiguredViews"]), 
				this.Component.Site, 
				CurrentController.ConfiguredViews);
			*/

			try
			{
				//Inevitably, code is almost duplicated compared with the editor...
				ViewMappingsEditorForm form = new ViewMappingsEditorForm(this);

				//Add configured items to the form.
				foreach (DictionaryEntry entry in CurrentController.ConfiguredViews)
				{
					form.lstMappings.Items.Add(entry.Value);
				}

				if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Hashtable mappings = new Hashtable(form.lstMappings.Items.Count);
					foreach (ViewInfo info in form.lstMappings.Items)
						mappings.Add(info.ControlID, info);

					//Set new value using the property descriptor in order to allow the 
					//IDE to take necessary actions to persist and notify changes to other components.
					PropertyDescriptor prop = TypeDescriptor.GetProperties(Component)["ConfiguredViews"];
					prop.SetValue(Component, mappings);
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(
					"An exception occurred during edition: " + ex.ToString());
			}
		}

		#endregion

		#region Initialization

		/// <summary>
		/// Sets up the design features and hooks to events.
		/// </summary>
		/// <param name="component"></param>
		public override void Initialize(IComponent component)
		{
			//DONE: always call base.Initialize!
			base.Initialize(component);
			IComponentChangeService ccs = (IComponentChangeService)
				GetService(typeof(IComponentChangeService));
			if (ccs != null) 
			{
				ccs.ComponentRemoving += new ComponentEventHandler(OnComponentRemoving);
				ccs.ComponentRename += new ComponentRenameEventHandler(OnComponentRename);
			}

			//Attach the adapter services at design and initialization time. 
			SetupServices();
		}

		/// <summary>
		/// Attaches the adapter services at design and initialization time. 
		/// </summary>
		/// <remarks>
		/// Provides the <see cref="IAdapterService"/> and <see cref="IControllerService"/> to components 
		/// at design-time. Both are added only once to the host.
		/// </remarks>
		void SetupServices()
		{
			//Attach the adapter services at design-time. 
			//EXPLAIN: We must check that it's the appropriate one for the current root component.
			object service = GetService(typeof(IAdapterService));
			IDesignerHost host = (IDesignerHost) GetService(typeof(IDesignerHost));
			if (host.RootComponent as System.Windows.Forms.Form != null)
			{
				if (service == null || service.GetType() != typeof(WindowsFormsAdapterService))
				{
					//EXPLAIN: warn about promoting services. They may not get removed later.
					host.RemoveService(typeof(IAdapterService), false);
					host.AddService(
						typeof(IAdapterService), 
						new WindowsFormsAdapterService(host.RootComponent, host.RootComponent.Site.Container),
						false);
				}
			}
			else if (host.RootComponent as System.Web.UI.Page != null)
			{
				if (service == null || service.GetType() != typeof(WebFormsAdapterService))
				{
					host.RemoveService(typeof(IAdapterService), false);
					host.AddService(
						typeof(IAdapterService), 
						new WebFormsAdapterService(host.RootComponent, host.RootComponent.Site.Container),
						false);
				}
			}

			//Setup the controller service.
			service = GetService(typeof(IControllerService));
			if (service == null)
			{
				host.AddService(typeof(IControllerService), new ControllerService(host), false);
			}
		}

		#endregion

		#region Command-related

		/// <summary>
		/// Passes the verification task to the <see cref="IControllerService"/>.
		/// </summary>
		void OnVerifyOne(object sender, EventArgs e)
		{
			IControllerService svc = (IControllerService) GetService(typeof(IControllerService));
			svc.VerifyMappings(CurrentController);
		}

		#endregion

		#region Component design-time changes tracking

		/// <summary>
		/// Removes associated view mappings whenever a component is removed from the page.
		/// </summary>
		void OnComponentRemoving(object sender, ComponentEventArgs e)
		{
			IReferenceService svc = (IReferenceService) GetService(typeof(IReferenceService));
			string id = svc.GetName(sender);
			if (id != null && CurrentController.ConfiguredViews.ContainsKey(id))
			{
				CurrentController.ConfiguredViews.Remove(id);
				//DONE: why we need to notify the host.
				RaiseComponentChanging(
					TypeDescriptor.GetProperties(CurrentController)["ConfiguredViews"]);
			}
		}

		/// <summary>
		/// Preserves view mappings in the event of a control rename.
		/// </summary>
		void OnComponentRename(object sender, ComponentRenameEventArgs e)
		{
			if (CurrentController.ConfiguredViews.ContainsKey(e.OldName))
			{
				ViewInfo info = (ViewInfo) CurrentController.ConfiguredViews[e.OldName];
				info.ControlID = e.NewName;
				CurrentController.ConfiguredViews.Remove(e.OldName);
				CurrentController.ConfiguredViews.Add(e.NewName, info);
				//Notify environment of the change.
				RaiseComponentChanging(TypeDescriptor.GetProperties(CurrentController)["ConfiguredViews"]);
			}
		}
			
		#endregion
	}
}