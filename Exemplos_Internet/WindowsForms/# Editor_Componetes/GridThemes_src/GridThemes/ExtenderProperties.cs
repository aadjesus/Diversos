using System;
using System.Collections.Generic;
using System.Text;

namespace UNLV.IAP.GridThemes
{
    // --------------------------------------------------------------
    // ExtenderProperties
    // --------------------------------------------------------------
    // Stores one record of extended properties for a given GridView
    // --------------------------------------------------------------
    public class ExtenderProperties
    {
        private string _gridTheme;
        private string _gridID;

        public string GridTheme
        {
            get { return _gridTheme; }
            set { _gridTheme = value; }
        }

        public string GridID
        {
            get { return _gridID; }
            set { _gridID = value; }
        }	
    }


    // --------------------------------------------------------------
    // ExtenderPropertiesCollection
    // --------------------------------------------------------------
    // Stores a collection of values associated with extended properties;
    // this collection also defines utility methods GetGridTheme and
    // SetGridTheme for easily retrieving/setting an individual
    // GridView's GridTheme extended property
    // --------------------------------------------------------------
    public class ExtenderPropertiesCollection : List<ExtenderProperties>
    {
        public string GetGridTheme(string gridID)
        {
            for (int index = 0; index < this.Count; index++)
            {
                if (this[index].GridID == gridID)
                {
                    return this[index].GridTheme;
                }
            }
            return "";
        }

        public void SetGridTheme(string gridID, string value)
        {
            bool b = false;
            for (int index = 0; index < this.Count; index++)
            {
                if (this[index].GridID == gridID)
                {
                    if (value == "")
                    {
                        this.RemoveAt(index);
                    }
                    else
                    {
                        this[index].GridTheme = value;
                        b = true;
                    }
                    break;
                }
            }
            if (!b && value != "")
            {
                ExtenderProperties p = new ExtenderProperties();
                p.GridTheme = value;
                p.GridID = gridID;
                this.Add(p);
            }
        }
    }
}
