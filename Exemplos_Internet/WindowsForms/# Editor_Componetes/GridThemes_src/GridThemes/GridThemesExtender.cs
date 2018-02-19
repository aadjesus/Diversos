using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Reflection;
using System.Drawing.Design;


namespace UNLV.IAP.GridThemes
{
    // --------------------------------------------------------------
    // GridThemesExtender class
    // --------------------------------------------------------------
    // An implementation of IExtenderProvider, providing GridView
    // controls with the extended property "GridTheme", which may be
    // set to any of the themes defined through <Theme> tags in a 
    // GridThemesBuildProvider-parsed file.
    // --------------------------------------------------------------

    [ProvideProperty("GridTheme", typeof(GridView))]
    [ToolboxData("<{0}:GridThemesExtender runat='server'></{0}:GridThemesExtender>")]
    [ParseChildren(true, "Props")]
    [PersistChildren(false)]
    [Designer(typeof(GridThemesExtenderDesigner))]
    public class GridThemesExtender : Control, IExtenderProvider
    {

        #region Private fields
        private ExtenderPropertiesCollection _props = new ExtenderPropertiesCollection();
        #endregion

        #region IExtenderProvider support

        // IExtenderProvider requires us to identify which control types this extender applies to
        bool IExtenderProvider.CanExtend(object o)
        {
            return (o is GridView);
        }
        
        #endregion

        #region Visual Studio support
        // this function helps ensure compatibility with the Visual Studio designer
        protected void NotifyDesignerOfChange()
        {
            // Thanks to Paul Easter for this code on microsoft.public.dotnet.framework.aspnet.buildingcontrols

            // Tell the designer that the component has changed
            if (this.Site != null && this.Site.DesignMode)
            {
                try
                {
                    IDesignerHost host = (IDesignerHost)this.Site.GetService(typeof(IDesignerHost));
                    if (host != null)
                    {
                        IComponentChangeService changer = (IComponentChangeService)host.GetService(typeof(IComponentChangeService));
                        if (changer != null)
                        {
                            //changer.OnComponentChanging(this, null);
                            changer.OnComponentChanged(this, null, null, null);
                        }
                    }
                }
                catch
                {
                    // nothing for now;
                }
            }
        }

        #endregion

        #region Extender property support

        // we're exposing our collection of ExenderProperties here to ensure Visual Studio
        // will persist the extended property values for each GridView; without this,
        // extended property settings are not persisted
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Browsable(false)]
        public ExtenderPropertiesCollection Props
        {
            get
            {
                return _props;                
            }
        }


        // return the GridTheme extended property value for the given control
        [Category("GridThemes")]
        [Editor(typeof(GridThemesEditor), typeof(UITypeEditor))]
        public string GetGridTheme(GridView grid)
        {
            return Props.GetGridTheme(grid.ID);
        }

        // set the GridTheme extended property value for the given control
        public void SetGridTheme(GridView grid, string theme)
        {
            Props.SetGridTheme(grid.ID, theme);
            NotifyDesignerOfChange();  // ensure compatibility with Visual Studio
        }

        #endregion

        #region Extender functionality
        // Assign our custom report format code to the RowDataBound event for each
        // GridView that has a conditional theme set
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            foreach (ExtenderProperties p in Props)
            {
                GridView g = FindControl(p.GridID) as GridView;
                if (g != null && p.GridTheme != "")
                {
                    // determine the method given the theme title
                    GridViewRowEventHandler method = GridThemesManager.GetThemeMethodFromTitle(p.GridTheme);
                    if (method != null)
                        g.RowDataBound += method;
                }
            }            
        }


        #endregion

    }

}
