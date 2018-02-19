using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DynamicAttributes
{

   [AttributeUsage(AttributeTargets.Property)]
    public class SecuredAttribute : Attribute
    {
    
        public string[] Roles { get; private set; }

        public SecuredAttribute()
        {
        }
        public SecuredAttribute(string[] roles)
        {
            Roles = roles;
        }
    }

}
