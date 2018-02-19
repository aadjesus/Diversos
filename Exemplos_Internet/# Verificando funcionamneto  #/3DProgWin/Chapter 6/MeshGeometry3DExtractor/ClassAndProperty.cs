//-------------------------------------------------
// ClassAndProperty.cs (c) 2007 by Charles Petzold
//-------------------------------------------------
using System;
using System.Reflection;

namespace Petzold.MeshGeometry3DExtractor
{
    public class ClassAndProperty
    {
        Type type;
        PropertyInfo prop;

        public ClassAndProperty(Type type, PropertyInfo prop)
        {
            this.type = type;
            this.prop = prop;
        }

        public Type Type
        {
            get { return type; }
            set { type = value; }
        }

        public PropertyInfo Property
        {
            get { return prop; }
            set { prop = value; }
        }
    }
}