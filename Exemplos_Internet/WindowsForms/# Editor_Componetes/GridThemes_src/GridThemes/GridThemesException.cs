using System;
using System.Collections.Generic;
using System.Text;

namespace UNLV.IAP.GridThemes
{
    // --------------------------------------------------------------
    // GridThemesException class
    // --------------------------------------------------------------
    // provides a strongly-typed class for throwing exceptions within
    // the GridThemes code
    // --------------------------------------------------------------
    [Serializable()]
    public class GridThemesException : ApplicationException
    {
        public GridThemesException()
            : base()
        {
        }

        public GridThemesException(string message) 
            : base(message)
        {
        }

        public GridThemesException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
