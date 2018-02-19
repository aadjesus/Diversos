using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Globus5.WPF.Sistemas.Wap
{
    public partial class _Default : System.Web.UI.MobileControls.MobilePage
    {
        protected void Command1_Click(object sender, EventArgs e)
        {
            Session["nome"] = TextBox1.Text;
            Response.Redirect("WebForm1.aspx");            
        }

        protected void Command2_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm1.aspx?nome=" + TextBox1.Text);
        }

    }
}
