using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace BgmRodotec.Globus.DataAccess {

    using Framework;
    using Framework.Configuration;
    
    using Comum;
    using BgmRodotec.FrameWork;

    public class ListasFactory{

        #region IConcedente  Members

        public Concedente[] ListadeConcedentes()
        {

            Concedente[] target = null;

            DatabaseProduct databaseProduct = (new DatabaseSettings()).CurrentDatabaseProduct;


            switch (databaseProduct)
            {
                case DatabaseProduct.Oracle10G:
                    {
                        target = new Oracle.Listas.ConcedenteLista().ListadeConcedentes();
                        break;
                    }
                case DatabaseProduct.SqlServer2008:
                    {
                        target = new SqlServer.Listas.ConcedenteLista().ListadeConcedentes();
                        break;
                    }
            }
            return target;
        }

        #endregion
        
/*
        #region IPaises Members

        public Pais[] ListadePaises() {

            Pais[] target = null;

            DatabaseProduct databaseProduct = (new DatabaseSettings()).CurrentDatabaseProduct;


            switch ( databaseProduct ) {
                case DatabaseProduct.Oracle10G: {
                        target = new Oracle.Listas.Paises().ListadePaises();
                        break;
                    }
                case DatabaseProduct.SqlServer2008: {
                        target = new SqlServer.Listas.Paises().ListadePaises();

                        break;
                    }
            }
            return target;
        }

        #endregion
 */
    }
}
