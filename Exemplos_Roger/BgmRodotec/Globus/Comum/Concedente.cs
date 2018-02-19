using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BgmRodotec.FrameWork;

namespace BgmRodotec.Globus.Comum
{
    /// <summary>
    /// Concedente do Globus 
    /// </summary>
    public class Concedente : BaseCadastro
    {
        #region private fields

        /// <summary>
        /// Identificador do registro no database.
        /// </summary>
        private UInt64 codIntConcedente;

        /// <summary>
        /// Representa a chave de seguranca gerada pela BgmRodotec garantindo que
        /// é um concedente válido.
        /// </summary>
        private String checksumConcedente;

        /// <summary>
        /// Nome do concedente que autoriza o uso do Globus.
        /// Hoje somente temos a concessão do uso do Globus pela BgmRodotec, mas
        /// antigamente tinhamos esta concessão gerada pela Blusys, Rodotec,
        /// iremos manter esta informação como Cadastral
        /// </summary>
        private String nomeConcedente;


        /// <summary>
        /// Código interno do cliente que está utilizando o Globus, este código é gerado
        /// pelo Sistema Interno BgmRodotec, e é um identificador tanto para a recepção 
        /// de logs, quanto para validações específicas de clientes em alguns módulos
        /// </summary>
        private UInt32? codigoCliente;

        /// <summary>
        /// Chave de segurança do cliente, é utilizada para a valização de senha do 
        /// usuário chave do Globus no cliente (Manager).
        /// </summary>
        private String chaveCliente;


        #endregion

        #region constructors

        /// <summary>
        /// Construtor default.
        /// </summary>
        public Concedente() 
		{
			codIntConcedente = 0; 
			checksumConcedente = null; 
			nomeConcedente = null; 
			codigoCliente = null; 
			chaveCliente = null; 
		}

        #endregion

        #region properties
        /// <summary>
        /// Identificador do registro no database.
        /// </summary>
        public UInt64 CodIntConcedente
        {
            get
            {
                return this.codIntConcedente;
            }
            set
            {
                this.codIntConcedente = value;
            }
        }

        /// <summary>
        /// Representa a chave de seguranca gerada pela BgmRodotec garantindo que
        /// é um concedente válido.
        /// </summary>
        public String ChecksumConcedente
        {
            get
            {
                return this.checksumConcedente;
            }
            set
            {
                this.checksumConcedente = value;
            }
        }

        /// <summary>
        /// Nome do concedente que autoriza o uso do Globus.
        /// Hoje somente temos a concessão do uso do Globus pela BgmRodotec, mas
        /// antigamente tinhamos esta concessão gerada pela Blusys, Rodotec,
        /// iremos manter esta informação como Cadastral
        /// </summary>
        public String NomeConcedente
        {
            get
            {
                return this.nomeConcedente;
            }
            set
            {
                this.nomeConcedente = value;
            }
        }


        /// <summary>
        /// Código interno do cliente que está utilizando o Globus, este código é gerado
        /// pelo Sistema Interno BgmRodotec, e é um identificador tanto para a recepção 
        /// de logs, quanto para validações específicas de clientes em alguns módulos
        /// </summary>
        public UInt32? CodigoCliente
        {
            get
            {
                return this.codigoCliente;
            }
            set
            {
                this.codigoCliente = value;
            }
        }

        /// <summary>
        /// Chave de segurança do cliente, é utilizada para a valização de senha do 
        /// usuário chave do Globus no cliente (Manager).
        /// </summary>
        public String ChaveCliente
        {
            get
            {
                return this.chaveCliente;
            }
            set 
            {
                this.chaveCliente = value;
            }

        }

        #endregion

    }

    public interface IConcedente
    {
        /// <summary>
        /// Retorna uma lista simples de concedentes 
        /// de acordo com os campos da classe concedente.        
        /// </summary>
        /// <returns>Retorna uma lista de objetos do tipo Concedente ou
        /// o valor null se não foi possível recuperar as informações.</returns>        
        Concedente[] ListadeConcedentes();

    }
}
