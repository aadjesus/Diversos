using System;
using Research.MVP.Presenters;
using Research.MVP.ViewsImpl;

namespace Research.MVP.PresentersImpl
{

    public class ContentContainerPresenter : GenericPresenter<IContentContainerView>
    {

        // Methods

        public override BasicPresenter Initialize()
        {
            base.Initialize();

            if ((null != this.Root) && (this.Root is IDateTimeFormatChanger))
            {
                (this.Root as IDateTimeFormatChanger).DateTimeFormatChanged += new DateTimeFormatEventHandler(ContentPresenter_DateTimeFormatChanged);
            }

            this.View.LastUpdated = DateTime.Now.ToString();

            return this;
        }

        private void ContentPresenter_DateTimeFormatChanged(object sender, DateTimeFormatEventArgs e)
        {
            this.View.LastUpdated = !string.IsNullOrEmpty(e.Format) ? string.Format(e.Format, DateTime.Now) : DateTime.Now.ToString();
        }

    }

}
