using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace ImageUrl
{
    public class MyImage : System.Web.UI.Control
    {
        [UrlProperty, Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(System.Drawing.Design.UITypeEditor)), Bindable(true), DefaultValue("")]
        public string ImageUrl
        {
            get
            {
                string url = (string)this.ViewState["ImageUrl"];
                if (url != null)
                {
                    return url;
                }
                return String.Empty;
            }
            set
            {
                this.ViewState["ImageUrl"] = value;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write("<img src=\"" + ResolveClientUrl(ImageUrl) + " />");
        }
    }
}
