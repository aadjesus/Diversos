using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenMind.Common {
    /// <summary>
    /// Sample struct!
    /// </summary>
    public struct State {
        #region private fields
        /// <summary>
        /// 
        /// </summary>
        private String name;        
        #endregion

        #region public constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public State( String name ):this() {
            this.Name = name;
        }
        #endregion

        #region public properties

        /// <summary>
        /// 
        /// </summary>
        public String Name {

            get {
                return this.name;
            }

            set {

                String temp = ( value as String );

                if ( temp != null ) this.name = temp.Trim();
                
                if ( this.name == String.Empty ) throw new StateNotValidException();

            }
        }
        #endregion
    }
}
