using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayNightMode
{
    // in mvvm this would be the model.  There is not much logic.  its main function
    // is to keep track of data that the view model needs to be aware of.
    public class AppInfo
    {
        private bool _dayMode = true;

        public bool DayMode
        {
            get
            {
                return _dayMode;
            }
            set
            {
                _dayMode = value;
                DayNightModeChanged(value);
            }
        }

        public Action<bool> DayNightModeChanged;
    }
}
