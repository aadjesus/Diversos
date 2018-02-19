using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuildTasks.CustomType
{
    public class Credential
    {
        public string Domain { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        
        public override string ToString()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return null;
            }
            else
            {
                return Domain + @"\" + UserName;
            }
        }
    }
}
