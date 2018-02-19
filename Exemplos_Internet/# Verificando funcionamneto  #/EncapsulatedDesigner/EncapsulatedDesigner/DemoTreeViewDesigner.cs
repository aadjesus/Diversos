using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace EncapsulatedDesigner
{
    /// <summary>
    /// Designer for <see cref="TreeView"/> controls.
    /// Overriden members are delegated to an encapsulated default System.Windows.Forms.Design.TreeViewDesigner.
    /// </summary>
    /// <remarks>System.Windows.Forms.Design.TreeViewDesigner is marked internal.</remarks>
    internal class DemoTreeViewDesigner : ControlDesigner
    {
        #region Fields

        protected ControlDesigner defaultDesigner;
        protected IDesignerFilter designerFilter;

        #endregion

        #region Initialize & Dispose

        public override void Initialize(IComponent component)
        {
            // internal class TreeViewDesigner : ControlDesigner
            // Name: System.Windows.Forms.Design.TreeViewDesigner , Assembly: System.Design, Version=4.0.0.0 
            Type tDesigner = Type.GetType("System.Windows.Forms.Design.TreeViewDesigner, System.Design");
            defaultDesigner = (ControlDesigner)Activator.CreateInstance(tDesigner, BindingFlags.Instance | BindingFlags.Public, null, null, null);
            designerFilter = defaultDesigner;

            // use Designer properties of default designer ( do before base.Initialize ! )
            TypeDescriptor.CreateAssociation(component, defaultDesigner);

            IServiceContainer site = (IServiceContainer)component.Site;
            if (site != null && GetService(typeof(DesignerCommandSet)) == null)
            {
                site.AddService(typeof(DesignerCommandSet), new CDDesignerCommandSet(this));
            }
            else { Debug.Fail("site != null && GetService(typeof (DesignerCommandSet)) == null"); }

            defaultDesigner.Initialize(component);
            base.Initialize(component);

            removeDuplicateDockingActionList();
        }

        public override void InitializeNewComponent(IDictionary defaultValues)
        {
            base.InitializeNewComponent(defaultValues);
            defaultDesigner.InitializeNewComponent(defaultValues);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (defaultDesigner != null)
                {
                    defaultDesigner.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Removes the duplicate DockingActionList, added by this designer to the <see cref="DesignerActionService"/>.
        /// Default designer has already added it's DockingActionList.
        /// </summary>
        /// <remarks>
        /// <see cref="ControlDesigner.Initialize"/> adds an internal DockingActionList : 'Dock/Undock in Parent Container'.
        /// </remarks>
        private void removeDuplicateDockingActionList()
        {
            // in ControlDesigner : private DockingActionList dockingAction;
            FieldInfo fi = typeof(ControlDesigner).GetField("dockingAction", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fi != null)
            {
                DesignerActionList dockingAction = (DesignerActionList)fi.GetValue(this);
                if (dockingAction != null)
                {
                    DesignerActionService service = (DesignerActionService)GetService(typeof(DesignerActionService));
                    if (service != null)
                    {
                        service.Remove(Control, dockingAction);
                    }
                }
            }
        }

        #endregion

        #region IDesignerFilter overrides

        // TreeViewDesigner does not override IDesignerFilter methods
        // base(ComponentDesigner).PreFilterAttributes() is empty
        // base(ComponentDesigner).PreFilterEvents() is empty
        // base(ComponentDesigner).PostFilterProperties() only if (this.Component is IPersistComponentSettings)

        protected override void PostFilterAttributes(IDictionary attributes)
        {
            designerFilter.PostFilterAttributes(attributes);

            // will set this.InheritanceAttribute property from nested designer attributes, if control is inherited
            base.PostFilterAttributes(attributes);
#if DEBUG
            if (attributes.Contains(typeof(InheritanceAttribute)))
            {
                Debug.Assert(base.InheritanceAttribute == attributes[typeof(InheritanceAttribute)]);
            }
            else
            {
                Debug.Assert(base.InheritanceAttribute == InheritanceAttribute.NotInherited);
            }
#endif
        }

        protected override void PostFilterEvents(IDictionary events)
        {
            // filters events based on InheritanceAttribute
            designerFilter.PostFilterEvents(events);
        }

        #endregion

        #region Overrides

        // only defaultDesigner.ActionLists is invoked, unless CDDesignerCommandSet is used
        // DesignerActionService queries via DesignerCommandSet for ActionLists and Verbs
        public override DesignerActionListCollection ActionLists
        {
            get { return defaultDesigner.ActionLists; }
        }

        #endregion

        #region DesignerCommandSet

        private class CDDesignerCommandSet : DesignerCommandSet
        {
            private readonly ComponentDesigner componentDesigner;

            public CDDesignerCommandSet(ComponentDesigner componentDesigner)
            {
                this.componentDesigner = componentDesigner;
            }

            public override ICollection GetCommands(string name)
            {
                if (name.Equals("Verbs"))
                {
                    // componentDesigner.Verbs & defaultDesigner.Verbs are empty
                    return null;
                }
                if (name.Equals("ActionLists"))
                {
                    return componentDesigner.ActionLists;
                }
                return base.GetCommands(name);
            }
        }

        #endregion
    }
}
