using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenMind.Common {
    /// <summary>
    /// Sample class!
    /// </summary>
    public class StateNotValidException: Exception {

        #region private fields
        /// <summary>
        /// 
        /// </summary>
        private const String DefaultMessage = "The Name of state is not valid.";
        #endregion

        #region constructores
        public StateNotValidException()
            : base() {            
        }


        public StateNotValidException(String message)
            : base(message) {            
        }


        public StateNotValidException( String message, Exception innerException )
            : base( message, innerException ) {
        }
        #endregion       
    }
}
