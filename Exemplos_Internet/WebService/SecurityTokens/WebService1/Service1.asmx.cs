using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Microsoft.Web.Services3.Security;
using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Security.Tokens;

namespace WebService1
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {

        [WebMethod]
        public long perform(long a, long b)
        {
            //check whether the request is from a valid source or not.
            if (ValidateToken())
                return a + b;
            else
                return long.MinValue;
        }


        public bool ValidateToken()
        {
            try
            {
                //The Security elements are extracted from the SOAP context and stored in a collection
                SecurityElementCollection e = RequestSoapContext.Current.Security.Elements;

                //The collection containing the SOAP Context is iterated through to get the message signature
                foreach (ISecurityElement secElement in e)
                {
                    if (secElement is MessageSignature)
                    {
                        MessageSignature msgSig = (MessageSignature)secElement;
                        if ((msgSig.SignatureOptions & SignatureOptions.IncludeSoapBody) != 0)
                        {
                            SecurityToken sigTok = msgSig.SigningToken;
                            //check whether the signature contains a username or a kerberos token
                            if (sigTok is UsernameToken)
                            {
                                //This checks against the BuiltIn Users
                                return sigTok.Principal.IsInRole(@"BUILTIN\Users");
                            }
                            else if (sigTok is KerberosToken)
                            {
                                //The logged in user is checked against the Kerberos Key Distribution Center(KDC).
                                return sigTok.Principal.Identity.IsAuthenticated;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }
    }
}