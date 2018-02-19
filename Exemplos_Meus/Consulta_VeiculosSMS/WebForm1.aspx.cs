using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.UI.MobileControls;

namespace Globus5.WPF.Sistemas.Wap
{
    public partial class WebForm1 : MobilePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Label1.Text = Session["nome"].ToString();
                Label21.Text = Session["nome"].ToString();
            }
            catch (Exception)
            {
                Label1.Text = Request.QueryString["nome"];
            }
        }

        protected void Command1_Click(object sender, EventArgs e)
        {
            Session["nome"] = Label1.Text;
            ActiveForm = Form1;
        }

    }
}
