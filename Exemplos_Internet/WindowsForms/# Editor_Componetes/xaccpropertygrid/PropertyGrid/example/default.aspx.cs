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

namespace PropertyGridWeb
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
    protected Xacc.PropertyGrid pg1;
    protected System.Web.UI.WebControls.Button Button1;
    protected System.Web.UI.WebControls.Button Button2;
    protected Xacc.PropertyGrid pg2;
  
		private void Page_Load(object sender, System.EventArgs e)
		{
      //if (!IsPostBack)
      {
        pg1.SelectedObject = Global.STATIC;
        pg2.SelectedObject = Global.STATIC;
      }
      //else
      {
        //PropertyGrid2.SelectedObject = Session["PropertyGrid_object"];
      }
		}

    protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
    {
      base.RaisePostBackEvent (sourceControl, eventArgument);
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
      this.Button1.Click += new System.EventHandler(this.Button1_Click);
      this.Button2.Click += new System.EventHandler(this.Button2_Click);
      this.Load += new System.EventHandler(this.Page_Load);

    }
		#endregion

    private void DropDownList1_SelectedIndexChanged(object sender, System.EventArgs e)
    {
      ;
    }

    private void Button1_Click(object sender, System.EventArgs e)
    {
      Response.Redirect("objectcode.aspx");
    }

    private void Button2_Click(object sender, System.EventArgs e)
    {
      Response.Redirect("usage.aspx");
    }
	}
}
