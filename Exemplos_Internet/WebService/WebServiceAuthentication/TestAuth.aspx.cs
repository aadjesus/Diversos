using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace WebServiceAuthentication
{
    public partial class TestAuth : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //JUST FOR TEST: set the correct UserName & Password
                this.TextBoxUserName.Text = "MyUserName";
                this.TextBoxPwd.Text = "SeCrEt";
            }
        }

        protected void ButtonAuth_Click(object sender, System.EventArgs e)
        {
            string ret;
            string UserName, Password, Key, ToHash;

            UserName = this.TextBoxUserName.Text;
            Password = this.TextBoxPwd.Text;
            DateTime dt = DateTime.Now;
            ToHash = UserName.ToUpper() + "|" + Password + "|" + dt.ToString("yyyyMMdd") + "|" + dt.ToString("HHmm");
            Key = Hash(ToHash) + "|" + UserName + "|I would like to log this string in a DB";

            ServicePointReference.ServicePoint Authenticate = new ServicePointReference.ServicePoint();
            ret = Authenticate.Authenticate(Key);
            if (ret == null)
                this.ServResponse.Text = "NULL RECEIVED!!"; //never!
            else
            {
                this.ServResponse.Text = "RECEIVED DATA: " + ret;
            }
        }

        protected void ButtonGetToken_Click(object sender, System.EventArgs e)
        {
            string ret;

            ServicePointReference.ServicePoint Authenticate = new ServicePointReference.ServicePoint();
            ret = Authenticate.GetToken();
            this.TextBoxToken.Text = ret;
            string TokenDate;
            DateTime dt = DateTime.Now;
            TokenDate = dt.ToString("yyyy-MM-dd") + "|" + dt.ToString("HH:mm:ss");
            this.LabelTokendate.Text = " Token get at: " + TokenDate;
        }

        protected void ButtonService_Click(object sender, System.EventArgs e)
        {
            string ret;
            string UserName, Password, ServiceName;
            string Key, ToHash;

            UserName = this.TextBoxUserName.Text;
            Password = this.TextBoxPwd.Text;
            ServiceName = this.TextBoxService.Text;
            DateTime dt = DateTime.Now;
            ToHash = UserName.ToUpper() + "|" + Password + "|" + dt.ToString("yyyyMMdd") + "|" + dt.ToString("HHmm");
            Key = Hash(ToHash) + "|" + UserName + "|I would like to log this string in a DB";

            ServicePointReference.ServicePoint Authenticate = new ServicePointReference.ServicePoint();
            ret = Authenticate.UseService(Key, ServiceName);
            this.ServResponse.Text = ret;
        }

        protected void ButtonUseToken_Click(object sender, System.EventArgs e)
        {
            string ret;
            string UserName, Password, ServiceName, Token;
            string Key, ToHash;

            UserName = this.TextBoxUserName.Text;
            Password = this.TextBoxPwd.Text;
            ServiceName = this.TextBoxService.Text;
            Token = this.TextBoxToken.Text;
            ToHash = UserName.ToUpper() + "|" + Password + "|" + Token;
            Key = Hash(ToHash) + "|" + UserName + "|I would like to log this string in a DB";

            ServicePointReference.ServicePoint Authenticate = new ServicePointReference.ServicePoint();
            ret = Authenticate.UseService(Key, ServiceName);
            this.ServResponse.Text = ret;

            string NowDate;
            DateTime dt = DateTime.Now;
            NowDate = dt.ToString("yyyy-MM-dd") + "|" + dt.ToString("HH:mm:ss");
            this.LabelNowDate.Text = " >> Data time now: " + NowDate;
        }

        private string Hash(string ToHash)
        {
            // First we need to convert the string into bytes, which means using a text encoder.
            Encoder enc = System.Text.Encoding.ASCII.GetEncoder();

            // Create a buffer large enough to hold the string
            byte[] data = new byte[ToHash.Length];
            enc.GetBytes(ToHash.ToCharArray(), 0, ToHash.Length, data, 0, true);

            // This is one implementation of the abstract class MD5.
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(data);

            return BitConverter.ToString(result).Replace("-", "").ToLower();
        }

    }
}
