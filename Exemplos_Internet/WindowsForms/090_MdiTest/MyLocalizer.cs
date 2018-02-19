using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors.Controls;

namespace MdiTest
{
    public class MyLocalizer : Localizer
    {

        public override string GetLocalizedString(StringId id)
        {
            if (id == StringId.FilterOutlookDateText)
                return CreateFilterOutlookDateText();
            return "HALLO";
        }

        private string CreateFilterOutlookDateText() 
        {
            return "None|Specific Date|Beyond This Year|Later This Year|Later This Month|Next Week|Later This Week|Tomorrow|Today|Yesterday|Earlier This Week|Last Week|Earlier This Month|Earlier This Year|Prior This Year|asdnasjh";
            
        }
    }
}
