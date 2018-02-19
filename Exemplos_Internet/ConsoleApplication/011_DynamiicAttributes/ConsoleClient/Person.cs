using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using DynamicAttributes;

namespace ConsoleClient
{

    public class Person : ISecurable
    {
        public int ID { get; set; }

        //[Secured(RoleProvider.GetPersonRoles())]
        public string Name {get; set;}

        //[Secured(RoleProvider.GetPersonRoles())]
        public int Age { get; set; }


        public Person()
        {            
        }

        #region ISecurable Members

        public string[] GetRoles()
        {
            //Look up the roles that can access all the artifacts of this instance from some data source, for now let's fake it.
            return new string[] { "Admin"};
        }

        public string[] GetRoles(string propertyName)
        {
            //Look up the roles that can access the specified property from some data source, for now let's fake it.
            if (ID == 1)
            {
                switch (propertyName)
                {
                    case "ID":
                        {
                            return new string[] { "Power Users" };
                        }
                    case "Name":
                        {
                            return new string[] { "Power Users","Users" };
                        }
                    default:
                        {
                            return new string[] { };
                        }
                }
            }
            else
            {
                switch (propertyName)
                {
                    case "ID":
                        {
                            return new string[] {"Power Users"};
                        }
                    case "Name":
                        {
                            return new string[] {"Power Users"};
                        }
                    default:
                        {
                            return new string[] {};
                        }
                }
            }
        }

        #endregion
    }
     
}
