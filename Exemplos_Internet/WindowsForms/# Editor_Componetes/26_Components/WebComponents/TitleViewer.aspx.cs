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
	/// Summary description for TitleViewer.
	/// </summary>
	public class TitleViewer : System.Web.UI.Page
	{
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.WebControls.TextBox txtType;
		protected System.Web.UI.WebControls.TextBox txtNotes;
		protected System.Web.UI.WebControls.TextBox txtPrice;
		protected System.Web.UI.WebControls.TextBox txtDate;
		protected PubsMvc.PublisherController publisher;
		protected System.Web.UI.WebControls.TextBox txtTitle;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (Request.QueryString["id"] != null)
				publisher.LoadTitle(Request.QueryString["id"]);
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
			this.publisher = new PubsMvc.PublisherController(this.components);
			((System.Web.UI.IAttributeAccessor)(this.txtTitle)).SetAttribute("MVC_Controller", "publisher");
			((System.Web.UI.IAttributeAccessor)(this.txtTitle)).SetAttribute("MVC_Model", "Title");
			((System.Web.UI.IAttributeAccessor)(this.txtTitle)).SetAttribute("MVC_ModelProperty", "Title");
			((System.Web.UI.IAttributeAccessor)(this.txtTitle)).SetAttribute("MVC_ControlProperty", "Text");
			((System.Web.UI.IAttributeAccessor)(this.txtType)).SetAttribute("MVC_Controller", "publisher");
			((System.Web.UI.IAttributeAccessor)(this.txtType)).SetAttribute("MVC_Model", "Title");
			((System.Web.UI.IAttributeAccessor)(this.txtType)).SetAttribute("MVC_ModelProperty", "Type");
			((System.Web.UI.IAttributeAccessor)(this.txtType)).SetAttribute("MVC_ControlProperty", "Text");
			((System.Web.UI.IAttributeAccessor)(this.txtNotes)).SetAttribute("MVC_Controller", "publisher");
			((System.Web.UI.IAttributeAccessor)(this.txtNotes)).SetAttribute("MVC_Model", "Title");
			((System.Web.UI.IAttributeAccessor)(this.txtNotes)).SetAttribute("MVC_ModelProperty", "Notes");
			((System.Web.UI.IAttributeAccessor)(this.txtNotes)).SetAttribute("MVC_ControlProperty", "Text");
			((System.Web.UI.IAttributeAccessor)(this.txtPrice)).SetAttribute("MVC_Controller", "publisher");
			((System.Web.UI.IAttributeAccessor)(this.txtPrice)).SetAttribute("MVC_Model", "Title");
			((System.Web.UI.IAttributeAccessor)(this.txtPrice)).SetAttribute("MVC_ModelProperty", "Price");
			((System.Web.UI.IAttributeAccessor)(this.txtPrice)).SetAttribute("MVC_ControlProperty", "Text");
			((System.Web.UI.IAttributeAccessor)(this.txtDate)).SetAttribute("MVC_Controller", "publisher");
			((System.Web.UI.IAttributeAccessor)(this.txtDate)).SetAttribute("MVC_Model", "Title");
			((System.Web.UI.IAttributeAccessor)(this.txtDate)).SetAttribute("MVC_ModelProperty", "PublicationDate");
			((System.Web.UI.IAttributeAccessor)(this.txtDate)).SetAttribute("MVC_ControlProperty", "Text");
			// 
			// publisher
			// 
			this.publisher.ConnectionString = ((string)(configurationAppSettings.GetValue("PubsConnection", typeof(string))));
			// 
			// ------------- Mvc Custom Code -------------
			// Controller code
			// 
			this.publisher.ConfiguredViews.Add("txtType", new Mvc.Components.Controller.ViewInfo("txtType", "Text", "Title", "Type"));
			this.publisher.ConfiguredViews.Add("txtTitle", new Mvc.Components.Controller.ViewInfo("txtTitle", "Text", "Title", "Title"));
			this.publisher.ConfiguredViews.Add("txtNotes", new Mvc.Components.Controller.ViewInfo("txtNotes", "Text", "Title", "Notes"));
			this.publisher.ConfiguredViews.Add("txtPrice", new Mvc.Components.Controller.ViewInfo("txtPrice", "Text", "Title", "Price"));
			this.publisher.ConfiguredViews.Add("txtDate", new Mvc.Components.Controller.ViewInfo("txtDate", "Text", "Title", "PublicationDate"));
			// Connect the controller with the hosting environment.
			new Mvc.Components.Adapter.WebFormsAdapter().Connect(this.publisher, this, this.components);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
