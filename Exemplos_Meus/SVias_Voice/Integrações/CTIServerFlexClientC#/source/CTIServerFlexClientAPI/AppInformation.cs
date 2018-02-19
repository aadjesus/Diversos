using System;
using System.Collections.Generic;
using System.Text;

namespace CTIServerFlexClientAPI
{
    public class AppInformation
    {
        private String device = "";
        private String password = "";
        private String version = "";
        private int number = 0;
        public String Device
        {
            get { return device; }
            set { device = value; }
        }
        public String Password
        {
            get { return password; }
            set { password = value; }
        }
        public String Version
        {
            get { return version; }
            set { version = value; }
        }
        public int Number
        {
            get { return number; }
            set { number = value; }
        }
    }
}
