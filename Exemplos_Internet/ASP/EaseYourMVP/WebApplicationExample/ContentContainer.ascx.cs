using System;
using System.Web.UI;
using Research.MVP.Web;
using Research.MVP.PresentersImpl;
using Research.MVP.ViewsImpl;

namespace Research.MVP.WebApplicationExample
{

    public partial class ContentContainer : MVPUserControl<ContentContainerPresenter, IContentContainerView>, IContentContainerView
    {

        #region IContentContainerView Members

        public string LastUpdated
        {
            set { this.labelLastUpdated.Text = value; }
        }

        #endregion

    }

}