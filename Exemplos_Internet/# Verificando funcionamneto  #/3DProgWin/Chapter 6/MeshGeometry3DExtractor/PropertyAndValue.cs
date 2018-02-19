//-------------------------------------------------
// PropertyAndValue.cs (c) 2007 by Charles Petzold
//-------------------------------------------------
using System;
using System.Reflection;

namespace Petzold.MeshGeometry3DExtractor
{
    public class PropertyAndValue
    {
        PropertyInfo prop;
        string val;

        public PropertyAndValue(PropertyInfo prop, string val)
        {
            this.prop = prop;
            this.val = val;
        }

        public PropertyInfo Property
        {
            get { return prop; }
            set { prop = value; }
        }

        public string Value
        {
            get { return val; }
            set { val = value; }
        }
    }
}