using System;
using System.Collections.Generic;
using System.Text;

namespace CTIServerFlexClientAPI.Tables
{
    public interface ITableChangedListener
    {
        void OnAdded(TableRecord record);
        void OnRemoved(TableRecord record);
        void OnUpdated(TableRecord record);
    }
}
