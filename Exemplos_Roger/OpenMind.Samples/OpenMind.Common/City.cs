using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenMind.Common {
    /// <summary>
    /// Sample struct!
    /// </summary>
    public struct City {

        #region private fields
        /// <summary>
        /// 
        /// </summary>
        private String name;

        /// <summary>
        /// 
        /// </summary>
        private State state;
        #endregion

        #region public constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="state"></param>
        public City( String name, State state ):this() {
            this.Name = name;
            this.State = state;
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
                
                if ( this.name != String.Empty ) throw new CityNotValidException();

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public State State {
            get {
                return this.state;
            }

            set {
                this.state = value;
            }
        }
        #endregion

    }
}
