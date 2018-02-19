using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BgmRodotec.FrameWork;

namespace BgmRodotec.Globus.Comum
{
    /// <summary>
    /// Entidade que contém todas as Razões Sociais e CNPJ's
    /// autorizados ao uso do Globus
    /// </summary>

    class EmpresaAutorizada : BaseCadastro
    {
        #region private fields

        /// <summary>
        /// Identificador do concedente que autorizou o uso do Globus.
        /// </summary>
        private Concedente concedente;

        /// <summary>
        /// Identificador do registro no database.
        /// </summary>
        private UInt64 codIntEmpAut;

        /// <summary>
        /// Representa a chave de seguranca gerada pela BgmRodotec garantindo que
        /// é uma empresa válida.
        /// </summary>
        private String checksumEmpresa;

        /// <summary>
        /// Razão Social da empresa autorizada.
        /// </summary>
        private String rSocialEmpresa;

        /// <summary>
        /// Nome Fantasia da empresa autorizada.
        /// </summary>
        private String nomeFantasiaEmpresa;

        /// <summary>
        /// Tipo de inscrição (CNPJ, CPF, CEI ...) da empresa autorizada.
        /// </summary>
        private String tipoInscricaoEmpAut;

        /// <summary>
        /// Número da instrição da empresa autorizada.
        /// </summary>
        private String inscricaoEmpresa;

        #endregion

        #region constructors

        /// <summary>
        /// Construtor default.
        /// </summary>
        public EmpresaAutorizada()
		{
            concedente = new Concedente();
            codIntEmpAut = 0;
            checksumEmpresa = null;
            rSocialEmpresa = null;
            nomeFantasiaEmpresa = null;
            inscricaoEmpresa = null;
            tipoInscricaoEmpAut = null; 
		}

        #endregion

        #region properties

        /// <summary>
        /// Identificador do concedente que autorizou o uso do Globus.
        /// </summary>
        public Concedente Concedente
        {
            get
            {
                return this.concedente;
            }
            set
            {
                this.concedente = value;
            }
        }

        /// <summary>
        /// Identificador do registro no database.
        /// </summary>
        public UInt64 CodIntEmpAut
        {
            get
            {
                return this.codIntEmpAut;
            }
            set
            {
                this.codIntEmpAut = value;
            }
        }

        /// <summary>
        /// Representa a chave de seguranca gerada pela BgmRodotec garantindo que
        /// é uma empresa válida.
        /// </summary>
        public String ChecksumEmpresa
        {
            get
            {
                return this.checksumEmpresa;
            }
            set
            {
                this.checksumEmpresa = value;
            }
        }

        /// <summary>
        /// Razão Social da empresa autorizada.
        /// </summary>
        public String RSocialEmpresa
        {
            get
            {
                return this.rSocialEmpresa;
            }
            set
            {
                this.rSocialEmpresa = value;
            }
        }

        /// <summary>
        /// Nome Fantasia da empresa autorizada.
        /// </summary>
        public String NomeFantasiaEmpresa
        {
            get
            {
                return this.nomeFantasiaEmpresa;
            }
            set
            {
                this.nomeFantasiaEmpresa = value;
            }
        }

        /// <summary>
        /// Tipo de inscrição (CNPJ, CPF, CEI ...) da empresa autorizada.
        /// </summary>
        public String TipoInscricaoEmpAut
        {
            get
            {
                return this.tipoInscricaoEmpAut;
            }
            set
            {
                this.tipoInscricaoEmpAut = value;
            }
        }

        /// <summary>
        /// Número da instrição da empresa autorizada.
        /// </summary>
        public String InscricaoEmpresa
        {
            get
            {
                return this.inscricaoEmpresa;
            }
            set
            {
                this.inscricaoEmpresa = value;
            }
        }

        #endregion

    }
}
