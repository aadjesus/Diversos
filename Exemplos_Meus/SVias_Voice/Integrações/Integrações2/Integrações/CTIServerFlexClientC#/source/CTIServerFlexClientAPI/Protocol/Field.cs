using System;
using System.Collections.Generic;
using System.Text;

namespace CTIServerFlexClientAPI.Protocol
{
    public class Field
    {
        private int _name;
        private Object _value;
        public Field(int name, Object value)
        {
            _name = name;
            _value = value;
        }
        public int Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public Object Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
