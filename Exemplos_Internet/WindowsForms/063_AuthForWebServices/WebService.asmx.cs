using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace AuthForWebServices
{
	/// <summary>
	/// Summary description for WebService.
	/// </summary>
	public class WebService : System.Web.Services.WebService
	{
		public AuthHeader Authentication;

		public WebService()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
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

		[SoapHeader ("Authentication", Required=true)]
		[WebMethod (Description="Returns some sample data")]
		public DataSet SensitiveData()
		{
			DataSet data = new DataSet();
			
			//Do our authentication
			//this can be via a database or whatever
			if(Authentication.Username == "test" && Authentication.Password == "test")
			{
				//they are allowed access to our sensitive data
				
				//just create some dummy data
				DataTable dtTable1 = new DataTable();
				DataColumn drCol1 = new DataColumn("Data", System.Type.GetType("System.String"));
				dtTable1.Columns.Add(drCol1);

				DataRow drRow = dtTable1.NewRow();
				drRow["Data"] = "Sensitive Data";
				dtTable1.Rows.Add(drRow);
				dtTable1.AcceptChanges();

				data.Tables.Add(dtTable1);


				
			}
			else
			{
				data = null;
			}			

			return data;
		}

	}

	public class AuthHeader : SoapHeader
	{
		public string Username;
		public string Password;
	}
}
