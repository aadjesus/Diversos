using System;
using Research.MVP.Views;

namespace Research.MVP.ViewsImpl
{

    public interface IEmployeesListView : IBasicView
    {

        // Properties

        object Source { set; }

        string DateTimeFormat { set; }

    }

}
