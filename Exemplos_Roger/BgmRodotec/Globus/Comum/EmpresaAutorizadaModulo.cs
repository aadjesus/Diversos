using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BgmRodotec.FrameWork;

namespace BgmRodotec.Globus.Comum
{
    /// <summary>
    /// Entidade que contém as empresas e os módulos autorizados
    /// </summary>

    class EmpresaAutorizadaModulo
    {

        #region private fields

        /// <summary>
        /// EmpresaAutorizada.
        /// </summary>
        private EmpresaAutorizada empresaAutorizada;

        /// <summary>
        /// Modulo.
        /// </summary>
        private ModuloGlobus modulo;

        /// <summary>
        /// Data limite para uso do sistema, a partir da data a empresa terá direito 
        /// apenas a consulta dos dados
        /// </summary>
        private DateTime dataLimiteDeUso;

        /// <summary>
        /// Representa a chave de seguranca gerada pela BgmRodotec garantindo que
        /// é uma autorização válida.
        /// </summary>
        private String checkSumAutSistemas;

        #endregion

        #region constructors

        /// <summary>
        /// Construtor default.
        /// </summary>
        public EmpresaAutorizadaModulo()
		{
            empresaAutorizada = new EmpresaAutorizada();
            modulo = new ModuloGlobus();
            dataLimiteDeUso = DateTime.MinValue;
            checkSumAutSistemas = null;
		}

        #endregion

        #region properties

        /// <summary>
        /// EmpresaAutorizada.
        /// </summary>
        public EmpresaAutorizada EmpresaAutorizada
        {
            get
            {
                return this.empresaAutorizada;
            }
            set
            {
                this.empresaAutorizada = value;
            }
        }

        /// <summary>
        /// Modulo.
        /// </summary>
        public ModuloGlobus Modulo
        {
            get
            {
                return this.modulo;
            }
            set
            {
                this.modulo = value;
            }
        }

        /// <summary>
        /// Data limite para uso do sistema, a partir da data a empresa terá direito 
        /// apenas a consulta dos dados
        /// </summary>
        public DateTime DataLimiteDeUso
        {
            get
            {
                return this.dataLimiteDeUso;
            }
            set
            {
                this.dataLimiteDeUso = value;
            }
        }

        /// <summary>
        /// Representa a chave de seguranca gerada pela BgmRodotec garantindo que
        /// é uma autorização válida.
        /// </summary>
        public String CheckSumAutSistemas
        {
            get
            {
                return this.checkSumAutSistemas;
            }
            set
            {
                this.checkSumAutSistemas = value;
            }
        }

        #endregion

    }
}
