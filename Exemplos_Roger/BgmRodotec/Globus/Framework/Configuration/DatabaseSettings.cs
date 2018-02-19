using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Diagnostics;

namespace BgmRodotec.Globus.Framework.Configuration {

    using Comum;
    using BgmRodotec.FrameWork;

    /// <summary>
    /// Gerencia a sessão do app.config com as informações
    /// de configuração de banco de dados.
    /// </summary>
    public class DatabaseSettings {

        #region private constants
        private const string CurrentDatabase = "CurrentDatabaseProduct";
        #endregion

        #region private behavior
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static  DatabaseProduct ReadConfig( String key ) {

            DatabaseProduct value = DatabaseProduct.Oracle9i;

            try {

                switch ( ConfigurationSettings.AppSettings[key] ) {
                    case "Oracle": {
                            value = DatabaseProduct.Oracle10G;
                            break;
                        }
                }

            } catch ( Exception exception ) {
#if DEBUG
                Debug.WriteLine( "Oopss: " + exception.Message );
#endif
            }
            
            return value;
        }
        #endregion
        
        #region public properties
        /// <summary>
        /// Retorna o nome do database utilizado para a instalação 
        /// do sistema.
        /// </summary>
        public DatabaseProduct CurrentDatabaseProduct {
            get {
                return DatabaseSettings.ReadConfig( DatabaseSettings.CurrentDatabase );
            }
        }
        #endregion

    }
}
