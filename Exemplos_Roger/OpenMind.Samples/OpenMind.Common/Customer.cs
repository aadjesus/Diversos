using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenMind.Common {
    /// <summary>
    /// Sample class!
    /// </summary>
    public class Customer {

        //TODO: Incluir a descrição dos membros nos comentários.

        #region private fields
        /// <summary>
        /// 
        /// </summary>
        private String id;


        /// <summary>
        /// 
        /// </summary>
        private String name;

        /// <summary>
        /// 
        /// </summary>
        private String phone;

        /// <summary>
        /// 
        /// </summary>
        private City? city;        
        #endregion

        #region constructors
        /// <summary>
        /// 
        /// </summary>
        private Customer():base() {
            this.id = String.Empty;
            this.name = String.Empty;
            this.phone = String.Empty;
            this.city = null;            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="city"></param>
        public Customer( String id, String name, String phone, City? city ):this() {
            this.Id = id;
            this.Name = name;
            this.Phone = phone;
            this.City = city;
        }
        #endregion

        #region public behavior        
        #endregion

        #region private behavior
        #endregion

        #region public properties
        /// <summary>
        /// 
        /// </summary>
        public String Id {
            get {
                return this.id;
            }

            set {
                this.id = value;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public String Name {
            get {
                return this.name;
            }

            set {
                this.name = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String Phone {
            get {
                return this.phone;
            }

            set {
                this.phone = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public City? City {
            get {
                return this.city;
            }

            set {
                this.city = value;
            }
        }
        #endregion

        #region private properties
        #endregion

    }
}
