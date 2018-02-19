using System;
using Research.MVP.Views;

namespace Research.MVP.ViewsImpl
{

    public interface IDefaultView : IBasicView
    {

        //// Methods

        //void SelectedDateTimeFormatChanged();


        // Properties

        object DateTimeFormatSource { set; }

        string SelectedDateTimeFormat { get; set; }

    }

}
