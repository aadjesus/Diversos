using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace AppTesteHuman
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //?id=5&from=1111111111&to=28413&msg=aguiamay_com_br&account=BGM.HG&dataHoraReceb=13/1/2010
            //BGM.HG
            //28413

            string id = Page.Request.QueryString["id"];
            string from = Page.Request.QueryString["from"];
            string to = Page.Request.QueryString["to"];
            string msg = Page.Request.QueryString["msg"];
            string date = Page.Request.QueryString["date"];
            string account = Page.Request.QueryString["account"];

            DateTime dt = DateTime.Now;

            try
            {
                dt = Convert.ToDateTime(date);
            }
            catch (Exception)
            {

            }

            try
            {
                IntegracaoWS.IntegracaoWS iWs = new AppTesteHuman.IntegracaoWS.IntegracaoWS();

                iWs.SalvarRecebimentoSMS("id1", "from1", "to1", "msg1", "account1", DateTime.Now);
                //iWs.SalvarRecebimentoSMS(id, from, to, msg, account, dt);
                Page.Response.Write("Dados gravados com sucesso.");
            }
            catch (Exception ex)
            {
                Page.Response.Write("Erro: " + ex.InnerException);
            }
        }
    }
}
