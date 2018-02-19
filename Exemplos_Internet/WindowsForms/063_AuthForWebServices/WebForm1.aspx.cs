using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace AuthForWebServices
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid dgData;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			//simple client
			AuthWebService.WebService webService = new AuthWebService.WebService();
			AuthWebService.AuthHeader authentication = new AuthWebService.AuthHeader();

			authentication.Username = "test";
			authentication.Password = "test";
			webService.AuthHeaderValue = authentication;

			DataSet dsData = webService.SensitiveData();

			dgData.DataSource = dsData;
			dgData.DataBind();	

		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
