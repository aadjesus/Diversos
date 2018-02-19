using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Design;
using Microsoft.Web.Services3.Security.Tokens;


namespace TestWebService
{
    class Program
    {
        static void Main(string[] args)
        {
            //init web service proxy 
            Service serviceProxy = new Service();

            //init UsernameToken, password is the reverted string of username, the same logic in AuthenticateToken
            //  of ServiceUsernameTokenManager class.
            UsernameToken token = new UsernameToken("admin", "nimda", PasswordOption.SendPlainText);

            // Set the token onto the proxy
            serviceProxy.SetClientCredential(token);

            // Set the ClientPolicy onto the proxy
            serviceProxy.SetPolicy("ClientPolicy");

            //invoke the HelloMyFriend web service method
            try
            {
                Console.WriteLine(serviceProxy.HelloMyFriend());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
