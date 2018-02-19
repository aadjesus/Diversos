using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;

using Microsoft.Web.Services2;
using Microsoft.Web.Services2.Security;
using Microsoft.Web.Services2.Security.Tokens;


namespace service
{
	/// <summary>
	/// The service requires a UserName or a Kerberos Token to ececute properly
	/// </summary>
	public class Service1 : System.Web.Services.WebService
	{

		public Service1()
		{
			InitializeComponent();
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion


		[WebMethod]
		public long perform(long a,long b)
		{
			//check whether the request is from a valid source or not.
			if (ValidateToken())
				return a+b;
			else
				return long.MinValue;
		}


		// Token validator method
		// This piece of code validates the incoming soap message for a valid token
		public bool ValidateToken()
		{
			try
			{
				//The Security elements are extracted from the SOAP context and stored in a collection
				SecurityElementCollection e = RequestSoapContext.Current.Security.Elements;

				//The collection containing the SOAP Context is iterated through to get the message signature
				foreach( ISecurityElement secElement in e ) 
				{ 
					if( secElement is MessageSignature ) 
					{ 
						MessageSignature msgSig = (MessageSignature)secElement; 
						if( (msgSig.SignatureOptions & SignatureOptions.IncludeSoapBody) != 0 ) 
						{ 
							SecurityToken sigTok = msgSig.SigningToken; 
							//check whether the signature contains a username or a kerberos token
							if( sigTok is UsernameToken ) 
							{
								//This checks against the BuiltIn Users
								return sigTok.Principal.IsInRole( @"BUILTIN\Users" );
							}
							else if( sigTok is KerberosToken )
							{
								//The logged in user is checked against the Kerberos Key Distribution Center(KDC).
								return sigTok.Principal.Identity.IsAuthenticated;
							}
						} 
					} 
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return false;
		}
	}
}
