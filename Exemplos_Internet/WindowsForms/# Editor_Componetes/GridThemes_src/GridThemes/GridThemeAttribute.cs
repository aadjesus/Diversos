using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;  

namespace UNLV.IAP.GridThemes
{
    // --------------------------------------------------------------
    // GridThemeAttribute
    // --------------------------------------------------------------
    // Used to mark dynamically constructed methods as GridView.RowDataBound
    // event handlers
    // --------------------------------------------------------------
    [AttributeUsage(AttributeTargets.Method)]
    public class GridThemeAttribute : Attribute
    {
        private string _title;

        public GridThemeAttribute(string title)
        {
            _title = title;
        }

        public string Title
        {
            get
            {
                return _title;
            }
        }
    }
}
