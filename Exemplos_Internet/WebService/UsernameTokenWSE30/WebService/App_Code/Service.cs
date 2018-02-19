using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Microsoft.Web.Services3;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[Microsoft.Web.Services3.Policy("ServerPolicy")]
public class Service : System.Web.Services.WebService
{
    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloMyFriend() 
    {
        //Say hello to the guy that you know
        return "Hello " + RequestSoapContext.Current.IdentityToken.Identity.Name;
    }
    
}
