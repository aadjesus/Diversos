using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysicsHost
{
    /// <summary>
    /// A custom Exception
    /// </summary>
    public class BindingException : ApplicationException
    {
        #region Ctors
        public BindingException() : base()
        {

        }

        public BindingException(string message)
            : base(message)
        {
        }

        public BindingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public BindingException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
        #endregion
    }
}
