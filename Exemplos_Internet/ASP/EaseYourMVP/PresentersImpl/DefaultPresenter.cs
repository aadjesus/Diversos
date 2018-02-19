using System;
using Research.MVP.Presenters;
using Research.MVP.ViewsImpl;
using System.Collections.Generic;

namespace Research.MVP.PresentersImpl
{

    public class DefaultPresenter : GenericPresenter<IDefaultView>, IDateTimeFormatChanger
    {

        // Methods

        public override BasicPresenter Initialize()
        {
            base.Initialize();

            Dictionary<string, string> source = new Dictionary<string, string>();
            source.Add("default", string.Empty);
            source.Add("MM/dd/yyyy", "{0:MM/dd/yyyy}");
            source.Add("dddd, dd MMMM yyyy HH:mm", "{0:dddd, dd MMMM yyyy HH:mm}");
            source.Add("MM/dd/yyyy HH:mm", "{0:MM/dd/yyyy HH:mm}");
            
            this.View.DateTimeFormatSource = source;

            return this;
        }

        public void HandleDateTimeFormatChanged()
        {
            if (null != this.DateTimeFormatChanged)
            {
                this.DateTimeFormatChanged(this, new DateTimeFormatEventArgs(this.View.SelectedDateTimeFormat));
            }
        }


        #region IDateTimeFormatChanger Members

        public event DateTimeFormatEventHandler DateTimeFormatChanged;

        #endregion

    }

}
