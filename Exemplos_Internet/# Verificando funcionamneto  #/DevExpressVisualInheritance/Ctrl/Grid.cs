using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Reflection;
using System.Data;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Design;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Localization;
using DevExpress.Utils.Design;

namespace Ctrl
{
    [Designer(typeof(BHGridDesigner))]
    public class Grid : GridControl, IButtonControl
    {

        /// <summary>
        /// Does Nothing. This is needed to support setting grid as default button.
        /// </summary>
        /// <param name="value"></param>
        public void NotifyDefault(bool value) { }

        /// <summary>
        /// Raises the grid's DoubleClick event. This will be used on "return" if
        /// BHGrid is set as the Form's AcceptButton.
        /// </summary>
        public void PerformClick()
        {
            if (this.CanSelect)
            {
                this.OnDoubleClick(EventArgs.Empty);
            }
        }

        // Implement IButtonControl interface, so that the
        // Grid's double-click can be used as a "default button" action
        /// <summary>
        /// Always returns DialogResult.None. Setting it has no effect.
        /// This is needed to support setting grid as default button.
        /// </summary>
        [Browsable(false),
         DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public DialogResult DialogResult
        {
            get { return DialogResult.None; }
            set { }
        }
    }


    /// <summary>
    /// Designer class for extending the design mode behaviour of a BHGrid
    /// </summary>
    public class BHGridDesigner : GridControlDesigner
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public BHGridDesigner()
        {
            this.DXVerbs.Add(new DesignerVerb("Set GridView Defaults", new EventHandler(OnSetGridViewDefaults)));
        }

        protected override bool AllowEditInherited
        {
            get
            {
                return true;
            }
        }


        //** Methods Definitons

        /// <summary>
        /// This method sets all default settings on the grid view control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSetGridViewDefaults(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
            GridView gv = Grid.MainView as GridView;

            gv.OptionsSelection.EnableAppearanceFocusedCell = false;
            gv.OptionsBehavior.Editable = false;
            gv.OptionsCustomization.AllowFilter = false;
            gv.OptionsCustomization.AllowGroup = false;
            gv.OptionsNavigation.AutoFocusNewRow = true;
            gv.OptionsNavigation.UseTabKey = false;
            gv.OptionsView.ShowGroupPanel = false;
            gv.OptionsView.ShowIndicator = false;
            gv.OptionsView.ShowGroupedColumns = false;
            gv.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
        }

    }
}
