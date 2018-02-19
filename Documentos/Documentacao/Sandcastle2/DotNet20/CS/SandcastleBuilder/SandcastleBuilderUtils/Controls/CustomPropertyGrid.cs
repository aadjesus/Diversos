//=============================================================================
// System  : EWSoftware Custom Property Grid
// File    : CustomPropertyGrid.cs
// Author  : Eric Woodruff
// Updated : 12/31/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a custom property grid that allows you to set the
// initial width of the property name pane and has a context menu that allows
// you to hide the description pane and reset the selected property value.
//
// This code may be used in compiled form in any way you desire.  This file
// may be redistributed unmodified by any means PROVIDING it is not sold for
// profit without the author's written consent, and providing that this notice
// and the author's name and all copyright notices remain intact.
//
// This code is provided "as is" with no warranty either express or implied.
// The author accepts no liability for any damage or loss of business that
// this product may cause.
//
// Version     Date     Who  Comments
// ============================================================================
// 1.0.0.0  08/03/2006  EFW  Created the code
//=============================================================================

using System;
using System.ComponentModel;
using System.Reflection;
using System.Security.Permissions;
using System.Windows.Forms;

// All classes go in the SandcastleBuilder.Utils.Controls namespace
namespace SandcastleBuilder.Utils.Controls
{
	/// <summary>
    /// This is a custom property grid that allows you to set the initial
    /// width of the property name pane and has a context menu that allows
    /// you to hide the description pane and reset the selected property
    /// value.
	/// </summary>
    [Description("A property grid with a default context menu and " +
      "user-definable property panel size"),
      PermissionSet(SecurityAction.InheritanceDemand, Name="FullTrust"),
      PermissionSet(SecurityAction.LinkDemand, Name="FullTrust")]
    public class CustomPropertyGrid : System.Windows.Forms.PropertyGrid
    {
        #region Private data members
        //=====================================================================
        // Private data members

        private IContainer components;
        private ContextMenuStrip ctxPropGrid;
        private ToolStripMenuItem miReset, miShowDescription;
        private ToolStripSeparator separator;
        private int paneWidth;
        #endregion

        #region Properties
        //=====================================================================
        // Properties

        /// <summary>
        /// This is used to set or get the width of the property name pane
        /// </summary>
        /// <remarks>Reflection is used to modify the pane width</remarks>
        [Category("Appearance"), Description("The width of the property name pane")]
        public int PropertyNamePaneWidth
        {
            get
            {
                if(paneWidth != 0)
                    return paneWidth;

                Type type = base.GetType().BaseType;
                BindingFlags bf = BindingFlags.NonPublic |
                    BindingFlags.Public | BindingFlags.Instance;
                object grid = type.GetField("gridView", bf).GetValue(this);

                PropertyInfo pi = grid.GetType().GetProperty(
                    "InternalLabelWidth", bf);
                object width = pi.GetValue(grid, BindingFlags.GetProperty,
                    null, null, null);

                return (int)width;
            }
            set
            {
                paneWidth = value;

                // Keep a reasonable minimum and maximum size
                if(paneWidth < 1)
                {
                    paneWidth = 0;
                    value = this.Width / 2;
                }
                else
                    if(paneWidth > this.Width / 2)
                        value = this.Width / 2;

                if(!this.DesignMode)
                {
                    Type type = base.GetType().BaseType;
                    BindingFlags bf = BindingFlags.NonPublic |
                        BindingFlags.Public | BindingFlags.Instance;

                    object grid = type.GetField("gridView", bf).GetValue(this);

                    MethodInfo mi = grid.GetType().GetMethod("MoveSplitterTo", bf);
                    mi.Invoke(grid, new object[] { value });
                }
            }
        }
        #endregion

        #region Private designer methods
        //=====================================================================
        // Private designer methods.  These are used because the default
        // values for these properties don't work with the DefaultValue
        // attribute.

        // The designer uses this to determine whether or not to serialize
        // changes to the PropertyNamePaneWidth property.
        private bool ShouldSerializePropertyNamePaneWidth()
        {
            return (paneWidth != 0);
        }

        // Reset the PropertyNamePaneWidth property
        private void ResetPropertyNamePaneWidth()
        {
            this.PropertyNamePaneWidth = 0;
        }
        #endregion

        //=====================================================================
        // Methods, etc

        /// <summary>
        /// Constructor
        /// </summary>
        public CustomPropertyGrid()
        {
            components = new Container();
            ctxPropGrid = new ContextMenuStrip(this.components);
            miReset = new ToolStripMenuItem("&Reset", null,
                new EventHandler(miReset_OnClick));
            separator = new ToolStripSeparator();
            miShowDescription = new ToolStripMenuItem("Description", null,
                new EventHandler(miShowDescription_OnClick));

            ctxPropGrid.Items.AddRange(new ToolStripItem[] {
                this.miReset, this.separator, this.miShowDescription});
            ctxPropGrid.Opening += new CancelEventHandler(ctxPropGrid_Opening);

            this.ContextMenuStrip = ctxPropGrid;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged
        /// resources, false to just release unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        /// <summary>
        /// This is overridden to reset the pane width to the user-preferred
        /// width after creating the control.
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if(paneWidth != 0)
                this.PropertyNamePaneWidth = paneWidth;
        }

        /// <summary>
        /// This updates the context menu based on the current property grid
        /// state before it is displayed.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void ctxPropGrid_Opening(object sender, CancelEventArgs e)
        {
            GridItem item = base.SelectedGridItem;
            Type type = item.GetType();
            object component = type.GetProperty("Instance", 
                BindingFlags.NonPublic | BindingFlags.Public |
                BindingFlags.Instance).GetValue(item, BindingFlags.GetProperty,
                null, null, null);

            miReset.Enabled = (item.PropertyDescriptor != null &&
                item.PropertyDescriptor.CanResetValue(component));
            miShowDescription.Checked = this.HelpVisible;
        }

        /// <summary>
        /// Reset the value of the current property
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miReset_OnClick(object sender, EventArgs e)
        {
            GridItem item = base.SelectedGridItem;
            object oldValue = item.Value;

            base.ResetSelectedProperty();

            if(!item.Value.Equals(oldValue))
                base.OnPropertyValueChanged(new PropertyValueChangedEventArgs(
                    item, oldValue));

            base.Refresh();
        }

        /// <summary>
        /// Show or hide the property description pane
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void miShowDescription_OnClick(object sender, EventArgs e)
        {
            this.HelpVisible = !this.HelpVisible;
        }
    }
}
