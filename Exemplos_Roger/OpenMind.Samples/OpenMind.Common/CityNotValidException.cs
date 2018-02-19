using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenMind.Common {
    /// <summary>
    /// Sample class!
    /// </summary>
    public class CityNotValidException: Exception {
         #region private fields
        /// <summary>
        /// 
        /// </summary>
        private const String DefaultMessage = "The Name of city is not valid.";
        #endregion

        #region constructores
        public CityNotValidException()
            : base() {            
        }


        public CityNotValidException(String message)
            : base(message) {            
        }


        public CityNotValidException( String message, Exception innerException )
            : base( message, innerException ) {
        }
        #endregion       
    }
}
