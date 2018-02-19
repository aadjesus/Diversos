using System;
using System.Web.UI;
using Research.MVP.Web;
using Research.MVP.ViewsImpl;
using Research.MVP.PresentersImpl;

namespace Research.MVP.WebApplicationExample
{
    public partial class Default : MVPPage<DefaultPresenter, IDefaultView>, IDefaultView
    {

        // Methods

        protected void dropDownListDateTimeFormats_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Presenter.HandleDateTimeFormatChanged();
        }

        #region IDefaultView Members

        public object DateTimeFormatSource
        {
            set 
            { 
                this.dropDownListDateTimeFormats.DataSource = value;
                this.dropDownListDateTimeFormats.DataTextField = "key";
                this.dropDownListDateTimeFormats.DataValueField = "value";
                this.dropDownListDateTimeFormats.DataBind();
            }
        }

        public string SelectedDateTimeFormat
        {
            get { return this.dropDownListDateTimeFormats.SelectedValue; }
            set { this.dropDownListDateTimeFormats.SelectedValue = value; }
        }

        #endregion

    }
}
