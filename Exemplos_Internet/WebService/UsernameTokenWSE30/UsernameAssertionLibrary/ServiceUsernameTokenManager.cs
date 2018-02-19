using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Design;
using Microsoft.Web.Services3.Security;
using Microsoft.Web.Services3.Security.Tokens;

namespace UsernameAssertionLibrary
{
    public class ServiceUsernameTokenManager : UsernameTokenManager
    {
        /// <summary>
        /// Constructs an instance of this security token manager.
        /// </summary>
        public ServiceUsernameTokenManager()
        {
        }

        /// <summary>
        /// Constructs an instance of this security token manager.
        /// </summary>
        /// <param name="nodes">An XmlNodeList containing XML elements from a configuration file.</param>
        public ServiceUsernameTokenManager(XmlNodeList nodes)
            : base(nodes)
        {
        }

        /// <summary>
        /// Returns the password or password equivalent for the username provided.
        /// </summary>
        /// <param name="token">The username token</param>
        /// <returns>The password (or password equivalent) for the username</returns>
        protected override string AuthenticateToken(UsernameToken token)
        {
            string username = token.Username;

            // it's up to you where you will get a password for some user
            // you may:
            // 1) get the password hash from web.config or system registry
            //    if you are implementing per-server security
            // 2) get the password from the database or XML file for the given user name

            // for example purposes we just return a reversed value of username
            char[] ch = username.ToCharArray();
            Array.Reverse(ch);
            return new String(ch);
        }

    }
}
