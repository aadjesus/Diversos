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

namespace WebComponents
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class Home : System.Web.UI.Page
	{
		#region Designer & Ctor stuff
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.WebControls.Button btnSave;
		protected System.Web.UI.WebControls.TextBox txtID;
		protected System.Web.UI.WebControls.TextBox txtName;
		protected System.Web.UI.WebControls.TextBox txtCity;
		protected System.Web.UI.WebControls.TextBox txtState;
		protected System.Web.UI.WebControls.TextBox txtCountry;
		protected System.Web.UI.WebControls.Button btnLoad;
		protected PubsMvc.PublisherController controller;
		protected System.Web.UI.WebControls.Button btnDelete;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			//Response.Write("Service component site: ");
			//object service = this.Site.GetService(typeof(System.ComponentModel.Design.IServiceContainer));
			//Response.Write(service);
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
			this.components = new System.ComponentModel.Container();
			System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
			this.controller = new PubsMvc.PublisherController(this.components);
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// controller
			// 
			this.controller.ConnectionString = ((string)(configurationAppSettings.GetValue("PubsConnection", typeof(string))));
			// 
			// ------------- Mvc Custom Code -------------
			// Controller code
			// 
			this.controller.ConfiguredViews.Add("txtName", new Mvc.Components.Controller.ViewInfo("txtName", "Text", "Publisher", "Name"));
			this.controller.ConfiguredViews.Add("txtID", new Mvc.Components.Controller.ViewInfo("txtID", "Text", "Publisher", "ID"));
			this.controller.ConfiguredViews.Add("txtCity", new Mvc.Components.Controller.ViewInfo("txtCity", "Text", "Publisher", "City"));
			this.controller.ConfiguredViews.Add("txtCountry", new Mvc.Components.Controller.ViewInfo("txtCountry", "Text", "Publisher", "Country"));
			this.controller.ConfiguredViews.Add("txtState", new Mvc.Components.Controller.ViewInfo("txtState", "Text", "Publisher", "State"));
			// Connect the controller with the hosting environment.
			new Mvc.Components.Adapter.WebFormsAdapter().Connect(this.controller, this, this.components);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#endregion

		private void btnLoad_Click(object sender, System.EventArgs e)
		{
			controller.LoadPublisher();
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			controller.SavePublisher();
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			controller.DeletePublisher();
		}

	}
}
