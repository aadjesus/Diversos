using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DynamicAttributes
{

    /// <summary>
    /// An interface representing any type which needs to be secured.
    /// </summary>
    public interface ISecurable
    {
        /// <summary>
        /// Gets the roles which have access to an instance of a type which implements this interface.
        /// </summary>
        /// <returns></returns>
        string[] GetRoles();

        /// <summary>
        /// Gets the roles which have access to the specified property of an instance of a type which implements this interface.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        string[] GetRoles(string propertyName);
    }

}
