using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.Design;

namespace UNLV.IAP.GridThemes
{
    // --------------------------------------------------------------
    // GridThemesExtenderDesigner class
    // --------------------------------------------------------------
    // An implementation of ControlDesigner for the GridThemesExtender
    // control; provides design-time appearance HTML for the extender
    // control.
    // --------------------------------------------------------------

    class GridThemesExtenderDesigner : ControlDesigner
    {
        public override string GetDesignTimeHtml()
        {
            string tmpl = "<span style='font-size: 8pt;'>"
                        + "To apply conditional formatting, set the <b>GridTheme</b> "
                        + "property on the desired GridView controls.  "
                        + "<br />{0}"
                        + "</span>";

            int iCount = 0;
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul style='margin-top: 3px; margin-bottom: 3px;'>");
            foreach (ExtenderProperties p in ((GridThemesExtender)Component).Props)
                if (p.GridTheme != "")
                {
                    iCount++;
                    sb.AppendFormat("<li style='margin-top: 1px; margin-bottom: 2px;'>{0}: <i>{1}</i></li>", p.GridID, p.GridTheme);
                }
            sb.Append("</ul>");

            string sDis = "";
            if (iCount > 0)
                sDis = string.Format("The extender will apply formatting to the following GridViews: {0}", sb.ToString());
            else
                sDis = "At present, no GridViews will be formatted by this extender.";

            string msg = string.Format(tmpl, sDis);
            return this.CreatePlaceHolderDesignTimeHtml(msg);
        }
    }
}
